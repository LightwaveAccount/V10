Imports DAL
Imports Model
Imports System.Collections.Specialized
Imports System.Data
Imports Utility.Utility
Imports Microsoft.VisualBasic

Public Class frmChartOfAccount

    Implements IReportsInterface, IGeneral

    Private Enum GridCol
        colGLNoteID = 0
        colNoteNo = 1
        colNoteTitle = 2
        colNoteType = 3
        colSortOrder = 4

    End Enum


#Region "Report Interface Metholds .. "


    Public Function FunAddReportCriteria() As String Implements IReportsInterface.FunAddReportCriteria
    End Function

    Public Sub FunAddReportPramaters() Implements IReportsInterface.FunAddReportPramaters

        Try

            Dim objHashTableParamter As New Hashtable

            ' Giving Report Name .. 
            objHashTableParamter.Add("ReportPath", "Reports\rptChartofAccounts.rpt")



            Dim ObjCompanyData As DataTable
            ObjCompanyData = UtilityDAL.setCompanyInfo("1")



            ' Passing Parameters .. (Report Parameters .. )
            objHashTableParamter.Add("companyname", ObjCompanyData.Rows(0).Item("CompanyName"))
            objHashTableParamter.Add("address", ObjCompanyData.Rows(0).Item("CompanyAddress"))



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

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Try

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


    Private Sub frmChartOfAccount_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ' Load GL Notes In Grid Control .. 
        loadGlNotes()

        ' Applies Grid Settings .. 
        ApplyStyleSheet(Me, Me.Name)

    End Sub

    Sub loadGlNotes()
        Dim strSQL As String
        strSQL = "SELECT gl_note_id, note_no, note_title, note_type, sort_order From TblGlDefGlNotes ORDER BY sort_order "

        Dim ObjGLNotes As DataTable = UtilityDAL.GetDataTable(strSQL)
        grdGlNotes.DataSource = ObjGLNotes
        grdGlNotes.RetrieveStructure()

        ApplyGridSettings()


    End Sub

#Region "IGeneral Interface"

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings


        ' Giving Captions .. 
        grdGlNotes.RootTable.Columns(GridCol.colGLNoteID).Caption = "Gl Note ID"
        grdGlNotes.RootTable.Columns(GridCol.colNoteNo).Caption = "Note #"
        grdGlNotes.RootTable.Columns(GridCol.colNoteTitle).Caption = "Note Title"
        grdGlNotes.RootTable.Columns(GridCol.colNoteType).Caption = "Note Types"
        grdGlNotes.RootTable.Columns(GridCol.colSortOrder).Caption = "Sort Order"

        ' Hiding Columns .. 
        grdGlNotes.RootTable.Columns(GridCol.colGLNoteID).Visible = False

        ' Totals Of Columns .. 
        grdGlNotes.RootTable.Columns(GridCol.colNoteNo).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Count

    End Sub

    Public Sub ApplySecurity(ByVal Mode As Utility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
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
    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting
    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As Utility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons
    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update
    End Function

#End Region

End Class