<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCustomerInfo
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
        Me.btnExit = New System.Windows.Forms.Button
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.btnFirst = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnPrevious = New System.Windows.Forms.Button
        Me.btnDelete = New System.Windows.Forms.Button
        Me.btnNext = New System.Windows.Forms.Button
        Me.btnUpdate = New System.Windows.Forms.Button
        Me.btnLast = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnNew = New System.Windows.Forms.Button
        Me.btnSendSMS = New System.Windows.Forms.Button
        Me.lblHeading = New System.Windows.Forms.Label
        Me.TabCustomerInfo = New System.Windows.Forms.TabControl
        Me.TabPgCustomerInfo = New System.Windows.Forms.TabPage
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.pbOnlineActivation = New System.Windows.Forms.ProgressBar
        Me.chkUpdateOnline = New System.Windows.Forms.CheckBox
        Me.UiCtrlGLAccount1 = New GLNet.uiCtrlGLAccount
        Me.Label1 = New System.Windows.Forms.Label
        Me.gbmaintdate = New System.Windows.Forms.GroupBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.dtMaintStartDate = New System.Windows.Forms.DateTimePicker
        Me.Label6 = New System.Windows.Forms.Label
        Me.dtCreationDate = New System.Windows.Forms.DateTimePicker
        Me.cmbProduct = New System.Windows.Forms.ComboBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.dtPaymentRcvdDate = New System.Windows.Forms.DateTimePicker
        Me.dtMaintEndDate = New System.Windows.Forms.DateTimePicker
        Me.txtMonthlyAmount = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.cmbSlaType = New System.Windows.Forms.ComboBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.cmbStatus = New System.Windows.Forms.ComboBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.gbContactPerson = New System.Windows.Forms.GroupBox
        Me.Label32 = New System.Windows.Forms.Label
        Me.txtSTNNum = New System.Windows.Forms.TextBox
        Me.Label31 = New System.Windows.Forms.Label
        Me.txtNTNNum = New System.Windows.Forms.TextBox
        Me.txtContactPerson = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.txtRemarks = New System.Windows.Forms.TextBox
        Me.txtPhoneOffice = New System.Windows.Forms.TextBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.txtMobile = New System.Windows.Forms.TextBox
        Me.txtFax = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtEmail = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtAddress = New System.Windows.Forms.TextBox
        Me.lblScreentext1 = New System.Windows.Forms.Label
        Me.lblScreentext = New System.Windows.Forms.Label
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.btnValidate = New System.Windows.Forms.Button
        Me.CmbSMSTemplates = New System.Windows.Forms.ComboBox
        Me.btnReset = New System.Windows.Forms.Button
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.Label24 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.dtpPaymentto_DtlSearch = New System.Windows.Forms.DateTimePicker
        Me.dtpMaintEndDate_DtlSearch = New System.Windows.Forms.DateTimePicker
        Me.dtpmaintTo_DtlSearch = New System.Windows.Forms.DateTimePicker
        Me.Label23 = New System.Windows.Forms.Label
        Me.Label30 = New System.Windows.Forms.Label
        Me.dtpPaymentDate_DtlSearch = New System.Windows.Forms.DateTimePicker
        Me.Label29 = New System.Windows.Forms.Label
        Me.dtpCreationDate_DtlSearch = New System.Windows.Forms.DateTimePicker
        Me.Label28 = New System.Windows.Forms.Label
        Me.dtpCreationDate2_DtlSearch = New System.Windows.Forms.DateTimePicker
        Me.Label27 = New System.Windows.Forms.Label
        Me.Label25 = New System.Windows.Forms.Label
        Me.Label26 = New System.Windows.Forms.Label
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmbProductType_DtlSearch = New System.Windows.Forms.ComboBox
        Me.cmbStatus_DtlSearch = New System.Windows.Forms.ComboBox
        Me.cmbSLAType_DtlSearch = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.UiCtrlGLAccount2 = New GLNet.uiCtrlGLAccount
        Me.Label21 = New System.Windows.Forms.Label
        Me.txtContactPerson_dtlSearch = New System.Windows.Forms.TextBox
        Me.btnDtlSearch = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.grdAllRecords = New Janus.Windows.GridEX.GridEX
        Me.UiCtrlGridBar1 = New GLNet.uiCtrlGridBar
        Me.GrdView = New Janus.Windows.GridEX.GridEX
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Panel2.SuspendLayout()
        Me.TabCustomerInfo.SuspendLayout()
        Me.TabPgCustomerInfo.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.gbmaintdate.SuspendLayout()
        Me.gbContactPerson.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdAllRecords, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(965, 0)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(48, 48)
        Me.btnExit.TabIndex = 25
        Me.btnExit.Tag = "HideText"
        Me.btnExit.Text = " "
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.btnExit)
        Me.Panel2.Controls.Add(Me.btnFirst)
        Me.Panel2.Controls.Add(Me.btnCancel)
        Me.Panel2.Controls.Add(Me.btnPrevious)
        Me.Panel2.Controls.Add(Me.btnDelete)
        Me.Panel2.Controls.Add(Me.btnNext)
        Me.Panel2.Controls.Add(Me.btnUpdate)
        Me.Panel2.Controls.Add(Me.btnLast)
        Me.Panel2.Controls.Add(Me.btnSave)
        Me.Panel2.Controls.Add(Me.btnNew)
        Me.Panel2.Location = New System.Drawing.Point(-2, 607)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1030, 50)
        Me.Panel2.TabIndex = 8
        '
        'btnFirst
        '
        Me.btnFirst.Location = New System.Drawing.Point(12, 0)
        Me.btnFirst.Name = "btnFirst"
        Me.btnFirst.Size = New System.Drawing.Size(48, 48)
        Me.btnFirst.TabIndex = 17
        Me.btnFirst.Tag = "HideText"
        Me.btnFirst.Text = " "
        Me.btnFirst.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(565, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(48, 48)
        Me.btnCancel.TabIndex = 23
        Me.btnCancel.Tag = "HideText"
        Me.btnCancel.Text = " "
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnPrevious
        '
        Me.btnPrevious.Location = New System.Drawing.Point(66, 0)
        Me.btnPrevious.Name = "btnPrevious"
        Me.btnPrevious.Size = New System.Drawing.Size(48, 48)
        Me.btnPrevious.TabIndex = 18
        Me.btnPrevious.Tag = "HideText"
        Me.btnPrevious.Text = " "
        Me.btnPrevious.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(511, 0)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(48, 48)
        Me.btnDelete.TabIndex = 22
        Me.btnDelete.Tag = "HideText"
        Me.btnDelete.Text = " "
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnNext
        '
        Me.btnNext.Location = New System.Drawing.Point(120, 0)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(48, 48)
        Me.btnNext.TabIndex = 19
        Me.btnNext.Tag = "HideText"
        Me.btnNext.Text = " "
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'btnUpdate
        '
        Me.btnUpdate.Location = New System.Drawing.Point(457, 0)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(48, 48)
        Me.btnUpdate.TabIndex = 21
        Me.btnUpdate.Tag = "HideText"
        Me.btnUpdate.Text = " "
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'btnLast
        '
        Me.btnLast.Location = New System.Drawing.Point(174, 0)
        Me.btnLast.Name = "btnLast"
        Me.btnLast.Size = New System.Drawing.Size(48, 48)
        Me.btnLast.TabIndex = 20
        Me.btnLast.Tag = "HideText"
        Me.btnLast.Text = " "
        Me.btnLast.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(403, 0)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(48, 48)
        Me.btnSave.TabIndex = 16
        Me.btnSave.Tag = "HideText"
        Me.btnSave.Text = " "
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnNew
        '
        Me.btnNew.Location = New System.Drawing.Point(349, 0)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(48, 48)
        Me.btnNew.TabIndex = 24
        Me.btnNew.Tag = "HideText"
        Me.btnNew.Text = " "
        Me.btnNew.UseVisualStyleBackColor = True
        '
        'btnSendSMS
        '
        Me.btnSendSMS.Location = New System.Drawing.Point(383, 13)
        Me.btnSendSMS.Name = "btnSendSMS"
        Me.btnSendSMS.Size = New System.Drawing.Size(86, 26)
        Me.btnSendSMS.TabIndex = 26
        Me.btnSendSMS.Text = "Send SMS"
        Me.btnSendSMS.UseVisualStyleBackColor = True
        '
        'lblHeading
        '
        Me.lblHeading.AccessibleDescription = "Title"
        Me.lblHeading.AutoSize = True
        Me.lblHeading.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold)
        Me.lblHeading.ForeColor = System.Drawing.SystemColors.Desktop
        Me.lblHeading.Location = New System.Drawing.Point(9, 9)
        Me.lblHeading.Name = "lblHeading"
        Me.lblHeading.Size = New System.Drawing.Size(208, 24)
        Me.lblHeading.TabIndex = 16
        Me.lblHeading.Text = "Customer Information"
        '
        'TabCustomerInfo
        '
        Me.TabCustomerInfo.Controls.Add(Me.TabPgCustomerInfo)
        Me.TabCustomerInfo.Controls.Add(Me.TabPage2)
        Me.TabCustomerInfo.Location = New System.Drawing.Point(-2, 0)
        Me.TabCustomerInfo.Margin = New System.Windows.Forms.Padding(0)
        Me.TabCustomerInfo.Name = "TabCustomerInfo"
        Me.TabCustomerInfo.Padding = New System.Drawing.Point(0, 0)
        Me.TabCustomerInfo.SelectedIndex = 0
        Me.TabCustomerInfo.Size = New System.Drawing.Size(1028, 604)
        Me.TabCustomerInfo.TabIndex = 0
        '
        'TabPgCustomerInfo
        '
        Me.TabPgCustomerInfo.Controls.Add(Me.GroupBox3)
        Me.TabPgCustomerInfo.Controls.Add(Me.lblScreentext)
        Me.TabPgCustomerInfo.Location = New System.Drawing.Point(4, 22)
        Me.TabPgCustomerInfo.Margin = New System.Windows.Forms.Padding(0)
        Me.TabPgCustomerInfo.Name = "TabPgCustomerInfo"
        Me.TabPgCustomerInfo.Size = New System.Drawing.Size(1020, 578)
        Me.TabPgCustomerInfo.TabIndex = 0
        Me.TabPgCustomerInfo.Text = "Customer Information"
        Me.TabPgCustomerInfo.ToolTipText = "Customer Information"
        Me.TabPgCustomerInfo.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.pbOnlineActivation)
        Me.GroupBox3.Controls.Add(Me.chkUpdateOnline)
        Me.GroupBox3.Controls.Add(Me.UiCtrlGLAccount1)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.gbmaintdate)
        Me.GroupBox3.Controls.Add(Me.gbContactPerson)
        Me.GroupBox3.Controls.Add(Me.lblScreentext1)
        Me.GroupBox3.Location = New System.Drawing.Point(8, 36)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(1001, 539)
        Me.GroupBox3.TabIndex = 49
        Me.GroupBox3.TabStop = False
        '
        'pbOnlineActivation
        '
        Me.pbOnlineActivation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pbOnlineActivation.Location = New System.Drawing.Point(8, 510)
        Me.pbOnlineActivation.Name = "pbOnlineActivation"
        Me.pbOnlineActivation.Size = New System.Drawing.Size(987, 23)
        Me.pbOnlineActivation.TabIndex = 50
        Me.pbOnlineActivation.Visible = False
        '
        'chkUpdateOnline
        '
        Me.chkUpdateOnline.AutoSize = True
        Me.chkUpdateOnline.Location = New System.Drawing.Point(25, 412)
        Me.chkUpdateOnline.Name = "chkUpdateOnline"
        Me.chkUpdateOnline.Size = New System.Drawing.Size(151, 17)
        Me.chkUpdateOnline.TabIndex = 48
        Me.chkUpdateOnline.Text = "Update to online database"
        Me.chkUpdateOnline.UseVisualStyleBackColor = True
        '
        'UiCtrlGLAccount1
        '
        Me.UiCtrlGLAccount1.AccountType = GLNet.EnumAccountTypes.Customer
        Me.UiCtrlGLAccount1.GLAccountCode = Nothing
        Me.UiCtrlGLAccount1.GLAccountHeadType = "4"
        Me.UiCtrlGLAccount1.GLAccountID = 0
        Me.UiCtrlGLAccount1.GLAccountName = Nothing
        Me.UiCtrlGLAccount1.GLFilterAccount = Nothing
        Me.UiCtrlGLAccount1.GLFilterCondition = Nothing
        Me.UiCtrlGLAccount1.Location = New System.Drawing.Point(121, 19)
        Me.UiCtrlGLAccount1.MinimumSize = New System.Drawing.Size(0, 30)
        Me.UiCtrlGLAccount1.Name = "UiCtrlGLAccount1"
        Me.UiCtrlGLAccount1.Size = New System.Drawing.Size(405, 30)
        Me.UiCtrlGLAccount1.TabIndex = 0
        Me.UiCtrlGLAccount1.Tag = Nothing
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(27, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 13)
        Me.Label1.TabIndex = 38
        Me.Label1.Text = "Account Code"
        '
        'gbmaintdate
        '
        Me.gbmaintdate.Controls.Add(Me.Label4)
        Me.gbmaintdate.Controls.Add(Me.dtMaintStartDate)
        Me.gbmaintdate.Controls.Add(Me.Label6)
        Me.gbmaintdate.Controls.Add(Me.dtCreationDate)
        Me.gbmaintdate.Controls.Add(Me.cmbProduct)
        Me.gbmaintdate.Controls.Add(Me.Label9)
        Me.gbmaintdate.Controls.Add(Me.Label7)
        Me.gbmaintdate.Controls.Add(Me.dtPaymentRcvdDate)
        Me.gbmaintdate.Controls.Add(Me.dtMaintEndDate)
        Me.gbmaintdate.Controls.Add(Me.txtMonthlyAmount)
        Me.gbmaintdate.Controls.Add(Me.Label8)
        Me.gbmaintdate.Controls.Add(Me.cmbSlaType)
        Me.gbmaintdate.Controls.Add(Me.Label17)
        Me.gbmaintdate.Controls.Add(Me.Label16)
        Me.gbmaintdate.Controls.Add(Me.cmbStatus)
        Me.gbmaintdate.Controls.Add(Me.Label15)
        Me.gbmaintdate.Location = New System.Drawing.Point(6, 273)
        Me.gbmaintdate.Name = "gbmaintdate"
        Me.gbmaintdate.Size = New System.Drawing.Size(989, 133)
        Me.gbmaintdate.TabIndex = 47
        Me.gbmaintdate.TabStop = False
        Me.gbmaintdate.Text = "Maintainance Information"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(407, 23)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 13)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Creation Date"
        '
        'dtMaintStartDate
        '
        Me.dtMaintStartDate.CustomFormat = "dd/MM/yyyy"
        Me.dtMaintStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtMaintStartDate.Location = New System.Drawing.Point(117, 19)
        Me.dtMaintStartDate.Name = "dtMaintStartDate"
        Me.dtMaintStartDate.ShowCheckBox = True
        Me.dtMaintStartDate.Size = New System.Drawing.Size(147, 20)
        Me.dtMaintStartDate.TabIndex = 8
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(16, 79)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(44, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Product"
        '
        'dtCreationDate
        '
        Me.dtCreationDate.CustomFormat = "dd/MM/yyyy"
        Me.dtCreationDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtCreationDate.Location = New System.Drawing.Point(516, 19)
        Me.dtCreationDate.Name = "dtCreationDate"
        Me.dtCreationDate.Size = New System.Drawing.Size(143, 20)
        Me.dtCreationDate.TabIndex = 12
        '
        'cmbProduct
        '
        Me.cmbProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProduct.FormattingEnabled = True
        Me.cmbProduct.Location = New System.Drawing.Point(117, 71)
        Me.cmbProduct.Name = "cmbProduct"
        Me.cmbProduct.Size = New System.Drawing.Size(147, 21)
        Me.cmbProduct.TabIndex = 10
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(407, 52)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(103, 13)
        Me.Label9.TabIndex = 41
        Me.Label9.Text = "Payment Rcvd Date"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(16, 26)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(87, 13)
        Me.Label7.TabIndex = 39
        Me.Label7.Text = "Maint. Start Date"
        '
        'dtPaymentRcvdDate
        '
        Me.dtPaymentRcvdDate.CustomFormat = "dd/MM/yyyy"
        Me.dtPaymentRcvdDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtPaymentRcvdDate.Location = New System.Drawing.Point(516, 45)
        Me.dtPaymentRcvdDate.Name = "dtPaymentRcvdDate"
        Me.dtPaymentRcvdDate.ShowCheckBox = True
        Me.dtPaymentRcvdDate.Size = New System.Drawing.Size(143, 20)
        Me.dtPaymentRcvdDate.TabIndex = 13
        '
        'dtMaintEndDate
        '
        Me.dtMaintEndDate.CustomFormat = "dd/MM/yyyy"
        Me.dtMaintEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtMaintEndDate.Location = New System.Drawing.Point(117, 45)
        Me.dtMaintEndDate.Name = "dtMaintEndDate"
        Me.dtMaintEndDate.ShowCheckBox = True
        Me.dtMaintEndDate.Size = New System.Drawing.Size(147, 20)
        Me.dtMaintEndDate.TabIndex = 9
        '
        'txtMonthlyAmount
        '
        Me.txtMonthlyAmount.Location = New System.Drawing.Point(516, 99)
        Me.txtMonthlyAmount.Name = "txtMonthlyAmount"
        Me.txtMonthlyAmount.Size = New System.Drawing.Size(143, 20)
        Me.txtMonthlyAmount.TabIndex = 15
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(16, 52)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(84, 13)
        Me.Label8.TabIndex = 40
        Me.Label8.Text = "Maint. End Date"
        '
        'cmbSlaType
        '
        Me.cmbSlaType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSlaType.FormattingEnabled = True
        Me.cmbSlaType.Items.AddRange(New Object() {"--Select--", "Warranty", "SLA1", "SLA2", "SLA3", "Job-to-Job", "Warranty Expired"})
        Me.cmbSlaType.Location = New System.Drawing.Point(117, 98)
        Me.cmbSlaType.Name = "cmbSlaType"
        Me.cmbSlaType.Size = New System.Drawing.Size(147, 21)
        Me.cmbSlaType.TabIndex = 11
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(407, 106)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(83, 13)
        Me.Label17.TabIndex = 33
        Me.Label17.Text = "Monthly Amount"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(16, 106)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(54, 13)
        Me.Label16.TabIndex = 30
        Me.Label16.Text = "SLA-Type"
        '
        'cmbStatus
        '
        Me.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStatus.FormattingEnabled = True
        Me.cmbStatus.Items.AddRange(New Object() {"--Select--", "Ok", "Job-to-Job", "Blocked", "Suspended"})
        Me.cmbStatus.Location = New System.Drawing.Point(516, 71)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.Size = New System.Drawing.Size(143, 21)
        Me.cmbStatus.TabIndex = 14
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(407, 79)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(37, 13)
        Me.Label15.TabIndex = 29
        Me.Label15.Text = "Status"
        '
        'gbContactPerson
        '
        Me.gbContactPerson.Controls.Add(Me.Label32)
        Me.gbContactPerson.Controls.Add(Me.txtSTNNum)
        Me.gbContactPerson.Controls.Add(Me.Label31)
        Me.gbContactPerson.Controls.Add(Me.txtNTNNum)
        Me.gbContactPerson.Controls.Add(Me.txtContactPerson)
        Me.gbContactPerson.Controls.Add(Me.Label5)
        Me.gbContactPerson.Controls.Add(Me.Label10)
        Me.gbContactPerson.Controls.Add(Me.Label11)
        Me.gbContactPerson.Controls.Add(Me.Label12)
        Me.gbContactPerson.Controls.Add(Me.txtRemarks)
        Me.gbContactPerson.Controls.Add(Me.txtPhoneOffice)
        Me.gbContactPerson.Controls.Add(Me.Label18)
        Me.gbContactPerson.Controls.Add(Me.txtMobile)
        Me.gbContactPerson.Controls.Add(Me.txtFax)
        Me.gbContactPerson.Controls.Add(Me.Label13)
        Me.gbContactPerson.Controls.Add(Me.txtEmail)
        Me.gbContactPerson.Controls.Add(Me.Label14)
        Me.gbContactPerson.Controls.Add(Me.txtAddress)
        Me.gbContactPerson.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbContactPerson.Location = New System.Drawing.Point(6, 55)
        Me.gbContactPerson.Name = "gbContactPerson"
        Me.gbContactPerson.Size = New System.Drawing.Size(989, 214)
        Me.gbContactPerson.TabIndex = 46
        Me.gbContactPerson.TabStop = False
        Me.gbContactPerson.Text = "Contact Information"
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(13, 190)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(87, 13)
        Me.Label32.TabIndex = 39
        Me.Label32.Text = "Sales Tax Reg.#"
        '
        'txtSTNNum
        '
        Me.txtSTNNum.Location = New System.Drawing.Point(115, 187)
        Me.txtSTNNum.Name = "txtSTNNum"
        Me.txtSTNNum.Size = New System.Drawing.Size(266, 20)
        Me.txtSTNNum.TabIndex = 38
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(14, 168)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(40, 13)
        Me.Label31.TabIndex = 37
        Me.Label31.Text = "NTN #"
        '
        'txtNTNNum
        '
        Me.txtNTNNum.Location = New System.Drawing.Point(116, 161)
        Me.txtNTNNum.Name = "txtNTNNum"
        Me.txtNTNNum.Size = New System.Drawing.Size(264, 20)
        Me.txtNTNNum.TabIndex = 36
        '
        'txtContactPerson
        '
        Me.txtContactPerson.Location = New System.Drawing.Point(117, 31)
        Me.txtContactPerson.Name = "txtContactPerson"
        Me.txtContactPerson.Size = New System.Drawing.Size(264, 20)
        Me.txtContactPerson.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(16, 34)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(80, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Contact Person"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(16, 63)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(69, 13)
        Me.Label10.TabIndex = 19
        Me.Label10.Text = "Phone Office"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(16, 89)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(38, 13)
        Me.Label11.TabIndex = 20
        Me.Label11.Text = "Mobile"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(16, 111)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(24, 13)
        Me.Label12.TabIndex = 21
        Me.Label12.Text = "Fax"
        '
        'txtRemarks
        '
        Me.txtRemarks.Location = New System.Drawing.Point(471, 95)
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(303, 108)
        Me.txtRemarks.TabIndex = 7
        '
        'txtPhoneOffice
        '
        Me.txtPhoneOffice.Location = New System.Drawing.Point(117, 56)
        Me.txtPhoneOffice.Name = "txtPhoneOffice"
        Me.txtPhoneOffice.Size = New System.Drawing.Size(264, 20)
        Me.txtPhoneOffice.TabIndex = 2
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(406, 108)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(49, 13)
        Me.Label18.TabIndex = 35
        Me.Label18.Text = "Remarks"
        '
        'txtMobile
        '
        Me.txtMobile.Location = New System.Drawing.Point(117, 82)
        Me.txtMobile.Name = "txtMobile"
        Me.txtMobile.Size = New System.Drawing.Size(264, 20)
        Me.txtMobile.TabIndex = 3
        '
        'txtFax
        '
        Me.txtFax.Location = New System.Drawing.Point(117, 108)
        Me.txtFax.Name = "txtFax"
        Me.txtFax.Size = New System.Drawing.Size(264, 20)
        Me.txtFax.TabIndex = 4
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(16, 141)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(32, 13)
        Me.Label13.TabIndex = 25
        Me.Label13.Text = "Email"
        '
        'txtEmail
        '
        Me.txtEmail.Location = New System.Drawing.Point(117, 134)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(264, 20)
        Me.txtEmail.TabIndex = 5
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(406, 38)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(45, 13)
        Me.Label14.TabIndex = 27
        Me.Label14.Text = "Address"
        '
        'txtAddress
        '
        Me.txtAddress.Location = New System.Drawing.Point(471, 31)
        Me.txtAddress.Multiline = True
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(303, 58)
        Me.txtAddress.TabIndex = 6
        '
        'lblScreentext1
        '
        Me.lblScreentext1.AccessibleDescription = "Title"
        Me.lblScreentext1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblScreentext1.AutoSize = True
        Me.lblScreentext1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold)
        Me.lblScreentext1.ForeColor = System.Drawing.SystemColors.Desktop
        Me.lblScreentext1.Location = New System.Drawing.Point(6, 483)
        Me.lblScreentext1.Name = "lblScreentext1"
        Me.lblScreentext1.Size = New System.Drawing.Size(208, 24)
        Me.lblScreentext1.TabIndex = 49
        Me.lblScreentext1.Text = "Customer Information"
        Me.lblScreentext1.Visible = False
        '
        'lblScreentext
        '
        Me.lblScreentext.AccessibleDescription = "Title"
        Me.lblScreentext.AutoSize = True
        Me.lblScreentext.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold)
        Me.lblScreentext.ForeColor = System.Drawing.SystemColors.Desktop
        Me.lblScreentext.Location = New System.Drawing.Point(12, 9)
        Me.lblScreentext.Name = "lblScreentext"
        Me.lblScreentext.Size = New System.Drawing.Size(208, 24)
        Me.lblScreentext.TabIndex = 48
        Me.lblScreentext.Text = "Customer Information"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.GroupBox2)
        Me.TabPage2.Controls.Add(Me.GroupBox1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1020, 578)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Records"
        Me.TabPage2.ToolTipText = "Records"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.GroupBox6)
        Me.GroupBox2.Controls.Add(Me.btnReset)
        Me.GroupBox2.Controls.Add(Me.GroupBox5)
        Me.GroupBox2.Controls.Add(Me.GroupBox4)
        Me.GroupBox2.Controls.Add(Me.btnDtlSearch)
        Me.GroupBox2.Location = New System.Drawing.Point(6, 14)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1007, 160)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Detailed Search"
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.btnValidate)
        Me.GroupBox6.Controls.Add(Me.btnSendSMS)
        Me.GroupBox6.Controls.Add(Me.CmbSMSTemplates)
        Me.GroupBox6.Location = New System.Drawing.Point(8, 112)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(522, 42)
        Me.GroupBox6.TabIndex = 35
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "SMS Templates"
        '
        'btnValidate
        '
        Me.btnValidate.Location = New System.Drawing.Point(293, 13)
        Me.btnValidate.Name = "btnValidate"
        Me.btnValidate.Size = New System.Drawing.Size(86, 26)
        Me.btnValidate.TabIndex = 36
        Me.btnValidate.Text = "Validate"
        Me.btnValidate.UseVisualStyleBackColor = True
        '
        'CmbSMSTemplates
        '
        Me.CmbSMSTemplates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbSMSTemplates.FormattingEnabled = True
        Me.CmbSMSTemplates.Location = New System.Drawing.Point(22, 16)
        Me.CmbSMSTemplates.Name = "CmbSMSTemplates"
        Me.CmbSMSTemplates.Size = New System.Drawing.Size(255, 21)
        Me.CmbSMSTemplates.TabIndex = 27
        '
        'btnReset
        '
        Me.btnReset.Location = New System.Drawing.Point(695, 117)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(84, 32)
        Me.btnReset.TabIndex = 13
        Me.btnReset.Text = "Reset"
        Me.btnReset.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.Label24)
        Me.GroupBox5.Controls.Add(Me.Label22)
        Me.GroupBox5.Controls.Add(Me.dtpPaymentto_DtlSearch)
        Me.GroupBox5.Controls.Add(Me.dtpMaintEndDate_DtlSearch)
        Me.GroupBox5.Controls.Add(Me.dtpmaintTo_DtlSearch)
        Me.GroupBox5.Controls.Add(Me.Label23)
        Me.GroupBox5.Controls.Add(Me.Label30)
        Me.GroupBox5.Controls.Add(Me.dtpPaymentDate_DtlSearch)
        Me.GroupBox5.Controls.Add(Me.Label29)
        Me.GroupBox5.Controls.Add(Me.dtpCreationDate_DtlSearch)
        Me.GroupBox5.Controls.Add(Me.Label28)
        Me.GroupBox5.Controls.Add(Me.dtpCreationDate2_DtlSearch)
        Me.GroupBox5.Controls.Add(Me.Label27)
        Me.GroupBox5.Controls.Add(Me.Label25)
        Me.GroupBox5.Controls.Add(Me.Label26)
        Me.GroupBox5.Location = New System.Drawing.Point(537, 11)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(464, 100)
        Me.GroupBox5.TabIndex = 6
        Me.GroupBox5.TabStop = False
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(16, 19)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(72, 13)
        Me.Label24.TabIndex = 33
        Me.Label24.Text = "Creation Date"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(16, 47)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(84, 13)
        Me.Label22.TabIndex = 27
        Me.Label22.Text = "Maint. End Date"
        '
        'dtpPaymentto_DtlSearch
        '
        Me.dtpPaymentto_DtlSearch.Checked = False
        Me.dtpPaymentto_DtlSearch.CustomFormat = "dd/MM/yyyy"
        Me.dtpPaymentto_DtlSearch.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpPaymentto_DtlSearch.Location = New System.Drawing.Point(313, 70)
        Me.dtpPaymentto_DtlSearch.Name = "dtpPaymentto_DtlSearch"
        Me.dtpPaymentto_DtlSearch.ShowCheckBox = True
        Me.dtpPaymentto_DtlSearch.Size = New System.Drawing.Size(111, 20)
        Me.dtpPaymentto_DtlSearch.TabIndex = 11
        '
        'dtpMaintEndDate_DtlSearch
        '
        Me.dtpMaintEndDate_DtlSearch.Checked = False
        Me.dtpMaintEndDate_DtlSearch.CustomFormat = "dd/MM/yyyy"
        Me.dtpMaintEndDate_DtlSearch.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpMaintEndDate_DtlSearch.Location = New System.Drawing.Point(149, 40)
        Me.dtpMaintEndDate_DtlSearch.Name = "dtpMaintEndDate_DtlSearch"
        Me.dtpMaintEndDate_DtlSearch.ShowCheckBox = True
        Me.dtpMaintEndDate_DtlSearch.Size = New System.Drawing.Size(111, 20)
        Me.dtpMaintEndDate_DtlSearch.TabIndex = 8
        '
        'dtpmaintTo_DtlSearch
        '
        Me.dtpmaintTo_DtlSearch.Checked = False
        Me.dtpmaintTo_DtlSearch.CustomFormat = "dd/MM/yyyy"
        Me.dtpmaintTo_DtlSearch.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpmaintTo_DtlSearch.Location = New System.Drawing.Point(313, 40)
        Me.dtpmaintTo_DtlSearch.Name = "dtpmaintTo_DtlSearch"
        Me.dtpmaintTo_DtlSearch.ShowCheckBox = True
        Me.dtpmaintTo_DtlSearch.Size = New System.Drawing.Size(111, 20)
        Me.dtpmaintTo_DtlSearch.TabIndex = 9
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(16, 74)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(74, 13)
        Me.Label23.TabIndex = 30
        Me.Label23.Text = "Payment Date"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(280, 73)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(20, 13)
        Me.Label30.TabIndex = 44
        Me.Label30.Text = "To"
        '
        'dtpPaymentDate_DtlSearch
        '
        Me.dtpPaymentDate_DtlSearch.Checked = False
        Me.dtpPaymentDate_DtlSearch.CustomFormat = "dd/MM/yyyy"
        Me.dtpPaymentDate_DtlSearch.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpPaymentDate_DtlSearch.Location = New System.Drawing.Point(150, 70)
        Me.dtpPaymentDate_DtlSearch.Name = "dtpPaymentDate_DtlSearch"
        Me.dtpPaymentDate_DtlSearch.ShowCheckBox = True
        Me.dtpPaymentDate_DtlSearch.Size = New System.Drawing.Size(111, 20)
        Me.dtpPaymentDate_DtlSearch.TabIndex = 10
        Me.dtpPaymentDate_DtlSearch.Value = New Date(2011, 8, 8, 0, 0, 0, 0)
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(280, 46)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(20, 13)
        Me.Label29.TabIndex = 43
        Me.Label29.Text = "To"
        '
        'dtpCreationDate_DtlSearch
        '
        Me.dtpCreationDate_DtlSearch.Checked = False
        Me.dtpCreationDate_DtlSearch.CustomFormat = "dd/MM/yyyy"
        Me.dtpCreationDate_DtlSearch.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpCreationDate_DtlSearch.Location = New System.Drawing.Point(149, 13)
        Me.dtpCreationDate_DtlSearch.Name = "dtpCreationDate_DtlSearch"
        Me.dtpCreationDate_DtlSearch.ShowCheckBox = True
        Me.dtpCreationDate_DtlSearch.Size = New System.Drawing.Size(111, 20)
        Me.dtpCreationDate_DtlSearch.TabIndex = 6
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(280, 20)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(20, 13)
        Me.Label28.TabIndex = 42
        Me.Label28.Text = "To"
        '
        'dtpCreationDate2_DtlSearch
        '
        Me.dtpCreationDate2_DtlSearch.Checked = False
        Me.dtpCreationDate2_DtlSearch.CustomFormat = "dd/MM/yyyy"
        Me.dtpCreationDate2_DtlSearch.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpCreationDate2_DtlSearch.Location = New System.Drawing.Point(313, 12)
        Me.dtpCreationDate2_DtlSearch.Name = "dtpCreationDate2_DtlSearch"
        Me.dtpCreationDate2_DtlSearch.ShowCheckBox = True
        Me.dtpCreationDate2_DtlSearch.Size = New System.Drawing.Size(111, 20)
        Me.dtpCreationDate2_DtlSearch.TabIndex = 7
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(108, 77)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(30, 13)
        Me.Label27.TabIndex = 41
        Me.Label27.Text = "From"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(108, 19)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(30, 13)
        Me.Label25.TabIndex = 39
        Me.Label25.Text = "From"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(108, 46)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(30, 13)
        Me.Label26.TabIndex = 40
        Me.Label26.Text = "From"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Label2)
        Me.GroupBox4.Controls.Add(Me.cmbProductType_DtlSearch)
        Me.GroupBox4.Controls.Add(Me.cmbStatus_DtlSearch)
        Me.GroupBox4.Controls.Add(Me.cmbSLAType_DtlSearch)
        Me.GroupBox4.Controls.Add(Me.Label3)
        Me.GroupBox4.Controls.Add(Me.Label19)
        Me.GroupBox4.Controls.Add(Me.Label20)
        Me.GroupBox4.Controls.Add(Me.UiCtrlGLAccount2)
        Me.GroupBox4.Controls.Add(Me.Label21)
        Me.GroupBox4.Controls.Add(Me.txtContactPerson_dtlSearch)
        Me.GroupBox4.Location = New System.Drawing.Point(6, 11)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(524, 100)
        Me.GroupBox4.TabIndex = 1
        Me.GroupBox4.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(22, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 13)
        Me.Label2.TabIndex = 23
        Me.Label2.Text = "Account Code"
        '
        'cmbProductType_DtlSearch
        '
        Me.cmbProductType_DtlSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProductType_DtlSearch.FormattingEnabled = True
        Me.cmbProductType_DtlSearch.Items.AddRange(New Object() {"--Select--", "LightWave", "Personal", "Professional", "Enterperise"})
        Me.cmbProductType_DtlSearch.Location = New System.Drawing.Point(116, 41)
        Me.cmbProductType_DtlSearch.Name = "cmbProductType_DtlSearch"
        Me.cmbProductType_DtlSearch.Size = New System.Drawing.Size(146, 21)
        Me.cmbProductType_DtlSearch.TabIndex = 2
        '
        'cmbStatus_DtlSearch
        '
        Me.cmbStatus_DtlSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStatus_DtlSearch.FormattingEnabled = True
        Me.cmbStatus_DtlSearch.Items.AddRange(New Object() {"--Select--", "Ok", "Job-to-Job", "Blocked", "Suspended", "Partner"})
        Me.cmbStatus_DtlSearch.Location = New System.Drawing.Point(116, 68)
        Me.cmbStatus_DtlSearch.Name = "cmbStatus_DtlSearch"
        Me.cmbStatus_DtlSearch.Size = New System.Drawing.Size(146, 21)
        Me.cmbStatus_DtlSearch.TabIndex = 3
        '
        'cmbSLAType_DtlSearch
        '
        Me.cmbSLAType_DtlSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSLAType_DtlSearch.FormattingEnabled = True
        Me.cmbSLAType_DtlSearch.Items.AddRange(New Object() {"--Select--", "Warranty", "SLA1", "SLA2", "SLA3", "Job-to-Job", "Warranty Expired", "Partner"})
        Me.cmbSLAType_DtlSearch.Location = New System.Drawing.Point(362, 41)
        Me.cmbSLAType_DtlSearch.Name = "cmbSLAType_DtlSearch"
        Me.cmbSLAType_DtlSearch.Size = New System.Drawing.Size(146, 21)
        Me.cmbSLAType_DtlSearch.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(22, 49)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(71, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Product Type"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(22, 76)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(37, 13)
        Me.Label19.TabIndex = 4
        Me.Label19.Text = "Status"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(268, 49)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(54, 13)
        Me.Label20.TabIndex = 5
        Me.Label20.Text = "SLA Type"
        '
        'UiCtrlGLAccount2
        '
        Me.UiCtrlGLAccount2.AccountType = GLNet.EnumAccountTypes.Customer
        Me.UiCtrlGLAccount2.GLAccountCode = Nothing
        Me.UiCtrlGLAccount2.GLAccountHeadType = "4"
        Me.UiCtrlGLAccount2.GLAccountID = 0
        Me.UiCtrlGLAccount2.GLAccountName = Nothing
        Me.UiCtrlGLAccount2.GLFilterAccount = Nothing
        Me.UiCtrlGLAccount2.GLFilterCondition = Nothing
        Me.UiCtrlGLAccount2.Location = New System.Drawing.Point(113, 10)
        Me.UiCtrlGLAccount2.MinimumSize = New System.Drawing.Size(0, 30)
        Me.UiCtrlGLAccount2.Name = "UiCtrlGLAccount2"
        Me.UiCtrlGLAccount2.Size = New System.Drawing.Size(405, 30)
        Me.UiCtrlGLAccount2.TabIndex = 1
        Me.UiCtrlGLAccount2.Tag = Nothing
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(268, 76)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(80, 13)
        Me.Label21.TabIndex = 25
        Me.Label21.Text = "Contact Person"
        '
        'txtContactPerson_dtlSearch
        '
        Me.txtContactPerson_dtlSearch.Location = New System.Drawing.Point(362, 68)
        Me.txtContactPerson_dtlSearch.Name = "txtContactPerson_dtlSearch"
        Me.txtContactPerson_dtlSearch.Size = New System.Drawing.Size(146, 20)
        Me.txtContactPerson_dtlSearch.TabIndex = 5
        '
        'btnDtlSearch
        '
        Me.btnDtlSearch.Location = New System.Drawing.Point(785, 117)
        Me.btnDtlSearch.Name = "btnDtlSearch"
        Me.btnDtlSearch.Size = New System.Drawing.Size(84, 32)
        Me.btnDtlSearch.TabIndex = 12
        Me.btnDtlSearch.Text = "Detail Search"
        Me.btnDtlSearch.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.grdAllRecords)
        Me.GroupBox1.Controls.Add(Me.UiCtrlGridBar1)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 180)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1007, 392)
        Me.GroupBox1.TabIndex = 21
        Me.GroupBox1.TabStop = False
        '
        'grdAllRecords
        '
        Me.grdAllRecords.EditorsControlStyle.ButtonAppearance = Janus.Windows.GridEX.ButtonAppearance.Regular
        Me.grdAllRecords.EmptyRows = True
        Me.grdAllRecords.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdAllRecords.GroupByBoxVisible = False
        Me.grdAllRecords.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight
        Me.grdAllRecords.Location = New System.Drawing.Point(6, 41)
        Me.grdAllRecords.Name = "grdAllRecords"
        Me.grdAllRecords.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdAllRecords.Size = New System.Drawing.Size(995, 345)
        Me.grdAllRecords.TabIndex = 16
        Me.grdAllRecords.TabKeyBehavior = Janus.Windows.GridEX.TabKeyBehavior.ControlNavigation
        Me.grdAllRecords.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        '
        'UiCtrlGridBar1
        '
        Me.UiCtrlGridBar1.Location = New System.Drawing.Point(4, 10)
        Me.UiCtrlGridBar1.MyGrid = Me.grdAllRecords
        Me.UiCtrlGridBar1.Name = "UiCtrlGridBar1"
        Me.UiCtrlGridBar1.Size = New System.Drawing.Size(1000, 25)
        Me.UiCtrlGridBar1.TabIndex = 4
        Me.UiCtrlGridBar1.TabStop = False
        '
        'GrdView
        '
        Me.GrdView.Location = New System.Drawing.Point(0, 0)
        Me.GrdView.Name = "GrdView"
        Me.GrdView.Size = New System.Drawing.Size(400, 376)
        Me.GrdView.TabIndex = 0
        '
        'Timer1
        '
        '
        'frmCustomerInfo
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1030, 746)
        Me.ControlBox = False
        Me.Controls.Add(Me.TabCustomerInfo)
        Me.Controls.Add(Me.lblHeading)
        Me.Controls.Add(Me.Panel2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCustomerInfo"
        Me.Text = "Customer Information"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel2.ResumeLayout(False)
        Me.TabCustomerInfo.ResumeLayout(False)
        Me.TabPgCustomerInfo.ResumeLayout(False)
        Me.TabPgCustomerInfo.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.gbmaintdate.ResumeLayout(False)
        Me.gbmaintdate.PerformLayout()
        Me.gbContactPerson.ResumeLayout(False)
        Me.gbContactPerson.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.grdAllRecords, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnFirst As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnPrevious As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents btnLast As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents lblHeading As System.Windows.Forms.Label
    Friend WithEvents TabCustomerInfo As System.Windows.Forms.TabControl
    Friend WithEvents TabPgCustomerInfo As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents txtContactPerson As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbProduct As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtFax As System.Windows.Forms.TextBox
    Friend WithEvents txtMobile As System.Windows.Forms.TextBox
    Friend WithEvents txtPhoneOffice As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress As System.Windows.Forms.TextBox
    Friend WithEvents cmbSlaType As System.Windows.Forms.ComboBox
    Friend WithEvents cmbStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtMonthlyAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents UiCtrlGridBar1 As GLNet.uiCtrlGridBar
    Friend WithEvents UiCtrlGLAccount1 As GLNet.uiCtrlGLAccount
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtMaintEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtMaintStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents UiCtrlGLAccount2 As GLNet.uiCtrlGLAccount
    Friend WithEvents gbmaintdate As System.Windows.Forms.GroupBox
    Friend WithEvents gbContactPerson As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtCreationDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents dtPaymentRcvdDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents GrdView As Janus.Windows.GridEX.GridEX
    Friend WithEvents grdAllRecords As Janus.Windows.GridEX.GridEX
    Friend WithEvents lblScreentext As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbSLAType_DtlSearch As System.Windows.Forms.ComboBox
    Friend WithEvents cmbStatus_DtlSearch As System.Windows.Forms.ComboBox
    Friend WithEvents cmbProductType_DtlSearch As System.Windows.Forms.ComboBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtContactPerson_dtlSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents dtpMaintEndDate_DtlSearch As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpCreationDate_DtlSearch As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents dtpPaymentDate_DtlSearch As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents btnDtlSearch As System.Windows.Forms.Button
    Friend WithEvents dtpCreationDate2_DtlSearch As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpPaymentto_DtlSearch As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpmaintTo_DtlSearch As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents chkUpdateOnline As System.Windows.Forms.CheckBox
    Friend WithEvents pbOnlineActivation As System.Windows.Forms.ProgressBar
    Friend WithEvents lblScreentext1 As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents btnReset As System.Windows.Forms.Button
    Friend WithEvents btnSendSMS As System.Windows.Forms.Button
    Friend WithEvents CmbSMSTemplates As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents btnValidate As System.Windows.Forms.Button
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents txtSTNNum As System.Windows.Forms.TextBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents txtNTNNum As System.Windows.Forms.TextBox
End Class
