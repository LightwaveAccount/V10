''/////////////////////////////////////////////////////////////////////////////////////////
''// GL-NET ..                       
''/////////////////////////////////////////////////////////////////////////////////////////
''//-------------------------------------------------------------------------------------
''// File Name       : Detail Account .. 
''// Programmer	     : Tariq Majeed
''// Creation Date	 : 
''// Description     : 
''// Function List   : 								                                    
''//											                                            
''//-------------------------------------------------------------------------------------
''// Date Modified     Modified by          Brief Description	
''//10 Feb,2010        Abdul Jabbar         Ref#00 On pressing Ctrl+N functionality is not ok,Ctrl+N should perform functionality of New button click...Active/Deactive Status was not getting clear 
''//15 Jul,2010        Abdul Jabbar         CR#143 Chart of Account should not be deleted if relevant detail account or Transaction exists.
''//10-Oct-2011        Asif Kamal           CR# 151 Account Detail search on Account detail form.
'// 02 Dec,2011        Abdul Jabbar         CR#160,New changes Relevant to Service Broker/Data Log 
''//13 Dec,2011        Asif Kamal           CR#166 Lightwave Issues need to be fixed.
''//20 March 2012      Fatima Tajammal      CR # 182 Detail Account: Exception is occurring on pressing Cancel button
''//24-Dec-2012         Fatima Tajammal     CR # 226 GL Chart of Accout should not be delete if GL integration is On and COA account mapped.
'// 18 june,2013        farooq-h            CR#254,SMS Implmentation in Lightwave
''// 323     23 Jul,2014          farooq-H   Cheque Printing: add cheque printing on voucher screen for bank payment voucher
'// 02-Oct-2014        M. Shoaib            CR# 330, Lightwave COA Detail: Select cheque type should work properly
'// 31-Dec-2014        Fatima Tajammal      CR # 353    Detail Account: Search On Detail Account works perfectly only once
''//------------------------------------------------------------------------------------
''/////////////////////////////////////////////////////////////////////////////////////////

Imports DAL
Imports Model
Imports System.Collections.Specialized
Imports System.Data
Imports Utility.Utility
Imports DAL.SystemConfigurationDAL


Public Class frmDetailAccount
    Implements IGeneral

#Region "Variables .. "
    Dim ObjModel As Model.AccountSubSubDetailModel
    Private mobjControlList As NameValueCollection
    '   Farooq-H            CR#254
    Dim mObjSMS As New SMSConfigurationDAL
#End Region

#Region "Enums .. "
    Private Enum GridCol

        colDetailID = 0
        colSubSubCode = 1
        colDetailCode = 2
        colDetailTitle = 3
        colEndDateStatus = 4
        colSubSubDetailID = 5
        colChequeType = 6  '''// 323     23 Jul,2014          farooq-H  

    End Enum

#End Region

#Region "IGeneral Methods .. "

    ' Apply Grid Settings .. 
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

        Try
            ' Giving Captions .. 
            grdMainAccounts.RootTable.Columns(GridCol.colDetailID).Caption = "Detail ID"
            grdMainAccounts.RootTable.Columns(GridCol.colSubSubCode).Caption = "A/C Sub Sub Code"
            grdMainAccounts.RootTable.Columns(GridCol.colDetailCode).Caption = "A/C Detail Code"
            grdMainAccounts.RootTable.Columns(GridCol.colDetailTitle).Caption = "A/C Detail Title"
            grdMainAccounts.RootTable.Columns(GridCol.colEndDateStatus).Caption = "Status"
            grdMainAccounts.RootTable.Columns(GridCol.colSubSubDetailID).Caption = "Sub Sub Detail ID"

            ' Hiding Columns .. 
            grdMainAccounts.RootTable.Columns(GridCol.colDetailID).Visible = False
            grdMainAccounts.RootTable.Columns(GridCol.colSubSubDetailID).Visible = False

            ' Totals Of Columns .. 
            grdMainAccounts.RootTable.Columns(GridCol.colSubSubCode).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Count

            ' Auto Fit .. 
            grdMainAccounts.AutoSizeColumns()

            ' Hide Total Row ..
            grdMainAccounts.TotalRow = Janus.Windows.GridEX.InheritableBoolean.False

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try


    End Sub

    ' Security Is Applied In This Method .. 
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

            If New DAL.AccountSubSubDetailDAL().Deleted(ObjModel) Then
                'change by Farooq-H  CR# 254 
                Try
                    mObjSMS.SendSMS(EnumSMSCodes.SMS_COA_DT_03.ToString, "&AccountCode&=" & Me.ObjModel.DetailCode.ToString & ";&AccountName&=" & Me.ObjModel.DetailTitle.ToString & ";&Status&=" & Me.ObjModel.EndDateFlag.ToString & " ")
                Catch ex As Exception
                End Try
                ReSetControls()
                Me.GetAllRecords(txtAccountSubSub.GLAccountID)
                Return True
                Exit Function

            End If



        Catch ex As Exception
            ShowErrorMessage("Error:" & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim ChequeType As DataTable
            'dvChargeToList = GetFilterDataFromDataTable(CType(gObjMyAppHashTable(EnumHashTableKeyConstants.GetChargeToList.ToString), DataTable), "")
            ChequeType = New DAL.AccountSubSubDetailDAL().GetChequeType() ' VoucherDAL().GetVoucherType()   'New DAL.AccountSubSubDetailDAL().Save(ObjModel)
            Me.CboChequeType.ValueMember = "Cheque_id"
            Me.CboChequeType.DisplayMember = "Cheque Name"
            Me.CboChequeType.DataSource = ChequeType
            Me.CboChequeType.SelectedIndex = 0
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ' Fill Model .. 
    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel

        Try
            ObjModel = New Model.AccountSubSubDetailModel

            ObjModel.SubSubAccountID = txtAccountSubSub.GLAccountID
            ObjModel.DetailCode = txtAccountSubSub.txtACCode.Text & "-" & txtDetailCode.Text
            ObjModel.DetailTitle = Replace(txtDetailTitle.Text, "'", "''")
            ''''// 323     23 Jul,2014          farooq-H  
            ObjModel.ChequeID = CboChequeType.SelectedValue
            'CR#160
            ObjModel.ActivityLog.ShopID = 0 'gObjUserInfo.ShopInfo.ShopID
            ObjModel.ActivityLog.ScreenTitle = Me.Text
            ObjModel.ActivityLog.LogGroup = "Definition"
            ObjModel.ActivityLog.UserID = gObjUserInfo.UserID
           

            If grdMainAccounts.RowCount > 0 Then
                ObjModel.DetailID = grdMainAccounts.GetRow().Cells(GridCol.colDetailID).Text

            End If

            ' Check Statuses .. 
            If chkStatus.Checked = True Then
                ObjModel.EndDateFlag = 1

            Else
                ObjModel.EndDateFlag = 0

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub

    ' Binding Grid Control With Extracted Db Data .. 
    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim dt As DataTable
            Dim dr As DataRow
            Dim counter As Integer
            Dim RowIndex As Integer


            Dim ObjDAL As New DAL.AccountSubSubDetailDAL
            dt = ObjDAL.GetAll(Condition)


            'For counter = 0 To dt.Rows.Count - 1
            '    dr = dt.Rows.Find(GridCol.colDetailID)
            '    If Not dr Is Nothing Then

            '        RowIndex = Me.grdMainAccounts.Row()
            '        Me.grdMainAccounts.Row() = RowIndex

            '    End If

            'Next

            grdMainAccounts.DataSource = dt
            grdMainAccounts.RetrieveStructure()

            ApplyGridSettings()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub

    ' Check Validation .. 
    Public Function IsValidate(Optional ByVal Mode As Utility.Utility.EnumDataMode = Utility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate

        Try

            If txtDetailCode.Text.Trim = "" Then
                txtDetailCode.Text = "00000"

            End If


            If txtAccountSubSub.GLAccountID = 0 Then
                ShowValidationMessage("Please enter Account Sub Sub")
                txtAccountSubSub.txtACCode.Focus()
                Return False
                Exit Function
            End If


            If txtDetailTitle.Text.Trim = "" Then
                ShowValidationMessage("Please enter Sub Sub Account Title")
                txtDetailTitle.Focus()
                Return False
                Exit Function


            End If
            'CR#143
            'CR # 226
            If Mode = EnumDataMode.Edit Then
                'Backend validation checking tranasctions against Account selected for deletion
                If New DAL.AccountSubSubDetailDAL().TransactionsExist(grdMainAccounts.GetRow().Cells(GridCol.colDetailID).Text) Then
                    ShowErrorMessage("Unable to Delete this record because related information (vouchers) exists")
                    Return False
                Else
                    'CR # 226
                    If Utility.Utility.SymmetricEncryption.Decrypt(New DAL.AccountSubSubDetailDAL().IsGLIntegrated(), "f") = "1" Then
                        If New DAL.AccountSubSubDetailDAL().IsMappingExsist(EnumMappingChk.System_Configuration, grdMainAccounts.GetRow().Cells(GridCol.colDetailID).Text) Then
                            ShowErrorMessage("Chart of account '" & grdMainAccounts.GetRow().Cells(GridCol.colDetailTitle).Text & "' can't be deleted as mapping exists in (Candela) System Configuration ")
                            Return False
                        ElseIf New DAL.AccountSubSubDetailDAL().IsMappingExsist(EnumMappingChk.Shop_Defination, grdMainAccounts.GetRow().Cells(GridCol.colDetailID).Text) Then
                            ShowErrorMessage("Chart of account '" & grdMainAccounts.GetRow().Cells(GridCol.colDetailTitle).Text & "' can't be deleted as mapping exists in (Candela) Shop Definition")
                            Return False
                        ElseIf New DAL.AccountSubSubDetailDAL().IsMappingExsist(EnumMappingChk.Customer, grdMainAccounts.GetRow().Cells(GridCol.colDetailID).Text) Then
                            ShowErrorMessage("Chart of account '" & grdMainAccounts.GetRow().Cells(GridCol.colDetailTitle).Text & "' can't be deleted as mapping exists in (Candela) Customer Definition")
                            Return False
                        ElseIf New DAL.AccountSubSubDetailDAL().IsMappingExsist(EnumMappingChk.Supplier, grdMainAccounts.GetRow().Cells(GridCol.colDetailID).Text) Then
                            ShowErrorMessage("Chart of account '" & grdMainAccounts.GetRow().Cells(GridCol.colDetailTitle).Text & "' can't be deleted as mapping exists in (Candela) Supplier Definition")
                            Return False
                        ElseIf New DAL.AccountSubSubDetailDAL().IsMappingExsist(EnumMappingChk.Line_Item, grdMainAccounts.GetRow().Cells(GridCol.colDetailID).Text) Then
                            ShowErrorMessage("Chart of account '" & grdMainAccounts.GetRow().Cells(GridCol.colDetailTitle).Text & "' can't be deleted as mapping exists in (Candela) Line Item Definition")
                            Return False
                        ElseIf New DAL.AccountSubSubDetailDAL().IsMappingExsist(EnumMappingChk.Acount_Heads, grdMainAccounts.GetRow().Cells(GridCol.colDetailID).Text) Then
                            ShowErrorMessage("Chart of account '" & grdMainAccounts.GetRow().Cells(GridCol.colDetailTitle).Text & "' can't be deleted as mapping exists in (Candela) Acount Heads Definition")
                            Return False
                        End If
                    End If
                End If
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
            ' CR#166    Asif Kamal      Lightwave Issues need to be fixed.
            txtDetailAccount.txtACCode.Text = ""
            txtDetailAccount.txtAccountName.Text = ""
            '------------
            txtDetailCode.Text = ""
            txtDetailTitle.Text = ""
            txtDetailCode.Focus()


            If txtAccountSubSub.GLAccountID = 0 Then Exit Sub

            ' Setting New Code To Its Attached Fields .. 
            txtDetailCode.Text = New DAL.AccountSubSubDetailDAL().GetNewAccountMainCode(txtAccountSubSub.GLAccountID).ToString

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub

    ' Click Event Of Save Button .. 
    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save

        Try

            Me.Cursor = Cursors.WaitCursor
            ' Filling Model .. 
            FillModel()

            If Not New DAL.AccountSubSubDetailDAL().IsAlreadyExists(ObjModel) Then
                If New DAL.AccountSubSubDetailDAL().Save(ObjModel) Then
                    'change by Farooq-H  CR# 254 
                    Try
                        mObjSMS.SendSMS(EnumSMSCodes.SMS_COA_DT_01.ToString, "&AccountCode&=" & Me.ObjModel.DetailCode.ToString & ";&AccountName&=" & Me.ObjModel.DetailTitle.ToString & ";&Status&=" & Me.ObjModel.EndDateFlag.ToString & " ")
                    Catch ex As Exception
                    End Try
                    Me.ReSetControls()
                    Me.GetAllRecords(txtAccountSubSub.GLAccountID)
                    Return True
                    Exit Function

                End If


            Else
                ShowErrorMessage("Duplicate Code/Name is not allowed .. ")
                txtAccountSubSub.Focus()
                Return False
                Exit Function

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Function

    ' Applying Button Images .. 
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

    ' Update Method .. 
    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update

        Try

            Me.Cursor = Cursors.WaitCursor

            ' Filling Model .. 
            FillModel()

            If Not New DAL.AccountSubSubDetailDAL().IsAlreadyExists(ObjModel, "Update") Then
                If New DAL.AccountSubSubDetailDAL().Update(ObjModel) Then
                    'change by Farooq-H  CR# 254 
                    Try
                        If ObjModel.EndDateFlag Then
                            mObjSMS.SendSMS(EnumSMSCodes.SMS_COA_DT_04.ToString, "&AccountCode&=" & Me.ObjModel.DetailCode.ToString & ";&AccountName&=" & Me.ObjModel.DetailTitle.ToString & ";&Status&=" & Me.ObjModel.EndDateFlag.ToString & " ")
                        Else
                            mObjSMS.SendSMS(EnumSMSCodes.SMS_COA_DT_02.ToString, "&AccountCode&=" & Me.ObjModel.DetailCode.ToString & ";&AccountName&=" & Me.ObjModel.DetailTitle.ToString & ";&Status&=" & Me.ObjModel.EndDateFlag.ToString & " ")
                        End If
                    Catch ex As Exception
                    End Try
                    Me.ReSetControls()
                    Me.GetAllRecords(txtAccountSubSub.GLAccountID)
                    Return True
                    Exit Function

                End If


            Else
                ShowErrorMessage("Duplicate Code/Name is not allowed .. ")
                txtAccountSubSub.Focus()
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

    ' Form Close Button .. (Exit Button .. )
    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            Me.Close()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub


    ' Gl-Account Click Event .. 
    Private Sub txtAccountSub_GetGLAccount(ByVal sender As uiCtrlGLAccount) Handles txtAccountSubSub.GetGLAccount

        Try
            

            If txtAccountSubSub.txtACCode.Text = "" Then Exit Sub
            ReSetControls()

            ' Binding Grid Control .. 
            Me.GetAllRecords(txtAccountSubSub.GLAccountID)
            txtDetailCode.Focus()
            Me.txtDetailAccount.txtACCode.Text = ""
            Me.txtDetailAccount.txtAccountName.Text = ""

            'Code commented against CR # 353
            'If Me.txtAccountSubSub.txtAccountName.Text <> "" Then
            '    Me.txtDetailAccount.GLFilterCondition = sender.GLAccountID
            '    Me.txtDetailAccount.GLAccountHeadType = 4
            'Else
            '    Me.txtDetailAccount.GLAccountHeadType = 4
            'End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
        

    End Sub


    ' Form Key Down Event .. (Handling ShortCut Keys .. )
    Private Sub frmDetailAccount_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        Try
            If e.Control And e.KeyCode = Keys.S Then
                If Me.btnSave.Enabled = True Then btnSave_Click(Nothing, Nothing)

            ElseIf e.Control And e.KeyCode = Keys.U Then
                If Me.btnUpdate.Enabled = True Then btnUpdate_Click(Nothing, Nothing)

            ElseIf e.Control And e.KeyCode = Keys.D Then
                If Me.btnDelete.Enabled = True Then btnDelete_Click(Nothing, Nothing)

            ElseIf e.Control And e.KeyCode = Keys.N Then
                'Ref#00

                'If Me.btnNew.Enabled = True Then
                '    If txtAccountSubSub.txtACCode.Text = "" Then
                '        ShowValidationMessage("Please Select Main Sub Sub Account InWhich You Want To Create Detail Account .. ")
                '        txtAccountSubSub.txtACCode.Focus()
                '        Exit Sub
                '    End If

                '    ' Me.ReSetControls()
                '    ' Setting New Code To Its Attached Fields .. 
                '    txtDetailCode.Text = New DAL.AccountSubSubDetailDAL().GetNewAccountMainCode(txtAccountSubSub.GLAccountID).ToString
                '    txtDetailTitle.Text = ""
                '    txtDetailCode.Focus()

                '    Me.ApplySecurity(EnumDataMode.[New])

                'End If

                btnNew_Click(Nothing, Nothing)

            ElseIf e.Control And e.KeyCode = Keys.E Then
                If Me.btnCancel.Enabled = True Then Me.grdMainAccounts_SelectionChanged(Nothing, Nothing)

            ElseIf e.Control And e.KeyCode = Keys.X Then
                If Me.btnExit.Enabled = True Then Me.Close()
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub


    ' Form Load Event .. 
    Private Sub frmSubSubAccountDetail_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            mobjControlList = GetFormSecurityControls(Me.Name)
            Me.ApplySecurity(EnumDataMode.[New])

            ' Assing Images to Buttons ..
            Me.SetButtonImages()
            '' change by farooq-H           ''// 323     23 Jul,2014          farooq-H  
            FillCombos()
            Me.ReSetControls()


        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub


    ' Grid Row Selection Change Event .. 
    Private Sub grdMainAccounts_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdMainAccounts.SelectionChanged

        Try
            'CR # 182 Detail Account: Exception is occurring on pressing Cancel button
            If Not Me.grdMainAccounts.RowCount = 0 Then
                If Not (grdMainAccounts.RowCount = 0 Or grdMainAccounts.GetRow().RowType = Janus.Windows.GridEX.RowType.TotalRow Or grdMainAccounts.GetRow().RowType = Janus.Windows.GridEX.RowType.GroupHeader Or grdMainAccounts.GetRow().RowType = Janus.Windows.GridEX.RowType.GroupFooter) Then

                    Dim strAccountMain() As String = Split(grdMainAccounts.GetRow().Cells(GridCol.colDetailCode).Text, "-")
                    txtAccountSubSub.Text = strAccountMain(0).Trim & "-" & strAccountMain(1).Trim & "-" & strAccountMain(2).Trim

                    Dim strTemp As String = grdMainAccounts.GetRow().Cells(GridCol.colDetailCode).Text

                    txtDetailCode.Text = Microsoft.VisualBasic.Right(strTemp, Len(strTemp) - 11).ToString.Trim
                    txtDetailTitle.Text = grdMainAccounts.GetRow().Cells(GridCol.colDetailTitle).Text

                    'chanage by farooq-H   ''// 323     23 Jul,2014          farooq-H  
                    'Me.CboChequeType.Text = grdMainAccounts.GetRow().Cells(GridCol.colChequeType).Text
                    ' CR # 330 
                    If grdMainAccounts.GetRow().Cells(GridCol.colChequeType).Text = "" Then
                        Me.CboChequeType.SelectedIndex = 0
                    Else
                        Me.CboChequeType.Text = grdMainAccounts.GetRow().Cells(GridCol.colChequeType).Text
                    End If
                    ' End CR # 330

                    If grdMainAccounts.GetRow().Cells(GridCol.colEndDateStatus).Text = "Open" Then
                        chkStatus.Checked = False
                    Else
                        chkStatus.Checked = True

                    End If

                End If
            End If
            ' txtDetailCode.Focus()
            Call ApplySecurity(EnumDataMode.Edit)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

        
    End Sub


    ' Detail Code Key Press Event . 
    Private Sub txtDetailCode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDetailCode.KeyPress

        Try
            If IsNumeric(e.KeyChar) Or Asc(e.KeyChar).ToString = 8 Or Asc(e.KeyChar).ToString = 46 Then
            Else
                e.Handled = True

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
        
    End Sub


    ' Detail Code Lost Focus Event .. 
    Private Sub txtSubSubAccountCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDetailCode.LostFocus
        Try
            txtDetailCode.Text = txtDetailCode.Text.PadLeft(5, "0")

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try


    End Sub


    ' Update Click Event .. 
    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click

        Try

            If Me.grdMainAccounts.RowCount > 0 Then
                If grdMainAccounts.GetRow().RowType = Janus.Windows.GridEX.RowType.TotalRow Or grdMainAccounts.GetRow().RowType = Janus.Windows.GridEX.RowType.GroupHeader Or grdMainAccounts.GetRow().RowType = Janus.Windows.GridEX.RowType.GroupFooter Then
                    ShowValidationMessage("Nothing To Update ")
                    Exit Sub

                End If
            End If


            If Me.grdMainAccounts.RowCount <= 0 Then
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


    ' Delete Click Event .. 
    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try

            If Me.grdMainAccounts.RowCount > 0 Then
                If grdMainAccounts.GetRow().RowType = Janus.Windows.GridEX.RowType.TotalRow Or grdMainAccounts.GetRow().RowType = Janus.Windows.GridEX.RowType.GroupHeader Or grdMainAccounts.GetRow().RowType = Janus.Windows.GridEX.RowType.GroupFooter Then
                    ShowValidationMessage("Nothing To Delete ")
                    Exit Sub

                End If
            End If


            If Me.grdMainAccounts.RowCount <= 0 Then
                ShowValidationMessage("Nothing To Delete ")
                Exit Sub

            End If



            'CR # 226
            'If IsValidate() Then
            If IsValidate(EnumDataMode.Edit) Then

                If ShowConfirmationMessage("Do you want to delete this record?", MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                    Me.Delete()

                End If



            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub


    ' Save Click Buton .. 
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


    ' New Button Click Event .. 
    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click

        Try

            If txtAccountSubSub.txtACCode.Text = "" Then
                ShowValidationMessage("Please Select Main Sub Sub Account InWhich You Want To Create Detail Account .. ")
                txtAccountSubSub.txtACCode.Focus()
                ' CR#166    Asif Kamal      Lightwave Issues need to be fixed.
                txtDetailAccount.txtACCode.Text = ""
                txtDetailAccount.txtAccountName.Text = ""
                Exit Sub
            End If

            ' Me.ReSetControls()
            ' Setting New Code To Its Attached Fields .. 
            txtDetailCode.Text = New DAL.AccountSubSubDetailDAL().GetNewAccountMainCode(txtAccountSubSub.GLAccountID).ToString

            ' CR#166    Asif Kamal      Lightwave Issues need to be fixed.
            txtDetailAccount.txtAccountName.Text = ""
            txtDetailAccount.txtACCode.Text = ""
            txtDetailAccount.txtAccountName.Text = ""
            txtDetailTitle.Text = ""
            Me.chkStatus.Checked = False
            txtDetailCode.Focus()

            'chanage by farooq-h            ''// 323     23 Jul,2014          farooq-H  
            Me.CboChequeType.SelectedIndex = 0

            Me.ApplySecurity(EnumDataMode.[New])


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

    Private Sub btnFirst_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFirst.Click, btnNext.Click, btnPrevious.Click, btnLast.Click
        Try

            Dim btn As Button = CType(sender, Button)

            ''If Move First is clicked, then navigate to first record of the grid
            If btn.Name = Me.btnFirst.Name Then
                Me.grdMainAccounts.Row = 0

                ''If Move Previous is clicked, then navigate to Previous record of the grid
            ElseIf btn.Name = Me.btnPrevious.Name Then
                If Me.grdMainAccounts.Row > 0 Then Me.grdMainAccounts.Row = (Me.grdMainAccounts.Row - 1)

                ''If Move Next is clicked, then navigate to next record of the grid
            ElseIf btn.Name = Me.btnNext.Name Then
                'Code commented against CR # 353
                'If Me.grdMainAccounts.Row >= 0 Then Me.grdMainAccounts.Row = (Me.grdMainAccounts.Row + 1)
                'CR # 353
                If Me.grdMainAccounts.Row < (Me.grdMainAccounts.RowCount - 1) Then Me.grdMainAccounts.Row = (Me.grdMainAccounts.Row + 1)


                ''If Move Last is clicked, then navigate to Last record of the grid
            ElseIf btn.Name = Me.btnLast.Name Then
                Me.grdMainAccounts.Row = (Me.grdMainAccounts.RowCount - 1)

            End If

        Catch ex As Exception
            Throw ex

        End Try

    End Sub


    ' 10-Oct-2011       Asif Kamal      CR# 151 Account Detail search on Account detail form.

    Private Sub txtDetailAccount_GetGLAccount(ByVal sender As GLNet.uiCtrlGLAccount) Handles txtDetailAccount.GetGLAccount
        Try
            

            Me.txtAccountSubSub.txtACCode.Text = Me.txtDetailAccount.txtACCode.Text.Substring(0, 10)
            Me.txtAccountSubSub.txtAccountName.Text = New AccountSubSubDetailDAL().GetAccountName(Me.txtAccountSubSub.txtACCode.Text.ToString())
            Me.txtAccountSubSub.GLAccountID = New AccountSubSubDetailDAL().GetSubSubGLId(Me.txtDetailAccount.GLAccountID)


            ' Getting Detail Accounts Against the SubSubAccounts
            'COde commented against CR # 353 
            'If Me.txtAccountSubSub.txtAccountName.Text <> "" Then
            '    Me.txtDetailAccount.GLFilterCondition = Me.txtAccountSubSub.GLAccountID
            '    Me.txtDetailAccount.GLAccountHeadType = 4
            'Else
            '    Me.txtDetailAccount.GLAccountHeadType = 4
            'End If


            'If Me.grdMainAccounts.DataSource Is Nothing Then
            Me.GetAllRecords(Me.txtAccountSubSub.GLAccountID)
            'End If

            Dim dtGrid As DataTable = CType(Me.grdMainAccounts.DataSource, DataTable)
            Dim drFound As DataRow

            If dtGrid.Rows.Count > 0 Then
                dtGrid.Constraints.Clear()
                Dim uq As New UniqueConstraint("coa_detail_id", dtGrid.Columns(GridCol.colDetailID), True)
                dtGrid.Constraints.Add(uq)
                drFound = dtGrid.Rows.Find(Me.txtDetailAccount.GLAccountID)
            End If

            If Not drFound Is Nothing Then
                Me.grdMainAccounts.Row = Me.grdMainAccounts.GetRow(drFound).RowIndex
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub
End Class