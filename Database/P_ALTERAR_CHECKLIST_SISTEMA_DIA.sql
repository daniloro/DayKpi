USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_ALTERAR_CHECKLIST_SISTEMA_DIA
      Objetivo : Altera um registro no checklist di�rio do sistema
      Analista : Lucas J
      Data     : 04/01/2021
      *** Hist�rico de Altera��es ****************************************
      Item  Data        Analista		Descri��o
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_ALTERAR_CHECKLIST_SISTEMA_DIA]
	@CodCheckList			int,		
	@LoginAd				varchar(8),
	@CodStatus				int
AS

BEGIN
	SET NOCOUNT ON		

	UPDATE CheckListSistemaDia 
		SET LoginAd = @LoginAd,
        CodStatus = @CodStatus,
        DataAlteracao = getdate()
    WHERE 
		CodCheckList = @CodCheckList

END
