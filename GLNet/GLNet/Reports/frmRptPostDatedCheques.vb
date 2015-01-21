Imports DAL
Imports Model
Imports System.Collections.Specialized
Imports System.Data
Imports Utility.Utility

Public Class frmRptPostDatedCheques
    Implements IGeneral, IReportsInterface

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

            dt = New PostDatedChequesDAL().GetCompanies(gObjUserInfo.UserID)
            dr = dt.NewRow
            dr("location_id") = 0
            dr("Location") = gstrComboZeroIndexString
            dt.Rows.InsertAt(dr, 0)

            cboCompany.DisplayMember = "Location"
            cboCompany.ValueMember = "location_id"
            cboCompany.DataSource = dt

            dt = Nothing

            If Me.cboCompany.Items.Count > 1 Then
                Me.cboCompany.SelectedValue = gobjLocationInfo.CompanyID

            End If

            dt = New PostDatedChequesDAL().GetBanks()
            dr = dt.NewRow
            dr("COA Detail ID") = 0
            dr("Bank Name") = gstrComboZeroIndexString
            dt.Rows.InsertAt(dr, 0)

            cboBank.DisplayMember = "Bank Name"
            cboBank.ValueMember = "COA Detail ID"
            cboBank.DataSource = dt

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

    Public Function Update(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update

    End Function
#End Region

#Region "Reports Interface Methods"
    Public Function FunAddReportCriteria() As String Implements IReportsInterface.FunAddReportCriteria

    End Function

    Public Sub FunAddReportPramaters() Implements IReportsInterface.FunAddReportPramaters
        Try
            ' Calling A Function Which Will Created View And Insert Data In Attached Table Of Grid ..
            FunAddReportCriteria()

            Dim objHashTableParamter As New Hashtable

            ' Giving Report Name .. 
            objHashTableParamter.Add("ReportPath", "\rptPostDatedCheques.rpt")

            ' Passing Parameters .. (Report Parameters .. )
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


            objHashTableParamter.Add("@FromDate", Me.dtpFrom.Value.Date.ToString("yyyyMMdd"))
            objHashTableParamter.Add("@ToDate", Me.dtpTo.Value.Date.ToString("yyyyMMdd"))
            objHashTableParamter.Add("@Post", IIf(chkIncludeUnPostedVouchers.Checked, 1, 0))
            objHashTableParamter.Add("@Other_voucher", IIf(optOtherVouchers.Checked = True, "1", IIf(optSTDVouchers.Checked = True, "0", "-1")))
            objHashTableParamter.Add("@Bank_ID", cboBank.SelectedValue)
            objHashTableParamter.Add("@Location_ID", cboCompany.SelectedValue)

            If optBPV.Checked Then
                objHashTableParamter.Add("@ChequeType", "BPV")
                objHashTableParamter.Add("mReportTitle", "Bank Payment Post Dated Cheques")
            Else
                objHashTableParamter.Add("@ChequeType", "BRV")
                objHashTableParamter.Add("mReportTitle", "Bank Receipts Post Dated Cheques")
            End If

            objHashTableParamter.Add("PrintRights", "True")
            objHashTableParamter.Add("ExportRights", "True")

            gObjMyAppHashTable.Add(EnumHashTableKeyConstants.SetReportParametersList, objHashTableParamter)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
#End Region

#Region "Form Control Events"

    Private Sub frmRptPostDatedCheques_InputLanguageChanging(ByVal sender As Object, ByVal e As System.Windows.Forms.InputLanguageChangingEventArgs) Handles Me.InputLanguageChanging
        e.Cancel = True
    End Sub

    Private Sub frmRptPostDatedCheques_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

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

    Private Sub frmRptPostDatedCheques_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim FromDate As DateTime
            Dim ToDate As DateTime

            FromDate = DateTime.Now
            'dtpFrom.MinDate = FromDate.AddDays(1)
            'dtpTo.MinDate = FromDate.AddDays(1)
            dtpFrom.Value = FromDate.AddDays(1)
            ToDate = FromDate.AddMonths(1).AddDays(1)
            dtpTo.Value = ToDate

            FillCombos()

            SetButtonImages()

            ApplySecurity(EnumDataMode.Disabled)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click

        Try
            'If cboBank.SelectedIndex = 0 Then
            '    ShowValidationMessage("Please Select Bank")
            '    cboBank.Focus()
            '    Exit Sub

            'End If

            Call FunAddReportPramaters()

            Dim rptViewer As New rptViewer
            rptViewer.Text = Me.Text
            rptViewer.Show()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub
#End Region

End Class