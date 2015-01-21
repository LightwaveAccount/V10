''/////////////////////////////////////////////////////////////////////////////////////////
''//                      Candela New
''/////////////////////////////////////////////////////////////////////////////////////////
''//-------------------------------------------------------------------------------------
''// File Name       : Main Sub Sub Account Mapping .. 
''// Programmer	     : Tariq Majeed
''// Creation Date	 : 16-July-2009
''// Description     : 
''// Function List   : 								                                    
''//											                                            
''//-------------------------------------------------------------------------------------
''// Date Modified     Modified by         Brief Description			                
''//------------------------------------------------------------------------------------
''//16-sep-2013         Fatima Tajammal     CR # 257    CTRL+P should work for printing the crystal reports
''/////////////////////////////////////////////////////////////////////////////////////////


Imports DAL
Imports Model
Imports System.Collections.Specialized
Imports System.Data
Imports Utility.Utility
Imports Microsoft.VisualBasic

Public Class frmMainSubSubAccountMappingReport
    Implements IGeneral
    Dim mobjControlList As NameValueCollection


    Private Enum GridCol
        colMainType = 0
        colMainTitle = 1
        colSubTitle = 2
        colSubSubTitle = 3
        colAccountType = 4
        colDrBSNotes = 5
        colCrBSNotes = 6
        colPLNotes = 7

    End Enum

    Private Sub frmMainSubSubAccountMappingReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Control And e.KeyCode = Keys.X Then
            If Me.btnExit.Enabled = True Then Me.Close()
        End If
    End Sub


    ' Load Event .. 
    Private Sub frmMainSubSubAccountMapping_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ' Getting all available controls list to the user on this screen; in a collection ..
            mobjControlList = GetFormSecurityControls(Me.Name)

            ''Assing Images to Buttons
            Me.SetButtonImages()

            ' Load Accounts Details In Grid Control .. 
            loadAccounts()

            ' Applies Grid Settings .. 
            ApplyStyleSheet(Me, Me.Name)

            ' Setting Title To Grid Bar .. 
            grdBar.txtGridTitle.Text = "Main Sub Sub Account Mapping"

            ' Apply Security . 
            ApplySecurity(EnumDataMode.Disabled)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

        
    End Sub

    Sub loadAccounts()
        Try
            Dim strSQL As String
            strSQL = " SELECT tblGlCOAMain.main_type, tblGlCOAMain.main_title + '-' + tblGlCOAMain.main_code AS Main_Title, " _
                   & " tblGlCOAMainSub.sub_title + '-' + tblGlCOAMainSub.sub_code AS  Sub_Title, " _
                   & " tblGlCOAMainSubSub.sub_sub_title AS Sub_Sub_Title, " _
                   & " tblGlCOAMainSubSub.account_type AS Account_Type, " _
                   & " DrBSNote.note_title AS DrBSNote, CrBSNote.note_title AS CrBSNote, PLNote.note_title AS PLNote " _
                   & " FROM tblGlCOAMainSub INNER JOIN tblGlCOAMainSubSub ON tblGlCOAMainSub.main_sub_id = tblGlCOAMainSubSub.main_sub_id " _
                   & " Inner Join tblGlCOAMain ON tblGlCOAMainSub.coa_main_id = tblGlCOAMain.coa_main_id LEFT Outer Join " _
                   & " tblGlDefGLNotes DrBSNote ON DrBSNote.gl_note_id = tblGlCOAMainSubSub.DrBS_note_id LEFT " _
                   & " Outer Join tblGlDefGLNotes CrBSNote ON CrBSNote.gl_note_id = tblGlCOAMainSubSub.CrBS_note_id LEFT " _
                   & " Outer Join tblGlDefGLNotes PLNote ON PLNote.gl_note_id = tblGlCOAMainSubSub.PL_note_id " _
                   & " ORDER BY tblGlCOAMainSubSub.sub_sub_code "

            Dim ObjGLNotes As DataTable = UtilityDAL.GetDataTable(strSQL)
            grdAccountMapping.DataSource = ObjGLNotes
            grdAccountMapping.RetrieveStructure()

            ApplyGridSettings()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

      

    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub


#Region "IGeneral Interface"

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            ' Giving Captions .. 
            grdAccountMapping.RootTable.Columns(GridCol.colMainType).Caption = "Main Type"
            grdAccountMapping.RootTable.Columns(GridCol.colMainTitle).Caption = "Main Title"
            grdAccountMapping.RootTable.Columns(GridCol.colSubTitle).Caption = "Sub Title"
            grdAccountMapping.RootTable.Columns(GridCol.colSubSubTitle).Caption = "Sub Sub Title"
            grdAccountMapping.RootTable.Columns(GridCol.colAccountType).Caption = "Account Type"
            grdAccountMapping.RootTable.Columns(GridCol.colDrBSNotes).Caption = "Dr BS Note"
            grdAccountMapping.RootTable.Columns(GridCol.colCrBSNotes).Caption = "Cr BS Note"
            grdAccountMapping.RootTable.Columns(GridCol.colPLNotes).Caption = "PL Note"

            ' Totals Of Columns .. 
            ' grdAccountMapping.RootTable.Columns(GridCol.colMainType).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Count
            '

            ' Auto Fit Columns .. 
            grdAccountMapping.AutoSizeColumns()

            ' Hide Total Row ..
            grdAccountMapping.TotalRow = Janus.Windows.GridEX.InheritableBoolean.False

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

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

   
End Class