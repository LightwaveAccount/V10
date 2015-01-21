Imports Model
Imports System.Data.SqlClient
Imports Utility.Utility

Public Class GroupDAL

#Region "Local Functions and Procedures"
   
    Public Function IsValidateForSave(ByVal objGroup As SecurityGroup) As Boolean

        Try
            Dim strSQL As String
            strSQL = "SELECT      GROUP_NAME " _
            & " FROM         tblGLSecurityGroup " _
            & " WHERE GROUP_NAME = '" & objGroup.GroupName.Replace("'", "''") & "' AND GROUP_ID <> '" & objGroup.GroupID & "'"


            Using objNameDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

                If objNameDR.HasRows Then

                    Throw New Exception(gstrMsgDuplicateName)

                End If

                Return True

            End Using


        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function IsValidateForDelete(ByVal objGroup As SecurityGroup) As Boolean

        Try
            Dim strSQL As String

            ''Check in tblGLSecurityUser
            strSQL = "SELECT     *  " _
            & " FROM         tblGLSecurityUser " _
            & " WHERE group_id = '" & objGroup.GroupID & "'"

            Using objNameDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

                If objNameDR.HasRows Then
                    Throw New Exception(gstrMsgDependentRecordExist & " (tblGLSecurityUser)")
                End If

            End Using

            ''========
            ''Check in tblGLSecurityFormRight
            strSQL = "SELECT     *  " _
            & " FROM         tblGLSecurityFormRight " _
            & " WHERE group_id = '" & objGroup.GroupID & "'"

            Using objNameDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

                If objNameDR.HasRows Then
                    Throw New Exception(gstrMsgDependentRecordExist & " (tblGLSecurityFormRight)")
                End If

            End Using

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region

#Region "Public Functions and Procedures"

    Public Function Add(ByVal objGroup As SecurityGroup) As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction

        Try

            Dim strSQL As String = ""

            strSQL = "insert into tblGLSecurityGroup ( group_name, group_comments ) " _
            & " VALUES ( '" & objGroup.GroupName.Trim.Replace("'", "''") & "','" & objGroup.GroupComments.Trim.Replace("'", "''") & "')" _
            & " Select Ident_Current('tblGLSecurityGroup')"

            ''Execute SQL 
            objGroup.GroupID = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing))

            ''SQL Statement Log
            objGroup.ActivityLog.SQLType = "INSERT"
            objGroup.ActivityLog.TableName = "tblGLSecurityGroup"
            objGroup.ActivityLog.SQL = strSQL
            UtilityDAL.BuildSQLLog(objGroup.ActivityLog, trans)

            ''Activity Log
            objGroup.ActivityLog.FormAction = "Save"
            UtilityDAL.BuildActivityLog(objGroup.ActivityLog, trans)

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

    Public Function Update(ByVal objGroup As SecurityGroup) As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction

        Try

            Dim strSQL As String

            strSQL = ""
            strSQL = "UPDATE tblGLSecurityGroup SET " _
            & " GROUP_NAME = '" & objGroup.GroupName.Trim.Replace("'", "''") & "', " _
            & " GROUP_COMMENTS = '" & objGroup.GroupComments.Trim.Replace("'", "''") & "'  " _
            & " WHERE group_id = " & objGroup.GroupID

            ''Execute SQL 
            Convert.ToInt32(SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing))


            ''SQL Statement Log
            objGroup.ActivityLog.SQLType = "UPDATE"
            objGroup.ActivityLog.TableName = "tblGLSecurityGroup"
            objGroup.ActivityLog.SQL = strSQL
            UtilityDAL.BuildSQLLog(objGroup.ActivityLog, trans)

            ''Activity Log
            objGroup.ActivityLog.FormAction = "Update"
            UtilityDAL.BuildActivityLog(objGroup.ActivityLog, trans)


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

   
    Public Function Deleted(ByVal objGroup As SecurityGroup) As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction

        Try

            Dim strSQL As String

            strSQL = "DELETE FROM tblGLSecurityGroup " _
            & " WHERE group_id = " & objGroup.GroupID

            ''Execute SQL 
            Convert.ToInt32(SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing))

            ''SQL Statement Log
            objGroup.ActivityLog.SQLType = "DELETE"
            objGroup.ActivityLog.TableName = "tblGLSecurityGroup"
            objGroup.ActivityLog.SQL = strSQL
            UtilityDAL.BuildSQLLog(objGroup.ActivityLog, trans)

            ''Activity Log
            objGroup.ActivityLog.FormAction = "Delete"
            UtilityDAL.BuildActivityLog(objGroup.ActivityLog, trans)

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
            strSQL = "SELECT tblglSecurityGroup.GROUP_ID as [Group ID]," _
            & " tblglSecurityGroup.GROUP_NAME as  [Group Name]," _
            & " tblglSecurityGroup.GROUP_COMMENTS as  Comments " _
            & "   FROM    tblGLSecurityGroup " _
            & " ORDER BY [Group Name]   "

            objDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Dim MyCollectionList As New DataTable("SecurityGroups")
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
