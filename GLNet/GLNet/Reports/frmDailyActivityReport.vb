''/////////////////////////////////////////////////////////////////////////////////////////
''//                      Candela New
''/////////////////////////////////////////////////////////////////////////////////////////
''//-------------------------------------------------------------------------------------
''// File Name       : Daily Activity Report .. 
''// Programmer	     : Tariq Majeed
''// Creation Date	 : 16-July-2009
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

Public Class frmDailyActivityReport
    Implements IReportsInterface, IGeneral

    Private mobjControlList As NameValueCollection
    ' Click Event Of Generate Button .. 
  
#Region "Interface Methods"

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(ByVal Mode As Utility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
        btnPrint.Enabled = True
        btnNew.Enabled = False ': btnNew.VisualStyle = C1.Win.C1Input.VisualStyle.System
        btnSave.Enabled = False ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System           
        btnUpdate.Enabled = False ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.System
        btnDelete.Enabled = False ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.System
        btnCancel.Enabled = False ': btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue

        btnFirst.Enabled = False
        btnLast.Enabled = False
        btnPrevious.Enabled = False
        btnNext.Enabled = False

        'Try


        '    If Mode.ToString = EnumDataMode.Disabled.ToString Then

        '        btnNew.Enabled = False ': btnNew.VisualStyle = C1.Win.C1Input.VisualStyle.System
        '        btnSave.Enabled = False ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System           
        '        btnUpdate.Enabled = False ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.System
        '        btnDelete.Enabled = False ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.System
        '        btnCancel.Enabled = False ': btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue

        '        If mobjControlList.Item("btnPrint") Is Nothing Then
        '            btnPrint.Enabled = False
        '        Else
        '            btnPrint.Enabled = True
        '        End If

        '        SetNavigationButtons(EnumDataMode.[New])
        '        ' Me.grdAllRecords.Enabled = True

        '    ElseIf Mode.ToString = EnumDataMode.[New].ToString Then

        '        btnNew.Enabled = False ': btnNew.VisualStyle = C1.Win.C1Input.VisualStyle.System

        '        If mobjControlList.Item("btnSave") Is Nothing Then
        '            btnSave.Enabled = False ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System
        '        Else
        '            btnSave.Enabled = True ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '        End If

        '        btnUpdate.Enabled = False ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.System
        '        btnDelete.Enabled = False ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.System
        '        btnCancel.Enabled = True ': btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue

        '        SetNavigationButtons(Mode)

        '        'Me.grdAllRecords.Enabled = False

        '    ElseIf Mode.ToString = EnumDataMode.Edit.ToString Then

        '        btnNew.Enabled = True ': btnNew.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '        btnSave.Enabled = False ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System

        '        If mobjControlList.Item("btnUpdate") Is Nothing Then
        '            btnUpdate.Enabled = False ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.System
        '        Else
        '            btnUpdate.Enabled = True ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '        End If

        '        If mobjControlList.Item("btnDelete") Is Nothing Then
        '            btnDelete.Enabled = False ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.System
        '        Else
        '            btnDelete.Enabled = True ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '        End If
        '        btnCancel.Enabled = False ': btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.System

        '        SetNavigationButtons(Mode)

        '        'Me.grdAllRecords.Enabled = True

        '        'Me.grdAllRecords.Focus()

        '    ElseIf Mode.ToString = EnumDataMode.ReadOnly.ToString Then

        '        btnNew.Enabled = True ': btnNew.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '        btnSave.Enabled = False ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System


        '        btnUpdate.Enabled = False ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.System
        '        btnDelete.Enabled = False ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.System
        '        btnCancel.Enabled = False ': btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.System

        '        SetNavigationButtons(Mode)

        '        'Me.grdAllRecords.Enabled = True

        '        'Me.grdAllRecords.Focus()

        '    End If


        '    '' Disabl/Enable the Button that converts Grid data in Excel Sheet According to Login User rights
        '    'If mobjControlList.Item("btnExport") Is Nothing Then
        '    '    Me.UiCtrlGridBar1.btnExport.Enabled = False
        '    'End If


        '    ' '' Disabl/Enable the Button that Prints Grid data According to Login User rights
        '    'If mobjControlList.Item("btnPrint") Is Nothing Then
        '    '    Me.UiCtrlGridBar1.btnPrint.Enabled = False
        '    'End If


        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos

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

    Public Function FunAddReportCriteria() As String Implements IReportsInterface.FunAddReportCriteria

        Try
            Dim strSQL As String
            Dim value1, value2 As Int16

            If optPosted.Checked = True Then
                value1 = 1
                value2 = 1

            ElseIf optUnPosted.Checked = True Then
                value1 = 0
                value2 = 0


            ElseIf optAll.Checked = True Then
                value1 = 1
                value2 = 0



            End If

            ' Building SQL ..
            strSQL = " Alter View vwDailyActivityReport as " _
                   & " SELECT TOP 100 PERCENT tblGlVoucher.voucher_code,tblGlDefVoucherType.voucher_type, tblGlVoucherDetail.debit_amount, " _
                   & " tblGlVoucherDetail.credit_amount, tblGlVoucher.voucher_month, tblGlDefFinancialYear.year_code,tblGlVoucher.voucher_date, " _
                   & " tblGlVoucherDetail.comments AS VDescription, tblGlCOAMainSubSubDetail.detail_title, tblGlCOAMainSubSubDetail.detail_code, " _
                   & " tblGlVoucherDetail.coa_detail_id , dbo.tblGlVoucher.post AS Status , dbo.tblGlDefLocation.location_code,   dbo.tblGlDefLocation.location_name " _
                   & " FROM tblGlCOAMainSubSub INNER JOIN " _
                   & " tblGlCOAMainSubSubDetail ON tblGlCOAMainSubSub.main_sub_sub_id = tblGlCOAMainSubSubDetail.main_sub_sub_id INNER JOIN " _
                   & " tblGlCOAMain INNER JOIN " _
                   & " tblGlCOAMainSub ON tblGlCOAMain.coa_main_id = tblGlCOAMainSub.coa_main_id ON " _
                   & " tblGlCOAMainSubSub.main_sub_id = tblGlCOAMainSub.main_sub_id INNER JOIN " _
                   & " tblGlVoucher INNER JOIN " _
                   & " tblGlVoucherDetail ON tblGlVoucher.voucher_id = tblGlVoucherDetail.voucher_id AND " _
                   & " tblGlVoucher.location_id = tblGlVoucherDetail.location_id INNER JOIN " _
                   & " tblGlDefFinancialYear ON tblGlVoucher.finiancial_year_id = tblGlDefFinancialYear.financial_year_id INNER JOIN " _
                   & " tblGlDefVoucherType ON tblGlVoucher.voucher_type_id = tblGlDefVoucherType.voucher_type_id ON " _
                   & " tblGlCOAMainSubSubDetail.coa_detail_id =tblGlVoucherDetail.coa_detail_id INNER JOIN                      dbo.tblGlDefLocation ON dbo.tblGlVoucher.location_id = dbo.tblGlDefLocation.location_id " _
                   & " Where (tblGlVoucher.voucher_date BETWEEN '" & Format(dtFromDate.Value.Date, "yyyy-MM-dd") & "' AND '" & Format(dtToDate.Value.Date, "yyyy-MM-dd") & "') And " _
                   & " ((tblGlVoucher.post= " & value1 & ") or (tblGlVoucher.post = " & value2 & " ))  ORDER BY tblGlVoucher.voucher_code "


            UtilityDAL.ExecuteQuery(strSQL)


            Dim ObjDAL As New DAL.ActivityReportDAL
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

            ' Calling A Function Which Will Created View And Insert Data In Attached Table Of Grid ..
            FunAddReportCriteria()



            Dim objHashTableParamter As New Hashtable

            ' Giving Report Name .. 
            objHashTableParamter.Add("ReportPath", "\rptDailyActivity.rpt")



            Dim ObjCompanyData As DataTable
            ObjCompanyData = UtilityDAL.setCompanyInfo(gobjLocationInfo.CompanyID)



            ' Passing Parameters .. (Report Parameters .. )
            objHashTableParamter.Add("companyname", ObjCompanyData.Rows(0).Item("CompanyName"))
            objHashTableParamter.Add("address", ObjCompanyData.Rows(0).Item("CompanyAddress"))
            objHashTableParamter.Add("fromdate", Format(dtFromDate.Value.Date, "dd/MMM/yyyy"))
            objHashTableParamter.Add("todate", Format(dtToDate.Value.Date, "dd/MMM/yyyy"))



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
            ShowErrorMessage(ex.Message)

        End Try

    End Sub

#End Region

    Private Sub frmDailyActivityReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
            mobjControlList = GetFormSecurityControls(Me.Name)

            ''Assing Images to Buttons
            Me.SetButtonImages()

            ApplySecurity(EnumDataMode.Disabled)

            Me.dtFromDate.Value = Now
            Me.dtToDate.Value = Now

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update

    End Function
    Private Sub btnGenerateButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click

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
    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnGenerateButton_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
End Class