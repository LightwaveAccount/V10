''22-Oct-2014      M.Shoaib        CR # 332  Data log: Datalog should prepare properly for all configuration of Lightwave
Imports Model
Imports System.Data.SqlClient
Imports Utility.Utility


Public Class AccountSubSubDAL

    Public Function Deleted(ByVal ObjModel As AccountSubSubModel) As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction

        Try

            Dim strSQL As String

            strSQL = " Delete from tblGlCOAMainSubSub where main_sub_sub_id = " & ObjModel.SubSubAccountID


            ' Execute SQL ..
            Convert.ToInt32(SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing))

            ' SQL Statement Log ..
            ObjModel.ActivityLog.SQLType = "DELETE"
            ObjModel.ActivityLog.TableName = "tblGlCOAMainSubSub"
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


    Public Function GetAll(Optional ByVal strCondition As String = "") As DataTable

        Dim ObjDA As SqlClient.SqlDataAdapter

        Try

            Dim strSQL As String = ""
            Dim G_SEPERATOR As String = "-"

            strSQL = " SELECT tblGlCOAMainSubSub.main_sub_sub_id, tblGlCOAMainSub.sub_title + '-' + tblGlCOAMainSub.sub_code AS sub_title, " _
                   & " tblGlCOAMainSubSub.sub_sub_code AS sub_sub_code, tblGlCOAMainSubSub.sub_sub_title, tblGlCOAMainSubSub.account_type, " _
                   & " DrBSNote.note_title AS DrBSNote, CrBSNote.note_title AS CrBSNote, PLNote.note_title AS PLNote, tblGlCOAMainSubSub.main_sub_id," _
                   & " DrBSNote.gl_note_id AS DrBSNote_id, CrBSNote.gl_note_id AS CrBSNote_id, PLNote.gl_note_id AS PLNote_id " _
                   & " From tblGLCOAMainSub INNER JOIN " _
                   & " tblGLCOAMainSubSub ON tblGLCOAMainSub.main_sub_id = tblGLCOAMainSubSub.main_sub_id LEFT OUTER JOIN" _
                   & " tblGLDefGLNotes DrBSNote ON DrBSNote.gl_note_id = tblGLCOAMainSubSub.DrBS_note_id LEFT OUTER JOIN" _
                   & " tblGLDefGLNotes CrBSNote ON CrBSNote.gl_note_id  = tblGLCOAMainSubSub.crBS_note_id LEFT OUTER JOIN" _
                   & " tblGLDefGLNotes PLNote ON PLNote.gl_note_id = tblGLCOAMainSubSub.pl_note_id" _
                   & " WHERE (tblGlCOAMainSubSub.main_sub_id = " & strCondition & ")" _
                   & " ORDER BY sub_sub_code "


            ObjDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Dim myDataTable As New DataTable("AccountMainSubSubData")
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

            strSQL = " SELECT MAX(sub_sub_code) AS NewCode FROM tblGlCOAMainSubSub GROUP BY main_sub_id HAVING (main_sub_id = " & strCondition & " ) "
            ObjDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Dim myDataTable As New DataTable("AccountSubSubData")
            ObjDA.Fill(myDataTable)


            If myDataTable.Rows.Count <> 0 AndAlso myDataTable.Rows(0).Item("NewCode").ToString <> "" Then
                Return (Val(Right(myDataTable.Rows(0).Item("NewCode").ToString, Len(myDataTable.Rows(0).Item("NewCode").ToString) - 7)) + 1).ToString.PadLeft(3, "0")
            Else
                Return Val("1").ToString.PadLeft(3, "0")
            End If



        Catch ex As SqlException
            Throw ex

        Catch ex As Exception
            Throw ex

        Finally
            ObjDA = Nothing

        End Try

    End Function


    Public Function IsAlreadyExists(ByVal ObjModel As AccountSubSubModel, Optional ByVal strMode As String = "") As Boolean

        Try

            Dim strSQL As String
            ' Building SQL ..

            If strMode = "Update" Then
                strSQL = " Select sub_sub_code from tblGLCOAMainSubSub WHERE ( sub_sub_code='" & ObjModel.SubSubAccountCode & "' or sub_sub_title='" & ObjModel.SubSubAccountTitle & "') AND main_sub_sub_id <> " & ObjModel.SubSubAccountID

            Else
                strSQL = " Select sub_sub_code from tblGLCOAMainSubSub Where ( sub_sub_code='" & ObjModel.SubSubAccountCode & "' or sub_sub_title = '" & ObjModel.SubSubAccountTitle & "') "

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

    Public Function Save(ByVal ObjModel As AccountSubSubModel) As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()

        Dim trans As SqlTransaction = conn.BeginTransaction
        Try

            Dim strSQL As String
            

            If ObjModel.PLNote = 0 Then

            End If

            strSQL = " Insert into tblGLCOAMainSubSub (main_sub_id, sub_sub_code, sub_sub_title, account_type, DrBS_note_id, CrBS_note_id, PL_note_id) " _
                   & " Values (" & ObjModel.AccountSubID & ", '" & ObjModel.SubSubAccountCode & "','" & ObjModel.SubSubAccountTitle & "', '" & ObjModel.AccountType & "'," _
                   & " " & ObjModel.DrBSNote & "," & ObjModel.CrBSNote & "," & IIf(ObjModel.PLNote = 0, "NULL", ObjModel.PLNote) & ")" _
                   & " Select Ident_Current('tblGLCOAMainSubSub')"

            ' Execute SQL  ..
            ObjModel.SubSubAccountID = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing))

            'CR#160
            strSQL = " Insert into tblGLCOAMainSubSub (main_sub_sub_id,main_sub_id, sub_sub_code, sub_sub_title, account_type, DrBS_note_id, CrBS_note_id, PL_note_id) " _
                  & " Values (" & ObjModel.SubSubAccountID & "," & ObjModel.AccountSubID & ", '" & ObjModel.SubSubAccountCode & "','" & ObjModel.SubSubAccountTitle & "', '" & ObjModel.AccountType & "'," _
                  & " " & ObjModel.DrBSNote & "," & ObjModel.CrBSNote & "," & IIf(ObjModel.PLNote = 0, "NULL", ObjModel.PLNote) & ")" _
                  & " Select Ident_Current('tblGLCOAMainSubSub')"


            ' SQL Statement Log ..
            ObjModel.ActivityLog.SQLType = "INSERT"
            ObjModel.ActivityLog.TableName = "tblGLCOAMainSubSub"
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


    Public Function Update(ByVal ObjModel As AccountSubSubModel) As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()

        Dim trans As SqlTransaction = conn.BeginTransaction
        Try


            Dim strSQL As String

            ' Building SQL ..
            strSQL = " Update tblGLCOAMainSubSub Set sub_sub_title = '" & ObjModel.SubSubAccountTitle & "', sub_sub_code ='" & ObjModel.SubSubAccountCode & "', account_type = '" & ObjModel.AccountType & "', " _
                   & " DrBS_note_id =" & ObjModel.DrBSNote & ", CrBS_note_id =" & ObjModel.CrBSNote & ", PL_note_id =" & IIf(ObjModel.PLNote = 0, "NULL", ObjModel.PLNote) & ", main_sub_id =" & ObjModel.AccountSubID & " where main_sub_sub_id = " & ObjModel.SubSubAccountID


            ' Execute SQL  ..
            ObjModel.SubSubAccountID = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing))



            ' SQL Statement Log ..
            ObjModel.ActivityLog.SQLType = "UPDATE"
            ObjModel.ActivityLog.TableName = "tblGLCOAMainSubSub"
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

    Public Function TransactionsExist(ByVal AccountSubSubId As String) As Boolean

        Dim ObjDA As SqlClient.SqlDataAdapter

        Try

            Dim strSQL As String = ""
            Dim isTransExist As Boolean = False

            strSQL = "Select count(*) from tblGlCOAMainSubSubDetail where main_sub_sub_id = " & AccountSubSubId
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
