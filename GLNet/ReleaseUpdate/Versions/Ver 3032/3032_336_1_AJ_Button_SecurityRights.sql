If not Exists(SELECT * FROM tblGLSecurityFormControl WHERE FormID= dbo.GetFormGLID('frmGLVoucher') and ControlName = 'btnPrintInvoice')
		Begin
			Insert into tblGLSecurityFormControl (FormID, ControlCaption, ControlName)
			Values (dbo.GetFormGLID('frmGLVoucher'),'Print PO or SI', 'btnPrintInvoice')
		End