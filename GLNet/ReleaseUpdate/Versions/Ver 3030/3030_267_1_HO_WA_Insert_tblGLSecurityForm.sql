--==============================================================================================
--Add Controls for:			frmDataTransfer
--==============================================================================================
If not Exists(SELECT * FROM tblGLSecurityForm WHERE Form_Name='frmDataTransfer')
Begin
	INSERT INTO [dbo].[tblGLSecurityForm] ([Form_Name],[Form_Label])
	VALUES ('frmDataTransfer','Utilities > Transfer GL Data')

End
		If not Exists(SELECT * FROM dbo.tblGLSecurityFormControl WHERE FormID= dbo.GetFormGLID('frmDataTransfer') and ControlName = 'View')
		Begin
			Insert into tblGLSecurityFormControl (FormID, ControlCaption, ControlName)
			Values (dbo.GetFormGLID('frmDataTransfer'),'View', 'View')
		End


		If not Exists(SELECT * FROM tblGLSecurityFormControl WHERE FormID= dbo.GetFormGLID('frmDataTransfer') and ControlName = 'btnTransfer')
		Begin
			Insert into tblGLSecurityFormControl (FormID, ControlCaption, ControlName)
			Values (dbo.GetFormGLID('frmDataTransfer'),'Import Data', 'btnTransfer')
		End

--==============================================================================================
--==============================================================================================