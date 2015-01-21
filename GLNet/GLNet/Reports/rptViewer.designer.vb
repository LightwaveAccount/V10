<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class rptViewer
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
        Me.CR = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.lblEmail = New System.Windows.Forms.LinkLabel
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.btnSendMail = New System.Windows.Forms.Button
        Me.txtMessageBody = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtSubject = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtEmailTo = New System.Windows.Forms.TextBox
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'CR
        '
        Me.CR.ActiveViewIndex = -1
        Me.CR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CR.DisplayGroupTree = False
        Me.CR.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CR.Location = New System.Drawing.Point(0, 0)
        Me.CR.Name = "CR"
        Me.CR.SelectionFormula = ""
        Me.CR.Size = New System.Drawing.Size(1028, 744)
        Me.CR.TabIndex = 0
        Me.CR.ViewTimeSelectionFormula = ""
        '
        'lblEmail
        '
        Me.lblEmail.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblEmail.AutoSize = True
        Me.lblEmail.Enabled = False
        Me.lblEmail.Location = New System.Drawing.Point(950, 37)
        Me.lblEmail.Name = "lblEmail"
        Me.lblEmail.Size = New System.Drawing.Size(66, 13)
        Me.lblEmail.TabIndex = 5
        Me.lblEmail.TabStop = True
        Me.lblEmail.Text = "E-Mail report"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.btnSendMail)
        Me.GroupBox1.Controls.Add(Me.txtMessageBody)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtSubject)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtEmailTo)
        Me.GroupBox1.Location = New System.Drawing.Point(703, 53)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(313, 254)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "E-Mail Options"
        Me.GroupBox1.Visible = False
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(232, 224)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 7
        Me.Button1.Text = "&Cancel"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 107)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Message:"
        '
        'btnSendMail
        '
        Me.btnSendMail.Location = New System.Drawing.Point(151, 224)
        Me.btnSendMail.Name = "btnSendMail"
        Me.btnSendMail.Size = New System.Drawing.Size(75, 23)
        Me.btnSendMail.TabIndex = 6
        Me.btnSendMail.Text = "&Send Mail"
        Me.btnSendMail.UseVisualStyleBackColor = True
        '
        'txtMessageBody
        '
        Me.txtMessageBody.Location = New System.Drawing.Point(67, 104)
        Me.txtMessageBody.Multiline = True
        Me.txtMessageBody.Name = "txtMessageBody"
        Me.txtMessageBody.Size = New System.Drawing.Size(240, 114)
        Me.txtMessageBody.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 81)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(46, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Subject:"
        '
        'txtSubject
        '
        Me.txtSubject.Location = New System.Drawing.Point(67, 78)
        Me.txtSubject.Name = "txtSubject"
        Me.txtSubject.Size = New System.Drawing.Size(240, 20)
        Me.txtSubject.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "E-Mail To:"
        '
        'txtEmailTo
        '
        Me.txtEmailTo.Location = New System.Drawing.Point(67, 20)
        Me.txtEmailTo.Multiline = True
        Me.txtEmailTo.Name = "txtEmailTo"
        Me.txtEmailTo.Size = New System.Drawing.Size(240, 52)
        Me.txtEmailTo.TabIndex = 1
        '
        'rptViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1028, 744)
        Me.Controls.Add(Me.lblEmail)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.CR)
        Me.Name = "rptViewer"
        Me.Text = "rptViewer"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CR As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents lblEmail As System.Windows.Forms.LinkLabel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnSendMail As System.Windows.Forms.Button
    Friend WithEvents txtMessageBody As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtSubject As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtEmailTo As System.Windows.Forms.TextBox
End Class
