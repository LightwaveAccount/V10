Imports Model
Imports System.Data.SqlClient
Imports Utility.Utility
''// Date Modified     Modified by         Brief Description			                
''//------------------------------------------------------------------------------------
''//22-Oct-2014      M.Shoaib        CR # 332  Data log: Datalog should prepare properly for all configuration of Lightwave
''/////////////////////////////////////////////////////////////////////////////////////////
Public Class FiniancialYearDefDAL

    Public Const G_STATUS_CLOSE As String = "Closed"                        'Use this variable to set status to Close

#Region "Local Functions and Procedures"
   

#End Region

#Region "Public Functions and Procedures"

    Public Function Add(ByVal objFyear As FiniancialYear) As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction

        Try


            Dim strSQL As String

            '//Deleting trigger if exist ,this trigger was used to mark Financial Year Status after defining Financial Year
            strSQL = "if  exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[trg_Location_Fyears]') and OBJECTPROPERTY(id, N'IsTrigger') = 1) Drop trigger [dbo].[trg_Location_Fyears]"

            ''Execute SQL 
            SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing)


            '---Saving Financial Year
            strSQL = "INSERT INTO tblGlDefFinancialYear (start_date, end_date, year_code) " & _
                    "  VALUES     (Convert(datetime,'" & Format(objFyear.StartDate, "dd-MMM-yyyy hh:m tt") & "',108),Convert(datetime,'" & Format(objFyear.EndDate, "dd-MMM-yyyy hh:m tt") & "',108),'" & objFyear.YearCode & "') " _
                    & " Select Ident_Current('tblGlDefFinancialYear')"

            ''Execute SQL 
            objFyear.FYearID = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing))

            'composing Sql query for log/datalog
            strSQL = "INSERT INTO tblGlDefFinancialYear (financial_year_id,start_date, end_date, year_code) " & _
                    "  VALUES     (" & objFyear.FYearID & ",Convert(datetime,'" & Format(objFyear.StartDate, "dd-MMM-yyyy hh:m tt") & "',108),Convert(datetime,'" & Format(objFyear.EndDate, "dd-MMM-yyyy hh:m tt") & "',108),'" & objFyear.YearCode & "')"


            '--Inserting log/data log
            ''SQL Statement Log
            objFyear.ActivityLog.SQLType = "INSERT"
            objFyear.ActivityLog.TableName = "tblGlDefFinancialYear"
            objFyear.ActivityLog.SQL = strSQL
            UtilityDAL.BuildSQLLog(objFyear.ActivityLog, trans, True)

         

            'Geting all defined locations and marking currently inserted Financial year status 
            strSQL = "SELECT Location_ID FROM tblGLDefLocation"

            Dim LocationId As Integer

            Using objDR As SqlClient.SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

                If objDR.HasRows Then
                    While objDR.Read

                        LocationId = Convert.ToInt16((objDR.Item("Location_ID")))

                        '--Inserting in to Financial Year status table
                        strSQL = "INSERT INTO tblGlDefFinancialYearStatus " & _
                             " (financial_year_id, status, location_id) " & _
                             " VALUES  ( " & objFyear.FYearID & ",'Open'," & LocationId & ")" & _
                             " Select Ident_Current('tblGlDefFinancialYearStatus')"

                        ''Execute SQL 
                        objFyear.FYearStatusID = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing))

                        '--composing query for Financial Year status log
                        strSQL = "INSERT INTO tblGlDefFinancialYearStatus " & _
                            " (financial_year_status_id,financial_year_id, status, location_id) " & _
                            " VALUES  ( " & objFyear.FYearStatusID & "," & objFyear.FYearID & ",'Open'," & LocationId & ")"


                        ''SQL Statement Log
                        objFyear.ActivityLog.SQLType = "INSERT"
                        objFyear.ActivityLog.TableName = "tblGlDefFinancialYearStatus"
                        objFyear.ActivityLog.SQL = strSQL
                        UtilityDAL.BuildSQLLog(objFyear.ActivityLog, trans, True)


                    End While

                End If

            End Using


            ''Activity Log
            objFyear.ActivityLog.FormAction = "Save"
            UtilityDAL.BuildActivityLog(objFyear.ActivityLog, trans)

            ''Commit Traction
            trans.Commit()

            ''Return
            Return True

        Catch ex As SqlException
            trans.Rollback()
            Throw ex
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try

    End Function
    Public Function Deleted(ByVal objFYear As FiniancialYear) As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction

        Try

            Dim strSQL As String

            strSQL = "Delete from tblGLDefFinancialYearStatus where financial_year_id = " & objFYear.FYearID

            ''Execute SQL 
            SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing)

            'SQL Statement Log
            objFYear.ActivityLog.SQLType = "DELETE"
            objFYear.ActivityLog.TableName = "tblGLDefFinancialYearStatus"
            objFYear.ActivityLog.SQL = strSQL
            ' CR # 332
            'UtilityDAL.BuildSQLLog(objFYear.ActivityLog, trans)
            UtilityDAL.BuildSQLLog(objFYear.ActivityLog, trans, True)

            ' delete all fyear status records
            strSQL = "Delete from tblGLDefFinancialYear where financial_year_id = " & objFYear.FYearID
            ''Execute SQL 
            Convert.ToInt32(SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing))

            ''SQL Statement Log
            objFYear.ActivityLog.SQLType = "DELETE"
            objFYear.ActivityLog.TableName = "tblGLDefFinancialYear"
            objFYear.ActivityLog.SQL = strSQL
            'CR # 332
            'UtilityDAL.BuildSQLLog(objFYear.ActivityLog, trans)
            UtilityDAL.BuildSQLLog(objFYear.ActivityLog, trans, True)

            ''Activity Log
            objFYear.ActivityLog.FormAction = "Delete"
            UtilityDAL.BuildActivityLog(objFYear.ActivityLog, trans)

            ''Commit Traction
            trans.Commit()

            ''Return
            Return True

        Catch ex As SqlException
            trans.Rollback()
            Throw ex
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function
    Public Function GetAll(Optional ByVal strCondition As String = "") As DataTable
        Dim objDA As SqlClient.SqlDataAdapter
        Try

            Dim strSQL As String
            strSQL = "SELECT financial_year_id as [FYear ID] , start_date as [Start Date] , end_date as [End Date] , year_code as [FYear Code] From tblGLDefFinancialYear ORDER BY year_code DESC"

            objDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Dim MyCollectionList As New DataTable("FYearList")
            objDA.Fill(MyCollectionList)

            Return MyCollectionList

        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        Finally
            objDA = Nothing
        End Try

    End Function

    Public Function IsValidateForSave() As Boolean

        Dim intTotalcompanies As Integer

        Try

            Dim strSQL As String
            strSQL = "Select location_id from tblGlDefLocation"

            intTotalcompanies = 0
            Using objDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

                If objDR.HasRows Then

                    While objDR.Read
                        intTotalcompanies += 1
                    End While

                End If

            End Using

            If intTotalcompanies > 0 Then
                Return True
            Else
                Return False
            End If


        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function funOnlyOpenYear() As Boolean
        Dim strSQL As String

        Try

            strSQL = "SELECT     tblOpenYears.location_id, tblOpenYears.NoOfOpenYears, tblTotalYears.NoOfTotalYears " & _
                     " FROM         (SELECT     location_id, COUNT(Status) NoOfOpenYears " & _
                     " From tblGlDefFinancialYearStatus " & _
                     " WHERE      status = 'Open' " & _
                     " GROUP BY location_id) tblOpenYears INNER JOIN " & _
                     " (SELECT     location_id, COUNT(Status) NoOfTotalYears " & _
                     " From tblGlDefFinancialYearStatus " & _
                     " GROUP BY location_id) tblTotalYears ON tblOpenYears.location_id = tblTotalYears.location_id "


            Using objDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

                If objDR.HasRows Then
                    While objDR.Read

                        If Val(objDR.Item("NoOfOpenYears")) = 1 And Val(objDR.Item("NoOfTotalYears")) > 1 Then
                            Return True
                        Else
                            Return False
                        End If

                    End While
                End If

            End Using

            Return False

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function IsValidateForDelete(ByVal ObjFYear As FiniancialYear) As Boolean

        Try

            Dim strSQL As String

            strSQL = "SELECT * FROM tblGLDefFinancialYearStatus where financial_year_id = " & ObjFYear.FYearID & " AND StatuS = '" & G_STATUS_CLOSE & "'"

            Using objNameDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

                If objNameDR.HasRows Then
                    Return False
                End If

            End Using

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function
#End Region



End Class
