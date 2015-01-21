IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].vwGLPLNotesCurrentDtl') AND type in (N'V'))
drop  view [dbo].vwGLPLNotesCurrentDtl
