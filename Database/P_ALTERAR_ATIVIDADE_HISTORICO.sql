USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_ALTERAR_ATIVIDADE_HISTORICO
      Objetivo : Altera um histório de alteração da atividade
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_ALTERAR_ATIVIDADE_HISTORICO]
	@CodHistorico			int,	
	@TipoLancamento			int,
	@Observacao				varchar(max),	
	@NroAtividadeAnterior	varchar(14),
	@LoginAd				varchar(8)
AS

BEGIN
	SET NOCOUNT ON		

	UPDATE AtividadeHistorico SET 
		TipoLancamento = @TipoLancamento,
		NroAtividadeAnterior = @NroAtividadeAnterior,
		Observacao = @Observacao,
		LoginAlteracao = @LoginAd,
		DataAlteracao = getdate()
    WHERE 
		CodHistorico = @CodHistorico

END
