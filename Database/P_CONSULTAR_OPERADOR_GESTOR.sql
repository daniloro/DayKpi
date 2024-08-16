USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_CONSULTAR_OPERADOR_GESTOR
      Objetivo : Consulta todos Operadores do Gestor
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
ALTER PROCEDURE [dbo].[P_CONSULTAR_OPERADOR_GESTOR]
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
		INNER JOIN Operador O ON O.Gestor = equipe.LoginAd AND O.TipoOperador > 0 AND O.Ativo = 1
	   )
	SELECT		
		O.LoginAd,
		Nome,		
		Email,
		TipoOperador,
		OG.Grupo
	FROM equipe
	INNER JOIN OperadorGrupo OG ON OG.LoginAd = equipe.LoginAd AND OG.Principal = 1
	INNER JOIN Operador O ON O.LoginAd = equipe.LoginAd
	WHERE
		O.LoginAd <> @Login
	ORDER BY Nome

END