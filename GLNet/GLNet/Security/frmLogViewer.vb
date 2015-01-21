''/////////////////////////////////////////////////////////////////////////////////////////
''//                      GL Net
''/////////////////////////////////////////////////////////////////////////////////////////
''//-------------------------------------------------------------------------------------
''// File Name       : frmLogViewer.vb           				                            
''// Programmer	     : Rizwan Asif
''// Creation Date	 : 21-Jul-2009
''// Description     : 
''// Function List   : 								                                    
''//											                                            
''//-------------------------------------------------------------------------------------
''// Date Modified     Modified by         Brief Description			                
''// 07-Dec-2011       Asif Kamal          Activity Log: transaction should be logged against proper user
''//------------------------------------------------------------------------------------
''/////////////////////////////////////////////////////////////////////////////////////////
Imports DAL
Imports Model
Imports System.Collections.Specialized
Imports System.Data
Imports Utility.Utility
Imports Microsoft.VisualBasic
Public Class frmLogViewer
    Implements IGeneral
#Region "Variables"
    ''This collection will hold the controls' names, upon which the logged in user has rights
    Private mobjControlList As NameValueCollection
    Private strQry As System.Text.StringBuilder
#End Region

    ''This is the representation of grid columns, and will be used while referring grid values, 
    ''instead of giving hard coded column's indexes
    Private Enum EnumGrid
        LogDate
        UserName
        FormCaption
        FormAction
        LogRef
        RetType
        LogGroup
    End Enum


#Region "Interface Methods"

    ''This will set the images of the buttons at runtime
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

            Me.btnExit.ImageList = gobjMyImageListForOperationBar
            Me.btnExit.ImageKey = "Exit"

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    ''Here will will use this function to fill-up all Combos and Listboxes on the form
    ''Optional condition would be used to fill-up combo or Listbox; which based on the selection of some other combo.
    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            'filling Software Users
            strQry = New System.Text.StringBuilder
            strQry.AppendLine("select user_name,user_id from tblGLsecurityuser order by user_name")
            Dim dt As DataTable = UtilityDAL.GetDataTable(strQry.ToString)
            Me.lstUsers.ListItem.DisplayMember = "user_name"
            Me.lstUsers.ListItem.ValueMember = "user_id"
            Me.lstUsers.ListItem.DataSource = dt
            Me.lstUsers.ListItem.SelectedIndex = 0

            'filling Groups
            Me.cmbGroups.Items.Add(gstrComboZeroIndexString)
            Me.cmbGroups.Items.Add("Definition")
            Me.cmbGroups.Items.Add("Transactions")
            Me.cmbGroups.SelectedIndex = 0

            ''filling Form Actions
            Dim strActions() As String = System.Enum.GetNames(GetType(EnumActions))
            Me.cmbFormAction.Items.Add(gstrComboZeroIndexString)
            If strActions.Length > 0 Then
                For Each str As String In strActions
                    Me.cmbFormAction.Items.Add(str)
                Next
            End If

            Me.cmbFormAction.SelectedIndex = 0

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            strQry = Nothing
        End Try
    End Sub

    ''Here we will use this procedure to load all master records; respective to the screen.
    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords


    End Sub


    ''This procedure will be used to set the formatting of the grid on that form. For Example, Grid's columns show/Hide,
    '' Caption setting, columns' width etc.
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
           
            Me.grdAllRecords.AutoSizeColumns()
            Me.UiCtrlGridBar1.MyGrid = Me.grdAllRecords
            Me.grdAllRecords.TotalRow = Janus.Windows.GridEX.InheritableBoolean.False

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try


    End Sub

    ''This procedure will be used (if applicable) to set Active/Deactive or Visible/Invisible some controls on form,
    ''which are based on System level configuration
    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    ''This procedure will be used to set the navigation buttons as per Mode
    Public Sub SetNavigationButtons(ByVal mode As EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons
        Try

            If mode = EnumDataMode.[New] Then
                ''if New Mode then Set Disable all Navigation Buttons
                btnFirst.Enabled = False
                btnPrevious.Enabled = False
                btnNext.Enabled = False
                btnLast.Enabled = False

            ElseIf mode = EnumDataMode.Edit Then
                ''if New Mode then Set Enable all Navigation Buttons
                btnFirst.Enabled = True
                btnPrevious.Enabled = True
                btnNext.Enabled = True
                btnLast.Enabled = True

            ElseIf mode = EnumDataMode.Disabled Then
                ''if New Mode then Set Enable all Navigation Buttons
                btnFirst.Enabled = False
                btnPrevious.Enabled = False
                btnNext.Enabled = False
                btnLast.Enabled = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''here we will clear all the contols of the screen for New Mode
    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.dtpFrom.Value = Microsoft.VisualBasic.DateAdd(DateInterval.Year, -1, Date.Today)
            Me.dtpTo.Value = Date.Today
            Me.dtpFrom.Checked = False
            Me.dtpTo.Checked = False
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ''Here we will pass an argument MODE (New|Edit|Disabled), which will be overwritten according to the rights 
    ''available to user on that screen
    Public Sub ApplySecurity(ByVal Mode As Utility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            btnNew.Enabled = False
            btnSave.Enabled = False
            btnUpdate.Enabled = False
            btnDelete.Enabled = False
            btnCancel.Enabled = False
            SetNavigationButtons(EnumDataMode.Disabled)
            Me.grdAllRecords.Enabled = True


            If mobjControlList.Item("btnGenerateReport") Is Nothing Then
                Me.btnGenerateReport.Enabled = False
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

    ''Here we will apply Front End Validations.
    Public Function IsValidate(Optional ByVal Mode As EnumDataMode = EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            
            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''Here we will create an instance of the class, according to the form, and will set the properties of the object
    ''Later this object will be refered in Save|Update|Delete function.
    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel


    End Sub

    ''Here we will call DAL Function for SAVE, and if the function successfully Saves the records
    ''then the function will return True, otherwise returns False
    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save

    End Function

    ''Here we will call DAL Function for Update the selected record, and if the function successfully Updates the records
    ''then the function will return True, otherwise returns False
    Public Function Update(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update
    End Function

    ''Here we will call DAL Function for Delete the selected record, and if the function successfully Deletes the records
    ''then the function will return True, otherwise returns False
    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function


#End Region

#Region "Local Functions and Procedures"


    Private Sub ShowResult()

        Try
            
            Dim strqry As New System.Text.StringBuilder

            If Not Me.IsValidate Then Exit Sub
            Me.Cursor = Cursors.WaitCursor

            strqry.AppendLine("SELECT tblGLActivityLog.action_date [Log Date], tblGLSecurityUser.user_name [User Name], tblGLActivityLog.form_caption [Form Caption], tblGLActivityLog.form_action [Form Action], ")
            strqry.AppendLine(" tblGLActivityLog.log_ref [Log Ref.] , tblGLActivityLog.ref_type [Ref. Type], tblGLActivityLog.Log_group [Log Group]")
            strqry.AppendLine(" FROM tblGLActivityLog INNER JOIN tblGLSecurityUser ON tblGLActivityLog.user_id = tblGLSecurityUser.User_id")
            strqry.AppendLine(" Where 1=1 ")

            If Me.cmbGroups.SelectedIndex > 0 Then
                strqry.AppendLine(" AND ( tblGLactivitylog.log_group ='" & Me.cmbGroups.Text & "')")
            End If

            If Me.cmbFormAction.SelectedIndex > 0 Then
                strqry.AppendLine(" AND (tblGLactivitylog.form_action='" & Me.cmbFormAction.Text & "')")
            End If

            'CR#159     Asif Kamal      Activity Log: transaction should be logged against proper user
            If Me.dtpFrom.Checked Then
                strqry.AppendLine(" AND (CONVERT(datetime,LEFT(tblGLactivitylog.action_date,11),102) >=  '" & dtpFrom.Value & "' AND CONVERT(datetime,LEFT(tblGLactivitylog.action_date,11),102) <= '" & dtpTo.Value & "')")

            End If

            If lstUsers.SelectedIDs > 0 Then
                strqry.AppendLine("  AND (tblGLactivitylog.user_id in(" & lstUsers.SelectedIDs & "))  ")
            End If

            Dim dt As DataTable = UtilityDAL.GetDataTable(strqry.ToString)
            Me.grdAllRecords.DataSource = Nothing
            Me.grdAllRecords.DataSource = dt
            Me.grdAllRecords.RetrieveStructure()

            ''apply grid stule sheet
            Me.ApplyGridSettings()

            ''setting text in grid bar for report heading
            ' Me.UiCtrlGridBar1.txtGridTitle.Text = ""
        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

#End Region

#Region "Form Controls Events"

    Private Sub frmShopSalesReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ''Getting all available controls list to the user on this screen; in a collection 
            mobjControlList = GetFormSecurityControls(Me.Name)

            ''Assing Images to Buttons
            Me.SetButtonImages()

            ''filling the combos
            Me.FillCombos()

            ''setting the default dates 
            Me.ReSetControls()

            ''apply seciryt rights
            Me.ApplySecurity(EnumDataMode.Disabled)

            Me.dtpFrom.Value = Date.Today.AddMonths(-1)
            Me.dtpFrom.Checked = False
            Me.dtpTo.Value = Date.Today
            Me.dtpTo.Checked = False
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub frmDefCity_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Control And e.KeyCode = Keys.X Then
                If Me.btnExit.Enabled = True Then Me.Close()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub



    Private Sub btnGenerateReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateReport.Click
        Try
            Me.ShowResult()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdAllRecords_LowdingRow(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowLoadEventArgs)
        Try
            If e.Row.RowType = Janus.Windows.GridEX.RowType.TotalRow Then

                ''to view the Total Records in Grid Footer
                Dim dv As DataView = GetFilterDataFromDataTable(CType(gObjMyAppHashTable.Item(EnumHashTableKeyConstants.GetLanguageBasedControlList.ToString()), DataTable), "[Control Type]= 'DataDictionary'  AND [Control Name] = 'GridRowCount'")
                Dim strTotalRecords As String = String.Empty
                If Not dv Is Nothing Then
                    If Not dv.Count = 0 Then
                        strTotalRecords = dv.Item(0).Item(dv.Table.Columns(gstrSystemLanguage).ColumnName)
                    End If
                End If
                e.Row.Cells(EnumGrid.LogDate).Text = strTotalRecords & " (" & Me.grdAllRecords.GetTotal(Me.grdAllRecords.RootTable.Columns(EnumGrid.LogDate), Janus.Windows.GridEX.AggregateFunction.Count) & ") "
            ElseIf e.Row.RowType = Janus.Windows.GridEX.RowType.Record Then

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub dtpFrom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFrom.ValueChanged, dtpTo.ValueChanged
        Dim dtp As DateTimePicker = CType(sender, DateTimePicker)
        Me.dtpTo.Checked = dtp.Checked
        Me.dtpFrom.Checked = dtp.Checked
    End Sub

#End Region


   

End Class