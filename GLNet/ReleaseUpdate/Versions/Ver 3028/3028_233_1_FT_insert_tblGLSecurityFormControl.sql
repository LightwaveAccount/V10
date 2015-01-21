If not Exists(SELECT * FROM dbo.tblGLSecurityFormControl WHERE FormID= dbo.GetFormGLID('frmGLPostingVoucher') and ControlName = 'Post')
Begin
	Insert into tblGLSecurityFormControl (FormID, ControlCaption, ControlName)
	Values (dbo.GetFormGLID('frmGLPostingVoucher'),'Post', 'Post')
End

If not Exists(SELECT * FROM dbo.tblGLSecurityFormControl WHERE FormID= dbo.GetFormGLID('frmGLPostingVoucher') and ControlName = 'UnPost')
Begin
		Insert into tblGLSecurityFormControl (FormID, ControlCaption, ControlName)
		Values (dbo.GetFormGLID('frmGLPostingVoucher'),'UnPost', 'UnPost')
End


