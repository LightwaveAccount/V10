<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBankReconcilation
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
        Dim GridEXLayout1 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBankReconcilation))
        Dim GridEXLayout2 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim GridEXLayout3 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.Panel1 = New System.Windows.Forms.Panel
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.ListBox1 = New System.Windows.Forms.ListBox
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.UiCtrlGridBar2 = New GLNet.uiCtrlGridBar
        Me.grdActivity = New Janus.Windows.GridEX.GridEX
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.dtpChequePaidDate = New System.Windows.Forms.DateTimePicker
        Me.Label3 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.pnlChecks = New System.Windows.Forms.Panel
        Me.chkCredited = New System.Windows.Forms.CheckBox
        Me.chkPresented = New System.Windows.Forms.CheckBox
        Me.chkUnCredited = New System.Windows.Forms.CheckBox
        Me.chkUnPresented = New System.Windows.Forms.CheckBox
        Me.btnDisplay = New System.Windows.Forms.Button
        Me.optActivitySelected = New System.Windows.Forms.RadioButton
        Me.optActivityAll = New System.Windows.Forms.RadioButton
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.dtpActivityToDate = New System.Windows.Forms.DateTimePicker
        Me.dtpActivityFromDate = New System.Windows.Forms.DateTimePicker
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.UiCtrlGridBar1 = New GLNet.uiCtrlGridBar
        Me.GrdView = New Janus.Windows.GridEX.GridEX
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.txtUnCredited = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtBankBalance = New System.Windows.Forms.TextBox
        Me.txtUnPresented = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtLedgerBalance = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.dtpViewToDate = New System.Windows.Forms.DateTimePicker
        Me.dtpViewStartDate = New System.Windows.Forms.DateTimePicker
        Me.optViewUnCredited = New System.Windows.Forms.RadioButton
        Me.optViewUnPresented = New System.Windows.Forms.RadioButton
        Me.optViewAll = New System.Windows.Forms.RadioButton
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.btnDsplyBnkStatmnt = New System.Windows.Forms.Button
        Me.UiCtrlGridBar3 = New GLNet.uiCtrlGridBar
        Me.grdBankStatement = New Janus.Windows.GridEX.GridEX
        Me.GroupBox8 = New System.Windows.Forms.GroupBox
        Me.txtBnkStatmntBlnc = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.GroupBox7 = New System.Windows.Forms.GroupBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.dtpToFrBnkSt = New System.Windows.Forms.DateTimePicker
        Me.dtpFrmForBnkst = New System.Windows.Forms.DateTimePicker
        Me.Label10 = New System.Windows.Forms.Label
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.grdActivity, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.pnlChecks.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.GrdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        CType(Me.grdBankStatement, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox8.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
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
        Me.Panel1.Location = New System.Drawing.Point(0, 612)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1030, 50)
        Me.Panel1.TabIndex = 7
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
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(565, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(48, 48)
        Me.btnCancel.TabIndex = 8
        Me.btnCancel.Tag = "HideText"
        Me.btnCancel.Text = " "
        Me.ToolTip1.SetToolTip(Me.btnCancel, "Delete (Ctrl+D)")
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
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(511, 0)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(48, 48)
        Me.btnDelete.TabIndex = 7
        Me.btnDelete.Tag = "HideText"
        Me.btnDelete.Text = " "
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
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ListBox1)
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.GroupBox1.Location = New System.Drawing.Point(12, 46)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(995, 136)
        Me.GroupBox1.TabIndex = 10
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Bank Accounts"
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(6, 19)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(254, 108)
        Me.ListBox1.TabIndex = 0
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(12, 188)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(995, 418)
        Me.TabControl1.TabIndex = 12
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.UiCtrlGridBar2)
        Me.TabPage1.Controls.Add(Me.GroupBox4)
        Me.TabPage1.Controls.Add(Me.GroupBox2)
        Me.TabPage1.Controls.Add(Me.GroupBox3)
        Me.TabPage1.Controls.Add(Me.grdActivity)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(987, 392)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Activity"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'UiCtrlGridBar2
        '
        Me.UiCtrlGridBar2.Location = New System.Drawing.Point(6, 361)
        Me.UiCtrlGridBar2.MyGrid = Me.grdActivity
        Me.UiCtrlGridBar2.Name = "UiCtrlGridBar2"
        Me.UiCtrlGridBar2.Size = New System.Drawing.Size(975, 25)
        Me.UiCtrlGridBar2.TabIndex = 1
        '
        'grdActivity
        '
        GridEXLayout1.LayoutString = resources.GetString("GridEXLayout1.LayoutString")
        Me.grdActivity.DesignTimeLayout = GridEXLayout1
        Me.grdActivity.EditorsControlStyle.ButtonAppearance = Janus.Windows.GridEX.ButtonAppearance.Regular
        Me.grdActivity.EmptyRows = True
        Me.grdActivity.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdActivity.GroupByBoxVisible = False
        Me.grdActivity.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight
        Me.grdActivity.Location = New System.Drawing.Point(6, 103)
        Me.grdActivity.Name = "grdActivity"
        Me.grdActivity.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdActivity.Size = New System.Drawing.Size(975, 252)
        Me.grdActivity.TabIndex = 12
        Me.grdActivity.TabKeyBehavior = Janus.Windows.GridEX.TabKeyBehavior.ControlNavigation
        Me.grdActivity.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.dtpChequePaidDate)
        Me.GroupBox4.Controls.Add(Me.Label3)
        Me.GroupBox4.Location = New System.Drawing.Point(418, 6)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(186, 47)
        Me.GroupBox4.TabIndex = 0
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Cheque Paid Date"
        '
        'dtpChequePaidDate
        '
        Me.dtpChequePaidDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpChequePaidDate.Location = New System.Drawing.Point(74, 19)
        Me.dtpChequePaidDate.Name = "dtpChequePaidDate"
        Me.dtpChequePaidDate.Size = New System.Drawing.Size(105, 20)
        Me.dtpChequePaidDate.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(20, 23)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(33, 13)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Date:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.pnlChecks)
        Me.GroupBox2.Controls.Add(Me.btnDisplay)
        Me.GroupBox2.Controls.Add(Me.optActivitySelected)
        Me.GroupBox2.Controls.Add(Me.optActivityAll)
        Me.GroupBox2.Location = New System.Drawing.Point(212, 6)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(200, 91)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Filter Criteria"
        '
        'pnlChecks
        '
        Me.pnlChecks.Controls.Add(Me.chkCredited)
        Me.pnlChecks.Controls.Add(Me.chkPresented)
        Me.pnlChecks.Controls.Add(Me.chkUnCredited)
        Me.pnlChecks.Controls.Add(Me.chkUnPresented)
        Me.pnlChecks.Location = New System.Drawing.Point(88, 6)
        Me.pnlChecks.Name = "pnlChecks"
        Me.pnlChecks.Size = New System.Drawing.Size(112, 83)
        Me.pnlChecks.TabIndex = 2
        '
        'chkCredited
        '
        Me.chkCredited.AutoSize = True
        Me.chkCredited.Location = New System.Drawing.Point(3, 63)
        Me.chkCredited.Name = "chkCredited"
        Me.chkCredited.Size = New System.Drawing.Size(65, 17)
        Me.chkCredited.TabIndex = 0
        Me.chkCredited.Text = "Credited"
        Me.chkCredited.UseVisualStyleBackColor = True
        '
        'chkPresented
        '
        Me.chkPresented.AutoSize = True
        Me.chkPresented.Location = New System.Drawing.Point(3, 44)
        Me.chkPresented.Name = "chkPresented"
        Me.chkPresented.Size = New System.Drawing.Size(74, 17)
        Me.chkPresented.TabIndex = 0
        Me.chkPresented.Text = "Presented"
        Me.chkPresented.UseVisualStyleBackColor = True
        '
        'chkUnCredited
        '
        Me.chkUnCredited.AutoSize = True
        Me.chkUnCredited.Location = New System.Drawing.Point(3, 24)
        Me.chkUnCredited.Name = "chkUnCredited"
        Me.chkUnCredited.Size = New System.Drawing.Size(82, 17)
        Me.chkUnCredited.TabIndex = 0
        Me.chkUnCredited.Text = "Un Credited"
        Me.chkUnCredited.UseVisualStyleBackColor = True
        '
        'chkUnPresented
        '
        Me.chkUnPresented.AutoSize = True
        Me.chkUnPresented.Location = New System.Drawing.Point(3, 5)
        Me.chkUnPresented.Name = "chkUnPresented"
        Me.chkUnPresented.Size = New System.Drawing.Size(91, 17)
        Me.chkUnPresented.TabIndex = 0
        Me.chkUnPresented.Text = "Un Presented"
        Me.chkUnPresented.UseVisualStyleBackColor = True
        '
        'btnDisplay
        '
        Me.btnDisplay.Location = New System.Drawing.Point(7, 66)
        Me.btnDisplay.Name = "btnDisplay"
        Me.btnDisplay.Size = New System.Drawing.Size(75, 23)
        Me.btnDisplay.TabIndex = 1
        Me.btnDisplay.Text = "*Display"
        Me.btnDisplay.UseVisualStyleBackColor = True
        '
        'optActivitySelected
        '
        Me.optActivitySelected.AutoSize = True
        Me.optActivitySelected.Location = New System.Drawing.Point(6, 42)
        Me.optActivitySelected.Name = "optActivitySelected"
        Me.optActivitySelected.Size = New System.Drawing.Size(67, 17)
        Me.optActivitySelected.TabIndex = 0
        Me.optActivitySelected.Text = "Selected"
        Me.optActivitySelected.UseVisualStyleBackColor = True
        '
        'optActivityAll
        '
        Me.optActivityAll.AutoSize = True
        Me.optActivityAll.Checked = True
        Me.optActivityAll.Location = New System.Drawing.Point(6, 19)
        Me.optActivityAll.Name = "optActivityAll"
        Me.optActivityAll.Size = New System.Drawing.Size(36, 17)
        Me.optActivityAll.TabIndex = 0
        Me.optActivityAll.TabStop = True
        Me.optActivityAll.Text = "All"
        Me.optActivityAll.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.dtpActivityToDate)
        Me.GroupBox3.Controls.Add(Me.dtpActivityFromDate)
        Me.GroupBox3.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(200, 91)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Voucher Dates"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 49)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "To Date:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "From Date:"
        '
        'dtpActivityToDate
        '
        Me.dtpActivityToDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpActivityToDate.Location = New System.Drawing.Point(89, 45)
        Me.dtpActivityToDate.Name = "dtpActivityToDate"
        Me.dtpActivityToDate.Size = New System.Drawing.Size(105, 20)
        Me.dtpActivityToDate.TabIndex = 0
        '
        'dtpActivityFromDate
        '
        Me.dtpActivityFromDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpActivityFromDate.Location = New System.Drawing.Point(89, 19)
        Me.dtpActivityFromDate.Name = "dtpActivityFromDate"
        Me.dtpActivityFromDate.Size = New System.Drawing.Size(105, 20)
        Me.dtpActivityFromDate.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.UiCtrlGridBar1)
        Me.TabPage2.Controls.Add(Me.GrdView)
        Me.TabPage2.Controls.Add(Me.GroupBox6)
        Me.TabPage2.Controls.Add(Me.GroupBox5)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(987, 392)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "UnPresented/UnCredited View"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'UiCtrlGridBar1
        '
        Me.UiCtrlGridBar1.Location = New System.Drawing.Point(6, 361)
        Me.UiCtrlGridBar1.MyGrid = Me.GrdView
        Me.UiCtrlGridBar1.Name = "UiCtrlGridBar1"
        Me.UiCtrlGridBar1.Size = New System.Drawing.Size(975, 25)
        Me.UiCtrlGridBar1.TabIndex = 1
        '
        'GrdView
        '
        GridEXLayout2.LayoutString = resources.GetString("GridEXLayout2.LayoutString")
        Me.GrdView.DesignTimeLayout = GridEXLayout2
        Me.GrdView.EditorsControlStyle.ButtonAppearance = Janus.Windows.GridEX.ButtonAppearance.Regular
        Me.GrdView.EmptyRows = True
        Me.GrdView.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrdView.GroupByBoxVisible = False
        Me.GrdView.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight
        Me.GrdView.Location = New System.Drawing.Point(6, 103)
        Me.GrdView.Name = "GrdView"
        Me.GrdView.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GrdView.Size = New System.Drawing.Size(975, 252)
        Me.GrdView.TabIndex = 15
        Me.GrdView.TabKeyBehavior = Janus.Windows.GridEX.TabKeyBehavior.ControlNavigation
        Me.GrdView.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.txtUnCredited)
        Me.GroupBox6.Controls.Add(Me.Label9)
        Me.GroupBox6.Controls.Add(Me.txtBankBalance)
        Me.GroupBox6.Controls.Add(Me.txtUnPresented)
        Me.GroupBox6.Controls.Add(Me.Label7)
        Me.GroupBox6.Controls.Add(Me.Label8)
        Me.GroupBox6.Controls.Add(Me.txtLedgerBalance)
        Me.GroupBox6.Controls.Add(Me.Label6)
        Me.GroupBox6.Location = New System.Drawing.Point(313, 6)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(408, 91)
        Me.GroupBox6.TabIndex = 14
        Me.GroupBox6.TabStop = False
        '
        'txtUnCredited
        '
        Me.txtUnCredited.Location = New System.Drawing.Point(297, 45)
        Me.txtUnCredited.Name = "txtUnCredited"
        Me.txtUnCredited.ReadOnly = True
        Me.txtUnCredited.Size = New System.Drawing.Size(100, 20)
        Me.txtUnCredited.TabIndex = 0
        Me.txtUnCredited.Text = "0"
        Me.txtUnCredited.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(206, 49)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(66, 13)
        Me.Label9.TabIndex = 5
        Me.Label9.Text = "Un Credited:"
        '
        'txtBankBalance
        '
        Me.txtBankBalance.Location = New System.Drawing.Point(100, 45)
        Me.txtBankBalance.Name = "txtBankBalance"
        Me.txtBankBalance.ReadOnly = True
        Me.txtBankBalance.Size = New System.Drawing.Size(100, 20)
        Me.txtBankBalance.TabIndex = 0
        Me.txtBankBalance.Text = "0"
        Me.txtBankBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtUnPresented
        '
        Me.txtUnPresented.Location = New System.Drawing.Point(297, 19)
        Me.txtUnPresented.Name = "txtUnPresented"
        Me.txtUnPresented.ReadOnly = True
        Me.txtUnPresented.Size = New System.Drawing.Size(100, 20)
        Me.txtUnPresented.TabIndex = 0
        Me.txtUnPresented.Text = "0"
        Me.txtUnPresented.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(9, 49)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(77, 13)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "Bank Balance:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(206, 23)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(75, 13)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "Un Presented:"
        '
        'txtLedgerBalance
        '
        Me.txtLedgerBalance.Location = New System.Drawing.Point(100, 19)
        Me.txtLedgerBalance.Name = "txtLedgerBalance"
        Me.txtLedgerBalance.ReadOnly = True
        Me.txtLedgerBalance.Size = New System.Drawing.Size(100, 20)
        Me.txtLedgerBalance.TabIndex = 0
        Me.txtLedgerBalance.Text = "0"
        Me.txtLedgerBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(9, 23)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(85, 13)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Ledger Balance:"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.Label4)
        Me.GroupBox5.Controls.Add(Me.Label5)
        Me.GroupBox5.Controls.Add(Me.dtpViewToDate)
        Me.GroupBox5.Controls.Add(Me.dtpViewStartDate)
        Me.GroupBox5.Controls.Add(Me.optViewUnCredited)
        Me.GroupBox5.Controls.Add(Me.optViewUnPresented)
        Me.GroupBox5.Controls.Add(Me.optViewAll)
        Me.GroupBox5.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(301, 91)
        Me.GroupBox5.TabIndex = 14
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Filter Criteria"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(7, 49)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(49, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "To Date:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 23)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(59, 13)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "From Date:"
        '
        'dtpViewToDate
        '
        Me.dtpViewToDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpViewToDate.Location = New System.Drawing.Point(89, 45)
        Me.dtpViewToDate.Name = "dtpViewToDate"
        Me.dtpViewToDate.Size = New System.Drawing.Size(105, 20)
        Me.dtpViewToDate.TabIndex = 2
        '
        'dtpViewStartDate
        '
        Me.dtpViewStartDate.Enabled = False
        Me.dtpViewStartDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpViewStartDate.Location = New System.Drawing.Point(89, 19)
        Me.dtpViewStartDate.Name = "dtpViewStartDate"
        Me.dtpViewStartDate.Size = New System.Drawing.Size(105, 20)
        Me.dtpViewStartDate.TabIndex = 3
        '
        'optViewUnCredited
        '
        Me.optViewUnCredited.AutoSize = True
        Me.optViewUnCredited.Location = New System.Drawing.Point(200, 59)
        Me.optViewUnCredited.Name = "optViewUnCredited"
        Me.optViewUnCredited.Size = New System.Drawing.Size(81, 17)
        Me.optViewUnCredited.TabIndex = 0
        Me.optViewUnCredited.Text = "Un Credited"
        Me.optViewUnCredited.UseVisualStyleBackColor = True
        '
        'optViewUnPresented
        '
        Me.optViewUnPresented.AutoSize = True
        Me.optViewUnPresented.Checked = True
        Me.optViewUnPresented.Location = New System.Drawing.Point(200, 36)
        Me.optViewUnPresented.Name = "optViewUnPresented"
        Me.optViewUnPresented.Size = New System.Drawing.Size(90, 17)
        Me.optViewUnPresented.TabIndex = 0
        Me.optViewUnPresented.TabStop = True
        Me.optViewUnPresented.Text = "Un Presented"
        Me.optViewUnPresented.UseVisualStyleBackColor = True
        '
        'optViewAll
        '
        Me.optViewAll.AutoSize = True
        Me.optViewAll.Location = New System.Drawing.Point(200, 13)
        Me.optViewAll.Name = "optViewAll"
        Me.optViewAll.Size = New System.Drawing.Size(36, 17)
        Me.optViewAll.TabIndex = 0
        Me.optViewAll.Text = "All"
        Me.optViewAll.UseVisualStyleBackColor = True
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.btnDsplyBnkStatmnt)
        Me.TabPage3.Controls.Add(Me.UiCtrlGridBar3)
        Me.TabPage3.Controls.Add(Me.GroupBox8)
        Me.TabPage3.Controls.Add(Me.grdBankStatement)
        Me.TabPage3.Controls.Add(Me.GroupBox7)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(987, 392)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Bank Statement"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'btnDsplyBnkStatmnt
        '
        Me.btnDsplyBnkStatmnt.Location = New System.Drawing.Point(211, 58)
        Me.btnDsplyBnkStatmnt.Name = "btnDsplyBnkStatmnt"
        Me.btnDsplyBnkStatmnt.Size = New System.Drawing.Size(75, 23)
        Me.btnDsplyBnkStatmnt.TabIndex = 17
        Me.btnDsplyBnkStatmnt.Text = "*Display"
        Me.btnDsplyBnkStatmnt.UseVisualStyleBackColor = True
        '
        'UiCtrlGridBar3
        '
        Me.UiCtrlGridBar3.Location = New System.Drawing.Point(6, 361)
        Me.UiCtrlGridBar3.MyGrid = Me.grdBankStatement
        Me.UiCtrlGridBar3.Name = "UiCtrlGridBar3"
        Me.UiCtrlGridBar3.Size = New System.Drawing.Size(975, 25)
        Me.UiCtrlGridBar3.TabIndex = 16
        '
        'grdBankStatement
        '
        GridEXLayout3.LayoutString = resources.GetString("GridEXLayout3.LayoutString")
        Me.grdBankStatement.DesignTimeLayout = GridEXLayout3
        Me.grdBankStatement.EditorsControlStyle.ButtonAppearance = Janus.Windows.GridEX.ButtonAppearance.Regular
        Me.grdBankStatement.EmptyRows = True
        Me.grdBankStatement.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdBankStatement.GroupByBoxVisible = False
        Me.grdBankStatement.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight
        Me.grdBankStatement.Location = New System.Drawing.Point(6, 90)
        Me.grdBankStatement.Name = "grdBankStatement"
        Me.grdBankStatement.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdBankStatement.Size = New System.Drawing.Size(975, 264)
        Me.grdBankStatement.TabIndex = 13
        Me.grdBankStatement.TabKeyBehavior = Janus.Windows.GridEX.TabKeyBehavior.ControlNavigation
        Me.grdBankStatement.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.txtBnkStatmntBlnc)
        Me.GroupBox8.Controls.Add(Me.Label14)
        Me.GroupBox8.Location = New System.Drawing.Point(211, 6)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(206, 47)
        Me.GroupBox8.TabIndex = 15
        Me.GroupBox8.TabStop = False
        '
        'txtBnkStatmntBlnc
        '
        Me.txtBnkStatmntBlnc.Location = New System.Drawing.Point(100, 18)
        Me.txtBnkStatmntBlnc.Name = "txtBnkStatmntBlnc"
        Me.txtBnkStatmntBlnc.ReadOnly = True
        Me.txtBnkStatmntBlnc.Size = New System.Drawing.Size(100, 20)
        Me.txtBnkStatmntBlnc.TabIndex = 0
        Me.txtBnkStatmntBlnc.Text = "0"
        Me.txtBnkStatmntBlnc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(9, 22)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(77, 13)
        Me.Label14.TabIndex = 5
        Me.Label14.Text = "Bank Balance:"
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.Label11)
        Me.GroupBox7.Controls.Add(Me.Label12)
        Me.GroupBox7.Controls.Add(Me.dtpToFrBnkSt)
        Me.GroupBox7.Controls.Add(Me.dtpFrmForBnkst)
        Me.GroupBox7.Location = New System.Drawing.Point(5, 6)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(200, 75)
        Me.GroupBox7.TabIndex = 1
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Paid Dates"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(7, 49)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(49, 13)
        Me.Label11.TabIndex = 1
        Me.Label11.Text = "To Date:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(6, 23)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(59, 13)
        Me.Label12.TabIndex = 1
        Me.Label12.Text = "From Date:"
        '
        'dtpToFrBnkSt
        '
        Me.dtpToFrBnkSt.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpToFrBnkSt.Location = New System.Drawing.Point(89, 45)
        Me.dtpToFrBnkSt.Name = "dtpToFrBnkSt"
        Me.dtpToFrBnkSt.Size = New System.Drawing.Size(105, 20)
        Me.dtpToFrBnkSt.TabIndex = 0
        '
        'dtpFrmForBnkst
        '
        Me.dtpFrmForBnkst.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFrmForBnkst.Location = New System.Drawing.Point(89, 19)
        Me.dtpFrmForBnkst.Name = "dtpFrmForBnkst"
        Me.dtpFrmForBnkst.Size = New System.Drawing.Size(105, 20)
        Me.dtpFrmForBnkst.TabIndex = 0
        '
        'Label10
        '
        Me.Label10.AccessibleDescription = "Title"
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold)
        Me.Label10.ForeColor = System.Drawing.SystemColors.Desktop
        Me.Label10.Location = New System.Drawing.Point(12, 9)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(194, 24)
        Me.Label10.TabIndex = 18
        Me.Label10.Text = "Bank Reconciliation"
        '
        'frmBankReconcilation
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1028, 744)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel1)
        Me.KeyPreview = True
        Me.Name = "frmBankReconcilation"
        Me.Text = "Bank Reconciliation"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.grdActivity, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.pnlChecks.ResumeLayout(False)
        Me.pnlChecks.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.GrdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        CType(Me.grdBankStatement, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
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
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents grdActivity As Janus.Windows.GridEX.GridEX
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents dtpActivityToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpActivityFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents pnlChecks As System.Windows.Forms.Panel
    Friend WithEvents btnDisplay As System.Windows.Forms.Button
    Friend WithEvents optActivitySelected As System.Windows.Forms.RadioButton
    Friend WithEvents optActivityAll As System.Windows.Forms.RadioButton
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chkCredited As System.Windows.Forms.CheckBox
    Friend WithEvents chkPresented As System.Windows.Forms.CheckBox
    Friend WithEvents chkUnCredited As System.Windows.Forms.CheckBox
    Friend WithEvents chkUnPresented As System.Windows.Forms.CheckBox
    Friend WithEvents dtpChequePaidDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents dtpViewToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpViewStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents optViewUnPresented As System.Windows.Forms.RadioButton
    Friend WithEvents optViewAll As System.Windows.Forms.RadioButton
    Friend WithEvents optViewUnCredited As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents txtLedgerBalance As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtUnCredited As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtBankBalance As System.Windows.Forms.TextBox
    Friend WithEvents txtUnPresented As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents GrdView As Janus.Windows.GridEX.GridEX
    Friend WithEvents UiCtrlGridBar2 As GLNet.uiCtrlGridBar
    Friend WithEvents UiCtrlGridBar1 As GLNet.uiCtrlGridBar
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents UiCtrlGridBar3 As GLNet.uiCtrlGridBar
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents txtBnkStatmntBlnc As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents grdBankStatement As Janus.Windows.GridEX.GridEX
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents dtpToFrBnkSt As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFrmForBnkst As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnDsplyBnkStatmnt As System.Windows.Forms.Button
End Class
