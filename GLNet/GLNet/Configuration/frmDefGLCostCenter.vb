
''/////////////////////////////////////////////////////////////////////////////////////////
''//                     GL CONFIGURATION
''/////////////////////////////////////////////////////////////////////////////////////////
''//-------------------------------------------------------------------------------------
''// File Name       : frmDefGLCostCenter.vb           				                            
''// Programmer	     : R@! Shahid
''// Creation Date	 : July 20,2009
''// Description     : Cost Center defination screen.
''//-------------------------------------------------------------------------------------
''// Date Modified     Modified by         Brief Description			                
''//------------------------------------------------------------------------------------
''//08-July-2010      Abdul Jabbar        CR#41-Cost Center Title Field length has been increased to 50 char at Front End. at design
''//08-Dec-2011       Fatima Tajammal     CR # 164 Lightwave Logviewer form is not working properly, need to find out and fix issues
''//13-Dec-2011       Asif Kamal          CR#166     Lightwave Issues need to be fixed.
''/////////////////////////////////////////////////////////////////////////////////////////

Imports DAL
Imports Model
Imports System.Collections.Specialized
Imports System.Data
Imports Utility.Utility

Public Class frmDefGLCostCenter

    Implements IGeneral

#Region "Variables"
    ''This collection will hold the controls' names, upon which the logged in user has rights
    Private mobjControlList As NameValueCollection
    ''This is the model object which will be set with data values and refered for Save|Update|Delete|Loading Record in Edit Mode
    Private mobjModel As GLCostCenter
    Private intPkId As Integer


#End Region

#Region "Enumerations"

    ''This is the representation of grid columns, and will be used while referring grid values, 
    ''instead of giving hard coded column's indexes
    Private Enum EnumGridFYear
        CostCenterID = 0
        CostCenterTitle = 1
        CostCenterType = 2
    End Enum


#End Region

#Region "Interface Methods"

    ''This will set the images of the buttons at runtime
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

            Me.btnExit.ImageList = gobjMyImageListForOperationBar
            Me.btnExit.ImageKey = "Exit"

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    ''Here will will use this function to fill-up all Combos and Listboxes on the form
    ''Optional condition would be used to fill-up combo or Listbox; which based on the selection of some other combo.
    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    ''Here we will use this procedure to load all master records; respective to the screen.
    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Dim lngTotalRecords As Long

        Try

            ''Getting Datasource for Grid from DAL
            Dim dt As DataTable = New GLCostCenterDal().GetAll()
            Me.grdAllRecords.DataSource = dt
            Me.grdAllRecords.RetrieveStructure()

            lngTotalRecords = dt.Rows.Count

            ''Applying Grid Formatting Setting
            Me.ApplyGridSettings()

        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    ''This procedure will be used to set the formatting of the grid on that form. For Example, Grid's columns show/Hide,
    '' Caption setting, columns' width etc.
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

        Try

            ''Columns In-visible
            Me.grdAllRecords.RootTable.Columns(EnumGridFYear.CostCenterID).Visible = False

            ''Set columns widths for visible columns
            Me.grdAllRecords.RootTable.Columns(EnumGridFYear.CostCenterTitle).Width = 140
            Me.grdAllRecords.RootTable.Columns(EnumGridFYear.CostCenterType).Width = 180

            Me.grdAllRecords.TotalRowFormatStyle.BackColor = gobjRequiredFieldtBKColor

            'Stop Editing in Grid
            Me.grdAllRecords.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
            Me.grdAllRecords.RootTable.SortKeys.Clear()

            Me.grdAllRecords.TotalRow = Janus.Windows.GridEX.InheritableBoolean.False

        Catch ex As Exception
            Throw ex
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
                btnFirst.Enabled = False ': btnFirst.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnPrevious.Enabled = False ': btnPrevious.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnNext.Enabled = False ': btnNext.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnLast.Enabled = False ': btnLast.VisualStyle = C1.Win.C1Input.VisualStyle.System

            ElseIf mode = EnumDataMode.Edit Then
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

    ''here we will clear all the contols of the screen for New Mode
    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try

            Me.intPkId = 0
            Me.txtCostCenterTitle.Text = String.Empty
            Me.txtCostCenterType.Text = String.Empty
            ''Set New Mode and Applying Security Setting
            Call ApplySecurity(EnumDataMode.[New])
            Me.txtCostCenterTitle.Focus()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''Here we will pass an argument MODE (New|Edit|Disabled), which will be overwritten according to the rights 
    ''available to user on that screen
    Public Sub ApplySecurity(ByVal Mode As Utility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
        Try


            If Mode.ToString = EnumDataMode.Disabled.ToString Then

                btnNew.Enabled = False ': btnNew.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnSave.Enabled = False ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System           
                btnUpdate.Enabled = False ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnDelete.Enabled = False ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnCancel.Enabled = False ': btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue

                SetNavigationButtons(EnumDataMode.Edit)
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

    ''Here we will apply Front End Validations.
    Public Function IsValidate(Optional ByVal Mode As EnumDataMode = EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate


        Try

            ''1 First Check Front End Validations
            If Mode = EnumDataMode.[New] Or Mode = EnumDataMode.Edit Then
                ''Check Name is Required
                'CR#166     Asif Kamal      Lightwave Issues need to be fixed.
                If Me.txtCostCenterTitle.Text.Trim = String.Empty Then
                    ShowValidationMessage("Cost Center Title is required")
                    Me.txtCostCenterTitle.Focus()
                    Return False

                    ''Check Code is Required
                    'CR#166     Asif Kamal      Lightwave Issues need to be fixed.
                ElseIf Me.txtCostCenterType.Text.Trim = String.Empty Then
                    ShowValidationMessage("Cost Center Type is required")
                    Me.txtCostCenterType.Focus()
                    Return False
                End If
            End If
            ''===========================================   
            ''2 Database End Validations

            ''Fill Model with the front end values
            Me.FillModel()

            If Condition = "BackEndDeleteValidation" Then
                ''Check Dependancy existance
                Return New GLCostCenterDal().IsValidateForDelete(mobjModel)

            End If

            ''Check Name or Code Duplication
            Return New GLCostCenterDal().IsValidateForSave(mobjModel)






            Return True

        Catch ex As Exception
            Throw ex
        End Try



    End Function

    ''Here we will create an instance of the class, according to the form, and will set the properties of the object
    ''Later this object will be refered in Save|Update|Delete function.
    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel

        Try

            ''Create Model object
            mobjModel = New GLCostCenter
            With mobjModel
                .CostCenterId = intPkId
                .CostCenterTitle = Me.txtCostCenterTitle.Text
                .CostCenterType = Me.txtCostCenterType.Text

                'CR #164
                mobjModel.ActivityLog.ShopID = 0 'gObjUserInfo.ShopInfo.ShopID
                mobjModel.ActivityLog.ScreenTitle = Me.Text
                mobjModel.ActivityLog.LogGroup = "Definition"
                mobjModel.ActivityLog.UserID = gObjUserInfo.UserID
            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    ''Here we will call DAL Function for SAVE, and if the function successfully Saves the records
    ''then the function will return True, otherwise returns False
    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try

            ''Applying Front End Validation Checks
            If Me.IsValidate(EnumDataMode.[New]) Then

                Dim result As DialogResult = Windows.Forms.DialogResult.Yes

                'Confirmation from user for Creation of New FYear
                If gblnShowSaveConfirmationMessages Then
                    ''Getting Save Confirmation from User
                    result = ShowConfirmationMessage(gstrMsgSave, MessageBoxDefaultButton.Button1)
                End If

                If result = Windows.Forms.DialogResult.Yes Then

                    ''Create a DAL Object and calls its Add Method by passing Model Object
                    If New GLCostCenterDal().Add(Me.mobjModel) Then

                        '#To Do
                        'If pbFinancialYearID = 0 Or pbLocationID = 0 Then
                        '    MsgBox("You must logout before continue to work with LS-GL", vbInformation)
                        'End If

                        ''Query to Database and get fressh modifications in the Grid
                        Me.GetAllRecords()

                        'to select the last updated record
                        For Rind As Int16 = 0 To (grdAllRecords.RowCount - 1)
                            If Me.grdAllRecords.GetRow(Rind).Cells(EnumGridFYear.CostCenterID).Value = mobjModel.CostCenterId Then
                                Me.grdAllRecords.Row = Rind
                                Exit For
                            End If
                        Next

                        Me.GridEX1_SelectionChanged(Nothing, Nothing)

                        ''Reset controls and set New Mode
                        Me.ReSetControls()

                    End If

                End If

            End If

        Catch ex As Exception
            If ex.Message = gstrMsgDuplicateName Then
                ShowErrorMessage(ex.Message)
                Me.txtCostCenterType.Focus()
            ElseIf ex.Message = gstrMsgDuplicateCode Then
                ShowErrorMessage(ex.Message)
            Else
                Me.txtCostCenterType.Focus()
                Throw ex
            End If
        End Try
    End Function

    ''Here we will call DAL Function for Update the selected record, and if the function successfully Updates the records
    ''then the function will return True, otherwise returns False
    Public Function Update(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update
        Try

            ''Applying Front End Validation Checks
            If Me.IsValidate(EnumDataMode.Edit) Then

                Dim result As DialogResult = Windows.Forms.DialogResult.Yes

                'Confirmation from user for Creation of New FYear
                If gblnShowSaveConfirmationMessages Then
                    ''Getting Save Confirmation from User
                    result = ShowConfirmationMessage(gstrMsgUpdate, MessageBoxDefaultButton.Button1)
                End If

                If result = Windows.Forms.DialogResult.Yes Then

                    ''Create a DAL Object and calls its Add Method by passing Model Object
                    If New GLCostCenterDal().Update(Me.mobjModel) Then

                        '#To Do
                        'If pbFinancialYearID = 0 Or pbLocationID = 0 Then
                        '    MsgBox("You must logout before continue to work with LS-GL", vbInformation)
                        'End If

                        ''Query to Database and get fressh modifications in the Grid
                        Me.GetAllRecords()

                        'to select the last updated record
                        For Rind As Int16 = 0 To (grdAllRecords.RowCount - 1)
                            If Me.grdAllRecords.GetRow(Rind).Cells(EnumGridFYear.CostCenterID).Value = mobjModel.CostCenterId Then
                                Me.grdAllRecords.Row = Rind
                                Exit For
                            End If
                        Next

                        Me.GridEX1_SelectionChanged(Nothing, Nothing)

                        ''Reset controls and set New Mode
                        Me.ReSetControls()

                    End If

                End If

            End If

        Catch ex As Exception
            If ex.Message = gstrMsgDuplicateName Then
                ShowErrorMessage(ex.Message)
                Me.txtCostCenterType.Focus()
            ElseIf ex.Message = gstrMsgDuplicateCode Then
                ShowErrorMessage(ex.Message)
            Else
                Me.txtCostCenterType.Focus()
                Throw ex
            End If
        End Try
    End Function

    ''Here we will call DAL Function for Delete the selected record, and if the function successfully Deletes the records
    ''then the function will return True, otherwise returns False
    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try

            ''Applying Front End Validation Checks
            If Me.IsValidate(, "BackEndDeleteValidation") Then
                Dim result As DialogResult = Windows.Forms.DialogResult.Yes
                ''Getting Save Confirmation from User
                result = ShowConfirmationMessage(gstrMsgDelete, MessageBoxDefaultButton.Button2)


                If result = Windows.Forms.DialogResult.Yes Then

                    ''Create a DAL Object and calls its Delete Method by passing Model Object
                    If New GLCostCenterDal().Deleted(Me.mobjModel) Then

                        ''This will hold row index of the selected row 
                        Dim intGridRowIndex As Integer
                        intGridRowIndex = Me.grdAllRecords.Row

                        ''Query to Database and get fressh modifications in the Grid
                        Me.GetAllRecords()

                        ''Call RowColumn Change Event
                        Me.GridEX1_SelectionChanged(Nothing, Nothing)

                        ''Reset the row index to the grid
                        If intGridRowIndex > (Me.grdAllRecords.RowCount - 1) Then intGridRowIndex = (Me.grdAllRecords.RowCount - 1)
                        If Not intGridRowIndex < 0 Then Me.grdAllRecords.Row = intGridRowIndex
                    End If
                End If
            End If

        Catch ex As Exception
            Me.txtCostCenterType.Focus()
            Throw ex
        End Try
    End Function


#End Region

#Region "Local Functions and Procedures"



#End Region

#Region "Form Controls Events"

    ''This event will prevent the user to change the system language.
    Private Sub frmDefFiniancialYear_InputLanguageChanging(ByVal sender As System.Object, ByVal e As System.Windows.Forms.InputLanguageChangingEventArgs) Handles MyBase.InputLanguageChanging
        e.Cancel = True
    End Sub

    Private Sub frmDefFiniancialYear_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            ''Getting all available controls list to the user on this screen; in a collection 
            mobjControlList = GetFormSecurityControls(Me.Name)

            ''Assing Images to Buttons
            Me.SetButtonImages()

            ''Get all available record for the respective screen and fill the grid
            Call GetAllRecords()

            ''To avoid implecit call of rowcol chang event , We are assinging event handler at runtime.
            'AddHandler grdAllRecords.SelectionChanged, AddressOf Me.grdAllRecords_SelectionChanged

            ''Reset the controls for new mode
            Call ReSetControls()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try


    End Sub

    Private Sub frmDefFiniancialYear_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        ''To avoid implecit call of rowcol chang event , We are assinging event handler at runtime.
        AddHandler grdAllRecords.SelectionChanged, AddressOf Me.GridEX1_SelectionChanged
    End Sub


    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click, btnNew.Click, btnUpdate.Click, btnDelete.Click, btnCancel.Click, btnExit.Click

        Try

            'Dim btn As C1.Win.C1Input.C1Button = CType(sender, C1.Win.C1Input.C1Button)
            Dim btn As Windows.Forms.Button = CType(sender, Windows.Forms.Button)

            ''If New Button is Clicked
            If btn.Name = btnNew.Name Then
                ''Refresh the controls for new mode
                Me.ReSetControls()

            ElseIf btn.Name = btnSave.Name Then
                '' Call Save method to save the record
                Me.Save()
            ElseIf btn.Name = btnUpdate.Name Then
                '' Call Update method to update the record
                Me.Update()
            ElseIf btn.Name = btnDelete.Name Then
                '' Call Delete method to delete the record
                Me.Delete()
            ElseIf btn.Name = btnCancel.Name Then
                ''Load Selected record in Edit Mode
                Me.GridEX1_SelectionChanged(Nothing, Nothing)

            ElseIf btn.Name = btnExit.Name Then
                Me.Close()
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            mobjModel = Nothing
        End Try

    End Sub

    Private Sub btnFirst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFirst.Click, btnPrevious.Click, btnNext.Click, btnLast.Click

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

        End Try

    End Sub

    Private Sub GridEX1_LoadingRow(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grdAllRecords.LoadingRow

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

                'e.Row.Cells(EnumGridCity.CityName).Text = strTotalRecords & " (" & Me.GridEX1.GetTotal(Me.GridEX1.RootTable.Columns(EnumGridCity.CityName), Janus.Windows.GridEX.AggregateFunction.Count) & ") "

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub frmDefFiniancialYear_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Control And e.KeyCode = Keys.S Then
                If Me.btnSave.Enabled = True Then Me.Save()
            ElseIf e.Control And e.KeyCode = Keys.U Then
                If Me.btnUpdate.Enabled = True Then Me.Update()
            ElseIf e.Control And e.KeyCode = Keys.D Then
                If Me.btnDelete.Enabled = True Then Me.Delete()
            ElseIf e.Control And e.KeyCode = Keys.N Then
                If Me.btnNew.Enabled = True Then Me.ReSetControls()
            ElseIf e.Control And e.KeyCode = Keys.E Then
                If Me.btnCancel.Enabled = True Then Me.GridEX1_SelectionChanged(Nothing, Nothing)
            ElseIf e.Control And e.KeyCode = Keys.X Then
                If Me.btnExit.Enabled = True Then Me.Close()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub GridEX1_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles GridEX1.SelectionChanged
        Try

            ''If there is no record found in grid then load the screen in new mode
            If grdAllRecords.RowCount = 0 Then
                Me.ReSetControls()
                Exit Sub
            End If

            If Me.grdAllRecords.GetRow().RowType = Janus.Windows.GridEX.RowType.TotalRow Then
                Me.grdAllRecords.Row = (Me.grdAllRecords.Row - 1)
                Exit Sub
            End If

            intPkId = Me.grdAllRecords.GetValue(EnumGridFYear.CostCenterID).ToString
            Me.txtCostCenterTitle.Text = Me.grdAllRecords.GetValue(EnumGridFYear.CostCenterTitle).ToString
            Me.txtCostCenterType.Text = Me.grdAllRecords.GetValue(EnumGridFYear.CostCenterType).ToString

            Call ApplySecurity(EnumDataMode.Edit)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
#End Region

End Class