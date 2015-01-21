''/////////////////////////////////////////////////////////////////////////////////////////
''//                      GL
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
''// 11 May,2010       Abdul Jabbar      CR#42. There should be a voucher Reports which have multiple vouchers print option with with each voucher # on new page facilitating following criteria,
'                                   '1) date i.e. From Voucher date-to Voucher Date
''// 11 Nov 2014       M. Shoaib        CR# 338 GL Voucher Printing: Add New Criteria of Voucher Number
''/////////////////////////////////////////////////////////////////////////////////////////


Imports DAL
Imports Model
Imports System.Collections.Specialized
Imports System.Data
Imports Utility.Utility
Imports Microsoft.VisualBasic

Public Class frmGLVoucherReport
    Implements IGeneral, IReportsInterface
    Dim mobjControlList As NameValueCollection

    Private Sub frmGLVoucherReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    ' Form Load Event .. 
    Private Sub frmGLVoucher_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            mobjControlList = GetFormSecurityControls(Me.Name)
            ''Assing Images to Buttons
            Me.SetButtonImages()
            ApplySecurity(EnumDataMode.Disabled)

            dtFromDate.Value = DateAdd(DateInterval.Day, -7, Now)
            dtToDate.Value = Now

            ' Fill Combos Of Financial Year, Company nd Voucher Type ..   
            FillCombos()

            SetConfigurationBaseSetting()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub

#Region "Report Interface Metholds .. "

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
    End Sub

    Public Sub ApplySecurity(ByVal Mode As Utility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

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

        Try
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


            cmbVoucherType.DataSource = ObjDataTable.Copy

            cmbVoucherType.DisplayMember = "VoucherType"
            cmbVoucherType.ValueMember = "TypeID"
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

        Try
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

            strSQL = strSQL + " WHERE   " & strYearCriteria & strLocationCriteria & "  "


            If chkUnPostedVoucher.Checked = False Then strSQL = strSQL & " (tblGlVoucher.post = 1) AND "
            If chkOtherVoucher.Checked = False Then strSQL = strSQL + "  (dbo.tblGlVoucher.Other_Voucher = 0) AND "

            ' Adding Dates Criteria .. 
            strSQL = strSQL + "  voucher_date between '" & Format(dtFromDate.Value.Date, "yyyy-MM-dd") & "' And '" & Format(dtToDate.Value.Date, "yyyy-MM-dd") & "'"

            If cmbVoucherType.SelectedIndex > 0 Then
                strSQL = strSQL + " and voucher_type_id = " & cmbVoucherType.SelectedValue

            End If
            ' CR # 338
            If chkVoucherNoWise.Checked = True Then
                strSQL = strSQL + " and voucher_no between  " & txtVoucherNoFrom.Text & " AND " & txtVoucherNoTo.Text
            End If
            ' CR # 338 End

            strSQL = strSQL + " Order by dbo.tblGlVoucher.voucher_date, dbo.tblGlVoucher.voucher_no "

            UtilityDAL.ExecuteQuery(strSQL)

            Dim ObjDAL As New DAL.GLVoucherDAL
            If ObjDAL.InsertDataForReport() Then
            Else
            End If

            Return ""

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Function

    Public Sub FunAddReportPramaters() Implements IReportsInterface.FunAddReportPramaters

        Try

            'CR# 42
            If Me.chkStandardVoucherPrint.Checked = True Then
                ' Calling A Function Which Will Created View And Insert Data In Attached Table Of Grid ..
                funaddStddReportCriteria()


                Dim objHashTableParamter As New Hashtable

                '''Report Name
                If DAL.SystemConfigurationDAL.GetSystemConfigurationValue("voucher_report_size") = "Long" Then
                    objHashTableParamter.Add("ReportPath", "\rptGlVoucherMulti.rpt")
                Else
                    objHashTableParamter.Add("ReportPath", "\rptGlVoucherMulti_Short.rpt")
                End If


                Dim ObjCompanyData As DataTable
                ObjCompanyData = UtilityDAL.setCompanyInfo(gobjLocationInfo.CompanyID)

                ' Passing Parameters .. (Report Parameters .. )
                objHashTableParamter.Add("companyname", ObjCompanyData.Rows(0).Item("CompanyName"))

                objHashTableParamter.Add("address", ObjCompanyData.Rows(0).Item("CompanyAddress"))

                If Me.cmbVoucherType.Text = "CPV" Or Me.cmbVoucherType.Text = "BPV" Then
                    objHashTableParamter.Add("ShowReceivedBy", True)
                Else
                    objHashTableParamter.Add("ShowReceivedBy", False)
                End If


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


                '' Adding Description Parameter .. 
                'If cmbVoucherType.SelectedIndex > 0 Then
                '    objHashTableParamter.Add("description", "(" & cmbVoucherType.Text & "  Vouchers) From " & Format(dtFromDate.Value.Date, "dd-MMM-yyyy") & " To  " & Format(dtToDate.Value.Date, "dd-MMM-yyyy"))

                'Else
                '    objHashTableParamter.Add("description", "(All Vouchers) From " & Format(dtFromDate.Value.Date, "dd-MMM-yyyy") & " To  " & Format(dtToDate.Value.Date, "dd-MMM-yyyy"))

                'End If


                '' Adding Location Parameter .. 
                'If cmbCompany.SelectedIndex > 0 Then
                '    objHashTableParamter.Add("Location", cmbCompany.Text)
                'Else
                '    objHashTableParamter.Add("Location", "ALL")

                'End If


                gObjMyAppHashTable.Add(EnumHashTableKeyConstants.SetReportParametersList, objHashTableParamter)

            Else


                ' Calling A Function Which Will Created View And Insert Data In Attached Table Of Grid ..
                FunAddReportCriteria()



                Dim objHashTableParamter As New Hashtable

                ' Giving Report Name .. 
                objHashTableParamter.Add("ReportPath", "\rptGlVoucher.rpt")




                If cmbCompany.SelectedIndex > 0 Then
                    Dim ObjCompanyData As DataTable
                    ObjCompanyData = UtilityDAL.setCompanyInfo(cmbCompany.SelectedValue)

                    ' Passing Parameters .. (Report Parameters .. )
                    objHashTableParamter.Add("companyname", ObjCompanyData.Rows(0).Item("CompanyName"))
                    objHashTableParamter.Add("address", ObjCompanyData.Rows(0).Item("CompanyAddress"))
                Else
                    ' Passing Parameters .. (Report Parameters .. )
                    objHashTableParamter.Add("companyname", DAL.SystemConfigurationDAL.GetSystemConfigurationValue("CompanyName").ToString)
                    objHashTableParamter.Add("address", DAL.SystemConfigurationDAL.GetSystemConfigurationValue("CompanyAddress").ToString)

                End If



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

            End If

         

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub

#End Region

    ' Click Event Of Generate Button .. 
    Private Sub btnGenerateButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try

            If dtFromDate.Value.Date > dtToDate.Value.Date Then
                ShowValidationMessage("FromDate should be less than ToDate")
                dtFromDate.Focus()
                Exit Sub

            End If
            ' CR # 338

            If chkVoucherNoWise.Checked = True Then
                If txtVoucherNoFrom.Text = String.Empty Then
                    ShowValidationMessage("Enter from voucher number")
                    txtVoucherNoFrom.Focus()
                    Exit Sub
                ElseIf txtVoucherNoTo.Text = String.Empty Then
                    ShowValidationMessage("Enter To voucher number")
                    txtVoucherNoTo.Focus()
                    Exit Sub
                End If

            End If
            ' CR # 338 End


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

    ' Selection Change Event Of Fincial Year .. 
    Private Sub cmbFinancialYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFinancialYear.SelectedIndexChanged
        Try
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

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

        
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Public Function funaddStddReportCriteria() As String

        Try

            Dim strYearCriteria As String = ""
            Dim strLocationCriteria As String = ""
            Dim strSQL As String = ""

            Dim objRptDal As New DALReports
            Dim DT As DataTable
            DT = New DataTable

            strSQL = "select * from dbo.sysobjects where id = object_id(N'[dbo].[vwGLVoucherMulti]') and OBJECTPROPERTY(id, N'IsView') = 1"

            DT = objRptDal.GetDataTable(strSQL)


            If DT.Rows.Count < 1 Then


                ' Checking If Financial Year Is Selected .. Then Add Its Criteria .. 
                If cmbFinancialYear.SelectedIndex > 0 Then
                    strYearCriteria = " (dbo.tblGlVoucher.finiancial_year_id = " & cmbFinancialYear.SelectedValue & " )   AND "

                End If

                ' Checking If Company Is Selected .. Then Add Its Criteria .. 
                If cmbCompany.SelectedIndex > 0 Then
                    strLocationCriteria = " (dbo.tblGlVoucher.location_id = " & cmbCompany.SelectedValue & " ) AND  "

                End If

                ' Building View .. 
                strSQL = " Create view vwGLVoucherMulti as SELECT dbo.tblGlVoucher.voucher_code, dbo.tblGlVoucher.voucher_type_id, dbo.tblGlVoucher.voucher_month, dbo.tblGlVoucher.voucher_date, " & _
                          " dbo.tblGlVoucherDetail.comments, dbo.tblGlVoucherDetail.debit_amount, dbo.tblGlVoucherDetail.credit_amount, dbo.tblGlVoucher.voucher_no, " & _
                          " dbo.tblGlDefFinancialYear.year_code,dbo.tblGlDefVoucherType.voucher_type, dbo.tblGlDefLocation.location_name, " & _
                          " dbo.tblGlCOAMainSubSubDetail.detail_code, dbo.tblGlCOAMainSubSubDetail.detail_title, dbo.tblGlVoucher.location_id, " & _
                          " dbo.tblGlVoucher.voucher_id AS voucherid, dbo.tblGlDefGLCostCenter.cost_center_title, dbo.tblGlVoucher.cheque_no, dbo.tblGlVoucher.cheque_date, " & _
                          " dbo.tblGlVoucher.cheque_paid, dbo.tblGlVoucher.cheque_paid_date " & _
                          " FROM dbo.tblGlVoucher INNER JOIN " & _
                          " dbo.tblGlVoucherDetail ON dbo.tblGlVoucher.voucher_id = dbo.tblGlVoucherDetail.voucher_id AND " & _
                          " dbo.tblGlVoucher.location_id = dbo.tblGlVoucherDetail.location_id INNER JOIN " & _
                          " dbo.tblGlDefFinancialYear ON dbo.tblGlVoucher.finiancial_year_id = dbo.tblGlDefFinancialYear.financial_year_id INNER JOIN " & _
                          " dbo.tblGlDefVoucherType ON dbo.tblGlVoucher.voucher_type_id = dbo.tblGlDefVoucherType.voucher_type_id INNER JOIN " & _
                          " dbo.tblGlDefLocation ON dbo.tblGlVoucher.location_id = dbo.tblGlDefLocation.location_id INNER JOIN " & _
                          " dbo.tblGlCOAMainSubSubDetail ON dbo.tblGlVoucherDetail.coa_detail_id = dbo.tblGlCOAMainSubSubDetail.coa_detail_id LEFT OUTER JOIN " & _
                          " dbo.tblGlDefGLCostCenter ON dbo.tblGlVoucherDetail.cost_center_id = dbo.tblGlDefGLCostCenter.cost_center_id"

                strSQL = strSQL + " WHERE   " & strYearCriteria & strLocationCriteria & "  "

                If chkUnPostedVoucher.Checked = False Then strSQL = strSQL & " (dbo.tblGlVoucher.post = 1) AND "
                If chkOtherVoucher.Checked = False Then strSQL = strSQL + "  (dbo.tblGlVoucher.Other_Voucher = 0) AND "

                ' Adding Dates Criteria .. 
                strSQL = strSQL + "  dbo.tblGlVoucher.voucher_date  between '" & Format(dtFromDate.Value.Date, "yyyy-MM-dd") & "' And '" & Format(dtToDate.Value.Date, "yyyy-MM-dd") & "'"

                If cmbVoucherType.SelectedIndex > 0 Then
                    strSQL = strSQL + " and dbo.tblGlVoucher.voucher_type_id= " & cmbVoucherType.SelectedValue

                End If
                ' CR # 338
                If chkVoucherNoWise.Checked = True Then
                    strSQL = strSQL + " and voucher_no between  " & txtVoucherNoFrom.Text & " AND " & txtVoucherNoTo.Text
                End If
                ' CR # 338 End


                ''strSQL = strSQL + " Order by dbo.tblGlVoucher.voucher_date, dbo.tblGlVoucher.voucher_no "

            Else

                ' Checking If Financial Year Is Selected .. Then Add Its Criteria .. 
                If cmbFinancialYear.SelectedIndex > 0 Then
                    strYearCriteria = " (dbo.tblGlVoucher.finiancial_year_id = " & cmbFinancialYear.SelectedValue & " )   AND "

                End If

                ' Checking If Company Is Selected .. Then Add Its Criteria .. 
                If cmbCompany.SelectedIndex > 0 Then
                    strLocationCriteria = " (dbo.tblGlVoucher.location_id = " & cmbCompany.SelectedValue & " ) AND  "

                End If

                ' Building View .. 
                strSQL = " Alter view vwGLVoucherMulti as SELECT dbo.tblGlVoucher.voucher_code, dbo.tblGlVoucher.voucher_type_id, dbo.tblGlVoucher.voucher_month, dbo.tblGlVoucher.voucher_date, " & _
                          " dbo.tblGlVoucherDetail.comments, dbo.tblGlVoucherDetail.debit_amount, dbo.tblGlVoucherDetail.credit_amount, dbo.tblGlVoucher.voucher_no, " & _
                          " dbo.tblGlDefFinancialYear.year_code,dbo.tblGlDefVoucherType.voucher_type, dbo.tblGlDefLocation.location_name, " & _
                          " dbo.tblGlCOAMainSubSubDetail.detail_code, dbo.tblGlCOAMainSubSubDetail.detail_title, dbo.tblGlVoucher.location_id, " & _
                          " dbo.tblGlVoucher.voucher_id AS voucherid, dbo.tblGlDefGLCostCenter.cost_center_title, dbo.tblGlVoucher.cheque_no, dbo.tblGlVoucher.cheque_date, " & _
                          " dbo.tblGlVoucher.cheque_paid, dbo.tblGlVoucher.cheque_paid_date " & _
                          " FROM dbo.tblGlVoucher INNER JOIN " & _
                          " dbo.tblGlVoucherDetail ON dbo.tblGlVoucher.voucher_id = dbo.tblGlVoucherDetail.voucher_id AND " & _
                          " dbo.tblGlVoucher.location_id = dbo.tblGlVoucherDetail.location_id INNER JOIN " & _
                          " dbo.tblGlDefFinancialYear ON dbo.tblGlVoucher.finiancial_year_id = dbo.tblGlDefFinancialYear.financial_year_id INNER JOIN " & _
                          " dbo.tblGlDefVoucherType ON dbo.tblGlVoucher.voucher_type_id = dbo.tblGlDefVoucherType.voucher_type_id INNER JOIN " & _
                          " dbo.tblGlDefLocation ON dbo.tblGlVoucher.location_id = dbo.tblGlDefLocation.location_id INNER JOIN " & _
                          " dbo.tblGlCOAMainSubSubDetail ON dbo.tblGlVoucherDetail.coa_detail_id = dbo.tblGlCOAMainSubSubDetail.coa_detail_id LEFT OUTER JOIN " & _
                          " dbo.tblGlDefGLCostCenter ON dbo.tblGlVoucherDetail.cost_center_id = dbo.tblGlDefGLCostCenter.cost_center_id"

                strSQL = strSQL + " WHERE   " & strYearCriteria & strLocationCriteria & "  "

                If chkUnPostedVoucher.Checked = False Then strSQL = strSQL & " (dbo.tblGlVoucher.post = 1) AND "
                If chkOtherVoucher.Checked = False Then strSQL = strSQL + "  (dbo.tblGlVoucher.Other_Voucher = 0) AND "

                ' Adding Dates Criteria .. 
                strSQL = strSQL + "  dbo.tblGlVoucher.voucher_date  between '" & Format(dtFromDate.Value.Date, "yyyy-MM-dd") & "' And '" & Format(dtToDate.Value.Date, "yyyy-MM-dd") & "'"

                If cmbVoucherType.SelectedIndex > 0 Then
                    strSQL = strSQL + " and dbo.tblGlVoucher.voucher_type_id= " & cmbVoucherType.SelectedValue

                End If
                ' CR # 338
                If chkVoucherNoWise.Checked = True Then
                    strSQL = strSQL + " and voucher_no between  " & txtVoucherNoFrom.Text & " AND " & txtVoucherNoTo.Text
                End If
                ' CR # 338 End


                ''strSQL = strSQL + " Order by dbo.tblGlVoucher.voucher_date, dbo.tblGlVoucher.voucher_no "

            End If
           

            UtilityDAL.ExecuteQuery(strSQL)

            Return ""

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Function

    Private Sub chkVoucherNoWise_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkVoucherNoWise.CheckedChanged
        ' CR # 338
        If chkVoucherNoWise.Checked = True Then
            txtVoucherNoFrom.Enabled = True
            txtVoucherNoTo.Enabled = True
        Else
            txtVoucherNoFrom.Enabled = False
            txtVoucherNoTo.Enabled = False
            txtVoucherNoFrom.Clear()
            txtVoucherNoTo.Clear()
        End If

    End Sub
    ' CR # 338
    Private Sub txtVoucherNoFrom_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtVoucherNoFrom.KeyPress
        Try

            If IsNumeric(e.KeyChar) Or Asc(e.KeyChar).ToString = 8 Or Asc(e.KeyChar).ToString = 46 Then
            Else
                e.Handled = True
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ' CR # 338
    Private Sub txtVoucherNoTo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtVoucherNoTo.KeyPress
        Try

            If IsNumeric(e.KeyChar) Or Asc(e.KeyChar).ToString = 8 Or Asc(e.KeyChar).ToString = 46 Then
            Else
                e.Handled = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class