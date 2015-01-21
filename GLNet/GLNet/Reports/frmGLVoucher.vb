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
''// Date Modified     Modified by         Brief Description			                
''//------------------------------------------------------------------------------------
''/////////////////////////////////////////////////////////////////////////////////////////


Imports DAL
Imports Model
Imports System.Collections.Specialized
Imports System.Data
Imports Utility.Utility
Imports Microsoft.VisualBasic

Public Class frmGLVoucher
    Implements IGeneral, IReportsInterface


    Private Sub frmGLVoucher_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        dtFromDate.Value = DateAdd(DateInterval.Day, -7, Now)
        dtToDate.Value = Now

        ' Fill Combos Of Financial Year, Company nd Voucher Type ..   
        FillCombos()


    End Sub

#Region "Report Interface Metholds .. "

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
    End Sub

    Public Sub ApplySecurity(ByVal Mode As Utility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
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


        cmbVoucherType.DataSource = ObjDataTable.Copy

        cmbVoucherType.DisplayMember = "VoucherType"
        cmbVoucherType.ValueMember = "TypeID"
        ' =========================================================================================
        ' =========================================================================================




        ' Binding Company .. 
        ' =========================================================================================
        ' =========================================================================================
        Dim ObjDalCompany As New CompanyDAL
        ObjDataTable = ObjDalCompany.GetAll()


        ObjDataRow = ObjDataTable.NewRow
        ObjDataRow.Item("Company Name") = gstrComboZeroIndexString
        ObjDataRow.Item("Company ID") = 0
        ObjDataTable.Rows.InsertAt(ObjDataRow, 0)


        cmbCompany.DataSource = ObjDataTable.Copy


        cmbCompany.DisplayMember = "Company Name"
        cmbCompany.ValueMember = "Company ID"

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
    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting
    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As Utility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons
    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update
    End Function

#End Region

#Region "Report Interface Metholds .. "

    Public Function FunAddReportCriteria() As String Implements IReportsInterface.FunAddReportCriteria


        Dim strYearCriteria As String = ""
        Dim strLocationCriteria As String = ""
        Dim strSQL As String = ""

        ' Checking If Financial Year Is Selected .. Then Add Its Criteria .. 
        If cmbFinancialYear.SelectedIndex > 0 Then
            strYearCriteria = " (dbo.tblGlVoucher.finiancial_year_id = " & cmbFinancialYear.SelectedValue & " )   AND "

        End If


        ' Checking If Company Is Selected .. Then Add Its Criteria .. 
        If cmbCompany.SelectedIndex > 0 Then
            strLocationCriteria = " (dbo.tblGlVoucher.location_id = " & cmbCompany.SelectedValue & " ) AND  "

        End If


        ' Building View .. 
        strSQL = " Alter view vwGlVouchers as  " _
               & " SELECT top 100 percent dbo.tblGlVoucher.voucher_code, dbo.tblGlVoucher.finiancial_year_id, dbo.tblGlVoucher.voucher_type_id, dbo.tblGlVoucher.location_id, " _
               & " dbo.tblGlVoucher.voucher_no, dbo.tblGlVoucher.voucher_date, dbo.tblGlDefLocation.location_name, dbo.tblGlVoucher.paid_to, " _
               & " dbo.tblGlVoucher.cheque_no, dbo.tblGlVoucher.cheque_date, dbo.tblGlVoucher.cheque_paid, dbo.tblGlVoucher.cheque_paid_date, " _
               & " dbo.tblGlVoucher.post, dbo.vwGlCOADetail.main_code, dbo.vwGlCOADetail.main_title, dbo.vwGlCOADetail.main_type, dbo.vwGlCOADetail.sub_code, " _
               & " dbo.vwGlCOADetail.sub_title, dbo.vwGlCOADetail.sub_sub_code, dbo.vwGlCOADetail.sub_sub_title, dbo.vwGlCOADetail.account_type, " _
               & " dbo.tblGlVoucherDetail.cost_center_id, dbo.tblGlDefGLCostCenter.cost_center_title, dbo.vwGlCOADetail.detail_code, dbo.vwGlCOADetail.detail_title, " _
               & " dbo.tblGlVoucherDetail.Comments , dbo.tblGlVoucherDetail.debit_amount, dbo.tblGlVoucherDetail.credit_amount " _
               & " FROM  dbo.tblGlVoucherDetail INNER JOIN " _
               & " dbo.tblGlVoucher ON dbo.tblGlVoucherDetail.voucher_id = dbo.tblGlVoucher.voucher_id AND " _
               & " dbo.tblGlVoucherDetail.location_id = dbo.tblGlVoucher.location_id INNER JOIN  " _
               & " dbo.vwGlCOADetail ON dbo.tblGlVoucherDetail.coa_detail_id = dbo.vwGlCOADetail.coa_detail_id INNER JOIN  " _
               & " dbo.tblGlDefLocation ON dbo.tblGlVoucherDetail.location_id = dbo.tblGlDefLocation.location_id LEFT OUTER JOIN  " _
               & " dbo.tblGlDefGLCostCenter ON dbo.tblGlVoucherDetail.cost_center_id = dbo.tblGlDefGLCostCenter.cost_center_id "

        strSql = strSql + " WHERE   " & strYearCriteria & strLocationCriteria & "  "


        If chkUnPostedVoucher.Checked = False Then strSQL = strSQL & " (tblGlVoucher.post = 1) AND "
        If chkOtherVoucher.Checked = False Then strSQL = strSQL + "  (dbo.tblGlVoucher.Other_Voucher = 0) AND "

        ' Adding Dates Criteria .. 
        strSQL = strSQL + "  voucher_date between '" & Format(dtFromDate.Value.Date, "yyyy-MM-dd") & "' And '" & Format(dtToDate.Value.Date, "yyyy-MM-dd") & "'"

        If cmbVoucherType.SelectedIndex > 0 Then
            strSQL = strSQL + " and voucher_type_id = " & cmbVoucherType.SelectedValue

        End If

        strSQL = strSQL + " Order by dbo.tblGlVoucher.voucher_date, dbo.tblGlVoucher.voucher_no "

        UtilityDAL.ExecuteQuery(strSQL)

        Dim ObjDAL As New DAL.GLVoucherDAL
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
            objHashTableParamter.Add("ReportPath", "Reports\rptGlVoucher.rpt")



            Dim ObjCompanyData As DataTable
            ObjCompanyData = UtilityDAL.setCompanyInfo("1")



            ' Passing Parameters .. (Report Parameters .. )
            objHashTableParamter.Add("companyname", ObjCompanyData.Rows(0).Item("CompanyName"))
            objHashTableParamter.Add("address", ObjCompanyData.Rows(0).Item("CompanyAddress"))



            ' Adding Description Parameter .. 
            If cmbVoucherType.SelectedIndex > 0 Then
                objHashTableParamter.Add("description", "(" & cmbVoucherType.Text & "  Vouchers) From " & Format(dtFromDate.Value.Date, "dd-MMM-yyyy") & " To  " & Format(dtToDate.Value.Date, "dd-MMM-yyyy"))

            Else
                objHashTableParamter.Add("description", "(All Vouchers) From " & Format(dtFromDate.Value.Date, "dd-MMM-yyyy") & " To  " & Format(dtToDate.Value.Date, "dd-MMM-yyyy"))


            End If


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
    

    Private Sub btnGenerateButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerateButton.Click

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
            dtToDate.MaxDate = CDate("30/06/3000")

            Me.dtFromDate.Value = DateAdd(DateInterval.Day, -7, Now)
            Me.dtToDate.Value = Now

        End If

    End Sub

End Class