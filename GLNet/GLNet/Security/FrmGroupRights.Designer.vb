<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmGroupRights
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.grdAllRecords = New Janus.Windows.GridEX.GridEX
        Me.chkExportAll = New System.Windows.Forms.CheckBox
        Me.chkPrintAll = New System.Windows.Forms.CheckBox
        Me.chkViewAll = New System.Windows.Forms.CheckBox
        Me.chkDeleteAll = New System.Windows.Forms.CheckBox
        Me.chkUpdateAll = New System.Windows.Forms.CheckBox
        Me.chkSaveAll = New System.Windows.Forms.CheckBox
        Me.btnExpnd1stLevel = New System.Windows.Forms.Button
        Me.cboFormGroups = New System.Windows.Forms.ComboBox
        Me.lblFormGroups = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkOther = New System.Windows.Forms.CheckBox
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.UiCtrlGridBar1 = New GLNet.uiCtrlGridBar
        Me.Label1 = New System.Windows.Forms.Label
        Me.Panel3 = New System.Windows.Forms.Panel
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
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.grdAllRecords, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'grdAllRecords
        '
        Me.grdAllRecords.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdAllRecords.AutomaticSort = False
        Me.grdAllRecords.EditorsControlStyle.ButtonAppearance = Janus.Windows.GridEX.ButtonAppearance.Regular
        Me.grdAllRecords.EmptyRows = True
        Me.grdAllRecords.FilterMode = Janus.Windows.GridEX.FilterMode.Manual
        Me.grdAllRecords.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdAllRecords.GroupMode = Janus.Windows.GridEX.GroupMode.[Default]
        Me.grdAllRecords.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight
        Me.grdAllRecords.Location = New System.Drawing.Point(8, 36)
        Me.grdAllRecords.Name = "grdAllRecords"
        Me.grdAllRecords.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdAllRecords.Size = New System.Drawing.Size(764, 526)
        Me.grdAllRecords.TabIndex = 0
        Me.grdAllRecords.TabKeyBehavior = Janus.Windows.GridEX.TabKeyBehavior.ControlNavigation
        Me.grdAllRecords.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        '
        'chkExportAll
        '
        Me.chkExportAll.AutoSize = True
        Me.chkExportAll.Location = New System.Drawing.Point(138, 153)
        Me.chkExportAll.Name = "chkExportAll"
        Me.chkExportAll.Size = New System.Drawing.Size(70, 17)
        Me.chkExportAll.TabIndex = 10
        Me.chkExportAll.Text = "Export All"
        Me.chkExportAll.UseVisualStyleBackColor = True
        '
        'chkPrintAll
        '
        Me.chkPrintAll.AutoSize = True
        Me.chkPrintAll.Location = New System.Drawing.Point(6, 153)
        Me.chkPrintAll.Name = "chkPrintAll"
        Me.chkPrintAll.Size = New System.Drawing.Size(61, 17)
        Me.chkPrintAll.TabIndex = 9
        Me.chkPrintAll.Text = "Print All"
        Me.chkPrintAll.UseVisualStyleBackColor = True
        '
        'chkViewAll
        '
        Me.chkViewAll.AutoSize = True
        Me.chkViewAll.Location = New System.Drawing.Point(6, 107)
        Me.chkViewAll.Name = "chkViewAll"
        Me.chkViewAll.Size = New System.Drawing.Size(63, 17)
        Me.chkViewAll.TabIndex = 5
        Me.chkViewAll.Text = "View All"
        Me.chkViewAll.UseVisualStyleBackColor = True
        '
        'chkDeleteAll
        '
        Me.chkDeleteAll.AutoSize = True
        Me.chkDeleteAll.Location = New System.Drawing.Point(138, 130)
        Me.chkDeleteAll.Name = "chkDeleteAll"
        Me.chkDeleteAll.Size = New System.Drawing.Size(71, 17)
        Me.chkDeleteAll.TabIndex = 8
        Me.chkDeleteAll.Text = "Delete All"
        Me.chkDeleteAll.UseVisualStyleBackColor = True
        '
        'chkUpdateAll
        '
        Me.chkUpdateAll.AutoSize = True
        Me.chkUpdateAll.Location = New System.Drawing.Point(6, 130)
        Me.chkUpdateAll.Name = "chkUpdateAll"
        Me.chkUpdateAll.Size = New System.Drawing.Size(75, 17)
        Me.chkUpdateAll.TabIndex = 7
        Me.chkUpdateAll.Text = "Update All"
        Me.chkUpdateAll.UseVisualStyleBackColor = True
        '
        'chkSaveAll
        '
        Me.chkSaveAll.AutoSize = True
        Me.chkSaveAll.Location = New System.Drawing.Point(138, 107)
        Me.chkSaveAll.Name = "chkSaveAll"
        Me.chkSaveAll.Size = New System.Drawing.Size(65, 17)
        Me.chkSaveAll.TabIndex = 6
        Me.chkSaveAll.Text = "Save All"
        Me.chkSaveAll.UseVisualStyleBackColor = True
        '
        'btnExpnd1stLevel
        '
        Me.btnExpnd1stLevel.Location = New System.Drawing.Point(3, 60)
        Me.btnExpnd1stLevel.Name = "btnExpnd1stLevel"
        Me.btnExpnd1stLevel.Size = New System.Drawing.Size(206, 41)
        Me.btnExpnd1stLevel.TabIndex = 3
        Me.btnExpnd1stLevel.Tag = ""
        Me.btnExpnd1stLevel.Text = " Expand/Collapse Groups"
        Me.btnExpnd1stLevel.UseVisualStyleBackColor = True
        '
        'cboFormGroups
        '
        Me.cboFormGroups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFormGroups.FormattingEnabled = True
        Me.cboFormGroups.Location = New System.Drawing.Point(3, 33)
        Me.cboFormGroups.Name = "cboFormGroups"
        Me.cboFormGroups.Size = New System.Drawing.Size(206, 21)
        Me.cboFormGroups.TabIndex = 1
        '
        'lblFormGroups
        '
        Me.lblFormGroups.Location = New System.Drawing.Point(8, 14)
        Me.lblFormGroups.Name = "lblFormGroups"
        Me.lblFormGroups.Size = New System.Drawing.Size(100, 15)
        Me.lblFormGroups.TabIndex = 0
        Me.lblFormGroups.Text = "Form Groups"
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.chkOther)
        Me.Panel1.Controls.Add(Me.chkExportAll)
        Me.Panel1.Controls.Add(Me.chkPrintAll)
        Me.Panel1.Controls.Add(Me.chkViewAll)
        Me.Panel1.Controls.Add(Me.chkDeleteAll)
        Me.Panel1.Controls.Add(Me.chkUpdateAll)
        Me.Panel1.Controls.Add(Me.chkSaveAll)
        Me.Panel1.Controls.Add(Me.btnExpnd1stLevel)
        Me.Panel1.Controls.Add(Me.cboFormGroups)
        Me.Panel1.Controls.Add(Me.lblFormGroups)
        Me.Panel1.Location = New System.Drawing.Point(5, 36)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(225, 570)
        Me.Panel1.TabIndex = 2
        '
        'chkOther
        '
        Me.chkOther.AutoSize = True
        Me.chkOther.Location = New System.Drawing.Point(6, 176)
        Me.chkOther.Name = "chkOther"
        Me.chkOther.Size = New System.Drawing.Size(66, 17)
        Me.chkOther.TabIndex = 11
        Me.chkOther.Text = "Other All"
        Me.chkOther.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.grdAllRecords)
        Me.Panel2.Controls.Add(Me.UiCtrlGridBar1)
        Me.Panel2.Location = New System.Drawing.Point(236, 36)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(782, 570)
        Me.Panel2.TabIndex = 1
        '
        'UiCtrlGridBar1
        '
        Me.UiCtrlGridBar1.Location = New System.Drawing.Point(3, 4)
        Me.UiCtrlGridBar1.MyGrid = Me.grdAllRecords
        Me.UiCtrlGridBar1.Name = "UiCtrlGridBar1"
        Me.UiCtrlGridBar1.Size = New System.Drawing.Size(769, 25)
        Me.UiCtrlGridBar1.TabIndex = 0
        Me.UiCtrlGridBar1.TabStop = False
        '
        'Label1
        '
        Me.Label1.AccessibleDescription = "Title"
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.SystemColors.Desktop
        Me.Label1.Location = New System.Drawing.Point(5, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(132, 24)
        Me.Label1.TabIndex = 25
        Me.Label1.Text = "Group Rights"
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
        Me.Panel3.Controls.Add(Me.btnSave)
        Me.Panel3.Controls.Add(Me.btnNew)
        Me.Panel3.Location = New System.Drawing.Point(0, 612)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1034, 49)
        Me.Panel3.TabIndex = 27
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(970, 0)
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
        Me.btnFirst.Location = New System.Drawing.Point(5, 1)
        Me.btnFirst.Name = "btnFirst"
        Me.btnFirst.Size = New System.Drawing.Size(48, 48)
        Me.btnFirst.TabIndex = 0
        Me.btnFirst.Tag = "HideText"
        Me.btnFirst.Text = " "
        Me.btnFirst.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
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
        Me.btnPrevious.Location = New System.Drawing.Point(59, 0)
        Me.btnPrevious.Name = "btnPrevious"
        Me.btnPrevious.Size = New System.Drawing.Size(48, 48)
        Me.btnPrevious.TabIndex = 1
        Me.btnPrevious.Tag = "HideText"
        Me.btnPrevious.Text = " "
        Me.btnPrevious.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.DialogResult = System.Windows.Forms.DialogResult.Cancel
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
        Me.btnNext.Location = New System.Drawing.Point(113, 1)
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
        Me.btnLast.Location = New System.Drawing.Point(167, 0)
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
        'FrmGroupRights
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1030, 746)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmGroupRights"
        Me.Text = "Group Rights"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.grdAllRecords, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdAllRecords As Janus.Windows.GridEX.GridEX
    Friend WithEvents UiCtrlGridBar1 As GLNet.uiCtrlGridBar
    Friend WithEvents cboFormGroups As System.Windows.Forms.ComboBox
    Friend WithEvents lblFormGroups As System.Windows.Forms.Label
    '  Friend WithEvents lstFormCategories As GLNet.uiListControl
    Friend WithEvents btnExpnd1stLevel As System.Windows.Forms.Button
    Friend WithEvents chkExportAll As System.Windows.Forms.CheckBox
    Friend WithEvents chkPrintAll As System.Windows.Forms.CheckBox
    Friend WithEvents chkViewAll As System.Windows.Forms.CheckBox
    Friend WithEvents chkDeleteAll As System.Windows.Forms.CheckBox
    Friend WithEvents chkUpdateAll As System.Windows.Forms.CheckBox
    Friend WithEvents chkSaveAll As System.Windows.Forms.CheckBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
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
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents chkOther As System.Windows.Forms.CheckBox
End Class
