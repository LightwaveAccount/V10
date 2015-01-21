Imports System.Windows.Forms
Imports DAL
Imports Model
Imports System.IO
Imports Utility.Utility
''/////////////////////////////////////////////////////////////////////////////////////////
''//-------------------------------------------------------------------------------------
''// File Name       : frmGLVoucher.vb           				                            
''// Programmer	     : Abdul Jabbar
''// Creation Date	 : Feb 18,2010
''// Description     : Creation & Deletion of Sample Chart of account for Trial version
''//-------------------------------------------------------------------------------------
Public Class frmEdition

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreateCOA.Click

        Try


            If MsgBox("Are you sure you want to create Chart of Account?It will overwrite existing Chart Of Account (if any COA exist)", vbYesNo + vbQuestion, ) = vbYes Then


                If funExecuteScriptFile("Sample_COA.sql") = True Then
                    If MsgBox("Chart of Account Created Successfully", vbInformation, ) = vbOK Then
                    End If
                End If

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Function funExecuteScriptFile(ByVal FileName As String) As Boolean

        Try


            Dim FilePath As String = Application.StartupPath & "\Utilities\EditionScripts\" & FileName

            Dim strSql As New System.Text.StringBuilder

            If IO.File.Exists(FilePath) Then
                Dim objReader As New IO.StreamReader(FilePath)
                strSql.AppendLine(objReader.ReadToEnd)
                UtilityDAL.ExecuteQuery(strSql.ToString)
                Return True
            Else
                ShowErrorMessage("File not found")
            End If
            Return False

        Catch ex As SqlClient.SqlException
            MsgBox("Unable to delete selected chart of accounts because child records (vouchers) exist against current chart of accounts")
            funExecuteScriptFile = False
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
            funExecuteScriptFile = False
        End Try

    End Function

    Private Sub frmEdition_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If gblnTrialVersion = False Then
                Me.btnCreateCOA.Enabled = False
                Me.btnDeleteCOA.Enabled = False
            End If

            If gObjFinancialYearInfo.YearCode.Trim = "" Then
                ShowErrorMessage("No Financial Year selected. Please define a Financial Year and than select Financial Year while loging In to program")
                Me.Dispose()
                Exit Sub
            End If


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnDeleteCOA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteCOA.Click
        Dim strSQL As String

        Try

            If MsgBox("Are you sure you want to Delete Chart of Account?", vbYesNo + vbQuestion, ) = vbYes Then

                Dim DR As SqlClient.SqlDataReader
                strSQL = "Select * from tblGlVoucher"
                DR = UtilityDAL.ExecuteReader(strSQL.ToString)

                If DR.HasRows Then
                    MessageBox.Show("Unable to delete chart of accounts because dependent vouchers exists", "Voucher Entry Exist", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                strSQL = "Delete from tblGlVoucherTempDetail"
                UtilityDAL.ExecuteQuery(strSQL.ToString)

                strSQL = "Delete from tblGlVoucherTemp"
                UtilityDAL.ExecuteQuery(strSQL.ToString)

                If funExecuteScriptFile("Delete_COA.sql") = True Then
                    MsgBox("Chart of Accounts Deleted Successfully.", vbInformation, )
                End If

            End If

        Catch ex As Exception
            'ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
