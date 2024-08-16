USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_CONSULTAR_AVALIACAO_DESTAQUE
      Objetivo : Lista os Destaques das Avaliações
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
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