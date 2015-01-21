--==============================================================================================
--Add Controls for:			frmProfitAndLossMonthWise
--==============================================================================================
If not Exists(SELECT * FROM tblGLSecurityForm WHERE Form_Name='frmProfitAndLossMonthWise')
Begin
	INSERT INTO [dbo].[tblGLSecurityForm] ([Form_Name],[Form_Label])
	VALUES ('frmProfitAndLossMonthWise','Profit and Loss Statement Month Wise')

End
		If not Exists(SELECT * FROM dbo.tblGLSecurityFormControl WHERE FormID= dbo.GetFormGLID('frmProfitAndLossMonthWise') and ControlName = 'View')
		Begin
			Insert into tblGLSecurityFormControl (FormID, ControlCaption, ControlName)
			Values (dbo.GetFormGLID('frmProfitAndLossMonthWise'),'View', 'View')
		End


		If not Exists(SELECT * FROM tblGLSecurityFormControl WHERE FormID= dbo.GetFormGLID('frmProfitAndLossMonthWise') and ControlName = 'btnPrint')
		Begin
			Insert into tblGLSecurityFormControl (FormID, ControlCaption, ControlName)
			Values (dbo.GetFormGLID('frmProfitAndLossMonthWise'),'Print', 'btnPrint')
		End

		If not Exists(SELECT * FROM tblGLSecurityFormControl WHERE FormID= dbo.GetFormGLID('frmProfitAndLossMonthWise') and ControlName = 'btnExport')
		Begin
			Insert into tblGLSecurityFormControl (FormID, ControlCaption, ControlName)
			Values (dbo.GetFormGLID('frmProfitAndLossMonthWise'),'Export To Excel', 'btnExport')
		End

--==============================================================================================
--==============================================================================================