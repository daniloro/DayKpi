USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_CONSULTAR_PONTO_MENSAL
      Objetivo : Consultar o resumo do Ponto Mensal da Equipe
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
ALTER PROCEDURE [dbo].[P_CONSULTAR_PONTO_MENSAL]
	@Login			varchar(8),
	@DataPonto		datetime	
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
		CodPontoMensal,
		DataPonto,
		O.LoginAd,
		Nome,
		Atraso,
		CinquentaPorcento,
		CemPorcento,
		Noturno,
		Observacao
	FROM equipe
	INNER JOIN OperadorGrupo OG ON OG.LoginAd = equipe.LoginAd AND OG.Principal = 1
	INNER JOIN Operador O ON O.LoginAd = equipe.LoginAd
	LEFT OUTER JOIN PontoMensal PM ON PM.LoginAd = O.LoginAd AND DataPonto = @DataPonto
	WHERE
		O.LoginAd <> @Login
	ORDER BY Nome

END
