If not Exists(SELECT * FROM tblGLSecurityForm WHERE Form_Name='frmContectDirec')
Begin
	INSERT INTO [dbo].[tblGLSecurityForm] ([form_name],[form_label])
	VALUES ('frmContectDirec','Contact Directory')

End
		If not Exists(SELECT * FROM dbo.tblGLSecurityFormControl WHERE FormID= dbo.GetFormGLID('frmContectDirec') and ControlName = 'View')
		Begin
			Insert into tblGLSecurityFormControl (FormID, ControlCaption, ControlName)
			Values (dbo.GetFormGLID('frmContectDirec'),'View', 'View')
		End

		If not Exists(SELECT * FROM dbo.tblGLSecurityFormControl WHERE FormID= dbo.GetFormGLID('frmContectDirec') and ControlName = 'BtnUpdate')
		Begin
			Insert into tblGLSecurityFormControl (FormID, ControlCaption, ControlName)
			Values (dbo.GetFormGLID('frmContectDirec'),'Update', 'BtnUpdate')
		End

		If not Exists(SELECT * FROM dbo.tblGLSecurityFormControl WHERE FormID= dbo.GetFormGLID('frmContectDirec') and ControlName = 'BtnSave')
		Begin
			Insert into tblGLSecurityFormControl (FormID, ControlCaption, ControlName)
			Values (dbo.GetFormGLID('frmContectDirec'),'Save', 'BtnSave')
		End

		If not Exists(SELECT * FROM dbo.tblGLSecurityFormControl WHERE FormID= dbo.GetFormGLID('frmContectDirec') and ControlName = 'BtnDelete')
		Begin
			Insert into tblGLSecurityFormControl (FormID, ControlCaption, ControlName)
			Values (dbo.GetFormGLID('frmContectDirec'),'Delate', 'BtnDelete')
		End

If not Exists(SELECT * FROM dbo.tblGLSecurityFormControl WHERE FormID= dbo.GetFormGLID('frmContectDirec') and ControlName = 'btnExport')
		Begin
			Insert into tblGLSecurityFormControl (FormID, ControlCaption, ControlName)
			Values (dbo.GetFormGLID('frmContectDirec'),'Export', 'btnExport')
		End

		If not Exists(SELECT * FROM dbo.tblGLSecurityFormControl WHERE FormID= dbo.GetFormGLID('frmContectDirec') and ControlName = 'btnPrint')
		Begin
			Insert into tblGLSecurityFormControl (FormID, ControlCaption, ControlName)
			Values (dbo.GetFormGLID('frmContectDirec'),'Print', 'btnPrint')
		End

	