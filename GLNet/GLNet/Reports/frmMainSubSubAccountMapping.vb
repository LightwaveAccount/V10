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
''/////////////////////////////////////////////////////////////////////////////////////////


Imports DAL
Imports Model
Imports System.Collections.Specialized
Imports System.Data
Imports Utility.Utility
Imports Microsoft.VisualBasic

Public Class frmMainSubSubAccountMapping
    Implements IGeneral

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



    Private Sub frmMainSubSubAccountMapping_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ' Load Accounts Details In Grid Control .. 
        loadAccounts()

        ' Applies Grid Settings .. 
        ApplyStyleSheet(Me, Me.Name)

        ' Setting Title To Grid Bar .. 
        grdBar.txtGridTitle.Text = "Main Sub Sub Account Mapping"

    End Sub

    Sub loadAccounts()

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

    End Sub




#Region "IGeneral Interface"

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

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
        grdAccountMapping.RootTable.Columns(GridCol.colMainType).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Count

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