If not Exists(SELECT * FROM tblGLConfiguration WHERE config_name='Desc_in_Vouchers' )
Begin
	INSERT INTO [dbo].[tblGLConfiguration]([config_no],[config_name],[config_value])
	VALUES (31,'Desc_in_Vouchers','False')
end