if exists(select * from tblglsecurityForm where form_name='frmDataTransfer')
Begin
	If not Exists(SELECT * FROM tblGLSecurityFormControl WHERE FormID= dbo.GetFormGLID('frmDataTransfer') and ControlName = 'btnTransfer')
	Begin
		Insert into tblGLSecurityFormControl (FormID, ControlCaption, ControlName)
		Values (dbo.GetFormGLID('frmDataTransfer'),'Export Data', 'btnTransfer')
	End
	else
	Begin
		update tblGLSecurityFormControl set ControlCaption='Export Data' WHERE FormID= dbo.GetFormGLID('frmDataTransfer') and ControlName = 'btnTransfer'
	End
End


