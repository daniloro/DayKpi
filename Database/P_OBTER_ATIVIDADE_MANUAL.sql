USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_OBTER_ATIVIDADE_MANUAL
      Objetivo : Obtem os dados da Atividade para alteração Manual
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_OBTER_ATIVIDADE_MANUAL]
	@NroAtividade varchar(14)
AS

BEGIN
	SET NOCOUNT ON	
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	IF EXISTS(SELECT NroAtividade FROM AtividadeManual WHERE NroAtividade = @NroAtividade)	
	BEGIN
		SELECT
			Nome,
			Chamado.NroChamado,
			DscChamado,
			AM.NroAtividade,
			ISNULL(DscAtividade, DscManual) DscAtividade,
			AM.DataFinal,
			(SELECT COUNT(NroAtividade) FROM AtividadeHistorico AH WHERE AH.NroAtividade = AM.NroAtividade AND AH.TipoLancamento > 6) QtdRepactuacao,
			CodStatus,
			DataConclusao
		FROM AtividadeManual AM	
		INNER JOIN Operador ON Operador.LoginAd = AM.LoginAd
		LEFT OUTER JOIN Atividade A ON A.NroAtividade = AM.NroAtividade
		LEFT OUTER JOIN Chamado ON Chamado.NroChamado = A.NroChamado
		WHERE 
			AM.NroAtividade = @NroAtividade
	END
	ELSE
	BEGIN
		SELECT
			Operador Nome,
			Chamado.NroChamado,
			DscChamado,
			NroAtividade,
			DscAtividade,
			Atividade.DataFinal,
			(SELECT COUNT(NroAtividade) FROM AtividadeHistorico AH WHERE AH.NroAtividade = Atividade.NroAtividade AND AH.TipoLancamento > 6) QtdRepactuacao,
			CodStatus,
			DataConclusao
		FROM Atividade
		INNER JOIN Chamado ON Chamado.NroChamado = Atividade.NroChamado		
		WHERE 
			NroAtividade = @NroAtividade
	END
END