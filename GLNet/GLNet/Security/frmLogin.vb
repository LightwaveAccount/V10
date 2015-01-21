'////////////////////////////////////////////////////////////////////////////
'//              LOGIN FOR THE SYSTEM                                   //
'////////////////////////////////////////////////////////////////////////////
'//                                                                        //
'//     Programmer : Rizwan Asif                                    //
'//     Creation Date : 20/Jul/2009                                    //
'//     Modified by :                                                      //
'//     Modification Date :                                                //
'//     Commented by : 
'//     Orignal File name : frmLogin                             //
'//     Version 1.0                                                        //
'//     Description :                                                      //
'//                                                                        //
'//         
'//updated by A Jabbar--04 Feb,2010 Mostly changes belongs to Release Managment/Release Update
'// Rizwan ASif         30-May2011 CR #  141 Email functionality                                                                //
'//                                                                        //
'////////////////////////////////////////////////////////////////////////////

Imports Model
Imports DAL
Imports Utility.Utility
Imports System.Data.SqlClient


Public Class frmLogin


#Region "Local Functions and Procedures"

    Public Sub New()

        Try

            '' This call is required by the Windows Form Designer.
            InitializeComponent()

            'Creating MushRoom common table if it doesn't exist (it will be commented when we will be sure Released has been updated on all clients) 15 Feb,2010
            'DAL.UtilityDAL.CreateMushRoomCommon()

            'Call AddMushroomCommonListToHashTable()

            Dim strConnectionString As String = SQLHelper.CON_STR
            If Not strConnectionString.Contains("Data Source") Then
                strConnectionString = Utility.Utility.SymmetricEncryption.Decrypt(strConnectionString, "f")
                SQLHelper.CON_STR = strConnectionString
            End If


            ''Encryption Method changed from release 3001
            ''09-Mar-2010 /Abdul Jabbar
            Dim strMsgFromEncryption As String = ""
            Dim FILE_NAME As String = "UpdateLog"
            Try
                Call ChangeEncryption(strMsgFromEncryption)

            Catch ex As Exception
            Finally
                If strMsgFromEncryption.Trim <> "" Then
                    Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "Encryption Log: ", "6019")
                    Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, strMsgFromEncryption, "6019")
                    Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===============================================================", "6019")
                    Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "Update Date: " & CBOCollectionDAL.GetServerDateTime().Date & "", "6019")
                End If
            End Try


            '' Add any initialization after the InitializeComponent() call.
            ''=========================
            ''Start DB Version Forwarding
            '===============================================================
            '03 Feb,2010    Abdul Jabbar
            '===============================================================
            Dim dblSchemaVersion As Double
            Dim dblReleaseVersion As Double
            Dim dblVerArray() As Double

            dblReleaseVersion = Trim(Application.ProductVersion).Replace(".", "")
            dblSchemaVersion = Val(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Schema_Version"))

            Try

                dblVerArray = GetVersionsDelta(dblSchemaVersion, dblReleaseVersion)

            Catch ex As Exception
                If ex.InnerException.ToString = "System.Exception: Version Is Not Compatible" Then
                    ShowErrorMessage(ex.Message)
                    Me.Close()
                    End
                End If
            End Try

            Try
                'check if array is not blank then
                If Not dblVerArray Is Nothing Then

                    If UBound(dblVerArray) > 0 Then

                        Application.DoEvents()

                        'Dim MyVersion As String = DecryptWithALP(GetSystemConfigurationValue("Version"))

                        'Update Patch should be based on following condition.

                        'If (IsReplicationDone() And (Not IsPublisher(System.Configuration.ConfigurationManager.AppSettings("StrDBServerName").ToString))) Then
                        '    '    ''Do Nothing in case of Replicated Shops
                        '    'MessageBox.Show("ad")
                        'Else

                        ShowProgressBar("Please Wait, It may take few minutes.")
                        Application.DoEvents()
                        '    ''Or (MyVersion = "1") Then
                        Dim intCounter As Integer
                        For intCounter = LBound(dblVerArray) To UBound(dblVerArray) - 1
                            If Not funVersionUpdate(dblVerArray(intCounter)) Then Exit For
                            Application.DoEvents()
                        Next

                        EndProgressBar()
                        Application.DoEvents()
                    End If
                End If
                ' End If

            Catch ex As Exception
                ShowErrorMessage(ex.Message)
            Finally
                EndProgressBar()
                Application.DoEvents()
            End Try

            Call AddSystemConfigurationListToHashTable()

            dblSchemaVersion = Val(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Schema_Version"))

            If dblReleaseVersion > dblSchemaVersion Then
                If ShowConfirmationMessage("Current Database Schema is not compatible with this Lightwave Release ! You want to continue", MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
                    'Me.Close()
                    End
                End If
            End If
            ''''"DB Version Forwarding"
            ''=========================
            ''End
            '==========================


            '******************
            'Done By Fahad
            '******************

            Dim strServerName As String = System.Configuration.ConfigurationManager.AppSettings("StrDBServerName")
            If Not New RegisterProductDAL().ValidateGLLicense(strServerName) Then
                Utility.Utility.IsTrialVersion = True
                Dim objRegisterProduct As New frmRegisterProduct
                objRegisterProduct.StartPosition = FormStartPosition.CenterScreen
                Call ApplyStyleSheet(objRegisterProduct, objRegisterProduct.Name)
                objRegisterProduct.ShowDialog()
            Else
                Utility.Utility.IsTrialVersion = False
            End If
            'This Global Variable is used in application to check Version is Trial or not
            gblnTrialVersion = Utility.Utility.IsTrialVersion

            Me.BackColor = Color.LightBlue
            ' Me.TransparencyKey = Color.LightBlue
            ' Me.Panel1.BackColor = Color.Transparent
            Me.Visible = True

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
            End

        End Try

    End Sub
    Public Shared Function IsReplicationDone() As Boolean
        ' method will check the value of replication_status from table tblMushroomCommon
        Dim dt As New DataTable
        Try
            dt = CType(gObjMyAppHashTable.Item(EnumHashTableKeyConstants.GetMushroomCommonList.ToString()), DataTable)
            If dt.Rows.Count > 0 Then
                'gblnIsReplicationDone = CBool(dt.Rows(0)("replication status"))
                IsReplicationDone = CBool(dt.Rows(0)("replication status"))
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        Finally
            dt = Nothing
        End Try

    End Function
    Public Shared Function IsPublisher(Optional ByVal ServerName As String = ".") As Boolean
        ' Function takes ServerName or system name as argument and will return a flag
        ' that will tell that passed (argument) system is Publisher or not.
        Try
            Dim sql As String
            Dim strReturn As String

            sql = "select name from master..sysdatabases where name='distribution'"
            strReturn = SQLHelper.ExecuteScaler(SQLHelper.CON_STR, CommandType.Text, sql, Nothing)

            If strReturn <> "" Then
                IsPublisher = True
            Else
                IsPublisher = False
            End If

            Exit Function

        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, "Error")
        End Try

    End Function

    Private Function LoginUser() As Boolean

        Dim objUserDAL As SecurityUserDAL

        Try
            objUserDAL = New SecurityUserDAL

            Dim strUserLoginID As String = txtLoginID.Text
            Dim strUserLoginPassword As String = txtLoginPassword.Text

            ' Call DAL Function ..
            gObjUserInfo = objUserDAL.FindValidUser(strUserLoginID, strUserLoginPassword)

            FillApplicationHashTable()

            'CR#160 
            '///Service broker variables intialization ,error/exception is expected due to missing TblRCMSConfiguration or value in it or any other object missing because
            '//we are reading from candela database/tables which may not exist (in existing clients dbs) in GL DB.
            Try

                G_Candela_Version = 1
                G_IsSSBOn = False
                UtilityDAL.SETDataLogFlag()

            Catch ex As Exception
                'MessageBox.Show("Exception Setting Data log variables")
            End Try

            Return True

        Catch ex As Exception
            Throw ex
        Finally
            objUserDAL = Nothing
        End Try

    End Function

    Private Sub FillCombos()
        Try

            Dim strSql As String
            strSql = "SELECT '[ ' + tblGlDefLocation.location_code + ' ] ' + tblGlDefFinancialYear.year_code AS FYear, tblGlDefFinancialYearStatus.financial_year_status_id FROM tblGlDefLocation INNER JOIN tblGlDefFinancialYearStatus ON tblGlDefLocation.location_id = tblGlDefFinancialYearStatus.location_id RIGHT OUTER JOIN " _
                        & " tblGlDefFinancialYear ON tblGlDefFinancialYearStatus.financial_year_id = tblGlDefFinancialYear.financial_year_id " _
                        & " WHERE (tblGlDefFinancialYearStatus.status <> 'Closed') ORDER BY tblGlDefLocation.sort_order , tblGlDefFinancialYear.year_code DESC"

            Dim dt As DataTable = UtilityDAL.GetDataTable(strSql)

            Dim dr As DataRow = dt.NewRow
            dr.Item(0) = gstrComboZeroIndexString
            dr.Item(1) = 0
            dt.Rows.InsertAt(dr, 0)
            Me.cboFinancialYear.DisplayMember = "FYear"
            Me.cboFinancialYear.ValueMember = "financial_year_status_id"
            Me.cboFinancialYear.DataSource = dt
            cboFinancialYear.SelectedIndex = IIf(cboFinancialYear.Items.Count > 1, 1, 0)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region


#Region "Form Controls Events"


    
    
#End Region

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click, btnEnd.Click

        Dim blnShowFYearForm As Boolean

        Dim btn As Label = CType(sender, Label)

        Try

            If btn.Name = btnLogin.Name Then

                btn.Enabled = False


                If LoginUser() Then

                    txtLoginPassword.Text = ""
                    If cboFinancialYear.Items.Count > 1 AndAlso cboFinancialYear.SelectedIndex = 0 Then
                        ShowInformationMessage("Please Select Financial Year")
                        cboFinancialYear.Focus()
                        blnShowFYearForm = False
                        Exit Sub
                    ElseIf cboFinancialYear.Items.Count = 1 Then
                        blnShowFYearForm = True
                        'MsgBoax "To start using this Application, first define Financial Year", vbInformation, "Define F.Year"
                        ShowInformationMessage("To start using this Application, first define Company and Financial Year")
                    End If

                    'Set the Global Variables when user is Authenticated
                    Dim lngFinancialYearStatusID As Long = 0L
                    Dim lngLocationId As Long = 0L

                    lngFinancialYearStatusID = cboFinancialYear.SelectedValue

                    If Not GetUserLocationRights(Me.txtLoginID.Text.Trim, lngFinancialYearStatusID) AndAlso cboFinancialYear.Items.Count > 1 Then

                        ShowErrorMessage("You have no right on selected location. Please select the other Location")
                        cboFinancialYear.Focus()
                        blnShowFYearForm = False
                        Exit Sub
                    End If


                    Dim strSql As String
                    Dim dtFind As DataTable


                    strSql = "SELECT tblGlDefFinancialYearStatus.financial_year_id, tblGlDefFinancialYear.year_code, tblGlDefFinancialYearStatus.location_id " _
                            & " FROM tblGlDefFinancialYearStatus INNER JOIN tblGlDefFinancialYear ON tblGlDefFinancialYearStatus.financial_year_id = tblGlDefFinancialYear.financial_year_id " _
                            & " WHERE (tblGlDefFinancialYearStatus.financial_year_status_id = " & lngFinancialYearStatusID & " )"

                    dtFind = UtilityDAL.GetDataTable(strSql)

                    gObjFinancialYearInfo = New FiniancialYear

                    If Not dtFind.Rows.Count = 0 Then
                        gObjFinancialYearInfo.FYearID = dtFind.Rows(0)("financial_year_id")
                        gObjFinancialYearInfo.YearCode = dtFind.Rows(0)("year_code")
                    Else
                        gObjFinancialYearInfo.FYearID = 0
                        gObjFinancialYearInfo.YearCode = "0000-0000"
                    End If

                    ''get Business start date
                    Dim dr As DataRow = UtilityDAL.ReturnDataRow(" SELECT Min(Start_Date) AS Date From tblGLDefFinancialYear")

                    If (Not dr Is Nothing) AndAlso Not IsDBNull(dr.Item(0)) Then
                        gobjBusinessStartDate = dr.Item(0)
                    End If

                    Me.SetFinancialYearVariables(lngFinancialYearStatusID)
                    Me.SetUserLocation(txtLoginID.Text.Trim, lngFinancialYearStatusID)

                    If Microsoft.VisualBasic.Left(gObjFinancialYearInfo.YearCode, 4) = Microsoft.VisualBasic.Left(gObjFinancialYearInfo.YearCode, 4) Then
                        G_START_DATE = "01/01"
                        G_END_DATE = "31/12"
                    Else

                        G_START_DATE = "01/07"
                        G_END_DATE = "30/06"
                    End If




                    ''sets the images in gloal imaglist control
                    Call SetMyImageList()


                    ''Following code is written by Khalid to get application startup path for rpt  reports
                    gstrReportPath = Application.StartupPath & "\Reports"

                    ''Dim strIsIntegrated As String = SystemConfigurationDAL.GetSystemConfigurationValue("Integration_Of_Sale_Purchase")
                    ''If strIsIntegrated = "" Then
                    ''    gblnIsIntegratedWithSalesAndPurchase = False
                    ''Else
                    ''    gblnIsIntegratedWithSalesAndPurchase = Utility.Utility.SymmetricEncryption.Decrypt(SystemConfigurationDAL.GetSystemConfigurationValue("Integration_Of_Sale_Purchase"), "true")
                    ''End If

                    Try
                        Dim strOtherVoucher As String = SystemConfigurationDAL.GetSystemConfigurationValue("Other_Voucher")

                        If strOtherVoucher = "" Then
                            gblnShowOtherVoucher = False

                        Else
                            gblnShowOtherVoucher = Utility.Utility.SymmetricEncryption.Decrypt(SystemConfigurationDAL.GetSystemConfigurationValue("Other_Voucher"), "true")

                        End If
                    Catch ex As Exception
                        gblnShowOtherVoucher = False

                    End Try

                    'CR # 141 add report email functionality
                    Try
                        SmtpServer = SystemConfigurationDAL.GetSystemConfigurationValue("Pop3Server")
                        MailSender = SystemConfigurationDAL.GetSystemConfigurationValue("EMailFromAddress")
                        PopUserName = SystemConfigurationDAL.GetSystemConfigurationValue("MailServerUser")
                        PopPassword = SystemConfigurationDAL.GetSystemConfigurationValue("MailServerPassword")
                        PopSSL = IIf(SystemConfigurationDAL.GetSystemConfigurationValue("MailServerSSL") = "True", True, False)
                    Catch ex As Exception

                    End Try


                    Dim frmMDI As New MDIParent1
                    ApplyStyleSheet(frmMDI, EnumProjectForms.MDIMain)
                    Me.Visible = False
                    frmMDI.Show()


                    If blnShowFYearForm Then
                        frmMDI.mnuProduct_Click(Nothing, Nothing)
                        frmMDI.MnuItemCompany_Click(Nothing, Nothing)

                    End If


                End If

            ElseIf btn.Name = btnEnd.Name Then
                End
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        Finally
            btn.Enabled = True
        End Try
    End Sub

    Private Sub SetUserLocation(ByVal strLoginID As String, ByVal lngFinancialYearStatusID As Long)
        Try

            Dim dtFind As DataTable
            Dim strSql As String = String.Empty

            strSql = " SELECT     tblGlDefLocation.location_code, tblGlDefLocation.location_name, tblGLSecurityUserLocation.location_id" & _
                     " FROM         tblGLSecurityUser INNER JOIN" & _
                     "          tblGLSecurityUserLocation ON tblGLSecurityUser.user_id = tblGLSecurityUserLocation.user_id INNER JOIN" & _
                     "          tblGlDefLocation ON tblGLSecurityUserLocation.location_id = tblGlDefLocation.location_id INNER JOIN " & _
                     "          tblGlDefFinancialYearStatus ON tblGlDefLocation.location_id = tblGlDefFinancialYearStatus.location_id " & _
                     " WHERE (tblGLSecurityUser.user_log_id = '" & strLoginID & "' AND financial_year_status_id = " & lngFinancialYearStatusID & ")"

            dtFind = UtilityDAL.GetDataTable(strSql)

            gobjLocationInfo = New Company

            If Not dtFind.Rows.Count = 0 Then

                gobjLocationInfo.CompanyID = dtFind.Rows(0)("location_id")
                gobjLocationInfo.CompanyCode = dtFind.Rows(0)("location_code")
                gobjLocationInfo.CompanyName = dtFind.Rows(0)("location_name")

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub SetFinancialYearVariables(ByVal lngFinancialYearStatusID As Long)
        Try

            Dim dtFind As DataTable
            Dim strSql As String = String.Empty

            'strSql = "SELECT  start_date, end_date, status From tblGlDefFinancialYear Where (financial_year_id = " & lngFinancialYearStatusID & ")"
            strSql = "SELECT tblGlDefFinancialYear.start_date, tblGlDefFinancialYear.end_date, tblGlDefFinancialYearStatus.status FROM tblGlDefFinancialYear INNER JOIN" _
                        & " tblGlDefFinancialYearStatus ON tblGlDefFinancialYear.financial_year_id = tblGlDefFinancialYearStatus.financial_year_id" _
                        & " WHERE (tblGlDefFinancialYearStatus.financial_year_status_id = " & lngFinancialYearStatusID & ")"

            dtFind = UtilityDAL.GetDataTable(strSql)


            If Not dtFind.Rows.Count = 0 Then

                gObjFinancialYearInfo.StartDate = dtFind.Rows(0)("start_date")
                gObjFinancialYearInfo.EndDate = dtFind.Rows(0)("end_date")
                gObjFinancialYearInfo.Status = dtFind.Rows(0)("status")
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub


    Private Function GetUserLocationRights(ByVal strUserLogID As String, ByVal lngFYearStatusID As Long) As Boolean
        Try

            Dim strSql As String

            strSql = "SELECT tblGLSecurityUser.user_name FROM  tblGLSecurityUserLocation INNER JOIN tblGLSecurityUser ON tblGLSecurityUserLocation.user_id = tblGLSecurityUser.user_id INNER JOIN " _
                        & " tblGlDefFinancialYearStatus ON tblGLSecurityUserLocation.location_id = tblGlDefFinancialYearStatus.location_id" _
                        & " WHERE (tblGLSecurityUser.user_log_id = '" & strUserLogID & "') AND (tblGlDefFinancialYearStatus.financial_year_status_id = " & lngFYearStatusID & ")"

            Dim dt As DataTable = UtilityDAL.GetDataTable(strSql)

            If dt.Rows.Count = 0 Then
                Return False
            Else
                Return True
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Sub frmLogin_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        ' FillCombos()
        Try

            'For Trial Version
            If gblnTrialVersion Then
                Me.txtLoginID.Text = "demo"
                Me.txtLoginPassword.Text = "demo"
                'Me.txtLoginID.Enabled = False
                'Me.txtLoginPassword.Enabled = False
            End If

            Me.FillCombos()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub

    Private Sub frmLogin_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        Try
            If e.KeyCode = Keys.Enter Then
                Me.Label1_Click(btnLogin, New EventArgs)

            ElseIf e.KeyCode = Keys.Escape Then
                Me.Label1_Click(btnEnd, New EventArgs)

            End If

        Catch ex As Exception
        End Try


    End Sub

    Private Sub frmLogin_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint

        Try
            Me.BackColor = Color.LightBlue
            ' Me.TransparencyKey = Color.LightBlue
            'Me.Panel1.BackColor = Color.Transparent

        Catch ex As Exception
            Throw ex

        End Try

    End Sub

    Private Sub frmLogin_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Me.lblVersion.Font = New Font(Me.lblVersion.Font.FontFamily, 15, FontStyle.Bold, GraphicsUnit.Point)
        ' Me.lblVersion.Text = IIf(DecryptWithALP(GetSystemConfigurationValue("Version")) = 1, "Personal Edition", IIf(DecryptWithALP(GetSystemConfigurationValue("Version")) = 2, "Professional Edition", "Enterprise Edition")) & " " & My.Application.Info.Version.ToString(4)

        Try

            

            Me.FillCombos()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub

    Private Sub frmLogin_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave

    End Sub

    Private Sub txtLoginID_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLoginID.GotFocus
        Me.txtLoginID.SelectionStart = 0
        Me.txtLoginID.SelectionLength = Me.txtLoginID.Text.Length
    End Sub

    'Private Sub txtLoginID_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtLoginID.KeyDown
    '    If e.KeyValue = 39 Then
    '        e.Handled = False
    '    End If
    'End Sub
End Class