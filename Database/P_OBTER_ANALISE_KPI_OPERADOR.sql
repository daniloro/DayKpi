USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_OBTER_ANALISE_KPI_OPERADOR
      Objetivo : Obtem uma analise por operador
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_OBTER_ANALISE_KPI_OPERADOR]
	@DataAnalise	datetime,
	@LoginOperador	varchar(8),
	@CodPergunta	int
AS

BEGIN
	SET NOCOUNT ON	
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
	
	SELECT		
		CodAnalise,
		DataAnalise,
		LoginOperador,
		CodPergunta,		
		DscAnalise,
		Concluido
	FROM AnaliseKPI	
	WHERE 
		DataAnalise = @DataAnalise AND
		LoginOperador = @LoginOperador AND
		CodPergunta = @CodPergunta
	
END
