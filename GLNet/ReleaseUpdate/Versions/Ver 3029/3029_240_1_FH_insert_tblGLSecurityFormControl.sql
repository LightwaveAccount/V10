If not Exists(SELECT * FROM tblGLSecurityForm WHERE Form_Name='frmSMS')
Begin
	INSERT INTO [dbo].[tblGLSecurityForm] ([form_name],[form_label])
	VALUES ('frmSMS','Send SMS Utility')

End
		If not Exists(SELECT * FROM dbo.tblGLSecurityFormControl WHERE FormID= dbo.GetFormGLID('frmSMS') and ControlName = 'View')
		Begin
			Insert into tblGLSecurityFormControl (FormID, ControlCaption, ControlName)
			Values (dbo.GetFormGLID('frmSMS'),'View', 'View')
		End

		If not Exists(SELECT * FROM dbo.tblGLSecurityFormControl WHERE FormID= dbo.GetFormGLID('frmSMS') and ControlName = 'btnSendSMS')
		Begin
			Insert into tblGLSecurityFormControl (FormID, ControlCaption, ControlName)
			Values (dbo.GetFormGLID('frmSMS'),'Send SMS', 'btnSendSMS')
		End

		