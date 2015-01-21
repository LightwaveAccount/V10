<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGLNotes
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
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.grdBar = New GLNet.uiCtrlGridBar
        Me.grdGlNotes = New Janus.Windows.GridEX.GridEX
        Me.Button1 = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.grdGlNotes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GridEX1
        '
        Me.GridEX1.Location = New System.Drawing.Point(0, 0)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.Size = New System.Drawing.Size(400, 376)
        Me.GridEX1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.grdBar)
        Me.Panel1.Controls.Add(Me.grdGlNotes)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Location = New System.Drawing.Point(12, 32)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1004, 701)
        Me.Panel1.TabIndex = 3
        '
        'grdBar
        '
        Me.grdBar.Location = New System.Drawing.Point(15, 55)
        Me.grdBar.MyGrid = Me.grdGlNotes
        Me.grdBar.Name = "grdBar"
        Me.grdBar.Size = New System.Drawing.Size(971, 25)
        Me.grdBar.TabIndex = 5
        '
        'grdGlNotes
        '
        Me.grdGlNotes.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdGlNotes.EmptyRows = True
        Me.grdGlNotes.Location = New System.Drawing.Point(15, 86)
        Me.grdGlNotes.Name = "grdGlNotes"
        Me.grdGlNotes.Size = New System.Drawing.Size(971, 601)
        Me.grdGlNotes.TabIndex = 4
        Me.grdGlNotes.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdGlNotes.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(15, 12)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(151, 37)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "Chart Of Account "
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.SystemColors.Desktop
        Me.Label1.Location = New System.Drawing.Point(12, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(164, 24)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "GL Notes Report"
        '
        'frmGLNotes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1026, 744)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmGLNotes"
        Me.Text = "GL Notes .. "
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.grdGlNotes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents grdBar As GLNet.uiCtrlGridBar
    Friend WithEvents grdGlNotes As Janus.Windows.GridEX.GridEX
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
