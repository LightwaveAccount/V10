
IF NOT columnproperty (object_id('TblrptAccountLedger'), 'comments', 'AllowsNull') IS NULL
  Begin
	ALTER TABLE TblrptAccountLedger ALTER COLUMN comments varchar(500)
End


