''/////////////////////////////////////////////////////////////////////////////////////////
''//                      Candela New
''/////////////////////////////////////////////////////////////////////////////////////////
''//-------------------------------------------------------------------------------------
''// File Name       : GL Notes .. 
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

Public Class frmGLNotesReport
    Implements IReportsInterface, IGeneral

    Private Enum GridCol
        colGLNoteID = 0
        colNoteNo = 1
        colNoteTitle = 2
        colNoteType = 3

    End Enum

    Private mobjControlList As NameValueCollection

#Region "Report Interface Metholds .. "


    Public Function FunAddReportCriteria() As String Implements IReportsInterface.FunAddReportCriteria
    End Function

    Public Sub FunAddReportPramaters() Implements IReportsInterface.FunAddReportPramaters

        Try

            Dim objHashTableParamter As New Hashtable

            ' Giving Report Name .. 
            objHashTableParamter.Add("ReportPath", "Reports\rptChartofAccounts.rpt")



            Dim ObjCompanyData As DataTable
            ObjCompanyData = UtilityDAL.setCompanyInfo(gobjLocationInfo.CompanyID)



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

    ' Click Event Of Button 1 .. 
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

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

    Private Sub frmGLNotesReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try

            If e.Control And e.KeyCode = Keys.X Then
                If Me.btnExit.Enabled = True Then Me.Close()
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ' Form Load Event .. 
    Private Sub frmChartOfAccount_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            mobjControlList = GetFormSecurityControls(Me.Name)

            ''Assing Images to Buttons
            Me.SetButtonImages()

            ApplySecurity(EnumDataMode.Disabled)

            ' Load GL Notes In Grid Control .. 
            loadGlNotes()

            ' Applies Grid Settings .. 
            ApplyStyleSheet(Me, Me.Name)

            ' Setting Title To Grid Bar .. 
            grdBar.txtGridTitle.Text = "GL Notes"

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub

    Sub loadGlNotes()

        Try
            Dim strSQL As String
            strSQL = "SELECT gl_note_id, note_no, note_title, note_type From TblGlDefGlNotes ORDER BY sort_order "

            Dim ObjGLNotes As DataTable = UtilityDAL.GetDataTable(strSQL)
            grdGlNotes.DataSource = ObjGLNotes
            grdGlNotes.RetrieveStructure()

            ApplyGridSettings()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub

#Region "IGeneral Interface"

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

        Try

            ' Giving Captions .. 
            grdGlNotes.RootTable.Columns(GridCol.colGLNoteID).Caption = "Gl Note ID"
            grdGlNotes.RootTable.Columns(GridCol.colNoteNo).Caption = "Note #"
            grdGlNotes.RootTable.Columns(GridCol.colNoteTitle).Caption = "Note Title"
            grdGlNotes.RootTable.Columns(GridCol.colNoteType).Caption = "Note Types"

            ' Hiding Columns .. 
            grdGlNotes.RootTable.Columns(GridCol.colGLNoteID).Visible = False

            ' Totals Of Columns .. 
            ' grdGlNotes.RootTable.Columns(GridCol.colNoteNo).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Count

            ' Auto Fit .. 
            grdGlNotes.AutoSizeColumns()

            ' Hiding Total Row .. 
            grdGlNotes.TotalRow = Janus.Windows.GridEX.InheritableBoolean.False

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

                'If mobjControlList.Item("btnPrint") Is Nothing Then
                btnPrint.Enabled = False
                'Else
                ' btnPrint.Enabled = True
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
                btnCancel.Enabled = True ': btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue

                SetNavigationButtons(Mode)

                'Me.grdAllRecords.Enabled = False

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

                'Me.grdAllRecords.Enabled = True

                'Me.grdAllRecords.Focus()

            ElseIf Mode.ToString = EnumDataMode.ReadOnly.ToString Then

                btnNew.Enabled = True ': btnNew.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                btnSave.Enabled = False ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System


                btnUpdate.Enabled = False ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnDelete.Enabled = False ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnCancel.Enabled = False ': btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.System

                SetNavigationButtons(Mode)

                'Me.grdAllRecords.Enabled = True

                'Me.grdAllRecords.Focus()

            End If


            ' Disabl/Enable the Button that converts Grid data in Excel Sheet According to Login User rights
            If mobjControlList.Item("btnExport") Is Nothing Then
                Me.grdBar.btnExport.Enabled = False
            End If


            '' Disabl/Enable the Button that Prints Grid data According to Login User rights
            If mobjControlList.Item("btnPrint") Is Nothing Then
                Me.grdBar.btnPrint.Enabled = False
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
    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update
    End Function

#End Region
    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

End Class