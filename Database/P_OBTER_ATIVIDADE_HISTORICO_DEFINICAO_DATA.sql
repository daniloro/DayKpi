USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_OBTER_ATIVIDADE_HISTORICO_DEFINICAO_DATA
      Objetivo : Verifica se a atividade já tem definição de data
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_OBTER_ATIVIDADE_HISTORICO_DEFINICAO_DATA]
	@NroAtividade varchar(14)
AS

BEGIN
	SET NOCOUNT ON	
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	SELECT
	1
	FROM AtividadeHistorico
	WHERE 
		NroAtividade = @NroAtividade 
		AND TipoLancamento = 2

END
