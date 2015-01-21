----Control of Voucher Date------------------------------------------
If not Exists(SELECT * FROM tblGLSecurityFormControl WHERE FormID= dbo.GetFormGLID('frmGLVoucher') and ControlName = 'dtpVoucherDate')
Begin
	Insert into tblGLSecurityFormControl (FormID, ControlCaption, ControlName)
	Values (dbo.GetFormGLID('frmGLVoucher'),'Block Voucher Date', 'dtpVoucherDate')
End

