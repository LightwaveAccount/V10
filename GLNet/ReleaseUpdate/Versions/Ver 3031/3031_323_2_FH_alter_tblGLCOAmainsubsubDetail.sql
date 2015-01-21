IF NOT EXISTS (SELECT * FROM SYS.COLUMNS WHERE name = 'cheque_id' AND object_id = object_id('tblGlCOAMainSubSubDetail'))
BEGIN 

	ALTER TABLE tblGlCOAMainSubSubDetail
	ADD cheque_id int NULL
END