If not Exists(SELECT * FROM tblGLSecurityForm WHERE Form_Name='frmSMSConfiguration')
Begin
	INSERT INTO [dbo].[tblGLSecurityForm] ([form_name],[form_label])
	VALUES ('frmSMSConfiguration','SMS Configuration')

End
		If not Exists(SELECT * FROM dbo.tblGLSecurityFormControl WHERE FormID= dbo.GetFormGLID('frmSMSConfiguration') and ControlName = 'View')
		Begin
			Insert into tblGLSecurityFormControl (FormID, ControlCaption, ControlName)
			Values (dbo.GetFormGLID('frmSMSConfiguration'),'View', 'View')
		End

		If not Exists(SELECT * FROM dbo.tblGLSecurityFormControl WHERE FormID= dbo.GetFormGLID('frmSMSConfiguration') and ControlName = 'BtnUpdate')
		Begin
			Insert into tblGLSecurityFormControl (FormID, ControlCaption, ControlName)
			Values (dbo.GetFormGLID('frmSMSConfiguration'),'Update', 'BtnUpdate')
		End
If not Exists(SELECT * FROM dbo.tblGLSecurityFormControl WHERE FormID= dbo.GetFormGLID('frmSMSConfiguration') and ControlName = 'btnExport')
		Begin
			Insert into tblGLSecurityFormControl (FormID, ControlCaption, ControlName)
			Values (dbo.GetFormGLID('frmSMSConfiguration'),'Export', 'btnExport')
		End

		If not Exists(SELECT * FROM dbo.tblGLSecurityFormControl WHERE FormID= dbo.GetFormGLID('frmSMSConfiguration') and ControlName = 'btnPrint')
		Begin
			Insert into tblGLSecurityFormControl (FormID, ControlCaption, ControlName)
			Values (dbo.GetFormGLID('frmSMSConfiguration'),'Print', 'btnPrint')
		End

		