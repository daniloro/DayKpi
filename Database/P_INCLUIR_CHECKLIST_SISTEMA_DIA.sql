USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_INCLUIR_CHECKLIST_SISTEMA_DIA
      Objetivo : Inclui um novo registro no checklist diário do sistema
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_INCLUIR_CHECKLIST_SISTEMA_DIA]
	@CodItem		int,
	@Tipo			int,
	@DataItem		datetime,
	@LoginAd		varchar(8),
	@CodStatus		int,
	@Observacao		varchar(100)	
AS

BEGIN
	SET NOCOUNT ON		

	INSERT INTO CheckListSistemaDia (CodItem, Tipo, DataItem, LoginAd, CodStatus, Observacao, DataAlteracao)
    VALUES (@CodItem, @Tipo, @DataItem, @LoginAd, @CodStatus, @Observacao, getdate())

END
