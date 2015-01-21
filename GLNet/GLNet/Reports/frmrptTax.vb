''/////////////////////////////////////////////////////////////////////////////////////////
''//                      GL Net
''/////////////////////////////////////////////////////////////////////////////////////////
''//-------------------------------------------------------------------------------------
''// File Name       : frmrptTax 
''// Programmer	     : Rizwan Asif
''// Creation Date	 : 21-July-2009
''// Description     : 
''// Function List   : 								                                    
''//											                                            
''//-------------------------------------------------------------------------------------
''// Date Modified     Modified by         Brief Description			                
''//------------------------------------------------------------------------------------
''// 05-june-2013       Fatima Tajammal     CR # 249    Tax Deduction report Issue
''// 16-sep-2013        Fatima Tajammal     CR # 257    CTRL+P should work for printing the crystal reports
''// 31-Mar-2014        Abdul Jabbar     CR # 299    Configuration of New Report: Sale Tax
''/////////////////////////////////////////////////////////////////////////////////////////


Imports DAL
Imports Model
Imports System.Collections.Specialized
Imports System.Data
Imports Utility.Utility
Imports Microsoft.VisualBasic
Public Class frmrptTax
    Implements IGeneral, IReportsInterface

    Dim mobjControlList As NameValueCollection
    Dim TaxAccount As String
    Dim AmtCol As String

    Private Sub frmrptTax_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try

            If e.Control And e.KeyCode = Keys.X Then
                If Me.btnExit.Enabled = True Then Me.Close()
                'CR # 257
            ElseIf e.Control And e.KeyCode = Keys.P Then
                If Me.btnPrint.Enabled = True Then btnGenerateButton_Click(Nothing, Nothing)
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub frmGLVoucher_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ''Getting all available controls list to the user on this screen; in a collection 
        mobjControlList = GetFormSecurityControls(Me.Name)
        Me.SetButtonImages()
        dtFromDate.Value = Date.Today.AddDays(-7)
        dtToDate.Value = Now

        ' Fill Combos Of Financial Year, Company nd Voucher Type ..   
        FillCombos()

        Me.ApplySecurity(EnumDataMode.[New])


    End Sub

#Region "Report Interface Metholds .. "

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
    End Sub

    Public Sub ApplySecurity(ByVal Mode As Utility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
        'Try

        '    If mobjControlList.Item("btnGenerateButton") Is Nothing Then
        '        btnGenerateButton.Enabled = False
        '    Else
        '        btnGenerateButton.Enabled = True
        '    End If

        'Catch ex As Exception
        '    Throw ex
        'End Try

        Try



            btnPrint.Enabled = True

            If Mode.ToString = EnumDataMode.Disabled.ToString Then

                btnNew.Enabled = False ': btnNew.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnSave.Enabled = False ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System           
                btnUpdate.Enabled = False ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnDelete.Enabled = False ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnCancel.Enabled = False ': btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue

                'If mobjControlList.Item("btnPrint") Is Nothing Then
                '    btnPrint.Enabled = False
                'Else
                '    btnPrint.Enabled = True
                'End If

                SetNavigationButtons(EnumDataMode.[New])
                ' Me.grdAllRecords.Enabled = True
            ElseIf Mode.ToString = EnumDataMode.[New].ToString Then

                btnNew.Enabled = False ': btnNew.VisualStyle = C1.Win.C1Input.VisualStyle.System

                If mobjControlList.Item("btnSave") Is Nothing Then
                    btnSave.Enabled = False ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System
                Else
                    btnSave.Enabled = True ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                End If

                btnUpdate.Enabled = False ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnDelete.Enabled = False ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnCancel.Enabled = False ': btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue

                SetNavigationButtons(Mode)

                ' Me.grdAllRecords.Enabled = False

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

                '  Me.grdAllRecords.Enabled = True

                '  Me.grdAllRecords.Focus()

            ElseIf Mode.ToString = EnumDataMode.ReadOnly.ToString Then

                btnNew.Enabled = True ': btnNew.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                btnSave.Enabled = False ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System


                btnUpdate.Enabled = False ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnDelete.Enabled = False ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnCancel.Enabled = False ': btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.System

                SetNavigationButtons(Mode)

                ' Me.grdAllRecords.Enabled = True

                '  Me.grdAllRecords.Focus()

            End If


            '' Disabl/Enable the Button that converts Grid data in Excel Sheet According to Login User rights
            If mobjControlList.Item("btnExport") Is Nothing Then
                ' Me.UiCtrlGridBar1.btnExport.Enabled = False
            End If


            '' Disabl/Enable the Button that Prints Grid data According to Login User rights
            If mobjControlList.Item("btnPrint") Is Nothing Then
                '   Me.UiCtrlGridBar1.btnPrint.Enabled = False
            End If


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

        If Me.cmbFinancialYear.Items.Count > 1 Then
            Me.cmbFinancialYear.SelectedValue = gObjFinancialYearInfo.FYearID

        End If

        ObjDalFinancialYear = Nothing


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

        If Me.cmbCompany.Items.Count > 1 Then
            Me.cmbCompany.SelectedValue = gobjLocationInfo.CompanyID

        End If

        ObjDalCompany = Nothing
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
        Dim strSQL As String = ""

        strSQL = "select * from dbo.sysobjects where id = object_id(N'[dbo].[vw_rpttax]') "

        Dim dr As DataRow = UtilityDAL.ReturnDataRow(strSQL)

        If dr Is Nothing Then
            strSQL = "Create view vw_rptTax as"
        Else
            strSQL = "Alter view vw_rptTax as"
        End If

        'Code commented against CR # 249
        'strSQL = strSQL + " SELECT     TOP 100 PERCENT tblUnion.voucher_id,tblunion.comments, tblUnion.coa_detail_id, tblUnion.detail_title, tblTaxVIDz.debit_amount, tblTaxVIDz.credit_amount" _
        ' & " FROM         (SELECT     location_id,voucher_id,comments, tblglvoucherdetail.coa_detail_id, tblGlCOAMainSubSubDetail.detail_title, debit_amount, credit_amount" _
        '& " FROM          tblglVoucherDetail INNER JOIN" _
        '& " tblGlCOAMainSubSubDetail ON tblGlVoucherDetail.coa_detail_id = tblGlCOAMainSubSubDetail.coa_detail_id" _
        '& " WHERE      voucher_id IN" _
        '& " (SELECT     voucher_id AS voucher_id_credit" _
        '& " FROM          (SELECT     tblglvoucherdetail.voucher_id, debit_amount, credit_amount" _
        '& " FROM          TblGLvoucherDetail INNER JOIN" _
        '& " tblglvoucher ON tblglvoucherdetail.voucher_id = tblglvoucher.voucher_id AND" _
        '& " tblglvoucher.location_id = tblglvoucherdetail.location_id AND" _
        '& " tblglvoucher.shop_id = tblglvoucherdetail.shop_id" _
        '& " WHERE      tblglvoucherdetail.coa_detail_id = " & TaxAccount & "  AND " _
        '& " tblglvoucher.voucher_date Between '" & Format(Me.dtFromDate.Value, pbDateFormat) & "' AND '" & Format(Me.dtToDate.Value, pbDateFormat) & "') tblTaxVIDz" _
        '& " WHERE      credit_amount = 0) AND credit_amount <> 0" _
        '& " Union" _
        '& " SELECT     location_id,voucher_id,comments, tblglvoucherdetail.coa_detail_id, tblGlCOAMainSubSubDetail.detail_title, debit_amount, credit_amount" _
        '& " FROM         tblglVoucherDetail INNER JOIN" _
        '& " tblGlCOAMainSubSubDetail ON tblGlVoucherDetail.coa_detail_id = tblGlCOAMainSubSubDetail.coa_detail_id" _
        '& " WHERE     voucher_id IN" _
        '& " (SELECT     voucher_id AS voucher_id_debit" _
        '& " FROM          (SELECT     tblglvoucherdetail.voucher_id, debit_amount, credit_amount" _
        '& " FROM          TblGLvoucherDetail INNER JOIN" _
        '& " tblglvoucher ON tblglvoucherdetail.voucher_id = tblglvoucher.voucher_id AND" _
        '& " tblglvoucher.location_id = tblglvoucherdetail.location_id AND" _
        '& " tblglvoucher.shop_id = tblglvoucherdetail.shop_id" _
        '& " WHERE      tblglvoucherdetail.coa_detail_id = " & TaxAccount & "  AND" _
        '& " tblglvoucher.voucher_date Between '" & Format(Me.dtFromDate.Value, pbDateFormat) & "' AND '" & Format(Me.dtToDate.Value, pbDateFormat) & "') tblTaxVIDz" _
        '& " WHERE      debit_amount = 0) AND debit_amount <> 0) tblUnion INNER JOIN" _
        '& " (SELECT     finiancial_year_id,tblglvoucherdetail.voucher_id, debit_amount, credit_amount" _
        '& " FROM          TblGLvoucherDetail INNER JOIN" _
        '& " tblglvoucher ON tblglvoucherdetail.voucher_id = tblglvoucher.voucher_id AND" _
        '& " tblglvoucher.location_id = tblglvoucherdetail.location_id And tblglvoucher.shop_id = tblglvoucherdetail.shop_id" _
        '& " WHERE      tblglvoucherdetail.coa_detail_id = " & TaxAccount & "  AND " _
        '& " tblglvoucher.voucher_date Between '" & Format(Me.dtFromDate.Value, pbDateFormat) & "' AND '" & Format(Me.dtToDate.Value, pbDateFormat) & "' AND " & AmtCol & " = 0) tblTaxVIDz ON tblUnion.voucher_id = tblTaxVIDz.voucher_id"


        'CR # 249
        strSQL = strSQL + " SELECT     TOP 100 PERCENT tblUnion.voucher_id,tblunion.comments, tblUnion.coa_detail_id, tblUnion.detail_title, tblTaxVIDz.debit_amount, tblTaxVIDz.credit_amount" _
         & " FROM         (select * from (SELECT     location_id,voucher_id,comments, tblglvoucherdetail.coa_detail_id, tblGlCOAMainSubSubDetail.detail_title, debit_amount, credit_amount, row_number() over (partition by   tblGlVoucherDetail.location_id,tblGlVoucherDetail.voucher_id order by   tblGlVoucherDetail.location_id) as Top1  " _
        & " FROM          tblglVoucherDetail INNER JOIN" _
        & " tblGlCOAMainSubSubDetail ON tblGlVoucherDetail.coa_detail_id = tblGlCOAMainSubSubDetail.coa_detail_id" _
        & " WHERE      voucher_id IN" _
        & " (SELECT     voucher_id AS voucher_id_credit" _
        & " FROM          (SELECT     tblglvoucherdetail.voucher_id, debit_amount, credit_amount" _
        & " FROM          TblGLvoucherDetail INNER JOIN" _
        & " tblglvoucher ON tblglvoucherdetail.voucher_id = tblglvoucher.voucher_id AND" _
        & " tblglvoucher.location_id = tblglvoucherdetail.location_id AND" _
        & " tblglvoucher.shop_id = tblglvoucherdetail.shop_id" _
        & " WHERE      tblglvoucherdetail.coa_detail_id = " & TaxAccount & "  AND " _
        & " tblglvoucher.voucher_date Between '" & Format(Me.dtFromDate.Value, pbDateFormat) & "' AND '" & Format(Me.dtToDate.Value, pbDateFormat) & "') tblTaxVIDz" _
        & " WHERE      credit_amount = 0) AND credit_amount <> 0 ) Customer where top1=1        " _
        & " Union " _
        & " select vendor.* from (SELECT     tblGlVoucherDetail_3.location_id, tblGlVoucherDetail_3.voucher_id, tblGlVoucherDetail_3.comments, tblGlVoucherDetail_3.coa_detail_id,tblGlCOAMainSubSubDetail_1.detail_title, tblGlVoucherDetail_3.debit_amount, tblGlVoucherDetail_3.credit_amount, row_number() over (partition by   tblGlVoucherDetail_3.location_id,tblGlVoucherDetail_3.voucher_id order by   tblGlVoucherDetail_3.location_id) as Top1      " _
        & " FROM         tblGlVoucherDetail AS tblGlVoucherDetail_3 INNER JOIN " _
        & " tblGlCOAMainSubSubDetail AS tblGlCOAMainSubSubDetail_1 ON tblGlVoucherDetail_3.coa_detail_id = tblGlCOAMainSubSubDetail_1.coa_detail_id " _
        & " WHERE     (tblGlVoucherDetail_3.voucher_id IN " _
        & " (SELECT     voucher_id AS voucher_id_debit " _
        & " FROM          (SELECT     tblGlVoucherDetail_2.voucher_id, tblGlVoucherDetail_2.debit_amount, tblGlVoucherDetail_2.credit_amount " _
        & " FROM          tblGlVoucherDetail AS tblGlVoucherDetail_2 INNER JOIN " _
        & " tblGlVoucher AS tblGlVoucher_2 ON tblGlVoucherDetail_2.voucher_id = tblGlVoucher_2.voucher_id AND " _
        & " tblGlVoucher_2.location_id = tblGlVoucherDetail_2.location_id AND " _
        & " tblGlVoucher_2.shop_id = tblGlVoucherDetail_2.shop_id " _
        & " WHERE      tblGlVoucherDetail_2.coa_detail_id = " & TaxAccount & "  AND" _
        & " tblGlVoucher_2.voucher_date Between '" & Format(Me.dtFromDate.Value, pbDateFormat) & "' AND '" & Format(Me.dtToDate.Value, pbDateFormat) & "') tblTaxVIDz" _
        & " WHERE      debit_amount = 0)) AND tblGlVoucherDetail_3.debit_amount <> 0 ) vendor where top1=1  ) tblUnion INNER JOIN" _
        & " (SELECT     finiancial_year_id,tblglvoucherdetail.voucher_id, debit_amount, credit_amount" _
        & " FROM          TblGLvoucherDetail INNER JOIN" _
        & " tblglvoucher ON tblglvoucherdetail.voucher_id = tblglvoucher.voucher_id AND" _
        & " tblglvoucher.location_id = tblglvoucherdetail.location_id And tblglvoucher.shop_id = tblglvoucherdetail.shop_id" _
        & " WHERE      tblglvoucherdetail.coa_detail_id = " & TaxAccount & "  AND " _
        & " tblglvoucher.voucher_date Between '" & Format(Me.dtFromDate.Value, pbDateFormat) & "' AND '" & Format(Me.dtToDate.Value, pbDateFormat) & "' AND " & AmtCol & " = 0) tblTaxVIDz ON tblUnion.voucher_id = tblTaxVIDz.voucher_id"

        If Not Me.txtAccount.txtACCode.Text = "" Then
            strSQL = strSQL + " and  tblUnion.coa_detail_id=" & Me.txtAccount.GLAccountID
        End If

        If Me.cmbCompany.SelectedIndex > 0 Then
            strSQL = strSQL + " AND tblunion.location_id = " & Me.cmbCompany.SelectedValue
        End If

        If Me.cmbFinancialYear.SelectedIndex > 0 Then
            strSQL = strSQL + " and tblTaxVIDz.finiancial_year_id = " & Me.cmbFinancialYear.SelectedValue
        End If

        strSQL = strSQL + " ORDER BY tblUnion.detail_title"

        UtilityDAL.ExecuteQuery(strSQL)

        Return ""

    End Function

    Public Sub FunAddReportPramaters() Implements IReportsInterface.FunAddReportPramaters

        Try

            ' Calling A Function Which Will Created View And Insert Data In Attached Table Of Grid ..
            FunAddReportCriteria()



            Dim objHashTableParamter As New Hashtable

            ' Giving Report Name .. 
            objHashTableParamter.Add("ReportPath", "\rptSalesTax.rpt")


            If cmbCompany.SelectedIndex > 0 Then
                Dim ObjCompanyData As DataTable
                ObjCompanyData = UtilityDAL.setCompanyInfo(cmbCompany.SelectedValue)

                ' Passing Parameters .. (Report Parameters .. )
                objHashTableParamter.Add("strCompanyName", ObjCompanyData.Rows(0).Item("CompanyName"))
                objHashTableParamter.Add("strCompanyAddress", ObjCompanyData.Rows(0).Item("CompanyAddress"))
            Else
                ' Passing Parameters .. (Report Parameters .. )
                objHashTableParamter.Add("strCompanyName", DAL.SystemConfigurationDAL.GetSystemConfigurationValue("CompanyName").ToString)
                objHashTableParamter.Add("strCompanyAddress", DAL.SystemConfigurationDAL.GetSystemConfigurationValue("CompanyAddress").ToString)

            End If

            objHashTableParamter.Add("strFromDate", Me.dtFromDate.Value)
            objHashTableParamter.Add("strToDate", Me.dtToDate.Value)

            'CR#299
            'objHashTableParamter.Add("strReportTitle", IIf(Me.rdbCustomerTax.Checked, Me.rdbCustomerTax.Text, Me.rdbVendorTax.Text))
            Dim strReportTitle As String = String.Empty
            If Me.rdbCustomerTax.Checked Then
                strReportTitle = Me.rdbCustomerTax.Text
            ElseIf Me.rdbVendorTax.Checked Then
                strReportTitle = Me.rdbVendorTax.Text
            ElseIf Me.rdbTaxOnServices.Checked Then
                strReportTitle = Me.rdbTaxOnServices.Text
            End If

            objHashTableParamter.Add("strReportTitle", strReportTitle)

            ' Adding Parameter Of Print And Export Button .. 
            ' =======================================================
            ''If mobjControlList.Item("btnPrint") Is Nothing Then
            ''    objHashTableParamter.Add("PrintRights", "False")
            ''Else
            objHashTableParamter.Add("PrintRights", "True")
            ' ''End If


            ' ''If mobjControlList.Item("btnExport") Is Nothing Then
            ' ''    objHashTableParamter.Add("ExportRights", "False")
            ' ''Else
            objHashTableParamter.Add("ExportRights", "True")
            ' ''End If
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


            If Me.rdbCustomerTax.Checked Then

                TaxAccount = DAL.SystemConfigurationDAL.GetSystemConfigurationValue("gl_ac_income_tax_receivable")
                AmtCol = "tblGLVoucherDetail.credit_amount"

            ElseIf Me.rdbVendorTax.Checked Then

                TaxAccount = DAL.SystemConfigurationDAL.GetSystemConfigurationValue("gl_ac_income_tax_payable")
                AmtCol = "tblGLVoucherDetail.debit_amount"

                'CR#299
            ElseIf Me.rdbTaxOnServices.Checked Then

                TaxAccount = DAL.SystemConfigurationDAL.GetSystemConfigurationValue("gl_ac_Services_Tax")
                AmtCol = "tblGLVoucherDetail.debit_amount"

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

    Private Sub rdbVendorTax_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbVendorTax.CheckedChanged
        Try

            Me.txtAccount.GLAccountID = 0
            Me.txtAccount.GLAccountCode = ""
            Me.txtAccount.GLAccountName = ""

            If rdbVendorTax.Checked = True Then
                txtAccount.AccountType = EnumAccountTypes.Vendor
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

 
    Private Sub rdbCustomerTax_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbCustomerTax.CheckedChanged
        Try

            Me.txtAccount.GLAccountID = 0
            Me.txtAccount.GLAccountCode = ""
            Me.txtAccount.GLAccountName = ""

            If Me.rdbCustomerTax.Checked = True Then
                txtAccount.AccountType = EnumAccountTypes.Customer
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtAccount_GetGLAccount(ByVal sender As uiCtrlGLAccount) Handles txtAccount.GetGLAccount
        Try

            If Me.txtAccount.txtACCode.Text <> "" Then
                Me.txtAccount.GLAccountName = ""
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtAccount_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccount.Validated
        Try

            If Me.txtAccount.GLAccountCode = "" Then
                Me.txtAccount.GLAccountName = ""
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

End Class