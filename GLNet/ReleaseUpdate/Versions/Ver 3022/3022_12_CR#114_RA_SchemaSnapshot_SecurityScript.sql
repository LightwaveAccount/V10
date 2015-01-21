If not Exists(SELECT * FROM tblGLSecurityForm WHERE Form_Name='frmSchemaSnapshot')
Begin
	INSERT INTO [dbo].[tblGLSecurityForm] ([Form_Name],[Form_Label])
	VALUES ('frmSchemaSnapshot','Utilities > Schema Snapshot')

End
		If not Exists(SELECT * FROM dbo.tblGLSecurityFormControl WHERE FormID= dbo.GetFormGLID('frmSchemaSnapshot') and ControlName = 'View')
		Begin
			Insert into tblGLSecurityFormControl (FormID, ControlCaption, ControlName)
			Values (dbo.GetFormGLID('frmSchemaSnapshot'),'View', 'View')
		End



