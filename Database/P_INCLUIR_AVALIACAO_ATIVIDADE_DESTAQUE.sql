USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_INCLUIR_AVALIACAO_ATIVIDADE_DESTAQUE
      Objetivo : Inclui um novo destaque para a Atividade
      Analista : Lucas J
      Data     : 04/01/2021
      *** Hist�rico de Altera��es ****************************************
      Item  Data        Analista		Descri��o
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_INCLUIR_AVALIACAO_ATIVIDADE_DESTAQUE]	
	@CodAvaliacao			int,	
	@CodDestaque			int
AS

BEGIN
	SET NOCOUNT ON		

	INSERT INTO AvaliacaoAtividadeDestaque (CodAvaliacao, CodDestaque)
		VALUES (@CodAvaliacao, @CodDestaque)

END
