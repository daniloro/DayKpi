USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_CONSULTAR_RELATORIO_CONCLUIDO_OPERADOR
      Objetivo : Consulta Chamados Em Fila Detalhado dos últimos Meses por Grupo
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
ALTER PROCEDURE [dbo].[P_CONSULTAR_RELATORIO_CONCLUIDO_OPERADOR]
	@Login		varchar(8),
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
		Atividade.Grupo,
		Atividade.Operador,	
		DataAbertura,
		DataConclusao,
		CodStatus		
	FROM Atividade
	INNER JOIN Chamado ON Chamado.NroChamado = Atividade.NroChamado
	INNER JOIN Operador ON Operador.Nome = Atividade.Operador	
	WHERE
		CodStatus = 1 AND
		DataConclusao >= @DataInicio AND DataConclusao < @DataFim AND		
		LoginAd = @Login

END