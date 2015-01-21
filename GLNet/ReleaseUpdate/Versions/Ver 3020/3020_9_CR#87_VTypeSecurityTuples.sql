----Control of JV-------------------------------------------------
If not Exists(SELECT * FROM tblGLSecurityFormControl WHERE FormID= dbo.GetFormGLID('frmGLVoucher') and ControlName = 'JV')
Begin
	Insert into tblGLSecurityFormControl (FormID, ControlCaption, ControlName)
	Values (dbo.GetFormGLID('frmGLVoucher'),'Block JV', 'JV')
End

----Control of CP--------------------------------------------------
If not Exists(SELECT * FROM tblGLSecurityFormControl WHERE FormID= dbo.GetFormGLID('frmGLVoucher') and ControlName = 'CP')
Begin
	Insert into tblGLSecurityFormControl (FormID, ControlCaption, ControlName)
	Values (dbo.GetFormGLID('frmGLVoucher'),'Block CPV', 'CP')
End

-----Control of CR------------------------------------------------
If not Exists(SELECT * FROM tblGLSecurityFormControl WHERE FormID= dbo.GetFormGLID('frmGLVoucher') and ControlName = 'CR')
Begin
	Insert into tblGLSecurityFormControl (FormID, ControlCaption, ControlName)
	Values (dbo.GetFormGLID('frmGLVoucher'),'Block CRV', 'CR')
End

-----Control of BP--------------------------------------------
If not Exists(SELECT * FROM tblGLSecurityFormControl WHERE FormID= dbo.GetFormGLID('frmGLVoucher') and ControlName = 'BP')
Begin
	Insert into tblGLSecurityFormControl (FormID, ControlCaption, ControlName)
	Values (dbo.GetFormGLID('frmGLVoucher'),'Block BPV', 'BP')
End

----Control of BR---------------------------------------------
If not Exists(SELECT * FROM tblGLSecurityFormControl WHERE FormID= dbo.GetFormGLID('frmGLVoucher') and ControlName = 'BR')
Begin
	Insert into tblGLSecurityFormControl (FormID, ControlCaption, ControlName)
	Values (dbo.GetFormGLID('frmGLVoucher'),'Block BRV', 'BR')
End


----Control of SV---------------------------------------------
If not Exists(SELECT * FROM tblGLSecurityFormControl WHERE FormID= dbo.GetFormGLID('frmGLVoucher') and ControlName = 'SV')
Begin
	Insert into tblGLSecurityFormControl (FormID, ControlCaption, ControlName)
	Values (dbo.GetFormGLID('frmGLVoucher'),'Block SV', 'SV')
End

----Control of PV---------------------------------------------
If not Exists(SELECT * FROM tblGLSecurityFormControl WHERE FormID= dbo.GetFormGLID('frmGLVoucher') and ControlName = 'PV')
Begin
	Insert into tblGLSecurityFormControl (FormID, ControlCaption, ControlName)
	Values (dbo.GetFormGLID('frmGLVoucher'),'Block PV', 'PV')
End