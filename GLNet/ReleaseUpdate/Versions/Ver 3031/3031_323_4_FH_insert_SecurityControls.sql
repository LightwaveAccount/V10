if exists(select * from tblglsecurityForm where form_name='frmGLVoucher')
Begin
	If not Exists(SELECT * FROM tblGLSecurityFormControl WHERE FormID= dbo.GetFormGLID('frmGLVoucher') and ControlName = 'ChequePrint')
	Begin
		Insert into tblGLSecurityFormControl (FormID, ControlCaption, ControlName)
		Values (dbo.GetFormGLID('frmGLVoucher'),'Cheque Print', 'ChequePrint')
	End
	else
	Begin
		update tblGLSecurityFormControl set ControlCaption='Cheque Print' WHERE FormID= dbo.GetFormGLID('frmGLVoucher') and ControlName = 'ChequePrint'
	End
End
