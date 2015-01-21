''/////////////////////////////////////////////////////////////////////////////////////////
''// GL-NET ..                       
''/////////////////////////////////////////////////////////////////////////////////////////
''//-------------------------------------------------------------------------------------
''// File Name       : Account Main .. 
''// Programmer	     : Tariq Majeed
''// Creation Date	 : 
''// Description     : 
''// Function List   : 								                                    
''//											                                            
''//-------------------------------------------------------------------------------------
''// Date Modified     Modified by         Brief Description			                
''//15 Jul,2010        Abdul Jabbar         CR#143 Chart of Account should not be deleted if relevant detail account or Transaction exists.
'// 02 Dec,2011        Abdul Jabbar         CR#160,New changes Relevant to Service Broker/Data Log 
''//------------------------------------------------------------------------------------
''/////////////////////////////////////////////////////////////////////////////////////////


Imports DAL
Imports Model
Imports System.Collections.Specialized
Imports System.Data
Imports Utility.Utility
Imports DAL.SystemConfigurationDAL


Public Class frmAccountMain
    Implements IGeneral

#Region "Variables .. "
    Dim ObjModel As Model.AccountMainModel
    Private mobjControlList As NameValueCollection

#End Region

#Region "Enums .. "

    Private Enum GridCol
        colMainAccountID = 0
        colMainAccountCode = 1
        colMainAccountTitle = 2
        colMainAccountType = 3

    End Enum

#End Region

#Region "IGeneral Methods .. "

    ' Apply Grid Settings .. 
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

        Try

            ' Giving Captions .. 
            grdMainAccounts.RootTable.Columns(GridCol.colMainAccountID).Caption = "Main Account ID"
            grdMainAccounts.RootTable.Columns(GridCol.colMainAccountCode).Caption = "A/C Main Code"
            grdMainAccounts.RootTable.Columns(GridCol.colMainAccountTitle).Caption = "A/C Main Title"
            grdMainAccounts.RootTable.Columns(GridCol.colMainAccountType).Caption = "A/C Type"

            ' Hiding Columns .. 
            grdMainAccounts.RootTable.Columns(GridCol.colMainAccountID).Visible = False

            ' Totals Of Columns .. 
            grdMainAccounts.RootTable.Columns(GridCol.colMainAccountCode).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Count

            ' Auto Fit Columns .. 
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

            If New DAL.AccountMainDAL().Deleted(ObjModel) Then
                Me.GetAllRecords()
                Return True
                Exit Function

            Else
                ShowErrorMessage("Unable to Delete this record because related information exists")
            End If

        Catch ex As Exception
            ShowErrorMessage("Transaction Cannnot Be Performed .. ")
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Function

    ' Combos Are Filled In This Method .. 
    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
    End Sub

    ' Model Is Filling In This Method .. 
    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel

        Try

            ObjModel = New Model.AccountMainModel

            ObjModel.AccountMainCode = txtMainAccountCode.Text
            ObjModel.AccountMainTitle = Replace(txtMainAccountTitle.Text, "'", "''")
            ObjModel.AccountMainType = cmbAccountType.Text
            'CR#160
            ObjModel.ActivityLog.ShopID = 0 'gObjUserInfo.ShopInfo.ShopID
            ObjModel.ActivityLog.ScreenTitle = Me.Text
            ObjModel.ActivityLog.LogGroup = "Definition"
            ObjModel.ActivityLog.UserID = gObjUserInfo.UserID

            If grdMainAccounts.RowCount > 0 Then
                ObjModel.AccountMainID = grdMainAccounts.GetRow().Cells(GridCol.colMainAccountID).Text

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

        

    End Sub

    ' Grid Data Will Be Extracted In This Method From DB .. 
    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords

        Try

            Dim ObjDAL As New DAL.AccountMainDAL
            grdMainAccounts.DataSource = ObjDAL.GetAll
            grdMainAccounts.RetrieveStructure()


            ApplyGridSettings()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub

    ' Validations Checks Are Here .. 
    Public Function IsValidate(Optional ByVal Mode As Utility.Utility.EnumDataMode = Utility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate

        Try

            If txtMainAccountCode.Text.Trim = "" Then
                txtMainAccountCode.Text = "00"

            End If

            If txtMainAccountTitle.Text.Trim = "" Then
                ShowValidationMessage("Please enter Account Title")
                txtMainAccountTitle.Focus()
                Return False
                Exit Function

            End If

            Return True

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Function

    ' Reset Controls .. 
    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls

        Try

            ' Setting Controls To Intial Positions .. 
            txtMainAccountCode.Text = ""
            txtMainAccountTitle.Text = ""
            cmbAccountType.SelectedIndex = 0
            txtMainAccountCode.Focus()

            Me.ApplySecurity(EnumDataMode.[New])
            If btnCancel.Enabled <> True Then
                ' Setting New Code To Its Attached Fields .. 
                txtMainAccountCode.Text = New DAL.AccountMainDAL().GetNewAccountMainCode().ToString


            End If
            txtMainAccountCode.Focus()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub

    ' Save Button .. 
    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save

        Try

            Me.Cursor = Cursors.WaitCursor
            ' Filling Model .. 
            FillModel()
            If Not New DAL.AccountMainDAL().IsAlreadyExists(ObjModel) Then

                If New DAL.AccountMainDAL().Save(ObjModel) Then
                    ReSetControls()
                    Me.GetAllRecords()
                    Return True
                    Exit Function

                End If


            Else
                ShowErrorMessage("Duplicate Code/Name is not allowed .. ")
                txtMainAccountCode.Focus()
                Return False
                Exit Function

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Function

    ' Give Images To Buttons Here .. 
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

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting
    End Sub

    ' Navigation Buttons Security Are Handled Here .. 
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

    ' Movement According To Naviagtion Buttons Are Done Here .. 
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
        End Try

    End Sub

    ' Update Method .. 
    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update

        Try

            Me.Cursor = Cursors.WaitCursor
            ' Filling Model .. 
            FillModel()
            If Not New DAL.AccountMainDAL().IsAlreadyExists(ObjModel, "Update") Then

                If New DAL.AccountMainDAL().Update(ObjModel) Then
                    Me.GetAllRecords()
                    Return True
                    Exit Function

                End If


            Else
                ShowErrorMessage("Duplicate Code/Name is not allowed .. ")
                txtMainAccountCode.Focus()
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

#Region "Form Events .. "

    ' Form Key Down Event .. (Use To Handle Short Keys .. )
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

    ' Key Press Event Of Main Code Text Box .. 
    Private Sub txtMainAccountCode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMainAccountCode.KeyPress

        Try
            If IsNumeric(e.KeyChar) Or Asc(e.KeyChar).ToString = 8 Or Asc(e.KeyChar).ToString = 46 Then
            Else
                e.Handled = True

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    ' Lost Focus Event Of Main Account Code .. 
    Private Sub txtMainAccountCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMainAccountCode.LostFocus

        Try
            txtMainAccountCode.Text = txtMainAccountCode.Text.PadLeft(2, "0")

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub

    ' Grid Row Selection Changed .. 
    Private Sub grdMainAccounts_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdMainAccounts.SelectionChanged

        Try

            If Me.grdMainAccounts.RowCount <= 0 Or grdMainAccounts.GetRow().RowType = Janus.Windows.GridEX.RowType.TotalRow Or grdMainAccounts.GetRow().RowType = Janus.Windows.GridEX.RowType.GroupHeader Or grdMainAccounts.GetRow().RowType = Janus.Windows.GridEX.RowType.GroupFooter Then
                Me.ReSetControls()
                Exit Sub

            End If


            If Me.grdMainAccounts.GetRow().RowType = Janus.Windows.GridEX.RowType.TotalRow Then
                Me.grdMainAccounts.Row = (Me.grdMainAccounts.Row - 1)
                Exit Sub
            End If


            txtMainAccountCode.Text = grdMainAccounts.GetRow().Cells(GridCol.colMainAccountCode).Text
            txtMainAccountTitle.Text = grdMainAccounts.GetRow().Cells(GridCol.colMainAccountTitle).Text
            cmbAccountType.Text = grdMainAccounts.GetRow().Cells(GridCol.colMainAccountType).Text

            txtMainAccountCode.Focus()
            Call ApplySecurity(EnumDataMode.Edit)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub

    ' Click Event Of Save Button .. 
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

    ' Click Event Of Update Button .. 
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

    ' Click Event Of Delete Button .. 
    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click

        Try

            If Me.grdMainAccounts.RowCount <= 0 Or grdMainAccounts.GetRow().RowType = Janus.Windows.GridEX.RowType.TotalRow Or grdMainAccounts.GetRow().RowType = Janus.Windows.GridEX.RowType.GroupHeader Or grdMainAccounts.GetRow().RowType = Janus.Windows.GridEX.RowType.GroupFooter Then
                ShowValidationMessage("Nothing To Delete ")
                Exit Sub

            End If



            If IsValidate() Then

                'If ShowConfirmationMessage("Do you want to delete this record?", MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                '    Me.Delete()
                'End If

                'CR#143
                'If ShowConfirmationMessage("Do you want to delete this record?", MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                '    Me.Delete()

                'End If

                'CR#143

                'Backend validation checking tranasctions against Account selected for deletion
                If New DAL.AccountMainDAL().TransactionsExist(grdMainAccounts.GetRow().Cells(GridCol.colMainAccountID).Text) Then

                    ShowErrorMessage("Unable to Delete this record because related Sub Account exists")

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

    ' Form Load .. 
    Private Sub frmAccountMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            mobjControlList = GetFormSecurityControls(Me.Name)

            ' Binding Grid Control .. 
            Me.GetAllRecords()

            ' Assing Images to Buttons ..
            Me.SetButtonImages()


            ' Reseting Controls . 
            Me.ReSetControls()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub

    ' Click Event Of New Button .. 
    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click

        Try
            Me.ReSetControls()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub

    ' Click Event Of Exit Button .. 
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click

        Try
            Me.Close()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub

    ' Click Evnet Of Cancel Button .. 
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        Try
            Me.grdMainAccounts_SelectionChanged(sender, e)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub

#End Region

End Class