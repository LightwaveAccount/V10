''/////////////////////////////////////////////////////////////////////////////////////////
''//                      Candela New
''/////////////////////////////////////////////////////////////////////////////////////////
''//-------------------------------------------------------------------------------------
''// File Name       : GL Voucher .. 
''// Programmer	     : Tariq Majeed
''// Creation Date	 : 17-July-2009
''// Description     : 
''// Function List   : 								                                    
''//											                                            
''//-------------------------------------------------------------------------------------
''// Date Modified     CR#      Modified by         Brief Description			                
''//------------------------------------------------------------------------------------
''//19 May,2010        51         Abdul Jabbar      function of 'Include Other voucher ' checkbox on Cash Flow Statement report is reverse.    
''/////////////////////////////////////////////////////////////////////////////////////////

Imports DAL
Imports Model
Imports System.Collections.Specialized
Imports System.Data
Imports Utility.Utility
Imports Microsoft.VisualBasic

Public Class frmRptGLCashFlow
    Implements IGeneral, IReportsInterface

    Dim dblCashBankOpening As Double
    ''This collection will hold the controls' names, upon which the logged in user has rights
    Private mobjControlList As NameValueCollection

    Private Sub frmRptGLCashFlow_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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


    Private Sub frmGLVoucher_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ''Getting all available controls list to the user on this screen; in a collection 
            mobjControlList = GetFormSecurityControls(Me.Name)
            Me.ApplySecurity(EnumDataMode.Disabled)
            Me.SetButtonImages()
            dtFromDate.Value = DateAdd(DateInterval.Day, -7, Now)
            dtToDate.Value = Now

            ' Fill Combos Of Financial Year, Company nd Voucher Type ..   
            FillCombos()

            SetConfigurationBaseSetting()
        Catch ex As Exception

        End Try


    End Sub

#Region "Report Interface Metholds .. "

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

        Dim strSQL As String = ""
        Dim ObjDataTable As DataTable
        Dim ObjDataRow As DataRow


        ' Binding Financial Year Combo .. 
        ' =========================================================================================
        ' =========================================================================================
        Dim ObjDalFinancialYear As New FiniancialYearDefDAL
        ObjDataTable = ObjDalFinancialYear.GetAll()


        ObjDataRow = ObjDataTable.NewRow
        ObjDataRow.Item("FYear Code") = gstrComboZeroIndexString
        ObjDataRow.Item("FYear ID") = 0
        ObjDataTable.Rows.InsertAt(ObjDataRow, 0)


        cmbFinancialYear.DataSource = ObjDataTable.Copy


        cmbFinancialYear.DisplayMember = "FYear Code"
        cmbFinancialYear.ValueMember = "FYear ID"

        ObjDalFinancialYear = Nothing

        If Me.cmbFinancialYear.Items.Count > 1 Then
            Me.cmbFinancialYear.SelectedValue = gObjFinancialYearInfo.FYearID

        End If
        ' =========================================================================================
        ' =========================================================================================



        ' Binding Voucher Type .. 
        ' =========================================================================================
        ' =========================================================================================
        strSQL = " SELECT voucher_type VoucherType, voucher_type_id TypeID FROM tblGlDefVoucherType "
        ObjDataTable = UtilityDAL.GetDataTable(strSQL)

        ObjDataRow = ObjDataTable.NewRow
        ObjDataRow.Item("VoucherType") = gstrComboZeroIndexString
        ObjDataRow.Item("TypeID") = 0
        ObjDataTable.Rows.InsertAt(ObjDataRow, 0)


        'cmbVoucherType.DataSource = ObjDataTable.Copy

        'cmbVoucherType.DisplayMember = "VoucherType"
        'cmbVoucherType.ValueMember = "TypeID"
        ' =========================================================================================
        ' =========================================================================================




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
        ' =========================================================================================


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
            chkOtherVoucher.Visible = gblnShowOtherVoucher
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

        Dim strSql As String
        Dim strCondAccount As String
        Dim strYearCriteria As String
        Dim strLocationCriteria As String


        '=========================================================================================
        '-- Selection Criteia Building in case of Financial Year selection
        '=========================================================================================
        If Me.cmbFinancialYear.SelectedIndex > 0 Then

            strYearCriteria = " (dbo.tblGlVoucher.finiancial_year_id = " & cmbFinancialYear.SelectedValue & " ) AND "
        Else

            strYearCriteria = " ( dbo.tblGlVoucher.Voucher_no <> '000000' ) AND  "
        End If

        '=========================================================================================
        '-- Selection Criteia Building in case of Location selection
        '=========================================================================================
        If Me.cmbCompany.SelectedIndex > 0 Then

            strLocationCriteria = " (dbo.tblGlVoucher.location_id = " & cmbCompany.SelectedValue & ") AND "
        Else
            strLocationCriteria = "  "
        End If

        Dim post As Boolean = Me.chkShowUnposted.Checked
        Dim strPostingCriteria As String = ""

        If post = True Then
            strPostingCriteria = ""
        Else
            strPostingCriteria = "  (tblGlVoucher.Post = '1') AND"
        End If

        Dim strPostCriteria As String
        Dim strOther_Voucher_Criteria As String
        Dim intlocation_id As Integer

        '' if user dont check the checkbox of "include unposted vouchers" then user want to see only
        '' posted vouchers so we add the check
        'If chkunposted.Value = vbUnchecked Then

        '    strPostCriteria = "  (tblGlVoucher.post = 1) AND "
        'Else

        '    strPostCriteria = ""
        'End If

        '' if user dont check the checkbox of "include unposted vouchers" then user want to see only
        '' posted vouchers so we add the check

        'CR# 51
        'If Me.chkOtherVoucher.Checked = True Then

        '    strOther_Voucher_Criteria = "  (tblGlVoucher.Other_Voucher = 0) AND "
        'Else

        '    strOther_Voucher_Criteria = ""
        'End If

        If Me.chkOtherVoucher.Checked = True Then
            strOther_Voucher_Criteria = ""              'Means Include Other Vouchers
        Else
            strOther_Voucher_Criteria = "  (tblGlVoucher.Other_Voucher = 0) AND " 'Don't include other vouchers
        End If

        '   get the location id
        If Me.cmbCompany.SelectedIndex > 0 Then

            intlocation_id = Me.cmbCompany.SelectedValue
        Else

            intlocation_id = 0
        End If

    

        strSql = "SELECT SUM(credit_amount)-SUM(debit_amount) from ("
        strSql = strSql & "SELECT     dbo.tblGlCOAMainSubSubDetail.coa_detail_id, dbo.tblGlVoucher.voucher_date, dbo.tblGlVoucher.cheque_no, dbo.tblGlVoucher.cheque_date, " & _
                         "                      dbo.tblGlVoucherDetail.comments, dbo.tblGlCOAMainSubSubDetail.detail_title, dbo.tblGlVoucherDetail.debit_amount, " & _
                         "                      dbo.tblGlVoucherDetail.credit_amount , dbo.tblGlVoucher.post " & _
                         "FROM         dbo.tblGlVoucher INNER JOIN " & _
                         "                      dbo.tblGlVoucherDetail ON dbo.tblGlVoucher.voucher_id = dbo.tblGlVoucherDetail.voucher_id INNER JOIN " & _
                         "                      dbo.tblGlCOAMainSubSubDetail ON dbo.tblGlVoucherDetail.coa_detail_id = dbo.tblGlCOAMainSubSubDetail.coa_detail_id " & _
                         "WHERE  " & strYearCriteria & strLocationCriteria & strPostingCriteria & strOther_Voucher_Criteria & "  " & _
                         "                      (dbo.tblGlVoucher.voucher_date BETWEEN '" & Format(Me.dtFromDate.MinDate, pbDateFormat) & "' AND '" & Format(Me.dtFromDate.Value.Date.AddDays(-1), pbDateFormat) & "') AND (dbo.tblGlVoucher.voucher_type_id IN " & _
                         "                          (SELECT     voucher_Type_id " & _
                         "                            From tblGLDefVoucherType " & _
                         "                            WHERE      gl_type IN ('BP', 'CP'))) AND (ISNULL(dbo.tblGlVoucherDetail.debit_amount, 0) > 0) " & _
                         "Union " & _
                         "SELECT     dbo.tblGlCOAMainSubSubDetail.coa_detail_id, dbo.tblGlVoucher.voucher_date, dbo.tblGlVoucher.cheque_no, dbo.tblGlVoucher.cheque_date, " & _
                         "                      dbo.tblGlVoucherDetail.comments, dbo.tblGlCOAMainSubSubDetail.detail_title, dbo.tblGlVoucherDetail.debit_amount, " & _
                         "                      dbo.tblGlVoucherDetail.credit_amount , dbo.tblGlVoucher.post " & _
                         "FROM         dbo.tblGlVoucher INNER JOIN " & _
                         "                      dbo.tblGlVoucherDetail ON dbo.tblGlVoucher.voucher_id = dbo.tblGlVoucherDetail.voucher_id INNER JOIN " & _
                         "                      dbo.tblGlCOAMainSubSubDetail ON dbo.tblGlVoucherDetail.coa_detail_id = dbo.tblGlCOAMainSubSubDetail.coa_detail_id " & _
                         "WHERE  " & strYearCriteria & strLocationCriteria & strPostingCriteria & strOther_Voucher_Criteria & "  " & _
                         "                      (dbo.tblGlVoucher.voucher_date BETWEEN '" & Format(dtFromDate.MinDate, pbDateFormat) & "' AND '" & Format(dtFromDate.Value.AddDays(-1), pbDateFormat) & "') AND (dbo.tblGlVoucher.voucher_type_id IN " & _
                         "                          (SELECT     voucher_Type_id " & _
                         "                            From tblGLDefVoucherType " & _
                         "                            WHERE      gl_type IN ('BR', 'CR'))) AND (ISNULL(dbo.tblGlVoucherDetail.credit_amount, 0) > 0) "

        strSql = strSql & ")tblOpeningBalance"

      
        dblCashBankOpening = Val(UtilityDAL.ReturnDataRow(strSql).Item(0).ToString)


        strSql = "Alter View vwGLCashFlowPeriodRPT As "

        strSql = strSql & "SELECT  0 AS Tr_Type ,   dbo.tblGlCOAMainSubSubDetail.coa_detail_id, dbo.tblGlVoucher.voucher_date, dbo.tblGlVoucher.cheque_no, dbo.tblGlVoucher.cheque_date, " & _
                         "                      dbo.tblGlVoucherDetail.comments, dbo.tblGlCOAMainSubSubDetail.detail_title, dbo.tblGlVoucherDetail.debit_amount, " & _
                         "                      dbo.tblGlVoucherDetail.credit_amount , dbo.tblGlVoucher.post " & _
                         "FROM         dbo.tblGlVoucher INNER JOIN " & _
                         "                      dbo.tblGlVoucherDetail ON dbo.tblGlVoucher.voucher_id = dbo.tblGlVoucherDetail.voucher_id INNER JOIN " & _
                         "                      dbo.tblGlCOAMainSubSubDetail ON dbo.tblGlVoucherDetail.coa_detail_id = dbo.tblGlCOAMainSubSubDetail.coa_detail_id " & _
                         "WHERE  " & strYearCriteria & strLocationCriteria & strPostingCriteria & strOther_Voucher_Criteria & "  " & _
                         "                      (dbo.tblGlVoucher.voucher_date BETWEEN '" & Format(dtFromDate.Value, pbDateFormat) & "' AND '" & Format(dtToDate.Value, pbDateFormat) & "') AND (dbo.tblGlVoucher.voucher_type_id IN " & _
                         "                          (SELECT     voucher_Type_id " & _
                         "                            From tblGLDefVoucherType " & _
                         "                            WHERE      gl_type IN ('BP', 'CP'))) AND (ISNULL(dbo.tblGlVoucherDetail.debit_amount, 0) > 0) " & _
                         "Union " & _
                         "SELECT    1 AS Tr_Type , dbo.tblGlCOAMainSubSubDetail.coa_detail_id, dbo.tblGlVoucher.voucher_date, dbo.tblGlVoucher.cheque_no, dbo.tblGlVoucher.cheque_date, " & _
                         "                      dbo.tblGlVoucherDetail.comments, dbo.tblGlCOAMainSubSubDetail.detail_title, dbo.tblGlVoucherDetail.debit_amount, " & _
                         "                      dbo.tblGlVoucherDetail.credit_amount , dbo.tblGlVoucher.post " & _
                         "FROM         dbo.tblGlVoucher INNER JOIN " & _
                         "                      dbo.tblGlVoucherDetail ON dbo.tblGlVoucher.voucher_id = dbo.tblGlVoucherDetail.voucher_id INNER JOIN " & _
                         "                      dbo.tblGlCOAMainSubSubDetail ON dbo.tblGlVoucherDetail.coa_detail_id = dbo.tblGlCOAMainSubSubDetail.coa_detail_id " & _
                         "WHERE  " & strYearCriteria & strLocationCriteria & strPostingCriteria & strOther_Voucher_Criteria & "  " & _
                         "                      (dbo.tblGlVoucher.voucher_date BETWEEN '" & Format(dtFromDate.Value, pbDateFormat) & "' AND '" & Format(dtToDate.Value, pbDateFormat) & "') AND (dbo.tblGlVoucher.voucher_type_id IN " & _
                         "                          (SELECT     voucher_Type_id " & _
                         "                            From tblGLDefVoucherType " & _
                         "                            WHERE      gl_type IN ('BR', 'CR'))) AND (ISNULL(dbo.tblGlVoucherDetail.credit_amount, 0) > 0) "


        UtilityDAL.ExecuteQuery(strSQL)

        Dim ObjDAL As New DAL.RptCashFlowDal
        If ObjDAL.InsertDataForReport() Then
        Else
        End If

        Return ""

    End Function

    Public Sub FunAddReportPramaters() Implements IReportsInterface.FunAddReportPramaters

        Try

            ' Calling A Function Which Will Created View And Insert Data In Attached Table Of Grid ..
            FunAddReportCriteria()



            Dim objHashTableParamter As New Hashtable

            ' Giving Report Name .. 
            objHashTableParamter.Add("ReportPath", "\rptGlCashFlow.rpt")



            Dim ObjCompanyData As DataTable
            ObjCompanyData = UtilityDAL.setCompanyInfo(gobjLocationInfo.CompanyID)



            ' Passing Parameters .. (Report Parameters .. )
            objHashTableParamter.Add("companyname", ObjCompanyData.Rows(0).Item("CompanyName"))
            objHashTableParamter.Add("address", ObjCompanyData.Rows(0).Item("CompanyAddress"))

            objHashTableParamter.Add("fromdate", Format(Me.dtFromDate.Value.Date, "dd/MMM/yyyy"))
            objHashTableParamter.Add("todate", Format(Me.dtToDate.Value.Date, "dd/MMM/yyyy"))

            '' Adding Description Parameter .. 
            'If cmbVoucherType.SelectedIndex > 0 Then
            objHashTableParamter.Add("OpeningBalance", dblCashBankOpening)

            'Else
            '    objHashTableParamter.Add("description", "(All Vouchers) From " & Format(dtFromDate.Value.Date, "dd-MMM-yyyy") & " To  " & Format(dtToDate.Value.Date, "dd-MMM-yyyy"))


            'End If


            ' Adding Location Parameter .. 
            If cmbCompany.SelectedIndex > 0 Then
                objHashTableParamter.Add("Location", cmbCompany.Text)
            Else
                objHashTableParamter.Add("Location", "ALL")

            End If


            '' '' Adding A Parameter OF Show Header, 
            ' ''If Convert.ToBoolean(GetSystemConfigurationValue("Show_Report_Header").ToString) = True Then
            ' ''    objHashTableParamter.Add("ShowHeader", True)

            ' ''Else
            ' ''    objHashTableParamter.Add("ShowHeader", False)

            ' ''End If



            ' Adding Parameter Of Print And Export Button .. 
            ' =======================================================
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


    Private Sub btnGenerateButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click

        Try

            If dtFromDate.Value.Date > dtToDate.Value.Date Then
                ShowValidationMessage("FromDate should be less than ToDate")
                dtFromDate.Focus()
                Exit Sub
            End If


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

    Private Sub cmbFinancialYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFinancialYear.SelectedIndexChanged


        If cmbFinancialYear.SelectedIndex > 0 Then
            Dim dtRow As DataRowView = CType(cmbFinancialYear.SelectedItem, DataRowView)

            dtFromDate.MinDate = dtRow("Start Date")
            dtToDate.MaxDate = dtRow("End Date")

            dtFromDate.Value = dtRow("Start Date")
            dtToDate.Value = dtRow("End Date")

        Else

            dtFromDate.MinDate = CDate("01/07/1980")
            dtToDate.MaxDate = CDate("01/01/3000")

            Me.dtFromDate.Value = gobjBusinessStartDate
            Me.dtToDate.Value = Now

        End If

    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class