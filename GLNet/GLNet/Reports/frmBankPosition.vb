Imports DAL
Imports Model
Imports System.Collections.Specialized
Imports System.Data
Imports Utility.Utility
''// 20 Dec,2011       Abdul Jabbar       CR#169:Bank Ledger in Bank Position and Bank Reconcilation doesn't match with Ledger report (change in DAL)
''// 06-june-2013      Fatima Tajmamal    CR#250:Trial Balance ANd Bank POstion Date Formate
Public Class frmBankPosition
    Implements IGeneral, IReportsInterface

#Region "Variables"
    ''This collection will hold the controls' names, upon which the logged in user has rights
    Private mobjControlList As NameValueCollection
#End Region

#Region "Enumerations"

    ''This is the representation of grid columns, and will be used while referring grid values, 
    ''instead of giving hard coded column's indexes
    Private Enum EnumGridBankPosition
        BankCode = 0
        BankName = 1
        LedgerBalance = 2
        UnPresented = 3
        UnCredited = 4
        BankBalance = 5
    End Enum

#End Region

#Region "Interface Methods"
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            grdAllRecords.RootTable.Columns(EnumGridBankPosition.BankCode).Caption = "Bank Code"
            grdAllRecords.RootTable.Columns(EnumGridBankPosition.BankName).Caption = "Bank Name"
            grdAllRecords.RootTable.Columns(EnumGridBankPosition.LedgerBalance).Caption = "Ledger Balance"
            grdAllRecords.RootTable.Columns(EnumGridBankPosition.UnPresented).Caption = "UnPresented"
            grdAllRecords.RootTable.Columns(EnumGridBankPosition.UnCredited).Caption = "UnCredited"
            grdAllRecords.RootTable.Columns(EnumGridBankPosition.BankBalance).Caption = "Bank Balance"

            grdAllRecords.RootTable.Columns(EnumGridBankPosition.BankCode).Visible = False

            grdAllRecords.RootTable.Columns(EnumGridBankPosition.LedgerBalance).FormatString = "n"
            grdAllRecords.RootTable.Columns(EnumGridBankPosition.UnPresented).FormatString = "n"
            grdAllRecords.RootTable.Columns(EnumGridBankPosition.UnCredited).FormatString = "n"
            grdAllRecords.RootTable.Columns(EnumGridBankPosition.BankBalance).FormatString = "n"


            grdAllRecords.RootTable.Columns(EnumGridBankPosition.LedgerBalance).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            grdAllRecords.RootTable.Columns(EnumGridBankPosition.UnPresented).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            grdAllRecords.RootTable.Columns(EnumGridBankPosition.UnCredited).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            grdAllRecords.RootTable.Columns(EnumGridBankPosition.BankBalance).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum


            ' Formatting Columns .. 
            grdAllRecords.RootTable.Columns(EnumGridBankPosition.LedgerBalance).TotalFormatString = "n"
            grdAllRecords.RootTable.Columns(EnumGridBankPosition.UnPresented).TotalFormatString = "n"
            grdAllRecords.RootTable.Columns(EnumGridBankPosition.UnCredited).TotalFormatString = "n"
            grdAllRecords.RootTable.Columns(EnumGridBankPosition.BankBalance).TotalFormatString = "n"


            For Each col As Janus.Windows.GridEX.GridEXColumn In grdAllRecords.RootTable.Columns
                col.AutoSize()
            Next

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
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
                Me.grdAllRecords.Enabled = True

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

                Me.grdAllRecords.Enabled = False

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

                Me.grdAllRecords.Enabled = True

                Me.grdAllRecords.Focus()

            ElseIf Mode.ToString = EnumDataMode.ReadOnly.ToString Then

                btnNew.Enabled = True ': btnNew.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                btnSave.Enabled = False ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System


                btnUpdate.Enabled = False ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnDelete.Enabled = False ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnCancel.Enabled = False ': btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.System

                SetNavigationButtons(Mode)

                Me.grdAllRecords.Enabled = True

                Me.grdAllRecords.Focus()

            End If


            '' Disabl/Enable the Button that converts Grid data in Excel Sheet According to Login User rights
            If mobjControlList.Item("btnExport") Is Nothing Then
                Me.UiCtrlGridBar1.btnExport.Enabled = False
            End If


            '' Disabl/Enable the Button that Prints Grid data According to Login User rights
            If mobjControlList.Item("btnPrint") Is Nothing Then
                Me.UiCtrlGridBar1.btnPrint.Enabled = False
            End If


        Catch ex As Exception
            Throw ex
        End Try
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

    Public Function Update(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update

    End Function
#End Region

#Region "Report Interface Methods"
    Public Function FunAddReportCriteria() As String Implements IReportsInterface.FunAddReportCriteria

    End Function

    Public Sub FunAddReportPramaters() Implements IReportsInterface.FunAddReportPramaters
        Try
            ' Calling A Function Which Will Created View And Insert Data In Attached Table Of Grid ..
            FunAddReportCriteria()

            Dim objHashTableParamter As New Hashtable

            ' Giving Report Name .. 
            objHashTableParamter.Add("ReportPath", "\rptBankPosition.rpt")

            Dim ObjCompanyData As DataTable
            ObjCompanyData = UtilityDAL.setCompanyInfo(gobjLocationInfo.CompanyID)

            ' Passing Parameters .. (Report Parameters .. )
            objHashTableParamter.Add("companyname", ObjCompanyData.Rows(0).Item("CompanyName"))
            objHashTableParamter.Add("address", ObjCompanyData.Rows(0).Item("CompanyAddress"))
            objHashTableParamter.Add("prdate", Format(dtpSearch.Value.Date, "dd/MMM/yyyy"))

            objHashTableParamter.Add("PrintRights", "True")
            objHashTableParamter.Add("ExportRights", "True")

            gObjMyAppHashTable.Add(EnumHashTableKeyConstants.SetReportParametersList, objHashTableParamter)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
#End Region

#Region "Form Control Events"

    Private Sub frmBankPosition_InputLanguageChanging(ByVal sender As Object, ByVal e As System.Windows.Forms.InputLanguageChangingEventArgs) Handles Me.InputLanguageChanging
        e.Cancel = True
    End Sub

    Private Sub frmBankPosition_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub frmBankPosition_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ''Getting all available controls list to the user on this screen; in a collection 
            mobjControlList = GetFormSecurityControls(Me.Name)

            ''Assing Images to Buttons
            Me.SetButtonImages()

            ApplySecurity(EnumDataMode.Disabled)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnGenerateReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerateReport.Click
        Try
            Dim dt As New DataTable
            dt = New BankPositionDAL().DisplayReport(dtpSearch.Value.Date)

            grdAllRecords.DataSource = dt
            grdAllRecords.RetrieveStructure()
            ApplyGridSettings()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            If New BankPositionDAL().GoForSelectionCriteria(dtpSearch.Value) Then

                Call FunAddReportPramaters()

                Dim rptViewer As New rptViewer
                rptViewer.Text = Me.Text
                rptViewer.Show()

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
#End Region

End Class