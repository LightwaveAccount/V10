if exists (select * from tblGLSecurityFormRight where Form_Id in (select form_id from tblGLSecurityForm where Form_name='frmRptGLTrialBalance'))
begin
delete from tblGLSecurityFormRight where Form_Id in (select form_id from tblGLSecurityForm where Form_name='frmRptGLTrialBalance')
end
if exists (select * from tblGLSecurityFormControl where FormId in (select form_id from tblGLSecurityForm where Form_name='frmRptGLTrialBalance'))
begin
delete from tblGLSecurityFormControl where formId in (select form_id from tblGLSecurityForm where Form_name='frmRptGLTrialBalance')
end
if exists (select * from tblGLSecurityForm where Form_name='frmRptGLTrialBalance')
begin
delete from tblGLSecurityForm where form_name='frmRptGLTrialBalance'
end
If not Exists(SELECT * FROM tblGLSecurityForm WHERE Form_Name='frmRptGLTrialBalance')
Begin
	INSERT INTO [dbo].[tblGLSecurityForm] ([Form_Name],[Form_Label])
	VALUES ('frmRptGLTrialBalance','Trial Balance')
End
If not Exists(SELECT * FROM dbo.tblGLSecurityFormControl WHERE FormID= dbo.GetFormGLID('frmRptGLTrialBalance') and ControlName = 'View')
Begin
	Insert into tblGLSecurityFormControl (FormID, ControlCaption, ControlName)
	Values (dbo.GetFormGLID('frmRptGLTrialBalance'),'View', 'View')
End
If not Exists(SELECT * FROM dbo.tblGLSecurityFormControl WHERE FormID= dbo.GetFormGLID('frmRptGLTrialBalance') and ControlName = 'btnPrint')
Begin
	Insert into tblGLSecurityFormControl (FormID, ControlCaption, ControlName)
	Values (dbo.GetFormGLID('frmRptGLTrialBalance'),'Print', 'btnPrint')
End


