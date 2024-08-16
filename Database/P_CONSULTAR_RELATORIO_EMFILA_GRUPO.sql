USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_CONSULTAR_RELATORIO_EMFILA_GRUPO
      Objetivo : Consulta Chamados Em Fila Detalhado dos últimos Meses por Grupo
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
ALTER PROCEDURE [dbo].[P_CONSULTAR_RELATORIO_EMFILA_GRUPO]
	@Grupo		varchar(50),
	@DataInicio	datetime,
	@DataFim	datetime
AS

BEGIN
	SET NOCOUNT ON	
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	SELECT				
		Atividade.NroChamado,
		DscChamado,
		NroAtividade,
		Atividade.TipoChamado,
		Atividade.CodPlanejador,
		Atividade.Grupo,
		Atividade.Operador,	
		DataAbertura,
		Atividade.DataFinal,
		DataConclusao,
		CodStatus		
	FROM Atividade
	INNER JOIN Chamado ON Chamado.NroChamado = Atividade.NroChamado
	WHERE
		DataAbertura < @DataFim AND (CodStatus = 0 OR DataConclusao >= @DataInicio) AND		
		Atividade.Grupo = @Grupo
	ORDER BY 
		Operador, DataAbertura

END