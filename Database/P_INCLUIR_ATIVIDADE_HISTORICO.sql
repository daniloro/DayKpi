USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_INCLUIR_ATIVIDADE_HISTORICO
      Objetivo : Inclui um novo histório de alteração da atividade
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_INCLUIR_ATIVIDADE_HISTORICO]
	@NroChamado				varchar(14),
	@NroAtividade			varchar(14),
	@Operador				varchar(100),
	@DataAnterior			datetime,
	@DataFinal				datetime,
	@Observacao				varchar(max),
	@TipoLancamento			int,
	@NroAtividadeAnterior	varchar(14),
	@LoginAd				varchar(8)
AS

BEGIN
	SET NOCOUNT ON		

	INSERT INTO AtividadeHistorico (NroChamado, NroAtividade, Daily, Operador, DataAnterior, DataFinal, TipoEntrada, TipoLancamento, NroAtividadeAnterior, Observacao, LoginAlteracao, DataAlteracao)
	VALUES (@NroChamado, @NroAtividade, CONVERT(VARCHAR(10), GETDATE(), 120), @Operador, @DataAnterior, @DataFinal, 'M', @TipoLancamento, @NroAtividadeAnterior, @Observacao, @LoginAd, getdate())

END
