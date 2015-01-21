Imports DAL
Imports Model
Imports System.Collections.Specialized
Imports System.Data
Imports Utility.Utility
Imports System.Data.SqlClient
''/////////////////////////////////////////////////////////////////////////////////////////
''//                     GL CONFIGURATION
''/////////////////////////////////////////////////////////////////////////////////////////
''//-------------------------------------------------------------------------------------
''// File Name       : frmDefCompany.vb           				                            
''// Programmer	     : Abdul Jabbar
''// Creation Date	 : July 17,2009
''// Description     : Company Defination screen.
''//-------------------------------------------------------------------------------------
''// Date Modified     Modified by         Brief Description			                
''//------------------------------------------------------------------------------------
''//05 April,2010      Abdul Jabbar         A company can't be deleted even no transaction exist. (Due to constraint with TblDefUserLocation) CR# 24
'// 02 Dec,2011        Abdul Jabbar         CR#160,New changes Relevant to Service Broker/Data Log
'//09-Dec-2011         Fatima Tajammal      CR#136 GL Company sould be deleted if no voucher eixts 
'//20-Dec-2011         Abdul Jabbar         CR#224 GL Company Code should not be greater than 2 characters.
''/////////////////////////////////////////////////////////////////////////////////////////
Public Class frmDefCompany

    Implements IGeneral

#Region "Variables"
    ''This collection will hold the controls' names, upon which the logged in user has rights
    Private mobjControlList As NameValueCollection
    ''This is the model object which will be set with data values and refered for Save|Update|Delete|Loading Record in Edit Mode
    Private mobjModel As Company
    Private StartDate As DateTime
    Private EndDate As DateTime
    Private intPkId As Integer

#End Region

#Region "Enumerations"

    ''This is the representation of grid columns, and will be used while referring grid values, 
    ''instead of giving hard coded column's indexes
    Private Enum EnumGridCompany
        CompanyID = 0
        CompanyCode = 1
        CompanyName = 2
        CompanyPhone = 3
        CompanyFax = 4
        CompanyURL = 5
        CompanyAddress = 6
        SortOrder = 7
        Comments = 8
    End Enum
    Private Enum EnumMode

        NewMode = 0
        UpdateMode = 1

    End Enum

#End Region

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

    End Sub

    ''Here we will use this procedure to load all master records; respective to the screen.
    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Dim lngTotalRecords As Long

        Try

            ''Getting Datasource for Grid from DAL
            Dim dt As DataTable = New CompanyDAL().GetAll()
            Me.grdAllRecords.DataSource = dt
            Me.grdAllRecords.RetrieveStructure()

            lngTotalRecords = dt.Rows.Count

            If lngTotalRecords <= 0 Then
                Me.ReSetControls()
            Else
                Me.grdAllRecords.Row = 0
            End If

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
            Me.grdAllRecords.RootTable.Columns(EnumGridCompany.CompanyID).Visible = False

            ''Set columns widths for visible columns
            Me.grdAllRecords.RootTable.Columns(EnumGridCompany.CompanyCode).Width = 100
            Me.grdAllRecords.RootTable.Columns(EnumGridCompany.CompanyName).Width = 160
            Me.grdAllRecords.RootTable.Columns(EnumGridCompany.CompanyPhone).Width = 80
            Me.grdAllRecords.RootTable.Columns(EnumGridCompany.CompanyFax).Width = 80
            Me.grdAllRecords.RootTable.Columns(EnumGridCompany.CompanyURL).Width = 120
            Me.grdAllRecords.RootTable.Columns(EnumGridCompany.CompanyAddress).Width = 200
            Me.grdAllRecords.RootTable.Columns(EnumGridCompany.SortOrder).Width = 80
            Me.grdAllRecords.RootTable.Columns(EnumGridCompany.Comments).Width = 180

            Me.grdAllRecords.TotalRowFormatStyle.BackColor = gobjRequiredFieldtBKColor

            'Stop Editing in Grid
            Me.grdAllRecords.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False

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
            Me.txtCompanyName.Text = String.Empty
            Me.txtCompanyCode.Text = String.Empty
            Me.txtSortOrder.Text = String.Empty
            Me.txtComments.Text = String.Empty
            Me.txtAddress.Text = String.Empty
            Me.txtCompanyFax.Text = String.Empty
            Me.txtCompanyPhone.Text = String.Empty
            Me.txtCompanyURL.Text = String.Empty

            ''Set New Mode and Applying Security Setting
            Call ApplySecurity(EnumDataMode.[New])

            Me.txtCompanyCode.Focus()

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

            Me.FillModel()

            ''1 First Check Front End Validations

            'CR#224
            If (Me.txtCompanyCode.Text.ToString().Trim.Length > 2) Then
                ShowErrorMessage("Company code can not be greater than 2 characters")
                Return False
            End If


            If Mode = EnumDataMode.[New] Or Mode = EnumDataMode.Edit Then

                If Me.txtCompanyName.Text.Trim = String.Empty Then
                    ShowErrorMessage("Required Information missing ! ")
                    Me.txtCompanyName.Focus()
                    Return False
                End If

                If Me.txtCompanyCode.Text.Trim = String.Empty Then
                    ShowErrorMessage("Required Information missing ! ")
                    Me.txtCompanyCode.Focus()
                    Return False
                End If

            End If

            If Mode = EnumDataMode.[New] Then

                ''Check License for Company

                If New CompanyDAL().IsValidateForSave() Then
                    ''Check for Duplication/Record Alreadt exist

                    If New CompanyDAL().ISRecordAlreadyExist(mobjModel, EnumMode.NewMode) = False Then
                        Return True
                    Else
                        ShowErrorMessage("Company with this Code/Name already defined,Duplicate Code/Name is not allowed ...")
                        Return False
                    End If
                Else
                    ShowErrorMessage("Please Puchase License for Multi Companies")
                    Return False
                End If

            End If

            If Mode = EnumDataMode.Edit Then

                ''Check for Duplication/Record Alreadt exist

                If New CompanyDAL().ISRecordAlreadyExist(mobjModel, EnumMode.UpdateMode) = False Then
                    Return True
                Else
                    ShowErrorMessage("Company with this Code/Name already defined,Duplicate Code/Name is not allowed ...")
                    Return False
                End If

            End If

          
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
            mobjModel = New Company
            With mobjModel

                .CompanyID = intPkId
                .CompanyName = funFilterReserveText(Me.txtCompanyName.Text)
                .CompanyCode = funFilterReserveText(Me.txtCompanyCode.Text)
                .CompanyFax = funFilterReserveText(Me.txtCompanyFax.Text)
                .CompanyURL = funFilterReserveText(Me.txtCompanyURL.Text)
                .Comments = funFilterReserveText(Me.txtComments.Text)
                .SortOrder = IIf(Me.txtSortOrder.Text.Trim = String.Empty, 0, Me.txtSortOrder.Text)
                .CompanyPhone = funFilterReserveText(Me.txtCompanyPhone.Text)
                .CompanyAddress = funFilterReserveText(Me.txtAddress.Text)

                'CR#160
                .ActivityLog.ShopID = 0 'gObjUserInfo.ShopInfo.ShopID
                .ActivityLog.ScreenTitle = Me.Text
                .ActivityLog.LogGroup = "Definition"
                .ActivityLog.UserID = gObjUserInfo.UserID

            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Private Function funFilterReserveText(ByVal Txt As String) As String
        Try

            funFilterReserveText = Replace(Txt, "'", "''", , , vbTextCompare)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Function

    ''Here we will call DAL Function for SAVE, and if the function successfully Saves the records
    ''then the function will return True, otherwise returns False
    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try

            ''Applying Validation Checks
            If Me.IsValidate(EnumDataMode.[New]) Then

                Dim result As DialogResult = Windows.Forms.DialogResult.Yes

                'Confirmation from user for Creation of New FYear
                If gblnShowSaveConfirmationMessages Then
                    ''Getting Save Confirmation from User
                    result = ShowConfirmationMessage(gstrMsgSave, MessageBoxDefaultButton.Button1)
                End If

                If result = Windows.Forms.DialogResult.Yes Then

                    ''Create a DAL Object and calls its Add Method by passing Model Object
                    If New CompanyDAL().Add(Me.mobjModel) Then

                        

                        ''Query to Database and get fressh modifications in the Grid
                        Me.GetAllRecords()

                        'to select the last updated record
                        For Rind As Int16 = 0 To (grdAllRecords.RowCount - 1)
                            If Me.grdAllRecords.GetRow(Rind).Cells(EnumGridCompany.CompanyID).Value = mobjModel.CompanyID Then
                                Me.grdAllRecords.Row = Rind
                                Exit For
                            End If
                        Next

                        Me.grdAllRecords_SelectionChanged(Nothing, Nothing)

                        If Not New CompanyDAL().FYearExist() Then
                            ShowErrorMessage("Now define Financial Year")
                            'frmDefFiniancialYear.MdiParent = MDIParent1
                            'CType(Me.MdiParent, MDIParent1).mnuProduct_Click(Nothing, Nothing)
                            Me.Close()
                            Exit Function


                        End If

                    End If

                End If

            End If

        Catch ex As Exception
            If ex.Message = gstrMsgDuplicateName Then
                ShowErrorMessage(ex.Message)
            ElseIf ex.Message = gstrMsgDuplicateCode Then
                ShowErrorMessage(ex.Message)
            Else
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
                ''Getting Save Confirmation from User
                result = ShowConfirmationMessage(gstrMsgUpdate, MessageBoxDefaultButton.Button1)

                If result = Windows.Forms.DialogResult.Yes Then

                    ''Create a DAL Object and calls its Update Method by passing Model Object
                    If New CompanyDAL().Update(Me.mobjModel) Then

                        ''Query to Database and get fressh modifications in the Grid
                        Me.GetAllRecords()

                        'to select the last updated record
                        For Rind As Int16 = 0 To (grdAllRecords.RowCount - 1)
                            If Me.grdAllRecords.GetRow(Rind).Cells(EnumGridCompany.CompanyID).Value = mobjModel.CompanyID Then
                                Me.grdAllRecords.Row = Rind
                                Exit For
                            End If
                        Next

                        Me.grdAllRecords_SelectionChanged(Nothing, Nothing)

                        If gblnShowAfterUpdateMessages Then
                            ''Getting Save Confirmation from User
                            ShowInformationMessage(gstrMsgAfterUpdate)
                        End If


                    End If
                End If
            End If

        Catch ex As Exception
            If ex.Message = gstrMsgDuplicateName Then
                ShowErrorMessage(ex.Message)
            ElseIf ex.Message = gstrMsgDuplicateCode Then
                ShowErrorMessage(ex.Message)
            Else
                Throw ex
            End If
        End Try
    End Function

    ''Here we will call DAL Function for Delete the selected record, and if the function successfully Deletes the records
    ''then the function will return True, otherwise returns False
    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try

            ''Applying Front End Validation Checks
            ' If Me.IsValidate(, "BackEndDeleteValidation") Then
            Dim result As DialogResult = Windows.Forms.DialogResult.Yes
            ''Getting Save Confirmation from User
            result = ShowConfirmationMessage(gstrMsgDelete, MessageBoxDefaultButton.Button2)


            If result = Windows.Forms.DialogResult.Yes Then

                ''Create a DAL Object and calls its Delete Method by passing Model Object
                Me.FillModel()
                'CR#136 GL Company sould be deleted if no voucher eixts 
                If New CompanyDAL().GetCompanyID(Me.mobjModel) Then
                    If New CompanyDAL().Deleted(Me.mobjModel) Then

                        ''This will hold row index of the selected row 
                        Dim intGridRowIndex As Integer
                        intGridRowIndex = Me.grdAllRecords.Row

                        ''Query to Database and get fressh modifications in the Grid
                        Me.GetAllRecords()

                        ''Call RowColumn Change Event
                        Me.grdAllRecords_SelectionChanged(Nothing, Nothing)

                        ''Reset the row index to the grid
                        If intGridRowIndex > (Me.grdAllRecords.RowCount - 1) Then intGridRowIndex = (Me.grdAllRecords.RowCount - 1)
                        If Not intGridRowIndex < 0 Then Me.grdAllRecords.Row = intGridRowIndex
                    End If
                Else
                    ShowErrorMessage("Can't Delete Company,because related information exist")
                End If
            End If

            'Unable to Delete this record because related information exists
        Catch ex As SqlException
            If ex.Number = 547 Then
                'ShowErrorMessage("Can't Delete Company,because related information exist")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function


#End Region

#Region "Local Functions and Procedures"



#End Region

#Region "Form Controls Events"

    ''This event will prevent the user to change the system language.
    Private Sub frmDefCompany_InputLanguageChanging(ByVal sender As System.Object, ByVal e As System.Windows.Forms.InputLanguageChangingEventArgs) Handles MyBase.InputLanguageChanging
        e.Cancel = True
    End Sub

    Private Sub frmDefCompany_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

            Dim VTScreentext As Font
            VTScreentext = New Font(New FontFamily("Microsoft Sans Serif"), 14, Drawing.FontStyle.Bold)
            Me.lblScreentext.Font = VTScreentext
            Me.lblScreentext.Text = "Company Definition"

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try


    End Sub

    Private Sub frmDefCompany_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        ''To avoid implecit call of rowcol chang event , We are assinging event handler at runtime.
        AddHandler grdAllRecords.SelectionChanged, AddressOf Me.grdAllRecords_SelectionChanged
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
                Me.grdAllRecords_SelectionChanged(Nothing, Nothing)

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

    Private Sub grdAllRecords_LoadingRow(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grdAllRecords.LoadingRow

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

                'e.Row.Cells(EnumGridCity.CityName).Text = strTotalRecords & " (" & Me.grdAllRecords.GetTotal(Me.grdAllRecords.RootTable.Columns(EnumGridCity.CityName), Janus.Windows.GridEX.AggregateFunction.Count) & ") "

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub frmDefCompany_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
                If Me.btnCancel.Enabled = True Then Me.grdAllRecords_SelectionChanged(Nothing, Nothing)
            ElseIf e.Control And e.KeyCode = Keys.X Then
                If Me.btnExit.Enabled = True Then Me.Close()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdAllRecords_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles grdAllRecords.SelectionChanged
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

            intPkId = Me.grdAllRecords.GetValue(EnumGridCompany.CompanyID).ToString
            Me.txtCompanyName.Text = Me.grdAllRecords.GetValue(EnumGridCompany.CompanyName).ToString
            Me.txtCompanyCode.Text = Me.grdAllRecords.GetValue(EnumGridCompany.CompanyCode).ToString
            Me.txtCompanyPhone.Text = Me.grdAllRecords.GetValue(EnumGridCompany.CompanyPhone).ToString
            Me.txtCompanyFax.Text = Me.grdAllRecords.GetValue(EnumGridCompany.CompanyFax).ToString
            Me.txtCompanyURL.Text = Me.grdAllRecords.GetValue(EnumGridCompany.CompanyURL).ToString
            Me.txtAddress.Text = Me.grdAllRecords.GetValue(EnumGridCompany.CompanyAddress).ToString
            Me.txtSortOrder.Text = Me.grdAllRecords.GetValue(EnumGridCompany.SortOrder).ToString
            Me.txtComments.Text = Me.grdAllRecords.GetValue(EnumGridCompany.Comments).ToString

            Call ApplySecurity(EnumDataMode.Edit)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtCompanyName_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCompanyName.LostFocus
        Try

            If Me.txtCompanyCode.Text.Trim = String.Empty Then
                Me.txtCompanyCode.Text = Me.txtCompanyName.Text
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
#End Region

    Private Sub txtSortOrder_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSortOrder.TextChanged
        Try

            If Not IsNumeric(Me.txtSortOrder.Text.Trim) Then Me.txtSortOrder.Text = ""

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class