USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_CONSULTAR_AVALIACAO_ATIVIDADE_PONDERACAO_MES_EQUIPE
      Objetivo : Obtem as atividades concluídas do mês com sua ponderação
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_CONSULTAR_AVALIACAO_ATIVIDADE_PONDERACAO_MES_EQUIPE]
	@Login		varchar(8),
	@DataInicio	datetime,
	@DataFim	datetime
AS

BEGIN
	SET NOCOUNT ON	
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	SELECT
		Operador.LoginAd,
		CASE WHEN Atividade.TipoChamado = 'Atividade' THEN ISNULL(Planejador.Ponderacao, 3) ELSE 10 END AS Ponderacao,
		(SELECT COUNT(NroAtividade) FROM AtividadeHistoricoConfirmacao AHC WHERE AHC.NroAtividade = Atividade.NroAtividade AND AHC.LoginAd = Operador.LoginAd) QtdConfirmacao,
		(SELECT COUNT(NroAtividade) FROM AtividadeHistorico AH WHERE AH.NroAtividade = Atividade.NroAtividade AND AH.Operador = Operador.Nome AND AH.TipoLancamento = 0) QtdRepactuacaoSemJustificativa,
		AvaAuto.CodAvaliacao CodAvaliacaoAuto,
		AvaGestor.CodAvaliacao,
		(SELECT SUM(Ponderacao) FROM AvaliacaoAtividadeDestaque AAD INNER JOIN AvaliacaoDestaque AD ON AD.CodDestaque = AAD.CodDestaque WHERE CodAvaliacao = AvaGestor.CodAvaliacao) NotaAvaliacao
	FROM Atividade
	INNER JOIN Chamado ON Chamado.NroChamado = Atividade.NroChamado
	INNER JOIN Operador ON Operador.Nome = Atividade.Operador
	INNER JOIN OperadorGrupo OG1 ON OG1.LoginAd = Operador.LoginAd AND Principal = 1
	INNER JOIN OperadorGrupo OG2 ON OG2.Grupo = OG1.Grupo
	LEFT OUTER JOIN AvaliacaoAtividade AvaAuto ON AvaAuto.NroAtividade = Atividade.NroAtividade AND AvaAuto.LoginAd = Operador.LoginAd AND AvaAuto.TipoAvaliacao = 1
	LEFT OUTER JOIN AvaliacaoAtividade AvaGestor ON AvaGestor.NroAtividade = Atividade.NroAtividade AND AvaGestor.LoginAd = Operador.LoginAd AND AvaGestor.TipoAvaliacao = 2
	LEFT OUTER JOIN Planejador ON Planejador.CodPlanejador = Atividade.CodPlanejador
	WHERE 
		CodStatus = 1 AND
		OG2.LoginAd = @Login AND
		DataConclusao >= @DataInicio AND DataConclusao < @DataFim

END