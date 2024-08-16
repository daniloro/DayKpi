USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_CONSULTAR_ATIVIDADE_HISTORICO
      Objetivo : Obtem as atividades pendentes do Operador
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_CONSULTAR_ATIVIDADE_HISTORICO]
	@NroAtividade varchar(14)
AS

BEGIN
	SET NOCOUNT ON	
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	SELECT
		Daily,
		Operador,
		DataAnterior,
		DataFinal,
		Observacao,
		TipoLancamento,
		NroAtividadeAnterior
	FROM AtividadeHistorico
	WHERE
		NroAtividade = @NroAtividade
	ORDER BY 
		CodHistorico

END