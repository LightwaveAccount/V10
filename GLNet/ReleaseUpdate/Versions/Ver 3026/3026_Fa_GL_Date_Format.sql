If not Exists(SELECT * FROM tblGlconfiguration WHERE config_name='GL_Date_Formate' )
Begin
        INSERT INTO [dbo].tblGlconfiguration([config_no],[config_name],[config_value])
        VALUES (28,'GL_Date_Formate','dd/MM/yyyy')
end
