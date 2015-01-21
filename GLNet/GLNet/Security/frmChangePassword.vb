''/////////////////////////////////////////////////////////////////////////////////////////
''//                     GL Net
''/////////////////////////////////////////////////////////////////////////////////////////
''//-------------------------------------------------------------------------------------
''// File Name       : frmChangePassword.vb           				                            
''// Programmer	     : Rizwan Asif
''// Creation Date	 : 21-Jul-2009
''// Description     : 
''//-------------------------------------------------------------------------------------
''// Date Modified     Modified by         Brief Description			                
''//------------------------------------------------------------------------------------
''//08-dec-2011       Fatima Tajammal       CR #164 Lightwave Logviewer form is not working properly, need to find out and fix issues
''//21-March-2012     Fatima Tajammal       'CR # 183 Change Password: Functionality of this Screen is not proper. 
''/////////////////////////////////////////////////////////////////////////////////////////

Imports DAL
Imports Model
Imports System.Collections.Specialized
Imports System.Data
Imports Utility.Utility

Public Class frmChangePassword
    Implements IGeneral

#Region "Variables"
    ''This collection will hold the controls' names, upon which the logged in user has rights
    Private mobjControlList As NameValueCollection
    ''This is the model object which will be set with data values and refered for Save|Update|Delete|Loading Record in Edit Mode
    Private mobjModel As SecurityUser
    Private intPkId As Integer
#End Region

#Region "Enumerations"

#End Region

#Region "Interface Methods"
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(ByVal Mode As Utility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
        Try


            If Mode.ToString = EnumDataMode.Disabled.ToString Then

                btnNew.Enabled = False
                btnSave.Enabled = False
                btnUpdate.Enabled = False
                btnDelete.Enabled = False
                btnCancel.Enabled = False

                SetNavigationButtons(EnumDataMode.Edit)

            ElseIf Mode.ToString = EnumDataMode.[New].ToString Then

                btnNew.Enabled = False

                If mobjControlList.Item("btnSave") Is Nothing Then
                    btnSave.Enabled = False
                Else
                    btnSave.Enabled = True
                End If

                btnUpdate.Enabled = False
                btnDelete.Enabled = False
                btnCancel.Enabled = False

                SetNavigationButtons(Mode)


            ElseIf Mode.ToString = EnumDataMode.Edit.ToString Then

                btnNew.Enabled = True
                btnSave.Enabled = False

                If mobjControlList.Item("btnUpdate") Is Nothing Then
                    btnUpdate.Enabled = False
                Else
                    btnUpdate.Enabled = True
                End If

                If mobjControlList.Item("btnDelete") Is Nothing Then
                    btnDelete.Enabled = False
                Else
                    btnDelete.Enabled = True
                End If
                btnCancel.Enabled = False

                SetNavigationButtons(Mode)


            ElseIf Mode.ToString = EnumDataMode.ReadOnly.ToString Then

                btnNew.Enabled = True
                btnSave.Enabled = False


                btnUpdate.Enabled = False
                btnDelete.Enabled = False
                btnCancel.Enabled = False

                SetNavigationButtons(Mode)

            End If


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        mobjModel = New SecurityUser
        With mobjModel
            .LoginID = gObjUserInfo.LoginID
            .LoginPassword = txtNewPassword.Text
            'CR# 164 
            mobjModel.ActivityLog.ShopID = 0 'gObjUserInfo.ShopInfo.ShopID
            mobjModel.ActivityLog.ScreenTitle = Me.Text
            mobjModel.ActivityLog.LogGroup = "Security"
            mobjModel.ActivityLog.UserID = gObjUserInfo.UserID
        End With
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional ByVal Mode As Utility.Utility.EnumDataMode = Utility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try

            ''1 First Check Front End Validations
            If Mode = EnumDataMode.[New] Or Mode = EnumDataMode.Edit Then
                ''Old Password is required
                If Me.txtOldPassword.Text.Trim = String.Empty Then
                    ShowValidationMessage("Please Enter Old Password")
                    Me.txtOldPassword.Focus()
                    Return False
                    'CR # 183 Change Password: Functionality of this Screen is not proper. 
                    '...............................
                ElseIf Me.txtOldPassword.Text.Trim <> gObjUserInfo.LoginPassword Then
                    ShowValidationMessage("old Password Is Wrong")
                    Me.txtOldPassword.Focus()
                    Return False
                    '..................

                    ''New Password is required
                ElseIf Me.txtNewPassword.Text.Trim = String.Empty Then
                    ShowValidationMessage("Please Enter New Password")
                    Me.txtNewPassword.Focus()
                    Return False

                    ''Confirm New Password is required
                ElseIf Me.txtConfirmNewPassword.Text.Trim = String.Empty Then
                    ShowValidationMessage("Please Enter Confirm New Password")
                    Me.txtConfirmNewPassword.Focus()
                    Return False

                    ''New Password and Confirm new Password should have same values                
                ElseIf Me.txtNewPassword.Text.Trim <> Me.txtConfirmNewPassword.Text.Trim Then
                    ShowValidationMessage("Values for New Password and " & vbCrLf _
                                            & "Confirm New Password fields do not match")
                    Me.txtConfirmNewPassword.Focus()
                    Return False
                End If
            End If
           
            Me.FillModel()

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.txtOldPassword.Text = ""
            Me.txtNewPassword.Text = ""
            Me.txtConfirmNewPassword.Text = ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try

            ''Applying Front End Validation Checks
            If Me.IsValidate(EnumDataMode.[New]) Then

                Dim result As DialogResult = Windows.Forms.DialogResult.Yes

                If gblnShowSaveConfirmationMessages Then
                    ''Getting Save Confirmation from User
                    result = ShowConfirmationMessage("Do you want to update your user information to the new password.", MessageBoxDefaultButton.Button1)
                End If

                If result = Windows.Forms.DialogResult.Yes Then

                    ''Create a DAL Object and calls its Add Method by passing Model Object
                    If New SecurityUserDAL().ChangePassword(Me.mobjModel) Then

                        Me.ReSetControls()
                        ShowInformationMessage("Password changed successfully")

                    End If

                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages
        Try

            If gEnumIsRightToLeft = Windows.Forms.RightToLeft.No Then
                Me.btnFirst.ImageList = gobjMyImageListForOperationBar
                Me.btnFirst.ImageKey = "First"

                Me.btnNext.ImageList = gobjMyImageListForOperationBar
                Me.btnNext.ImageKey = "Next"

                Me.btnPrevious.ImageList = gobjMyImageListForOperationBar
                Me.btnPrevious.ImageKey = "Previous"

                Me.btnLast.ImageList = gobjMyImageListForOperationBar
                Me.btnLast.ImageKey = "Last"


            Else
                Me.btnFirst.ImageList = gobjMyImageListForOperationBar
                Me.btnFirst.ImageKey = "Last"

                Me.btnNext.ImageList = gobjMyImageListForOperationBar
                Me.btnNext.ImageKey = "Previous"

                Me.btnPrevious.ImageList = gobjMyImageListForOperationBar
                Me.btnPrevious.ImageKey = "Next"

                Me.btnLast.ImageList = gobjMyImageListForOperationBar
                Me.btnLast.ImageKey = "First"
            End If

            Me.btnNew.ImageList = gobjMyImageListForOperationBar
            Me.btnNew.ImageKey = "New"

            Me.btnSave.ImageList = gobjMyImageListForOperationBar
            Me.btnSave.ImageKey = "Save"

            Me.btnUpdate.ImageList = gobjMyImageListForOperationBar
            Me.btnUpdate.ImageKey = "Update"

            Me.btnCancel.ImageList = gobjMyImageListForOperationBar
            Me.btnCancel.ImageKey = "Cancel"

            Me.btnDelete.ImageList = gobjMyImageListForOperationBar
            Me.btnDelete.ImageKey = "Delete"

            Me.btnExit.ImageList = gobjMyImageListForOperationBar
            Me.btnExit.ImageKey = "Exit"

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As Utility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons
        Try

            If Mode = EnumDataMode.[New] Then
                ''if New Mode then Set Disable all Navigation Buttons
                btnFirst.Enabled = False
                btnPrevious.Enabled = False
                btnNext.Enabled = False
                btnLast.Enabled = False

            ElseIf Mode = EnumDataMode.Edit Then
                ''if New Mode then Set Enable all Navigation Buttons
                btnFirst.Enabled = True
                btnPrevious.Enabled = True
                btnNext.Enabled = True
                btnLast.Enabled = True

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Update(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update

    End Function
#End Region

#Region "Form Control Events"
  

    Private Sub frmChangePassword_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ''Getting all available controls list to the user on this screen; in a collection 
            mobjControlList = GetFormSecurityControls(Me.Name)

            ''Assing Images to Buttons
            Me.SetButtonImages()

            Me.ApplySecurity(EnumDataMode.[New])

            txtUserName.Text = gObjUserInfo.LoginID
            'CR # 183 Change Password: Functionality of this Screen is not proper. 
            'txtOldPassword.Text = gObjUserInfo.LoginPassword
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click, btnUpdate.Click, btnDelete.Click, btnCancel.Click, btnExit.Click, btnNew.Click

        Try
            'Dim btn As C1.Win.C1Input.C1Button = CType(sender, C1.Win.C1Input.C1Button)
            Dim btn As Windows.Forms.Button = CType(sender, Windows.Forms.Button)

            ''If New Button is Clicked
            If btn.Name = btnNew.Name Then

                ''Refresh the controls for new mode
                Me.ReSetControls()

            ElseIf btn.Name = btnSave.Name Then
                '' Call Save method to save the record
                Me.Save()
            ElseIf btn.Name = btnUpdate.Name Then
                '' Call Update method to update the record
                Me.Update()
            ElseIf btn.Name = btnDelete.Name Then
                '' Call Delete method to delete the record
                Me.Delete()
            ElseIf btn.Name = btnCancel.Name Then

            ElseIf btn.Name = btnExit.Name Then
                Me.Close()
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            mobjModel = Nothing
        End Try

    End Sub

    Private Sub frmChangePassword_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.Control And e.KeyCode = Keys.S Then
                If Me.btnSave.Enabled = True Then Me.Save()
            ElseIf e.Control And e.KeyCode = Keys.U Then
                If Me.btnUpdate.Enabled = True Then Me.Update()
            ElseIf e.Control And e.KeyCode = Keys.D Then
                If Me.btnDelete.Enabled = True Then Me.Delete()
            ElseIf e.Control And e.KeyCode = Keys.N Then
                If Me.btnNew.Enabled = True Then Me.ReSetControls()
            ElseIf e.Control And e.KeyCode = Keys.E Then
                ''If Me.btnCancel.Enabled = True Then Me.grdAllRecords_SelectionChanged(Nothing, Nothing)
            ElseIf e.Control And e.KeyCode = Keys.X Then
                If Me.btnExit.Enabled = True Then Me.Close()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
#End Region


End Class