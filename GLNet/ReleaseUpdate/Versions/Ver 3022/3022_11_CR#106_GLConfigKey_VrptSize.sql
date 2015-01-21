If not Exists(SELECT * FROM tblGlconfiguration WHERE config_name='voucher_report_size' )
Begin
	INSERT INTO [dbo].tblGlconfiguration([config_no],[config_name],[config_value])
	VALUES (142,'voucher_report_size','Long')
end
