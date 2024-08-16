USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_CONSULTAR_ATIVIDADE_DESTAQUE
      Objetivo : Obtem os destaques da avaliação
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_CONSULTAR_ATIVIDADE_DESTAQUE]
	@CodAvaliacao	int
AS

BEGIN
	SET NOCOUNT ON	
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	SELECT
		AAD.CodAvaliacao,
		AAD.CodDestaque,
		AD.DscDestaque,
		AD.Ponderacao
	FROM AvaliacaoAtividadeDestaque AAD
	INNER JOIN AvaliacaoDestaque AD ON AD.CodDestaque = AAD.CodDestaque
	WHERE 
		CodAvaliacao = @CodAvaliacao

END