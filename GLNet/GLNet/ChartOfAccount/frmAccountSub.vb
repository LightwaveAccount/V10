''/////////////////////////////////////////////////////////////////////////////////////////
''// GL-NET ..                       
''/////////////////////////////////////////////////////////////////////////////////////////
''//-------------------------------------------------------------------------------------
''// File Name       : Account Sub .. 
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
Imports Microsoft.VisualBasic

Public Class frmAccountSub
    Implements IGeneral


#Region "Variables .. "
    Dim ObjModel As Model.AccountSubModel
    Private mobjControlList As NameValueCollection

#End Region
    
#Region "Enums .. "
    Private Enum GridCol
        colMainAccountID = 0
        colMainSubAccountID = 1
        colMainAccount = 2
        colMainSubAccountCode = 3
        colMainSubAccountTitle = 4

    End Enum


#End Region

#Region "IGeneral Methods .. "

    ' Apply Grid Settings .. 
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

        Try

            ' Giving Captions .. 
            grdSubAccounts.RootTable.Columns(GridCol.colMainAccountID).Caption = "Main Account ID"
            grdSubAccounts.RootTable.Columns(GridCol.colMainSubAccountID).Caption = "Sub Account ID"
            grdSubAccounts.RootTable.Columns(GridCol.colMainAccount).Caption = "A/C Main Code"
            grdSubAccounts.RootTable.Columns(GridCol.colMainSubAccountCode).Caption = "A/C Sub Code"
            grdSubAccounts.RootTable.Columns(GridCol.colMainSubAccountTitle).Caption = "A/C Sub Title"

            ' Hiding Columns .. 
            grdSubAccounts.RootTable.Columns(GridCol.colMainAccountID).Visible = False
            grdSubAccounts.RootTable.Columns(GridCol.colMainSubAccountID).Visible = False

            ' Totals Of Columns .. 
            grdSubAccounts.RootTable.Columns(GridCol.colMainAccount).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Count

            ' Auto Fit .. 
            grdSubAccounts.AutoSizeColumns()

            ' Hide Total Row ..
            grdSubAccounts.TotalRow = Janus.Windows.GridEX.InheritableBoolean.False

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
                Me.grdSubAccounts.Enabled = True


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
                Me.grdSubAccounts.Enabled = False


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

                Me.grdSubAccounts.Enabled = True
                Me.grdSubAccounts.Focus()


            ElseIf Mode.ToString = EnumDataMode.ReadOnly.ToString Then
                btnNew.Enabled = True
                btnSave.Enabled = False

                btnUpdate.Enabled = False
                btnDelete.Enabled = False
                btnCancel.Enabled = False

                SetNavigationButtons(Mode)

                Me.grdSubAccounts.Enabled = True
                Me.grdSubAccounts.Focus()

            End If


            ' Disable/Enable the Button that converts Grid data in Excel Sheet According to Login User rights ..
            If mobjControlList.Item("btnExport") Is Nothing Then
                Me.UiCtrlGridBar1.btnExport.Enabled = False

            End If


            ' Disable/Enable the Button that Prints Grid data According to Login User rights ..
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

            If New DAL.AccountSubDAL().Deleted(ObjModel) Then
                ReSetControls()
                Me.GetAllRecords(txtMainAccount.GLAccountID)
                Return True
                Exit Function

                'Else
                '    ShowErrorMessage("Unable to Delete this record because related information exists")
            End If


        Catch ex As Exception
            ShowErrorMessage("Error:" & ex.Message)

        Finally
            Me.Cursor = Cursors.Default

        End Try

    End Function


    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
    End Sub

    ' Fill Model .. 
    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel

        Try

            ObjModel = New Model.AccountSubModel

            ObjModel.AccountMainSubCode = txtMainAccount.txtACCode.Text & "-" & txtSubAccountCode.Text
            ObjModel.AccountMainSubTitle = Replace(txtSubAccountTitle.Text, "'", "''")
            ObjModel.AccountMainID = txtMainAccount.GLAccountID
            'CR#160
            ObjModel.ActivityLog.ShopID = 0 'gObjUserInfo.ShopInfo.ShopID
            ObjModel.ActivityLog.ScreenTitle = Me.Text
            ObjModel.ActivityLog.LogGroup = "Definition"
            ObjModel.ActivityLog.UserID = gObjUserInfo.UserID

            If grdSubAccounts.RowCount > 0 Then
                ObjModel.AccountMainSubID = grdSubAccounts.GetRow().Cells(GridCol.colMainSubAccountID).Text

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub

    ' Binding Grid Control .. Data Extracted From DB .. 
    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords

        Try
            Dim ObjDAL As New DAL.AccountSubDAL
            grdSubAccounts.DataSource = ObjDAL.GetAll(Condition)
            grdSubAccounts.RetrieveStructure()


            ApplyGridSettings()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub


    ' Validation Methods .. 
    Public Function IsValidate(Optional ByVal Mode As Utility.Utility.EnumDataMode = Utility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate

        Try
            If txtSubAccountCode.Text.Trim = "" Then
                txtSubAccountCode.Text = "000"

            End If


            If txtMainAccount.GLAccountID = 0 Then
                ShowValidationMessage("Please enter Main Account")
                txtMainAccount.txtACCode.Focus()
                Return False
                Exit Function


            ElseIf txtSubAccountTitle.Text.Trim = "" Then
                ShowValidationMessage("Please enter Sub Account Title")
                txtSubAccountTitle.Focus()
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
            txtSubAccountCode.Text = ""
            txtSubAccountTitle.Text = ""


            If txtMainAccount.GLAccountID = 0 Then Exit Sub
            txtSubAccountCode.Text = New DAL.AccountSubDAL().GetNewAccountMainSubCode(txtMainAccount.GLAccountID).ToString
            txtSubAccountCode.Focus()

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

            If Not New DAL.AccountSubDAL().IsAlreadyExists(ObjModel) Then
                If New DAL.AccountSubDAL().Save(ObjModel) Then
                    ReSetControls()
                    Me.GetAllRecords(txtMainAccount.GLAccountID)
                    Return True
                    Exit Function

                End If


            Else
                ShowErrorMessage("Duplicate Code/Name is not allowed .. ")
                txtSubAccountCode.Focus()
                Return False
                Exit Function

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default

        End Try

    End Function


    ' Setting Button Images .. 
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


    ' Setting Enabling/ Disabling Of Navigation Buttons Here .. 
    Public Sub SetNavigationButtons(ByVal Mode As Utility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons
        Try
            If Mode = EnumDataMode.[New] Then
                ' if New Mode then Set Disable all Navigation Buttons
                btnFirst.Enabled = False
                btnPrevious.Enabled = False
                btnNext.Enabled = False
                btnLast.Enabled = False

            ElseIf Mode = EnumDataMode.Edit Then
                ' if New Mode then Set Enable all Navigation Buttons
                btnFirst.Enabled = True
                btnPrevious.Enabled = True
                btnNext.Enabled = True
                btnLast.Enabled = True

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub


    ' Moving Of Records Through Navigation Buttons Is Done Here .. 
    Private Sub btnFirst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFirst.Click, btnNext.Click, btnPrevious.Click, btnLast.Click

        Try
            Dim btn As Button = CType(sender, Button)

            If btn.Name = Me.btnFirst.Name Then
                Me.grdSubAccounts.MoveFirst()

            ElseIf btn.Name = Me.btnPrevious.Name Then

                Me.grdSubAccounts.MovePrevious()
            ElseIf btn.Name = Me.btnNext.Name Then
                Me.grdSubAccounts.MoveNext()

            ElseIf btn.Name = Me.btnLast.Name Then
                Me.grdSubAccounts.MoveLast()

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub


    ' Update Method .. 
    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update

        Try

            Me.Cursor = Cursors.WaitCursor
            ' Filling Model .. 
            FillModel()

            If Not New DAL.AccountSubDAL().IsAlreadyExists(ObjModel, "Update") Then
                If New DAL.AccountSubDAL().Update(ObjModel) Then
                    ReSetControls()
                    Me.GetAllRecords(txtMainAccount.GLAccountID)
                    Return True
                    Exit Function

                End If


            Else
                ShowErrorMessage("Duplicate Code/Name is not allowed .. ")
                txtSubAccountCode.Focus()
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

#Region "Interface Methods .. "

    ' Form Key Down Event .. (Use To Handle ShortCut Keys .. )
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
                If Me.btnCancel.Enabled = True Then Me.grdSubAccounts_SelectionChanged(Nothing, Nothing)

            ElseIf e.Control And e.KeyCode = Keys.X Then
                If Me.btnExit.Enabled = True Then Me.Close()
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub

    ' Account Code Key Press Event .. 
    Private Sub txtSubAccountCode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSubAccountCode.KeyPress
        Try
            If IsNumeric(e.KeyChar) Or Asc(e.KeyChar).ToString = 8 Or Asc(e.KeyChar).ToString = 46 Then
            Else
                e.Handled = True

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub

    ' Lost Focus Event Of Sub Account Code .. 
    Private Sub txtSubAccountCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSubAccountCode.LostFocus
        Try
            txtSubAccountCode.Text = txtSubAccountCode.Text.PadLeft(3, "0")

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub

    ' Grid Row Selection Changed .. 
    Private Sub grdSubAccounts_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSubAccounts.SelectionChanged

        Try

            If Me.grdSubAccounts.RowCount > 0 Then
                If grdSubAccounts.GetRow().RowType = Janus.Windows.GridEX.RowType.TotalRow Or grdSubAccounts.GetRow().RowType = Janus.Windows.GridEX.RowType.GroupHeader Or grdSubAccounts.GetRow().RowType = Janus.Windows.GridEX.RowType.GroupFooter Then
                    Exit Sub

                End If
            End If

            If grdSubAccounts.RowCount = 0 Then
                ' Me.ReSetControls()
                Exit Sub
            End If

            If Me.grdSubAccounts.GetRow().RowType = Janus.Windows.GridEX.RowType.TotalRow Then
                Me.grdSubAccounts.Row = (Me.grdSubAccounts.Row - 1)
                Exit Sub
            End If

            Dim strAccountMain() As String = Split(grdSubAccounts.GetRow().Cells(GridCol.colMainAccount).Text, "-")

            txtMainAccount.Text = strAccountMain(strAccountMain.Length - 1).Trim

            Dim strTemp As String = grdSubAccounts.GetRow().Cells(GridCol.colMainSubAccountCode).Text

            txtSubAccountCode.Text = Microsoft.VisualBasic.Right(strTemp, Len(strTemp) - 3)
            txtSubAccountTitle.Text = grdSubAccounts.GetRow().Cells(GridCol.colMainSubAccountTitle).Text

            'txtMainAccount.Focus()
            Call ApplySecurity(EnumDataMode.Edit)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub

    ' Save Button .. 
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

    ' Update Button .. 
    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click

        Try

            If Me.grdSubAccounts.RowCount > 0 Then
                If grdSubAccounts.GetRow().RowType = Janus.Windows.GridEX.RowType.TotalRow Or grdSubAccounts.GetRow().RowType = Janus.Windows.GridEX.RowType.GroupHeader Or grdSubAccounts.GetRow().RowType = Janus.Windows.GridEX.RowType.GroupFooter Then
                    ShowValidationMessage("Nothing To Update ")
                    Exit Sub

                End If
            End If

            If grdSubAccounts.RowCount = 0 Then
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

    ' Delete Button .. 
    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click

        Try

            If Me.grdSubAccounts.RowCount > 0 Then
                If grdSubAccounts.GetRow().RowType = Janus.Windows.GridEX.RowType.TotalRow Or grdSubAccounts.GetRow().RowType = Janus.Windows.GridEX.RowType.GroupHeader Or grdSubAccounts.GetRow().RowType = Janus.Windows.GridEX.RowType.GroupFooter Then
                    ShowValidationMessage("Nothing To Delete ")
                    Exit Sub

                End If
            End If

            If grdSubAccounts.RowCount = 0 Then
                ShowValidationMessage("Nothing To Delete ")
                Exit Sub
            End If

            If IsValidate() Then
                'CR#143
                'If ShowConfirmationMessage("Do you want to delete this record?", MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                '    Me.Delete()

                'End If

                'CR#143

                'Backend validation checking tranasctions against Account selected for deletion
                If New DAL.AccountSubDAL().TransactionsExist(grdSubAccounts.GetRow().Cells(GridCol.colMainSubAccountID).Text) Then

                    ShowErrorMessage("Unable to Delete this record because related Sub Sub Account exists")

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

    ' New Button .. 
    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click

        Try
            If txtMainAccount.txtACCode.Text = "" Then
                ShowValidationMessage("Please Select Main Account InWhich You Want To Create Sub Account .. ")
                txtMainAccount.Focus()
                Exit Sub
            End If
            Me.ReSetControls()
            Me.ApplySecurity(EnumDataMode.[New])



        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try


    End Sub

    ' Load Event .. 
    Private Sub frmAccountSub_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            mobjControlList = GetFormSecurityControls(Me.Name)

            ' Assing Images to Buttons ..
            Me.SetButtonImages()

            ' Reseting Controls ..
            Me.ReSetControls()

            Me.ApplySecurity(EnumDataMode.[New])

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub

    ' Called Whenver User Click Help Window Of Account Heads .. 
    Private Sub txtMainAccount_GetGLAccount(ByVal sender As uiCtrlGLAccount) Handles txtMainAccount.GetGLAccount

        Try
            If txtMainAccount.txtACCode.Text = "" Then Exit Sub

            ' Reset Controls .. 
            ReSetControls()

            ' Binding Grid Control .. 
            Me.GetAllRecords(txtMainAccount.GLAccountID)


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

    ' Click Event Of Cancel Button .. 
    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        Try
            ' Reset Controls .. 
            ReSetControls()
            Me.ApplySecurity(EnumDataMode.Edit)
            Me.grdSubAccounts_SelectionChanged(Nothing, Nothing)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub


#End Region

End Class