<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSchemaSnapshot
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
        Me.chkGLSQLLog = New System.Windows.Forms.CheckBox
        Me.Label28 = New System.Windows.Forms.Label
        Me.dtpGLLogDateTo = New System.Windows.Forms.DateTimePicker
        Me.dtpGLLogFrom = New System.Windows.Forms.DateTimePicker
        Me.Label25 = New System.Windows.Forms.Label
        Me.Label26 = New System.Windows.Forms.Label
        Me.dtpVoucherDateTo = New System.Windows.Forms.DateTimePicker
        Me.dtpVoucherDateFrom = New System.Windows.Forms.DateTimePicker
        Me.grpGL = New System.Windows.Forms.GroupBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btnCreate = New System.Windows.Forms.Button
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
        Me.lstShops = New GLNet.uiListControl
        Me.grpGL.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'chkGLSQLLog
        '
        Me.chkGLSQLLog.AutoSize = True
        Me.chkGLSQLLog.Location = New System.Drawing.Point(6, 0)
        Me.chkGLSQLLog.Name = "chkGLSQLLog"
        Me.chkGLSQLLog.Size = New System.Drawing.Size(68, 17)
        Me.chkGLSQLLog.TabIndex = 29
        Me.chkGLSQLLog.Text = "SQL Log"
        Me.chkGLSQLLog.UseVisualStyleBackColor = True
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(118, 26)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(13, 13)
        Me.Label28.TabIndex = 3
        Me.Label28.Text = "_"
        '
        'dtpGLLogDateTo
        '
        Me.dtpGLLogDateTo.Checked = False
        Me.dtpGLLogDateTo.CustomFormat = "dd/MMM/yyyy"
        Me.dtpGLLogDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpGLLogDateTo.Location = New System.Drawing.Point(135, 29)
        Me.dtpGLLogDateTo.Name = "dtpGLLogDateTo"
        Me.dtpGLLogDateTo.ShowCheckBox = True
        Me.dtpGLLogDateTo.Size = New System.Drawing.Size(108, 20)
        Me.dtpGLLogDateTo.TabIndex = 4
        '
        'dtpGLLogFrom
        '
        Me.dtpGLLogFrom.Checked = False
        Me.dtpGLLogFrom.CustomFormat = "dd/MMM/yyyy"
        Me.dtpGLLogFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpGLLogFrom.Location = New System.Drawing.Point(6, 29)
        Me.dtpGLLogFrom.Name = "dtpGLLogFrom"
        Me.dtpGLLogFrom.ShowCheckBox = True
        Me.dtpGLLogFrom.Size = New System.Drawing.Size(108, 20)
        Me.dtpGLLogFrom.TabIndex = 3
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(119, 30)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(13, 13)
        Me.Label25.TabIndex = 2
        Me.Label25.Text = "_"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(7, 14)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(73, 13)
        Me.Label26.TabIndex = 1
        Me.Label26.Text = "Voucher Date"
        '
        'dtpVoucherDateTo
        '
        Me.dtpVoucherDateTo.Checked = False
        Me.dtpVoucherDateTo.CustomFormat = "dd/MMM/yyyy"
        Me.dtpVoucherDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpVoucherDateTo.Location = New System.Drawing.Point(136, 31)
        Me.dtpVoucherDateTo.Name = "dtpVoucherDateTo"
        Me.dtpVoucherDateTo.ShowCheckBox = True
        Me.dtpVoucherDateTo.Size = New System.Drawing.Size(108, 20)
        Me.dtpVoucherDateTo.TabIndex = 0
        '
        'dtpVoucherDateFrom
        '
        Me.dtpVoucherDateFrom.Checked = False
        Me.dtpVoucherDateFrom.CustomFormat = "dd/MMM/yyyy"
        Me.dtpVoucherDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpVoucherDateFrom.Location = New System.Drawing.Point(7, 31)
        Me.dtpVoucherDateFrom.Name = "dtpVoucherDateFrom"
        Me.dtpVoucherDateFrom.ShowCheckBox = True
        Me.dtpVoucherDateFrom.Size = New System.Drawing.Size(108, 20)
        Me.dtpVoucherDateFrom.TabIndex = 0
        '
        'grpGL
        '
        Me.grpGL.Controls.Add(Me.Label25)
        Me.grpGL.Controls.Add(Me.Label26)
        Me.grpGL.Controls.Add(Me.dtpVoucherDateTo)
        Me.grpGL.Controls.Add(Me.dtpVoucherDateFrom)
        Me.grpGL.Location = New System.Drawing.Point(12, 12)
        Me.grpGL.Name = "grpGL"
        Me.grpGL.Size = New System.Drawing.Size(261, 63)
        Me.grpGL.TabIndex = 27
        Me.grpGL.TabStop = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.dtpGLLogFrom)
        Me.GroupBox1.Controls.Add(Me.chkGLSQLLog)
        Me.GroupBox1.Controls.Add(Me.dtpGLLogDateTo)
        Me.GroupBox1.Controls.Add(Me.Label28)
        Me.GroupBox1.Location = New System.Drawing.Point(279, 14)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(261, 61)
        Me.GroupBox1.TabIndex = 30
        Me.GroupBox1.TabStop = False
        '
        'btnCreate
        '
        Me.btnCreate.Location = New System.Drawing.Point(714, 565)
        Me.btnCreate.Name = "btnCreate"
        Me.btnCreate.Size = New System.Drawing.Size(277, 41)
        Me.btnCreate.TabIndex = 32
        Me.btnCreate.Text = "Create"
        Me.btnCreate.UseVisualStyleBackColor = True
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
        Me.Panel1.TabIndex = 33
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(965, 0)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(48, 48)
        Me.btnExit.TabIndex = 9
        Me.btnExit.Tag = "HideText"
        Me.btnExit.Text = " "
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
        Me.btnNew.UseVisualStyleBackColor = True
        '
        'lstShops
        '
        Me.lstShops.AddWhichConfiguration = Utility.Utility.EnumProjectForms.ForAllForms
        Me.lstShops.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstShops.BackColor = System.Drawing.Color.Transparent
        Me.lstShops.disableWhenChecked = False
        Me.lstShops.HeadingLabelName = Nothing
        Me.lstShops.HeadingText = "Companies info"
        Me.lstShops.Location = New System.Drawing.Point(714, 12)
        Me.lstShops.Name = "lstShops"
        Me.lstShops.ShowAddNewButton = False
        Me.lstShops.ShowInverse = True
        Me.lstShops.ShowMagnifierButton = False
        Me.lstShops.ShowNoCheck = False
        Me.lstShops.ShowResetAllButton = False
        Me.lstShops.ShowSelectall = True
        Me.lstShops.Size = New System.Drawing.Size(299, 547)
        Me.lstShops.TabIndex = 31
        Me.lstShops.WhichHelp = GLNet.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'frmSchemaSnapshot
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1026, 742)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnCreate)
        Me.Controls.Add(Me.lstShops)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.grpGL)
        Me.KeyPreview = True
        Me.Name = "frmSchemaSnapshot"
        Me.Text = "Schema Snapshot"
        Me.grpGL.ResumeLayout(False)
        Me.grpGL.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents chkGLSQLLog As System.Windows.Forms.CheckBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents dtpGLLogDateTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpGLLogFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents dtpVoucherDateTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpVoucherDateFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents grpGL As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lstShops As GLNet.uiListControl
    Friend WithEvents btnCreate As System.Windows.Forms.Button
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
End Class
