USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_CONSULTAR_ATIVIDADE_PENDENTE
      Objetivo : Obtem as atividades pendentes do Operador
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_CONSULTAR_ATIVIDADE_PENDENTE]
	@Login varchar(8)
AS

BEGIN
	SET NOCOUNT ON	
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	SELECT
		Atividade.Grupo,
		Chamado.NroChamado,
		DscChamado,
		NroAtividade,
		DscAtividade,
		Operador,
		Atividade.DataFinal,
		DataAbertura,
		(SELECT Count(NroAtividade) FROM AtividadeHistorico AH WHERE AH.NroAtividade = Atividade.NroAtividade AND (AH.TipoLancamento = 0 OR AH.TipoLancamento > 6)) QtdRepactuacao
	FROM Atividade
	INNER JOIN Chamado ON Chamado.NroChamado = Atividade.NroChamado
	INNER JOIN OperadorGrupo ON OperadorGrupo.Grupo = Atividade.Grupo
	WHERE 
		CodStatus = 0 AND
		OperadorGrupo.LoginAd = @Login
	ORDER BY 
		Operador, DataFinal

END