If not Exists(SELECT * FROM tblGlconfiguration WHERE config_name='VoucherAutoSorting' )
Begin
	INSERT INTO [dbo].tblGlconfiguration([config_no],[config_name],[config_value])
	VALUES (25,'VoucherAutoSorting','True')
end
