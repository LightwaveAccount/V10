IF NOT EXISTS (SELECT * FROM SYS.COLUMNS WHERE name = 'IsLightwaveVoucher' AND object_id = object_id('tblGlVoucherTemp'))
BEGIN 
ALTER TABLE dbo.tblGlVoucherTemp ADD
	IsLightwaveVoucher bit NULL
END