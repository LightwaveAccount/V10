''/////////////////////////////////////////////////////////////////////////////////////////
''//                        SMS Utility 
''/////////////////////////////////////////////////////////////////////////////////////////
''//-------------------------------------------------------------------------------------
''// File Name       : frmSMSConfiguration.vb           				                            
''// Programmer	     : Farooq ul hassan 
''// Creation Date	 : 09-July-2012
''// Description     : CR # 241 This form will be used for sms related settings of Candela    
''// Function List   : 								                                    
''//											                                            
''//-------------------------------------------------------------------------------------
''// Date Modified     Modified by           CR#        Brief Description		
''//
''//------------------------------------------------------------------------------------
''/////////////////////////////////////////////////////////////////////////////////////////
Imports DAL
Imports Model
Imports System.Collections.Specialized
Imports System.Data
Imports Utility.Utility
Imports Microsoft.VisualBasic
Imports System.IO

Public Class frmSMSConfiguration
    Implements IGeneral
#Region "Variables"
    ''This collection will hold the controls' names, upon which the logged in user has rights
    Private mobjControlList As NameValueCollection
    ''This is the model object which will be set with data values and refered for Save|Update|Delete|Loading Record in Edit Mode
    Private mobjModel As SMSConfiguration
    Private StartDate As DateTime
    Private EndDate As DateTime
    Private intPkId As Integer
    Private Dt As DataTable
    Private objList As New Dictionary(Of String, SMSConfiguration)
#End Region
#Region "Enumerations"

    ''This is the representation of grid columns, and will be used while referring grid values, 
    ''instead of giving hard coded column's indexes
    Private Enum EnumGridSMSConfiguration
        SMSConfig_id
        SMSCode
        ScreenName
        Action
        Mode
        SMSRecipient
        SendSMS
        SMS
        PhoneNumber
    End Enum
#End Region
#Region "Interface methodes "
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try


            ''Columns with In-visible setting
            'Me.grdSMSConfiguration.RootTable.Columns(EnumGridSMSConfiguration.SMSConfig_id).FormatString = ""
            Me.grdSMSConfiguration.RootTable.Columns(EnumGridSMSConfiguration.SMSConfig_id).Visible = False
            Me.grdSMSConfiguration.RootTable.Columns(EnumGridSMSConfiguration.SMSCode).Visible = True
            Me.grdSMSConfiguration.RootTable.Columns(EnumGridSMSConfiguration.ScreenName).Visible = False
            Me.grdSMSConfiguration.RootTable.Columns(EnumGridSMSConfiguration.Action).Visible = False
            Me.grdSMSConfiguration.RootTable.Columns(EnumGridSMSConfiguration.Mode).Visible = True
            Me.grdSMSConfiguration.RootTable.Columns(EnumGridSMSConfiguration.SMSRecipient).Visible = True

            Me.grdSMSConfiguration.RootTable.Columns(EnumGridSMSConfiguration.SendSMS).Visible = True
          
            Me.grdSMSConfiguration.RootTable.Columns(EnumGridSMSConfiguration.SMS).Visible = True
            Me.grdSMSConfiguration.RootTable.Columns(EnumGridSMSConfiguration.PhoneNumber).Visible = True
            ''Set columns widths for visible columns
           
            Me.grdSMSConfiguration.RootTable.Columns(EnumGridSMSConfiguration.SMS).MaxLength = 800
            Me.grdSMSConfiguration.RootTable.Columns(EnumGridSMSConfiguration.SMS).Width = 320
            Me.grdSMSConfiguration.RootTable.Columns(EnumGridSMSConfiguration.PhoneNumber).MaxLength = 99
            ' Me.grdSMSConfiguration.RootTable.Columns(EnumGridSMSConfiguration.

            Me.grdSMSConfiguration.RootTable.Columns(EnumGridSMSConfiguration.ScreenName).FormatString = ""
            Me.grdSMSConfiguration.RootTable.Columns(EnumGridSMSConfiguration.SendSMS).ColumnType = Janus.Windows.GridEX.ColumnType.CheckBox
           
            Dim grpFormScreenName As New Janus.Windows.GridEX.GridEXGroup(Me.grdSMSConfiguration.RootTable.Columns(EnumGridSMSConfiguration.ScreenName))
            grpFormScreenName.GroupPrefix = String.Empty
            Me.grdSMSConfiguration.RootTable.Groups.Add(grpFormScreenName)

            Dim grpFormAction As New Janus.Windows.GridEX.GridEXGroup(Me.grdSMSConfiguration.RootTable.Columns(EnumGridSMSConfiguration.Action))
            grpFormAction.GroupPrefix = String.Empty
            Me.grdSMSConfiguration.RootTable.Groups.Add(grpFormAction)



            Me.grdSMSConfiguration.RootTable.Columns(EnumGridSMSConfiguration.SMSCode).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSMSConfiguration.RootTable.Columns(EnumGridSMSConfiguration.ScreenName).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSMSConfiguration.RootTable.Columns(EnumGridSMSConfiguration.Mode).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSMSConfiguration.RootTable.Columns(EnumGridSMSConfiguration.SMSRecipient).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSMSConfiguration.RootTable.Columns(EnumGridSMSConfiguration.SMS).EditType = Janus.Windows.GridEX.EditType.TextBox
            Me.grdSMSConfiguration.RootTable.Columns(EnumGridSMSConfiguration.SendSMS).EditType = Janus.Windows.GridEX.EditType.CheckBox
            Me.grdSMSConfiguration.RootTable.Columns(EnumGridSMSConfiguration.Action).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSMSConfiguration.RootTable.Columns(EnumGridSMSConfiguration.PhoneNumber).EditType = Janus.Windows.GridEX.EditType.TextBox


            Me.grdSMSConfiguration.RootTable.Columns(EnumGridSMSConfiguration.SMSCode).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Me.grdSMSConfiguration.RootTable.Columns(EnumGridSMSConfiguration.ScreenName).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Me.grdSMSConfiguration.RootTable.Columns(EnumGridSMSConfiguration.Mode).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Me.grdSMSConfiguration.RootTable.Columns(EnumGridSMSConfiguration.SMSRecipient).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Me.grdSMSConfiguration.RootTable.Columns(EnumGridSMSConfiguration.SMS).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Me.grdSMSConfiguration.RootTable.Columns(EnumGridSMSConfiguration.PhoneNumber).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Me.grdSMSConfiguration.TotalRowFormatStyle.BackColor = gobjRequiredFieldtBKColor
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub ApplySecurity(ByVal Mode As Utility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
        Try


           If Mode.ToString = EnumDataMode.Edit.ToString Then

                btnNew.Enabled = False
                btnSave.Enabled = False

                If mobjControlList.Item("btnUpdate") Is Nothing Then
                    btnUpdate.Enabled = False
                Else
                    btnUpdate.Enabled = True
                End If


                btnDelete.Enabled = False

                btnCancel.Enabled = True

                SetNavigationButtons(Mode)

                Me.grdSMSConfiguration.Enabled = True

                Me.grdSMSConfiguration.Focus()
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
            ShowErrorMessage(ex.Message)

        End Try
    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        ' MessageBox.Show(Condition)
        Try
            ''Create Model object

            mobjModel = New SMSConfiguration
            With mobjModel
                'Setting the Model Object Values
                .SMSConfig_id = Me.grdSMSConfiguration.GetRow.Cells(EnumGridSMSConfiguration.SMSConfig_id).Value

                .SMSCode = Me.grdSMSConfiguration.GetRow.Cells(EnumGridSMSConfiguration.SMSCode).Text
                .ScreenName = Me.grdSMSConfiguration.GetRow.Cells(EnumGridSMSConfiguration.ScreenName).Text
                .Action = Me.grdSMSConfiguration.GetRow.Cells(EnumGridSMSConfiguration.Action).Text
                .Mode = Me.grdSMSConfiguration.GetRow.Cells(EnumGridSMSConfiguration.Mode).Text
                .SMSRecipient = Me.grdSMSConfiguration.GetRow.Cells(EnumGridSMSConfiguration.SMSRecipient).Text

                .SendSMS = Me.grdSMSConfiguration.GetRow.Cells(EnumGridSMSConfiguration.SendSMS).Value
                .SMS = Me.grdSMSConfiguration.GetRow.Cells(EnumGridSMSConfiguration.SMS).Text
                .PhoneNumber = Me.grdSMSConfiguration.GetRow.Cells(EnumGridSMSConfiguration.PhoneNumber).Text

                .ActivityLog.ShopID = -1
                .ActivityLog.ScreenTitle = Me.TabPgSMSConfiguration.Text
                .ActivityLog.LogGroup = "Definition"
                .ActivityLog.UserID = gObjUserInfo.UserID
            End With
            If objList.ContainsKey(mobjModel.SMSCode) = True Then
                objList.Remove(mobjModel.SMSCode)
            End If
            objList.Add(mobjModel.SMSCode, mobjModel)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try

            ''Getting Datasource for Grid from DAL
            Dim dt As DataTable = New SMSConfigurationDAL().GetAll()
            Me.grdSMSConfiguration.DataSource = dt
            Me.grdSMSConfiguration.RetrieveStructure()

            ''Applying Grid Formatting Setting


            Me.ApplyGridSettings()



        Catch ex As Exception
            Throw ex
        End Try
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

            Me.btnExit.ImageList = gobjMyImageListForOperationBar
            Me.btnExit.ImageKey = "Exit"

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As Utility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update
        Try
             
            ''Getting Save Confirmation from User
            If ShowConfirmationMessage("Do you want to Update this record?", MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then

                ''Create a DAL Object and calls its Update Method by passing Model Object
                Dim valueColl As _
            Dictionary(Of String, SMSConfiguration).ValueCollection = _
            objList.Values
                For Each item As SMSConfiguration In valueColl
                    If New SMSConfigurationDAL().Update(item) Then

                        ''Query to Database and get fressh modifications in the Grid

                        'to select the last updated record
                    End If
                Next
                Me.GetAllRecords()

                ''Getting Save Confirmation from User
                ShowInformationMessage("Record Updated Successfully")
               
            End If
        Catch ex As Exception

        Finally
            objList.Clear()
        End Try
    End Function
#End Region

    Private Sub frmSMSConfiguration_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.Control And e.KeyCode = Keys.S Then
                If btnSave.Enabled = True Then Me.Save()
            ElseIf e.Control And e.KeyCode = Keys.U Then
                If btnUpdate.Enabled = True Then Me.Update1()
            ElseIf e.Control And e.KeyCode = Keys.N Then
                If btnNew.Enabled = True Then Me.ReSetControls()
            ElseIf e.Control And e.KeyCode = Keys.E Then
                If btnCancel.Enabled = True Then GetAllRecords() : mobjModel = Nothing : objList.Clear()
            ElseIf e.Control And e.KeyCode = Keys.D Then
                If btnDelete.Enabled = True Then Me.Delete()
            ElseIf e.Control And e.KeyCode = Keys.X Then
                If btnExit.Enabled = True Then Me.Close()
            ElseIf e.Control And e.KeyCode = Keys.F Then
                If btnNext.Enabled = True Then Me.grdSMSConfiguration.MoveNext()
            ElseIf e.Control And e.KeyCode = Keys.B Then
                If btnPrevious.Enabled = True Then Me.grdSMSConfiguration.MovePrevious()
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub


    Private Sub frmSMSConfiguration_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            mobjControlList = GetFormSecurityControls(Me.Name)

            ''Assing Images to Buttons
            Me.SetButtonImages()

            ApplySecurity(EnumDataMode.Edit)

            GetAllRecords()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdSMSConfiguration_RecordUpdated(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSMSConfiguration.RecordUpdated
        Try
            FillModel()
        Catch ex As Exception
        End Try
    End Sub
    
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click, btnCancel.Click, btnExit.Click

        Try


            'Dim btn As C1.Win.C1Input.C1Button = CType(sender, C1.Win.C1Input.C1Button)
            Dim btn As Windows.Forms.Button = CType(sender, Windows.Forms.Button)

            If btn.Name = btnUpdate.Name Then
                '' Call Update method to update the record
                Me.Update1()
            ElseIf btn.Name = btnCancel.Name Then
                GetAllRecords()
                mobjModel = Nothing
                objList.Clear()
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
                Me.grdSMSConfiguration.Row = 0

                ''If Move Previous is clicked, then navigate to Previous record of the grid
            ElseIf btn.Name = Me.btnPrevious.Name Then
                If Me.grdSMSConfiguration.Row > 0 Then Me.grdSMSConfiguration.Row = (Me.grdSMSConfiguration.Row - 1)

                ''If Move Next is clicked, then navigate to next record of the grid
            ElseIf btn.Name = Me.btnNext.Name Then
                If Me.grdSMSConfiguration.Row >= 0 Then Me.grdSMSConfiguration.Row = (Me.grdSMSConfiguration.Row + 1)


                ''If Move Last is clicked, then navigate to Last record of the grid
            ElseIf btn.Name = Me.btnLast.Name Then
                Me.grdSMSConfiguration.Row = (Me.grdSMSConfiguration.RowCount - 1)

            End If

        Catch ex As Exception

        End Try

    End Sub

End Class