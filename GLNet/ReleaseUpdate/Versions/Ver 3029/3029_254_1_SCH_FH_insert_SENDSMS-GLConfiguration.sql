If not Exists(SELECT * FROM tblGLConfiguration WHERE config_name='SendSMS' )
Begin
	INSERT INTO [dbo].tblGLConfiguration([config_no],[config_name],[config_value])
	VALUES (41,'SendSMS','false')
end


 
 