IF NOT EXISTS (SELECT * FROM SYS.COLUMNS WHERE name = 'IsLightwaveVoucher' AND object_id = object_id('tblGlVoucher'))
BEGIN 
ALTER TABLE dbo.tblGlVoucher ADD
	IsLightwaveVoucher bit NULL
END