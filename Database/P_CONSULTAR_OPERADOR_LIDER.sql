USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_CONSULTAR_OPERADOR_LIDER
      Objetivo : Consulta todos Lideres do sistema
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
ALTER PROCEDURE [dbo].[P_CONSULTAR_OPERADOR_LIDER]
	@Login	varchar(8)
AS

BEGIN
	SET NOCOUNT ON	
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
	
	SELECT		
		LoginAd,
		Nome,
		CodTipoEquipe
	FROM Operador	
	WHERE
		TipoOperador > 1
	ORDER BY Nome

END