Imports Model
Imports System.Data.SqlClient
Imports Utility.Utility

Public Class GLCostCenterDal

    
#Region "Local Functions and Procedures"


#End Region

#Region "Public Functions and Procedures"

    Public Function Update(ByVal objCostCenter As GLCostCenter) As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction

        Try

            Dim strSQL As String

            strSQL = "update tblGlDefGLCostCenter set cost_center_title='" & objCostCenter.CostCenterTitle.ToString.Trim.Replace("'", "''") & "', cost_center_type='" & objCostCenter.CostCenterType.ToString.Trim.Replace("'", "''") & "' where cost_center_id= " & objCostCenter.CostCenterId

            ''Execute SQL 
            Convert.ToInt32(SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing))

            ''SQL Statement Log
            objCostCenter.ActivityLog.SQLType = "UPDATE"
            objCostCenter.ActivityLog.TableName = "tblGlDefGLCostCenter "
            objCostCenter.ActivityLog.SQL = strSQL
            UtilityDAL.BuildSQLLog(objCostCenter.ActivityLog, trans)

            ''Activity Log
            objCostCenter.ActivityLog.FormAction = "UPDATE"
            UtilityDAL.BuildActivityLog(objCostCenter.ActivityLog, trans)

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

    Public Function Add(ByVal objCostCenter As GLCostCenter) As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction

        Try

            Dim strSQL As String

            strSQL = "INSERT INTO tblGlDefGLCostCenter (cost_center_title, cost_center_type) " & _
                    "  VALUES     ('" & objCostCenter.CostCenterTitle.ToString.Trim.Replace("'", "''") & "','" & objCostCenter.CostCenterType.ToString.Trim.Replace("'", "''") & "') " _
                    & " Select Ident_Current('tblGlDefGLCostCenter')"

            ''Execute SQL 
            objCostCenter.CostCenterId = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing))

            ''SQL Statement Log
            objCostCenter.ActivityLog.SQLType = "INSERT"
            objCostCenter.ActivityLog.TableName = "tblGlDefGLCostCenter "
            objCostCenter.ActivityLog.SQL = strSQL
            UtilityDAL.BuildSQLLog(objCostCenter.ActivityLog, trans)

            ''Activity Log
            objCostCenter.ActivityLog.FormAction = "Save"
            UtilityDAL.BuildActivityLog(objCostCenter.ActivityLog, trans)

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

    Public Function Deleted(ByVal objCostCenter As GLCostCenter) As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction

        Try

            Dim strSQL As String

            strSQL = "Delete from tblGlDefGLCostCenter where cost_center_id = " & objCostCenter.CostCenterId

            ''Execute SQL 
            SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing)

            'SQL Statement Log
            objCostCenter.ActivityLog.SQLType = "DELETE"
            objCostCenter.ActivityLog.TableName = "tblGlDefGLCostCenter"
            objCostCenter.ActivityLog.SQL = strSQL
            UtilityDAL.BuildSQLLog(objCostCenter.ActivityLog, trans)

            ''SQL Statement Log
            objCostCenter.ActivityLog.SQLType = "DELETE"
            objCostCenter.ActivityLog.TableName = "tblGlDefGLCostCenter"
            objCostCenter.ActivityLog.SQL = strSQL
            UtilityDAL.BuildSQLLog(objCostCenter.ActivityLog, trans)

            ''Activity Log
            objCostCenter.ActivityLog.FormAction = "Delete"
            UtilityDAL.BuildActivityLog(objCostCenter.ActivityLog, trans)

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
            strSQL = "SELECT cost_center_id as [Cost Center Id], cost_center_title as [Cost Center Title] , cost_center_type as [Cost Center Type]  From tblGlDefGLCostCenter ORDER BY cost_center_title "

            objDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Dim MyCollectionList As New DataTable("CostCenter")
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

    Public Function IsValidateForSave(ByVal ObjCostCenter As GLCostCenter) As Boolean

        Dim intTotalcompanies As Integer

        Try

            Dim strSQL As String
            strSQL = "Select cost_center_id from tblGlDefGLCostCenter where cost_center_title='" & ObjCostCenter.CostCenterTitle.ToString.Replace("'", "''") & "' and cost_center_type='" & ObjCostCenter.CostCenterType.ToString.Replace("'", "''") & "' and cost_center_id <>" & ObjCostCenter.CostCenterId

            intTotalcompanies = 0
            Using objDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

                If objDR.HasRows Then
                    Throw New Exception("Cost center title with the same type already exist")
                Else
                    Return True
                End If

            End Using


        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function funOnlyOpenYear() As Boolean
        Dim strSQL As String

        Try

            strSQL = "SELECT     tblOpenYears.location_id, tblOpenYears.NoOfOpenYears, tblTotalYears.NoOfTotalYears " & _
                     " FROM         (SELECT     location_id, COUNT(Status) NoOfOpenYears " & _
                     " From tblGlDefGLCostCenterStatus " & _
                     " WHERE      status = 'Open' " & _
                     " GROUP BY location_id) tblOpenYears INNER JOIN " & _
                     " (SELECT     location_id, COUNT(Status) NoOfTotalYears " & _
                     " From tblGlDefGLCostCenterStatus " & _
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

    Public Function IsValidateForDelete(ByVal ObjCostCenter As GLCostCenter) As Boolean

        Try

            Dim strSQL As String

            strSQL = "SELECT * FROM tblGlVoucherDetail where cost_center_id = " & ObjCostCenter.CostCenterId.ToString.Replace("'", "''") & ""

            Using objNameDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

                If objNameDR.HasRows Then
                    Throw New Exception("This cost center can not be deleted, it's being used in vouchers")
                End If

            End Using

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function
#End Region



End Class


