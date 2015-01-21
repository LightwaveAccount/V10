<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGLVoucher
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
        Dim grdVoucher_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.lblTotalRecord = New System.Windows.Forms.Label
        Me.lblPosted = New System.Windows.Forms.TextBox
        Me.cmdSearchVoucher = New System.Windows.Forms.Button
        Me.cmdPostVoucher = New System.Windows.Forms.Button
        Me.lblVoucherString = New System.Windows.Forms.TextBox
        Me.lblCompany = New System.Windows.Forms.Label
        Me.chkOtherVoucher = New System.Windows.Forms.CheckBox
        Me.dtpDueDate = New System.Windows.Forms.DateTimePicker
        Me.lblDueDate = New System.Windows.Forms.Label
        Me.lblVDate = New System.Windows.Forms.Label
        Me.dtpVoucherDate = New System.Windows.Forms.DateTimePicker
        Me.cboVoucherType = New System.Windows.Forms.ComboBox
        Me.txtFYearCode = New System.Windows.Forms.TextBox
        Me.lblFYear = New System.Windows.Forms.Label
        Me.txtVNo = New System.Windows.Forms.TextBox
        Me.lblVNo = New System.Windows.Forms.Label
        Me.lblVType = New System.Windows.Forms.Label
        Me.grdDetailEntry = New Janus.Windows.GridEX.GridEX
        Me.grdVoucher = New Janus.Windows.GridEX.GridEX
        Me.grpCashBankInfo = New System.Windows.Forms.GroupBox
        Me.lblDrCr = New System.Windows.Forms.Label
        Me.lblChqdate = New System.Windows.Forms.Label
        Me.dtpChequeDate = New System.Windows.Forms.DateTimePicker
        Me.txtVoucherNarration = New System.Windows.Forms.TextBox
        Me.txtAmount = New System.Windows.Forms.TextBox
        Me.lblAmount = New System.Windows.Forms.Label
        Me.txtChequeNo = New System.Windows.Forms.TextBox
        Me.lblChequeNo = New System.Windows.Forms.Label
        Me.lblVNarration = New System.Windows.Forms.Label
        Me.lblBankCashAc = New System.Windows.Forms.Label
        Me.BtnCheckPrint = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnPrintLandScape = New System.Windows.Forms.Button
        Me.btnPrintInvoice = New System.Windows.Forms.Button
        Me.btnprint = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnFirst = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnPrevious = New System.Windows.Forms.Button
        Me.btnDelete = New System.Windows.Forms.Button
        Me.btnNext = New System.Windows.Forms.Button
        Me.btnUpdate = New System.Windows.Forms.Button
        Me.btnLast = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnNew = New System.Windows.Forms.Button
        Me.lblScreentext = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.grpVoucherDtl = New System.Windows.Forms.GroupBox
        Me.grpVNoDetail = New System.Windows.Forms.Panel
        Me.cboSource = New System.Windows.Forms.ComboBox
        Me.cboCompany = New System.Windows.Forms.ComboBox
        Me.lblShop = New System.Windows.Forms.Label
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.VoucherToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnReport = New System.Windows.Forms.ToolStripMenuItem
        Me.btnNewReport = New System.Windows.Forms.ToolStripMenuItem
        Me.btnReportNew = New System.Windows.Forms.ToolStripMenuItem
        Me.btnPrevCostReport = New System.Windows.Forms.ToolStripMenuItem
        Me.btnReport3Inch = New System.Windows.Forms.ToolStripMenuItem
        Me.btnReportNewWitOutCost = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem5 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem6 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem7 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem8 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem9 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem10 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem11 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem12 = New System.Windows.Forms.ToolStripMenuItem
        Me.ctrlGLAccounts = New GLNet.uiCtrlGLAccount
        Me.UiCtrlGLAccHidden = New GLNet.uiCtrlGLAccount
        Me.UiCtrlGridBar1 = New GLNet.uiCtrlGridBar
        Me.GroupBox3.SuspendLayout()
        CType(Me.grdDetailEntry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdVoucher, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCashBankInfo.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.grpVoucherDtl.SuspendLayout()
        Me.grpVNoDetail.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.lblTotalRecord)
        Me.GroupBox3.Controls.Add(Me.lblPosted)
        Me.GroupBox3.Controls.Add(Me.cmdSearchVoucher)
        Me.GroupBox3.Controls.Add(Me.cmdPostVoucher)
        Me.GroupBox3.Controls.Add(Me.lblVoucherString)
        Me.GroupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.GroupBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(755, 4)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(245, 156)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Selected Voucher Detail"
        '
        'lblTotalRecord
        '
        Me.lblTotalRecord.BackColor = System.Drawing.Color.DarkGray
        Me.lblTotalRecord.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalRecord.Location = New System.Drawing.Point(7, 130)
        Me.lblTotalRecord.Name = "lblTotalRecord"
        Me.lblTotalRecord.Size = New System.Drawing.Size(228, 19)
        Me.lblTotalRecord.TabIndex = 2
        Me.lblTotalRecord.Text = "Record Of Total Rec"
        '
        'lblPosted
        '
        Me.lblPosted.BackColor = System.Drawing.Color.DarkGray
        Me.lblPosted.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPosted.ForeColor = System.Drawing.Color.White
        Me.lblPosted.Location = New System.Drawing.Point(8, 47)
        Me.lblPosted.MaxLength = 9
        Me.lblPosted.Name = "lblPosted"
        Me.lblPosted.ReadOnly = True
        Me.lblPosted.Size = New System.Drawing.Size(227, 20)
        Me.lblPosted.TabIndex = 1
        Me.lblPosted.TabStop = False
        Me.lblPosted.Tag = ""
        '
        'cmdSearchVoucher
        '
        Me.cmdSearchVoucher.Location = New System.Drawing.Point(6, 72)
        Me.cmdSearchVoucher.Name = "cmdSearchVoucher"
        Me.cmdSearchVoucher.Size = New System.Drawing.Size(229, 24)
        Me.cmdSearchVoucher.TabIndex = 3
        Me.cmdSearchVoucher.TabStop = False
        Me.cmdSearchVoucher.Tag = "HideText"
        Me.cmdSearchVoucher.Text = " Search Voucher         (Ctrl+R)"
        Me.VoucherToolTip.SetToolTip(Me.cmdSearchVoucher, "Search Voucher (Ctrl+R)")
        Me.cmdSearchVoucher.UseVisualStyleBackColor = True
        '
        'cmdPostVoucher
        '
        Me.cmdPostVoucher.Location = New System.Drawing.Point(6, 101)
        Me.cmdPostVoucher.Name = "cmdPostVoucher"
        Me.cmdPostVoucher.Size = New System.Drawing.Size(229, 24)
        Me.cmdPostVoucher.TabIndex = 4
        Me.cmdPostVoucher.TabStop = False
        Me.cmdPostVoucher.Tag = "HideText"
        Me.cmdPostVoucher.Text = "Post this Voucher       (Ctrl+T)"
        Me.VoucherToolTip.SetToolTip(Me.cmdPostVoucher, "Post Voucher (Ctrl+T)")
        Me.cmdPostVoucher.UseVisualStyleBackColor = True
        '
        'lblVoucherString
        '
        Me.lblVoucherString.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVoucherString.Location = New System.Drawing.Point(8, 17)
        Me.lblVoucherString.MaxLength = 9
        Me.lblVoucherString.Multiline = True
        Me.lblVoucherString.Name = "lblVoucherString"
        Me.lblVoucherString.ReadOnly = True
        Me.lblVoucherString.Size = New System.Drawing.Size(227, 25)
        Me.lblVoucherString.TabIndex = 0
        Me.lblVoucherString.TabStop = False
        Me.lblVoucherString.Tag = ""
        '
        'lblCompany
        '
        Me.lblCompany.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompany.Location = New System.Drawing.Point(161, 8)
        Me.lblCompany.Name = "lblCompany"
        Me.lblCompany.Size = New System.Drawing.Size(61, 15)
        Me.lblCompany.TabIndex = 2
        Me.lblCompany.Text = "Company"
        '
        'chkOtherVoucher
        '
        Me.chkOtherVoucher.AutoSize = True
        Me.chkOtherVoucher.Location = New System.Drawing.Point(882, 164)
        Me.chkOtherVoucher.Name = "chkOtherVoucher"
        Me.chkOtherVoucher.Size = New System.Drawing.Size(95, 17)
        Me.chkOtherVoucher.TabIndex = 6
        Me.chkOtherVoucher.TabStop = False
        Me.chkOtherVoucher.Text = "Other Voucher"
        Me.chkOtherVoucher.UseVisualStyleBackColor = True
        Me.chkOtherVoucher.Visible = False
        '
        'dtpDueDate
        '
        Me.dtpDueDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDueDate.Location = New System.Drawing.Point(624, 32)
        Me.dtpDueDate.Name = "dtpDueDate"
        Me.dtpDueDate.ShowCheckBox = True
        Me.dtpDueDate.Size = New System.Drawing.Size(106, 20)
        Me.dtpDueDate.TabIndex = 5
        Me.dtpDueDate.TabStop = False
        '
        'lblDueDate
        '
        Me.lblDueDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDueDate.Location = New System.Drawing.Point(533, 36)
        Me.lblDueDate.Name = "lblDueDate"
        Me.lblDueDate.Size = New System.Drawing.Size(62, 17)
        Me.lblDueDate.TabIndex = 11
        Me.lblDueDate.Text = "Due Date"
        '
        'lblVDate
        '
        Me.lblVDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVDate.Location = New System.Drawing.Point(533, 7)
        Me.lblVDate.Name = "lblVDate"
        Me.lblVDate.Size = New System.Drawing.Size(86, 17)
        Me.lblVDate.TabIndex = 9
        Me.lblVDate.Text = "Voucher Date"
        '
        'dtpVoucherDate
        '
        Me.dtpVoucherDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpVoucherDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpVoucherDate.Location = New System.Drawing.Point(624, 5)
        Me.dtpVoucherDate.Name = "dtpVoucherDate"
        Me.dtpVoucherDate.Size = New System.Drawing.Size(106, 20)
        Me.dtpVoucherDate.TabIndex = 4
        '
        'cboVoucherType
        '
        Me.cboVoucherType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboVoucherType.FormattingEnabled = True
        Me.cboVoucherType.Location = New System.Drawing.Point(60, 32)
        Me.cboVoucherType.Name = "cboVoucherType"
        Me.cboVoucherType.Size = New System.Drawing.Size(89, 21)
        Me.cboVoucherType.TabIndex = 2
        Me.cboVoucherType.Tag = "IsRequired"
        '
        'txtFYearCode
        '
        Me.txtFYearCode.Location = New System.Drawing.Point(60, 5)
        Me.txtFYearCode.MaxLength = 9
        Me.txtFYearCode.Name = "txtFYearCode"
        Me.txtFYearCode.ReadOnly = True
        Me.txtFYearCode.Size = New System.Drawing.Size(89, 20)
        Me.txtFYearCode.TabIndex = 0
        Me.txtFYearCode.Tag = ""
        '
        'lblFYear
        '
        Me.lblFYear.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFYear.Location = New System.Drawing.Point(5, 8)
        Me.lblFYear.Name = "lblFYear"
        Me.lblFYear.Size = New System.Drawing.Size(48, 15)
        Me.lblFYear.TabIndex = 0
        Me.lblFYear.Text = "F. Year"
        '
        'txtVNo
        '
        Me.txtVNo.BackColor = System.Drawing.SystemColors.Control
        Me.txtVNo.Enabled = False
        Me.txtVNo.Location = New System.Drawing.Point(224, 34)
        Me.txtVNo.MaxLength = 6
        Me.txtVNo.Name = "txtVNo"
        Me.txtVNo.ReadOnly = True
        Me.txtVNo.Size = New System.Drawing.Size(74, 20)
        Me.txtVNo.TabIndex = 3
        Me.txtVNo.Tag = "IsRequired"
        '
        'lblVNo
        '
        Me.lblVNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVNo.Location = New System.Drawing.Point(161, 37)
        Me.lblVNo.Name = "lblVNo"
        Me.lblVNo.Size = New System.Drawing.Size(56, 16)
        Me.lblVNo.TabIndex = 7
        Me.lblVNo.Text = "V. No"
        '
        'lblVType
        '
        Me.lblVType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVType.Location = New System.Drawing.Point(5, 35)
        Me.lblVType.Name = "lblVType"
        Me.lblVType.Size = New System.Drawing.Size(51, 15)
        Me.lblVType.TabIndex = 5
        Me.lblVType.Text = "V. Type"
        '
        'grdDetailEntry
        '
        Me.grdDetailEntry.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdDetailEntry.AutomaticSort = False
        Me.grdDetailEntry.EditorsControlStyle.ButtonAppearance = Janus.Windows.GridEX.ButtonAppearance.Regular
        Me.grdDetailEntry.EmptyRows = True
        Me.grdDetailEntry.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdDetailEntry.GroupByBoxVisible = False
        Me.grdDetailEntry.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight
        Me.grdDetailEntry.Location = New System.Drawing.Point(3, 33)
        Me.grdDetailEntry.Name = "grdDetailEntry"
        Me.grdDetailEntry.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdDetailEntry.Size = New System.Drawing.Size(993, 335)
        Me.grdDetailEntry.TabIndex = 0
        Me.grdDetailEntry.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        '
        'grdVoucher
        '
        grdVoucher_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /></RootTable></GridEXLayoutData>"
        Me.grdVoucher.DesignTimeLayout = grdVoucher_DesignTimeLayout
        Me.grdVoucher.EditorsControlStyle.ButtonAppearance = Janus.Windows.GridEX.ButtonAppearance.Regular
        Me.grdVoucher.EmptyRows = True
        Me.grdVoucher.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdVoucher.GroupByBoxVisible = False
        Me.grdVoucher.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight
        Me.grdVoucher.Location = New System.Drawing.Point(19, 46)
        Me.grdVoucher.Name = "grdVoucher"
        Me.grdVoucher.NewRowPosition = Janus.Windows.GridEX.NewRowPosition.BottomRow
        Me.grdVoucher.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdVoucher.Size = New System.Drawing.Size(830, 76)
        Me.grdVoucher.TabIndex = 1
        Me.grdVoucher.TabKeyBehavior = Janus.Windows.GridEX.TabKeyBehavior.ControlNavigation
        Me.grdVoucher.TabStop = False
        Me.grdVoucher.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdVoucher.Visible = False
        '
        'grpCashBankInfo
        '
        Me.grpCashBankInfo.BackColor = System.Drawing.SystemColors.Control
        Me.grpCashBankInfo.Controls.Add(Me.lblDrCr)
        Me.grpCashBankInfo.Controls.Add(Me.ctrlGLAccounts)
        Me.grpCashBankInfo.Controls.Add(Me.lblChqdate)
        Me.grpCashBankInfo.Controls.Add(Me.dtpChequeDate)
        Me.grpCashBankInfo.Controls.Add(Me.txtVoucherNarration)
        Me.grpCashBankInfo.Controls.Add(Me.txtAmount)
        Me.grpCashBankInfo.Controls.Add(Me.lblAmount)
        Me.grpCashBankInfo.Controls.Add(Me.txtChequeNo)
        Me.grpCashBankInfo.Controls.Add(Me.lblChequeNo)
        Me.grpCashBankInfo.Controls.Add(Me.lblVNarration)
        Me.grpCashBankInfo.Controls.Add(Me.lblBankCashAc)
        Me.grpCashBankInfo.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.grpCashBankInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpCashBankInfo.Location = New System.Drawing.Point(2, 84)
        Me.grpCashBankInfo.Name = "grpCashBankInfo"
        Me.grpCashBankInfo.Size = New System.Drawing.Size(747, 98)
        Me.grpCashBankInfo.TabIndex = 1
        Me.grpCashBankInfo.TabStop = False
        Me.grpCashBankInfo.Text = "Bank / Cash Info"
        '
        'lblDrCr
        '
        Me.lblDrCr.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDrCr.Location = New System.Drawing.Point(590, 73)
        Me.lblDrCr.Name = "lblDrCr"
        Me.lblDrCr.Size = New System.Drawing.Size(99, 16)
        Me.lblDrCr.TabIndex = 5
        Me.lblDrCr.Text = "V. No"
        '
        'lblChqdate
        '
        Me.lblChqdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChqdate.Location = New System.Drawing.Point(224, 73)
        Me.lblChqdate.Name = "lblChqdate"
        Me.lblChqdate.Size = New System.Drawing.Size(74, 17)
        Me.lblChqdate.TabIndex = 7
        Me.lblChqdate.Text = "Cheque Date"
        '
        'dtpChequeDate
        '
        Me.dtpChequeDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpChequeDate.Enabled = False
        Me.dtpChequeDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpChequeDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpChequeDate.Location = New System.Drawing.Point(304, 71)
        Me.dtpChequeDate.Name = "dtpChequeDate"
        Me.dtpChequeDate.ShowCheckBox = True
        Me.dtpChequeDate.Size = New System.Drawing.Size(96, 20)
        Me.dtpChequeDate.TabIndex = 3
        '
        'txtVoucherNarration
        '
        Me.txtVoucherNarration.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVoucherNarration.Location = New System.Drawing.Point(129, 44)
        Me.txtVoucherNarration.MaxLength = 500
        Me.txtVoucherNarration.Name = "txtVoucherNarration"
        Me.txtVoucherNarration.Size = New System.Drawing.Size(571, 20)
        Me.txtVoucherNarration.TabIndex = 1
        Me.txtVoucherNarration.TabStop = False
        Me.txtVoucherNarration.Tag = ""
        '
        'txtAmount
        '
        Me.txtAmount.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmount.Location = New System.Drawing.Point(463, 71)
        Me.txtAmount.MaxLength = 100
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.ReadOnly = True
        Me.txtAmount.Size = New System.Drawing.Size(112, 20)
        Me.txtAmount.TabIndex = 4
        Me.txtAmount.TabStop = False
        Me.txtAmount.Tag = ""
        '
        'lblAmount
        '
        Me.lblAmount.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmount.Location = New System.Drawing.Point(413, 74)
        Me.lblAmount.Name = "lblAmount"
        Me.lblAmount.Size = New System.Drawing.Size(49, 15)
        Me.lblAmount.TabIndex = 9
        Me.lblAmount.Text = "Amount"
        '
        'txtChequeNo
        '
        Me.txtChequeNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChequeNo.Location = New System.Drawing.Point(129, 71)
        Me.txtChequeNo.MaxLength = 25
        Me.txtChequeNo.Name = "txtChequeNo"
        Me.txtChequeNo.Size = New System.Drawing.Size(91, 20)
        Me.txtChequeNo.TabIndex = 2
        Me.txtChequeNo.Tag = ""
        '
        'lblChequeNo
        '
        Me.lblChequeNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChequeNo.Location = New System.Drawing.Point(18, 74)
        Me.lblChequeNo.Name = "lblChequeNo"
        Me.lblChequeNo.Size = New System.Drawing.Size(94, 15)
        Me.lblChequeNo.TabIndex = 5
        Me.lblChequeNo.Text = "Cheque No."
        '
        'lblVNarration
        '
        Me.lblVNarration.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVNarration.Location = New System.Drawing.Point(18, 47)
        Me.lblVNarration.Name = "lblVNarration"
        Me.lblVNarration.Size = New System.Drawing.Size(108, 15)
        Me.lblVNarration.TabIndex = 3
        Me.lblVNarration.Text = "Voucher Narration"
        '
        'lblBankCashAc
        '
        Me.lblBankCashAc.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBankCashAc.Location = New System.Drawing.Point(18, 22)
        Me.lblBankCashAc.Name = "lblBankCashAc"
        Me.lblBankCashAc.Size = New System.Drawing.Size(94, 15)
        Me.lblBankCashAc.TabIndex = 0
        Me.lblBankCashAc.Text = "Cash / Bank A/C"
        '
        'BtnCheckPrint
        '
        Me.BtnCheckPrint.Enabled = False
        Me.BtnCheckPrint.Location = New System.Drawing.Point(693, 1)
        Me.BtnCheckPrint.Name = "BtnCheckPrint"
        Me.BtnCheckPrint.Size = New System.Drawing.Size(50, 47)
        Me.BtnCheckPrint.TabIndex = 12
        Me.BtnCheckPrint.Tag = "HideText"
        Me.BtnCheckPrint.Text = " Check Print"
        Me.VoucherToolTip.SetToolTip(Me.BtnCheckPrint, "Print (Ctrl+P)")
        Me.BtnCheckPrint.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnPrintLandScape)
        Me.Panel1.Controls.Add(Me.BtnCheckPrint)
        Me.Panel1.Controls.Add(Me.btnPrintInvoice)
        Me.Panel1.Controls.Add(Me.btnprint)
        Me.Panel1.Controls.Add(Me.btnExit)
        Me.Panel1.Controls.Add(Me.btnFirst)
        Me.Panel1.Controls.Add(Me.btnCancel)
        Me.Panel1.Controls.Add(Me.btnPrevious)
        Me.Panel1.Controls.Add(Me.btnDelete)
        Me.Panel1.Controls.Add(Me.btnNext)
        Me.Panel1.Controls.Add(Me.btnUpdate)
        Me.Panel1.Controls.Add(Me.btnLast)
        Me.Panel1.Controls.Add(Me.btnSave)
        Me.Panel1.Controls.Add(Me.btnNew)
        Me.Panel1.Location = New System.Drawing.Point(-2, 607)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1051, 50)
        Me.Panel1.TabIndex = 0
        '
        'btnPrintLandScape
        '
        Me.btnPrintLandScape.Location = New System.Drawing.Point(872, -1)
        Me.btnPrintLandScape.Name = "btnPrintLandScape"
        Me.btnPrintLandScape.Size = New System.Drawing.Size(48, 48)
        Me.btnPrintLandScape.TabIndex = 13
        Me.btnPrintLandScape.Tag = "HideText"
        Me.VoucherToolTip.SetToolTip(Me.btnPrintLandScape, "Land Scape Voucher Print")
        Me.btnPrintLandScape.UseVisualStyleBackColor = True
        '
        'btnPrintInvoice
        '
        Me.btnPrintInvoice.Location = New System.Drawing.Point(754, 0)
        Me.btnPrintInvoice.Name = "btnPrintInvoice"
        Me.btnPrintInvoice.Size = New System.Drawing.Size(50, 48)
        Me.btnPrintInvoice.TabIndex = 11
        Me.btnPrintInvoice.Tag = "HideText"
        Me.btnPrintInvoice.Text = " "
        Me.VoucherToolTip.SetToolTip(Me.btnPrintInvoice, "Print PO or Sale Invoice")
        Me.btnPrintInvoice.UseVisualStyleBackColor = True
        '
        'btnprint
        '
        Me.btnprint.Location = New System.Drawing.Point(815, -1)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(48, 48)
        Me.btnprint.TabIndex = 9
        Me.btnprint.Tag = "HideText"
        Me.btnprint.Text = " "
        Me.VoucherToolTip.SetToolTip(Me.btnprint, "Print (Ctrl+P)")
        Me.btnprint.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(965, 0)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(48, 48)
        Me.btnExit.TabIndex = 10
        Me.btnExit.Tag = "HideText"
        Me.btnExit.Text = " "
        Me.VoucherToolTip.SetToolTip(Me.btnExit, "Exit (Ctrl+X)")
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnFirst
        '
        Me.btnFirst.Location = New System.Drawing.Point(12, 0)
        Me.btnFirst.Name = "btnFirst"
        Me.btnFirst.Size = New System.Drawing.Size(48, 48)
        Me.btnFirst.TabIndex = 0
        Me.btnFirst.Tag = "HideText"
        Me.btnFirst.Text = " "
        Me.VoucherToolTip.SetToolTip(Me.btnFirst, "First")
        Me.btnFirst.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(565, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(48, 48)
        Me.btnCancel.TabIndex = 8
        Me.btnCancel.Tag = "HideText"
        Me.btnCancel.Text = " "
        Me.VoucherToolTip.SetToolTip(Me.btnCancel, "Cancel (Ctrl+E)")
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnPrevious
        '
        Me.btnPrevious.Location = New System.Drawing.Point(66, 0)
        Me.btnPrevious.Name = "btnPrevious"
        Me.btnPrevious.Size = New System.Drawing.Size(48, 48)
        Me.btnPrevious.TabIndex = 1
        Me.btnPrevious.Tag = "HideText"
        Me.btnPrevious.Text = " "
        Me.VoucherToolTip.SetToolTip(Me.btnPrevious, "Previous (Ctrl+B)")
        Me.btnPrevious.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(511, 0)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(48, 48)
        Me.btnDelete.TabIndex = 7
        Me.btnDelete.Tag = "HideText"
        Me.btnDelete.Text = " "
        Me.VoucherToolTip.SetToolTip(Me.btnDelete, "Delete (Ctrl+D)")
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnNext
        '
        Me.btnNext.Location = New System.Drawing.Point(120, 0)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(48, 48)
        Me.btnNext.TabIndex = 2
        Me.btnNext.Tag = "HideText"
        Me.btnNext.Text = " "
        Me.VoucherToolTip.SetToolTip(Me.btnNext, "Next (Ctrl+F)")
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'btnUpdate
        '
        Me.btnUpdate.Location = New System.Drawing.Point(457, 0)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(48, 48)
        Me.btnUpdate.TabIndex = 6
        Me.btnUpdate.Tag = "HideText"
        Me.btnUpdate.Text = " "
        Me.VoucherToolTip.SetToolTip(Me.btnUpdate, "Update (Ctrl+U)")
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'btnLast
        '
        Me.btnLast.Location = New System.Drawing.Point(174, 0)
        Me.btnLast.Name = "btnLast"
        Me.btnLast.Size = New System.Drawing.Size(48, 48)
        Me.btnLast.TabIndex = 3
        Me.btnLast.Tag = "HideText"
        Me.btnLast.Text = " "
        Me.VoucherToolTip.SetToolTip(Me.btnLast, "Last")
        Me.btnLast.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(403, 0)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(48, 48)
        Me.btnSave.TabIndex = 5
        Me.btnSave.Tag = "HideText"
        Me.btnSave.Text = " "
        Me.VoucherToolTip.SetToolTip(Me.btnSave, "Save (Ctrl+S)")
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnNew
        '
        Me.btnNew.Location = New System.Drawing.Point(349, 0)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(48, 48)
        Me.btnNew.TabIndex = 4
        Me.btnNew.Tag = "HideText"
        Me.btnNew.Text = " "
        Me.VoucherToolTip.SetToolTip(Me.btnNew, "New (Ctrl+N)")
        Me.btnNew.UseVisualStyleBackColor = True
        '
        'lblScreentext
        '
        Me.lblScreentext.AccessibleDescription = "Title"
        Me.lblScreentext.AutoSize = True
        Me.lblScreentext.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold)
        Me.lblScreentext.ForeColor = System.Drawing.SystemColors.Desktop
        Me.lblScreentext.Location = New System.Drawing.Point(7, 0)
        Me.lblScreentext.Name = "lblScreentext"
        Me.lblScreentext.Size = New System.Drawing.Size(144, 24)
        Me.lblScreentext.TabIndex = 1
        Me.lblScreentext.Text = "Voucher Entry"
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.Panel4)
        Me.Panel2.Controls.Add(Me.Panel3)
        Me.Panel2.Controls.Add(Me.lblScreentext)
        Me.Panel2.Location = New System.Drawing.Point(3, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1019, 601)
        Me.Panel2.TabIndex = 0
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.grpVoucherDtl)
        Me.Panel4.Controls.Add(Me.grpCashBankInfo)
        Me.Panel4.Controls.Add(Me.GroupBox3)
        Me.Panel4.Controls.Add(Me.chkOtherVoucher)
        Me.Panel4.Location = New System.Drawing.Point(6, 27)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1006, 190)
        Me.Panel4.TabIndex = 1
        '
        'grpVoucherDtl
        '
        Me.grpVoucherDtl.Controls.Add(Me.grpVNoDetail)
        Me.grpVoucherDtl.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpVoucherDtl.Location = New System.Drawing.Point(2, 0)
        Me.grpVoucherDtl.Name = "grpVoucherDtl"
        Me.grpVoucherDtl.Size = New System.Drawing.Size(747, 80)
        Me.grpVoucherDtl.TabIndex = 0
        Me.grpVoucherDtl.TabStop = False
        Me.grpVoucherDtl.Text = "Voucher No. Detail"
        '
        'grpVNoDetail
        '
        Me.grpVNoDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grpVNoDetail.Controls.Add(Me.cboSource)
        Me.grpVNoDetail.Controls.Add(Me.cboCompany)
        Me.grpVNoDetail.Controls.Add(Me.lblShop)
        Me.grpVNoDetail.Controls.Add(Me.cboVoucherType)
        Me.grpVNoDetail.Controls.Add(Me.txtFYearCode)
        Me.grpVNoDetail.Controls.Add(Me.lblVType)
        Me.grpVNoDetail.Controls.Add(Me.lblCompany)
        Me.grpVNoDetail.Controls.Add(Me.lblVNo)
        Me.grpVNoDetail.Controls.Add(Me.txtVNo)
        Me.grpVNoDetail.Controls.Add(Me.dtpDueDate)
        Me.grpVNoDetail.Controls.Add(Me.lblFYear)
        Me.grpVNoDetail.Controls.Add(Me.lblDueDate)
        Me.grpVNoDetail.Controls.Add(Me.dtpVoucherDate)
        Me.grpVNoDetail.Controls.Add(Me.lblVDate)
        Me.grpVNoDetail.Location = New System.Drawing.Point(3, 15)
        Me.grpVNoDetail.Name = "grpVNoDetail"
        Me.grpVNoDetail.Size = New System.Drawing.Size(738, 62)
        Me.grpVNoDetail.TabIndex = 0
        '
        'cboSource
        '
        Me.cboSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSource.FormattingEnabled = True
        Me.cboSource.Location = New System.Drawing.Point(353, 32)
        Me.cboSource.Name = "cboSource"
        Me.cboSource.Size = New System.Drawing.Size(169, 21)
        Me.cboSource.TabIndex = 8
        Me.cboSource.Tag = "IsRequired"
        '
        'cboCompany
        '
        Me.cboCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCompany.FormattingEnabled = True
        Me.cboCompany.Location = New System.Drawing.Point(223, 5)
        Me.cboCompany.Name = "cboCompany"
        Me.cboCompany.Size = New System.Drawing.Size(299, 21)
        Me.cboCompany.TabIndex = 6
        Me.cboCompany.Tag = "IsRequired"
        '
        'lblShop
        '
        Me.lblShop.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblShop.Location = New System.Drawing.Point(301, 36)
        Me.lblShop.Name = "lblShop"
        Me.lblShop.Size = New System.Drawing.Size(60, 15)
        Me.lblShop.TabIndex = 7
        Me.lblShop.Text = "Source"
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.grdDetailEntry)
        Me.Panel3.Controls.Add(Me.UiCtrlGLAccHidden)
        Me.Panel3.Controls.Add(Me.UiCtrlGridBar1)
        Me.Panel3.Controls.Add(Me.grdVoucher)
        Me.Panel3.Location = New System.Drawing.Point(6, 223)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1006, 373)
        Me.Panel3.TabIndex = 2
        '
        'btnReport
        '
        Me.btnReport.Name = "btnReport"
        Me.btnReport.Size = New System.Drawing.Size(223, 22)
        Me.btnReport.Text = "Linear Reports"
        '
        'btnNewReport
        '
        Me.btnNewReport.Name = "btnNewReport"
        Me.btnNewReport.Size = New System.Drawing.Size(223, 22)
        Me.btnNewReport.Text = "Matrix Report"
        '
        'btnReportNew
        '
        Me.btnReportNew.Name = "btnReportNew"
        Me.btnReportNew.Size = New System.Drawing.Size(223, 22)
        Me.btnReportNew.Text = "Landscape Report"
        '
        'btnPrevCostReport
        '
        Me.btnPrevCostReport.Name = "btnPrevCostReport"
        Me.btnPrevCostReport.Size = New System.Drawing.Size(223, 22)
        Me.btnPrevCostReport.Text = "Previous Cost Report"
        '
        'btnReport3Inch
        '
        Me.btnReport3Inch.Name = "btnReport3Inch"
        Me.btnReport3Inch.Size = New System.Drawing.Size(223, 22)
        Me.btnReport3Inch.Text = "3 Inch Report"
        '
        'btnReportNewWitOutCost
        '
        Me.btnReportNewWitOutCost.Name = "btnReportNewWitOutCost"
        Me.btnReportNewWitOutCost.Size = New System.Drawing.Size(223, 22)
        Me.btnReportNewWitOutCost.Text = "Matrix Report With Out Cost"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(223, 22)
        Me.ToolStripMenuItem1.Text = "Linear Reports"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(223, 22)
        Me.ToolStripMenuItem2.Text = "Matrix Report"
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(223, 22)
        Me.ToolStripMenuItem3.Text = "Landscape Report"
        '
        'ToolStripMenuItem4
        '
        Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        Me.ToolStripMenuItem4.Size = New System.Drawing.Size(223, 22)
        Me.ToolStripMenuItem4.Text = "Previous Cost Report"
        '
        'ToolStripMenuItem5
        '
        Me.ToolStripMenuItem5.Name = "ToolStripMenuItem5"
        Me.ToolStripMenuItem5.Size = New System.Drawing.Size(223, 22)
        Me.ToolStripMenuItem5.Text = "3 Inch Report"
        '
        'ToolStripMenuItem6
        '
        Me.ToolStripMenuItem6.Name = "ToolStripMenuItem6"
        Me.ToolStripMenuItem6.Size = New System.Drawing.Size(223, 22)
        Me.ToolStripMenuItem6.Text = "Matrix Report With Out Cost"
        '
        'ToolStripMenuItem7
        '
        Me.ToolStripMenuItem7.Name = "ToolStripMenuItem7"
        Me.ToolStripMenuItem7.Size = New System.Drawing.Size(223, 22)
        Me.ToolStripMenuItem7.Text = "Linear Reports"
        '
        'ToolStripMenuItem8
        '
        Me.ToolStripMenuItem8.Name = "ToolStripMenuItem8"
        Me.ToolStripMenuItem8.Size = New System.Drawing.Size(223, 22)
        Me.ToolStripMenuItem8.Text = "Matrix Report"
        '
        'ToolStripMenuItem9
        '
        Me.ToolStripMenuItem9.Name = "ToolStripMenuItem9"
        Me.ToolStripMenuItem9.Size = New System.Drawing.Size(223, 22)
        Me.ToolStripMenuItem9.Text = "Landscape Report"
        '
        'ToolStripMenuItem10
        '
        Me.ToolStripMenuItem10.Name = "ToolStripMenuItem10"
        Me.ToolStripMenuItem10.Size = New System.Drawing.Size(223, 22)
        Me.ToolStripMenuItem10.Text = "Previous Cost Report"
        '
        'ToolStripMenuItem11
        '
        Me.ToolStripMenuItem11.Name = "ToolStripMenuItem11"
        Me.ToolStripMenuItem11.Size = New System.Drawing.Size(223, 22)
        Me.ToolStripMenuItem11.Text = "3 Inch Report"
        '
        'ToolStripMenuItem12
        '
        Me.ToolStripMenuItem12.Name = "ToolStripMenuItem12"
        Me.ToolStripMenuItem12.Size = New System.Drawing.Size(223, 22)
        Me.ToolStripMenuItem12.Text = "Matrix Report With Out Cost"
        '
        'ctrlGLAccounts
        '
        Me.ctrlGLAccounts.AccountType = GLNet.EnumAccountTypes.None
        Me.ctrlGLAccounts.GLAccountCode = Nothing
        Me.ctrlGLAccounts.GLAccountHeadType = "4"
        Me.ctrlGLAccounts.GLAccountID = 0
        Me.ctrlGLAccounts.GLAccountName = Nothing
        Me.ctrlGLAccounts.GLFilterAccount = Nothing
        Me.ctrlGLAccounts.GLFilterCondition = Nothing
        Me.ctrlGLAccounts.Location = New System.Drawing.Point(127, 13)
        Me.ctrlGLAccounts.MinimumSize = New System.Drawing.Size(0, 30)
        Me.ctrlGLAccounts.Name = "ctrlGLAccounts"
        Me.ctrlGLAccounts.Size = New System.Drawing.Size(602, 30)
        Me.ctrlGLAccounts.TabIndex = 0
        Me.ctrlGLAccounts.Tag = Nothing
        '
        'UiCtrlGLAccHidden
        '
        Me.UiCtrlGLAccHidden.AccountType = GLNet.EnumAccountTypes.None
        Me.UiCtrlGLAccHidden.GLAccountCode = Nothing
        Me.UiCtrlGLAccHidden.GLAccountHeadType = "4"
        Me.UiCtrlGLAccHidden.GLAccountID = 0
        Me.UiCtrlGLAccHidden.GLAccountName = Nothing
        Me.UiCtrlGLAccHidden.GLFilterAccount = Nothing
        Me.UiCtrlGLAccHidden.GLFilterCondition = Nothing
        Me.UiCtrlGLAccHidden.Location = New System.Drawing.Point(68, 71)
        Me.UiCtrlGLAccHidden.MinimumSize = New System.Drawing.Size(0, 30)
        Me.UiCtrlGLAccHidden.Name = "UiCtrlGLAccHidden"
        Me.UiCtrlGLAccHidden.Size = New System.Drawing.Size(202, 30)
        Me.UiCtrlGLAccHidden.TabIndex = 2
        Me.UiCtrlGLAccHidden.Tag = Nothing
        Me.UiCtrlGLAccHidden.Visible = False
        '
        'UiCtrlGridBar1
        '
        Me.UiCtrlGridBar1.Location = New System.Drawing.Point(3, 5)
        Me.UiCtrlGridBar1.MyGrid = Me.grdDetailEntry
        Me.UiCtrlGridBar1.Name = "UiCtrlGridBar1"
        Me.UiCtrlGridBar1.Size = New System.Drawing.Size(996, 25)
        Me.UiCtrlGridBar1.TabIndex = 0
        Me.UiCtrlGridBar1.TabStop = False
        '
        'frmGLVoucher
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1028, 742)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.KeyPreview = True
        Me.Name = "frmGLVoucher"
        Me.Text = "GL Voucher"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.grdDetailEntry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdVoucher, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCashBankInfo.ResumeLayout(False)
        Me.grpCashBankInfo.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.grpVoucherDtl.ResumeLayout(False)
        Me.grpVNoDetail.ResumeLayout(False)
        Me.grpVNoDetail.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents UiCtrlGridBar1 As GLNet.uiCtrlGridBar
    Friend WithEvents grpCashBankInfo As System.Windows.Forms.GroupBox
    Friend WithEvents txtAmount As System.Windows.Forms.TextBox
    Friend WithEvents lblAmount As System.Windows.Forms.Label
    Friend WithEvents txtChequeNo As System.Windows.Forms.TextBox
    Friend WithEvents lblChequeNo As System.Windows.Forms.Label
    Friend WithEvents txtVoucherNarration As System.Windows.Forms.TextBox
    Friend WithEvents lblVNarration As System.Windows.Forms.Label
    Friend WithEvents lblBankCashAc As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnFirst As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnPrevious As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents btnLast As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents txtVNo As System.Windows.Forms.TextBox
    Friend WithEvents lblVNo As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblVType As System.Windows.Forms.Label
    Friend WithEvents txtFYearCode As System.Windows.Forms.TextBox
    Friend WithEvents lblFYear As System.Windows.Forms.Label
    Friend WithEvents chkAllowCredit As System.Windows.Forms.CheckBox
    Friend WithEvents dtpDueDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblDueDate As System.Windows.Forms.Label
    Friend WithEvents lblVDate As System.Windows.Forms.Label
    Friend WithEvents dtpVoucherDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents cboVoucherType As System.Windows.Forms.ComboBox
    Friend WithEvents lblChqdate As System.Windows.Forms.Label
    Friend WithEvents dtpChequeDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents lblVoucherString As System.Windows.Forms.TextBox
    Friend WithEvents cmdSearchVoucher As System.Windows.Forms.Button
    Friend WithEvents cmdPostVoucher As System.Windows.Forms.Button
    Friend WithEvents lblPosted As System.Windows.Forms.TextBox
    Friend WithEvents chkOtherVoucher As System.Windows.Forms.CheckBox
    Friend WithEvents lblCompany As System.Windows.Forms.Label
    Friend WithEvents ctrlGLAccounts As GLNet.uiCtrlGLAccount
    Friend WithEvents grdVoucher As Janus.Windows.GridEX.GridEX
    Friend WithEvents lblDrCr As System.Windows.Forms.Label
    Friend WithEvents UiCtrlGLAccHidden As GLNet.uiCtrlGLAccount
    Friend WithEvents grdDetailEntry As Janus.Windows.GridEX.GridEX
    Friend WithEvents btnprint As System.Windows.Forms.Button
    Friend WithEvents lblTotalRecord As System.Windows.Forms.Label
    Friend WithEvents lblScreentext As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents grpVNoDetail As System.Windows.Forms.Panel
    Friend WithEvents grpVoucherDtl As System.Windows.Forms.GroupBox
    Friend WithEvents VoucherToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents btnPrintInvoice As System.Windows.Forms.Button
    Friend WithEvents cboCompany As System.Windows.Forms.ComboBox
    Friend WithEvents lblShop As System.Windows.Forms.Label
    Friend WithEvents cboSource As System.Windows.Forms.ComboBox
    Friend WithEvents BtnCheckPrint As System.Windows.Forms.Button
    Friend WithEvents btnPrintLandScape As System.Windows.Forms.Button
    Friend WithEvents btnReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnNewReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnReportNew As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnPrevCostReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnReport3Inch As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnReportNewWitOutCost As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem4 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem5 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem6 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem7 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem8 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem9 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem10 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem11 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem12 As System.Windows.Forms.ToolStripMenuItem
End Class
