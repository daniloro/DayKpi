USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_CONSULTAR_OPERADOR_GESTOR_LIDER
      Objetivo : Consulta todos Operadores Lider do Gestor
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
ALTER PROCEDURE [dbo].[P_CONSULTAR_OPERADOR_GESTOR_LIDER]
	@Login	varchar(8)
AS

BEGIN
	SET NOCOUNT ON	
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	;WITH ope AS (
		SELECT
			LoginAd
		FROM Operador 
		WHERE LoginAd = @Login
	   ),	   
	   equipe as (
		SELECT LoginAd FROM ope 
		UNION ALL
		SELECT			
			O.LoginAd
		FROM equipe
		INNER JOIN Operador O ON O.Gestor = equipe.LoginAd AND O.TipoOperador > 1 AND O.Ativo = 1
	   )
	SELECT		
		O.LoginAd,
		Nome,
		CodTipoEquipe
	FROM equipe	
	INNER JOIN Operador O ON O.LoginAd = equipe.LoginAd	
	ORDER BY Nome

END