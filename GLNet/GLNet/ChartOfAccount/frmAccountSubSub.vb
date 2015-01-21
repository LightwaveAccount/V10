''/////////////////////////////////////////////////////////////////////////////////////////
''// GL-NET ..                       
''/////////////////////////////////////////////////////////////////////////////////////////
''//-------------------------------------------------------------------------------------
''// File Name       : Account Sub Sub .. 
''// Programmer	     : Tariq Majeed
''// Creation Date	 : 
''// Description     : 
''// Function List   : 								                                    
''//											                                            
''//-------------------------------------------------------------------------------------
''// Date Modified     Modified by         Brief Description			                
''//------------------------------------------------------------------------------------
''//15 Jul,2010        Abdul Jabbar         CR#143 Chart of Account should not be deleted if relevant detail account or Transaction exists.
'// 02 Dec,2011        Abdul Jabbar         CR#160,New changes Relevant to Service Broker/Data Log 
''/////////////////////////////////////////////////////////////////////////////////////////

Imports DAL
Imports Model
Imports System.Collections.Specialized
Imports System.Data
Imports Utility.Utility
Imports DAL.SystemConfigurationDAL


Public Class frmAccountSubSub
    Implements IGeneral

#Region "Variables .. "
    Dim ObjModel As Model.AccountSubSubModel
    Private mobjControlList As NameValueCollection

#End Region

#Region "Enums .. "

    Private Enum GridCol
        colSubSubAccountID = 0
        colSubAccountCode = 1
        colSubSubAccountCode = 2
        colSubSubAccountTitle = 3
        colAccountType = 4
        colDrBSNote = 5
        colCrBSNote = 6
        colPLNote = 7
        colSubAccountID = 8
        colDrBSNoteID = 9
        colCrBSNoteID = 10
        colPLNoteID = 11

    End Enum

#End Region

#Region "IGeneral Methods .. "

    ' Applying Grid Settings .. 
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

        Try
            ' Giving Captions .. 
            grdMainAccounts.RootTable.Columns(GridCol.colSubAccountID).Caption = "Sub Account ID"
            grdMainAccounts.RootTable.Columns(GridCol.colSubSubAccountID).Caption = "Sub Sub Account ID"
            grdMainAccounts.RootTable.Columns(GridCol.colSubAccountCode).Caption = "A/C Sub Code"
            grdMainAccounts.RootTable.Columns(GridCol.colSubSubAccountCode).Caption = "A/C Sub Sub Code"
            grdMainAccounts.RootTable.Columns(GridCol.colSubSubAccountTitle).Caption = "A/C Sub Sub Title"
            grdMainAccounts.RootTable.Columns(GridCol.colAccountType).Caption = "A/C Type"
            grdMainAccounts.RootTable.Columns(GridCol.colDrBSNote).Caption = "Dr BS Note"
            grdMainAccounts.RootTable.Columns(GridCol.colCrBSNote).Caption = "Cr BS Note"
            grdMainAccounts.RootTable.Columns(GridCol.colPLNote).Caption = "PL Note"
            grdMainAccounts.RootTable.Columns(GridCol.colDrBSNoteID).Caption = "Dr BS Note ID"
            grdMainAccounts.RootTable.Columns(GridCol.colCrBSNoteID).Caption = "Cr BS Note ID"
            grdMainAccounts.RootTable.Columns(GridCol.colPLNoteID).Caption = "PL Note ID"


            ' Hiding Columns .. 
            grdMainAccounts.RootTable.Columns(GridCol.colSubAccountID).Visible = False
            grdMainAccounts.RootTable.Columns(GridCol.colSubSubAccountID).Visible = False
            grdMainAccounts.RootTable.Columns(GridCol.colDrBSNoteID).Visible = False
            grdMainAccounts.RootTable.Columns(GridCol.colCrBSNoteID).Visible = False
            grdMainAccounts.RootTable.Columns(GridCol.colPLNoteID).Visible = False


            ' Totals Of Columns .. 
            grdMainAccounts.RootTable.Columns(GridCol.colSubAccountCode).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Count


            ' Auto Fit .. 
            grdMainAccounts.AutoSizeColumns()

            ' Hide Total Row ..
            grdMainAccounts.TotalRow = Janus.Windows.GridEX.InheritableBoolean.False

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    ' Apply Security .. 
    Public Sub ApplySecurity(ByVal Mode As Utility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
        Try


            If Mode.ToString = EnumDataMode.Disabled.ToString Then

                btnNew.Enabled = False
                btnSave.Enabled = False
                btnUpdate.Enabled = False
                btnDelete.Enabled = False
                btnCancel.Enabled = False

                SetNavigationButtons(EnumDataMode.Edit)
                Me.grdMainAccounts.Enabled = True

            ElseIf Mode.ToString = EnumDataMode.[New].ToString Then

                btnNew.Enabled = False

                If mobjControlList.Item("btnSave") Is Nothing Then
                    btnSave.Enabled = False
                Else
                    btnSave.Enabled = True
                End If

                btnUpdate.Enabled = False
                btnDelete.Enabled = False
                btnCancel.Enabled = True

                SetNavigationButtons(Mode)

                Me.grdMainAccounts.Enabled = False

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

                Me.grdMainAccounts.Enabled = True

                Me.grdMainAccounts.Focus()

            ElseIf Mode.ToString = EnumDataMode.ReadOnly.ToString Then

                btnNew.Enabled = True
                btnSave.Enabled = False

                btnUpdate.Enabled = False
                btnDelete.Enabled = False
                btnCancel.Enabled = False

                SetNavigationButtons(Mode)

                Me.grdMainAccounts.Enabled = True

                Me.grdMainAccounts.Focus()

            End If

            '' Disabl/Enable the Button that converts Grid data in Excel Sheet According to Login User rights
            If mobjControlList.Item("btnExport") Is Nothing Then
                Me.UiCtrlGridBar1.btnExport.Enabled = False
            End If


            '' Disabl/Enable the Button that Prints Grid data According to Login User rights
            If mobjControlList.Item("btnPrint") Is Nothing Then
                Me.UiCtrlGridBar1.btnPrint.Enabled = False
            End If


        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub

    ' Delete Method .. 
    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete

        Try
            Me.Cursor = Cursors.WaitCursor
            ' Filling Model .. 
            FillModel()

            If New DAL.AccountSubSubDAL().Deleted(ObjModel) Then
                ReSetControls()
                Me.GetAllRecords(txtAccountSub.GLAccountID)
                Return True
                Exit Function

                'Else
                '    ShowErrorMessage("Unable to Delete this record because related information exists")

            End If


        Catch ex As Exception
            'ShowErrorMessage("Transaction Cannnot Be Performed .. ")
            ShowErrorMessage("Error:" & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Function

    ' Fill Combos .. 
    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos

        Try
            Dim strSQL As String = ""
            Dim G_SEPERATOR As String = "-"
            Dim ObjDataTable As DataTable
            Dim ObjDataRow As DataRow


            ' - Account Type .. (Binding Drop Down List .. )
            ' ----------------------------------------------
            cmbAccountType.Items.Clear()
            cmbAccountType.Items.Add(gstrComboZeroIndexString)
            cmbAccountType.Items.Add("General")
            cmbAccountType.Items.Add("Cash")
            cmbAccountType.Items.Add("Bank")
            cmbAccountType.Items.Add("Customer")
            cmbAccountType.Items.Add("Vendor")
            ' ----------------------------------------------


            ' Binding Cr & Dr BS Notes .. 
            ' =========================================================================================
            ' =========================================================================================
            strSQL = " Select note_title, gl_note_id from tblGLDefGLNotes where note_type = 'BS' "
            ObjDataTable = UtilityDAL.GetDataTable(strSQL)

            ObjDataRow = ObjDataTable.NewRow
            ObjDataRow.Item("note_title") = gstrComboZeroIndexString
            ObjDataRow.Item("gl_note_id") = 0
            ObjDataTable.Rows.InsertAt(ObjDataRow, 0)


            cmbCrBSNote.DataSource = ObjDataTable.Copy
            cmbDrBSNote.DataSource = ObjDataTable.Copy

            cmbCrBSNote.DisplayMember = "note_title"
            cmbCrBSNote.ValueMember = "gl_note_id"

            cmbDrBSNote.DisplayMember = "note_title"
            cmbDrBSNote.ValueMember = "gl_note_id"

            ' =========================================================================================
            ' =========================================================================================


            ' Binding PL Notes .. 
            ' =========================================================================================
            ' =========================================================================================
            strSQL = " Select note_title, gl_note_id from tblGLDefGLNotes where note_type = 'PL' "
            ObjDataTable = UtilityDAL.GetDataTable(strSQL)

            ObjDataRow = ObjDataTable.NewRow
            ObjDataRow.Item("note_title") = gstrComboZeroIndexString
            ObjDataRow.Item("gl_note_id") = 0
            ObjDataTable.Rows.InsertAt(ObjDataRow, 0)


            cmbPLNote.DataSource = ObjDataTable.Copy

            cmbPLNote.DisplayMember = "note_title"
            cmbPLNote.ValueMember = "gl_note_id"
            ' =========================================================================================
            ' =========================================================================================

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub

    ' Filling Model .. 
    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel

        Try
            ObjModel = New Model.AccountSubSubModel

            ObjModel.AccountSubID = txtAccountSub.GLAccountID
            ObjModel.SubSubAccountCode = txtAccountSub.txtACCode.Text & "-" & txtSubSubAccountCode.Text
            ObjModel.SubSubAccountTitle = Replace(txtSubSubAccountTitle.Text, "'", "''")
            ObjModel.AccountType = cmbAccountType.Text
            ObjModel.CrBSNote = cmbCrBSNote.SelectedValue
            ObjModel.DrBSNote = cmbDrBSNote.SelectedValue
            ObjModel.PLNote = cmbPLNote.SelectedValue

            'CR#160
            ObjModel.ActivityLog.ShopID = 0 'gObjUserInfo.ShopInfo.ShopID
            ObjModel.ActivityLog.ScreenTitle = Me.Text
            ObjModel.ActivityLog.LogGroup = "Definition"
            ObjModel.ActivityLog.UserID = gObjUserInfo.UserID

            If grdMainAccounts.RowCount > 0 Then
                ObjModel.SubSubAccountID = grdMainAccounts.GetRow().Cells(GridCol.colSubSubAccountID).Text
            End If


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

     
    End Sub

    ' Binding Grid Control .. Which Data Is Exracted From DB .. 
    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords

        Try

            Dim ObjDAL As New DAL.AccountSubSubDAL
            grdMainAccounts.DataSource = ObjDAL.GetAll(Condition)
            grdMainAccounts.RetrieveStructure()

            ApplyGridSettings()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub

    ' Validation Checks .. 
    Public Function IsValidate(Optional ByVal Mode As Utility.Utility.EnumDataMode = Utility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate

        Try
            If txtSubSubAccountCode.Text.Trim = "" Then
                txtSubSubAccountCode.Text = "000"

            End If

            If txtSubSubAccountTitle.Text.Trim = "" Then
                ShowValidationMessage("Please enter Sub Sub Account Title")
                txtSubSubAccountTitle.Focus()
                Return False
                Exit Function

            ElseIf cmbAccountType.SelectedIndex = 0 Then
                ShowValidationMessage("Please enter Account Type")
                cmbAccountType.Focus()
                Return False
                Exit Function

            ElseIf cmbDrBSNote.SelectedIndex = 0 Then
                ShowValidationMessage("Please enter Dr BS Note")
                cmbDrBSNote.Focus()
                Return False
                Exit Function

            ElseIf cmbCrBSNote.SelectedIndex = 0 Then
                ShowValidationMessage("Please enter Cr BS Note")
                cmbCrBSNote.Focus()
                Return False
                Exit Function

            End If

            Return True


        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try


    End Function

    ' Reseting Controls .. 
    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls

        Try
            ' Setting Controls To Intial Positions .. 
            txtSubSubAccountCode.Text = ""
            txtSubSubAccountTitle.Text = ""
            cmbAccountType.SelectedIndex = 0
            cmbCrBSNote.SelectedIndex = 0
            cmbDrBSNote.SelectedIndex = 0
            cmbPLNote.SelectedIndex = 0

            txtSubSubAccountCode.Focus()

            Me.ApplySecurity(EnumDataMode.[New])
            If txtAccountSub.GLAccountID = 0 Then Exit Sub

            ' Setting New Code To Its Attached Fields .. 
            txtSubSubAccountCode.Text = New DAL.AccountSubSubDAL().GetNewAccountMainCode(txtAccountSub.GLAccountID).ToString

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub

    ' Save Method .. 
    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save

        Try

            Me.Cursor = Cursors.WaitCursor

            ' Filling Model .. 
            FillModel()

            If Not New DAL.AccountSubSubDAL().IsAlreadyExists(ObjModel) Then
                If New DAL.AccountSubSubDAL().Save(ObjModel) Then
                    ReSetControls()
                    Me.GetAllRecords(txtAccountSub.GLAccountID)
                    Return True
                    Exit Function

                End If


            Else
                ShowErrorMessage("Duplicate Code/Name is not allowed .. ")
                txtAccountSub.Focus()
                Return False
                Exit Function

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally


            Me.Cursor = Cursors.Default
        End Try

    End Function

    ' Giving Images To Buttons .. 
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
            ShowErrorMessage(ex.Message)

        End Try

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting
    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As Utility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons
    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update

        Try

            Me.Cursor = Cursors.WaitCursor
            ' Filling Model .. 
            FillModel()
            If Not New DAL.AccountSubSubDAL().IsAlreadyExists(ObjModel, "Update") Then

                If New DAL.AccountSubSubDAL().Update(ObjModel) Then
                    ReSetControls()
                    Me.GetAllRecords(txtAccountSub.GLAccountID)
                    Return True
                    Exit Function

                End If


            Else
                ShowErrorMessage("Duplicate Code/Name is not allowed .. ")
                txtAccountSub.Focus()
                Return False
                Exit Function

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Function

#End Region

#Region "Form Events.. "

    ' Form Key Down Event .. 
    Private Sub frmDetailAccount_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.Control And e.KeyCode = Keys.S Then
                If Me.btnSave.Enabled = True Then btnSave_Click(Nothing, Nothing)

            ElseIf e.Control And e.KeyCode = Keys.U Then
                If Me.btnUpdate.Enabled = True Then btnUpdate_Click(Nothing, Nothing)

            ElseIf e.Control And e.KeyCode = Keys.D Then
                If Me.btnDelete.Enabled = True Then btnDelete_Click(Nothing, Nothing)

            ElseIf e.Control And e.KeyCode = Keys.N Then
                If Me.btnNew.Enabled = True Then Me.ReSetControls()

            ElseIf e.Control And e.KeyCode = Keys.E Then
                If Me.btnCancel.Enabled = True Then Me.grdMainAccounts_SelectionChanged(Nothing, Nothing)

            ElseIf e.Control And e.KeyCode = Keys.X Then
                If Me.btnExit.Enabled = True Then Me.Close()
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub

    ' Form Close Button .. 
    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click

        Try
            Me.Close()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub


    ' GL-Account Control .. Click Event .. 
    Private Sub txtAccountSub_GetGLAccount(ByVal sender As uiCtrlGLAccount) Handles txtAccountSub.GetGLAccount
        Try
            If txtAccountSub.txtACCode.Text = "" Then Exit Sub

            ReSetControls()

            ' Binding Grid Control .. 
            Me.GetAllRecords(txtAccountSub.GLAccountID)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub


    ' Form Load Event .. 
    Private Sub frmAccountSubSub_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            mobjControlList = GetFormSecurityControls(Me.Name)

            ' Filling Combs .. 
            FillCombos()


            ' Assing Images to Buttons ..
            Me.SetButtonImages()


            Me.ReSetControls()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub


    ' New Buttton Click Event .. 
    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            If txtAccountSub.txtACCode.Text = "" Then
                ShowValidationMessage("Please Select Main Sub Account InWhich You Want To Create Sub Sub Account .. ")
                txtAccountSub.Focus()
                Exit Sub

            End If
            Me.ReSetControls()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub

    ' Delete Click Event .. 
    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click

        Try
            If Me.grdMainAccounts.RowCount <= 0 Or grdMainAccounts.GetRow().RowType = Janus.Windows.GridEX.RowType.TotalRow Or grdMainAccounts.GetRow().RowType = Janus.Windows.GridEX.RowType.GroupHeader Or grdMainAccounts.GetRow().RowType = Janus.Windows.GridEX.RowType.GroupFooter Then
                ShowValidationMessage("Nothing To Delete ")
                Exit Sub

            End If


            If IsValidate() Then

                'CR#143
                'If ShowConfirmationMessage("Do you want to delete this record?", MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                '    Me.Delete()

                'End If

                'Backend validation checking tranasctions against Account selected for deletion
                If New DAL.AccountSubSubDAL().TransactionsExist(grdMainAccounts.GetRow().Cells(GridCol.colSubSubAccountID).Text) Then

                    ShowErrorMessage("Unable to Delete this record because related Detail Accounts exists")

                Else

                    If ShowConfirmationMessage("Do you want to delete this record?", MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                        Me.Delete()

                    End If

                End If

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub


    ' Update Click Event .. 
    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click

        Try

            If Me.grdMainAccounts.RowCount <= 0 Or grdMainAccounts.GetRow().RowType = Janus.Windows.GridEX.RowType.TotalRow Or grdMainAccounts.GetRow().RowType = Janus.Windows.GridEX.RowType.GroupHeader Or grdMainAccounts.GetRow().RowType = Janus.Windows.GridEX.RowType.GroupFooter Then
                ShowValidationMessage("Nothing To Update ")
                Exit Sub
            End If


            If IsValidate() Then
                If ShowConfirmationMessage("Do you want to save changes in the selected record?", MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                    Me.Update1()

                End If

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub


    ' Click Button Of Save Button .. 
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Try
            If IsValidate() Then
                If ShowConfirmationMessage("Do you want to save this record?", MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                    Save()

                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub


    ' Selection Changed OF Grid Control .. 
    Private Sub grdMainAccounts_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdMainAccounts.SelectionChanged

        Try

            If grdMainAccounts.RowCount > 0 Then
                If grdMainAccounts.GetRow().RowType = Janus.Windows.GridEX.RowType.TotalRow Or grdMainAccounts.GetRow().RowType = Janus.Windows.GridEX.RowType.GroupHeader Or grdMainAccounts.GetRow().RowType = Janus.Windows.GridEX.RowType.GroupFooter Then
                    Exit Sub
                End If
            End If


            If grdMainAccounts.RowCount = 0 Then
                Exit Sub
            End If



            If Me.grdMainAccounts.GetRow().RowType = Janus.Windows.GridEX.RowType.TotalRow Then
                Me.grdMainAccounts.Row = (Me.grdMainAccounts.Row - 1)
                Exit Sub

            End If


            Dim strAccountMain() As String = Split(grdMainAccounts.GetRow().Cells(GridCol.colSubAccountCode).Text, "-")
            txtAccountSub.Text = strAccountMain(strAccountMain.Length - 2).Trim & "-" & strAccountMain(strAccountMain.Length - 1).Trim


            Dim strTemp As String = grdMainAccounts.GetRow().Cells(GridCol.colSubSubAccountCode).Text

            txtSubSubAccountCode.Text = Microsoft.VisualBasic.Right(strTemp, Len(strTemp) - 7).ToString.Trim
            txtSubSubAccountTitle.Text = grdMainAccounts.GetRow().Cells(GridCol.colSubSubAccountTitle).Text
            cmbAccountType.Text = grdMainAccounts.GetRow().Cells(GridCol.colAccountType).Text
            cmbCrBSNote.Text = grdMainAccounts.GetRow().Cells(GridCol.colCrBSNote).Text
            cmbDrBSNote.Text = grdMainAccounts.GetRow().Cells(GridCol.colDrBSNote).Text
            cmbPLNote.Text = grdMainAccounts.GetRow().Cells(GridCol.colPLNote).Text
            'txtAccountSub.Focus()


            Me.ApplySecurity(EnumDataMode.Edit)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub


    ' Key Press Event Of Sub Sub Account KeyPress .. 
    Private Sub txtSubSubAccountCode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSubSubAccountCode.KeyPress

        Try
            If IsNumeric(e.KeyChar) Or Asc(e.KeyChar).ToString = 8 Or Asc(e.KeyChar).ToString = 46 Then
            Else
                e.Handled = True

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

        
    End Sub


    ' Lost Focus Event Of Sub Sub Account Code .. 
    Private Sub txtSubSubAccountCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSubSubAccountCode.LostFocus

        Try
            txtSubSubAccountCode.Text = txtSubSubAccountCode.Text.PadLeft(3, "0")

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub


    ' Navigation Buttons .. 
    Private Sub btnFirst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFirst.Click, btnNext.Click, btnPrevious.Click, btnLast.Click

        Try
            Dim btn As Button = CType(sender, Button)

            If btn.Name = Me.btnFirst.Name Then
                Me.grdMainAccounts.MoveFirst()

            ElseIf btn.Name = Me.btnPrevious.Name Then
                Me.grdMainAccounts.MovePrevious()

            ElseIf btn.Name = Me.btnNext.Name Then
                Me.grdMainAccounts.MoveNext()

            ElseIf btn.Name = Me.btnLast.Name Then
                Me.grdMainAccounts.MoveLast()

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub


    ' Click Event Of Cancel Button .. 
    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            Me.grdMainAccounts_SelectionChanged(sender, e)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub

#End Region

End Class