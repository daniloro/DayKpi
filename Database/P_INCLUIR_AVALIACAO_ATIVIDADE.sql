USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_INCLUIR_AVALIACAO_ATIVIDADE
      Objetivo : Inclui um novo registro na AvaliacaoAtividade
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_INCLUIR_AVALIACAO_ATIVIDADE]
	@NroAtividade			varchar(14),
	@NroChamado				varchar(14),
	@TipoAvaliacao			int,	
	@LoginAd				varchar(8),
	@LoginAvaliador			varchar(8),
	@DataAvaliacao			datetime,
	@Observacao				varchar(max)
AS

BEGIN			

	INSERT INTO AvaliacaoAtividade (NroAtividade, NroChamado, TipoAvaliacao, LoginAd, LoginAvaliador, DataAvaliacao, Observacao)
    VALUES (@NroAtividade, @NroChamado, @TipoAvaliacao, @LoginAd, @LoginAvaliador, @DataAvaliacao, @Observacao) 
	
	SELECT SCOPE_IDENTITY()

END
