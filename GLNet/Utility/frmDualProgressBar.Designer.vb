<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDualProgressBar
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
        Me.lblStatus = New System.Windows.Forms.Label
        Me.PB = New System.Windows.Forms.ProgressBar
        Me.lblMain = New System.Windows.Forms.Label
        Me.PBMain = New System.Windows.Forms.ProgressBar
        Me.SuspendLayout()
        '
        'lblStatus
        '
        Me.lblStatus.Location = New System.Drawing.Point(7, 33)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(431, 17)
        Me.lblStatus.TabIndex = 3
        Me.lblStatus.Text = "Label1"
        '
        'PB
        '
        Me.PB.Location = New System.Drawing.Point(7, 4)
        Me.PB.Name = "PB"
        Me.PB.Size = New System.Drawing.Size(436, 26)
        Me.PB.TabIndex = 2
        '
        'lblMain
        '
        Me.lblMain.Location = New System.Drawing.Point(7, 82)
        Me.lblMain.Name = "lblMain"
        Me.lblMain.Size = New System.Drawing.Size(431, 17)
        Me.lblMain.TabIndex = 5
        Me.lblMain.Text = "Label1"
        '
        'PBMain
        '
        Me.PBMain.Location = New System.Drawing.Point(7, 53)
        Me.PBMain.Name = "PBMain"
        Me.PBMain.Size = New System.Drawing.Size(436, 26)
        Me.PBMain.TabIndex = 4
        '
        'frmDualProgressBar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(450, 104)
        Me.Controls.Add(Me.lblMain)
        Me.Controls.Add(Me.PBMain)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.PB)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDualProgressBar"
        Me.Text = "Progress Bar"
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents lblStatus As System.Windows.Forms.Label
    Public WithEvents PB As System.Windows.Forms.ProgressBar
    Public WithEvents lblMain As System.Windows.Forms.Label
    Public WithEvents PBMain As System.Windows.Forms.ProgressBar
End Class
