IF not columnproperty (object_id('TblrptGLCashFlowStander'), 'comments', 'AllowsNull') IS NULL
  BEGIN
   alter table TblrptGLCashFlowStander alter column comments varchar(1000) 
  END
--------------------------------------------------------------------------------
IF not columnproperty (object_id('TblrptGLCashFlow'), 'comments', 'AllowsNull') IS NULL
  BEGIN
   alter table TblrptGLCashFlow alter column comments varchar(1000) 
  END
------------------------------------------------------------------------------
IF not columnproperty (object_id('TblrptLedgerDetail'), 'VDescription', 'AllowsNull') IS NULL
  BEGIN
   alter table TblrptLedgerDetail alter column VDescription varchar(1000) 
  END
--------------------------------------------------------------------------
IF not columnproperty (object_id('TblrptAccountLedger'), 'paid_to', 'AllowsNull') IS NULL
  BEGIN
   alter table TblrptAccountLedger alter column paid_to varchar(500) 
  END

