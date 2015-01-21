Imports DAL
Imports Model
Imports System.Collections.Specialized
Imports System.Data
Imports Utility.Utility
Imports MySql.Data
Imports MySql.Data.MySqlClient

''/////////////////////////////////////////////////////////////////////////////////////////
''//                     GL CONFIGURATION
''/////////////////////////////////////////////////////////////////////////////////////////
''//-------------------------------------------------------------------------------------
''// File Name       : frmCustomerInfo.vb           				                            
''// Programmer	     : Farooq-H
''// Creation Date	 : June 09, 2013
''// Description     : Contect Directory Screen 
''//-------------------------------------------------------------------------------------
''// Date Modified     Modified by         Brief Description			                
''// 19-sep-2013        Fatima Tajammal     CR # 276    Contact Directory: Some issues need to be resolved

''//------------------------------------------------------------------------------------



Public Class frmContectDirec
    Implements IGeneral

#Region "Variables"
    Private mobjControlList As NameValueCollection
    Private mobjModel As ContactDirectory
    Private intPkId As Integer
    Dim timerTicks As Integer = 0
   
#End Region

#Region "Enumerations"

    Private Enum EnumGridCustomerInfo
        InfoId
        AccountID
        AccountCode
        AccountName
        ContactPerson
        PhoneOffice
        Mobile
        Fax
        Email
        Address
        Remarks
    End Enum


#End Region

#Region "Interface Methods"

    '' To set the Button Images.
    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

        Try

            Me.btnFirst.ImageList = gobjMyImageListForOperationBar
            Me.btnFirst.ImageKey = "First"

            Me.btnNext.ImageList = gobjMyImageListForOperationBar
            Me.btnNext.ImageKey = "Next"

            Me.btnPrevious.ImageList = gobjMyImageListForOperationBar
            Me.btnPrevious.ImageKey = "Previous"

            Me.btnLast.ImageList = gobjMyImageListForOperationBar
            Me.btnLast.ImageKey = "Last"


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

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
 
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords

        Dim lngTotalRecords As Long

        Try

            ''Getting Datasource for Grid from DAL
            Dim dt As DataTable = New ContactDirectoryDAL().GetAll()
            Me.grdAllRecords.DataSource = dt
            Me.grdAllRecords.RetrieveStructure()

            lngTotalRecords = dt.Rows.Count

            If lngTotalRecords <= 0 Then
                Me.ReSetControls()
            Else

            End If
            ''Applying Grid Formatting Setting
            Me.ApplyGridSettings()

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

        Try

            ''Columns In-visible

            'Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.CustomerID).Visible = False
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.InfoId).Visible = False
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.AccountID).Visible = False
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.AccountID).Width = 110
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.AccountName).Width = 150

            ''Set columns widths for visible columns

             
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.ContactPerson).Width = 80
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.PhoneOffice).Width = 80
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.Mobile).Width = 80
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.PhoneOffice).Width = 80
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.Fax).Width = 80
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.Email).Width = 80
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.Address).Width = 120
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.Remarks).Width = 200
            ' 30-05-2013       farooq-H           CR# 242  SMS: Send sms from customer information screen. 

            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.InfoId).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.AccountID).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.AccountName).EditType = Janus.Windows.GridEX.EditType.NoEdit
            
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.PhoneOffice).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.ContactPerson).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.PhoneOffice).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.Mobile).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.Fax).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.Email).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.Address).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.Remarks).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.AccountCode).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdAllRecords.TotalRowFormatStyle.BackColor = gobjRequiredFieldtBKColor
            'Stop Editing in Grid
            'Me.grdAllRecords.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False 
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

        Try

            If Mode = EnumDataMode.Disabled Then
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

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls

        Try
            '' Resetting All Controls
            Me.UiCtrlGLAccount1.txtACCode.Text = String.Empty
            Me.UiCtrlGLAccount1.txtAccountName.Text = String.Empty
            Me.txtContactPerson.Text = String.Empty
            Me.txtOfficAddress.Text = String.Empty
            Me.txtOfficEmail.Text = String.Empty
            Me.txtOfficFax.Text = String.Empty
            Me.txtMobile.Text = String.Empty
            Me.txtOfficPhone.Text = String.Empty
            Me.txtRemarks.Text = String.Empty

            Me.UiCtrlGLAccount1.Focus()
            Me.ApplySecurity(EnumDataMode.New)

        Catch ex As Exception
            Throw ex

        End Try



    End Sub

    Public Sub ApplySecurity(ByVal Mode As EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

        Try
            '' For the New Mode
            If Mode.ToString = EnumDataMode.[New].ToString Then

                btnNew.Enabled = False

                If mobjControlList.Item("btnSave") Is Nothing Then
                    btnSave.Enabled = False
                Else
                    btnSave.Enabled = True
                End If

                btnUpdate.Enabled = False
                btnDelete.Enabled = False
                btnCancel.Enabled = True
                SetNavigationButtons(EnumDataMode.Disabled)

                '' For the Edit Mode
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
                SetNavigationButtons(EnumDataMode.Edit)

                '' For the Read Only Mode
            ElseIf Mode.ToString = EnumDataMode.ReadOnly.ToString Then

                btnNew.Enabled = True
                btnSave.Enabled = False
                btnUpdate.Enabled = False
                btnDelete.Enabled = False
                btnCancel.Enabled = False
            End If

            If mobjControlList.Item("btnExport") Is Nothing Then
                Me.UiCtrlGridBar1.btnExport.Enabled = False
            End If


            '' Disabl/Enable the Button that Prints Grid data According to Login User rights
            If mobjControlList.Item("btnPrint") Is Nothing Then
                Me.UiCtrlGridBar1.btnPrint.Enabled = False
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel

        Try
            '' Filling the Model
            mobjModel = New ContactDirectory
            With mobjModel

                Me.UiCtrlGLAccount1.txtACCode.Focus()

                .InfoID = intPkId
                .AccountID = Me.UiCtrlGLAccount1.GLAccountID
                .ContactPerson = funFilterReserveText(Me.txtContactPerson.Text)
                .PhoneOffice = funFilterReserveText(Me.txtOfficPhone.Text)
                .Mobile = funFilterReserveText(Me.txtMobile.Text)
                .Fax = funFilterReserveText(Me.txtOfficFax.Text)
                .Email = funFilterReserveText(Me.txtOfficEmail.Text)
                .Address = funFilterReserveText(Me.txtOfficAddress.Text)
                .Remarks = funFilterReserveText(Me.txtRemarks.Text)

                mobjModel.ActivityLog.ShopID = 0
                mobjModel.ActivityLog.ScreenTitle = Me.Text
                mobjModel.ActivityLog.LogGroup = "Transactions"
                mobjModel.ActivityLog.UserID = gObjUserInfo.UserID

            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function funFilterReserveText(ByVal Txt As String) As String
        Try

            funFilterReserveText = Replace(Txt, "'", "''", , , vbTextCompare)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Function

    Public Function IsValidate(Optional ByVal Mode As EnumDataMode = EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate

        '' For the front end validation

        Try

            If Mode = EnumDataMode.[New] Or EnumDataMode.Edit Then
                If Me.UiCtrlGLAccount1.txtACCode.Text = String.Empty Then
                    ShowInformationMessage("Account Code is Missing")
                    Me.UiCtrlGLAccount1.Focus()
                    Return False

                End If
                If Me.txtContactPerson.Text.ToString.Trim = String.Empty Then
                    ShowInformationMessage("Contact Person name is Missing")
                    Me.txtContactPerson.Focus()
                    Return False

                End If
            End If
            
            '' Filling the Model
            FillModel()

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save

        Try

            If Me.IsValidate(EnumDataMode.[New]) Then
                If ShowConfirmationMessage("Do you want to save?", MessageBoxDefaultButton.Button1) <> Windows.Forms.DialogResult.Yes Then
                    Exit Function
                End If
                If New ContactDirectoryDAL().Add(mobjModel) Then

                    ShowInformationMessage("Record has been Inserted Successfully")

                End If

                Call ReSetControls()

                'SearchAgainstCriteria()
                'Call GetAllRecords()

            End If

        Catch ex As Exception
            Throw ex

        End Try

    End Function

    

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update

        Try

            ''Applying Front End Validation Checks
            If Me.IsValidate(EnumDataMode.Edit) Then

                Dim result As DialogResult = Windows.Forms.DialogResult.Yes
                ''Getting Save Confirmation from User
                result = ShowConfirmationMessage("Do you want to update?", MessageBoxDefaultButton.Button1)

                If result = Windows.Forms.DialogResult.Yes Then

                    ''Create a DAL Object and calls its Update Method by passing Model Object

                    If New ContactDirectoryDAL().Update(Me.mobjModel) Then

                        If gblnShowAfterUpdateMessages Then
                            ''Getting Save Confirmation from User
                            ShowInformationMessage(gstrMsgAfterUpdate)
                        End If

                        



                        ''Query to Database and get fressh modifications in the Grid
                        'CR#212
                        ' Me.GetAllRecords()
                        ' SearchAgainstCriteria()

                        'to select the last updated record
                        For Rind As Int16 = 0 To (grdAllRecords.RowCount - 1)
                            If Me.grdAllRecords.GetRow(Rind).Cells(EnumGridCustomerInfo.AccountID).Value = mobjModel.AccountID Then
                                Me.grdAllRecords.Row = Rind
                                Exit For
                            End If
                        Next


                    End If
                End If
            End If

        Catch ex As Exception
            If ex.Message = gstrMsgDuplicateName Then
                ShowErrorMessage(ex.Message)
            ElseIf ex.Message = gstrMsgDuplicateCode Then
                ShowErrorMessage(ex.Message)
            Else
                Throw ex
            End If
        End Try

    End Function

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete

        Try

            ''Applying Front End Validation Checks
            ' If Me.IsValidate(, "BackEndDeleteValidation") Then
            Dim result As DialogResult = Windows.Forms.DialogResult.Yes
            ''Getting Save Confirmation from User
            result = ShowConfirmationMessage("Do you want to delete?", MessageBoxDefaultButton.Button2)
            If result = Windows.Forms.DialogResult.Yes Then

                ''Create a DAL Object and calls its Delete Method by passing Model Object
                Me.FillModel()
                If New ContactDirectoryDAL().Deleted(Me.mobjModel) Then

                    ''This will hold row index of the selected row 
                    Dim intGridRowIndex As Integer
                    intGridRowIndex = Me.grdAllRecords.Row

                    ''Query to Database and get fressh modifications in the Grid
                    'CR#212
                    'Me.GetAllRecords()
                    SearchAgainstCriteria()
                    Call ReSetControls()

                    '        ''Call RowColumn Change Event
                    '        Me.grdAllRecords_SelectionChanged(Nothing, Nothing)

                    '        ''Reset the row index to the grid
                    '        If intGridRowIndex > (Me.grdAllRecords.RowCount - 1) Then intGridRowIndex = (Me.grdAllRecords.RowCount - 1)
                    '        If Not intGridRowIndex < 0 Then Me.grdAllRecords.Row = intGridRowIndex
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function


#End Region

    Private Sub frmCustomerInfo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load


        Try

            mobjControlList = GetFormSecurityControls(Me.Name)
            '' Setting Images to the Buttons
            Me.SetButtonImages()

            Me.FillCombos()

            Call GetAllRecords()
            Call ReSetControls()
         

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click, btnDelete.Click, btnCancel.Click, btnExit.Click, btnUpdate.Click, btnNew.Click, btnSearch.Click
        Try

            Dim btn As Windows.Forms.Button = CType(sender, Windows.Forms.Button)

            ''If New Button is Clicked
            If btn.Name = btnNew.Name Then
                ''Refresh the controls for new mode
                Me.TabContectInfo.SelectedTab = Me.TabContectInfo.TabPages(0)
                Me.ReSetControls()
            ElseIf btn.Name = btnSave.Name Then
                '' Call Save method 
                Me.Save()
            ElseIf btn.Name = btnUpdate.Name Then
                '' Call Update method 
                Me.Update1()
            ElseIf btn.Name = btnDelete.Name Then
                '' Call Delete method   
                Me.Delete()
            ElseIf btn.Name = btnCancel.Name Then
                grdAllRecords_DoubleClick(Nothing, Nothing)
            ElseIf btn.Name = btnExit.Name Then
                Me.Close()
            ElseIf btn.Name = btnSearch.Name Then
                SearchAgainstCriteria()
                ApplySecurity(EnumDataMode.ReadOnly)
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            mobjModel = Nothing
        End Try
    End Sub
    Private Sub SearchAgainstCriteria()
        '' Call GetAll_Detail Method
        Try
            Dim lngTotalRecords As Long
            FillModel()
            '' [1]. Front End Validations


            Dim dt As DataTable = New ContactDirectoryDAL().GetAll(Me.txtContactPerson_dtlSearch.Text.ToString.Trim & "|" & IIf(Me.UiCtrlGLAccount2.txtACCode.Text <> "", Me.UiCtrlGLAccount2.GLAccountID, ""))
            Me.grdAllRecords.DataSource = dt
            Me.grdAllRecords.RetrieveStructure()

            lngTotalRecords = dt.Rows.Count

            If lngTotalRecords <= 0 Then
                Me.ReSetControls()
            Else
                ''Me.grdAllRecords.MoveLast()
            End If
            Call ApplyGridSettings()
            Me.UiCtrlGLAccount2.txtACCode.Text = String.Empty
            Me.UiCtrlGLAccount2.txtAccountName.Text = String.Empty
            'Me.ReSetControls()

        Catch ex As Exception
            Throw ex

        End Try
    End Sub
    Private Sub GridEX1_FormattingRow(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grdAllRecords.FormattingRow

    End Sub

    Private Sub btnFirst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFirst.Click, btnPrevious.Click, btnNext.Click, btnLast.Click
        Try

            Dim btn As Button = CType(sender, Button)

            ''If Move First is clicked, then navigate to first record of the grid
            If btn.Name = Me.btnFirst.Name Then

                Me.grdAllRecords.MoveFirst()

                ''If Move Previous is clicked,
            ElseIf btn.Name = Me.btnPrevious.Name Then

                Me.grdAllRecords.MovePrevious()

                ''If Move Next is clicked, 
            ElseIf btn.Name = Me.btnNext.Name Then

                Me.grdAllRecords.MoveNext()


                ''If Move Last is clicked, then navigate to Last record of the grid
            ElseIf btn.Name = Me.btnLast.Name Then

                Me.grdAllRecords.MoveLast()

            End If

        Catch ex As Exception

        End Try
    End Sub

    'CR # 276
    Private Sub grdAllRecords_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdAllRecords.DoubleClick ', grdAllRecords.SelectionChanged

        Try


            ''If there is no record found in grid then load the screen in new mode
            If grdAllRecords.RowCount = 0 Then
                Me.ReSetControls()
                Exit Sub
            End If

            If Me.grdAllRecords.GetRow().RowType = Janus.Windows.GridEX.RowType.TotalRow Then
                Me.grdAllRecords.Row = (Me.grdAllRecords.Row - 1)
                Exit Sub
            End If


            ''intPkId = Convert.ToInt32()
            Me.UiCtrlGLAccount1.GLAccountID = Val(Me.grdAllRecords.GetValue(EnumGridCustomerInfo.AccountID).ToString)
            Me.UiCtrlGLAccount1.txtACCode.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.AccountCode).ToString
            Me.UiCtrlGLAccount1.txtAccountName.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.AccountName).ToString
            Me.txtContactPerson.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.ContactPerson).ToString
            Me.txtMobile.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.Mobile).ToString
            Me.txtOfficFax.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.Fax).ToString
            Me.txtOfficEmail.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.Email).ToString
            Me.txtOfficAddress.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.Address).ToString
            Me.txtRemarks.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.Remarks).ToString
            Me.txtOfficAddress.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.Address).ToString
            Me.txtOfficPhone.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.PhoneOffice).ToString

            Call ApplySecurity(EnumDataMode.Edit)
            Me.TabContectInfo.SelectedTab = Me.TabContectInfo.TabPages(0)
            Me.txtContactPerson.Focus()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Private Sub grdAllRecords_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdAllRecords.SelectionChanged
    '    Try
    '        If grdAllRecords.RowCount = 0 Then
    '            Me.ReSetControls()
    '            Exit Sub
    '        End If

    '        If Me.grdAllRecords.GetRow().RowType = Janus.Windows.GridEX.RowType.TotalRow Then
    '            Me.grdAllRecords.Row = (Me.grdAllRecords.Row - 1)
    '            Exit Sub
    '        End If


    '        ''intPkId = Convert.ToInt32()
    '        Me.UiCtrlGLAccount1.GLAccountID = Val(Me.grdAllRecords.GetValue(EnumGridCustomerInfo.AccountID).ToString)
    '        Me.UiCtrlGLAccount1.txtACCode.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.AccountCode).ToString
    '        Me.UiCtrlGLAccount1.txtAccountName.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.AccountName).ToString
    '        Me.txtContactPerson.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.ContactPerson).ToString
    '        Me.txtMobile.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.Mobile).ToString
    '        Me.txtOfficFax.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.Fax).ToString
    '        Me.txtOfficEmail.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.Email).ToString
    '        Me.txtOfficAddress.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.Address).ToString
    '        Me.txtRemarks.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.Remarks).ToString
    '        Me.txtOfficPhone.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.PhoneOffice).ToString

    '        Call ApplySecurity(EnumDataMode.Edit)
    '        Me.txtContactPerson.Focus()

    '    Catch ex As Exception
    '        Throw ex
    '    End Try


    'End Sub

    Private Sub UiCtrlGLAccount1_GetGLAccount(ByVal sender As uiCtrlGLAccount) Handles UiCtrlGLAccount1.GetGLAccount

        Try
            Dim dtGrid As DataTable = CType(Me.grdAllRecords.DataSource, DataTable)
            If dtGrid.Constraints.Count = 0 Then
                Dim uk As New UniqueConstraint("AccID", dtGrid.Columns(EnumGridCustomerInfo.AccountID), True)
                dtGrid.Constraints.Add(uk)
            End If

            Dim drFound As DataRow = dtGrid.Rows.Find(sender.GLAccountID)

            If Not drFound Is Nothing Then

                If grdAllRecords.RowCount = 0 Then
                    Me.ReSetControls()
                    Exit Sub
                End If

                If Me.grdAllRecords.GetRow().RowType = Janus.Windows.GridEX.RowType.TotalRow Then
                    Me.grdAllRecords.Row = (Me.grdAllRecords.Row - 1)
                    Exit Sub
                End If


                ''intPkId = Convert.ToInt32()
                'Me.UiCtrlGLAccount1.GLAccountID = Val(Me.grdAllRecords.GetValue(EnumGridCustomerInfo.AccountID).ToString)

                Me.grdAllRecords.Row = Me.grdAllRecords.GetRow(drFound).RowIndex

                Me.UiCtrlGLAccount1.txtACCode.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.AccountCode).ToString
                Me.UiCtrlGLAccount1.txtAccountName.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.AccountName).ToString
                Me.txtContactPerson.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.ContactPerson).ToString
                Me.txtMobile.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.Mobile).ToString
                Me.txtOfficFax.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.Fax).ToString
                Me.txtOfficEmail.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.Email).ToString
                Me.txtOfficPhone.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.PhoneOffice).ToString
                Me.txtOfficAddress.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.Address).ToString
                Me.txtRemarks.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.Remarks).ToString


                
                Call ApplySecurity(EnumDataMode.Edit)
                Me.txtContactPerson.Focus()
            Else
                Me.txtContactPerson.Text = String.Empty
                Me.txtOfficAddress.Text = String.Empty
                Me.txtOfficEmail.Text = String.Empty
                Me.txtOfficFax.Text = String.Empty
                Me.txtMobile.Text = String.Empty
                Me.txtOfficPhone.Text = String.Empty
                Me.txtRemarks.Text = String.Empty
                Me.txtContactPerson.Focus()
                Me.ApplySecurity(EnumDataMode.New)


            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub UiCtrlGLAccount2_GetGLAccount(ByVal sender As uiCtrlGLAccount) Handles UiCtrlGLAccount2.GetGLAccount
        Try
            Dim dtGrid As DataTable = CType(Me.grdAllRecords.DataSource, DataTable)
            If dtGrid.Constraints.Count = 0 Then
                Dim uk As New UniqueConstraint("AccID", dtGrid.Columns(EnumGridCustomerInfo.AccountID), True)
                dtGrid.Constraints.Add(uk)
            End If

            Dim drFound As DataRow = dtGrid.Rows.Find(sender.GLAccountID)
            If Not drFound Is Nothing Then
                Me.UiCtrlGLAccount2.txtAccountName.Focus()
                Me.grdAllRecords.Row = Me.grdAllRecords.GetRow(drFound).Position

            Else
                ShowInformationMessage("Record does not exist in Grid")
                Exit Sub

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)

        End Try

    End Sub


    Private Sub frmCustomerInfo_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        Try
            If e.Control And e.KeyCode = Keys.S Then
                If btnSave.Enabled = True Then Me.Save()
            ElseIf e.Control And e.KeyCode = Keys.U Then
                'Cr # 276
                'If btnUpdate.Enabled = True Then Me.Update()
                If btnUpdate.Enabled = True Then Me.Update1()
            ElseIf e.Control And e.KeyCode = Keys.N Then
                If btnNew.Enabled = True Then Me.ReSetControls()
            ElseIf e.Control And e.KeyCode = Keys.E Then
                If btnCancel.Enabled = True Then Me.grdAllRecords_DoubleClick(Nothing, Nothing)
            ElseIf e.Control And e.KeyCode = Keys.D Then
                If btnDelete.Enabled = True Then Me.Delete()
            ElseIf e.Control And e.KeyCode = Keys.X Then
                If btnExit.Enabled = True Then Me.Close()
            ElseIf e.Control And e.KeyCode = Keys.F Then
                If btnNext.Enabled = True Then Me.grdAllRecords.MoveNext()
            ElseIf e.Control And e.KeyCode = Keys.B Then
                If btnPrevious.Enabled = True Then Me.grdAllRecords.MovePrevious()

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try



    End Sub

    Private Sub TabContectInfo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabContectInfo.SelectedIndexChanged
        Try
            If Me.TabContectInfo.SelectedIndex = 0 Then
                Me.grdAllRecords_DoubleClick(sender, e)
            Else
                Me.ApplySecurity(EnumDataMode.ReadOnly)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

     
End Class