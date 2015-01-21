<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSMSConfiguration
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
        Me.TabPgSMSConfiguration = New System.Windows.Forms.TabPage
        Me.lbltext = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.UiCtrlGridBar1 = New GLNet.uiCtrlGridBar
        Me.grdSMSConfiguration = New Janus.Windows.GridEX.GridEX
        Me.BtnHelpText = New System.Windows.Forms.Button
        Me.TabSmsConfiguration = New System.Windows.Forms.TabControl
        Me.Panel2 = New System.Windows.Forms.Panel
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
        Me.TabPgSMSConfiguration.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.grdSMSConfiguration, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabSmsConfiguration.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabPgSMSConfiguration
        '
        Me.TabPgSMSConfiguration.Controls.Add(Me.lbltext)
        Me.TabPgSMSConfiguration.Controls.Add(Me.GroupBox2)
        Me.TabPgSMSConfiguration.Location = New System.Drawing.Point(4, 22)
        Me.TabPgSMSConfiguration.Margin = New System.Windows.Forms.Padding(0)
        Me.TabPgSMSConfiguration.Name = "TabPgSMSConfiguration"
        Me.TabPgSMSConfiguration.Size = New System.Drawing.Size(1020, 530)
        Me.TabPgSMSConfiguration.TabIndex = 0
        Me.TabPgSMSConfiguration.Text = "SMS Configuration"
        Me.TabPgSMSConfiguration.UseVisualStyleBackColor = True
        '
        'lbltext
        '
        Me.lbltext.AccessibleDescription = "Title"
        Me.lbltext.AutoSize = True
        Me.lbltext.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold)
        Me.lbltext.ForeColor = System.Drawing.SystemColors.Desktop
        Me.lbltext.Location = New System.Drawing.Point(8, 14)
        Me.lbltext.Name = "lbltext"
        Me.lbltext.Size = New System.Drawing.Size(183, 24)
        Me.lbltext.TabIndex = 16
        Me.lbltext.Text = "SMS Configuration"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.UiCtrlGridBar1)
        Me.GroupBox2.Controls.Add(Me.grdSMSConfiguration)
        Me.GroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.GroupBox2.Location = New System.Drawing.Point(4, 45)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1011, 483)
        Me.GroupBox2.TabIndex = 9
        Me.GroupBox2.TabStop = False
        '
        'UiCtrlGridBar1
        '
        Me.UiCtrlGridBar1.Location = New System.Drawing.Point(2, 16)
        Me.UiCtrlGridBar1.MyGrid = Me.grdSMSConfiguration
        Me.UiCtrlGridBar1.Name = "UiCtrlGridBar1"
        Me.UiCtrlGridBar1.Size = New System.Drawing.Size(1004, 25)
        Me.UiCtrlGridBar1.TabIndex = 5
        Me.UiCtrlGridBar1.TabStop = False
        '
        'grdSMSConfiguration
        '
        Me.grdSMSConfiguration.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdSMSConfiguration.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdSMSConfiguration.GroupByBoxVisible = False
        Me.grdSMSConfiguration.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.grdSMSConfiguration.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight
        Me.grdSMSConfiguration.Location = New System.Drawing.Point(4, 47)
        Me.grdSMSConfiguration.Name = "grdSMSConfiguration"
        Me.grdSMSConfiguration.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdSMSConfiguration.Size = New System.Drawing.Size(1002, 436)
        Me.grdSMSConfiguration.TabIndex = 4
        Me.grdSMSConfiguration.TabKeyBehavior = Janus.Windows.GridEX.TabKeyBehavior.ControlNavigation
        Me.grdSMSConfiguration.UpdateMode = Janus.Windows.GridEX.UpdateMode.CellUpdate
        '
        'BtnHelpText
        '
        Me.BtnHelpText.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnHelpText.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.BtnHelpText.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnHelpText.Location = New System.Drawing.Point(953, -20)
        Me.BtnHelpText.Name = "BtnHelpText"
        Me.BtnHelpText.Size = New System.Drawing.Size(20, 20)
        Me.BtnHelpText.TabIndex = 55
        Me.BtnHelpText.TabStop = False
        Me.BtnHelpText.Tag = "HideText"
        Me.BtnHelpText.UseVisualStyleBackColor = True
        '
        'TabSmsConfiguration
        '
        Me.TabSmsConfiguration.Controls.Add(Me.TabPgSMSConfiguration)
        Me.TabSmsConfiguration.Location = New System.Drawing.Point(1, 1)
        Me.TabSmsConfiguration.Margin = New System.Windows.Forms.Padding(0)
        Me.TabSmsConfiguration.Name = "TabSmsConfiguration"
        Me.TabSmsConfiguration.Padding = New System.Drawing.Point(0, 0)
        Me.TabSmsConfiguration.SelectedIndex = 0
        Me.TabSmsConfiguration.Size = New System.Drawing.Size(1028, 556)
        Me.TabSmsConfiguration.TabIndex = 54
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
        Me.Panel2.Location = New System.Drawing.Point(1, 557)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1022, 50)
        Me.Panel2.TabIndex = 56
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(962, 0)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(48, 48)
        Me.btnExit.TabIndex = 9
        Me.btnExit.Tag = "HideText"
        Me.btnExit.Text = " "
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnFirst
        '
        Me.btnFirst.Location = New System.Drawing.Point(10, 0)
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
        Me.btnPrevious.Location = New System.Drawing.Point(64, 0)
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
        Me.btnNext.Location = New System.Drawing.Point(118, 0)
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
        Me.btnLast.Location = New System.Drawing.Point(172, 0)
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
        'frmSMSConfiguration
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1023, 669)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.BtnHelpText)
        Me.Controls.Add(Me.TabSmsConfiguration)
        Me.KeyPreview = True
        Me.Name = "frmSMSConfiguration"
        Me.Text = "frmSMSConfiguration"
        Me.TabPgSMSConfiguration.ResumeLayout(False)
        Me.TabPgSMSConfiguration.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.grdSMSConfiguration, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabSmsConfiguration.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabPgSMSConfiguration As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents grdSMSConfiguration As Janus.Windows.GridEX.GridEX
    Friend WithEvents BtnHelpText As System.Windows.Forms.Button
    Friend WithEvents TabSmsConfiguration As System.Windows.Forms.TabControl
    Friend WithEvents UiCtrlGridBar1 As GLNet.uiCtrlGridBar
    Friend WithEvents lbltext As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
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
