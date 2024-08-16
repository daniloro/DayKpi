USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_OBTER_OPERADOR
      Objetivo : Obtem os dados do Operador
      Analista : Lucas J
      Data     : 04/01/2021
      *** Hist�rico de Altera��es ****************************************
      Item  Data        Analista		Descri��o
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_OBTER_OPERADOR]
	@Login	 varchar(8)
AS

BEGIN
	SET NOCOUNT ON	
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	SELECT
		LoginAd,
		Nome,		
		TipoOperador,
		CodTipoEquipe
	FROM Operador	
	WHERE 
		LoginAd = @Login

END
