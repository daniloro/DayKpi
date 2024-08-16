USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_ALTERAR_GESTOR
      Objetivo : Altera o gestor do operador
      Analista : Lucas J
      Data     : 04/01/2021
      *** Hist�rico de Altera��es ****************************************
      Item  Data        Analista		Descri��o
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_ALTERAR_GESTOR]	
	@Login			varchar(8),
	@Gestor			varchar(8) = null
AS

BEGIN
	SET NOCOUNT ON

	UPDATE Operador SET 
		Gestor = @Gestor		
	WHERE 
		LoginAd = @Login

END
