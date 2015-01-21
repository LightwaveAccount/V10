''/////////////////////////////////////////////////////////////////////////////////////////
''//                      GL Net
''/////////////////////////////////////////////////////////////////////////////////////////
''//-------------------------------------------------------------------------------------
''// File Name       : frmDataTransfer.vb        				                            
''// Programmer	     : Waqas Anwar
''// Creation Date	 : 15-Aug-2013
''// Description     : 
''//                   
''//	
''//-------------------------------------------------------------------------------------
''// Date Modified     Modified by         Brief Description	
''// '20-sep-2013       Fatima Tajammal     CR # 277    Transfer GL Data: Some issues need to be fixed
''// '31-Dec-2014       M.Shoaib            CR # 352    Lightwave Tranfer Utility: Connected database should not populate in drop down
''//------------------------------------------------------------------------------------
''//

Imports DAL
Imports Model
Imports System.Collections.Specialized
Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports Utility.Utility
Public Class frmDataTransfer
    Implements IGeneral

#Region "Variables"
    ''This collection will hold the controls' names, upon which the logged in user has rights
    Private mobjControlList As NameValueCollection
    Dim strsql As String = String.Empty
#End Region

#Region "Interface Methods"
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(ByVal Mode As Utility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If Mode.ToString = EnumDataMode.Disabled.ToString Then

                btnNew.Enabled = False ': btnNew.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnSave.Enabled = False ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System           
                btnUpdate.Enabled = False ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnDelete.Enabled = False ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnCancel.Enabled = False ': btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue

                SetNavigationButtons(EnumDataMode.[New])
            End If
            'Cr # 277
            If mobjControlList.Item("btnTransfer") Is Nothing Then
                Me.btnTransfer.Enabled = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim dt As New DataTable
            Dim strquery As String = String.Empty
            Dim CurrentDB As String = System.Configuration.ConfigurationManager.AppSettings("StrDBName")
            'Cr # 277
            'strquery = "Select [Name] FROM master.dbo.sysdatabases"
            ' CR # 352
            'strquery = "Select [Name] FROM master.dbo.sysdatabases WHERE  name NOT IN ('master', 'tempdb', 'model', 'msdb')"
            strquery = "Select [Name] FROM master.dbo.sysdatabases WHERE  name NOT IN ('master', 'tempdb', 'model', 'msdb', '" & CurrentDB & "')"
            dt = New DAL.DALReports().GetDataTable(strquery)
            cboDatabases.DisplayMember = "Name"
            cboDatabases.ValueMember = "Name"
            cboDatabases.DataSource = dt


        Catch ex As Exception

        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional ByVal Mode As Utility.Utility.EnumDataMode = Utility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        If Me.rdoPartial.Checked = True Then
            If Me.chkCOA.Checked = False And Me.chkDefinition.Checked = False And Me.chkSecurity.Checked = False And Me.chkVoucher.Checked = False Then
                MessageBox.Show("Please select some Option.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If
            Return True
        End If
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

            'Me.btnPrint.ImageList = gobjMyImageListForOperationBar
            'Me.btnPrint.ImageKey = "Print"

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

#Region "Form Control Events"

    Private Sub frmDataTransfer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try

            If e.Control And e.KeyCode = Keys.X Then
                If Me.btnExit.Enabled = True Then Me.Close()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
    Private Sub frmDataTransfer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            mobjControlList = GetFormSecurityControls(Me.Name)

            FillCombos()
            SetButtonImages()
            ApplySecurity(EnumDataMode.Disabled)

            Me.rdoCmplt.Checked = True
            Me.chkCOA.Enabled = False
            Me.chkDefinition.Enabled = False
            Me.chkSecurity.Enabled = False
            Me.chkDCS.Enabled = False

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rdoCmplt_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoCmplt.CheckedChanged, rdoPartial.CheckedChanged
        Try
            If Me.rdoCmplt.Checked = True Then
                Me.GroupBox1.Enabled = False
                Me.rdoPartial.Checked = False
                Me.chkDefinition.Checked = False
                Me.chkCOA.Checked = False
                Me.chkSecurity.Checked = False
                Me.chkVoucher.Checked = False
                Me.chkDCS.Checked = False
                Me.chkDCS.Enabled = False
                Me.grpTransaction.Enabled = False
            ElseIf Me.rdoPartial.Checked = True Then
                Me.rdoCmplt.Checked = False
                Me.GroupBox1.Enabled = True
                Me.chkDCS.Enabled = True
                Me.grpTransaction.Enabled = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Private Sub btnTransfer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTransfer.Click

        Dim builder As New SqlConnectionStringBuilder(SQLHelper.CON_STR)
        Dim con As New SqlConnection(builder.ToString())
        con.Open()
        Application.DoEvents()
        Application.DoEvents()

        Dim trans As SqlTransaction = con.BeginTransaction
        
        Try
            'CR # 277
            'If MessageBox.Show("Are you sure to import database?", "Data base transfer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            If MessageBox.Show("Are you sure to transfer GL data?", "Data base transfer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                ShowProgressBar("Transferring GL Data")
                If Me.rdoCmplt.Checked = True Then
                    ''Deletion of Data From Tables
                    strsql = " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglvoucherdetailhistory " _
                    & " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglvoucherhistory " _
                    & " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglvoucherdetail " _
                    & " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglvoucher " _
                    & " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglcoamainsubsubdetail " _
                    & " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglcoamainsubsub " _
                    & " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglcoamainsub" _
                    & " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglcoamain " _
                    & " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecurityformright " _
                    & " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecurityuserlocation " _
                    & " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecurityuser " _
                    & " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecuritygroup " _
                    & " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecurityform " _
                    & " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecuritycontrolright " _
                    & " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecurityformcontrol " _
                    & " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblgldeffinancialyearstatus " _
                    & " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblgldeffinancialyear " _
                    & " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblgldeflocation " _
                    & " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblGlDefGLCostCenter " _
                    & " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblgldefglnotes " _
                    & " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblgldefvouchertype " _
                    & " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.cboglvouchertype " _
                    & " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblGlConfiguration "
                    Application.DoEvents()
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                    Application.DoEvents()

                    ''----------********Configurations*******----------
                    'tblGLConfiguration
                    strsql = " insert into " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblGLConfiguration " _
                    & "(config_no,config_name,config_value) " _
                    & " select config_no,config_name,config_value " _
                    & " from tblGLConfiguration "
                    Application.DoEvents()
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                    Application.DoEvents()

                    ''cboglvouchertype
                    strsql = " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.cboglvouchertype on" _
                    & " insert into " & Me.cboDatabases.SelectedValue.ToString & ".dbo.cboglvouchertype (" _
                    & " voucher_type_id,voucher_name,sort_order) select voucher_type_id,voucher_name,sort_order from cboglvouchertype " _
                    & " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.cboglvouchertype off "
                    Application.DoEvents()
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                    Application.DoEvents()

                    ''tblGlDefGLCostCenter
                    strsql = " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblGlDefGLCostCenter on " _
                    & " insert into " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblGlDefGLCostCenter (cost_center_id,cost_center_title,cost_center_type) " _
                    & " select cost_center_id,cost_center_title,cost_center_type from tblGlDefGLCostCenter " _
                    & " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblGlDefGLCostCenter off "
                    Application.DoEvents()
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                    Application.DoEvents()

                    ''tblgldefvouchertype
                    strsql = " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblgldefvouchertype on " _
                    & " insert into " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblgldefvouchertype (" _
                    & " voucher_type_id,voucher_type,comments,gl_type,sort_order,read_only) " _
                    & " select voucher_type_id,voucher_type,comments,gl_type,sort_order,read_only from tblgldefvouchertype " _
                    & " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblgldefvouchertype off "
                    Application.DoEvents()
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                    Application.DoEvents()

                    ''tblgldefglnotes
                    strsql = " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblgldefglnotes on" _
                    & " insert into " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblgldefglnotes (" _
                    & " gl_note_id,note_title,note_type,note_no,sort_order) select gl_note_id,note_title,note_type,note_no,sort_order from tblgldefglnotes" _
                    & " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblgldefglnotes off"
                    Application.DoEvents()
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                    Application.DoEvents()

                    ''tblgldeflocation
                    strsql = " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblgldeflocation on " _
                    & " insert into " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblgldeflocation (location_id,location_code,location_name,comments,sort_order,location_address,location_phone,location_fax,location_url)" _
                    & " select location_id,location_code,location_name,comments,sort_order,location_address,location_phone,location_fax,location_url from tblgldeflocation " _
                    & " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblgldeflocation off"
                    Application.DoEvents()
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                    Application.DoEvents()

                    ''Drop Trigger on tblgldeffinancialyear
                    strsql = " use " & Me.cboDatabases.SelectedValue.ToString & "; IF EXISTS(SELECT * FROM sys.triggers WHERE Name = 'trg_Location_Fyears') " _
                    & " DROP TRIGGER  [trg_Location_Fyears] "
                    Application.DoEvents()
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                    Application.DoEvents()

                    ''Use Source Database
                    strsql = " use " & builder.InitialCatalog.ToString & "; "
                    Application.DoEvents()
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                    Application.DoEvents()

                    ''tblgldeffinancialyear
                    strsql = " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblgldeffinancialyear on " _
                    & " insert into " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblgldeffinancialyear (" _
                    & " financial_year_id,start_date,end_date,year_code,status,location_id)" _
                    & " select financial_year_id,start_date,end_date,year_code,status,location_id from tblgldeffinancialyear " _
                    & " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblgldeffinancialyear off "
                    Application.DoEvents()
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                    Application.DoEvents()

                    ''tblgldeffinancialyearstatus
                    strsql = " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblgldeffinancialyearstatus on " _
                    & " insert into " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblgldeffinancialyearstatus (" _
                    & " financial_year_status_id,financial_year_id,status,location_id)" _
                    & " select financial_year_status_id,financial_year_id,status,location_id from tblgldeffinancialyearstatus " _
                    & " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblgldeffinancialyearstatus off "
                    Application.DoEvents()
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                    Application.DoEvents()

                    ''---------------*************Security*************---------------
                    ''tblglsecurityform
                    strsql = " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecurityform on " _
                    & " insert into " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecurityform (form_id,form_name,form_label) " _
                    & " select form_id,form_name,form_label from tblglsecurityform " _
                    & " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecurityform off "
                    Application.DoEvents()
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                    Application.DoEvents()

                    ''tblglsecuritycontrolright
                    strsql = " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecuritycontrolright on " _
                    & " insert into " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecuritycontrolright (RightsID,GroupID,ControlID)" _
                    & " select RightsID,GroupID,ControlID from tblglsecuritycontrolright " _
                    & " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecuritycontrolright off "
                    Application.DoEvents()
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                    Application.DoEvents()

                    ''tblglsecurityformcontrol
                    strsql = " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecurityformcontrol " _
                    & " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecurityformcontrol on " _
                    & " insert into " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecurityformcontrol (ControlID,FormID,ControlCaption,ControlName) " _
                    & " select ControlID,FormID,ControlCaption,ControlName from tblglsecurityformcontrol " _
                    & " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecurityformcontrol off "
                    Application.DoEvents()
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                    Application.DoEvents()

                    ''tblglsecuritygroup
                    strsql = " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecuritygroup on " _
                    & " insert into " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecuritygroup (" _
                    & " group_id,group_name,group_type,group_comments) select group_id,group_name,group_type,group_comments from tblglsecuritygroup " _
                    & " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecuritygroup off "
                    Application.DoEvents()
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                    Application.DoEvents()

                    ''tblglsecurityuser
                    strsql = " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecurityuser on " _
                    & " insert into " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecurityuser (" _
                    & " user_id,group_id,user_name,user_log_id,user_log_password,user_email,user_comments,location_id,Block,Mobile_No) " _
                    & " select user_id,group_id,user_name,user_log_id,user_log_password,user_email,user_comments,location_id,Block,Mobile_No from tblglsecurityuser" _
                    & " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecurityuser off "
                    Application.DoEvents()
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                    Application.DoEvents()

                    ''tblglsecurityuserlocation
                    strsql = " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecurityuserlocation on " _
                    & " insert into " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecurityuserlocation (user_location_id,user_id,location_id) " _
                    & " select user_location_id,user_id,location_id from tblglsecurityuserlocation " _
                    & " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecurityuserlocation off "
                    Application.DoEvents()
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                    Application.DoEvents()

                    ''tblglsecurityformright
                    strsql = " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecurityformright on " _
                    & " insert into " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecurityformright (" _
                    & " form_right_id,group_id,form_id,views,saves,updates,deletes,prints,exports,Post) " _
                    & " select form_right_id,group_id,form_id,views,saves,updates,deletes,prints,exports,Post from tblglsecurityformright " _
                    & " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecurityformright off "
                    Application.DoEvents()
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                    Application.DoEvents()

                    ''-----------------***********Chart OF Account**********-----------
                    ''tblglcoamain
                    strsql = " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglcoamain on " _
                    & " insert into " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglcoamain (coa_main_id,main_code,main_title,main_type) " _
                    & " select coa_main_id,main_code,main_title,main_type from tblglcoamain " _
                    & " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglcoamain off "
                    Application.DoEvents()
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                    Application.DoEvents()

                    ''tblglcoamainsub
                    strsql = " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglcoamainsub on " _
                    & " insert into " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglcoamainsub (main_sub_id,coa_main_id,sub_code,sub_title)" _
                    & " select main_sub_id,coa_main_id,sub_code,sub_title from tblglcoamainsub" _
                    & " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglcoamainsub off "
                    Application.DoEvents()
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                    Application.DoEvents()

                    ''tblglcoamainsubsub
                    strsql = " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglcoamainsubsub on " _
                    & " insert into " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglcoamainsubsub (" _
                    & " main_sub_sub_id,main_sub_id,sub_sub_code,sub_sub_title,account_type,DrBS_note_id,CrBS_note_id,PL_note_id)" _
                    & " select main_sub_sub_id,main_sub_id,sub_sub_code,sub_sub_title,account_type,DrBS_note_id,CrBS_note_id,PL_note_id from tblglcoamainsubsub " _
                    & " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglcoamainsubsub off "
                    Application.DoEvents()
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                    Application.DoEvents()

                    ''tblglcoamainsubsubdetail
                    strsql = " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglcoamainsubsubdetail on " _
                    & " insert into " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglcoamainsubsubdetail (" _
                    & " coa_detail_id,main_sub_sub_id,detail_code,detail_title,end_date) " _
                    & " select coa_detail_id,main_sub_sub_id,detail_code,detail_title,end_date from tblglcoamainsubsubdetail " _
                    & " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglcoamainsubsubdetail off "
                    Application.DoEvents()
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                    Application.DoEvents()

                    ''---------------***********Voucher***********-----------------
                    ''tblglvoucher
                    strsql = " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglvoucher on " _
                    & " insert into " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglvoucher (voucher_id,location_id,voucher_code,finiancial_year_id," _
                    & " voucher_type_id,voucher_month,voucher_no,voucher_date,paid_to,coa_detail_id,cheque_no,cheque_date,cheque_paid,cheque_paid_date,post,other_voucher," _
                    & " source,cheque_credited,temp_voucher_id,due_date,shop_id,shop_code) select voucher_id,location_id,voucher_code,finiancial_year_id," _
                    & " voucher_type_id,voucher_month,voucher_no,voucher_date,paid_to,coa_detail_id,cheque_no,cheque_date,cheque_paid,cheque_paid_date,post,other_voucher," _
                    & " source,cheque_credited,temp_voucher_id,due_date,shop_id,shop_code from tblglvoucher " _
                    & " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglvoucher off "
                    Application.DoEvents()
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                    Application.DoEvents()

                    ''tblglvoucherdetail
                    strsql = " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglvoucherdetail on " _
                    & " insert into " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglvoucherdetail (" _
                    & " voucher_detail_id,voucher_id,location_id,coa_detail_id,comments,debit_amount,credit_amount,cost_center_id,sp_refrence,direction,shop_id) " _
                    & " select voucher_detail_id,voucher_id,location_id,coa_detail_id,comments,debit_amount,credit_amount,cost_center_id,sp_refrence,direction,shop_id from tblglvoucherdetail" _
                    & " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglvoucherdetail off "
                    Application.DoEvents()
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                    Application.DoEvents()
                    ''tblglvoucherhistory
                    strsql = " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglvoucherhistory on " _
                    & " insert into " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglvoucherhistory (id,voucher_id,location_id,voucher_code,finiancial_year_id," _
                    & " voucher_type_id,voucher_month,voucher_no,voucher_date,paid_to,coa_detail_id,cheque_no,cheque_date,cheque_paid,cheque_paid_date,post," _
                    & " other_voucher,source,cheque_credited,temp_voucher_id,due_date,shop_id,shop_code,Action,Action_date) select id,voucher_id,location_id," _
                    & " voucher_code,finiancial_year_id,voucher_type_id,voucher_month,voucher_no,voucher_date,paid_to,coa_detail_id,cheque_no,cheque_date," _
                    & " cheque_paid,cheque_paid_date,post,other_voucher,source,cheque_credited,temp_voucher_id,due_date,shop_id,shop_code,Action,Action_date from tblglvoucherhistory " _
                    & " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglvoucherhistory off "
                    Application.DoEvents()
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                    Application.DoEvents()
                    Bar.Value += 1

                    ''tblglvoucherdetailhistory
                    strsql = " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglvoucherdetailhistory on " _
                    & " insert into " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglvoucherdetailhistory (VDetail_id,id,voucher_detail_id,voucher_id," _
                    & " location_id,coa_detail_id,comments,debit_amount,credit_amount,cost_center_id,sp_refrence,direction,shop_id,Action,Action_date) " _
                    & " select VDetail_id,id,voucher_detail_id,voucher_id,location_id,coa_detail_id,comments,debit_amount,credit_amount,cost_center_id,sp_refrence,direction,shop_id,Action,Action_date from tblglvoucherdetailhistory " _
                    & " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglvoucherdetailhistory off "
                    Application.DoEvents()
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                    Application.DoEvents()

                    ''Commiting Transaction

                ElseIf Me.rdoPartial.Checked = True Then

                    If Not IsValidate() Then Exit Sub

                    If Me.chkDefinition.Checked = True Then
                        strsql = " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglvoucherdetailhistory " _
                        & " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglvoucherhistory " _
                        & " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglvoucherdetail " _
                        & " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglvoucher " _
                        & " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglcoamainsubsubdetail " _
                        & " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglcoamainsubsub " _
                        & " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglcoamainsub" _
                        & " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglcoamain " _
                        & " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecurityformright " _
                        & " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecurityuserlocation " _
                        & " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecurityuser " _
                        & " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecuritygroup " _
                        & " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecurityform " _
                        & " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecuritycontrolright " _
                        & " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecurityformcontrol " _
                        & " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblgldeffinancialyearstatus " _
                        & " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblgldeffinancialyear " _
                        & " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblgldeflocation " _
                        & " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblGlDefGLCostCenter " _
                        & " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblgldefglnotes " _
                        & " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblgldefvouchertype " _
                        & " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.cboglvouchertype " _
                        & " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblGlConfiguration "
                        Application.DoEvents()
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                        Application.DoEvents()
                    End If

                    If Me.chkVoucher.Checked = True Then
                        strsql = " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglvoucherdetailhistory " _
                        & " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglvoucherhistory " _
                        & " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglvoucherdetail " _
                        & " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglvoucher "
                        Application.DoEvents()
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                        Application.DoEvents()
                    End If

                    If Me.chkDefinition.Checked = True Then
                        ''----------********Configurations*******----------
                        'tblGLConfiguration
                        strsql = " insert into " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblGLConfiguration " _
                        & "(config_no,config_name,config_value) " _
                        & " select config_no,config_name,config_value " _
                        & " from tblGLConfiguration "
                        Application.DoEvents()
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                        Application.DoEvents()

                        ''cboglvouchertype
                        strsql = " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.cboglvouchertype on" _
                        & " insert into " & Me.cboDatabases.SelectedValue.ToString & ".dbo.cboglvouchertype (" _
                        & " voucher_type_id,voucher_name,sort_order) select voucher_type_id,voucher_name,sort_order from cboglvouchertype " _
                        & " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.cboglvouchertype off "
                        Application.DoEvents()
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                        Application.DoEvents()

                        ''tblGlDefGLCostCenter
                        strsql = " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblGlDefGLCostCenter on " _
                        & " insert into " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblGlDefGLCostCenter (cost_center_id,cost_center_title,cost_center_type) " _
                        & " select cost_center_id,cost_center_title,cost_center_type from tblGlDefGLCostCenter " _
                        & " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblGlDefGLCostCenter off "
                        Application.DoEvents()
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                        Application.DoEvents()

                        ''tblgldefvouchertype
                        strsql = " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblgldefvouchertype on " _
                        & " insert into " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblgldefvouchertype (" _
                        & " voucher_type_id,voucher_type,comments,gl_type,sort_order,read_only) " _
                        & " select voucher_type_id,voucher_type,comments,gl_type,sort_order,read_only from tblgldefvouchertype " _
                        & " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblgldefvouchertype off "
                        Application.DoEvents()
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                        Application.DoEvents()

                        ''tblgldefglnotes
                        strsql = " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblgldefglnotes on" _
                        & " insert into " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblgldefglnotes (" _
                        & " gl_note_id,note_title,note_type,note_no,sort_order) select gl_note_id,note_title,note_type,note_no,sort_order from tblgldefglnotes" _
                        & " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblgldefglnotes off"
                        Application.DoEvents()
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                        Application.DoEvents()

                        ''tblgldeflocation
                        strsql = " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblgldeflocation on " _
                        & " insert into " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblgldeflocation (location_id,location_code,location_name,comments,sort_order,location_address,location_phone,location_fax,location_url)" _
                        & " select location_id,location_code,location_name,comments,sort_order,location_address,location_phone,location_fax,location_url from tblgldeflocation " _
                        & " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblgldeflocation off"
                        Application.DoEvents()
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                        Application.DoEvents()

                        ''tblgldeffinancialyear
                        strsql = " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblgldeffinancialyear on " _
                        & " insert into " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblgldeffinancialyear (" _
                        & " financial_year_id,start_date,end_date,year_code,status,location_id)" _
                        & " select financial_year_id,start_date,end_date,year_code,status,location_id from tblgldeffinancialyear" _
                        & " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblgldeffinancialyear off "
                        Application.DoEvents()
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                        Application.DoEvents()

                        ''tblgldeffinancialyearstatus
                        strsql = " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblgldeffinancialyearstatus on " _
                        & " insert into " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblgldeffinancialyearstatus (" _
                        & " financial_year_status_id,financial_year_id,status,location_id)" _
                        & " select financial_year_status_id,financial_year_id,status,location_id from tblgldeffinancialyearstatus " _
                        & " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblgldeffinancialyearstatus off "
                        Application.DoEvents()
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                        Application.DoEvents()
                    End If

                    If Me.chkSecurity.Checked = True Then
                        ''---------------*************Security*************---------------
                        ''tblglsecurityform
                        strsql = " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecurityform on " _
                        & " insert into " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecurityform (form_id,form_name,form_label) " _
                        & " select form_id,form_name,form_label from tblglsecurityform " _
                        & " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecurityform off "
                        Application.DoEvents()
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                        Application.DoEvents()

                        ''tblglsecuritycontrolright
                        strsql = " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecuritycontrolright on " _
                        & " insert into " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecuritycontrolright (RightsID,GroupID,ControlID)" _
                        & " select RightsID,GroupID,ControlID from tblglsecuritycontrolright " _
                        & " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecuritycontrolright off "
                        Application.DoEvents()
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                        Application.DoEvents()

                        ''tblglsecurityformcontrol
                        strsql = " delete from " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecurityformcontrol " _
                        & " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecurityformcontrol on " _
                        & " insert into " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecurityformcontrol (ControlID,FormID,ControlCaption,ControlName) " _
                        & " select ControlID,FormID,ControlCaption,ControlName from tblglsecurityformcontrol " _
                        & " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecurityformcontrol off "
                        Application.DoEvents()
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                        Application.DoEvents()

                        ''tblglsecuritygroup
                        strsql = " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecuritygroup on " _
                        & " insert into " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecuritygroup (" _
                        & " group_id,group_name,group_type,group_comments) select group_id,group_name,group_type,group_comments from tblglsecuritygroup " _
                        & " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecuritygroup off "
                        Application.DoEvents()
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                        Application.DoEvents()

                        ''tblglsecurityuser
                        strsql = " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecurityuser on " _
                        & " insert into " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecurityuser (" _
                        & " user_id,group_id,user_name,user_log_id,user_log_password,user_email,user_comments,location_id,Block,Mobile_No) " _
                        & " select user_id,group_id,user_name,user_log_id,user_log_password,user_email,user_comments,location_id,Block,Mobile_No from tblglsecurityuser" _
                        & " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecurityuser off "
                        Application.DoEvents()
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                        Application.DoEvents()

                        ''tblglsecurityuserlocation
                        strsql = " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecurityuserlocation on " _
                        & " insert into " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecurityuserlocation (user_location_id,user_id,location_id) " _
                        & " select user_location_id,user_id,location_id from tblglsecurityuserlocation " _
                        & " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecurityuserlocation off "
                        Application.DoEvents()
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                        Application.DoEvents()

                        ''tblglsecurityformright
                        strsql = " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecurityformright on " _
                        & " insert into " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecurityformright (" _
                        & " form_right_id,group_id,form_id,views,saves,updates,deletes,prints,exports,Post) " _
                        & " select form_right_id,group_id,form_id,views,saves,updates,deletes,prints,exports,Post from tblglsecurityformright " _
                        & " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglsecurityformright off "
                        Application.DoEvents()
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                        Application.DoEvents()
                    End If

                    If Me.chkCOA.Checked = True Then
                        ''-----------------***********Chart OF Account**********-----------
                        ''tblglcoamain
                        strsql = " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglcoamain on " _
                        & " insert into " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglcoamain (coa_main_id,main_code,main_title,main_type) " _
                        & " select coa_main_id,main_code,main_title,main_type from tblglcoamain " _
                        & " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglcoamain off "
                        Application.DoEvents()
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                        Application.DoEvents()

                        ''tblglcoamainsub
                        strsql = " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglcoamainsub on " _
                        & " insert into " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglcoamainsub (main_sub_id,coa_main_id,sub_code,sub_title)" _
                        & " select main_sub_id,coa_main_id,sub_code,sub_title from tblglcoamainsub" _
                        & " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglcoamainsub off "
                        Application.DoEvents()
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                        Application.DoEvents()

                        ''tblglcoamainsubsub
                        strsql = " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglcoamainsubsub on " _
                        & " insert into " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglcoamainsubsub (" _
                        & " main_sub_sub_id,main_sub_id,sub_sub_code,sub_sub_title,account_type,DrBS_note_id,CrBS_note_id,PL_note_id)" _
                        & " select main_sub_sub_id,main_sub_id,sub_sub_code,sub_sub_title,account_type,DrBS_note_id,CrBS_note_id,PL_note_id from tblglcoamainsubsub " _
                        & " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglcoamainsubsub off "
                        Application.DoEvents()
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                        Application.DoEvents()

                        ''tblglcoamainsubsubdetail
                        strsql = " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglcoamainsubsubdetail on " _
                        & " insert into " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglcoamainsubsubdetail (" _
                        & " coa_detail_id,main_sub_sub_id,detail_code,detail_title,end_date) " _
                        & " select coa_detail_id,main_sub_sub_id,detail_code,detail_title,end_date from tblglcoamainsubsubdetail " _
                        & " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglcoamainsubsubdetail off "
                        Application.DoEvents()
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                        Application.DoEvents()
                    End If

                    If Me.chkVoucher.Checked = True Then
                        ''---------------***********Voucher***********-----------------
                        ''tblglvoucher
                        strsql = " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglvoucher on " _
                        & " insert into " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglvoucher (voucher_id,location_id,voucher_code,finiancial_year_id," _
                        & " voucher_type_id,voucher_month,voucher_no,voucher_date,paid_to,coa_detail_id,cheque_no,cheque_date,cheque_paid,cheque_paid_date,post,other_voucher," _
                        & " source,cheque_credited,temp_voucher_id,due_date,shop_id,shop_code) select voucher_id,location_id,voucher_code,finiancial_year_id," _
                        & " voucher_type_id,voucher_month,voucher_no,voucher_date,paid_to,coa_detail_id,cheque_no,cheque_date,cheque_paid,cheque_paid_date,post,other_voucher," _
                        & " source,cheque_credited,temp_voucher_id,due_date,shop_id,shop_code from tblglvoucher " _
                        & " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglvoucher off "
                        Application.DoEvents()
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                        Application.DoEvents()

                        ''tblglvoucherdetail
                        strsql = " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglvoucherdetail on " _
                        & " insert into " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglvoucherdetail (" _
                        & " voucher_detail_id,voucher_id,location_id,coa_detail_id,comments,debit_amount,credit_amount,cost_center_id,sp_refrence,direction,shop_id) " _
                        & " select voucher_detail_id,voucher_id,location_id,coa_detail_id,comments,debit_amount,credit_amount,cost_center_id,sp_refrence,direction,shop_id from tblglvoucherdetail" _
                        & " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglvoucherdetail off "
                        Application.DoEvents()
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                        Application.DoEvents()

                        ''tblglvoucherhistory
                        strsql = " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglvoucherhistory on " _
                        & " insert into " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglvoucherhistory (id,voucher_id,location_id,voucher_code,finiancial_year_id," _
                        & " voucher_type_id,voucher_month,voucher_no,voucher_date,paid_to,coa_detail_id,cheque_no,cheque_date,cheque_paid,cheque_paid_date,post," _
                        & " other_voucher,source,cheque_credited,temp_voucher_id,due_date,shop_id,shop_code,Action,Action_date) select id,voucher_id,location_id," _
                        & " voucher_code,finiancial_year_id,voucher_type_id,voucher_month,voucher_no,voucher_date,paid_to,coa_detail_id,cheque_no,cheque_date," _
                        & " cheque_paid,cheque_paid_date,post,other_voucher,source,cheque_credited,temp_voucher_id,due_date,shop_id,shop_code,Action,Action_date from tblglvoucherhistory " _
                        & " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglvoucherhistory off "
                        Application.DoEvents()
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                        Application.DoEvents()

                        ''tblglvoucherdetailhistory
                        strsql = " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglvoucherdetailhistory on " _
                        & " insert into " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglvoucherdetailhistory (VDetail_id,id,voucher_detail_id,voucher_id," _
                        & " location_id,coa_detail_id,comments,debit_amount,credit_amount,cost_center_id,sp_refrence,direction,shop_id,Action,Action_date) " _
                        & " select VDetail_id,id,voucher_detail_id,voucher_id,location_id,coa_detail_id,comments,debit_amount,credit_amount,cost_center_id,sp_refrence,direction,shop_id,Action,Action_date from tblglvoucherdetailhistory " _
                        & " set identity_insert " & Me.cboDatabases.SelectedValue.ToString & ".dbo.tblglvoucherdetailhistory off "
                        Application.DoEvents()
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, Nothing)
                        Application.DoEvents()

                    End If
                End If
                trans.Commit()
                EndProgressBar()
                MessageBox.Show("Successfully Transferred.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information)

            End If

        Catch ex As Exception
            EndProgressBar()
            ShowErrorMessage(ex.Message)
        Finally
            con.Close()
            EndProgressBar()
        End Try
    End Sub

    Private Sub chkDCS_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkDCS.CheckedChanged
        If Me.chkDCS.Checked = True Then
            Me.chkCOA.Checked = True
            Me.chkDefinition.Checked = True
            Me.chkSecurity.Checked = True
        Else
            Me.chkCOA.Checked = False
            Me.chkDefinition.Checked = False
            Me.chkSecurity.Checked = False
        End If

    End Sub

#End Region

End Class