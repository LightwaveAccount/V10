''/////////////////////////////////////////////////////////////////////////////////////////
''//                      GL Net
''/////////////////////////////////////////////////////////////////////////////////////////
''//-------------------------------------------------------------------------------------
''// File Name       : frmGroupRights.vb           				                            
''// Programmer	     : Rizwan Asif
''// Creation Date	 : 17-Jul-2009
''// Description     : 
''//                   
''// Function List   : 								                                    
''//											                                            
''//-------------------------------------------------------------------------------------
''// Date Modified     Modified by         Brief Description			                
''//------------------------------------------------------------------------------------
''//08-dec-2011       Fatima Tajammal       CR#164 Lightwave Logviewer form is not working properly, need to find out and fix issues
''//11-sep-2013       Fatima Tajammal       CR#243 Group Rights: CTRL+S is not working
''//16-sep-2013       Fatima Tajammal       CR#261 Group Rights: There should be a filter to search screen
''/////////////////////////////////////////////////////////////////////////////////////////

Imports DAL
Imports Model
Imports System.Collections.Specialized
Imports System.Data
Imports Utility.Utility
Public Class FrmGroupRights
    Implements IGeneral


#Region "Variables"
    ''This collection will hold the controls' names, upon which the logged in user has rights
    Private mobjControlList As NameValueCollection
    ''This is the model object which will be set with data values and refered for Save|Update|Delete|Loading Record in Edit Mode
    Private mobjModel As List(Of SecurityGroupRights)
    Private intPkId As Integer
    Private int1stLvl As Integer = 0
#End Region

#Region "Enumerations"
    ''This is the representation of grid columns, and will be used while referring grid values, 
    ''instead of giving hard coded column's indexes
    Private Enum EnumGrid
        ControlID = 0
        FormLabel = 1
        ControlCaption = 2
        IsSelected = 3
    End Enum
#End Region

#Region "Interface Methods"

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

        Try
            ''Columns with In-visible setting
            Me.grdAllRecords.RootTable.Columns(EnumGrid.ControlID).FormatString = ""
            Me.grdAllRecords.RootTable.Columns(EnumGrid.ControlID).Visible = False

            ' ''Set columns widths for visible columns
            Me.grdAllRecords.RootTable.Columns(EnumGrid.ControlCaption).Width = 350
            Me.grdAllRecords.RootTable.Columns(EnumGrid.IsSelected).Width = 75


            Me.grdAllRecords.TotalRowFormatStyle.BackColor = gobjRequiredFieldtBKColor
            Me.grdAllRecords.GroupByBoxVisible = False
            Me.grdAllRecords.HideColumnsWhenGrouped = Janus.Windows.GridEX.InheritableBoolean.True

            ''define groups
            Dim grpFormLabel As New Janus.Windows.GridEX.GridEXGroup(Me.grdAllRecords.RootTable.Columns(EnumGrid.FormLabel))
            grpFormLabel.GroupPrefix = String.Empty
            Me.grdAllRecords.RootTable.Groups.Add(grpFormLabel)

            grpFormLabel.Collapse()

            Me.grdAllRecords.GroupIndent = 50

            Me.grdAllRecords.TotalRow = Janus.Windows.GridEX.InheritableBoolean.False
            Me.grdAllRecords.EmptyRows = False

            Me.grdAllRecords.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grdAllRecords.RootTable.Columns(EnumGrid.FormLabel).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdAllRecords.RootTable.Columns(EnumGrid.ControlCaption).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdAllRecords.RootTable.Columns(EnumGrid.ControlID).EditType = Janus.Windows.GridEX.EditType.NoEdit

            If Me.grdAllRecords.RowCount > 0 Then Me.grdAllRecords.Row = 0

            'CR # 261
            Me.grdAllRecords.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
            Me.grdAllRecords.RootTable.Columns(EnumGrid.IsSelected).FilterEditType = Janus.Windows.GridEX.FilterEditType.SameAsEditType
            Me.grdAllRecords.FilterRow.Cells(EnumGrid.ControlCaption).Text = "Search here ......"
            Me.grdAllRecords.RootTable.Columns(EnumGrid.ControlCaption).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(ByVal Mode As Utility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
        Try

            Me.cboFormGroups.Enabled = True

            If mobjControlList.Item("btnSave") Is Nothing Then
                btnSave.Enabled = False
            Else
                btnSave.Enabled = True
            End If

           
            Me.btnNew.Enabled = False
            Me.btnUpdate.Enabled = False
            Me.btnDelete.Enabled = False
            Me.SetNavigationButtons(EnumDataMode.Disabled)


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

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try

            ''filling Group  combo
            Dim strSQL As String = "select group_name, group_id from tblGLSecurityGroup order by  group_name"
            Dim dt As DataTable = DAL.UtilityDAL.GetDataTable(strSQL)
            'Dim dr As DataRow = dt.NewRow
            'dr.Item(0) = gstrComboZeroIndexString
            'dr.Item(1) = 0
            'dt.Rows.InsertAt(dr, 0)
            Me.cboFormGroups.DisplayMember = "group_name"
            Me.cboFormGroups.ValueMember = "group_id"
            Me.cboFormGroups.DataSource = dt
           
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            ''Create Model object
            mobjModel = New List(Of SecurityGroupRights)

            Dim dt As DataTable = CType(Me.grdAllRecords.DataSource, DataTable)
            If dt Is Nothing Then Exit Sub
            If dt.Rows.Count > 0 Then
                Dim gr As SecurityGroupRights
                For Each r As DataRow In dt.Rows
                    gr = New SecurityGroupRights
                    gr.GroupInfo = New SecurityGroup
                    gr.GroupInfo.GroupID = Me.cboFormGroups.SelectedValue
                    gr.IsSelected = r.Item(EnumGrid.IsSelected)
                    gr.ControlID = r.Item(EnumGrid.ControlID)
                    mobjModel.Add(gr)
                Next

                'filing activity log
                'CR#164 
                mobjModel(0).ActivityLog = New ActivityLog()
                mobjModel(0).ActivityLog.ScreenTitle = Me.Text
                mobjModel(0).ActivityLog.LogGroup = "Security"
                mobjModel(0).ActivityLog.ShopID = 0
                mobjModel(0).ActivityLog.UserID = gObjUserInfo.UserID
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try

            Me.grdAllRecords.DataSource = Nothing

            If Me.cboFormGroups.SelectedIndex < 0 Then Exit Sub
            ''Getting Datasource for Grid from DAL
            Dim dt As DataTable = New GroupRightsDAL().GetAll(Convert.ToInt32(cboFormGroups.SelectedValue))

            Me.grdAllRecords.DataSource = dt
            Me.grdAllRecords.RetrieveStructure()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As Utility.Utility.EnumDataMode = Utility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            Me.FillModel()
            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try

            Me.intPkId = 0
            Me.int1stLvl = 0
            Me.cboFormGroups.Focus()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try

            'Cr # 243
            Me.grdAllRecords.UpdateData()

            ''Applying Front End Validation Checks
            If Me.IsValidate(EnumDataMode.[New]) Then

                Dim result As DialogResult = Windows.Forms.DialogResult.Yes

                If gblnShowSaveConfirmationMessages Then
                    ''Getting Save Confirmation from User
                    result = ShowConfirmationMessage("Are you sure you want to Save changes?", MessageBoxDefaultButton.Button1)
                End If

                If result = Windows.Forms.DialogResult.Yes Then

                    ''Create a DAL Object and calls its Add Method by passing Model Object

                    If New GroupRightsDAL().Update(Me.mobjModel) Then

                        ''Query to Database and get fressh modifications in the Grid
                        Me.GetAllRecords()
                        Me.ApplyGridSettings()

                        ''Reset controls and set New Mode
                        Me.ReSetControls()

                    End If

                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
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
                btnFirst.Enabled = False
                btnPrevious.Enabled = False
                btnNext.Enabled = False
                btnLast.Enabled = False

            ElseIf Mode = EnumDataMode.Edit Then
                ''if New Mode then Set Enable all Navigation Buttons
                btnFirst.Enabled = True
                btnPrevious.Enabled = True
                btnNext.Enabled = True
                btnLast.Enabled = True

            ElseIf Mode = EnumDataMode.Disabled Then
                ''if New Mode then Set Enable all Navigation Buttons
                btnFirst.Enabled = False
                btnPrevious.Enabled = False
                btnNext.Enabled = False
                btnLast.Enabled = False '
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update

    End Function

#End Region

#Region "Local Functions and Procedures"


    Private Sub RestGrid()
        Try
            Me.GetAllRecords()
            Me.ApplyGridSettings()
            Me.ReSetControls()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


#End Region

#Region "Form Controls Events"



    Private Sub frmGroupRights_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            ''Getting all available controls list to the user on this screen; in a collection 
            mobjControlList = GetFormSecurityControls(Me.Name)

            Me.ApplySecurity(EnumDataMode.[New])

            ''Assing Images to Buttons
            Me.SetButtonImages()

            Call FillCombos()

            Me.ReSetControls()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmDefArea_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.Control And e.KeyCode = Keys.S Then
                If Me.btnSave.Enabled = True Then Me.Save()
            ElseIf e.Control And e.KeyCode = Keys.U Then
                'If Me.btnUpdate.Enabled = True Then Me.Update()
            ElseIf e.Control And e.KeyCode = Keys.D Then
                ' If Me.btnDelete.Enabled = True Then Me.Delete()
            ElseIf e.Control And e.KeyCode = Keys.N Then
                'If Me.btnNew.Enabled = True Then Me.ReSetControls()
            ElseIf e.Control And e.KeyCode = Keys.E Then
                If Me.btnCancel.Enabled = True Then Me.RestGrid()
            ElseIf e.Control And e.KeyCode = Keys.X Then
                If Me.btnExit.Enabled = True Then Me.Close()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cboFormGroups_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboFormGroups.SelectedIndexChanged
        Try
            Me.GetAllRecords()
            Me.ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Cr # 261
    Private Sub chkSaveAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSaveAll.CheckedChanged, chkUpdateAll.CheckedChanged, chkDeleteAll.CheckedChanged, chkViewAll.CheckedChanged, chkPrintAll.CheckedChanged, chkExportAll.CheckedChanged, chkOther.CheckedChanged
        Try
            Dim chk As CheckBox = CType(sender, CheckBox)
            Me.grdAllRecords.RootTable.Groups(0).Expand()
            Me.int1stLvl = 1

            Select Case chk.Name
                Case Me.chkSaveAll.Name
                    Dim enumrator As IEnumerator = CType(Me.grdAllRecords.DataSource, DataTable).DefaultView.GetEnumerator
                    While enumrator.MoveNext
                        Dim r As DataRowView = CType(enumrator.Current, DataRowView)
                        If r.Item(EnumGrid.ControlCaption).ToString = "Save" Then
                            r.Item(EnumGrid.IsSelected) = Convert.ToByte(Me.chkSaveAll.Checked)
                        End If
                    End While


                Case Me.chkUpdateAll.Name

                    Dim enumrator As IEnumerator = CType(Me.grdAllRecords.DataSource, DataTable).DefaultView.GetEnumerator
                    While enumrator.MoveNext
                        Dim r As DataRowView = CType(enumrator.Current, DataRowView)
                        If r.Item(EnumGrid.ControlCaption).ToString = "Update" Then
                            r.Item(EnumGrid.IsSelected) = Convert.ToByte(Me.chkUpdateAll.Checked)
                        End If
                    End While


                Case Me.chkDeleteAll.Name

                    Dim enumrator As IEnumerator = CType(Me.grdAllRecords.DataSource, DataTable).DefaultView.GetEnumerator
                    While enumrator.MoveNext
                        Dim r As DataRowView = CType(enumrator.Current, DataRowView)
                        If r.Item(EnumGrid.ControlCaption).ToString = "Delete" Then
                            r.Item(EnumGrid.IsSelected) = Convert.ToByte(Me.chkDeleteAll.Checked)
                        End If
                    End While

                Case Me.chkViewAll.Name

                    Dim enumrator As IEnumerator = CType(Me.grdAllRecords.DataSource, DataTable).DefaultView.GetEnumerator
                    While enumrator.MoveNext
                        Dim r As DataRowView = CType(enumrator.Current, DataRowView)
                        If r.Item(EnumGrid.ControlCaption).ToString = "View" Then
                            r.Item(EnumGrid.IsSelected) = Convert.ToByte(Me.chkViewAll.Checked)
                        End If
                    End While

                Case Me.chkPrintAll.Name

                    Dim enumrator As IEnumerator = CType(Me.grdAllRecords.DataSource, DataTable).DefaultView.GetEnumerator
                    While enumrator.MoveNext
                        Dim r As DataRowView = CType(enumrator.Current, DataRowView)
                        If r.Item(EnumGrid.ControlCaption).ToString = "Print" Then
                            r.Item(EnumGrid.IsSelected) = Convert.ToByte(Me.chkPrintAll.Checked)
                        End If
                    End While

                Case Me.chkExportAll.Name
                    Dim enumrator As IEnumerator = CType(Me.grdAllRecords.DataSource, DataTable).DefaultView.GetEnumerator
                    While enumrator.MoveNext
                        Dim r As DataRowView = CType(enumrator.Current, DataRowView)

                        'CR # 261
                        'If r.Item(EnumGrid.ControlCaption).ToString = "Export To Excel" Then
                        If r.Item(EnumGrid.ControlCaption).ToString = "Export To Excel" Or r.Item(EnumGrid.ControlCaption).ToString = "Export" Then
                            r.Item(EnumGrid.IsSelected) = Convert.ToByte(Me.chkExportAll.Checked)
                        End If
                    End While

                    'Cr # 261
                Case Me.chkOther.Name
                    Dim enumrator As IEnumerator = CType(Me.grdAllRecords.DataSource, DataTable).DefaultView.GetEnumerator
                    While enumrator.MoveNext
                        Dim r As DataRowView = CType(enumrator.Current, DataRowView)
                        If r.Item(EnumGrid.ControlCaption).ToString <> "Save" AndAlso r.Item(EnumGrid.ControlCaption).ToString <> "Update" AndAlso r.Item(EnumGrid.ControlCaption).ToString <> "Delete" AndAlso r.Item(EnumGrid.ControlCaption).ToString <> "View" AndAlso r.Item(EnumGrid.ControlCaption).ToString <> "Print" AndAlso r.Item(EnumGrid.ControlCaption).ToString <> "Export To Excel" AndAlso r.Item(EnumGrid.ControlCaption).ToString <> "Export" Then
                            r.Item(EnumGrid.IsSelected) = Convert.ToByte(Me.chkOther.Checked)
                        End If
                    End While
            End Select

            Me.grdAllRecords.Refetch()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnFirst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFirst.Click, btnNew.Click, btnPrevious.Click, btnLast.Click

        Try

            Dim btn As Button = CType(sender, Button)

            ''If Move First is clicked, then navigate to first record of the grid
            If btn.Name = Me.btnFirst.Name Then
                Me.grdAllRecords.Row = 0

                ''If Move Previous is clicked, then navigate to Previous record of the grid
            ElseIf btn.Name = Me.btnPrevious.Name Then
                If Me.grdAllRecords.Row > 0 Then Me.grdAllRecords.Row = (Me.grdAllRecords.Row - 1)

                ''If Move Next is clicked, then navigate to next record of the grid
            ElseIf btn.Name = Me.btnNext.Name Then
                If Me.grdAllRecords.Row >= 0 Then Me.grdAllRecords.Row = (Me.grdAllRecords.Row + 1)


                ''If Move Last is clicked, then navigate to Last record of the grid
            ElseIf btn.Name = Me.btnLast.Name Then
                Me.grdAllRecords.Row = (Me.grdAllRecords.RowCount - 1)

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click, btnUpdate.Click, btnDelete.Click, btnCancel.Click, btnExit.Click, btnNew.Click, btnExpnd1stLevel.Click
        Try
            'Dim btn As C1.Win.C1Input.C1Button = CType(sender, C1.Win.C1Input.C1Button)
            Dim btn As Windows.Forms.Button = CType(sender, Windows.Forms.Button)

            If btn.Name = btnSave.Name Then
                '' Call Save method to save the record
                Me.Save()

            ElseIf btn.Name = btnCancel.Name Then
                Me.RestGrid()

            ElseIf btn.Name = btnExit.Name Then
                Me.Close()

            ElseIf btn.Name = Me.btnExpnd1stLevel.Name Then
                If Me.int1stLvl = 0 Then
                    Me.grdAllRecords.RootTable.Groups(0).Expand()
                    Me.int1stLvl = 1
                Else
                    Me.grdAllRecords.RootTable.Groups(0).Collapse()
                    Me.int1stLvl = 0
                End If
            End If



        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            mobjModel = Nothing
        End Try

    End Sub

#End Region

  
    
End Class
