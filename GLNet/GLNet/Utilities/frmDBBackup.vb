''/////////////////////////////////////////////////////////////////////////////////////////
''//                      Candela .Net
''/////////////////////////////////////////////////////////////////////////////////////////
''//-------------------------------------------------------------------------------------
''// File Name       : frmDBBackup.vb           				                            
''// Programmer	     : Fahad Amin Rizvi
''// Creation Date	 : 29-Apr-2009
''// Description     : This form will be used to backup & restore database       
''// Function List   : 								                                    
''//											                                            
''//-------------------------------------------------------------------------------------
''// Date Modified     CR#      Modified by         Brief Description			                
''//------------------------------------------------------------------------------------
''// April 16,2010     38       Abdul Jabbar        Backup Utility is not working.System doesn't show the databases to take the backup.
'//  July 02,2010      59       Abdul Jabbar        GL Database Backup utility should contactenate Date & Time with backup file name
'//  Nov 29,2013       285      Fatima Tajammal     GL Backup Utility: System should show the DB selected by default with which user is logged in
''/////////////////////////////////////////////////////////////////////////////////////////

'CR# 38 
#Region "Existing Code before Implmentation of CR# 38"
'Imports DAL
'Imports Model
'Imports System.Collections.Specialized
'Imports System.Data
'Imports Utility.Utility

'Public Class frmDBBackup
'    Implements IGeneral

'#Region "Variables"
'    ''This collection will hold the controls' names, upon which the logged in user has rights
'    Private mobjControlList As NameValueCollection
'#End Region

'#Region "Enumerations"

'#End Region

'#Region "Interface Methods"
'    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

'    End Sub

'    Public Sub ApplySecurity(ByVal Mode As Utility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
'        Try
'            btnNew.Enabled = False
'            btnSave.Enabled = False
'            btnUpdate.Enabled = False
'            btnDelete.Enabled = False
'            btnCancel.Enabled = False
'            SetNavigationButtons(EnumDataMode.Disabled)

'        Catch ex As Exception
'            Throw ex
'        End Try
'    End Sub

'    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete

'    End Function

'    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos

'    End Sub

'    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel

'    End Sub

'    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords

'    End Sub

'    Public Function IsValidate(Optional ByVal Mode As Utility.Utility.EnumDataMode = Utility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate

'    End Function

'    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls

'    End Sub

'    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save

'    End Function

'    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages
'        Try

'            If gEnumIsRightToLeft = Windows.Forms.RightToLeft.No Then
'                Me.btnFirst.ImageList = gobjMyImageListForOperationBar
'                Me.btnFirst.ImageKey = "First"

'                Me.btnNext.ImageList = gobjMyImageListForOperationBar
'                Me.btnNext.ImageKey = "Next"

'                Me.btnPrevious.ImageList = gobjMyImageListForOperationBar
'                Me.btnPrevious.ImageKey = "Previous"

'                Me.btnLast.ImageList = gobjMyImageListForOperationBar
'                Me.btnLast.ImageKey = "Last"


'            Else
'                Me.btnFirst.ImageList = gobjMyImageListForOperationBar
'                Me.btnFirst.ImageKey = "Last"

'                Me.btnNext.ImageList = gobjMyImageListForOperationBar
'                Me.btnNext.ImageKey = "Previous"

'                Me.btnPrevious.ImageList = gobjMyImageListForOperationBar
'                Me.btnPrevious.ImageKey = "Next"

'                Me.btnLast.ImageList = gobjMyImageListForOperationBar
'                Me.btnLast.ImageKey = "First"
'            End If

'            Me.btnNew.ImageList = gobjMyImageListForOperationBar
'            Me.btnNew.ImageKey = "New"

'            Me.btnSave.ImageList = gobjMyImageListForOperationBar
'            Me.btnSave.ImageKey = "Save"

'            Me.btnUpdate.ImageList = gobjMyImageListForOperationBar
'            Me.btnUpdate.ImageKey = "Update"

'            Me.btnCancel.ImageList = gobjMyImageListForOperationBar
'            Me.btnCancel.ImageKey = "Cancel"

'            Me.btnDelete.ImageList = gobjMyImageListForOperationBar
'            Me.btnDelete.ImageKey = "Delete"

'            Me.btnExit.ImageList = gobjMyImageListForOperationBar
'            Me.btnExit.ImageKey = "Exit"

'        Catch ex As Exception
'            Throw ex
'        End Try
'    End Sub

'    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

'    End Sub

'    Public Sub SetNavigationButtons(ByVal Mode As Utility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons
'        Try

'            If Mode = EnumDataMode.[New] Then
'                ''if New Mode then Set Disable all Navigation Buttons
'                btnFirst.Enabled = False ': btnFirst.VisualStyle = C1.Win.C1Input.VisualStyle.System
'                btnPrevious.Enabled = False ': btnPrevious.VisualStyle = C1.Win.C1Input.VisualStyle.System
'                btnNext.Enabled = False ': btnNext.VisualStyle = C1.Win.C1Input.VisualStyle.System
'                btnLast.Enabled = False ': btnLast.VisualStyle = C1.Win.C1Input.VisualStyle.System

'            ElseIf Mode = EnumDataMode.Edit Then
'                ''if New Mode then Set Enable all Navigation Buttons
'                btnFirst.Enabled = True ': btnFirst.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
'                btnPrevious.Enabled = True ': btnPrevious.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
'                btnNext.Enabled = True ': btnNext.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
'                btnLast.Enabled = True ': btnLast.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue

'            ElseIf Mode = EnumDataMode.Disabled Then
'                ''if New Mode then Set Enable all Navigation Buttons
'                btnFirst.Enabled = False  ': btnFirst.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
'                btnPrevious.Enabled = False ': btnPrevious.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
'                btnNext.Enabled = False ': btnNext.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
'                btnLast.Enabled = False '
'            End If
'        Catch ex As Exception
'            Throw ex
'        End Try
'    End Sub

'    Public Function Update(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update

'    End Function
'#End Region

'#Region "Form Control Events"
'    Private Sub frmDBBackup_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
'        Try
'            'Call SetShopComboOnMDI(False)

'        Catch ex As Exception
'            ShowErrorMessage(ex.Message)
'        End Try
'    End Sub

'    Private Sub frmDBBackup_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
'        Try
'            ''Getting all available controls list to the user on this screen; in a collection 
'            mobjControlList = GetFormSecurityControls(Me.Name)

'            Me.txtServerName.Text = System.Configuration.ConfigurationManager.AppSettings("StrDBServerName").ToString
'            ''Assing Images to Buttons
'            Me.SetButtonImages()

'            ApplySecurity(EnumDataMode.Disabled)
'            'cboDatabase.Text = System.Configuration.ConfigurationManager.AppSettings("StrDBName").ToString
'            Me.txtBackupLocation.Text = DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Backup_Location")
'            'Me.txtBackupFileName.Text = cboDatabase.Text & Format(Date.Today, "yyyyMMdd") & "_" & Date.Now.Hour.ToString & "_" & Date.Now.Minute.ToString & "_" & Date.Now.Second.ToString & ".BAK"
'        Catch ex As Exception
'            ShowErrorMessage(ex.Message)
'        End Try
'    End Sub

'    Private Sub frmDBBackup_InputLanguageChanging(ByVal sender As Object, ByVal e As System.Windows.Forms.InputLanguageChangingEventArgs) Handles Me.InputLanguageChanging
'        e.Cancel = True
'    End Sub

'    Private Sub frmDBBackup_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
'        Try
'            If e.Control And e.KeyCode = Keys.S Then
'                If Me.btnSave.Enabled = True Then Me.Save()
'            ElseIf e.Control And e.KeyCode = Keys.U Then
'                If Me.btnUpdate.Enabled = True Then Me.Update()
'            ElseIf e.Control And e.KeyCode = Keys.D Then
'                If Me.btnDelete.Enabled = True Then Me.Delete()
'            ElseIf e.Control And e.KeyCode = Keys.N Then
'                If Me.btnNew.Enabled = True Then Me.ReSetControls()
'            ElseIf e.Control And e.KeyCode = Keys.E Then
'                'If Me.btnCancel.Enabled = True Then Me.grdAllRecords_SelectionChanged(Nothing, Nothing)
'            ElseIf e.Control And e.KeyCode = Keys.X Then
'                If Me.btnExit.Enabled = True Then Me.Close()
'            End If
'        Catch ex As Exception
'            ShowErrorMessage(ex.Message)
'        End Try
'    End Sub

'    Private Sub frmDBBackup_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
'        'Me.WindowState = FormWindowState.Maximized
'    End Sub

'    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click, btnNew.Click, btnUpdate.Click, btnDelete.Click, btnCancel.Click, btnExit.Click
'        Me.Close()
'    End Sub

'    Private Sub btnConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConnect.Click
'        Dim objDBBackup As New DBBackupDAL
'        Try
'            Dim strUsername As String = System.Configuration.ConfigurationManager.AppSettings.Get("StrUserName")
'            Dim strPassword As String = System.Configuration.ConfigurationManager.AppSettings.Get("StrPassword")

'            If objDBBackup.ConnectToServer(txtServerName.Text, strUsername, strPassword) Then
'                Dim dt As New DataTable
'                dt = objDBBackup.GetDBList()
'                cboDatabase.DisplayMember = "name"
'                cboDatabase.ValueMember = "name"
'                cboDatabase.DataSource = dt
'                cboDatabase.Text = System.Configuration.ConfigurationManager.AppSettings("StrDBName").ToString
'                Me.txtBackupLocation.Text = DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Backup_Location")
'                Me.txtBackupFileName.Text = cboDatabase.Text & Format(Date.Today, "yyyyMMdd") & "_" & Date.Now.Hour.ToString & "_" & Date.Now.Minute.ToString & "_" & Date.Now.Second.ToString & "_Backup.bak"
'            Else
'                ShowValidationMessage("Server not found")
'            End If
'        Catch ex As Exception
'            ShowErrorMessage(ex.Message)
'        End Try
'    End Sub

'    Private Sub cboDatabase_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDatabase.SelectedIndexChanged
'        If cboDatabase.Items.Count > 0 Then
'            Me.txtBackupLocation.Text = DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Backup_Location")
'            Me.txtBackupName.Text = cboDatabase.Text & Format(Date.Today, "yyyyMMdd") & "_" & Date.Now.Hour.ToString & "_" & Date.Now.Minute.ToString & "_" & Date.Now.Second.ToString & "_Backup"
'            Me.txtBackupFileName.Text = cboDatabase.Text & Format(Date.Today, "yyyyMMdd") & "_" & Date.Now.Hour.ToString & "_" & Date.Now.Minute.ToString & "_" & Date.Now.Second.ToString & "_Backup.bak"
'            grpBoxBackup.Text = "Complete Backup of Database (" & cboDatabase.SelectedValue & ")"
'        End If
'    End Sub

'    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
'        dlgBrowse.ShowDialog()
'        txtBackupLocation.Text = dlgBrowse.SelectedPath
'    End Sub

'    Private Sub btnBackup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackup.Click
'        Try
'            If cboDatabase.SelectedItem Is Nothing Then
'                ShowErrorMessage("Please select a database")
'                Exit Sub
'            End If

'            If txtBackupName.Text.Trim = String.Empty Then
'                ShowErrorMessage("Please enter Backup Name")
'                Exit Sub
'            End If

'            If txtBackupLocation.Text.Trim = String.Empty Then
'                ShowErrorMessage("Please enter Backup File Location")
'                Exit Sub
'            End If

'            If txtBackupFileName.Text.Trim = String.Empty Then
'                ShowErrorMessage("Please enter Backup File Name")
'                Exit Sub
'            End If

'            Dim objDBBackDAL As New DBBackupDAL
'            If btnBackup.Text = "Backup Now!" Then
'                Me.Cursor = Cursors.WaitCursor
'                If objDBBackDAL.BackupDatabase(System.Configuration.ConfigurationManager.AppSettings.Get("StrUserName"), _
'                                                           System.Configuration.ConfigurationManager.AppSettings.Get("StrPassword"), _
'                                                           cboDatabase.SelectedValue, _
'                                                           txtServerName.Text, _
'                                                           txtBackupLocation.Text & "\" & txtBackupFileName.Text) Then
'                    Me.Cursor = Cursors.Arrow
'                    ShowInformationMessage("Database backup created successfully")
'                End If

'            ElseIf btnBackup.Text = "Restore Now!" Then
'                Me.Cursor = Cursors.WaitCursor
'                If objDBBackDAL.RestoreDatabase(System.Configuration.ConfigurationManager.AppSettings.Get("StrUserName"), _
'                                                                           System.Configuration.ConfigurationManager.AppSettings.Get("StrPassword"), _
'                                                                           cboDatabase.SelectedValue, _
'                                                                           txtServerName.Text, _
'                                                                           txtBackupLocation.Text & "\" & txtBackupFileName.Text) Then
'                    Me.Cursor = Cursors.Arrow
'                    ShowInformationMessage("Database restored successfully")
'                End If
'            End If

'        Catch ex As Exception
'            Me.Cursor = Cursors.Arrow
'            ShowErrorMessage(ex.Message)
'        End Try
'    End Sub

'    Private Sub optBackup_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optBackup.CheckedChanged, optRestore.CheckedChanged
'        Try
'            Dim rdoButton As RadioButton = CType(sender, RadioButton)
'            If rdoButton.Name = "optBackup" Then
'                btnBackup.Text = "Backup Now!"
'            ElseIf rdoButton.Name = "optRestore" Then
'                btnBackup.Text = "Restore Now!"
'            End If
'        Catch ex As Exception
'            ShowErrorMessage(ex.Message)
'        End Try
'    End Sub
'#End Region



'    Private Sub lblDescription_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblDescription.Click

'    End Sub

'    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

'    End Sub

'    Private Sub btnSchedule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSchedule.Click

'    End Sub

'    Private Sub btnVerify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVerify.Click

'    End Sub
#End Region
Imports DAL
Imports Model
Imports System.Collections.Specialized
Imports System.Data
Imports Utility.Utility

Public Class frmDBBackup
    Implements IGeneral

#Region "Variables"
    ''This collection will hold the controls' names, upon which the logged in user has rights
    Private mobjControlList As NameValueCollection
    Dim strUsername As String
    Dim strPassword As String
#End Region

#Region "Enumerations"

#End Region

#Region "Interface Methods"
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(ByVal Mode As Utility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            btnNew.Enabled = False
            btnSave.Enabled = False
            btnUpdate.Enabled = False
            btnDelete.Enabled = False
            btnCancel.Enabled = False
            SetNavigationButtons(EnumDataMode.Disabled)

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
        Try
            txtBackupLocation.Text = DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Backup_Location")

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
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

            ElseIf Mode = EnumDataMode.Disabled Then
                ''if New Mode then Set Enable all Navigation Buttons
                btnFirst.Enabled = False  ': btnFirst.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                btnPrevious.Enabled = False ': btnPrevious.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                btnNext.Enabled = False ': btnNext.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                btnLast.Enabled = False '
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Update(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update

    End Function
#End Region

#Region "Form Control Events"
    Private Sub frmDBBackup_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            'Call SetShopComboOnMDI(False)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmDBBackup_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ''Getting all available controls list to the user on this screen; in a collection 
            mobjControlList = GetFormSecurityControls(Me.Name)

            txtServerName.Text = System.Configuration.ConfigurationManager.AppSettings.Get("StrDBServerName")
            SetConfigurationBaseSetting()
            ApplySecurity(EnumDataMode.Disabled)
            SetButtonImages()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Private Sub frmDBBackup_InputLanguageChanging(ByVal sender As Object, ByVal e As System.Windows.Forms.InputLanguageChangingEventArgs) Handles Me.InputLanguageChanging
    '    e.Cancel = True
    'End Sub

    Private Sub frmDBBackup_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
                'If Me.btnCancel.Enabled = True Then Me.grdAllRecords_SelectionChanged(Nothing, Nothing)
            ElseIf e.Control And e.KeyCode = Keys.X Then
                If Me.btnExit.Enabled = True Then Me.Close()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmDBBackup_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        'Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click, btnNew.Click, btnUpdate.Click, btnDelete.Click, btnCancel.Click, btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConnect.Click
        Dim objDBBackup As New DBBackupDAL
        Try
            strUsername = System.Configuration.ConfigurationManager.AppSettings.Get("StrUserName")
            strPassword = System.Configuration.ConfigurationManager.AppSettings.Get("StrPassword")

            If Not strUsername.Contains("sa") Then
                strUsername = Utility.Utility.SymmetricEncryption.Decrypt(strUsername, "f")
            End If

            ''CR#38
            If Not (strPassword.Contains("ms98lumensoft") Or strPassword.Contains("lumensoft2003")) Then
                strPassword = Utility.Utility.SymmetricEncryption.Decrypt(strPassword, "f")
            End If

            ShowProgressBar("Loading databases ...")
            Application.DoEvents()
            Application.DoEvents()
            Application.DoEvents()
            Application.DoEvents()

            If objDBBackup.ConnectToServer(txtServerName.Text, strUsername, strPassword) Then
                Dim dt As New DataTable
                dt = objDBBackup.GetDBList()
                
                cboDatabase.DisplayMember = "name"
                cboDatabase.ValueMember = "name"
                cboDatabase.DataSource = dt
                'CR # 285
                If dt.Rows.Count > 1 Then
                    Dim DBName As String = System.Configuration.ConfigurationManager.AppSettings("StrDBName")
                    Me.cboDatabase.SelectedValue = DBName
                End If
            Else
                'ShowValidationMessage(GetMessageString("gMsgServerNotFoundValidation"))
                ShowValidationMessage("Server not found")
            End If

            btnConnect.Enabled = False

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            EndProgressBar()
        End Try
    End Sub

    Private Sub cboDatabase_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDatabase.SelectedIndexChanged
        Try
            'CR#59

            'If cboDatabase.Items.Count > 0 Then
            '    txtBackupName.Text = cboDatabase.SelectedValue & " Backup"
            '    txtBackupFileName.Text = cboDatabase.SelectedValue & " Backup.bak"
            '    grpBoxBackup.Text = "Complete Backup of Database (" & cboDatabase.SelectedValue & ")"
            'End If

            If cboDatabase.Items.Count > 0 Then
                txtBackupName.Text = cboDatabase.SelectedValue & " Backup"
                txtBackupFileName.Text = cboDatabase.SelectedValue & "_Backup" & Date.Now.Year & "_" & Date.Now.Month & "_" & Date.Now.Day & "_" & Date.Now.Hour & "_" & Date.Now.Minute & "_" & Date.Now.Second & " .bak"
                grpBoxBackup.Text = "Complete Backup of Database (" & cboDatabase.SelectedValue & ")"
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        dlgBrowse.ShowDialog()
        txtBackupLocation.Text = dlgBrowse.SelectedPath
    End Sub

    Private Sub btnBackup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackup.Click
        Try


            If cboDatabase.SelectedItem Is Nothing Then
                ShowErrorMessage("Please select a database")
                Exit Sub
            End If

            If txtBackupName.Text.Trim = String.Empty Then
                ShowErrorMessage("Please enter Backup Name")
                Exit Sub
            End If

            If txtBackupLocation.Text.Trim = String.Empty Then
                ShowErrorMessage("Please enter Backup File Location")
                Exit Sub
            End If

            If txtBackupFileName.Text.Trim = String.Empty Then
                ShowErrorMessage("Please enter Backup File Name")
                Exit Sub
            End If

            Dim objDBBackDAL As New DBBackupDAL
            If btnBackup.Text = "Backup Now!" Then
                Me.Cursor = Cursors.WaitCursor
                If objDBBackDAL.BackupDatabase(strUsername, _
                                               strPassword, _
                                               cboDatabase.SelectedValue, _
                                               txtServerName.Text, _
                                               txtBackupLocation.Text & "\" & txtBackupFileName.Text) Then
                    Me.Cursor = Cursors.Arrow
                    ShowInformationMessage("Database backup created successfully")
                End If

            ElseIf btnBackup.Text = "Restore Now!" Then
                Me.Cursor = Cursors.WaitCursor
                If objDBBackDAL.RestoreDatabase(strUsername, _
                                                strPassword, _
                                                cboDatabase.SelectedValue, _
                                                txtServerName.Text, _
                                                txtBackupLocation.Text & "\" & txtBackupFileName.Text) Then
                    Me.Cursor = Cursors.Arrow
                    ShowInformationMessage("Database Restored successfully")
                End If
            End If

        Catch ex As Exception
            Me.Cursor = Cursors.Arrow
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub optBackup_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optBackup.CheckedChanged, optRestore.CheckedChanged
        Try
            Dim rdoButton As RadioButton = CType(sender, RadioButton)
            If rdoButton.Name = "optBackup" Then
                btnBackup.Text = "Backup Now!"
            ElseIf rdoButton.Name = "optRestore" Then
                btnBackup.Text = "Restore Now!"
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
#End Region


End Class