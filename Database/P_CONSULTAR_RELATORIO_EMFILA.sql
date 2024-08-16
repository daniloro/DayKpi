USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_CONSULTAR_RELATORIO_EMFILA
      Objetivo : Consulta Consolidada dos Chamados Em Fila da equipe
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_CONSULTAR_RELATORIO_EMFILA]
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
		DscAtividade,
		Atividade.TipoChamado,
		Atividade.Grupo,
		Atividade.Operador,
		Atividade.CodPlanejador,
		DataAbertura,
		Atividade.DataFinal,
		DataConclusao,
		CodStatus	
	FROM Atividade
	INNER JOIN Chamado ON Chamado.NroChamado = Atividade.NroChamado
	INNER JOIN OperadorGrupo ON OperadorGrupo.Grupo = Atividade.Grupo	
	WHERE
		DataAbertura < @DataFim AND (CodStatus = 0 OR DataConclusao >= @DataInicio) AND		
		OperadorGrupo.LoginAd = @Login
	ORDER BY
		Atividade.Grupo

END