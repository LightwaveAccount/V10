If not Exists(SELECT * FROM tblGLSecurityFormControl WHERE FormID= dbo.GetFormGLID('frmGLVoucher') and ControlName = 'cboCompany')
Begin
	Insert into tblGLSecurityFormControl (FormID, ControlCaption, ControlName)
	Values (dbo.GetFormGLID('frmGLVoucher'),'Edit Company', 'cboCompany')
End

If not Exists(SELECT * FROM tblGLSecurityFormControl WHERE FormID= dbo.GetFormGLID('frmGLVoucher') and ControlName = 'cboSource')
Begin
	Insert into tblGLSecurityFormControl (FormID, ControlCaption, ControlName)
	Values (dbo.GetFormGLID('frmGLVoucher'),'Edit Source', 'cboSource')
End

