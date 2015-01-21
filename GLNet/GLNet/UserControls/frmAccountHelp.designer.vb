<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAccountHelp
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
        Dim GridEXLayout3 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAccountHelp))
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.txtAcName = New System.Windows.Forms.TextBox
        Me.txtAcCode = New System.Windows.Forms.TextBox
        Me.grdHelp = New Janus.Windows.GridEX.GridEX
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.grdHelp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(423, 294)
        Me.TabControl1.TabIndex = 0
        Me.TabControl1.TabStop = False
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.txtAcName)
        Me.TabPage1.Controls.Add(Me.txtAcCode)
        Me.TabPage1.Controls.Add(Me.grdHelp)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(415, 268)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "GL Account Titles"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'txtAcName
        '
        Me.txtAcName.Location = New System.Drawing.Point(161, 11)
        Me.txtAcName.MaxLength = 50
        Me.txtAcName.Name = "txtAcName"
        Me.txtAcName.Size = New System.Drawing.Size(246, 20)
        Me.txtAcName.TabIndex = 0
        '
        'txtAcCode
        '
        Me.txtAcCode.Location = New System.Drawing.Point(4, 11)
        Me.txtAcCode.MaxLength = 16
        Me.txtAcCode.Name = "txtAcCode"
        Me.txtAcCode.Size = New System.Drawing.Size(148, 20)
        Me.txtAcCode.TabIndex = 1
        '
        'grdHelp
        '
        Me.grdHelp.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdHelp.AlternatingColors = True
        GridEXLayout3.LayoutString = resources.GetString("GridEXLayout3.LayoutString")
        Me.grdHelp.DesignTimeLayout = GridEXLayout3
        Me.grdHelp.EditorsControlStyle.ButtonAppearance = Janus.Windows.GridEX.ButtonAppearance.Regular
        Me.grdHelp.EmptyRows = True
        Me.grdHelp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdHelp.GroupByBoxVisible = False
        Me.grdHelp.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight
        Me.grdHelp.Location = New System.Drawing.Point(4, 37)
        Me.grdHelp.Name = "grdHelp"
        Me.grdHelp.RecordNavigator = True
        Me.grdHelp.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[Default]
        Me.grdHelp.Size = New System.Drawing.Size(408, 228)
        Me.grdHelp.TabIndex = 2
        Me.grdHelp.TabKeyBehavior = Janus.Windows.GridEX.TabKeyBehavior.ControlNavigation
        Me.grdHelp.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'frmAccountHelp
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(423, 294)
        Me.Controls.Add(Me.TabControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAccountHelp"
        Me.Text = "GL Account Titles"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.grdHelp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents grdHelp As Janus.Windows.GridEX.GridEX
    Friend WithEvents txtAcName As System.Windows.Forms.TextBox
    Friend WithEvents txtAcCode As System.Windows.Forms.TextBox
End Class
