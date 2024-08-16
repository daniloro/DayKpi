USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_OBTER_GESTOR
      Objetivo : Obtem os dados do Gestor
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_OBTER_GESTOR]
	@Login			varchar(8)
AS

BEGIN
	SET NOCOUNT ON	
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
	
	SELECT
		O.Nome,
		O.LoginAd,		
		O.Gestor,
		G.Nome AS NomeGestor
	FROM Operador O
	LEFT OUTER JOIN Operador G ON (G.LoginAd = O.Gestor)
	WHERE 
		O.LoginAd = @Login
	
END