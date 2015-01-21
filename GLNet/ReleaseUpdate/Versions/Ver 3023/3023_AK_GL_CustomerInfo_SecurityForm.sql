If not Exists(SELECT * FROM tblGLSecurityForm WHERE Form_Name='frmCustomerInfo')
Begin
	INSERT INTO [dbo].[tblGLSecurityForm] ([form_name],[form_label])
	VALUES ('frmCustomerInfo','Customer Information')

End
		If not Exists(SELECT * FROM dbo.tblGLSecurityFormControl WHERE FormID= dbo.GetFormGLID('frmCustomerInfo') and ControlName = 'View')
		Begin
			Insert into tblGLSecurityFormControl (FormID, ControlCaption, ControlName)
			Values (dbo.GetFormGLID('frmCustomerInfo'),'View', 'View')
		End

		If not Exists(SELECT * FROM dbo.tblGLSecurityFormControl WHERE FormID= dbo.GetFormGLID('frmCustomerInfo') and ControlName = 'btnSave')
		Begin
			Insert into tblGLSecurityFormControl (FormID, ControlCaption, ControlName)
			Values (dbo.GetFormGLID('frmCustomerInfo'),'Save', 'btnSave')
		End

		If not Exists(SELECT * FROM dbo.tblGLSecurityFormControl WHERE FormID= dbo.GetFormGLID('frmCustomerInfo') and ControlName = 'btnUpdate')
		Begin
			Insert into tblGLSecurityFormControl (FormID, ControlCaption, ControlName)
			Values (dbo.GetFormGLID('frmCustomerInfo'),'Update', 'btnUpdate')
		End

		If not Exists(SELECT * FROM dbo.tblGLSecurityFormControl WHERE FormID= dbo.GetFormGLID('frmCustomerInfo') and ControlName = 'btnDelete')
		Begin
			Insert into tblGLSecurityFormControl (FormID, ControlCaption, ControlName)
			Values (dbo.GetFormGLID('frmCustomerInfo'),'Delete', 'btnDelete')
		End

--
		If not Exists(SELECT * FROM tblGLSecurityFormControl WHERE FormID= dbo.GetFormGLID('frmCustomerInfo') and ControlName = 'btnPrint')
		Begin
			Insert into tblGLSecurityFormControl (FormID, ControlCaption, ControlName)
			Values (dbo.GetFormGLID('frmCustomerInfo'),'Print', 'btnPrint')
		End

		If not Exists(SELECT * FROM tblGLSecurityFormControl WHERE FormID= dbo.GetFormGLID('frmCustomerInfo') and ControlName = 'btnExport')
		Begin
			Insert into tblGLSecurityFormControl (FormID, ControlCaption, ControlName)
			Values (dbo.GetFormGLID('frmCustomerInfo'),'Export To Excel', 'btnExport')
		End

