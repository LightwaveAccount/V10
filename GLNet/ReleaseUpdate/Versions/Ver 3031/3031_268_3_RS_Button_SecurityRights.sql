If not Exists(SELECT * FROM tblGLSecurityFormControl WHERE FormID= dbo.GetFormID('frmGLVoucher') and ControlName = 'btnPrintInvoice')
		Begin
			Insert into tblGLSecurityFormControl (FormID, ControlCaption, ControlName)
			Values (dbo.GetFormID('frmGLVoucher'),'Print PO or SI', 'btnPrintInvoice')
		End