''/////////////////////////////////////////////////////////////////////////////////////////
''//                      GL .Net
''/////////////////////////////////////////////////////////////////////////////////////////
''//-------------------------------------------------------------------------------------
''// File Name       : frmRegisterProduct.vb           				                            
''// Programmer	     : Fahad Amin Rizvi
''// Creation Date	 : 29-July-2009
''// Description     : This form will be used to register GL
''// Function List   : 								                                    
''//-------------------------------------------------------------------------------------
''// Date Modified     Modified by         Brief Description			                
''//------------------------------------------------------------------------------------
'//  14-Dec-2011       Fatima Tajammal     CR#168 Register Product and Generate finger print button should not be visible for Trial DBs. 
''/////////////////////////////////////////////////////////////////////////////////////////

Imports DAL
Imports Model
Imports System.Collections.Specialized
Imports System.Data
Imports Utility.Utility

Public Class frmRegisterProduct
    Implements IGeneral

#Region "Variables"

#End Region

#Region "Enumerations"

#End Region

#Region "Interface Methods"
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(ByVal Mode As Utility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional ByVal Mode As Utility.Utility.EnumDataMode = Utility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls

    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save

    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As Utility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update

    End Function
#End Region

#Region "Local Functions and Procedures"

#End Region

#Region "Form Control Events"
    Private Sub frmRegisterProduct_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'CR#168
            '......................................
            Me.btnGenerateFingerPrint.Visible = False
            Me.btnRegister.Visible = False
            '...........................
            Dim strFingerPrint As String
            strFingerPrint = New RegisterProductDAL().GetFingerPrint()
            txtProductKey.Text = strFingerPrint

            'If gblnTrialVersion Then
            If Utility.Utility.IsTrialVersion Then
                lblRegistered.Text = "UnRegistered Product"
                lblRegistered.ForeColor = Color.Red
                btnRegister.Enabled = True
            Else
                lblRegistered.Text = "Registered Product"
                lblRegistered.ForeColor = Color.Green
                btnRegister.Enabled = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmRegisterProduct_InputLanguageChanging(ByVal sender As Object, ByVal e As System.Windows.Forms.InputLanguageChangingEventArgs) Handles Me.InputLanguageChanging
        e.Cancel = True
    End Sub

    Private Sub btnRegister_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegister.Click
        Try
            If txtRegistrationKey.Text.Trim = "" Then
                ShowValidationMessage("Please enter registration key")
                Exit Sub
            End If

            If New RegisterProductDAL().RegisterProduct(txtProductKey.Text, txtRegistrationKey.Text) Then
                ShowInformationMessage("Thank you for registering LumenSoft Product")
                Me.Close()
            Else
                ShowErrorMessage("Registration failed. Invalid Registration Key")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnGenerateFingerPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateFingerPrint.Click
        Try
            Dim objRegProductDAL As New RegisterProductDAL

            objRegProductDAL.GenerateRegistrationKey(Application.StartupPath, txtProductKey.Text)

            ShowInformationMessage("Product Fingerprint has been generated!" _
                                   & vbCrLf & Application.StartupPath & "\Registration\Fingreprint.txt" _
                                   & vbCrLf & "Please send this file to LumenSoft Technologies for Valid License")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRegisterLater_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegisterLater.Click
        Try
            Dim objRegProductDAL As New RegisterProductDAL
            objRegProductDAL.RegisterLater()
            Utility.Utility.IsTrialVersion = True
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnBrowse_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        Try
            openProductKey.ShowDialog()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub openProductKey_FileOk(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles openProductKey.FileOk
        Try
            txtFileName.Text = openProductKey.FileName
            txtRegistrationKey.Text = System.IO.File.ReadAllText(txtFileName.Text).Trim
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSendViaEmail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSendViaEmail.Click
        Try
            SendMail("fahad@lumensoft.biz", "", "", "LumenSoft Product Registration Request", _
                     "FINGERPRINT: " & txtProductKey.Text, "", False)

            ShowInformationMessage("The FingerPrint has been emailed to LumenSoft Technologies. " & vbCrLf _
                                   & "You will get the Registration Key shortly. ")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
#End Region


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()

    End Sub
End Class