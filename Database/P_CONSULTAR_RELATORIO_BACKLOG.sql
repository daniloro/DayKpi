USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_CONSULTAR_RELATORIO_BACKLOG
      Objetivo : Consulta Consolidada do BackLog da equipe
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
ALTER PROCEDURE [dbo].[P_CONSULTAR_RELATORIO_BACKLOG]
	@Login			varchar(8),
	@DataInicio		datetime,
	@DataFim		datetime
AS

BEGIN
	SET NOCOUNT ON	
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	SELECT				
		NroChamado,		
		TipoChamado,
		DscAtividade AS DscChamado,
		Atividade.Grupo,
		Atividade.Operador,	
		'' AS DscPlanejador,
		DataAbertura,
		DataConclusao,
		CodStatus		
	FROM Atividade
	INNER JOIN OperadorGrupo ON OperadorGrupo.Grupo = Atividade.Grupo AND EntradaManual = 0
	WHERE
		DataAbertura < @DataFim AND (CodStatus = 0 OR DataConclusao >= @DataInicio) AND
		TipoChamado IN ('Incidente', 'Req. Simples') AND
		OperadorGrupo.LoginAd = @Login		

	UNION ALL

	SELECT
		Chamado.NroChamado,
		'Req. Complexa',
		Chamado.DscChamado,
		Chamado.Grupo,
		Chamado.Analista,
		DscPlanejador,
		DataInicio,
		DataImplementacao,
		CodStatusChamado
	FROM Chamado
	INNER JOIN OperadorGrupo ON OperadorGrupo.Grupo = Chamado.Grupo AND EntradaManual = 0
	LEFT OUTER JOIN Planejador ON Planejador.CodPlanejador = Chamado.CodPlanejador
	WHERE
		DataInicio < @DataFim AND (CodStatusChamado = 0 OR DataImplementacao >= @DataInicio) AND
		TipoChamado = 'Req. Complexa' AND
		OperadorGrupo.LoginAd = @Login
	ORDER BY
		Grupo, Operador, TipoChamado, DataAbertura

END

