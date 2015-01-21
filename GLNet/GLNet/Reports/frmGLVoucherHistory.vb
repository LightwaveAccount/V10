'Cr#245  27-may-2013                      by Fatima Tajammal  Make a new report for Voucher History
'CR#257  16-sep-2013                      by Fatima Tajammal  CTRL+P should work for printing the crystal reports

Imports DAL
Imports Model
Imports System.Collections.Specialized
Imports System.Data
Imports Utility.Utility
Public Class frmGLVoucherHistory
    Implements IGeneral, IReportsInterface

    Dim mobjControlList As NameValueCollection


    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    'CR # 245
    Public Sub ApplySecurity(ByVal Mode As Utility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If mobjControlList.Item("btnPrint") Is Nothing Then
                Me.btnprint.Enabled = False
            Else
                Me.btnprint.Enabled = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim DTVType As DataTable

            'dvChargeToList = GetFilterDataFromDataTable(CType(gObjMyAppHashTable(EnumHashTableKeyConstants.GetChargeToList.ToString), DataTable), "")
            DTVType = New VoucherDAL().GetVoucherType()
            Me.cboVoucherType.ValueMember = "VType ID"
            Me.cboVoucherType.DisplayMember = "VType"
            Me.cboVoucherType.DataSource = DTVType
            Me.cboVoucherType.SelectedIndex = 0
        Catch ex As Exception

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

            Me.btnprint.ImageList = gobjMyImageListForOperationBar
            Me.btnprint.ImageKey = "Print"

            Me.btnExit.ImageList = gobjMyImageListForOperationBar
            Me.btnExit.ImageKey = "Exit"

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As Utility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update

    End Function

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        Try
            If dtpVoucherFromDate.Value.Date > dtpVoucherToDate.Value.Date Then
                ShowValidationMessage("FromDate should be less than ToDate")
                dtpVoucherFromDate.Focus()
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

    Public Function FunAddReportCriteria() As String Implements IReportsInterface.FunAddReportCriteria

    End Function

    Public Sub FunAddReportPramaters() Implements IReportsInterface.FunAddReportPramaters
        Try
            Dim ObjCompanyData As DataTable
            ObjCompanyData = UtilityDAL.setCompanyInfo(gobjLocationInfo.CompanyID)
            Dim objHashTableParamter As New Hashtable
            objHashTableParamter.Add("ReportPath", "\rptGLVoucherHistory.rpt")
            objHashTableParamter.Add("CompanyName", ObjCompanyData.Rows(0).Item("CompanyName").ToString())
            objHashTableParamter.Add("Address", ObjCompanyData.Rows(0).Item("CompanyAddress").ToString())
            If Me.txtVoucherNo.Text <> String.Empty Then
                objHashTableParamter.Add("@voucherNo", Me.txtVoucherNo.Text)
            Else
                objHashTableParamter.Add("@voucherNo", 0)
            End If
            objHashTableParamter.Add("@VoucherFromDate", Me.dtpVoucherFromDate.Value.ToString("yyyy-MM-dd") & " 01:01:01 PM")
            objHashTableParamter.Add("@VoucherToDate", Me.dtpVoucherToDate.Value.ToString("yyyy-MM-dd") & " 11:59:59 PM ")
            objHashTableParamter.Add("@VoucherType", Me.cboVoucherType.SelectedValue)

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
            gObjMyAppHashTable.Add(EnumHashTableKeyConstants.SetReportParametersList, objHashTableParamter)


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'CR # 257
    Private Sub frmGLVoucherHistory_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.Control And e.KeyCode = Keys.X Then
                If Me.btnExit.Enabled = True Then Me.Close()
            ElseIf e.Control And e.KeyCode = Keys.P Then
                If Me.btnprint.Enabled = True Then btnprint_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmGLVoucherHistory_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            mobjControlList = GetFormSecurityControls(Me.Name)
            Me.SetButtonImages()
            Me.FillCombos()
            Me.ApplySecurity(EnumDataMode.ReadOnly)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class