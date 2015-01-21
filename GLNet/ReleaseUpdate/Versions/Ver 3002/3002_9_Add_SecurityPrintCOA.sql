If not Exists(SELECT * FROM tblGLSecurityFormControl WHERE FormID= dbo.GetFormGLID('frmrptCOA') and ControlName = 'btnPrint')
Begin
	Insert into tblGLSecurityFormControl (FormID, ControlCaption, ControlName)
	Values (dbo.GetFormGLID('frmrptCOA'),'Print', 'btnPrint')
End