IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].vwGLPLNotesPrevDtl') AND type in (N'V'))
drop  view [dbo].vwGLPLNotesPrevDtl
