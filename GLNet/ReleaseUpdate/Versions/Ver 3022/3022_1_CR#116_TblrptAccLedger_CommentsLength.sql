IF not columnproperty (object_id('TblrptAccountLedger'), 'comments', 'AllowsNull') IS NULL
  BEGIN
   alter table TblrptAccountLedger alter column comments varchar(1500) Null
  END

