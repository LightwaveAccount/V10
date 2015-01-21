Imports DAL
Imports Model
Imports System.Collections.Specialized
Imports System.Data
Imports Utility.Utility
Imports Microsoft.VisualBasic

Public Class frmRptAgeingLedger
    Implements IReportsInterface, IGeneral


    Public pbFormType As String '= "Ageing Receivable (Ledger Based)" '"Ageing Payable (Ledger Based)"
    ''This collection will hold the controls' names, upon which the logged in user has rights
    Private mobjControlList As NameValueCollection

    Private Sub btnGenerateButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click

        Try


            ''Getting all available controls list to the user on this screen; in a collection 
            mobjControlList = GetFormSecurityControls(Me.Name)
            ' Implemented Interface Method .. 
            ' Used To Add Report Parameters .. (Also Report Name Is Given In This Function .. )
            Call FunAddReportPramaters()


            ' Create A Object Of Report Viewer .. And Calls His Show Method, To Show The Report .. 
            ' ------------------------------------------------------------------------------------
            Dim rptViewer As New rptViewer
            rptViewer.Text = Me.Text
            rptViewer.Show()
            ' ------------------------------------------------------------------------------------

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub

#Region "Interface Methods .. "
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(ByVal Mode As Utility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
        Try


            If Mode.ToString = EnumDataMode.Disabled.ToString Then

                btnNew.Enabled = False ': btnNew.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnSave.Enabled = False ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System           
                btnUpdate.Enabled = False ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnDelete.Enabled = False ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnCancel.Enabled = False ': btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue

                SetNavigationButtons(EnumDataMode.[New])
                ''Me.grdAllRecords.Enabled = True

            ElseIf Mode.ToString = EnumDataMode.[New].ToString Then

                btnNew.Enabled = False ': btnNew.VisualStyle = C1.Win.C1Input.VisualStyle.System

                If mobjControlList.Item("btnSave") Is Nothing Then
                    btnSave.Enabled = False ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System
                Else
                    btnSave.Enabled = True ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                End If

                btnUpdate.Enabled = False ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnDelete.Enabled = False ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnCancel.Enabled = True ': btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue

                SetNavigationButtons(Mode)

                ''Me.grdAllRecords.Enabled = False

            ElseIf Mode.ToString = EnumDataMode.Edit.ToString Then

                btnNew.Enabled = True ': btnNew.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                btnSave.Enabled = False ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System

                If mobjControlList.Item("btnUpdate") Is Nothing Then
                    btnUpdate.Enabled = False ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.System
                Else
                    btnUpdate.Enabled = True ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                End If

                If mobjControlList.Item("btnDelete") Is Nothing Then
                    btnDelete.Enabled = False ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.System
                Else
                    btnDelete.Enabled = True ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                End If
                btnCancel.Enabled = False ': btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.System

                SetNavigationButtons(Mode)

                ''Me.grdAllRecords.Enabled = True

                ''Me.grdAllRecords.Focus()

            ElseIf Mode.ToString = EnumDataMode.ReadOnly.ToString Then

                btnNew.Enabled = True ': btnNew.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                btnSave.Enabled = False ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System


                btnUpdate.Enabled = False ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnDelete.Enabled = False ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnCancel.Enabled = False ': btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.System

                SetNavigationButtons(Mode)

                ''Me.grdAllRecords.Enabled = True

                ''Me.grdAllRecords.Focus()

            End If


            '' Disabl/Enable the Button that converts Grid data in Excel Sheet According to Login User rights
            ''If mobjControlList.Item("btnExport") Is Nothing Then
            ''Me.UiCtrlGridBar1.btnExport.Enabled = False
            ''End If


            '' Disabl/Enable the Button that Prints Grid data According to Login User rights
            ''If mobjControlList.Item("btnPrint") Is Nothing Then
            ''Me.UiCtrlGridBar1.btnPrint.Enabled = False
            ''End If


        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Dim ObjDataTable As DataTable
        Dim ObjDataRow As DataRow

        Try

            ' Binding Company .. 
            ' =========================================================================================
            ' =========================================================================================
            Dim ObjDalCompany As New CompanyDAL
            ObjDataTable = ObjDalCompany.GetAll(gObjUserInfo.UserID)


            ObjDataRow = ObjDataTable.NewRow
            ObjDataRow.Item("Company Name") = gstrComboZeroIndexString
            ObjDataRow.Item("Company ID") = 0
            ObjDataTable.Rows.InsertAt(ObjDataRow, 0)


            cmbCompany.DataSource = ObjDataTable.Copy


            cmbCompany.DisplayMember = "Company Name"
            cmbCompany.ValueMember = "Company ID"

            ObjDalCompany = Nothing

            If Me.cmbCompany.Items.Count > 1 Then
                Me.cmbCompany.SelectedValue = gobjLocationInfo.CompanyID

            End If
            ' =========================================================================================
            ' ===================

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
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

            Me.btnPrint.ImageList = gobjMyImageListForOperationBar
            Me.btnPrint.ImageKey = "Print"

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting
        Try
            grpAgeing.Visible = gblnShowOtherVoucher
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As Utility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons
        Try
            If Mode = EnumDataMode.[New] Then
                ''if New Mode then Set Disable all Navigation Buttons
                btnFirst.Enabled = False ': btnFirst.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnPrevious.Enabled = False ': btnPrevious.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnNext.Enabled = False ': btnNext.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnLast.Enabled = False ': btnLast.VisualStyle = C1.Win.C1Input.VisualStyle.System

            ElseIf Mode = EnumDataMode.Edit Then
                ''if New Mode then Set Enable all Navigation Buttons
                btnFirst.Enabled = True ': btnFirst.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                btnPrevious.Enabled = True ': btnPrevious.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                btnNext.Enabled = True ': btnNext.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                btnLast.Enabled = True ': btnLast.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update

    End Function
#End Region

#Region "Report Interface Metholds .. "

    Public Function FunAddReportCriteria() As String Implements IReportsInterface.FunAddReportCriteria

        'Dim strSQL As String
        'Dim value1, value2 As Int16

        'If optVoucherDate.Checked = True Then
        '    value1 = 1
        '    value2 = 1

        'ElseIf optVoucherDueDate.Checked = True Then
        '    value1 = 0
        '    value2 = 0


        'ElseIf optAll.Checked = True Then
        '    value1 = 1
        '    value2 = 0



        'End If


        '' Building SQL ..

        'strSQL = " Alter View vwDailyActivityReport as " _
        '       & " SELECT TOP 100 PERCENT tblGlVoucher.voucher_code,tblGlDefVoucherType.voucher_type, tblGlVoucherDetail.debit_amount, " _
        '       & " tblGlVoucherDetail.credit_amount, tblGlVoucher.voucher_month, tblGlDefFinancialYear.year_code,tblGlVoucher.voucher_date, " _
        '       & " tblGlVoucherDetail.comments AS VDescription, tblGlCOAMainSubSubDetail.detail_title, tblGlCOAMainSubSubDetail.detail_code, " _
        '       & " tblGlVoucherDetail.coa_detail_id , dbo.tblGlVoucher.post AS Status , dbo.tblGlDefLocation.location_code,   dbo.tblGlDefLocation.location_name " _
        '       & " FROM tblGlCOAMainSubSub INNER JOIN " _
        '       & " tblGlCOAMainSubSubDetail ON tblGlCOAMainSubSub.main_sub_sub_id = tblGlCOAMainSubSubDetail.main_sub_sub_id INNER JOIN " _
        '       & " tblGlCOAMain INNER JOIN " _
        '       & " tblGlCOAMainSub ON tblGlCOAMain.coa_main_id = tblGlCOAMainSub.coa_main_id ON " _
        '       & " tblGlCOAMainSubSub.main_sub_id = tblGlCOAMainSub.main_sub_id INNER JOIN " _
        '       & " tblGlVoucher INNER JOIN " _
        '       & " tblGlVoucherDetail ON tblGlVoucher.voucher_id = tblGlVoucherDetail.voucher_id AND " _
        '       & " tblGlVoucher.location_id = tblGlVoucherDetail.location_id INNER JOIN " _
        '       & " tblGlDefFinancialYear ON tblGlVoucher.finiancial_year_id = tblGlDefFinancialYear.financial_year_id INNER JOIN " _
        '       & " tblGlDefVoucherType ON tblGlVoucher.voucher_type_id = tblGlDefVoucherType.voucher_type_id ON " _
        '       & " tblGlCOAMainSubSubDetail.coa_detail_id =tblGlVoucherDetail.coa_detail_id INNER JOIN                      dbo.tblGlDefLocation ON dbo.tblGlVoucher.location_id = dbo.tblGlDefLocation.location_id " _
        '       & " Where (tblGlVoucher.voucher_date BETWEEN '" & Format(dtFromDate.Value.Date, "yyyy-MM-dd") & "' AND '" & Format(dtToDate.Value.Date, "yyyy-MM-dd") & "') And " _
        '       & " ((tblGlVoucher.post= " & value1 & ") or (tblGlVoucher.post = " & value2 & " ))  ORDER BY tblGlVoucher.voucher_code "


        'UtilityDAL.ExecuteQuery(strSQL)


        'Dim ObjDAL As New DAL.ActivityReportDAL
        'If ObjDAL.InsertDataForReport() Then
        'Else
        'End If

        'Return ""


    End Function

    Public Sub FunAddReportPramaters() Implements IReportsInterface.FunAddReportPramaters

        Try

            ' Calling A Function Which Will Created View And Insert Data In Attached Table Of Grid ..
            FunAddReportCriteria()



            Dim objHashTableParamter As New Hashtable

            ' Giving Report Name .. 
            If Me.pbFormType = "Ageing Payable (Ledger Based)" Then

                objHashTableParamter.Add("ReportPath", "\rptAgeingPayableLedgerBase.rpt")
            Else

                objHashTableParamter.Add("ReportPath", "\rptAgeingReceivableLedgerBase.rpt")
            End If

            objHashTableParamter.Add("@Date", Format(Me.dtFromDate.Value.Date, pbDateFormat))


            If optVoucherDate.Checked = True Then

                ''qury for voucher date
                objHashTableParamter.Add("@Voucher_Date", "1")
                objHashTableParamter.Add("@Due_Date", "0")
                objHashTableParamter.Add("Date_Criteria", "Voucher Date")
            Else

                'query for due date
                objHashTableParamter.Add("@Voucher_Date", "0")
                objHashTableParamter.Add("@Due_Date", "1")
                objHashTableParamter.Add("Date_Criteria", "Due Date")
            End If

            'CR#107
            If optVendorName.Checked = True Then
                objHashTableParamter.Add("@SortedBy", "0")
            Else
                objHashTableParamter.Add("@SortedBy", "1")
            End If
            '------

            'CR#108
            objHashTableParamter.Add("@Slot1From", Convert.ToInt32(txtSlot1From.Text))
            objHashTableParamter.Add("@Slot1To", Convert.ToInt32(txtSlot1To.Text))
            objHashTableParamter.Add("@Slot2From", Convert.ToInt32(txtSlot2From.Text))
            objHashTableParamter.Add("@Slot2To", Convert.ToInt32(txtSlot2To.Text))
            objHashTableParamter.Add("@Slot3From", Convert.ToInt32(txtSlot3.Text))
            '------

            Dim ObjCompanyData As DataTable
            ObjCompanyData = UtilityDAL.setCompanyInfo(gobjLocationInfo.CompanyID)


            ' Passing Parameters .. (Report Parameters .. )
            objHashTableParamter.Add("mcompanyname", ObjCompanyData.Rows(0).Item("CompanyName"))
            objHashTableParamter.Add("address", ObjCompanyData.Rows(0).Item("CompanyAddress"))
            objHashTableParamter.Add("Ageing_Date", Format(dtFromDate.Value.Date, "dd/MMM/yyyy"))
            objHashTableParamter.Add("mReportTitle", Me.pbFormType.ToString)

            Dim Other_Voucher As String

            If optStandard.Checked = True Then

                Other_Voucher = 1
            ElseIf optOther.Checked = True Then

                Other_Voucher = 0
            ElseIf optBoth.Checked = True Then

                Other_Voucher = 99
            End If

            objHashTableParamter.Add("@Other_Voucher", Other_Voucher)

            ' Passing Parameters .. (Report Parameters .. )
            'If Me.cmbCompany.SelectedIndex > 0 Then
            objHashTableParamter.Add("@Location_Id", Me.cmbCompany.SelectedValue)
            '

            If chkSuppressZero.Checked = True Then

                objHashTableParamter.Add("SuppressZero", True)
            Else

                objHashTableParamter.Add("SuppressZero", False)
            End If


            'TODO: (R@! Shahid) Pending sorting of crystal report
            ''======================= Start of the portion ============
            'If Me.pbFormType = "Ageing Payable (Ledger Based)" Then

            '    If rdoBtnVendorName.Value = True Then

            '        frmCrpt.crptReport.SortFields(0) = "+{SP_Rpt_AgedPayableLedger.Vendor_Name}"
            '    Else

            '        frmCrpt.crptReport.SortFields(0) = "+{SP_Rpt_AgedPayableLedger.GL_Balance}"
            '    End If
            'Else

            '    If rdoBtnVendorName.Value = True Then

            '        frmCrpt.crptReport.SortFields(0) = "+{SP_Rpt_AgedReceivableLedger.customer_name}"
            '    Else

            '        frmCrpt.crptReport.SortFields(0) = "+{SP_Rpt_AgedReceivableLedger.GL_Balance}"
            '    End If
            'End If
            ''======================= End of the portion ============

            If mobjControlList.Item("btnPrint") Is Nothing Then

                objHashTableParamter.Add("PrintRights", "False")
            Else
                objHashTableParamter.Add("PrintRights", "True")
            End If

            If mobjControlList.Item("btnExport") Is Nothing Then
                objHashTableParamter.Add("ExportRights", "False")
            Else
                objHashTableParamter.Add("ExportRights", "True")
            End If

            ' =======================================================
            ' =======================================================


            gObjMyAppHashTable.Add(EnumHashTableKeyConstants.SetReportParametersList, objHashTableParamter)

        Catch ex As Exception
            Throw ex

        End Try

    End Sub

#End Region

    Private Sub frmRptAgeingLedger_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        'TODO: (R@! Shahid) Un comment to hide/unhide other voucher option
        'grpAgeing.Visible = IIf(DecryptWithCSP(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Other_Voucher"), "true") = "1", True, False)

    End Sub

    Private Sub frmRptAgeingLedger_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try

            If e.Control And e.KeyCode = Keys.X Then
                If Me.btnExit.Enabled = True Then Me.Close()
            ElseIf e.Control And e.KeyCode = Keys.P Then
                If Me.btnPrint.Enabled = True Then btnGenerateButton_Click(Nothing, Nothing)
            End If


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ' Load Event Of Form .. 
    Private Sub frmDailyActivity_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try


            Me.Text = Me.pbFormType
            Me.Label5.Text = Me.pbFormType
            'lblFormName.Caption = Me.pbFormType
            ''Getting all available controls list to the user on this screen; in a collection 
            mobjControlList = GetFormSecurityControls(Me.Name)

            Me.ApplySecurity(EnumDataMode.Disabled)
            Me.SetButtonImages()

            If Me.pbFormType = "Ageing Payable (Ledger Based)" Then

                optVendorName.Text = "Vendor"
            Else

                optVendorName.Text = "Customer"
            End If


            dtFromDate.Value = Now
            FillCombos()
            SetConfigurationBaseSetting()


        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub txtSlot1From_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSlot1From.KeyPress, txtSlot3.KeyPress, txtSlot2To.KeyPress, txtSlot2From.KeyPress, txtSlot1To.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            Dim iNum As String = "0123456789"
            If InStr(1, iNum, e.KeyChar) = 0 Then e.Handled = True
        End If
    End Sub

    Private Sub txtSlot1From_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSlot1From.LostFocus, txtSlot3.LostFocus, txtSlot2To.LostFocus, txtSlot2From.LostFocus, txtSlot1To.LostFocus
        Dim txt As Windows.Forms.TextBox = CType(sender, Windows.Forms.TextBox)

        If txt.Name = "txtSlot1To" Then
            txtSlot2From.Text = Convert.ToInt16(txtSlot1To.Text) + 1
        ElseIf txt.Name = "txtSlot2To" Then
            txtSlot3.Text = Convert.ToInt16(txtSlot2To.Text) + 1
        End If
    End Sub

    Private Sub txtSlot1From_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtSlot1From.Validating, txtSlot3.Validating, txtSlot2To.Validating, txtSlot2From.Validating, txtSlot1To.Validating
        Dim txt As Windows.Forms.TextBox = CType(sender, Windows.Forms.TextBox)

        If txt.Name = "txtSlot1To" Then
            If Convert.ToInt32(txtSlot1To.Text) < Convert.ToInt32(txtSlot1From.Text) Then
                ShowErrorMessage("Slot 1 To value must be greater than Slot 1 From Value")
                txtSlot1To.Text = Convert.ToInt32(txtSlot1From.Text) + 1
            End If
        ElseIf txt.Name = "txtSlot2From" Then
            If Convert.ToInt32(txtSlot2From.Text) < Convert.ToInt32(txtSlot1To.Text) Then
                ShowErrorMessage("Slot 2 From value must be greater than Slot 1 To Value")
                txtSlot2From.Text = Convert.ToInt16(txtSlot1To.Text) + 1
            End If
        ElseIf txt.Name = "txtSlot2To" Then
            If Convert.ToInt32(txtSlot2To.Text) < Convert.ToInt32(txtSlot2From.Text) Then
                ShowErrorMessage("Slot 2 To value must be greater than Slot 2 From Value")
                txtSlot2To.Text = Convert.ToInt16(txtSlot2From.Text) + 1
            End If
        ElseIf txt.Name = "txtSlot3" Then
            If Convert.ToInt32(txtSlot3.Text) < Convert.ToInt32(txtSlot2To.Text) Then
                ShowErrorMessage("Slot 3 value must be greater than Slot 2 To Value")
                txtSlot3.Text = Convert.ToInt16(txtSlot2To.Text) + 1
            End If
        End If

    End Sub
End Class