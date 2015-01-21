Imports Model
Imports Utility.Utility
Imports System.Data.SqlClient

Public Class GroupRightsDAL

#Region "Local Functions and Procedures"

#End Region

#Region "Public Functions and Procedures"
   
    ''' <summary>
    ''' 
    ''' </summary>
    Public Function Update(ByVal objRights As List(Of SecurityGroupRights)) As Boolean

        Try
            ''check if rights collection is empty
            If objRights Is Nothing Then Return False
            If objRights.Count = 0 Then Return False

            Dim conn As New SqlConnection(SQLHelper.CON_STR)
            conn.Open()
            Dim trans As SqlTransaction = conn.BeginTransaction

            Try

                Dim strInsertSQL As String = "Insert Into tblGLSecurityControlRight ( GroupID , ControlID) " _
                & " Values(@GroupID , @ControlID)"

                Dim strDeleteSQL As String = "Delete From tblGLSecurityControlRight " _
                & " Where GroupID = @GroupID AND ControlID = @ControlID"


                ''setting parameters for the command
                Dim prms(1) As SqlParameter
                prms(0) = SQLHelper.CreateParameter("@GroupID", SqlDbType.BigInt, objRights(0).GroupInfo.GroupID)
                prms(1) = SQLHelper.CreateParameter("@ControlID", SqlDbType.BigInt, Nothing)

                ''iterate the rights collection
                For Each r As SecurityGroupRights In objRights
                    ''assiging values to parameters
                    prms(1).Value = r.ControlID

                    ''delete existing records
                    Convert.ToInt32(SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strDeleteSQL, prms))
                    ''buil sql Log
                    objRights(0).ActivityLog.SQLType = "DELETE"
                    objRights(0).ActivityLog.TableName = "TblGLSecurityUser"
                    objRights(0).ActivityLog.SQL = strInsertSQL
                    UtilityDAL.BuildSQLLog(objRights(0).ActivityLog, trans)

                    If r.IsSelected = True Then
                        Convert.ToInt32(SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strInsertSQL, prms))
                        ''buil sql Log
                        objRights(0).ActivityLog.SQLType = "INSERT"
                        objRights(0).ActivityLog.TableName = "TblGLSecurityUser"
                        objRights(0).ActivityLog.SQL = strInsertSQL
                        UtilityDAL.BuildSQLLog(objRights(0).ActivityLog, trans)
                    End If
                Next

                ''Activity Log
                objRights(0).ActivityLog.FormAction = "Update"
                UtilityDAL.BuildActivityLog(objRights(0).ActivityLog, trans)

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

        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try


    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    Public Function GetAll(ByVal GroupID As Integer) As DataTable
        Dim objDA As SqlClient.SqlDataAdapter
        Try
            Dim strSQL As String
            strSQL = "SELECT     tblGLSecurityFormControl.ControlID as [Control ID]," _
            & " tblGLSecurityForm.FORM_LABEL as [Form Label], tblGLSecurityFormControl.ControlCaption [Control Caption], " _
            & " convert(bit, CASE WHEN tblGLSecurityControlRight.RightsID IS NULL THEN 0 ELSE 1 END) AS [Is Selected]" _
            & " FROM         tblGLSecurityFormControl INNER JOIN" _
            & " tblGLSecurityForm ON tblGLSecurityFormControl.FormID = tblGLSecurityForm.FORM_ID LEFT OUTER JOIN" _
            & " (SELECT     GroupID, ControlID, RightsID" _
            & " FROM tblGLSecurityControlRight" _
            & " WHERE      GroupID = " & GroupID & " ) tblGLSecurityControlRight ON tblGLSecurityControlRight.ControlID = " _
            & " tblGLSecurityFormControl.ControlID " _
            '& " WHERE tblGLSecurityForm.IsInUse = 1 "
            '& " ORDER BY tblGLSecurityForm.SortOrder"

            objDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Dim MyCollectionList As New DataTable("GroupRights")
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


#End Region



End Class
