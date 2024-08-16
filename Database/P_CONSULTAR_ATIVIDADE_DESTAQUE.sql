USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_CONSULTAR_ATIVIDADE_DESTAQUE
      Objetivo : Obtem os destaques da avalia��o
      Analista : Lucas J
      Data     : 04/01/2021
      *** Hist�rico de Altera��es ****************************************
      Item  Data        Analista		Descri��o
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