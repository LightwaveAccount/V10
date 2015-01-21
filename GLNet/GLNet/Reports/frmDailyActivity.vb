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

Public Class frmDailyActivity
    Implements IReportsInterface

    Private Sub btnGenerateButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateButton.Click

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


#Region "Report Interface Metholds .. "

    Public Function FunAddReportCriteria() As String Implements IReportsInterface.FunAddReportCriteria

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


    End Function

    Public Sub FunAddReportPramaters() Implements IReportsInterface.FunAddReportPramaters

        Try

            ' Calling A Function Which Will Created View And Insert Data In Attached Table Of Grid ..
            FunAddReportCriteria()



            Dim objHashTableParamter As New Hashtable

            ' Giving Report Name .. 
            objHashTableParamter.Add("ReportPath", "Reports\rptDailyActivity.rpt")



            Dim ObjCompanyData As DataTable
            ObjCompanyData = UtilityDAL.setCompanyInfo("1")



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
            Throw ex

        End Try

    End Sub

#End Region

    ' Load Event Of Form .. 
    Private Sub frmDailyActivity_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.dtFromDate.Value = DateAdd(DateInterval.Day, -7, Now)
        Me.dtToDate.Value = Now

    End Sub
End Class