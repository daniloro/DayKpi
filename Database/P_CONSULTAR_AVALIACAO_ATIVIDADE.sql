USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_CONSULTAR_AVALIACAO_ATIVIDADE
      Objetivo : Obtem os detalhes da Atividade
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_CONSULTAR_AVALIACAO_ATIVIDADE]
	@NroAtividade varchar(14)
AS

BEGIN
	SET NOCOUNT ON	
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	SELECT
		Atividade.NroChamado,
		Atividade.NroAtividade,
		Chamado.DscChamado,
		Atividade.TipoChamado,
		Atividade.DscAtividade,
		ISNULL(Planejador.DscPlanejador, 'Outros') DscPlanejador,
		Operador.Nome,
		Operador.LoginAd,
		Atividade.DataConclusao,
		CASE WHEN Atividade.TipoChamado = 'Atividade' THEN ISNULL(Planejador.Ponderacao, 3) ELSE 10 END AS Ponderacao,
		(SELECT COUNT(NroAtividade) FROM AtividadeHistoricoConfirmacao AHC WHERE AHC.NroAtividade = Atividade.NroAtividade AND AHC.LoginAd = Operador.LoginAd) QtdConfirmacao,
		(SELECT COUNT(NroAtividade) FROM AtividadeHistorico AH WHERE AH.NroAtividade = Atividade.NroAtividade AND AH.Operador = Operador.Nome AND (AH.TipoLancamento = 0 OR AH.TipoLancamento > 6)) QtdRepactuacao,
		(SELECT COUNT(NroAtividade) FROM AtividadeHistorico AH WHERE AH.NroAtividade = Atividade.NroAtividade AND AH.Operador = Operador.Nome AND AH.TipoLancamento = 0) QtdRepactuacaoSemJustificativa,
		AvaAuto.CodAvaliacao CodAvaliacaoAuto,
		AvaAuto.Observacao ObservacaoAuto,
		AvaGestor.CodAvaliacao,
		AvaGestor.Observacao
	FROM Atividade
	INNER JOIN Chamado ON Chamado.NroChamado = Atividade.NroChamado
	INNER JOIN Operador ON Operador.Nome = Atividade.Operador
	LEFT OUTER JOIN AvaliacaoAtividade AvaAuto ON AvaAuto.NroAtividade = Atividade.NroAtividade AND AvaAuto.LoginAd = Operador.LoginAd AND AvaAuto.TipoAvaliacao = 1
	LEFT OUTER JOIN AvaliacaoAtividade AvaGestor ON AvaGestor.NroAtividade = Atividade.NroAtividade AND AvaGestor.LoginAd = Operador.LoginAd AND AvaGestor.TipoAvaliacao = 2
	LEFT OUTER JOIN Planejador ON Planejador.CodPlanejador = Atividade.CodPlanejador
	WHERE 
		Atividade.NroAtividade = @NroAtividade

END