''/////////////////////////////////////////////////////////////////////////////////////////
''// GL-NET ..                       
''/////////////////////////////////////////////////////////////////////////////////////////
''//-------------------------------------------------------------------------------------
''// File Name       : Searching/ Posting Vouchers .. 
''// Programmer	     : Tariq Majeed
''// Creation Date	 : 
''// Description     : 
''// Function List   : 								                                    
''//											                                            
''//-------------------------------------------------------------------------------------
''// Date Modified     Modified by         Brief Description			                
''//------------------------------------------------------------------------------------
''// 08 Jan,2010       Abdul Jabbar       CR#4: Vouchers which were posted from Candela (External) should not be unposted from GL.
''// 08 Jan,2010       Abdul Jabbar       CR#5: Source combo should have POS Code, which could be used to filter vouchers; posted from Candela. 
''// 05 Apr,2010       Abdul Jabbar       CR#30:Filter voucher type drop down for SV/PV on Temporary voucher Search/Post screen.
''// 29 Jun,2010       Abdul Jabbar       CR#60:Voucher Post/unpost process is slow
''// 29 Jun,2010       Abdul Jabbar       CR#61:System should maintain Acivity log of Voucher Post/Unpost process
''// 12 Oct,2010       Abdul Jabbar       CR#71:Select 'All' Voucher option on voucher POST/UNPOST activity.
''// 14 Jan,2011       Abdul Jabbar       CR#117:Disable Save button on Search/Post screen of Temporary vouchers.
''// 7  Dec,2011       Fatima Tajammal    CR#152:Search Voucher option: should be optimize. time out expire error appear
''// 08 Dec,2011       Abdul Jabbar       CR#160:(changes in DAL)    Service Broker relevant changes in GL , when SSB will be On identity of both voucher master and detail will be off, now we will use shop id in where clause with shop_id<=0 so pick next voucher id in head office 
''// 16-Feb-2012       Abdul Jabbar       CR#194 Default date for start date & end date on form load should be current date and current date -30 respectively
''// 26-Jul-2012       Abdul Jabbar       CR#215 Voucher: System allow posting of unbalanced voucher from Search/Post Screen.
''// 03-apr-2013       Fatima Tajammal    CR#233 Posting/Unposting of vouchers should be right based
''// 03-May-2012       Abdul Jabbar       CR#238 Voucher Searh/Post screen should show unposted voucher by default
'// 18 june,2013        farooq-h            CR#254,SMS Implmentation in Lightwave
'// 19-jul-2013        Fatima Tajammal    Cr#248 Voucher Search Post screen; as unpost voucher is selected by default ,post voucher button should be visible. 
''//09-Jun-2014        Abdul Jabbar       CR#300 Voucher: Company Wise & shop wise voucher entry option should be available.
''//14-Jul-2014        Abdul Jabbar       CR#318 Voucher Search Post: On selection (double click) on voucher message is showing 'Please select a valid company'
''//18 Jul,2014        Abdul Jabbar       CR#322: Voucher search required against voucher amount, invoice no. & cheque number
''//02-jan-2015        Fatima             CR#356 Fix bugs on Voucher screen


Imports DAL
Imports Model
Imports System.Collections.Specialized
Imports System.Data
Imports Utility.Utility
Imports DAL.SystemConfigurationDAL

Public Class frmGLPostingVoucher
    Implements IGeneral

#Region "Variables .. "
    Dim PostUnPostStatus As String ' For Query Purpose .. 
    Dim BalancedUnBalancedStatus As String ' For Query Purpose .. 
    Private pbLocationID As Integer = gobjLocationInfo.CompanyID
    Dim ObjModel As Model.PostingVouchersModel
    Dim objModalVoucher As GLVoucher
    Dim VouchersArray As New ArrayList

    Private mobjControlList As NameValueCollection
    Dim blnLoad As Boolean = False
    Dim blnFormLoaded As Boolean = False

    '   Farooq-H            CR#254
    Dim mObjSMS As New SMSConfigurationDAL
    Dim Voucher_Code As String = ""
    Dim TotalDabit As Double = 0.0
#End Region

#Region "Enums .. "
    Private Enum GridCol
        colCheckBoxValue = 0
        colVoucherCode = 1
        colVoucherDate = 2
        colYearCode = 3
        colLocationName = 4
        colDebitAmount = 5
        colCreditAmount = 6
        colVoucherID = 7
        colLocationID = 8
        colSource = 9
        colIsLightwaveV = 10

    End Enum
#End Region

#Region "IGeneral Methods .. "

    ' Applying Grid Settings .. 
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

        Try

            ' Giving Captions .. 
            grdMainAccounts.RootTable.Columns(GridCol.colCheckBoxValue).Caption = "Post"
            grdMainAccounts.RootTable.Columns(GridCol.colVoucherCode).Caption = "Voucher No"
            grdMainAccounts.RootTable.Columns(GridCol.colVoucherDate).Caption = "Date"
            grdMainAccounts.RootTable.Columns(GridCol.colYearCode).Caption = "Financial Year"
            grdMainAccounts.RootTable.Columns(GridCol.colLocationName).Caption = "Location"
            grdMainAccounts.RootTable.Columns(GridCol.colDebitAmount).Caption = "Debit"
            grdMainAccounts.RootTable.Columns(GridCol.colCreditAmount).Caption = "Credit"
            grdMainAccounts.RootTable.Columns(GridCol.colVoucherID).Caption = "Voucher ID"
            grdMainAccounts.RootTable.Columns(GridCol.colLocationID).Caption = "Location ID"
            grdMainAccounts.RootTable.Columns(GridCol.colSource).Caption = "Source"



            ' Hiding Columns .. 
            grdMainAccounts.RootTable.Columns(GridCol.colVoucherID).Visible = False
            grdMainAccounts.RootTable.Columns(GridCol.colLocationID).Visible = False
            'CR#300
            grdMainAccounts.RootTable.Columns(GridCol.colIsLightwaveV).Visible = False


            ' Totals Of Columns .. 
            grdMainAccounts.RootTable.Columns(GridCol.colVoucherCode).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Count
            grdMainAccounts.RootTable.Columns(GridCol.colDebitAmount).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            grdMainAccounts.RootTable.Columns(GridCol.colCreditAmount).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum


            ' Setting Editable Columns .. 
            grdMainAccounts.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
            grdMainAccounts.RootTable.Columns(GridCol.colCheckBoxValue).EditType = Janus.Windows.GridEX.EditType.CheckBox
            grdMainAccounts.RootTable.Columns(GridCol.colVoucherDate).EditType = Janus.Windows.GridEX.EditType.NoEdit
            grdMainAccounts.RootTable.Columns(GridCol.colVoucherCode).EditType = Janus.Windows.GridEX.EditType.NoEdit
            grdMainAccounts.RootTable.Columns(GridCol.colYearCode).EditType = Janus.Windows.GridEX.EditType.NoEdit
            grdMainAccounts.RootTable.Columns(GridCol.colLocationName).EditType = Janus.Windows.GridEX.EditType.NoEdit
            grdMainAccounts.RootTable.Columns(GridCol.colDebitAmount).EditType = Janus.Windows.GridEX.EditType.NoEdit
            grdMainAccounts.RootTable.Columns(GridCol.colCreditAmount).EditType = Janus.Windows.GridEX.EditType.NoEdit
            grdMainAccounts.RootTable.Columns(GridCol.colVoucherID).EditType = Janus.Windows.GridEX.EditType.NoEdit
            grdMainAccounts.RootTable.Columns(GridCol.colLocationID).EditType = Janus.Windows.GridEX.EditType.NoEdit
            grdMainAccounts.RootTable.Columns(GridCol.colSource).EditType = Janus.Windows.GridEX.EditType.NoEdit


            ' Formatting Columns .. 
            grdMainAccounts.RootTable.Columns(GridCol.colDebitAmount).FormatString = "n"
            grdMainAccounts.RootTable.Columns(GridCol.colCreditAmount).FormatString = "n"

            grdMainAccounts.RootTable.Columns(GridCol.colDebitAmount).TotalFormatString = "n"
            grdMainAccounts.RootTable.Columns(GridCol.colCreditAmount).TotalFormatString = "n"

            ' Auto Fit Columns .. 
            grdMainAccounts.AutoSizeColumns()

            grdMainAccounts.RootTable.Columns(GridCol.colDebitAmount).Width = 100
            grdMainAccounts.RootTable.Columns(GridCol.colCreditAmount).Width = 100

            grdMainAccounts.RootTable.Columns(GridCol.colVoucherDate).FormatString = "dd MMM yyyy"


        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub

    ' Apply Security .. 
    Public Sub ApplySecurity(ByVal Mode As Utility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

        Try
            btnUpdate.Enabled = False
            btnDelete.Enabled = False
            btnCancel.Enabled = False
            btnNew.Enabled = False



            If mobjControlList.Item("btnSave") Is Nothing Then
                btnSave.Enabled = False

            Else
                If _TempVouchers Then
                    btnSave.Enabled = False '
                Else
                    'CR#117
                    btnSave.Enabled = True
                End If


            End If


            ' Disable / Enable the Button that converts Grid data in Excel Sheet According to Login User rights ..
            If mobjControlList.Item("btnExport") Is Nothing Then
                Me.UiCtrlGridBar1.btnExport.Enabled = False

            End If


            ' Disable / Enable the Button that Prints Grid data According to Login User rights ..
            If mobjControlList.Item("btnPrint") Is Nothing Then
                Me.UiCtrlGridBar1.btnPrint.Enabled = False

            End If
        Catch ex As Exception
            Throw ex

        End Try
    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
    End Function


    ' Filling Combos .. 
    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos

        Try
            Dim strSQL As String = ""
            Dim ObjDataTable As DataTable
            Dim ObjDataRow As DataRow

            ' Binding Voucher Type .. 
            ' =========================================================================================
            ' =========================================================================================
            'CR# 13
            'strSQL = "SELECT voucher_type, voucher_type_id FROM tblGlDefVoucherType WHERE Voucher_type <> 'TV' Order by sort_order "
            If _TempVouchers = False Then
                strSQL = "SELECT voucher_type, voucher_type_id FROM tblGlDefVoucherType WHERE Voucher_type <> 'TV' Order by sort_order "
            Else
                strSQL = "SELECT voucher_type, voucher_type_id FROM tblGlDefVoucherType WHERE Voucher_Type IN ('CRV', 'CPV') AND Voucher_Type <> 'TV' ORDER BY sort_order, voucher_type"
            End If

            ObjDataTable = UtilityDAL.GetDataTable(strSQL)

            ObjDataRow = ObjDataTable.NewRow
            ObjDataRow.Item("voucher_type") = gstrComboZeroIndexString
            ObjDataRow.Item("voucher_type_id") = 0
            ObjDataTable.Rows.InsertAt(ObjDataRow, 0)


            cmbVoucherType.DataSource = ObjDataTable.Copy

            cmbVoucherType.DisplayMember = "voucher_type"
            cmbVoucherType.ValueMember = "voucher_type_id"
            ' =========================================================================================
            ' =========================================================================================


            ' Source Drop Down List .. 
            cmbSource.Items.Clear()


            'CR#5
            'cmbSource.Items.Add(gstrComboZeroIndexString)
            'cmbSource.Items.Add("Accounts")
            'cmbSource.SelectedIndex = 0

            Dim ObjDAL As New DAL.PostedVouchersDAL
            Dim DTConfigList As New DataTable

            DTConfigList = ObjDAL.GetTblshopConfigValues()

            If DTConfigList.Rows.Count > 0 Then

                ObjDataRow = DTConfigList.NewRow
                ObjDataRow.Item("config_value") = gstrComboZeroIndexString
                DTConfigList.Rows.InsertAt(ObjDataRow, 0)

                ObjDataRow = DTConfigList.NewRow
                ObjDataRow.Item("config_value") = "Accounts"
                DTConfigList.Rows.InsertAt(ObjDataRow, 1)

                cmbSource.DataSource = DTConfigList
                cmbSource.SelectedIndex = 0

                cmbSource.DisplayMember = "config_value"
                cmbSource.ValueMember = "config_value"

            Else

                cmbSource.Items.Add(gstrComboZeroIndexString)
                cmbSource.Items.Add("Accounts")
                cmbSource.SelectedIndex = 0

            End If

            'CR#300
            Dim DTLocation As DataTable
            DTLocation = New VoucherDAL().GetLocationOfLoggedInUser(gObjUserInfo.UserID)

            Me.cboCompany.ValueMember = "LocID"
            Me.cboCompany.DisplayMember = "Location"
            Me.cboCompany.DataSource = DTLocation
            If pbLocationID = 0 Then
                pbLocationID = gobjLocationInfo.CompanyID
            End If
            Me.cboCompany.SelectedValue = pbLocationID

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    ' Filling Model .. 
    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Dim intCountTran As Integer

        Try

            ObjModel = New Model.PostingVouchersModel
            ObjModel.TempVouchers = _TempVouchers

            ' ---------------------------------------------------------------------------------------------------
            ' ---------------------------------------------------------------------------------------------------
            Dim ObjModelTemp As Model.PostingVouchersModel
            Dim intRows As Integer = 0

            Dim dt As DataTable

            Me.grdMainAccounts.Update()
            dt = CType(Me.grdMainAccounts.DataSource, DataTable)

            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                dt.AcceptChanges()

                For Each r As DataRow In dt.Rows

                    ObjModelTemp = New Model.PostingVouchersModel

                    ObjModelTemp.Post = CBool(r.Item(GridCol.colCheckBoxValue).ToString) '  IIf(grdMainAccounts.GetRow(intRows).Cells(GridCol.colCheckBoxValue).Value = -1, 1, 0)
                    'CR#300
                    'ObjModelTemp.LocationID = r.Item(GridCol.colLocationID).ToString ' grdMainAccounts.GetRows(intRows).Cells(GridCol.colLocationID).Value
                    If Me.cboCompany.SelectedIndex > 0 Then
                        ObjModelTemp.LocationID = r.Item(GridCol.colLocationID).ToString ' grdMainAccounts.GetRows(intRows).Cells(GridCol.colLocationID).Value
                    Else
                        ObjModelTemp.LocationID = 0 ' grdMainAccounts.GetRows(intRows).Cells(GridCol.colLocationID).Value
                    End If

                    ObjModelTemp.VoucherID = r.Item(GridCol.colVoucherID).ToString '  grdMainAccounts.GetRows(intRows).Cells(GridCol.colVoucherID).Value

                    VouchersArray.Add(ObjModelTemp)

                Next

            Else


                For intRows = 0 To grdMainAccounts.RowCount - 2

                    ObjModelTemp = New Model.PostingVouchersModel

                    ObjModelTemp.Post = IIf(grdMainAccounts.GetRow(intRows).Cells(GridCol.colCheckBoxValue).Value = -1, 1, 0)
                    ObjModelTemp.LocationID = grdMainAccounts.GetRows(intRows).Cells(GridCol.colLocationID).Value
                    ObjModelTemp.VoucherID = grdMainAccounts.GetRows(intRows).Cells(GridCol.colVoucherID).Value

                    VouchersArray.Add(ObjModelTemp)

                Next

            End If
            'For intRows = 0 To grdMainAccounts.RowCount - 2

            '    ObjModelTemp = New Model.PostingVouchersModel

            '    ObjModelTemp.Post = IIf(grdMainAccounts.GetRow(intRows).Cells(GridCol.colCheckBoxValue).Value = -1, 1, 0)
            '    ObjModelTemp.LocationID = grdMainAccounts.GetRows(intRows).Cells(GridCol.colLocationID).Value
            '    ObjModelTemp.VoucherID = grdMainAccounts.GetRows(intRows).Cells(GridCol.colVoucherID).Value

            '    VouchersArray.Add(ObjModelTemp)

            'Next

            ObjModel.SELECTEDRECORD_ARRAYLIST = VouchersArray
            ' ---------------------------------------------------------------------------------------------------
            ' ---------------------------------------------------------------------------------------------------
            ObjModel.FinancialYearCode = gObjFinancialYearInfo.YearCode
            'CR#300
            'ObjModel.LocationID = gobjLocationInfo.CompanyID
            ObjModel.LocationID = Me.cboCompany.SelectedValue
            ' Voucher Type .. 
            If cmbVoucherType.SelectedIndex <> 0 Then ObjModel.VoucherType = cmbVoucherType.SelectedValue
            ' Voucher Month .. 
            If cmbVoucherMonth.SelectedIndex <> 0 Then ObjModel.VoucherMonth = cmbVoucherMonth.Text

            ' Voucher Source .. 
            If cmbSource.SelectedIndex <> 0 Then ObjModel.VoucherSource = cmbSource.Text

            ' Checking Voucher # Wise Search Is Requested Or Not .. 
            If optSearchVoucherWise.Checked = True Then
                ObjModel.VoucherNoWiseFlag = 1
                ObjModel.VoucherNoFrom = txtVoucherNoFrom.Text
                ObjModel.VoucherNoTO = txtVoucherNoTo.Text

            End If

            'CR#322
            ObjModel.ChequeNo = IIf(Me.txtChequeNo.Text.Trim.Length > 0, Me.txtChequeNo.Text.Trim, String.Empty)
            ObjModel.InvAmount = IIf(Me.txtInvAmount.Text.Trim.Length > 0, Me.txtInvAmount.Text.Trim, 0)
            ObjModel.Remarks = IIf(Me.txtRemarks.Text.Trim.Length > 0, Me.txtRemarks.Text.Trim, String.Empty)

            ' Checking User Has Selected Search Date Wise Check Box .. If Yes Then Add Parameters Accordingly .. 
            If optSearchDateWise.Checked = True Then
                ObjModel.VoucherDateWiseFlag = 1
                ObjModel.VoucherStartDate = Format(dtVoucherStartDate.Value.Date, "dd-MMM-yyyy").ToString
                ObjModel.VoucherEndDate = Format(dtVoucherEndDate.Value.Date, "dd-MMM-yyyy").ToString

            End If

            '''Filling Activity Log for Lod Viewer

            ObjModel.ActivityLog.UserID = gObjUserInfo.UserID
            ObjModel.ActivityLog.LogGroup = gObjUserInfo.GroupInfo.GroupName
            ObjModel.ActivityLog.LogRef = ObjModel.VoucherID
            ObjModel.ActivityLog.LogGroup = "Transactions"
            ObjModel.ActivityLog.ScreenTitle = "Voucher Posting"
            ObjModel.ActivityLog.RefType = "Voucher Number"
            '--------------------------------------


        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub

    Public Sub FillModelSave(Optional ByVal Condition As String = "")   'CR#60

        Try


            objModalVoucher = New GLVoucher

            Dim dtVouchers As DataTable = CType(Me.grdMainAccounts.DataSource, DataTable).GetChanges(DataRowState.Modified)

            'Dim dtVouchers As DataTable = CType(Me.grdMainAccounts.DataSource, DataTable)

            If Not dtVouchers Is Nothing Then

                With objModalVoucher

                    For Each modifiedRow As DataRow In dtVouchers.Rows


                        Application.DoEvents()

                        Dim mVListModal As New VoucherDetailItem

                        With mVListModal

                            'CR # 233
                            '.MPost = CBool(modifiedRow.Item(GridCol.colCheckBoxValue).ToString)
                            If modifiedRow.Item("checkboxvalue").ToString().ToUpper() = "FALSE" Then
                                .MPost = False
                            Else
                                .MPost = True
                            End If
                            .MLocation = CInt(modifiedRow.Item(GridCol.colLocationID).ToString)
                            .MVoucherID = CLng(modifiedRow.Item(GridCol.colVoucherID).ToString)

                            '   Farooq-H            CR#254
                            TotalDabit = TotalDabit + CDbl(modifiedRow.Item(GridCol.colDebitAmount))
                            If Voucher_Code = "" Then
                                Voucher_Code = modifiedRow.Item(GridCol.colVoucherCode).ToString
                            Else
                                Voucher_Code = Voucher_Code & " , " & modifiedRow.Item(GridCol.colVoucherCode).ToString
                            End If


                        End With
                        .ActivityLog.ScreenTitle = Me.Text
                        .ActivityLog.UserID = gObjUserInfo.UserID
                        .ActivityLog.FormAction = "Post"
                        .ActivityLog.LogGroup = "Transactions"
                        .ActivityLog.LogRef = CLng(modifiedRow.Item(GridCol.colVoucherID).ToString)
                        .ActivityLog.RefType = "Voucher Id"


                        .ListofVouchers.Add(mVListModal)

                    Next

                End With

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    ' Binding Grid Control .. With Extracted DB Data .. 
    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords

        Try
            FillModel()

            Dim ObjDAL As New DAL.PostedVouchersDAL
            grdMainAccounts.DataSource = ObjDAL.GetAll(ObjModel, PostUnPostStatus, BalancedUnBalancedStatus)
            grdMainAccounts.RetrieveStructure()

            ApplyGridSettings()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub

    ' Check Validation .. 
    Public Function IsValidate(Optional ByVal Mode As Utility.Utility.EnumDataMode = Utility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Dim intRows As Integer

        Try

            'CR # 233
            Dim dt As DataTable
            Dim count As Integer
            'CR#4

            If optPosted.Checked Then

                For intRows = 0 To grdMainAccounts.RowCount - 2
                    'CR#300
                    ' If grdMainAccounts.GetRow(intRows).Cells(GridCol.colSource).Value.ToString <> "Accounts" AndAlso CBool(Me.grdMainAccounts.GetRow(intRows).Cells(GridCol.colCheckBoxValue).Value) = False Then
                    'When posted/unposted check is OFF
                    If CBool(Me.grdMainAccounts.GetRow(intRows).Cells(GridCol.colCheckBoxValue).Value) = False Then
                        'For existing data/customers check if source is not account and new columns IsLightwaveVoucher is null or empty then its external voucher Or 
                        If IsDBNull(grdMainAccounts.GetRow(intRows).Cells(GridCol.colIsLightwaveV).Value.ToString()) Or grdMainAccounts.GetRow(intRows).Cells(GridCol.colIsLightwaveV).Value.ToString() = "" Then
                            If grdMainAccounts.GetRow(intRows).Cells(GridCol.colSource).Value.ToString <> "Accounts" Then
                                ShowErrorMessage("External Vouchers can't be un-posted from GL")
                                Return False
                                Exit Function
                            End If

                            'When data exist against IsLightwaveVoucher, new data
                        ElseIf CBool(grdMainAccounts.GetRow(intRows).Cells(GridCol.colIsLightwaveV).Value.ToString()) = False Then
                            ShowErrorMessage("External Vouchers can't be un-posted from GL")
                            Return False
                            Exit Function

                        End If

                    End If

                Next
                'CR # 233
                dt = CType(Me.grdMainAccounts.DataSource, DataTable)
                If Not dt Is Nothing Then
                    For Each dr As DataRow In dt.Rows
                        If dr.Item("checkboxvalue").ToString = "False" Then
                            count = count + 1
                        End If
                    Next
                    If count = 0 Then
                        ShowErrorMessage("Please uncheck voucher(s) you want to Un-post")
                        Return False
                    End If
                End If

            ElseIf optUnPosted.Checked Then

                'CR#215

                Dim dtVouchers As DataTable = CType(Me.grdMainAccounts.DataSource, DataTable).GetChanges(DataRowState.Modified)

                If Not dtVouchers Is Nothing Then

                    For Each modifiedRow As DataRow In dtVouchers.Rows

                        Application.DoEvents()


                        If (CLng(modifiedRow.Item(GridCol.colCreditAmount)) <> CLng(modifiedRow.Item(GridCol.colDebitAmount)) AndAlso CBool(modifiedRow.Item(GridCol.colCheckBoxValue))) Then
                            ShowErrorMessage("Unbalanced vouchers can't be posted")
                            Return False
                        End If

                    Next


                End If
                'CR # 233
                dt = CType(Me.grdMainAccounts.DataSource, DataTable)
                If Not dt Is Nothing Then
                    For Each dr As DataRow In dt.Rows
                        If dr.Item("checkboxvalue").ToString = "True" Then
                            count = count + 1
                        End If
                    Next
                    If count = 0 Then
                        ShowErrorMessage("Please check voucher(s) you want to post")
                        Return False
                    End If
                End If

            End If


            Return True


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try


    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
    End Sub

    ' Save Method .. 
    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save

        Try
            ' Filling Model .. 
            FillModelSave()
            If IsValidate() Then


                If New DAL.PostedVouchersDAL().Save(objModalVoucher) Then
                     
                    'Cr # 233
                    If Me.btnPost.Visible = True Then
                        'change by Farooq-H  CR# 254 
                        Try
                            mObjSMS.SendSMS(EnumSMSCodes.SMS_VSP_PO_01.ToString, "&VoucherType&=" & IIf(Me.cmbVoucherType.Text <> "---Select---", Me.cmbVoucherType.Text.ToString, "") & ";&VocuherCode&=" & Me.Voucher_Code.ToString & ";&ParamVAmount&=" & TotalDabit.ToString & " ")
                        Catch ex As Exception
                        End Try
                        ShowInformationMessage("Vouchers are Posted Successfully")
                    ElseIf Me.btnUnpost.Visible = True Then
                        'change by Farooq-H  CR# 254 
                        Try
                            mObjSMS.SendSMS(EnumSMSCodes.SMS_VSP_UP_01.ToString, "&VoucherType&=" & IIf(Me.cmbVoucherType.Text <> "---Select---", Me.cmbVoucherType.Text.ToString, "") & ";&VocuherCode&=" & Me.Voucher_Code.ToString & ";&ParamVAmount&=" & TotalDabit.ToString & " ")
                        Catch ex As Exception
                        End Try
                        ShowInformationMessage("Vouchers are Un-Posted Successfully")
                    End If

                    Me.GetAllRecords()
                    Return True
                    Exit Function

                End If

            End If

        Catch ex As Exception
            Throw ex

        End Try

    End Function

    ' Setting Button Images .. 
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

    ' Setting Other Voucher Here .. (This Check Box Is To Be Set Or UnSet According to Configuration .. )
    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting
        Try
            optOther.Visible = gblnShowOtherVoucher
        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As Utility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons
    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update
    End Function

#End Region

#Region "From Events nd General Functions .. "

    ' Fill Parameters Accordingly To Selected Radio Buttons .. 
    Sub FillGridRecords()

        Try

            Me.Cursor = Cursors.WaitCursor
            ' If Form Is New Mode Then Don't Call Loading .. 
            If blnLoad = True Then Exit Sub

            If optAll.Checked = False AndAlso optPosted.Checked = False AndAlso optUnPosted.Checked = False AndAlso optUnBalanced.Checked = False AndAlso optOther.Checked = False Then Exit Sub


            If optAll.Checked = True Then
                PostUnPostStatus = "1"
                BalancedUnBalancedStatus = "ALL"

            End If



            If optPosted.Checked = True Then
                PostUnPostStatus = "1"
                BalancedUnBalancedStatus = "OnlyPosted"

            End If


            If optUnPosted.Checked = True Then
                PostUnPostStatus = "0"
                BalancedUnBalancedStatus = "Balanced"

            End If


            If optUnBalanced.Checked = True Then
                PostUnPostStatus = "0"
                BalancedUnBalancedStatus = "UnBalanced"

            End If


            If optOther.Checked = True Then
                PostUnPostStatus = "0"
                BalancedUnBalancedStatus = "Other"

            End If
            Me.GetAllRecords()


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub



    ' All Radio Button Checked Event .. 
    Private Sub optAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optAll.CheckedChanged, optPosted.CheckedChanged, optUnPosted.CheckedChanged, optUnBalanced.CheckedChanged, optOther.CheckedChanged
        Try
            ' If Form Is New Mode Then Don't Call Loading .. 
            If blnLoad = True Then Exit Sub

            If blnFormLoaded = False Then Exit Sub

            'FillGridRecords()
            'If optAll.Checked = True Then
            '    PostUnPostStatus = "1"
            '    BalancedUnBalancedStatus = "ALL"
            '    Me.GetAllRecords()

            'End If

            

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub

    Private Sub frmGLPostingVoucher_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated

        If _TempVouchers = True Then
            lblHeading.Text = "Searching/ Posting Of Temp Vouchers"
        Else
            lblHeading.Text = "Searching/ Posting Of Vouchers"
        End If

        FillGridRecords()

    End Sub

    ' Short Cut Keys Handling .. 
    Private Sub frmGLPostingVoucher_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        Try
            If e.Control And e.KeyCode = Keys.S Then
                'CR#4
                If IsValidate() AndAlso Me.btnSave.Enabled = True Then

                    If ShowConfirmationMessage("Confirm Posting/Un-Posting", MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then

                        Me.btnSave.Focus()
                        Me.Save()

                    End If
                End If

            ElseIf e.Control And e.KeyCode = Keys.X Then
                If Me.btnExit.Enabled = True Then Me.Close()


            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    ' Load Event .. 
    Private Sub frmGLPostingVoucher_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            ' Getting all available controls list to the user on this screen; in a collection 
            mobjControlList = GetFormSecurityControls(Me.Name)

            blnLoad = True
            blnFormLoaded = True
            ' Filling Combos .. 
            FillCombos()
            cmbVoucherMonth.SelectedIndex = 0

            txtFinancialYear.Text = gObjFinancialYearInfo.YearCode & " - " & gobjLocationInfo.CompanyName

            ' Setting Dates .. 
            'dtVoucherStartDate.MinDate = gObjFinancialYearInfo.StartDate
            'dtVoucherStartDate.Value = gObjFinancialYearInfo.StartDate

            '' Ending Dates .. 
            'dtVoucherEndDate.MaxDate = gObjFinancialYearInfo.EndDate
            'dtVoucherEndDate.Value = gObjFinancialYearInfo.EndDate

            'CR # 152 Search Voucher option: should be optimize. time out expire error appear
            '.............................................................

            '//Abdul Jabbar relevant to CR#152 Fetching current year (as loged in)
            Dim ArrFYear() As String = gObjFinancialYearInfo.YearCode.Split("-")

            Dim CurrYear As Integer
            If Not (gObjFinancialYearInfo.StartDate.Month = 7) Then 'If Fyear is from 01 Jan to 31 Dec
                CurrYear = Convert.ToInt64(ArrFYear(0))
            Else 'If Fyear is from 01 Jul to 30 jun

                If (DateTime.Now.Month < 7) Then
                    CurrYear = Convert.ToInt64(ArrFYear(1))
                Else
                    CurrYear = Convert.ToInt64(ArrFYear(0))
                End If
            End If

            Me.optSearchDateWise.Checked = True
            Dim start As DateTime
            'start = New DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)

            'CR#
            'CR#194
            'startDate = New DateTime(CurrYear, DateTime.Now.Month, 1)
            Dim DefStartDate As DateTime
            Dim DefEndDate As DateTime
            DefEndDate = New Date(CurrYear, Date.Now.Month, Date.Now.Day)    '---end date will be current date
            DefStartDate = DefEndDate.AddMonths(-1) 'Date.Now.AddMonths(-1)   '--start date will be current date -30 days

            'if start date is less then Financial Year start date then set StartDate=FYearStart Date
            If DefStartDate < gObjFinancialYearInfo.StartDate Then
                DefStartDate = gObjFinancialYearInfo.StartDate
            End If

            dtVoucherStartDate.MinDate = gObjFinancialYearInfo.StartDate
            dtVoucherStartDate.Value = DefStartDate

            dtVoucherEndDate.MaxDate = gObjFinancialYearInfo.EndDate
            dtVoucherEndDate.Value = DefEndDate

            ''setting Min Limit      Asif Kamal
            'dtVoucherStartDate.MinDate = gObjFinancialYearInfo.StartDate
            ''dtVoucherStartDate.Value = start


            'Dim endDate As DateTime
            'endDate = start.AddMonths(1).AddDays(-1)
            '' Setting Max Limit     Asif Kamal
            'dtVoucherEndDate.MaxDate = gObjFinancialYearInfo.EndDate
            'dtVoucherEndDate.Value = endDate
            '.............................................................

            ' Assing Images to Buttons ..
            Me.SetButtonImages()

            blnLoad = False
            'CR#238
            Me.optUnPosted.Checked = True
            FillGridRecords()


            SetConfigurationBaseSetting()

            ' Applying Security Setting ..
            Call ApplySecurity(EnumDataMode.[New])


            If _TempVouchers = True Then
                lblHeading.Text = "Searching/ Posting Of Temp Vouchers"
            Else
                lblHeading.Text = "Searching/ Posting Of Vouchers"
            End If

            'Cr # 233
            '..................
            Me.btnUnpost.Visible = False
            Me.btnPost.Visible = False
            Me.btnSave.Enabled = False
            '..................

            'CR # 248
            If Not mobjControlList.Item("Post") Is Nothing Then
                Me.btnPost.Visible = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub

    ' Key Press Event Of Voucher # From .. 
    Private Sub txtVoucherNoFrom_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtVoucherNoFrom.KeyPress
        Try
            If IsNumeric(e.KeyChar) Or Asc(e.KeyChar).ToString = 8 Or Asc(e.KeyChar).ToString = 46 Then
            Else
                e.Handled = True

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
        
    End Sub

    ' Lost Focus Event Of Voucher No From .. 
    Private Sub txtVoucherNoFrom_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtVoucherNoFrom.LostFocus

        Try

            ' If Form Is New Mode Then Don't Call Loading .. 
            If blnLoad = True Then Exit Sub

            txtVoucherNoFrom.Text = txtVoucherNoFrom.Text.PadLeft(6, "0")
            If txtVoucherNoTo.Text.Trim = "" Then
                txtVoucherNoTo.Text = txtVoucherNoFrom.Text

            End If
            'FillGridRecords()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub

    ' Key Press Event Of Voucher # To .. 
    Private Sub txtVoucherNoTo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtVoucherNoTo.KeyPress
        Try
            If IsNumeric(e.KeyChar) Or Asc(e.KeyChar).ToString = 8 Or Asc(e.KeyChar).ToString = 46 Then
            Else
                e.Handled = True

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub

    ' Lost Focus Event Of Voucher # To ... 
    Private Sub txtVoucherNoTo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtVoucherNoTo.LostFocus
        Try

            ' If Form Is New Mode Then Don't Call Loading .. 
            If blnLoad = True Then Exit Sub

            txtVoucherNoTo.Text = txtVoucherNoTo.Text.PadLeft(6, "0")
            'FillGridRecords()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub


    ' Checked Changed Event Of Search Voucher Wise .. (Radio Button .. )
    Private Sub optSearchVoucherWise_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optSearchVoucherWise.CheckedChanged

        Try

            ' If Form Is New Mode Then Don't Call Loading .. 
            If blnLoad = True Then Exit Sub

            If optSearchVoucherWise.Checked = True Then
                dtVoucherStartDate.Enabled = False
                dtVoucherEndDate.Enabled = False

                txtVoucherNoFrom.Enabled = True
                txtVoucherNoTo.Enabled = True
                txtVoucherNoFrom.Focus()

                'FillGridRecords()

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub


    ' Checked Changed Event Of Search DateWise .. (Radio Button .. )
    Private Sub optSearchDateWise_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optSearchDateWise.CheckedChanged

        Try

            ' If Form Is New Mode Then Don't Call Loading .. 
            If blnLoad = True Then Exit Sub

            If optSearchDateWise.Checked = True Then

                cmbVoucherMonth.SelectedIndex = 0

                dtVoucherStartDate.Enabled = True
                dtVoucherEndDate.Enabled = True
                dtVoucherStartDate.Focus()

                txtVoucherNoFrom.Text = ""
                txtVoucherNoTo.Text = ""

                txtVoucherNoFrom.Enabled = False
                txtVoucherNoTo.Enabled = False

                'FillGridRecords()

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub


    ' Checked Changed Event Of Search All .. (Radio Button .. )
    Private Sub optSearchAll_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optSearchAll.CheckedChanged

        Try

            ' If Form Is New Mode Then Don't Call Loading .. 
            If blnLoad = True Then Exit Sub

            If optSearchAll.Checked = True Then
                dtVoucherStartDate.Enabled = False
                dtVoucherEndDate.Enabled = False

                txtVoucherNoFrom.Enabled = False
                txtVoucherNoTo.Enabled = False

                txtVoucherNoFrom.Text = ""
                txtVoucherNoTo.Text = ""

                'FillGridRecords()

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub


    ' Click Event Of Save Button .. 
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Try

            'CR#4
            If IsValidate() Then

                If ShowConfirmationMessage("Confirm Posting/Un-Posting", MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then

                    Save()

                End If

            End If

        Catch ex As Exception

            ShowErrorMessage(ex.Message)

        End Try

    End Sub


    ' Double Click Event Of Main Grid Control .. 
    Private Sub grdMainAccounts_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdMainAccounts.DoubleClick

        Try

            If grdMainAccounts.RowCount = 0 Or grdMainAccounts.GetRow().RowType = Janus.Windows.GridEX.RowType.TotalRow Or grdMainAccounts.GetRow().RowType = Janus.Windows.GridEX.RowType.GroupHeader Or grdMainAccounts.GetRow().RowType = Janus.Windows.GridEX.RowType.GroupFooter Then
                ShowValidationMessage("No vouchers are found in grid to be load")
                Exit Sub

            End If

            _blnCallFromSearchPost = True
            _gstrVoucherIDs = grdMainAccounts.GetRow().Cells(GridCol.colVoucherID).Text
            'CR#318
            gobjLocationInfo.CompanyID = Me.cboCompany.SelectedValue
            gobjLocationInfo.CompanyID = grdMainAccounts.GetRow().Cells(GridCol.colLocationID).Text

            If _TempVouchers = True Then
                CType(Me.MdiParent, MDIParent1).TemporarayVoucherToolStripMenuItem_Click(Nothing, Nothing)
            Else
                CType(Me.MdiParent, MDIParent1).MnuCustomerCardPrinting_Click(Nothing, Nothing)

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default

        End Try
    End Sub


    ' Load Selected Vouchers Button Click Event . 
    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click

        Try

            'code commented against CR # 356
            'If grdMainAccounts.RowCount <= 0 Then
            'CR # 356
            If grdMainAccounts.RowCount <= 1 Then
                ShowValidationMessage("No vouchers are found in grid to be load")
                Exit Sub

            End If

            Dim intRow As Integer = 0
            Dim strVouchers As String = ""

            For intRow = 0 To grdMainAccounts.RowCount - 2
                strVouchers = strVouchers & grdMainAccounts.GetRow(intRow).Cells(GridCol.colVoucherID).Text & ","
                '                strVouchers = grdMainAccounts.GetRow(intRow).Cells(GridCol.colVoucherID).Text & "," & strVouchers

            Next

            strVouchers = Microsoft.VisualBasic.Left(strVouchers, Len(strVouchers) - 1)
            'CR#300 Setting Company to selected one instead of loggedin company
            'CR#318
            gobjLocationInfo.CompanyID = Me.cboCompany.SelectedValue
            gobjLocationInfo.CompanyID = grdMainAccounts.GetRow().Cells(GridCol.colLocationID).Text

            _blnCallFromSearchPost = True
            _gstrVoucherIDs = strVouchers

            If _TempVouchers = True Then
                CType(Me.MdiParent, MDIParent1).TemporarayVoucherToolStripMenuItem_Click(Nothing, Nothing)
            Else
                CType(Me.MdiParent, MDIParent1).MnuCustomerCardPrinting_Click(Nothing, Nothing)

            End If


            'For Each exfrm As Form In Application.OpenForms
            '    If exfrm.Name = frmGLVoucher.Name Then
            '        Dim ObjForm As frmGLVoucher = CType(exfrm, frmGLVoucher)
            '        ObjForm.IsCallFromSearchPostScreen = True
            '        ObjForm.StrVoucherIDs = strVouchers
            '        exfrm.BringToFront()
            '        Exit Sub
            '    End If
            'Next

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default

        End Try


    End Sub


    ' Change Event Of Drop Down List .. 
    Private Sub cmbSource_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSource.SelectedIndexChanged, cmbVoucherMonth.SelectedIndexChanged, cmbVoucherType.SelectedIndexChanged, dtVoucherEndDate.ValueChanged, dtVoucherStartDate.ValueChanged

        ' If Form Is New Mode Then Don't Call Loading .. 
        If blnLoad = True Then Exit Sub

        'FillGridRecords()

    End Sub


    ' Exit Button .. (Click Button .. )
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()

    End Sub

#End Region

   
    Private Sub ChkSelectAll_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkSelectAll.CheckedChanged
        Try

            Me.Cursor = Cursors.WaitCursor
            If Me.ChkSelectAll.Checked Then
                For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdMainAccounts.GetRows
                    Row.BeginEdit()
                    Row.Cells(GridCol.colCheckBoxValue).Value = True
                    Row.EndEdit()
                Next
            Else
                For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdMainAccounts.GetRows
                    Row.BeginEdit()
                    Row.Cells(GridCol.colCheckBoxValue).Value = False
                    Row.EndEdit()
                Next
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    'CR # 174 Voucher search post screen, modification requires on filtering records.
    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Me.FillGridRecords()

            'CR # 233
            If optPosted.Checked Then
                If Not mobjControlList.Item("UnPost") Is Nothing Then
                    Me.btnUnpost.Visible = True
                End If
                Me.btnPost.Visible = False
            ElseIf optUnPosted.Checked Then
                If Not mobjControlList.Item("Post") Is Nothing Then
                    Me.btnPost.Visible = True
                End If
                Me.btnUnpost.Visible = False
            Else
                Me.btnPost.Visible = False
                Me.btnUnpost.Visible = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub btnFirst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFirst.Click, btnPrevious.Click, btnNext.Click, btnLast.Click

        Try

            Dim btn As Button = CType(sender, Button)

            ''If Move First is clicked, then navigate to first record of the grid
            If btn.Name = Me.btnFirst.Name Then
                'Me.grdVoucher.Row = 0
                Me.grdMainAccounts.MoveFirst()

                ''If Move Previous is clicked, then navigate to Previous record of the grid
            ElseIf btn.Name = Me.btnPrevious.Name Then
                'If Me.grdVoucher.Row > 0 Then Me.grdVoucher.Row = (Me.grdVoucher.Row - 1)
                Me.grdMainAccounts.MovePrevious()

                ''If Move Next is clicked, then navigate to next record of the grid
            ElseIf btn.Name = Me.btnNext.Name Then
                'If Me.grdVoucher.Row >= 0 Then Me.grdVoucher.Row = (Me.grdVoucher.Row + 1)
                Me.grdMainAccounts.MoveNext()


                ''If Move Last is clicked, then navigate to Last record of the grid
            ElseIf btn.Name = Me.btnLast.Name Then
                'Me.grdVoucher.Row = (Me.grdVoucher.RowCount - 1)
                Me.grdMainAccounts.MoveLast()

            End If



        Catch ex As Exception

        End Try

    End Sub

    Private Sub grdMainAccounts_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdMainAccounts.SelectionChanged
        Try

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub

    'CR # 233
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        Try
            grdMainAccounts.UpdateData()
            If Me.IsValidate(EnumDataMode.ReadOnly, "Posted Vouchers") Then
                Dim dt As DataTable = CType(Me.grdMainAccounts.DataSource, DataTable).GetChanges(DataRowState.Modified)
                If Not dt Is Nothing Then
                    If ShowConfirmationMessage("Confirm Posting", MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                        Me.Save()
                    End If
                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    'Cr # 233
    Private Sub btnUnpost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUnpost.Click
        Try
            Me.grdMainAccounts.UpdateData()
            If Me.IsValidate() Then
                Dim dt As DataTable = CType(Me.grdMainAccounts.DataSource, DataTable).GetChanges(DataRowState.Modified)
                If Not dt Is Nothing Then
                    If ShowConfirmationMessage("Confirm Un-Posting", MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                        Me.Save()
                    End If
                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try

            Me.optAll.Checked = True
            Me.txtInvAmount.Text = String.Empty
            Me.txtRemarks.Text = String.Empty
            Me.txtChequeNo.Text = String.Empty

        Catch ex As Exception

        End Try
    End Sub
End Class