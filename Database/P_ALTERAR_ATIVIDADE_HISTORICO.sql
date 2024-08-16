USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_ALTERAR_ATIVIDADE_HISTORICO
      Objetivo : Altera um hist�rio de altera��o da atividade
      Analista : Lucas J
      Data     : 04/01/2021
      *** Hist�rico de Altera��es ****************************************
      Item  Data        Analista		Descri��o
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
