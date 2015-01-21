'09-dec-2011 Fatima Tajammal CR#136 GL Company sould be deleted if no voucher eixts
'22-Oct-2014      M.Shoaib        CR # 332  Data log: Datalog should prepare properly for all configuration of Lightwave
'14-Nov-2014      M.Shoaib        CR # 339  Lightwave: Error on New company definition
Imports Model
Imports System.Data.SqlClient
Imports Utility.Utility

Public Class CompanyDAL

    Public MaxLocId As Integer

    Private Enum EnumMode

        NewMode = 0
        UpdateMode = 1

    End Enum
#Region "Local Functions and Procedures"


#End Region

#Region "Public Functions and Procedures"

    Public Function Add(ByVal objCompany As Company) As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction

        Try

            Dim strSQL As String


            strSQL = "Insert into TblGlDefLocation(location_name, location_code, sort_order, comments, location_address, location_phone, location_fax, location_url ) " & _
                     " Values('" & objCompany.CompanyName & "', '" & objCompany.CompanyCode & "', " & IIf(objCompany.SortOrder = 0, "NULL", objCompany.SortOrder) & ", '" & objCompany.Comments & "', '" & objCompany.CompanyAddress & "','" & objCompany.CompanyPhone & "','" & objCompany.CompanyFax & "','" & objCompany.CompanyURL & "' ) " & _
                     " Select Ident_Current('TblGlDefLocation')"

            ''Execute SQL 
            objCompany.CompanyID = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing))

            'Building query for data log
            strSQL = "Insert into TblGlDefLocation(location_id,location_name, location_code, sort_order, comments, location_address, location_phone, location_fax, location_url ) " & _
                     " Values(" & objCompany.CompanyID & ",'" & objCompany.CompanyName & "', '" & objCompany.CompanyCode & "', " & IIf(objCompany.SortOrder = 0, "NULL", objCompany.SortOrder) & ", '" & objCompany.Comments & "', '" & objCompany.CompanyAddress & "','" & objCompany.CompanyPhone & "','" & objCompany.CompanyFax & "','" & objCompany.CompanyURL & "' ) "

            ''SQL Statement Log
            objCompany.ActivityLog.SQLType = "INSERT"
            objCompany.ActivityLog.TableName = "TblGlDefLocation "
            objCompany.ActivityLog.SQL = strSQL
            UtilityDAL.BuildSQLLog(objCompany.ActivityLog, trans, True)


            'Rights of this location to the lumensoft default user
            If funRightsUserLocation(objCompany, trans) = False Then
                Throw New Exception("Error while assigning Rights to lumensoft default user")
            End If


            If funAssociateYear(objCompany, trans) = False Then
                Throw New Exception("Error while associating Year with company")
            End If

          

            ''Activity Log
            objCompany.ActivityLog.FormAction = "Save"
            UtilityDAL.BuildActivityLog(objCompany.ActivityLog, trans)

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
    Private Function funRightsUserLocation(ByVal objCompany As Company, ByVal trans As SqlTransaction) As Boolean

        Dim strSQL As String

        Try

            strSQL = "SELECT ISNULL(MAX(location_id),0) as MaxLoc FROM tblGlDefLocation"

            MaxLocId = 0

            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL, trans)
            'Using objDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

            If dt.Rows.Count > 0 Then
                MaxLocId = CInt(dt.Rows(0).Item("MaxLoc").ToString)
            Else
                MaxLocId = 0
            End If

            'End Using
            'Give rights of this new location to the lumensoft default user.
            'strSQL = "INSERT INTO tblGLSecurityUserLocation ([user_id], location_id) VALUES(1," & MaxLocId & ")"       
            ' CR # 339      Current user's user id in passed instead of Default user id 1.
            strSQL = "INSERT INTO tblGLSecurityUserLocation ([user_id], location_id) VALUES(" & objCompany.ActivityLog.UserID & "," & MaxLocId & ")"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)

            ''SQL Statement Log
            objCompany.ActivityLog.SQLType = "INSERT"
            objCompany.ActivityLog.TableName = "tblGLSecurityUserLocation "
            objCompany.ActivityLog.SQL = strSQL
            UtilityDAL.BuildSQLLog(objCompany.ActivityLog, trans)

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function funAssociateYear(ByVal objCompany As Company, ByVal trans As SqlTransaction) As Boolean
        Dim strSQL As String
        Dim objDA As SqlDataAdapter
        Dim DT As New DataTable

        Try

            Dim objFyear As FiniancialYear

            strSQL = "SELECT distinct tblGlDefFinancialYear.financial_year_id [FyearID] FROM tblGLDefFinancialyear " & _
                     "  Inner Join tblGlDefFinancialYearStatus " & _
                     "   on tblGlDefFinancialYear.financial_year_id =tblGlDefFinancialYearStatus.financial_year_id " & _
                     "   Where tblGlDefFinancialYearStatus.status='Open'"

            objDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)
            objDA.Fill(DT)

            If DT.Rows.Count > 0 Then
                For Each Row As DataRow In DT.Rows
                    objFyear = New FiniancialYear

                    strSQL = "INSERT INTO tblGlDefFinancialYearStatus (financial_year_id, status, location_id) VALUES  ( " & Row.Item("FyearID").ToString & ",'Open'," & MaxLocId & ")" & _
                            " Select Ident_Current('tblGlDefFinancialYearStatus')"

                    'CR#160
                    'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)
                    ''Execute SQL 
                    objFyear.FYearStatusID = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing))

                    strSQL = "INSERT INTO tblGlDefFinancialYearStatus (financial_year_status_id,financial_year_id, status, location_id) " & _
                        " VALUES  (" & objFyear.FYearStatusID & ", " & Row.Item("FyearID").ToString & ",'Open'," & MaxLocId & ")"

                    ''SQL Statement Log
                    objCompany.ActivityLog.SQLType = "INSERT"
                    objCompany.ActivityLog.TableName = "tblGlDefFinancialYearStatus"
                    objCompany.ActivityLog.SQL = strSQL
                    UtilityDAL.BuildSQLLog(objCompany.ActivityLog, trans, True)

                Next
            End If

            Return True


        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Deleted(ByVal objCompany As Company) As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction

        Try

            Dim strSQL As String

            'CR# 24
            strSQL = "Delete from tblGLSecurityUserLocation where location_id =" & objCompany.CompanyID
            ''Execute SQL 

            SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing)
            'SQL Statement Log
            objCompany.ActivityLog.SQLType = "DELETE"
            objCompany.ActivityLog.TableName = "tblGLSecurityUserLocation"
            objCompany.ActivityLog.SQL = strSQL
            ' CR # 332
            'UtilityDAL.BuildSQLLog(objCompany.ActivityLog, trans)
            UtilityDAL.BuildSQLLog(objCompany.ActivityLog, trans, True)

            'CR#136
            strSQL = "Delete from tblGlDefFinancialYearStatus where location_id =" & objCompany.CompanyID
            ''Execute SQL 

            SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing)
            'SQL Statement Log
            objCompany.ActivityLog.SQLType = "DELETE"
            objCompany.ActivityLog.TableName = "tblGlDefFinancialYearStatus"
            objCompany.ActivityLog.SQL = strSQL
            ' CR # 332
            'UtilityDAL.BuildSQLLog(objCompany.ActivityLog, trans)
            UtilityDAL.BuildSQLLog(objCompany.ActivityLog, trans, True)


            strSQL = "Delete from TblGlDefLocation where location_id = " & objCompany.CompanyID

            ''Execute SQL 
            SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing)

            'SQL Statement Log
            objCompany.ActivityLog.SQLType = "DELETE"
            objCompany.ActivityLog.TableName = "TblGlDefLocation"
            objCompany.ActivityLog.SQL = strSQL
            ' CR # 332
            'UtilityDAL.BuildSQLLog(objCompany.ActivityLog, trans)
            UtilityDAL.BuildSQLLog(objCompany.ActivityLog, trans, True)

            ''Activity Log
            objCompany.ActivityLog.FormAction = "Delete"
            UtilityDAL.BuildActivityLog(objCompany.ActivityLog, trans)

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

            If Not strCondition = Nothing Then
                strSQL = "SELECT tblGlDefLocation.location_id AS [Company ID], tblGlDefLocation.location_code as [Company Code], tblGlDefLocation.location_name as [Company Name], tblGlDefLocation.location_phone as [Phone], tblGlDefLocation.location_fax as [Fax], tblGlDefLocation.location_url as [URL], tblGlDefLocation.location_address as [Address], tblGlDefLocation.sort_order as [Sort Order], tblGlDefLocation.comments as Comments From tblGlDefLocation Inner Join tblGlSecurityUserLocation on tblGlDefLocation.Location_Id= tblGlSecurityUserLocation.Location_Id  Where User_Id=" & strCondition & ""
            Else
                strSQL = "SELECT location_id AS [Company ID], location_code as [Company Code],location_name as [Company Name], location_phone as [Phone], location_fax as [Fax], location_url as [URL], location_address as [Address], sort_order as [Sort Order], comments as Comments From TblGlDefLocation ORDER BY sort_order, location_name"
            End If

            objDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Dim MyCollectionList As New DataTable("Company")
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
            strSQL = "Select count(*) as TotalComp from TblGlDefLocation"

            intTotalcompanies = 0
            Using objDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

                If objDR.HasRows Then

                    While objDR.Read
                        intTotalcompanies = CInt(objDR.Item("TotalComp"))
                    End While

                End If

            End Using

            If intTotalcompanies < 1 Then

                Return True
            Else
                '#To DO
                Dim tru As String = String.Empty
                tru = Utility.Utility.SymmetricEncryption.Decrypt(SystemConfigurationDAL.GetSystemConfigurationValue("Multi_Company"), "f")
                Return IIf(tru.ToUpper = "TRUE", True, False)
                Return True
            End If




        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ISRecordAlreadyExist(ByVal objCompany As Company, ByVal Mode As Integer) As Boolean

        Dim strSQL As String
        Dim intTotalcompanies As Integer

        Try

            If Mode = EnumMode.NewMode Then

                strSQL = "select Count(*) as AlreadyDefined from TblGlDefLocation Where (location_code='" & objCompany.CompanyCode & "' or location_name='" & objCompany.CompanyName & "') "

                Using objDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

                    If objDR.HasRows Then

                        While objDR.Read
                            intTotalcompanies = CInt(objDR.Item("AlreadyDefined"))
                        End While

                    End If

                End Using

                If intTotalcompanies > 0 Then

                    Return True
                Else

                    Return False
                End If

            ElseIf Mode = EnumMode.UpdateMode Then

                strSQL = "select Count(*) as AlreadyDefined from TblGlDefLocation where (location_code='" & objCompany.CompanyCode & "' or location_name='" & objCompany.CompanyName & "') and location_id <> " & objCompany.CompanyID

                Using objDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

                    If objDR.HasRows Then

                        While objDR.Read
                            intTotalcompanies = CInt(objDR.Item("AlreadyDefined"))
                        End While

                    End If

                End Using

                If intTotalcompanies > 0 Then

                    Return True
                Else

                    Return False
                End If


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
    Public Function FYearExist() As Boolean
        Dim strSQL As String
        Dim intFyear As Integer

        Try

            strSQL = "select count(*) as FYears from tblGlDefFinancialYear"

            Using objDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

                If objDR.HasRows Then

                    While objDR.Read
                        intFyear = CInt(objDR.Item("FYears"))
                    End While

                End If

            End Using

            If intFyear = 0 Then
                Return False
            Else
                Return True
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function Update(ByVal ObjCompany As Company) As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction
    
        Try

            Dim strSQL As String
            strSQL = "Update TblGlDefLocation Set location_name ='" & ObjCompany.CompanyName & "', location_code ='" & ObjCompany.CompanyCode & "', sort_order = " & ObjCompany.SortOrder & " , comments ='" & ObjCompany.Comments & "', location_address ='" & ObjCompany.CompanyAddress & "', location_phone ='" & ObjCompany.CompanyPhone & "', location_fax ='" & ObjCompany.CompanyFax & "', location_url='" & ObjCompany.CompanyURL & "' where location_id = " & ObjCompany.CompanyID

            SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing)

            'SQL Statement Log
            ObjCompany.ActivityLog.SQLType = "UPDATE"
            ObjCompany.ActivityLog.TableName = "TblGlDefLocation"
            ObjCompany.ActivityLog.SQL = strSQL
            UtilityDAL.BuildSQLLog(ObjCompany.ActivityLog, trans, True)

            ' ''Activity Log
            ObjCompany.ActivityLog.FormAction = "UPDATE"
            UtilityDAL.BuildActivityLog(ObjCompany.ActivityLog, trans)

            '    ''Commit Traction
            trans.Commit()

            '    ''Return
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

    'CR# 136
    Public Function GetCompanyID(ByVal objCompany As Company) As Boolean
        Try
            Dim conn As New SqlConnection(SQLHelper.CON_STR)
            conn.Open()
            Dim rd As SqlDataReader
            Dim trans As SqlTransaction = conn.BeginTransaction
            Dim strSQl As String
            strSQl = "Select * From dbo.tblGlVoucher Where location_id=" & objCompany.CompanyID & " "
            rd = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQl, Nothing)
            If rd.HasRows Then
                Return False
            Else
                Return True
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region



End Class
