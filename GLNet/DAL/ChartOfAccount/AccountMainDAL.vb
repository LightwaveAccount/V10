''22-Oct-2014      M.Shoaib        CR # 332  Data log: Datalog should prepare properly for all configuration of Lightwave
Imports Model
Imports System.Data.SqlClient
Imports Utility.Utility


Public Class AccountMainDAL


    Public Function IsAlreadyExists(ByVal objModel As AccountMainModel, Optional ByVal strMode As String = "") As Boolean

        Try

            Dim strSQL As String
            ' Building SQL ..

            If strMode = "Update" Then
                strSQL = "select main_code from tblGLCOAMain where (main_code='" & objModel.AccountMainCode & "' OR main_title = '" & objModel.AccountMainTitle & "') AND (coa_main_id <> " & objModel.AccountMainID & ")"

            Else
                strSQL = " Select main_code from tblGLCOAMain Where (main_code='" & objModel.AccountMainCode & "' OR main_title = '" & objModel.AccountMainTitle & "')"

            End If


            Using objNameDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

                If Not objNameDR.HasRows Then
                    Return False

                Else
                    Return True

                End If
                Return True

            End Using

        Catch ex As Exception
            Throw ex

        End Try

    End Function


    Public Function Deleted(ByVal ObjModel As AccountMainModel) As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction

        Try

            Dim strSQL As String

            strSQL = " Delete from tblGLCOAMain where coa_main_id = " & ObjModel.AccountMainID


            ' Execute SQL ..
            Convert.ToInt32(SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing))

            ''SQL Statement Log
            ObjModel.ActivityLog.SQLType = "DELETE"
            ObjModel.ActivityLog.TableName = "TblDefCities"
            ObjModel.ActivityLog.SQL = strSQL
            ' CR # 332
            'UtilityDAL.BuildSQLLog(ObjModel.ActivityLog, trans)
            UtilityDAL.BuildSQLLog(ObjModel.ActivityLog, trans, True)


            ' Activity Log ..
            ObjModel.ActivityLog.FormAction = "Delete"
            UtilityDAL.BuildActivityLog(ObjModel.ActivityLog, trans)


            ' Commit Transaction .. 
            trans.Commit()


            ' Return ..
            Return True


        Catch ex As SqlException
            trans.Rollback()
            Return False
            Throw ex

        Catch ex As Exception
            trans.Rollback()
            Return False
            Throw ex

        Finally
            conn.Close()

        End Try
    End Function


    Public Function Save(ByVal ObjModel As AccountMainModel) As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()

        Dim trans As SqlTransaction = conn.BeginTransaction
        Try


            Dim strSQL As String

            strSQL = " Insert into tblGLCOAMain (main_title, main_code, main_type) Values('" _
                     & ObjModel.AccountMainTitle & "', '" & ObjModel.AccountMainCode & "','" & ObjModel.AccountMainType & "' ) "
            strSQL = strSQL & " Select Ident_Current('tblGLCOAMain')"

            ' Execute SQL  ..
            ObjModel.AccountMainID = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing))

            'CR#160
            strSQL = " Insert into tblGLCOAMain (coa_main_id,main_title, main_code, main_type)" & _
                     " Values('" & ObjModel.AccountMainID & "','" & ObjModel.AccountMainTitle & "', '" & ObjModel.AccountMainCode & "','" & ObjModel.AccountMainType & "' ) "


            ' SQL Statement Log ..
            ObjModel.ActivityLog.SQLType = "INSERT"
            ObjModel.ActivityLog.TableName = "tblGLCOAMain"
            ObjModel.ActivityLog.SQL = strSQL
            UtilityDAL.BuildSQLLog(ObjModel.ActivityLog, trans, True)

            ' Activity Log ..
            ObjModel.ActivityLog.FormAction = "Save"
            UtilityDAL.BuildActivityLog(ObjModel.ActivityLog, trans)

            ''Commit Traction
            trans.Commit()

            ' Return
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



    Public Function Update(ByVal ObjModel As AccountMainModel) As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()

        Dim trans As SqlTransaction = conn.BeginTransaction
        Try


            Dim strSQL As String

            ' Building SQL ..
            strSQL = " Update tblGLCOAMain Set main_title ='" & ObjModel.AccountMainTitle & "', main_code ='" & ObjModel.AccountMainCode & "', main_type ='" & ObjModel.AccountMainType & "' where coa_main_id = " & ObjModel.AccountMainID

            ' Execute SQL  ..
            ObjModel.AccountMainID = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing))


            ' SQL Statement Log ..
            ObjModel.ActivityLog.SQLType = "UPDATE"
            ObjModel.ActivityLog.TableName = "tblGLCOAMain"
            ObjModel.ActivityLog.SQL = strSQL
            UtilityDAL.BuildSQLLog(ObjModel.ActivityLog, trans, True)


            ' Activity Log ..
            ObjModel.ActivityLog.FormAction = "Update"
            UtilityDAL.BuildActivityLog(ObjModel.ActivityLog, trans)


            ' Commit Transaction .. 
            trans.Commit()


            ' Return
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

        Dim ObjDA As SqlClient.SqlDataAdapter

        Try

            Dim strSQL As String = ""

            strSQL = "SELECT coa_main_id, main_code, main_title, main_type From tblGLCOAMain ORDER BY main_code, main_title, main_type "
            ObjDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Dim myDataTable As New DataTable("AccountMainData")
            ObjDA.Fill(myDataTable)

            Return myDataTable

        Catch ex As SqlException
            Throw ex

        Catch ex As Exception
            Throw ex

        Finally
            ObjDA = Nothing

        End Try

    End Function

    Public Function GetNewAccountMainCode(Optional ByVal strCondition As String = "") As String

        Dim ObjDA As SqlClient.SqlDataAdapter

        Try

            Dim strSQL As String = ""

            strSQL = "SELECT max(main_code) + 1 as NewCode From tblGLCOAMain "
            ObjDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Dim myDataTable As New DataTable("AccountMainData")
            ObjDA.Fill(myDataTable)

            Return Format(myDataTable.Rows(0).Item("NewCode"), "00")

        Catch ex As SqlException
            Throw ex

        Catch ex As Exception
            Throw ex

        Finally
            ObjDA = Nothing

        End Try

    End Function

    Public Function TransactionsExist(Optional ByVal SubAccountID As String = "") As Boolean

        Dim ObjDA As SqlClient.SqlDataAdapter

        Try

            Dim strSQL As String = ""
            Dim isTransExist As Boolean = False

            strSQL = " Select count(*) from tblGlCOAMainSub where coa_main_id = " & SubAccountID
            ObjDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Using objTransDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

                If objTransDR.HasRows Then

                    objTransDR.Read()
                    If Convert.ToInt32(objTransDR.Item(0)) > 0 Then
                        isTransExist = True
                    Else
                        isTransExist = False
                    End If

                End If

                Return isTransExist

            End Using


        Catch ex As SqlException
            Throw ex

        Catch ex As Exception
            Throw ex

        Finally
            ObjDA = Nothing

        End Try

    End Function
End Class
