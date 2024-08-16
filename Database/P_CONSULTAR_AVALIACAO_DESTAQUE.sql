USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_CONSULTAR_AVALIACAO_DESTAQUE
      Objetivo : Lista os Destaques das Avalia��es
      Analista : Lucas J
      Data     : 04/01/2021
      *** Hist�rico de Altera��es ****************************************
      Item  Data        Analista		Descri��o
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_CONSULTAR_AVALIACAO_DESTAQUE]
AS

BEGIN
	SET NOCOUNT ON	
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	SELECT
		CodDestaque,
		DscDestaque
    FROM AvaliacaoDestaque

END