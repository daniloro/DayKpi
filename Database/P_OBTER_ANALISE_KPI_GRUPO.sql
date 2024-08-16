USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_OBTER_ANALISE_KPI_GRUPO
      Objetivo : Obtem a analise do KPI por Grupo
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_OBTER_ANALISE_KPI_GRUPO]
	@DataAnalise	datetime,
	@Grupo			varchar(50),
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
		Grupo = @Grupo AND
		CodPergunta = @CodPergunta
	
END
