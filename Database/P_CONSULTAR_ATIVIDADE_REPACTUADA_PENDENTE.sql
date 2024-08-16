USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_CONSULTAR_ATIVIDADE_REPACTUADA_PENDENTE
      Objetivo : Consulta as atividades repactuadas pendentes
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
ALTER PROCEDURE [dbo].[P_CONSULTAR_ATIVIDADE_REPACTUADA_PENDENTE]
	@Login			varchar(8),
	@DataConclusao	datetime
AS

BEGIN
	SET NOCOUNT ON	
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	SELECT
		CodHistorico,
		Atividade.NroChamado,
		Atividade.NroAtividade,
		DscChamado,
		Atividade.DscAtividade,
		AtividadeHistorico.Operador,
		Atividade.Grupo,
		DataAnterior,
		AtividadeHistorico.DataFinal,
		NroAtividadeAnterior,
		TipoLancamento,
		Daily,
		(SELECT Count(NroAtividade) FROM AtividadeHistorico AH WHERE AH.NroAtividade = AtividadeHistorico.NroAtividade AND (AH.TipoLancamento = 0 OR AH.TipoLancamento > 6) AND AH.CodHistorico <> AtividadeHistorico.CodHistorico) QtdRepactuacao,
		(SELECT COUNT(NroAtividade) FROM AtividadeHistoricoConfirmacao AHC WHERE AHC.NroAtividade = AtividadeHistorico.NroAtividade AND AHC.DataFinal = AtividadeHistorico.DataAnterior) QtdConfirmacao
	FROM AtividadeHistorico
	INNER JOIN Atividade ON Atividade.NroAtividade = AtividadeHistorico.NroAtividade
	INNER JOIN Chamado ON Chamado.NroChamado = Atividade.NroChamado
	INNER JOIN OperadorGrupo ON OperadorGrupo.Grupo = Atividade.Grupo
	INNER JOIN Operador ON Operador.LoginAd = OperadorGrupo.LoginAd
	WHERE 
		(CodStatus = 0 OR DataConclusao >= @DataConclusao) AND
		TipoLancamento = 0 AND
		LoginAlteracao IS NULL AND
		OperadorGrupo.LoginAd = @Login AND
		(Operador.TipoOperador IN (2,3) OR Operador.Nome = Atividade.Operador)
	ORDER BY 
		Atividade.NroChamado, Atividade.NroAtividade

END