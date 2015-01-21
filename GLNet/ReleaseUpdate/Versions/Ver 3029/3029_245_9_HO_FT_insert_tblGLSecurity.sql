--==============================================================================================
--Add Controls for:			frmGLVoucherHistory
--==============================================================================================
If not Exists(SELECT * FROM tblGLSecurityForm WHERE Form_Name='frmGLVoucherHistory')
Begin
	INSERT INTO [dbo].[tblGLSecurityForm] ([Form_Name],[Form_Label])
	VALUES ('frmGLVoucherHistory','Reports > Voucher History')

End
		If not Exists(SELECT * FROM dbo.tblGLSecurityFormControl WHERE FormID= dbo.GetFormGLID('frmGLVoucherHistory') and ControlName = 'View')
		Begin
			Insert into tblGLSecurityFormControl (FormID, ControlCaption, ControlName)
			Values (dbo.GetFormGLID('frmGLVoucherHistory'),'View', 'View')
		End
		If not Exists(SELECT * FROM tblGLSecurityFormControl WHERE FormID= dbo.GetFormGLID('frmGLVoucherHistory') and ControlName = 'btnPrint')
		Begin
			Insert into tblGLSecurityFormControl (FormID, ControlCaption, ControlName)
			Values (dbo.GetFormGLID('frmGLVoucherHistory'),'Print', 'btnPrint')
		End

		If not Exists(SELECT * FROM tblGLSecurityFormControl WHERE FormID= dbo.GetFormGLID('frmGLVoucherHistory') and ControlName = 'btnExport')
		Begin
			Insert into tblGLSecurityFormControl (FormID, ControlCaption, ControlName)
			Values (dbo.GetFormGLID('frmGLVoucherHistory'),'Export To Excel', 'btnExport')
		End

--==============================================================================================
--==============================================================================================