USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_OBTER_ANALISE_KPI_GESTOR
      Objetivo : Obtem a analise do KPI por Gestor
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_OBTER_ANALISE_KPI_GESTOR]
	@DataAnalise	datetime,
	@Login			varchar(8),
	@CodPergunta	int
AS

BEGIN
	SET NOCOUNT ON	
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
	
	SELECT		
		CodAnalise,
		DataAnalise,
		Grupo,
		CodPergunta,		
		DscAnalise,
		Concluido
	FROM AnaliseKPI	
	WHERE 
		DataAnalise = @DataAnalise AND
		LoginAd = @Login AND
		CodPergunta = @CodPergunta
	
END
