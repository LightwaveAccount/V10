Imports Model
Imports System.Data.SqlClient
Imports Utility.Utility

Public Class ContactDirectoryDAL

#Region "Global Variables and Functions "

    '' For the Insertion of Record in the "Contact Directory Information Table" 

    Public Function Add(ByVal objContactDirectory As ContactDirectory) As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)        '' Assigning Connection String
        conn.Open()                                             '' Connection Opening
        Dim trans As SqlTransaction = conn.BeginTransaction     '' Begining Transaction

        Try

            Dim strSQL As String


            strSQL = "Insert into tblGLContactDirectory (Account_id, Contact_person,Phone_office,Mobile,Fax,Email,Address,Remarks ) " & _
                     " Values(" & objContactDirectory.AccountID & ",'" & objContactDirectory.ContactPerson & "','" & objContactDirectory.PhoneOffice & "','" & objContactDirectory.Mobile & "','" & objContactDirectory.Fax & "','" & objContactDirectory.Email & "','" & objContactDirectory.Address & "','" & objContactDirectory.Remarks & "' ) "

            ''Execute SQL Command
            objContactDirectory.InfoID = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing))

            ''For SQL Statement Log
            objContactDirectory.ActivityLog.SQLType = "INSERT"
            objContactDirectory.ActivityLog.TableName = "tblGLContactDirectory"
            objContactDirectory.ActivityLog.SQL = strSQL
            UtilityDAL.BuildSQLLog(objContactDirectory.ActivityLog, trans)

            ''For Activity Log
            objContactDirectory.ActivityLog.FormAction = "Save"
            UtilityDAL.BuildActivityLog(objContactDirectory.ActivityLog, trans)

            ''Commiting Transaction
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

#End Region


    '' For the Updation of the Record in "Customer Information Table"

    Public Function Update(ByVal objContactDirectory As ContactDirectory) As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction

        Try

            Dim strSQL As String
            strSQL = "Update tblGLContactDirectory Set Account_id =" & objContactDirectory.AccountID & ", Contact_person='" & objContactDirectory.ContactPerson & "', Phone_office='" & objContactDirectory.PhoneOffice & "', Mobile='" & objContactDirectory.Mobile & "', Fax='" & objContactDirectory.Fax & "', Email='" & objContactDirectory.Email & "', Address='" & objContactDirectory.Address & "', Remarks='" & objContactDirectory.Remarks & "' where Account_id =" & objContactDirectory.AccountID & ""

            SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing)

            'SQL Statement Log
            objContactDirectory.ActivityLog.SQLType = "UPDATE"
            objContactDirectory.ActivityLog.TableName = "tblGLContactDirectory"
            objContactDirectory.ActivityLog.SQL = strSQL
            UtilityDAL.BuildSQLLog(objContactDirectory.ActivityLog, trans)

            ' ''Activity Log
            objContactDirectory.ActivityLog.FormAction = "UPDATE"
            UtilityDAL.BuildActivityLog(objContactDirectory.ActivityLog, trans)

            '    ''Committing Transaction
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

    '' For the Deletion of the Record from the "Customer Information Table"

    Public Function Deleted(ByVal objContactDirectory As ContactDirectory) As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction

        Try

            Dim strSQL As String

            strSQL = "Delete from tblGLContactDirectory where Account_id =" & objContactDirectory.AccountID & ""
            ''Execute SQL 

            SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing)
            'SQL Statement Log
            objContactDirectory.ActivityLog.SQLType = "DELETE"
            objContactDirectory.ActivityLog.TableName = "tblGLContactDirectory"
            objContactDirectory.ActivityLog.SQL = strSQL
            UtilityDAL.BuildSQLLog(objContactDirectory.ActivityLog, trans)

            ''Activity Log
            objContactDirectory.ActivityLog.FormAction = "Delete"
            UtilityDAL.BuildActivityLog(objContactDirectory.ActivityLog, trans)

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

    '' Get All Records

    Public Function GetAll(Optional ByVal strCondition As String = "") As DataTable
        Dim objDA As SqlClient.SqlDataAdapter
        Try

            Dim strSQL As String

            strSQL = "SELECT tblGLContactDirectory.InfoId, tblGlCOAMainSubSubDetail.Coa_Detail_Id, tblGlCOAMainSubSubDetail.detail_code AS [Account Code], tblGlCOAMainSubSubDetail.detail_title as [Account Name], tblGLContactDirectory.contact_person [Contact Person],tblGLContactDirectory.phone_office [Phone Office],tblGLContactDirectory.mobile [Mobile],tblGLContactDirectory.fax [Fax],tblGLContactDirectory.email [Email],tblGLContactDirectory.address [Address],tblGLContactDirectory.Remarks [Remarks] From tblGLContactDirectory INNER JOIN tblGlCOAMainSubSubDetail ON tblGLContactDirectory.Account_id = tblGlCOAMainSubSubDetail.coa_detail_id where 1= 1 "

            If strCondition <> "" Then
                Dim str() As String = strCondition.Split("|")
                If str(0).ToString <> "" Then
                    strSQL = strSQL & " AND tblGLContactDirectory.Contact_Person  LIKE '%" & str(0).ToString.Trim & "%'"
                ElseIf str(1).ToString <> "" Then
                    strSQL = strSQL & " AND tblGLContactDirectory.Account_id = " & str(1).ToString.Trim & " "
                End If

            End If
            objDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Dim MyCollectionList As New DataTable("CustomerInfo")
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

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class
