--==============================================================================================
--Add Controls for:			frmGLVoucherHistory
--==============================================================================================
If not Exists(SELECT * FROM tblGLSecurityForm WHERE Form_Name='frmReindex')
Begin
	INSERT INTO [dbo].[tblGLSecurityForm] ([Form_Name],[Form_Label])
	VALUES ('frmReindex','Utilities > ReIndex Database')

End
		If not Exists(SELECT * FROM dbo.tblGLSecurityFormControl WHERE FormID= dbo.GetFormGLID('frmReindex') and ControlName = 'View')
		Begin
			Insert into tblGLSecurityFormControl (FormID, ControlCaption, ControlName)
			Values (dbo.GetFormGLID('frmReindex'),'View', 'View')
		End
		

--==============================================================================================
--==============================================================================================


