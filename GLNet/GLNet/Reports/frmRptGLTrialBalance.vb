Imports DAL
Imports Model
Imports System.Collections.Specialized
Imports System.Data
Imports Utility.Utility
''//-------------------------------------------------------------------------------------
''// Date Modified     Modified by         Brief Description			                
''//------------------------------------------------------------------------------------
''// 18 May,2010       Abdul Jabbar      CR#46.     'Date Fromate on Trial Balance report is "YYYY-MM-dd" wherase it should be "dd-MMM-yyyy".
''// 18 May,2010       Abdul Jabbar      CR#47.      Validation has been applied before report printing which enforce the user to select group.
''// 06-june-2013      Fatima Tajammal   CR#250      Trial Balance ANd Bank POstion Date Formate
''// 17 Jul,2014       Abdul Jabbar      CR#320.     Include source filter in Trial Balance, Balance Sheet and P&L Report
''// 30 Dec,2014       M.Shoaib          CR#350.     Trial Balance: Implement Cost Center filter on trial balance reports
''/////////////////////////////////////////////////////////////////////////////////////////

Public Class frmRptGLTrialBalance
    Implements IGeneral, IReportsInterface
    Dim strAlterViewOpening As String = ""
    Dim strProcedure As String = ""
    Dim strAlterViewTrialPeriod As String = ""
#Region "Variables"
    ''This collection will hold the controls' names, upon which the logged in user has rights
    Private mobjControlList As NameValueCollection
#End Region

#Region "Interface Methods"
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

                If mobjControlList.Item("btnPrint") Is Nothing Then
                    btnPrint.Enabled = False
                Else
                    btnPrint.Enabled = True
                End If

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
        Try
            Dim dt As New DataTable
            Dim dr As DataRow

            dt = New FiniancialYearDefDAL().GetAll()
            dr = dt.NewRow
            dr("FYear ID") = 0
            dr("FYear Code") = "---All Financial Years---"
            dt.Rows.InsertAt(dr, 0)

            cboFinancialYear.DisplayMember = "FYear Code"
            cboFinancialYear.ValueMember = "FYear ID"
            cboFinancialYear.DataSource = dt

            dt = Nothing

            If Me.cboFinancialYear.Items.Count > 1 Then
                Me.cboFinancialYear.SelectedValue = gObjFinancialYearInfo.FYearID

            End If

            dt = New PostDatedChequesDAL().GetCompanies(gObjUserInfo.UserID)
            dr = dt.NewRow
            dr("location_id") = 0
            dr("Location") = gstrComboZeroIndexString
            dt.Rows.InsertAt(dr, 0)

            cboCompany.DisplayMember = "Location"
            cboCompany.ValueMember = "location_id"
            cboCompany.DataSource = dt

            If Me.cboCompany.Items.Count > 1 Then
                Me.cboCompany.SelectedValue = gobjLocationInfo.CompanyID

            End If

            ' CR # 350 Start
            ' Fill combobox
            dt = Nothing
            dt = New GLCostCenterDal().GetAll()
            dr = dt.NewRow
            dr("Cost Center ID") = 0
            dr("Cost Center Title") = "---Select---"
            dt.Rows.InsertAt(dr, 0)
            cboCostCenter.DataSource = dt
            cboCostCenter.DisplayMember = "Cost Center Title"
            cboCostCenter.ValueMember = "Cost Center ID"

            ' CR # 350 End

            'CR#320, Populating source (replicated shops) drop down
            cmbSource.Items.Clear()
            Dim ObjDataRow As DataRow
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

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
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

            Me.btnPrevious.ImageList = gobjMyImageListForOperationBar
            Me.btnPrevious.ImageKey = "Previous"

            Me.btnNext.ImageList = gobjMyImageListForOperationBar
            Me.btnNext.ImageKey = "Next"

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

            Me.btnPrint.ImageList = gobjMyImageListForOperationBar
            Me.btnPrint.ImageKey = "Print"

            Me.btnExit.ImageList = gobjMyImageListForOperationBar
            Me.btnExit.ImageKey = "Exit"

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting
        Try
            chkIncludeOtherVouchers.Visible = gblnShowOtherVoucher
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

    Public Function Update(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update

    End Function
#End Region

#Region "Reports Interface Methods"
    Public Function FunAddReportCriteria() As String Implements IReportsInterface.FunAddReportCriteria

    End Function

    Public Sub FunAddReportPramaters() Implements IReportsInterface.FunAddReportPramaters
        Try
            Dim strReportName As String
            Dim strReportCaption As String
            Dim strSupressDetail As Boolean

            Dim strAcLevel As String
            strSupressDetail = True

            strReportName = "\rptGlTrailBalance.rpt"

            If cboGroup.SelectedItem.ToString = "First Level" Then
                strReportCaption = "Trial Balance First Level "
                strAcLevel = "First Level"

            ElseIf cboGroup.SelectedItem.ToString = "Second Level" Then
                strReportCaption = "Trial Balance Second Level "
                strAcLevel = "Second Level"

            ElseIf cboGroup.SelectedItem.ToString = "Third Level" Then
                strReportCaption = "Trial Balance Third Level "
                strAcLevel = "Third Level"

            ElseIf cboGroup.SelectedItem.ToString = "Detail Level" Then
                strReportCaption = "Trial Balance"
                strAcLevel = "Detail Level"
                strSupressDetail = False

            End If
            'CR#320 Stop Extra method call as method body has no Code
            'FunAddReportCriteria()

            Dim objHashTableParamter As New Hashtable

            ' Giving Report Name .. 
            objHashTableParamter.Add("ReportPath", "\" & strReportName)

            If cboCompany.SelectedIndex > 0 Then
                Dim ObjCompanyData As DataTable
                ObjCompanyData = UtilityDAL.setCompanyInfo(cboCompany.SelectedValue)

                ' Passing Parameters .. (Report Parameters .. )
                objHashTableParamter.Add("companyname", ObjCompanyData.Rows(0).Item("CompanyName"))
                objHashTableParamter.Add("address", ObjCompanyData.Rows(0).Item("CompanyAddress"))
            Else
                ' Passing Parameters .. (Report Parameters .. )
                objHashTableParamter.Add("companyname", DAL.SystemConfigurationDAL.GetSystemConfigurationValue("CompanyName").ToString)
                objHashTableParamter.Add("address", DAL.SystemConfigurationDAL.GetSystemConfigurationValue("CompanyAddress").ToString)

            End If

            'CR# 46
            'objHashTableParamter.Add("fromDate", Format(dtpStartDate.Value, pbDateFormat))
            'objHashTableParamter.Add("toDate", Format(dtpEndDate.Value, pbDateFormat))

            objHashTableParamter.Add("fromdate", Format(Me.dtpStartDate.Value.Date, "dd/MMM/yyyy"))
            objHashTableParamter.Add("todate", Format(Me.dtpEndDate.Value.Date, "dd/MMM/yyyy"))

            objHashTableParamter.Add("SupressDetail", strSupressDetail)

            If chkIncludeUnPostedVouchers.Checked Then
                objHashTableParamter.Add("PostedFlag", "With UnPosted Vouchers")
            Else
                objHashTableParamter.Add("PostedFlag", "")
            End If

            'CR109: Hide Zero Balance Accounts
            If chkHideZeroBalance.Checked Then
                objHashTableParamter.Add("HideZeroBal", "True")
            Else
                objHashTableParamter.Add("HideZeroBal", "False")
            End If

            
            '**************************************************************************
            ' The mask check for Account Control is removed after discussion with Tariq
            '**************************************************************************

            ''If strAcLevel = "First Level" Then
            ''    objHashTableParamter.Add("AcLevel", strAcLevel)

            ''    If UiCtrlGLAccount1.txtACCode.Text <> "  " Then
            ''        ''txtGLAccountCode.PromptInclude = True
            ''        objHashTableParamter.Add("detail_code", UiCtrlGLAccount1.GLAccountCode)

            ''    Else
            ''        objHashTableParamter.Add("detail_code", "")

            ''    End If

            ''ElseIf strAcLevel = "Second Level" Then
            ''    objHashTableParamter.Add("AcLevel", strAcLevel)

            ''    If UiCtrlGLAccount1.txtACCode.Text <> "  -   " Then
            ''        ''txtGLAccountCode.PromptInclude = True
            ''        objHashTableParamter.Add("detail_code", UiCtrlGLAccount1.GLAccountCode)

            ''    Else
            ''        objHashTableParamter.Add("detail_code", "")

            ''    End If



            ''ElseIf strAcLevel = "Third Level" Then
            ''    objHashTableParamter.Add("AcLevel", strAcLevel)

            ''    If UiCtrlGLAccount1.txtACCode.Text <> "  -   -   " Then
            ''        ''txtGLAccountCode.PromptInclude = True
            ''        objHashTableParamter.Add("detail_code", UiCtrlGLAccount1.GLAccountCode)

            ''    Else
            ''        objHashTableParamter.Add("detail_code", "")

            ''    End If


            ''ElseIf strAcLevel = "Detail Level" Then
            ''    objHashTableParamter.Add("AcLevel", strAcLevel)

            ''    If UiCtrlGLAccount1.txtACCode.Text <> "  -   -   -     " Then
            ''        '' txtGLAccountCode.PromptInclude = True
            ''        objHashTableParamter.Add("detail_code", UiCtrlGLAccount1.GLAccountCode)

            ''    Else
            ''        objHashTableParamter.Add("detail_code", "")

            ''    End If

            ''End If

            objHashTableParamter.Add("AcLevel", strAcLevel)

            If UiCtrlGLAccount1.txtACCode.Text.ToString.Trim = "" Then
                ''txtGLAccountCode.PromptInclude = True
                objHashTableParamter.Add("detail_code", "")
            Else
                objHashTableParamter.Add("detail_code", UiCtrlGLAccount1.GLAccountCode)
            End If


            If cboCompany.SelectedIndex > 0 Then
                Dim ObjCompanyData As DataTable
                ObjCompanyData = UtilityDAL.setCompanyInfo(cboCompany.SelectedValue)

                ' Passing Parameters .. (Report Parameters .. )
                objHashTableParamter.Add("Location", ObjCompanyData.Rows(0).Item("CompanyName"))
            Else
                ' Passing Parameters .. (Report Parameters .. )
                objHashTableParamter.Add("Location", "ALL")

            End If

            'If cboCompany.SelectedIndex = 0 Then
            '    objHashTableParamter.Add("Location", "ALL")
            'Else
            '    objHashTableParamter.Add("Location", CType(cboCompany.SelectedItem, DataRow).Item(1).ToString)

            'End If


            ' objHashTableParamter.Add("Location", IIf(cboCompany.SelectedValue = 0, "ALL", cboCompany.SelectedText))
            ' CR # 350
            Dim isCostCenter As Boolean = False

            If cboCostCenter.SelectedValue > 0 Then
                isCostCenter = True
            End If
            ' End CR # 350
            SettingReportFilter()
            ' CR # 350
            'TrialBalanceDAL.GenerateTrialBalance(strProcedure, strAlterViewOpening, strAlterViewTrialPeriod)
            TrialBalanceDAL.GenerateTrialBalance(strProcedure, strAlterViewOpening, strAlterViewTrialPeriod, isCostCenter)

            objHashTableParamter.Add("PrintRights", "True")
            objHashTableParamter.Add("ExportRights", "True")

            gObjMyAppHashTable.Add(EnumHashTableKeyConstants.SetReportParametersList, objHashTableParamter)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub



    Private Sub SettingReportFilter()

        

        Dim strCondAccount As String = ""
        Dim strYearCriteria As String = ""
        Dim strLocationCriteria As String = ""
        Dim strCostCenterCriteria As String = ""


        ' Adding Financial Year Selection Criteria .. 
        If cboFinancialYear.SelectedIndex > 0 Then
            strYearCriteria = " (dbo.tblGlVoucher.finiancial_year_id = " & cboFinancialYear.SelectedValue & " ) AND "
        Else
            strYearCriteria = " ( dbo.tblGlVoucher.Voucher_no <> '000000' ) AND  "

        End If


        ' Adding Company Criteria .. 
        If cboCompany.SelectedIndex > 0 Then
            strLocationCriteria = " (dbo.tblGlVoucher.location_id = " & cboCompany.SelectedValue & ") AND "
        Else
            strLocationCriteria = ""

        End If
        ' CR # 350, Adding Cost Center Criteria....

        If cboCostCenter.SelectedIndex > 0 Then
            strCostCenterCriteria = " (tblGlDefGLCostCenter.cost_center_id = " & cboCostCenter.SelectedValue & ") AND   "
        Else
            strCostCenterCriteria = ""
        End If



        Dim strPostCriteria As String
        Dim strOther_Voucher_Criteria As String


        ' Adding Selection Criteria .. (Posted Or Un-Posted .. )
        If chkIncludeUnPostedVouchers.Checked = False Then
            strPostCriteria = "  (tblGlVoucher.post = 1) AND "
        Else

            strPostCriteria = ""

        End If

        
        If chkIncludeOtherVouchers.Checked = False Then
            strOther_Voucher_Criteria = "  (tblGlVoucher.Other_Voucher = 0) AND "
        Else
            strOther_Voucher_Criteria = ""

        End If

        Dim ilocation_id As Integer


        ' Get Location ID .. 
        If cboCompany.SelectedIndex > 0 Then
            ilocation_id = cboCompany.SelectedValue
        Else
            ilocation_id = 0

        End If

        'CR#320
        Dim strSource As String = String.Empty
        If Me.cmbSource.SelectedIndex > 0 Then
            strSource = strSource + " tblGlVoucher.source = '" & Me.cmbSource.Text & "' And"
        Else
            strSource = strSource + " "
        End If


        strProcedure = " usp_AccTrialOpeningBalance '" & UiCtrlGLAccount1.GLAccountCode & "','" & Format(dtpStartDate.Value.Date, "yyyy/MM/dd") & "'," & ilocation_id & "," & Val(chkIncludeUnPostedVouchers.Checked) & "," & Val(chkIncludeOtherVouchers.Checked) & ",'" & IIf(Me.cmbSource.SelectedIndex > 0, Me.cmbSource.Text, "Accounts") & "'"

        strAlterViewOpening = " ALTER VIEW vwGlTrailOpening as " _
               & " SELECT  dbo.tblGlCOAMainSubSubDetail.coa_detail_id, Sum(dbo.tmpTblGLAccountsOpening.Opening_debit_Amount) AS Opening_debit_Amount, " _
               & " SUM(dbo.tmpTblGLAccountsOpening.Opening_credit_Amount) AS Opening_credit_Amount  , SUM(dbo.tmpTblGLAccountsOpening.OpeningBalance) AS OpeningBalance " _
               & " FROM         dbo.tblGlCOAMainSubSubDetail INNER JOIN " _
               & " dbo.tmpTblGLAccountsOpening ON dbo.tblGlCOAMainSubSubDetail.coa_detail_id = dbo.tmpTblGLAccountsOpening.coa_detail_id" _
               & " GROUP BY dbo.tblGlCOAMainSubSubDetail.coa_detail_id , dbo.tblGlCOAMainSubSubDetail.detail_code "

        '        strAlterViewOpening = " ALTER VIEW vwGlTrailOpening as " _
        '& "SELECT     tblGlCOAMainSubSubDetail.coa_detail_id, SUM(tmpTblGLAccountsOpening.Opening_debit_Amount) AS Opening_debit_Amount, " _
        '                      & " SUM(tmpTblGLAccountsOpening.Opening_credit_Amount) AS Opening_credit_Amount, SUM(tmpTblGLAccountsOpening.OpeningBalance) AS OpeningBalance,  " _
        '        & " tblGlDefGLCostCenter.cost_center_title" _
        '& " FROM         tblGlCOAMainSubSubDetail INNER JOIN " _
        '    & "                  tmpTblGLAccountsOpening ON tblGlCOAMainSubSubDetail.coa_detail_id = tmpTblGLAccountsOpening.coa_detail_id INNER JOIN " _
        '       & "               tblGlVoucherDetail ON tblGlCOAMainSubSubDetail.coa_detail_id = tblGlVoucherDetail.coa_detail_id INNER JOIN " _
        '          & "            tblGlDefGLCostCenter ON tblGlVoucherDetail.cost_center_id = tblGlDefGLCostCenter.cost_center_id " _
        '& " GROUP BY tblGlCOAMainSubSubDetail.coa_detail_id, tblGlCOAMainSubSubDetail.detail_code, tblGlDefGLCostCenter.cost_center_title"

        'CR#320 Source criteria Added in query, source is basically filteration on replicated shop
        'strAlterViewTrialPeriod = " Alter view vwGlTrailForPeriod as " _
        '       & " SELECT dbo.tblGlCOAMainSubSubDetail.coa_detail_id, SUM(dbo.tblGlVoucherDetail.debit_amount) AS Debit_Amount, " _
        '       & " SUM(dbo.tblGlVoucherDetail.credit_amount) AS Credit_Amount, SUM(dbo.tblGlVoucherDetail.debit_amount) " _
        '       & " - SUM(dbo.tblGlVoucherDetail.credit_amount) AS Balance , tblGlVoucher.post " _
        '       & " FROM dbo.tblGlVoucher INNER JOIN " _
        '       & " dbo.tblGlVoucherDetail ON dbo.tblGlVoucher.voucher_id = dbo.tblGlVoucherDetail.voucher_id INNER JOIN " _
        '       & " dbo.tblGlCOAMainSubSubDetail ON dbo.tblGlVoucherDetail.coa_detail_id = dbo.tblGlCOAMainSubSubDetail.coa_detail_id  " _
        '       & " INNER JOIN dbo.tblGlDefFinancialYear ON dbo.tblGlVoucher.finiancial_year_id = dbo.tblGlDefFinancialYear.financial_year_id " _
        '       & " WHERE  " & strYearCriteria & strLocationCriteria & strPostCriteria & strOther_Voucher_Criteria & strSource & "  (dbo.tblGlVoucher.voucher_date BETWEEN '" & Format(dtpStartDate.Value.Date, pbDateFormat) & "' AND '" & Format(dtpEndDate.Value.Date, pbDateFormat) & "') " _
        '       & " " & strCondAccount & "  " _
        '       & " GROUP BY dbo.tblGlCOAMainSubSubDetail.coa_detail_id, dbo.tblGlCOAMainSubSubDetail.detail_code ,tblGlVoucher.post "
        ' CR # 350, Cost Center criteria is added, it will only works if any cost center is selected.

        strAlterViewTrialPeriod = " Alter view vwGlTrailForPeriod as " _
               & " SELECT dbo.tblGlCOAMainSubSubDetail.coa_detail_id, SUM(dbo.tblGlVoucherDetail.debit_amount) AS Debit_Amount, " _
               & " SUM(dbo.tblGlVoucherDetail.credit_amount) AS Credit_Amount, SUM(dbo.tblGlVoucherDetail.debit_amount) " _
               & " - SUM(dbo.tblGlVoucherDetail.credit_amount) AS Balance , tblGlVoucher.post "

        If cboCostCenter.SelectedValue > 0 Then
            strAlterViewTrialPeriod = strAlterViewTrialPeriod & ", tblGlDefGLCostCenter.cost_center_title  "
        Else
            strAlterViewTrialPeriod = strAlterViewTrialPeriod & ", '' as cost_center_title  "
        End If

        strAlterViewTrialPeriod = strAlterViewTrialPeriod & " FROM dbo.tblGlVoucher INNER JOIN " _
               & " dbo.tblGlVoucherDetail ON dbo.tblGlVoucher.voucher_id = dbo.tblGlVoucherDetail.voucher_id INNER JOIN " _
               & " dbo.tblGlCOAMainSubSubDetail ON dbo.tblGlVoucherDetail.coa_detail_id = dbo.tblGlCOAMainSubSubDetail.coa_detail_id  " _
               & " INNER JOIN dbo.tblGlDefFinancialYear ON dbo.tblGlVoucher.finiancial_year_id = dbo.tblGlDefFinancialYear.financial_year_id " _
               & " INNER JOIN tblGlDefGLCostCenter ON tblGlVoucherDetail.cost_center_id = tblGlDefGLCostCenter.cost_center_id " _
               & " WHERE  " & strYearCriteria & strLocationCriteria & strPostCriteria & strOther_Voucher_Criteria & strSource & strCostCenterCriteria & "  (dbo.tblGlVoucher.voucher_date BETWEEN '" & Format(dtpStartDate.Value.Date, pbDateFormat) & "' AND '" & Format(dtpEndDate.Value.Date, pbDateFormat) & "') " _
               & " " & strCondAccount & "  " _
               & " GROUP BY dbo.tblGlCOAMainSubSubDetail.coa_detail_id, dbo.tblGlCOAMainSubSubDetail.detail_code ,tblGlVoucher.post, tblGlDefGLCostCenter.cost_center_title "
        '' End CR # 350

    End Sub


#End Region

#Region "Form Control Events"

    Private Sub frmRptGLTrialBalance_InputLanguageChanging(ByVal sender As Object, ByVal e As System.Windows.Forms.InputLanguageChangingEventArgs) Handles Me.InputLanguageChanging
        e.Cancel = True
    End Sub

    Private Sub frmRptGLTrialBalance_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try

            If e.Control And e.KeyCode = Keys.X Then
                If Me.btnExit.Enabled = True Then Me.Close()
            ElseIf e.Control And e.KeyCode = Keys.P Then
                If Me.btnPrint.Enabled = True Then btnPrint_Click(Nothing, Nothing)
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmRptGLTrialBalance_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            mobjControlList = GetFormSecurityControls(Me.Name)

            FillCombos()

            SetButtonImages()

            SetConfigurationBaseSetting()

            ApplySecurity(EnumDataMode.Disabled)

            cboGroup.SelectedIndex = 4
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cboGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboGroup.SelectedIndexChanged
        Try
            Select Case cboGroup.SelectedItem.ToString
                Case "First Level"
                    UiCtrlGLAccount1.GLAccountHeadType = "1"
                Case "Second Level"
                    UiCtrlGLAccount1.GLAccountHeadType = "2"
                Case "Third Level"
                    UiCtrlGLAccount1.GLAccountHeadType = "3"
                Case "Detail Level"
                    UiCtrlGLAccount1.GLAccountHeadType = "4"
            End Select
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click

        Try

            If Me.cboGroup.SelectedIndex = 0 Then
                MessageBox.Show("Please select a group", "Trial Balance", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            Call FunAddReportPramaters()

            Dim rptViewer As New rptViewer
            rptViewer.Text = Me.Text
            rptViewer.Show()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub

    Private Sub cboFinancialYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboFinancialYear.SelectedIndexChanged
        Try
            If cboFinancialYear.SelectedIndex = 0 Then
                dtpStartDate.Value = gobjBusinessStartDate
                dtpEndDate.Value = Now
                Exit Sub
            End If

            Dim dt As New DataTable
            dt = CType(cboFinancialYear.DataSource, DataTable)

            Dim dr() As DataRow
            dr = dt.Select("[FYear ID] = " & cboFinancialYear.SelectedValue)

            dtpStartDate.Value = dr(0)("Start Date")
            dtpEndDate.Value = dr(0)("End Date")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub

#End Region

End Class