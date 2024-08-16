USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_OBTER_PERGUNTA_KPI
      Objetivo : Obtem os dados do Chamado
      Analista : Lucas J
      Data     : 04/01/2021
      *** Hist�rico de Altera��es ****************************************
      Item  Data        Analista		Descri��o
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
