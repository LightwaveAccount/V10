''22-Oct-2014      M.Shoaib        CR # 332  Data log: Datalog should prepare properly for all configuration of Lightwave

Imports Model
Imports System.Data.SqlClient
Imports Utility.Utility
Imports DAL
Imports System.Collections.Specialized
Imports System.Data
Imports DAL.SystemConfigurationDAL
Imports Microsoft.VisualBasic




Public Class AccountSubDAL

    Public Function Deleted(ByVal ObjModel As AccountSubModel) As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction

        Try

            Dim strSQL As String

            strSQL = " Delete from tblGlCOAMainSub where main_sub_id = " & ObjModel.AccountMainSubID


            ' Execute SQL ..
            Convert.ToInt32(SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing))

            ' SQL Statement Log ..
            ObjModel.ActivityLog.SQLType = "DELETE"
            ObjModel.ActivityLog.TableName = "tblGlCOAMainSub"
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

            strSQL = " SELECT tblGlCOAMain.coa_main_id AS MainAccountID, tblGlCOAMainSub.main_sub_id AS MainSubAccountID, " _
                   & " tblGlCOAMain.main_title + ' " & G_SEPERATOR & " ' + tblGlCOAMain.main_code AS MainAccount, tblGlCOAMainSub.sub_code AS MainSubAccountCode, tblGlCOAMainSub.sub_title AS MainSubAccountTitle " _
                   & " FROM tblGlCOAMainSub INNER JOIN tblGlCOAMain ON tblGlCOAMainSub.coa_main_id = tblGlCOAMain.coa_main_id " _
                   & " Where tblGLCOAMain.coa_main_id = " & strCondition _
                   & " ORDER BY MainSubAccountCode "


            ObjDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Dim myDataTable As New DataTable("AccountMainSubData")
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

    Public Function GetNewAccountMainSubCode(Optional ByVal strCondition As String = "") As String

        Dim ObjDA As SqlClient.SqlDataAdapter

        Try

            Dim strSQL As String = ""

            strSQL = "SELECT max(sub_code) as NewCode From tblGlCOAMainSub WHERE (coa_main_id = " & strCondition & " )"
            ObjDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Dim myDataTable As New DataTable("AccountMainData")
            ObjDA.Fill(myDataTable)

            ' Dim strCode() As String = Split(myDataTable.Rows(0).Item("NewCode").ToString.PadLeft(3, "0"), "-")

            If myDataTable.Rows(0).Item("NewCode").ToString <> "" Then
                Return (Val(Right(myDataTable.Rows(0).Item("NewCode").ToString, Len(myDataTable.Rows(0).Item("NewCode").ToString) - 3)) + 1).ToString.PadLeft(3, "0")
            Else
                Return (Val(myDataTable.Rows(0).Item("NewCode").ToString) + 1).ToString.PadLeft(3, "0")
            End If

        Catch ex As SqlException
            Throw ex

        Catch ex As Exception
            Throw ex

        Finally
            ObjDA = Nothing

        End Try

    End Function

    Public Function IsAlreadyExists(ByVal ObjModel As AccountSubModel, Optional ByVal strMode As String = "") As Boolean

        Try

            Dim strSQL As String
            ' Building SQL ..

            If strMode = "Update" Then
                strSQL = " Select sub_code from tblGLCOAMainSub where (sub_code = '" & ObjModel.AccountMainSubCode & "' or sub_title = '" & ObjModel.AccountMainSubTitle & "') and main_sub_id <> " & ObjModel.AccountMainSubID

            Else
                strSQL = " Select sub_code from tblGLCOAMainSub Where (sub_code = '" & ObjModel.AccountMainSubCode & "' or sub_title = '" & ObjModel.AccountMainSubTitle & "') "

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

    Public Function Save(ByVal ObjModel As AccountSubModel) As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()

        Dim trans As SqlTransaction = conn.BeginTransaction
        Try


            Dim strSQL As String

            strSQL = " Insert into tblGLCOAMainSub (coa_main_id, sub_code, sub_title) Values ( " & ObjModel.AccountMainID & ", '" & ObjModel.AccountMainSubCode & "','" & ObjModel.AccountMainSubTitle & "' ) " _
                     & " Select Ident_Current('tblGLCOAMainSub')"

            ' Execute SQL  ..
            ObjModel.AccountMainSubID = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing))

            'CR#160
            strSQL = " Insert into tblGLCOAMainSub (main_sub_id,coa_main_id, sub_code, sub_title) " & _
                  " Values (" & ObjModel.AccountMainSubID & "," & ObjModel.AccountMainID & ", '" & ObjModel.AccountMainSubCode & "','" & ObjModel.AccountMainSubTitle & "' ) "


            ' SQL Statement Log ..
            ObjModel.ActivityLog.SQLType = "INSERT"
            ObjModel.ActivityLog.TableName = "tblGLCOAMainSub"
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

    Public Function Update(ByVal ObjModel As AccountSubModel) As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()

        Dim trans As SqlTransaction = conn.BeginTransaction
        Try


            Dim strSQL As String

            ' Building SQL ..
            strSQL = " Update tblGLCOAMainSub Set sub_title = '" & ObjModel.AccountMainSubTitle & "', sub_code = '" & ObjModel.AccountMainSubCode & "', coa_main_id =" & ObjModel.AccountMainID & " where main_sub_id = " & ObjModel.AccountMainSubID


            ' Execute SQL  ..
            ObjModel.AccountMainID = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing))


            ' SQL Statement Log ..
            ObjModel.ActivityLog.SQLType = "UPDATE"
            ObjModel.ActivityLog.TableName = "tblGLCOAMainSub"
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

    Public Function TransactionsExist(Optional ByVal SubSubAccountID As String = "") As Boolean

        Dim ObjDA As SqlClient.SqlDataAdapter

        Try

            Dim strSQL As String = ""
            Dim isTransExist As Boolean = False

            strSQL = " Select count(*) from tblGlCOAMainSubSub where main_sub_id = " & SubSubAccountID
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
