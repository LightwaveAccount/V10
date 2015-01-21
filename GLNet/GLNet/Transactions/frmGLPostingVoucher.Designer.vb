<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGLPostingVoucher
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
        Me.grdDtlRecords = New Janus.Windows.GridEX.GridEX
        Me.btnDelete = New System.Windows.Forms.Button
        Me.btnNext = New System.Windows.Forms.Button
        Me.btnUpdate = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnFirst = New System.Windows.Forms.Button
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnPrevious = New System.Windows.Forms.Button
        Me.btnLast = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnNew = New System.Windows.Forms.Button
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.grdMainAccounts = New Janus.Windows.GridEX.GridEX
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Button2 = New System.Windows.Forms.Button
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.txtRemarks = New System.Windows.Forms.TextBox
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.txtInvAmount = New System.Windows.Forms.TextBox
        Me.txtChequeNo = New System.Windows.Forms.TextBox
        Me.lblinvamt = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel
        Me.btnPost = New System.Windows.Forms.Button
        Me.btnUnpost = New System.Windows.Forms.Button
        Me.btnSearch = New System.Windows.Forms.Button
        Me.optSearchDateWise = New System.Windows.Forms.RadioButton
        Me.optSearchAll = New System.Windows.Forms.RadioButton
        Me.optSearchVoucherWise = New System.Windows.Forms.RadioButton
        Me.lblHeading = New System.Windows.Forms.Label
        Me.Button1 = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.optOther = New System.Windows.Forms.RadioButton
        Me.optUnBalanced = New System.Windows.Forms.RadioButton
        Me.optUnPosted = New System.Windows.Forms.RadioButton
        Me.optPosted = New System.Windows.Forms.RadioButton
        Me.optAll = New System.Windows.Forms.RadioButton
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.dtVoucherEndDate = New System.Windows.Forms.DateTimePicker
        Me.dtVoucherStartDate = New System.Windows.Forms.DateTimePicker
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtVoucherNoTo = New System.Windows.Forms.TextBox
        Me.txtVoucherNoFrom = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.ChkSelectAll = New System.Windows.Forms.CheckBox
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.txtFinancialYear = New System.Windows.Forms.TextBox
        Me.cboCompany = New System.Windows.Forms.ComboBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.cmbVoucherType = New System.Windows.Forms.ComboBox
        Me.cmbVoucherMonth = New System.Windows.Forms.ComboBox
        Me.cmbSource = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.lblPhone = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.UiCtrlGridBar1 = New GLNet.uiCtrlGridBar
        CType(Me.grdDtlRecords, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.grdMainAccounts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'grdDtlRecords
        '
        Me.grdDtlRecords.Location = New System.Drawing.Point(0, 0)
        Me.grdDtlRecords.Name = "grdDtlRecords"
        Me.grdDtlRecords.Size = New System.Drawing.Size(400, 376)
        Me.grdDtlRecords.TabIndex = 0
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(511, 0)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(48, 48)
        Me.btnDelete.TabIndex = 7
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
        Me.btnNext.Tag = "HideText"
        Me.btnNext.Text = " "
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
        Me.ToolTip1.SetToolTip(Me.btnUpdate, "Update (Ctrl+U)")
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(965, 0)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(48, 48)
        Me.btnExit.TabIndex = 9
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
        Me.btnFirst.Tag = "HideText"
        Me.btnFirst.Text = " "
        Me.btnFirst.UseVisualStyleBackColor = True
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
        Me.Panel2.Size = New System.Drawing.Size(1051, 50)
        Me.Panel2.TabIndex = 7
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(565, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(48, 48)
        Me.btnCancel.TabIndex = 8
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
        Me.btnPrevious.Tag = "HideText"
        Me.btnPrevious.Text = " "
        Me.btnPrevious.UseVisualStyleBackColor = True
        '
        'btnLast
        '
        Me.btnLast.Location = New System.Drawing.Point(174, 0)
        Me.btnLast.Name = "btnLast"
        Me.btnLast.Size = New System.Drawing.Size(48, 48)
        Me.btnLast.TabIndex = 3
        Me.btnLast.Tag = "HideText"
        Me.btnLast.Text = " "
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
        Me.ToolTip1.SetToolTip(Me.btnSave, "Save (Ctrl+S)")
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
        Me.ToolTip1.SetToolTip(Me.btnNew, "New (Ctrl+N)")
        Me.btnNew.UseVisualStyleBackColor = True
        '
        'Panel4
        '
        Me.Panel4.Location = New System.Drawing.Point(2, 119)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(974, 447)
        Me.Panel4.TabIndex = 5
        '
        'grdMainAccounts
        '
        Me.grdMainAccounts.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdMainAccounts.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight
        Me.grdMainAccounts.Location = New System.Drawing.Point(14, 38)
        Me.grdMainAccounts.Name = "grdMainAccounts"
        Me.grdMainAccounts.Size = New System.Drawing.Size(942, 278)
        Me.grdMainAccounts.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Button2)
        Me.Panel1.Controls.Add(Me.GroupBox5)
        Me.Panel1.Controls.Add(Me.GroupBox4)
        Me.Panel1.Controls.Add(Me.FlowLayoutPanel1)
        Me.Panel1.Controls.Add(Me.btnSearch)
        Me.Panel1.Controls.Add(Me.optSearchDateWise)
        Me.Panel1.Controls.Add(Me.optSearchAll)
        Me.Panel1.Controls.Add(Me.optSearchVoucherWise)
        Me.Panel1.Controls.Add(Me.lblHeading)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.GroupBox3)
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.Panel5)
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Location = New System.Drawing.Point(12, 11)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1000, 593)
        Me.Panel1.TabIndex = 6
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(674, 229)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(74, 23)
        Me.Button2.TabIndex = 34
        Me.Button2.Text = "Reset"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.txtRemarks)
        Me.GroupBox5.Location = New System.Drawing.Point(14, 204)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(545, 53)
        Me.GroupBox5.TabIndex = 33
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Voucher Remarks/Candela Invoice No."
        '
        'txtRemarks
        '
        Me.txtRemarks.Location = New System.Drawing.Point(19, 15)
        Me.txtRemarks.MaxLength = 1000
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtRemarks.Size = New System.Drawing.Size(520, 33)
        Me.txtRemarks.TabIndex = 20
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.txtInvAmount)
        Me.GroupBox4.Controls.Add(Me.txtChequeNo)
        Me.GroupBox4.Controls.Add(Me.lblinvamt)
        Me.GroupBox4.Controls.Add(Me.Label10)
        Me.GroupBox4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.Location = New System.Drawing.Point(473, 120)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(235, 81)
        Me.GroupBox4.TabIndex = 32
        Me.GroupBox4.TabStop = False
        '
        'txtInvAmount
        '
        Me.txtInvAmount.Location = New System.Drawing.Point(100, 51)
        Me.txtInvAmount.MaxLength = 10
        Me.txtInvAmount.Name = "txtInvAmount"
        Me.txtInvAmount.Size = New System.Drawing.Size(132, 21)
        Me.txtInvAmount.TabIndex = 22
        '
        'txtChequeNo
        '
        Me.txtChequeNo.Location = New System.Drawing.Point(100, 24)
        Me.txtChequeNo.MaxLength = 12
        Me.txtChequeNo.Name = "txtChequeNo"
        Me.txtChequeNo.Size = New System.Drawing.Size(132, 21)
        Me.txtChequeNo.TabIndex = 21
        '
        'lblinvamt
        '
        Me.lblinvamt.AutoSize = True
        Me.lblinvamt.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblinvamt.Location = New System.Drawing.Point(6, 55)
        Me.lblinvamt.Name = "lblinvamt"
        Me.lblinvamt.Size = New System.Drawing.Size(87, 13)
        Me.lblinvamt.TabIndex = 18
        Me.lblinvamt.Text = "Inv. Amount"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(6, 28)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(80, 13)
        Me.Label10.TabIndex = 17
        Me.Label10.Text = "Cheque No."
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Controls.Add(Me.btnPost)
        Me.FlowLayoutPanel1.Controls.Add(Me.btnUnpost)
        Me.FlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(498, 225)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(173, 31)
        Me.FlowLayoutPanel1.TabIndex = 31
        '
        'btnPost
        '
        Me.btnPost.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(90, 3)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(80, 23)
        Me.btnPost.TabIndex = 29
        Me.btnPost.Text = "Post"
        Me.btnPost.UseVisualStyleBackColor = True
        '
        'btnUnpost
        '
        Me.btnUnpost.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUnpost.Location = New System.Drawing.Point(4, 3)
        Me.btnUnpost.Name = "btnUnpost"
        Me.btnUnpost.Size = New System.Drawing.Size(80, 23)
        Me.btnUnpost.TabIndex = 30
        Me.btnUnpost.Text = "UnPost"
        Me.btnUnpost.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        Me.btnSearch.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearch.Location = New System.Drawing.Point(753, 229)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(74, 23)
        Me.btnSearch.TabIndex = 28
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'optSearchDateWise
        '
        Me.optSearchDateWise.AutoSize = True
        Me.optSearchDateWise.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optSearchDateWise.Location = New System.Drawing.Point(175, 103)
        Me.optSearchDateWise.Name = "optSearchDateWise"
        Me.optSearchDateWise.Size = New System.Drawing.Size(111, 16)
        Me.optSearchDateWise.TabIndex = 27
        Me.optSearchDateWise.Text = "Search Date Wise"
        Me.optSearchDateWise.UseVisualStyleBackColor = True
        '
        'optSearchAll
        '
        Me.optSearchAll.AutoSize = True
        Me.optSearchAll.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optSearchAll.Location = New System.Drawing.Point(300, 103)
        Me.optSearchAll.Name = "optSearchAll"
        Me.optSearchAll.Size = New System.Drawing.Size(73, 16)
        Me.optSearchAll.TabIndex = 26
        Me.optSearchAll.Text = "Search All"
        Me.optSearchAll.UseVisualStyleBackColor = True
        '
        'optSearchVoucherWise
        '
        Me.optSearchVoucherWise.AutoSize = True
        Me.optSearchVoucherWise.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optSearchVoucherWise.Location = New System.Drawing.Point(21, 103)
        Me.optSearchVoucherWise.Name = "optSearchVoucherWise"
        Me.optSearchVoucherWise.Size = New System.Drawing.Size(141, 16)
        Me.optSearchVoucherWise.TabIndex = 24
        Me.optSearchVoucherWise.Text = "Search Voucher  # Wise"
        Me.optSearchVoucherWise.UseVisualStyleBackColor = True
        '
        'lblHeading
        '
        Me.lblHeading.AccessibleDescription = "Title"
        Me.lblHeading.AutoSize = True
        Me.lblHeading.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold)
        Me.lblHeading.ForeColor = System.Drawing.SystemColors.Desktop
        Me.lblHeading.Location = New System.Drawing.Point(17, 4)
        Me.lblHeading.Name = "lblHeading"
        Me.lblHeading.Size = New System.Drawing.Size(309, 24)
        Me.lblHeading.TabIndex = 15
        Me.lblHeading.Text = "Searching/ Posting Of Vouchers"
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(833, 229)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(156, 23)
        Me.Button1.TabIndex = 23
        Me.Button1.Text = "Load Selected Voucher"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.optOther)
        Me.GroupBox3.Controls.Add(Me.optUnBalanced)
        Me.GroupBox3.Controls.Add(Me.optUnPosted)
        Me.GroupBox3.Controls.Add(Me.optPosted)
        Me.GroupBox3.Controls.Add(Me.optAll)
        Me.GroupBox3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(712, 120)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(283, 81)
        Me.GroupBox3.TabIndex = 22
        Me.GroupBox3.TabStop = False
        '
        'optOther
        '
        Me.optOther.AutoSize = True
        Me.optOther.Location = New System.Drawing.Point(113, 51)
        Me.optOther.Name = "optOther"
        Me.optOther.Size = New System.Drawing.Size(61, 17)
        Me.optOther.TabIndex = 4
        Me.optOther.Text = "Other"
        Me.optOther.UseVisualStyleBackColor = True
        '
        'optUnBalanced
        '
        Me.optUnBalanced.AutoSize = True
        Me.optUnBalanced.Location = New System.Drawing.Point(11, 52)
        Me.optUnBalanced.Name = "optUnBalanced"
        Me.optUnBalanced.Size = New System.Drawing.Size(101, 17)
        Me.optUnBalanced.TabIndex = 3
        Me.optUnBalanced.Text = "UnBalanced"
        Me.optUnBalanced.UseVisualStyleBackColor = True
        '
        'optUnPosted
        '
        Me.optUnPosted.AutoSize = True
        Me.optUnPosted.Location = New System.Drawing.Point(194, 28)
        Me.optUnPosted.Name = "optUnPosted"
        Me.optUnPosted.Size = New System.Drawing.Size(86, 17)
        Me.optUnPosted.TabIndex = 2
        Me.optUnPosted.Text = "UnPosted"
        Me.optUnPosted.UseVisualStyleBackColor = True
        '
        'optPosted
        '
        Me.optPosted.AutoSize = True
        Me.optPosted.Location = New System.Drawing.Point(113, 28)
        Me.optPosted.Name = "optPosted"
        Me.optPosted.Size = New System.Drawing.Size(69, 17)
        Me.optPosted.TabIndex = 1
        Me.optPosted.Text = "Posted"
        Me.optPosted.UseVisualStyleBackColor = True
        '
        'optAll
        '
        Me.optAll.AutoSize = True
        Me.optAll.Checked = True
        Me.optAll.Location = New System.Drawing.Point(11, 28)
        Me.optAll.Name = "optAll"
        Me.optAll.Size = New System.Drawing.Size(42, 17)
        Me.optAll.TabIndex = 0
        Me.optAll.TabStop = True
        Me.optAll.Text = "All"
        Me.optAll.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.dtVoucherEndDate)
        Me.GroupBox2.Controls.Add(Me.dtVoucherStartDate)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(253, 119)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(214, 81)
        Me.GroupBox2.TabIndex = 21
        Me.GroupBox2.TabStop = False
        '
        'dtVoucherEndDate
        '
        Me.dtVoucherEndDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtVoucherEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtVoucherEndDate.Location = New System.Drawing.Point(82, 54)
        Me.dtVoucherEndDate.Name = "dtVoucherEndDate"
        Me.dtVoucherEndDate.Size = New System.Drawing.Size(117, 21)
        Me.dtVoucherEndDate.TabIndex = 20
        '
        'dtVoucherStartDate
        '
        Me.dtVoucherStartDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtVoucherStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtVoucherStartDate.Location = New System.Drawing.Point(82, 27)
        Me.dtVoucherStartDate.Name = "dtVoucherStartDate"
        Me.dtVoucherStartDate.Size = New System.Drawing.Size(117, 21)
        Me.dtVoucherStartDate.TabIndex = 19
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(6, 55)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(65, 13)
        Me.Label6.TabIndex = 18
        Me.Label6.Text = "End Date"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(6, 28)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(73, 13)
        Me.Label7.TabIndex = 17
        Me.Label7.Text = "Start Date"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtVoucherNoTo)
        Me.GroupBox1.Controls.Add(Me.txtVoucherNoFrom)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(14, 119)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(232, 81)
        Me.GroupBox1.TabIndex = 18
        Me.GroupBox1.TabStop = False
        '
        'txtVoucherNoTo
        '
        Me.txtVoucherNoTo.Location = New System.Drawing.Point(118, 54)
        Me.txtVoucherNoTo.MaxLength = 6
        Me.txtVoucherNoTo.Name = "txtVoucherNoTo"
        Me.txtVoucherNoTo.Size = New System.Drawing.Size(108, 21)
        Me.txtVoucherNoTo.TabIndex = 20
        '
        'txtVoucherNoFrom
        '
        Me.txtVoucherNoFrom.Location = New System.Drawing.Point(118, 27)
        Me.txtVoucherNoFrom.MaxLength = 6
        Me.txtVoucherNoFrom.Name = "txtVoucherNoFrom"
        Me.txtVoucherNoFrom.Size = New System.Drawing.Size(108, 21)
        Me.txtVoucherNoFrom.TabIndex = 19
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(5, 55)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(93, 13)
        Me.Label5.TabIndex = 18
        Me.Label5.Text = "Voucher # To"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(5, 28)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(115, 13)
        Me.Label4.TabIndex = 17
        Me.Label4.Text = "Voucher # From "
        '
        'Panel5
        '
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Controls.Add(Me.ChkSelectAll)
        Me.Panel5.Controls.Add(Me.UiCtrlGridBar1)
        Me.Panel5.Controls.Add(Me.grdMainAccounts)
        Me.Panel5.Location = New System.Drawing.Point(14, 262)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(973, 323)
        Me.Panel5.TabIndex = 5
        '
        'ChkSelectAll
        '
        Me.ChkSelectAll.Location = New System.Drawing.Point(16, 12)
        Me.ChkSelectAll.Name = "ChkSelectAll"
        Me.ChkSelectAll.Size = New System.Drawing.Size(110, 17)
        Me.ChkSelectAll.TabIndex = 8
        Me.ChkSelectAll.Text = "Select All"
        Me.ChkSelectAll.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.txtFinancialYear)
        Me.Panel3.Controls.Add(Me.cboCompany)
        Me.Panel3.Controls.Add(Me.Label8)
        Me.Panel3.Controls.Add(Me.cmbVoucherType)
        Me.Panel3.Controls.Add(Me.cmbVoucherMonth)
        Me.Panel3.Controls.Add(Me.cmbSource)
        Me.Panel3.Controls.Add(Me.Label3)
        Me.Panel3.Controls.Add(Me.lblPhone)
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Controls.Add(Me.Panel4)
        Me.Panel3.Location = New System.Drawing.Point(14, 34)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(973, 63)
        Me.Panel3.TabIndex = 4
        '
        'txtFinancialYear
        '
        Me.txtFinancialYear.Location = New System.Drawing.Point(527, 5)
        Me.txtFinancialYear.MaxLength = 250
        Me.txtFinancialYear.Name = "txtFinancialYear"
        Me.txtFinancialYear.ReadOnly = True
        Me.txtFinancialYear.Size = New System.Drawing.Size(420, 20)
        Me.txtFinancialYear.TabIndex = 14
        '
        'cboCompany
        '
        Me.cboCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCompany.FormattingEnabled = True
        Me.cboCompany.Items.AddRange(New Object() {"--Select Any Value--", "Assets", "Liability", "Capital", "Income", "Expense"})
        Me.cboCompany.Location = New System.Drawing.Point(79, 6)
        Me.cboCompany.Name = "cboCompany"
        Me.cboCompany.Size = New System.Drawing.Size(255, 21)
        Me.cboCompany.TabIndex = 15
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(6, 10)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(67, 13)
        Me.Label8.TabIndex = 14
        Me.Label8.Text = "Company"
        '
        'cmbVoucherType
        '
        Me.cmbVoucherType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbVoucherType.FormattingEnabled = True
        Me.cmbVoucherType.Items.AddRange(New Object() {"--Select Any Value--", "Assets", "Liability", "Capital", "Income", "Expense"})
        Me.cmbVoucherType.Location = New System.Drawing.Point(527, 34)
        Me.cmbVoucherType.Name = "cmbVoucherType"
        Me.cmbVoucherType.Size = New System.Drawing.Size(152, 21)
        Me.cmbVoucherType.TabIndex = 13
        '
        'cmbVoucherMonth
        '
        Me.cmbVoucherMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbVoucherMonth.FormattingEnabled = True
        Me.cmbVoucherMonth.Items.AddRange(New Object() {"---Select---", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"})
        Me.cmbVoucherMonth.Location = New System.Drawing.Point(808, 35)
        Me.cmbVoucherMonth.MaxDropDownItems = 13
        Me.cmbVoucherMonth.Name = "cmbVoucherMonth"
        Me.cmbVoucherMonth.Size = New System.Drawing.Size(139, 21)
        Me.cmbVoucherMonth.TabIndex = 13
        '
        'cmbSource
        '
        Me.cmbSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSource.FormattingEnabled = True
        Me.cmbSource.Items.AddRange(New Object() {"--Select Any Value--", "Assets", "Liability", "Capital", "Income", "Expense"})
        Me.cmbSource.Location = New System.Drawing.Point(79, 33)
        Me.cmbSource.Name = "cmbSource"
        Me.cmbSource.Size = New System.Drawing.Size(255, 21)
        Me.cmbSource.TabIndex = 12
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(749, 39)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(46, 13)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Month"
        '
        'lblPhone
        '
        Me.lblPhone.AutoSize = True
        Me.lblPhone.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPhone.Location = New System.Drawing.Point(355, 8)
        Me.lblPhone.Name = "lblPhone"
        Me.lblPhone.Size = New System.Drawing.Size(167, 13)
        Me.lblPhone.TabIndex = 8
        Me.lblPhone.Text = "Financial Year/ Location"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(354, 36)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Voucher Type"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 37)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Source"
        '
        'UiCtrlGridBar1
        '
        Me.UiCtrlGridBar1.Location = New System.Drawing.Point(132, 8)
        Me.UiCtrlGridBar1.MyGrid = Me.grdMainAccounts
        Me.UiCtrlGridBar1.Name = "UiCtrlGridBar1"
        Me.UiCtrlGridBar1.Size = New System.Drawing.Size(824, 25)
        Me.UiCtrlGridBar1.TabIndex = 4
        Me.UiCtrlGridBar1.TabStop = False
        '
        'frmGLPostingVoucher
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1028, 744)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.KeyPreview = True
        Me.Name = "frmGLPostingVoucher"
        Me.Text = "Searching/ Posting of Vouchers"
        CType(Me.grdDtlRecords, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        CType(Me.grdMainAccounts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grdDtlRecords As Janus.Windows.GridEX.GridEX
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnFirst As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnPrevious As System.Windows.Forms.Button
    Friend WithEvents btnLast As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents UiCtrlGridBar1 As GLNet.uiCtrlGridBar
    Friend WithEvents grdMainAccounts As Janus.Windows.GridEX.GridEX
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblPhone As System.Windows.Forms.Label
    Friend WithEvents cmbSource As System.Windows.Forms.ComboBox
    Friend WithEvents cmbVoucherType As System.Windows.Forms.ComboBox
    Friend WithEvents cmbVoucherMonth As System.Windows.Forms.ComboBox
    Friend WithEvents txtFinancialYear As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtVoucherNoTo As System.Windows.Forms.TextBox
    Friend WithEvents txtVoucherNoFrom As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents dtVoucherEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtVoucherStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents optUnPosted As System.Windows.Forms.RadioButton
    Friend WithEvents optPosted As System.Windows.Forms.RadioButton
    Friend WithEvents optAll As System.Windows.Forms.RadioButton
    Friend WithEvents optOther As System.Windows.Forms.RadioButton
    Friend WithEvents optUnBalanced As System.Windows.Forms.RadioButton
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents lblHeading As System.Windows.Forms.Label
    Friend WithEvents optSearchDateWise As System.Windows.Forms.RadioButton
    Friend WithEvents optSearchAll As System.Windows.Forms.RadioButton
    Friend WithEvents optSearchVoucherWise As System.Windows.Forms.RadioButton
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents ChkSelectAll As System.Windows.Forms.CheckBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents btnUnpost As System.Windows.Forms.Button
    Friend WithEvents FlowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents btnPost As System.Windows.Forms.Button
    Friend WithEvents cboCompany As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents lblinvamt As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtInvAmount As System.Windows.Forms.TextBox
    Friend WithEvents txtChequeNo As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
End Class
