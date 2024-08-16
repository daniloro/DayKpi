USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_OBTER_PERGUNTA_KPI
      Objetivo : Obtem os dados do Chamado
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_OBTER_PERGUNTA_KPI]
	@CodPergunta int
AS

BEGIN
	SET NOCOUNT ON	
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
	
	SELECT		
		CodPergunta,
		DscPergunta
	FROM PerguntaKPI	
	WHERE 
		CodPergunta = @CodPergunta
	
END
