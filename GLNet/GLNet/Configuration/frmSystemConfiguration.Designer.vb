<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSystemConfiguration
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.tabHeadOffice = New System.Windows.Forms.TabPage
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.GroupBox11 = New System.Windows.Forms.GroupBox
        Me.lblPassword = New System.Windows.Forms.Label
        Me.txtPassword = New System.Windows.Forms.TextBox
        Me.lblUserName = New System.Windows.Forms.Label
        Me.txtUserName = New System.Windows.Forms.TextBox
        Me.lblEmail = New System.Windows.Forms.Label
        Me.txtEmail = New System.Windows.Forms.TextBox
        Me.chkSSL = New System.Windows.Forms.CheckBox
        Me.lblServer = New System.Windows.Forms.Label
        Me.txtMailServer = New System.Windows.Forms.TextBox
        Me.txtURL = New System.Windows.Forms.TextBox
        Me.txtPhone = New System.Windows.Forms.TextBox
        Me.txtFax = New System.Windows.Forms.TextBox
        Me.txtAddress = New System.Windows.Forms.TextBox
        Me.txtName = New System.Windows.Forms.TextBox
        Me.lblAddress = New System.Windows.Forms.Label
        Me.lblFax = New System.Windows.Forms.Label
        Me.lblPhone = New System.Windows.Forms.Label
        Me.lblURL = New System.Windows.Forms.Label
        Me.lblName = New System.Windows.Forms.Label
        Me.tabConfiguration = New System.Windows.Forms.TabPage
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.GrpSMS = New System.Windows.Forms.GroupBox
        Me.ChkSendSMS = New System.Windows.Forms.CheckBox
        Me.txtBrandName = New System.Windows.Forms.TextBox
        Me.txtSMSPassword = New System.Windows.Forms.TextBox
        Me.txtSMSLogin = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.chkCustomerInfo = New System.Windows.Forms.CheckBox
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.btnPath = New System.Windows.Forms.Button
        Me.txtPath = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.cmbDateDisplay = New System.Windows.Forms.ComboBox
        Me.lblDateDisplaty = New System.Windows.Forms.Label
        Me.chkShowDescInVouchers = New System.Windows.Forms.CheckBox
        Me.chkAutoSortingV = New System.Windows.Forms.CheckBox
        Me.chkOtherVoucher = New System.Windows.Forms.CheckBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.optLongVoucher = New System.Windows.Forms.RadioButton
        Me.optShortVoucher = New System.Windows.Forms.RadioButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.ctrl_GLTaxOnServices = New GLNet.uiCtrlGLAccount
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.ctrlGL_TaxDeductedCustomer = New GLNet.uiCtrlGLAccount
        Me.ctrlGL_TaxDeductedVendor = New GLNet.uiCtrlGLAccount
        Me.ctrlGL_ProfitLossAccount = New GLNet.uiCtrlGLAccount
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.BrowseFolder = New System.Windows.Forms.FolderBrowserDialog
        Me.Label5 = New System.Windows.Forms.Label
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnFirst = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnPrevious = New System.Windows.Forms.Button
        Me.btnDelete = New System.Windows.Forms.Button
        Me.btnNext = New System.Windows.Forms.Button
        Me.btnUpdate = New System.Windows.Forms.Button
        Me.btnLast = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.btnNew = New System.Windows.Forms.Button
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TabControl1.SuspendLayout()
        Me.tabHeadOffice.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.GroupBox11.SuspendLayout()
        Me.tabConfiguration.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.GrpSMS.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.tabHeadOffice)
        Me.TabControl1.Controls.Add(Me.tabConfiguration)
        Me.TabControl1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(12, 31)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1000, 594)
        Me.TabControl1.TabIndex = 0
        '
        'tabHeadOffice
        '
        Me.tabHeadOffice.Controls.Add(Me.Panel1)
        Me.tabHeadOffice.Location = New System.Drawing.Point(4, 22)
        Me.tabHeadOffice.Name = "tabHeadOffice"
        Me.tabHeadOffice.Padding = New System.Windows.Forms.Padding(3)
        Me.tabHeadOffice.Size = New System.Drawing.Size(992, 568)
        Me.tabHeadOffice.TabIndex = 0
        Me.tabHeadOffice.Text = "Head Office Info"
        Me.tabHeadOffice.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.GroupBox11)
        Me.Panel1.Controls.Add(Me.txtURL)
        Me.Panel1.Controls.Add(Me.txtPhone)
        Me.Panel1.Controls.Add(Me.txtFax)
        Me.Panel1.Controls.Add(Me.txtAddress)
        Me.Panel1.Controls.Add(Me.txtName)
        Me.Panel1.Controls.Add(Me.lblAddress)
        Me.Panel1.Controls.Add(Me.lblFax)
        Me.Panel1.Controls.Add(Me.lblPhone)
        Me.Panel1.Controls.Add(Me.lblURL)
        Me.Panel1.Controls.Add(Me.lblName)
        Me.Panel1.Location = New System.Drawing.Point(12, 17)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(964, 537)
        Me.Panel1.TabIndex = 0
        '
        'GroupBox11
        '
        Me.GroupBox11.Controls.Add(Me.lblPassword)
        Me.GroupBox11.Controls.Add(Me.txtPassword)
        Me.GroupBox11.Controls.Add(Me.lblUserName)
        Me.GroupBox11.Controls.Add(Me.txtUserName)
        Me.GroupBox11.Controls.Add(Me.lblEmail)
        Me.GroupBox11.Controls.Add(Me.txtEmail)
        Me.GroupBox11.Controls.Add(Me.chkSSL)
        Me.GroupBox11.Controls.Add(Me.lblServer)
        Me.GroupBox11.Controls.Add(Me.txtMailServer)
        Me.GroupBox11.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.GroupBox11.Location = New System.Drawing.Point(493, 22)
        Me.GroupBox11.Name = "GroupBox11"
        Me.GroupBox11.Size = New System.Drawing.Size(417, 229)
        Me.GroupBox11.TabIndex = 15
        Me.GroupBox11.TabStop = False
        Me.GroupBox11.Text = "Email Configuration"
        '
        'lblPassword
        '
        Me.lblPassword.Location = New System.Drawing.Point(16, 104)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(100, 15)
        Me.lblPassword.TabIndex = 25
        Me.lblPassword.Text = "Password:"
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(176, 101)
        Me.txtPassword.MaxLength = 100
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(124)
        Me.txtPassword.Size = New System.Drawing.Size(212, 21)
        Me.txtPassword.TabIndex = 13
        Me.txtPassword.Tag = "MailServerPassword"
        '
        'lblUserName
        '
        Me.lblUserName.Location = New System.Drawing.Point(16, 78)
        Me.lblUserName.Name = "lblUserName"
        Me.lblUserName.Size = New System.Drawing.Size(100, 15)
        Me.lblUserName.TabIndex = 23
        Me.lblUserName.Text = "User Name:"
        '
        'txtUserName
        '
        Me.txtUserName.Location = New System.Drawing.Point(176, 75)
        Me.txtUserName.MaxLength = 100
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(212, 21)
        Me.txtUserName.TabIndex = 12
        Me.txtUserName.Tag = "MailServerUser"
        '
        'lblEmail
        '
        Me.lblEmail.Location = New System.Drawing.Point(16, 51)
        Me.lblEmail.Name = "lblEmail"
        Me.lblEmail.Size = New System.Drawing.Size(100, 15)
        Me.lblEmail.TabIndex = 21
        Me.lblEmail.Text = "Email Address:"
        '
        'txtEmail
        '
        Me.txtEmail.Location = New System.Drawing.Point(176, 48)
        Me.txtEmail.MaxLength = 100
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(212, 21)
        Me.txtEmail.TabIndex = 11
        Me.txtEmail.Tag = "EMailFromAddress"
        '
        'chkSSL
        '
        Me.chkSSL.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkSSL.Location = New System.Drawing.Point(16, 127)
        Me.chkSSL.Name = "chkSSL"
        Me.chkSSL.Size = New System.Drawing.Size(175, 24)
        Me.chkSSL.TabIndex = 16
        Me.chkSSL.Tag = "MailServerSSL"
        Me.chkSSL.Text = "Enable SSL"
        Me.chkSSL.UseVisualStyleBackColor = True
        '
        'lblServer
        '
        Me.lblServer.Location = New System.Drawing.Point(16, 25)
        Me.lblServer.Name = "lblServer"
        Me.lblServer.Size = New System.Drawing.Size(154, 17)
        Me.lblServer.TabIndex = 9
        Me.lblServer.Text = "Outgoing Mail Server (SMTP):"
        '
        'txtMailServer
        '
        Me.txtMailServer.Location = New System.Drawing.Point(176, 22)
        Me.txtMailServer.MaxLength = 100
        Me.txtMailServer.Name = "txtMailServer"
        Me.txtMailServer.Size = New System.Drawing.Size(212, 21)
        Me.txtMailServer.TabIndex = 9
        Me.txtMailServer.Tag = "Pop3Server"
        '
        'txtURL
        '
        Me.txtURL.Location = New System.Drawing.Point(131, 47)
        Me.txtURL.MaxLength = 250
        Me.txtURL.Name = "txtURL"
        Me.txtURL.Size = New System.Drawing.Size(356, 21)
        Me.txtURL.TabIndex = 1
        '
        'txtPhone
        '
        Me.txtPhone.Location = New System.Drawing.Point(131, 72)
        Me.txtPhone.MaxLength = 250
        Me.txtPhone.Name = "txtPhone"
        Me.txtPhone.Size = New System.Drawing.Size(356, 21)
        Me.txtPhone.TabIndex = 2
        Me.txtPhone.Tag = ""
        '
        'txtFax
        '
        Me.txtFax.Location = New System.Drawing.Point(131, 97)
        Me.txtFax.MaxLength = 250
        Me.txtFax.Name = "txtFax"
        Me.txtFax.Size = New System.Drawing.Size(356, 21)
        Me.txtFax.TabIndex = 3
        Me.txtFax.Tag = ""
        '
        'txtAddress
        '
        Me.txtAddress.Location = New System.Drawing.Point(131, 122)
        Me.txtAddress.MaxLength = 1000
        Me.txtAddress.Multiline = True
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtAddress.Size = New System.Drawing.Size(356, 129)
        Me.txtAddress.TabIndex = 4
        Me.txtAddress.Tag = ""
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(131, 22)
        Me.txtName.MaxLength = 250
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(356, 21)
        Me.txtName.TabIndex = 0
        Me.txtName.Tag = "IsRequired"
        '
        'lblAddress
        '
        Me.lblAddress.AutoSize = True
        Me.lblAddress.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddress.Location = New System.Drawing.Point(28, 122)
        Me.lblAddress.Name = "lblAddress"
        Me.lblAddress.Size = New System.Drawing.Size(60, 13)
        Me.lblAddress.TabIndex = 4
        Me.lblAddress.Text = "Address"
        '
        'lblFax
        '
        Me.lblFax.AutoSize = True
        Me.lblFax.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFax.Location = New System.Drawing.Point(28, 97)
        Me.lblFax.Name = "lblFax"
        Me.lblFax.Size = New System.Drawing.Size(44, 13)
        Me.lblFax.TabIndex = 3
        Me.lblFax.Text = "Fax #"
        '
        'lblPhone
        '
        Me.lblPhone.AutoSize = True
        Me.lblPhone.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPhone.Location = New System.Drawing.Point(28, 72)
        Me.lblPhone.Name = "lblPhone"
        Me.lblPhone.Size = New System.Drawing.Size(60, 13)
        Me.lblPhone.TabIndex = 2
        Me.lblPhone.Text = "Phone #"
        '
        'lblURL
        '
        Me.lblURL.AutoSize = True
        Me.lblURL.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblURL.Location = New System.Drawing.Point(28, 47)
        Me.lblURL.Name = "lblURL"
        Me.lblURL.Size = New System.Drawing.Size(31, 13)
        Me.lblURL.TabIndex = 1
        Me.lblURL.Text = "URL"
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblName.Location = New System.Drawing.Point(28, 22)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(44, 13)
        Me.lblName.TabIndex = 0
        Me.lblName.Text = "Name"
        '
        'tabConfiguration
        '
        Me.tabConfiguration.Controls.Add(Me.Panel2)
        Me.tabConfiguration.Location = New System.Drawing.Point(4, 22)
        Me.tabConfiguration.Name = "tabConfiguration"
        Me.tabConfiguration.Padding = New System.Windows.Forms.Padding(3)
        Me.tabConfiguration.Size = New System.Drawing.Size(992, 568)
        Me.tabConfiguration.TabIndex = 1
        Me.tabConfiguration.Text = "Configurations"
        Me.tabConfiguration.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.GrpSMS)
        Me.Panel2.Controls.Add(Me.GroupBox5)
        Me.Panel2.Controls.Add(Me.GroupBox4)
        Me.Panel2.Controls.Add(Me.GroupBox3)
        Me.Panel2.Controls.Add(Me.GroupBox1)
        Me.Panel2.Location = New System.Drawing.Point(15, 14)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(957, 596)
        Me.Panel2.TabIndex = 0
        '
        'GrpSMS
        '
        Me.GrpSMS.Controls.Add(Me.ChkSendSMS)
        Me.GrpSMS.Controls.Add(Me.txtBrandName)
        Me.GrpSMS.Controls.Add(Me.txtSMSPassword)
        Me.GrpSMS.Controls.Add(Me.txtSMSLogin)
        Me.GrpSMS.Controls.Add(Me.Label6)
        Me.GrpSMS.Controls.Add(Me.Label7)
        Me.GrpSMS.Controls.Add(Me.Label8)
        Me.GrpSMS.Location = New System.Drawing.Point(18, 332)
        Me.GrpSMS.Name = "GrpSMS"
        Me.GrpSMS.Size = New System.Drawing.Size(388, 153)
        Me.GrpSMS.TabIndex = 17
        Me.GrpSMS.TabStop = False
        Me.GrpSMS.Text = "SMS Configuration"
        '
        'ChkSendSMS
        '
        Me.ChkSendSMS.AutoSize = True
        Me.ChkSendSMS.Location = New System.Drawing.Point(138, 26)
        Me.ChkSendSMS.Name = "ChkSendSMS"
        Me.ChkSendSMS.Size = New System.Drawing.Size(84, 17)
        Me.ChkSendSMS.TabIndex = 12
        Me.ChkSendSMS.Tag = "SendSMS"
        Me.ChkSendSMS.Text = "Send SMS"
        Me.ChkSendSMS.UseVisualStyleBackColor = True
        '
        'txtBrandName
        '
        Me.txtBrandName.Location = New System.Drawing.Point(137, 114)
        Me.txtBrandName.MaxLength = 100
        Me.txtBrandName.Name = "txtBrandName"
        Me.txtBrandName.Size = New System.Drawing.Size(164, 21)
        Me.txtBrandName.TabIndex = 11
        Me.txtBrandName.Tag = "BrandName"
        '
        'txtSMSPassword
        '
        Me.txtSMSPassword.Location = New System.Drawing.Point(137, 84)
        Me.txtSMSPassword.MaxLength = 100
        Me.txtSMSPassword.Name = "txtSMSPassword"
        Me.txtSMSPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(124)
        Me.txtSMSPassword.Size = New System.Drawing.Size(164, 21)
        Me.txtSMSPassword.TabIndex = 10
        Me.txtSMSPassword.Tag = "SMSAPIPassword"
        '
        'txtSMSLogin
        '
        Me.txtSMSLogin.Location = New System.Drawing.Point(137, 55)
        Me.txtSMSLogin.MaxLength = 100
        Me.txtSMSLogin.Name = "txtSMSLogin"
        Me.txtSMSLogin.Size = New System.Drawing.Size(164, 21)
        Me.txtSMSLogin.TabIndex = 9
        Me.txtSMSLogin.Tag = "SMSAPIUserName"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(13, 114)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(86, 13)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Brand Name"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(13, 87)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(69, 13)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "Password"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(13, 62)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(42, 13)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "Login"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.chkCustomerInfo)
        Me.GroupBox5.Location = New System.Drawing.Point(412, 332)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(407, 151)
        Me.GroupBox5.TabIndex = 5
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Other Configurations"
        '
        'chkCustomerInfo
        '
        Me.chkCustomerInfo.AutoSize = True
        Me.chkCustomerInfo.Location = New System.Drawing.Point(29, 32)
        Me.chkCustomerInfo.Name = "chkCustomerInfo"
        Me.chkCustomerInfo.Size = New System.Drawing.Size(153, 17)
        Me.chkCustomerInfo.TabIndex = 0
        Me.chkCustomerInfo.Text = "Customer Information"
        Me.chkCustomerInfo.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.btnPath)
        Me.GroupBox4.Controls.Add(Me.txtPath)
        Me.GroupBox4.Controls.Add(Me.Label4)
        Me.GroupBox4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.Location = New System.Drawing.Point(14, 264)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(806, 55)
        Me.GroupBox4.TabIndex = 4
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "BackUp Default Location"
        '
        'btnPath
        '
        Me.btnPath.Location = New System.Drawing.Point(702, 27)
        Me.btnPath.Name = "btnPath"
        Me.btnPath.Size = New System.Drawing.Size(84, 23)
        Me.btnPath.TabIndex = 1
        Me.btnPath.Text = "Browse"
        Me.btnPath.UseVisualStyleBackColor = True
        '
        'txtPath
        '
        Me.txtPath.Location = New System.Drawing.Point(98, 28)
        Me.txtPath.MaxLength = 250
        Me.txtPath.Name = "txtPath"
        Me.txtPath.Size = New System.Drawing.Size(598, 21)
        Me.txtPath.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(30, 28)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(36, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Path"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.GroupBox6)
        Me.GroupBox3.Controls.Add(Me.chkShowDescInVouchers)
        Me.GroupBox3.Controls.Add(Me.chkAutoSortingV)
        Me.GroupBox3.Controls.Add(Me.chkOtherVoucher)
        Me.GroupBox3.Controls.Add(Me.GroupBox2)
        Me.GroupBox3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(14, 160)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(806, 100)
        Me.GroupBox3.TabIndex = 4
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Voucher Configuration"
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.cmbDateDisplay)
        Me.GroupBox6.Controls.Add(Me.lblDateDisplaty)
        Me.GroupBox6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox6.Location = New System.Drawing.Point(493, 26)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(293, 67)
        Me.GroupBox6.TabIndex = 4
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Date Format"
        '
        'cmbDateDisplay
        '
        Me.cmbDateDisplay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDateDisplay.FormattingEnabled = True
        Me.cmbDateDisplay.Items.AddRange(New Object() {"MMM/dd/yyyy", "dd/MMM/yyyy", "dd/MM/yyyy", "MM/dd/yyyy"})
        Me.cmbDateDisplay.Location = New System.Drawing.Point(156, 25)
        Me.cmbDateDisplay.Name = "cmbDateDisplay"
        Me.cmbDateDisplay.Size = New System.Drawing.Size(131, 21)
        Me.cmbDateDisplay.TabIndex = 14
        Me.cmbDateDisplay.Tag = "DisplayDateFormat"
        '
        'lblDateDisplaty
        '
        Me.lblDateDisplaty.Location = New System.Drawing.Point(6, 31)
        Me.lblDateDisplaty.Name = "lblDateDisplaty"
        Me.lblDateDisplaty.Size = New System.Drawing.Size(141, 15)
        Me.lblDateDisplaty.TabIndex = 13
        Me.lblDateDisplaty.Text = "Date Display Format"
        '
        'chkShowDescInVouchers
        '
        Me.chkShowDescInVouchers.AutoSize = True
        Me.chkShowDescInVouchers.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkShowDescInVouchers.Location = New System.Drawing.Point(329, 47)
        Me.chkShowDescInVouchers.Name = "chkShowDescInVouchers"
        Me.chkShowDescInVouchers.Size = New System.Drawing.Size(158, 17)
        Me.chkShowDescInVouchers.TabIndex = 3
        Me.chkShowDescInVouchers.Text = "Show Desc. in Voucher"
        Me.chkShowDescInVouchers.UseVisualStyleBackColor = True
        '
        'chkAutoSortingV
        '
        Me.chkAutoSortingV.AutoSize = True
        Me.chkAutoSortingV.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAutoSortingV.Location = New System.Drawing.Point(176, 70)
        Me.chkAutoSortingV.Name = "chkAutoSortingV"
        Me.chkAutoSortingV.Size = New System.Drawing.Size(148, 17)
        Me.chkAutoSortingV.TabIndex = 2
        Me.chkAutoSortingV.Text = "Voucher Auto Sorting"
        Me.chkAutoSortingV.UseVisualStyleBackColor = True
        '
        'chkOtherVoucher
        '
        Me.chkOtherVoucher.AutoSize = True
        Me.chkOtherVoucher.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOtherVoucher.Location = New System.Drawing.Point(176, 47)
        Me.chkOtherVoucher.Name = "chkOtherVoucher"
        Me.chkOtherVoucher.Size = New System.Drawing.Size(115, 17)
        Me.chkOtherVoucher.TabIndex = 1
        Me.chkOtherVoucher.Text = "Other Vouchers"
        Me.chkOtherVoucher.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.optLongVoucher)
        Me.GroupBox2.Controls.Add(Me.optShortVoucher)
        Me.GroupBox2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(6, 23)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(156, 67)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Voucher Report Size"
        '
        'optLongVoucher
        '
        Me.optLongVoucher.AutoSize = True
        Me.optLongVoucher.Checked = True
        Me.optLongVoucher.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optLongVoucher.Location = New System.Drawing.Point(23, 22)
        Me.optLongVoucher.Name = "optLongVoucher"
        Me.optLongVoucher.Size = New System.Drawing.Size(103, 17)
        Me.optLongVoucher.TabIndex = 0
        Me.optLongVoucher.TabStop = True
        Me.optLongVoucher.Text = "Long Voucher"
        Me.optLongVoucher.UseVisualStyleBackColor = True
        '
        'optShortVoucher
        '
        Me.optShortVoucher.AutoSize = True
        Me.optShortVoucher.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optShortVoucher.Location = New System.Drawing.Point(23, 45)
        Me.optShortVoucher.Name = "optShortVoucher"
        Me.optShortVoucher.Size = New System.Drawing.Size(107, 17)
        Me.optShortVoucher.TabIndex = 1
        Me.optShortVoucher.Text = "Short Voucher"
        Me.optShortVoucher.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.ctrl_GLTaxOnServices)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.ctrlGL_TaxDeductedCustomer)
        Me.GroupBox1.Controls.Add(Me.ctrlGL_TaxDeductedVendor)
        Me.GroupBox1.Controls.Add(Me.ctrlGL_ProfitLossAccount)
        Me.GroupBox1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(14, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(806, 157)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Account Mapping"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(16, 124)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(174, 13)
        Me.Label9.TabIndex = 7
        Me.Label9.Text = "Tax calculated (Sale Tax)"
        '
        'ctrl_GLTaxOnServices
        '
        Me.ctrl_GLTaxOnServices.AccountType = GLNet.EnumAccountTypes.None
        Me.ctrl_GLTaxOnServices.GLAccountCode = Nothing
        Me.ctrl_GLTaxOnServices.GLAccountHeadType = "4"
        Me.ctrl_GLTaxOnServices.GLAccountID = 0
        Me.ctrl_GLTaxOnServices.GLAccountName = Nothing
        Me.ctrl_GLTaxOnServices.GLFilterAccount = Nothing
        Me.ctrl_GLTaxOnServices.GLFilterCondition = Nothing
        Me.ctrl_GLTaxOnServices.Location = New System.Drawing.Point(234, 115)
        Me.ctrl_GLTaxOnServices.MinimumSize = New System.Drawing.Size(0, 30)
        Me.ctrl_GLTaxOnServices.Name = "ctrl_GLTaxOnServices"
        Me.ctrl_GLTaxOnServices.Size = New System.Drawing.Size(552, 30)
        Me.ctrl_GLTaxOnServices.TabIndex = 6
        Me.ctrl_GLTaxOnServices.Tag = Nothing
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(16, 93)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(182, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Tax Deducted By Customer"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(16, 60)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(184, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Tax Deducted From Vendor"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(146, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Profit && Loss Account"
        '
        'ctrlGL_TaxDeductedCustomer
        '
        Me.ctrlGL_TaxDeductedCustomer.AccountType = GLNet.EnumAccountTypes.None
        Me.ctrlGL_TaxDeductedCustomer.GLAccountCode = Nothing
        Me.ctrlGL_TaxDeductedCustomer.GLAccountHeadType = "4"
        Me.ctrlGL_TaxDeductedCustomer.GLAccountID = 0
        Me.ctrlGL_TaxDeductedCustomer.GLAccountName = Nothing
        Me.ctrlGL_TaxDeductedCustomer.GLFilterAccount = Nothing
        Me.ctrlGL_TaxDeductedCustomer.GLFilterCondition = Nothing
        Me.ctrlGL_TaxDeductedCustomer.Location = New System.Drawing.Point(234, 84)
        Me.ctrlGL_TaxDeductedCustomer.MinimumSize = New System.Drawing.Size(0, 30)
        Me.ctrlGL_TaxDeductedCustomer.Name = "ctrlGL_TaxDeductedCustomer"
        Me.ctrlGL_TaxDeductedCustomer.Size = New System.Drawing.Size(552, 30)
        Me.ctrlGL_TaxDeductedCustomer.TabIndex = 2
        Me.ctrlGL_TaxDeductedCustomer.Tag = Nothing
        '
        'ctrlGL_TaxDeductedVendor
        '
        Me.ctrlGL_TaxDeductedVendor.AccountType = GLNet.EnumAccountTypes.None
        Me.ctrlGL_TaxDeductedVendor.GLAccountCode = Nothing
        Me.ctrlGL_TaxDeductedVendor.GLAccountHeadType = "4"
        Me.ctrlGL_TaxDeductedVendor.GLAccountID = 0
        Me.ctrlGL_TaxDeductedVendor.GLAccountName = Nothing
        Me.ctrlGL_TaxDeductedVendor.GLFilterAccount = Nothing
        Me.ctrlGL_TaxDeductedVendor.GLFilterCondition = Nothing
        Me.ctrlGL_TaxDeductedVendor.Location = New System.Drawing.Point(234, 51)
        Me.ctrlGL_TaxDeductedVendor.MinimumSize = New System.Drawing.Size(0, 30)
        Me.ctrlGL_TaxDeductedVendor.Name = "ctrlGL_TaxDeductedVendor"
        Me.ctrlGL_TaxDeductedVendor.Size = New System.Drawing.Size(552, 30)
        Me.ctrlGL_TaxDeductedVendor.TabIndex = 1
        Me.ctrlGL_TaxDeductedVendor.Tag = Nothing
        '
        'ctrlGL_ProfitLossAccount
        '
        Me.ctrlGL_ProfitLossAccount.AccountType = GLNet.EnumAccountTypes.None
        Me.ctrlGL_ProfitLossAccount.GLAccountCode = Nothing
        Me.ctrlGL_ProfitLossAccount.GLAccountHeadType = "4"
        Me.ctrlGL_ProfitLossAccount.GLAccountID = 0
        Me.ctrlGL_ProfitLossAccount.GLAccountName = Nothing
        Me.ctrlGL_ProfitLossAccount.GLFilterAccount = Nothing
        Me.ctrlGL_ProfitLossAccount.GLFilterCondition = Nothing
        Me.ctrlGL_ProfitLossAccount.Location = New System.Drawing.Point(234, 18)
        Me.ctrlGL_ProfitLossAccount.MinimumSize = New System.Drawing.Size(0, 30)
        Me.ctrlGL_ProfitLossAccount.Name = "ctrlGL_ProfitLossAccount"
        Me.ctrlGL_ProfitLossAccount.Size = New System.Drawing.Size(552, 30)
        Me.ctrlGL_ProfitLossAccount.TabIndex = 0
        Me.ctrlGL_ProfitLossAccount.Tag = Nothing
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'Label5
        '
        Me.Label5.AccessibleDescription = "Title"
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold)
        Me.Label5.ForeColor = System.Drawing.SystemColors.Desktop
        Me.Label5.Location = New System.Drawing.Point(12, 4)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(207, 24)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "System Configuration"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.btnExit)
        Me.Panel3.Controls.Add(Me.btnFirst)
        Me.Panel3.Controls.Add(Me.btnCancel)
        Me.Panel3.Controls.Add(Me.btnPrevious)
        Me.Panel3.Controls.Add(Me.btnDelete)
        Me.Panel3.Controls.Add(Me.btnNext)
        Me.Panel3.Controls.Add(Me.btnUpdate)
        Me.Panel3.Controls.Add(Me.btnLast)
        Me.Panel3.Controls.Add(Me.Button1)
        Me.Panel3.Controls.Add(Me.btnNew)
        Me.Panel3.Location = New System.Drawing.Point(-1, 626)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1024, 50)
        Me.Panel3.TabIndex = 16
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(965, 0)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(48, 48)
        Me.btnExit.TabIndex = 5
        Me.btnExit.Tag = "HideText"
        Me.btnExit.Text = " "
        Me.ToolTip1.SetToolTip(Me.btnExit, "Exit (Ctrl+X)")
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnFirst
        '
        Me.btnFirst.Location = New System.Drawing.Point(12, 0)
        Me.btnFirst.Name = "btnFirst"
        Me.btnFirst.Size = New System.Drawing.Size(48, 48)
        Me.btnFirst.TabIndex = 0
        Me.btnFirst.TabStop = False
        Me.btnFirst.Tag = "HideText"
        Me.btnFirst.Text = " "
        Me.btnFirst.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(565, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(48, 48)
        Me.btnCancel.TabIndex = 4
        Me.btnCancel.Tag = "HideText"
        Me.btnCancel.Text = " "
        Me.ToolTip1.SetToolTip(Me.btnCancel, "Cancel (Ctrl+E)")
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnPrevious
        '
        Me.btnPrevious.Location = New System.Drawing.Point(66, 0)
        Me.btnPrevious.Name = "btnPrevious"
        Me.btnPrevious.Size = New System.Drawing.Size(48, 48)
        Me.btnPrevious.TabIndex = 1
        Me.btnPrevious.TabStop = False
        Me.btnPrevious.Tag = "HideText"
        Me.btnPrevious.Text = " "
        Me.btnPrevious.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(511, 0)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(48, 48)
        Me.btnDelete.TabIndex = 3
        Me.btnDelete.Tag = "HideText"
        Me.btnDelete.Text = " "
        Me.ToolTip1.SetToolTip(Me.btnDelete, "Delete (Ctrl+D)")
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnNext
        '
        Me.btnNext.Location = New System.Drawing.Point(120, 0)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(48, 48)
        Me.btnNext.TabIndex = 2
        Me.btnNext.TabStop = False
        Me.btnNext.Tag = "HideText"
        Me.btnNext.Text = " "
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'btnUpdate
        '
        Me.btnUpdate.Location = New System.Drawing.Point(457, 0)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(48, 48)
        Me.btnUpdate.TabIndex = 2
        Me.btnUpdate.Tag = "HideText"
        Me.btnUpdate.Text = " "
        Me.ToolTip1.SetToolTip(Me.btnUpdate, "Update (Ctrl+U)")
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'btnLast
        '
        Me.btnLast.Location = New System.Drawing.Point(174, 0)
        Me.btnLast.Name = "btnLast"
        Me.btnLast.Size = New System.Drawing.Size(48, 48)
        Me.btnLast.TabIndex = 3
        Me.btnLast.TabStop = False
        Me.btnLast.Tag = "HideText"
        Me.btnLast.Text = " "
        Me.btnLast.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(403, 0)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(48, 48)
        Me.Button1.TabIndex = 0
        Me.Button1.Tag = "HideText"
        Me.Button1.Text = " "
        Me.ToolTip1.SetToolTip(Me.Button1, "Save (Ctrl+S)")
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btnNew
        '
        Me.btnNew.Location = New System.Drawing.Point(349, 0)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(48, 48)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Tag = "HideText"
        Me.btnNew.Text = " "
        Me.ToolTip1.SetToolTip(Me.btnNew, "New (Ctrl+N)")
        Me.btnNew.UseVisualStyleBackColor = True
        '
        'frmSystemConfiguration
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1030, 748)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TabControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Name = "frmSystemConfiguration"
        Me.Text = "System Configuration"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.TabControl1.ResumeLayout(False)
        Me.tabHeadOffice.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox11.ResumeLayout(False)
        Me.GroupBox11.PerformLayout()
        Me.tabConfiguration.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.GrpSMS.ResumeLayout(False)
        Me.GrpSMS.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tabHeadOffice As System.Windows.Forms.TabPage
    Friend WithEvents tabConfiguration As System.Windows.Forms.TabPage
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblAddress As System.Windows.Forms.Label
    Friend WithEvents lblFax As System.Windows.Forms.Label
    Friend WithEvents lblPhone As System.Windows.Forms.Label
    Friend WithEvents lblURL As System.Windows.Forms.Label
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents txtURL As System.Windows.Forms.TextBox
    Friend WithEvents txtPhone As System.Windows.Forms.TextBox
    Friend WithEvents txtFax As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress As System.Windows.Forms.TextBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ctrlGL_TaxDeductedCustomer As GLNet.uiCtrlGLAccount
    Friend WithEvents ctrlGL_TaxDeductedVendor As GLNet.uiCtrlGLAccount
    Friend WithEvents ctrlGL_ProfitLossAccount As GLNet.uiCtrlGLAccount
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents optLongVoucher As System.Windows.Forms.RadioButton
    Friend WithEvents optShortVoucher As System.Windows.Forms.RadioButton
    Friend WithEvents chkOtherVoucher As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtPath As System.Windows.Forms.TextBox
    Friend WithEvents btnPath As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents BrowseFolder As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnFirst As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnPrevious As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents btnLast As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents chkAutoSortingV As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox11 As System.Windows.Forms.GroupBox
    Friend WithEvents lblPassword As System.Windows.Forms.Label
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents lblUserName As System.Windows.Forms.Label
    Friend WithEvents txtUserName As System.Windows.Forms.TextBox
    Friend WithEvents lblEmail As System.Windows.Forms.Label
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents chkSSL As System.Windows.Forms.CheckBox
    Friend WithEvents lblServer As System.Windows.Forms.Label
    Friend WithEvents txtMailServer As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents chkCustomerInfo As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowDescInVouchers As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents lblDateDisplaty As System.Windows.Forms.Label
    Friend WithEvents cmbDateDisplay As System.Windows.Forms.ComboBox
    Friend WithEvents GrpSMS As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtBrandName As System.Windows.Forms.TextBox
    Friend WithEvents txtSMSPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtSMSLogin As System.Windows.Forms.TextBox
    Friend WithEvents ChkSendSMS As System.Windows.Forms.CheckBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents ctrl_GLTaxOnServices As GLNet.uiCtrlGLAccount
End Class
