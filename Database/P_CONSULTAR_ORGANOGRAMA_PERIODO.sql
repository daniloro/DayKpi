USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_CONSULTAR_ORGANOGRAMA_PERIODO
      Objetivo : Lista o organograma do login no periodo selecionado
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
ALTER PROCEDURE [dbo].[P_CONSULTAR_ORGANOGRAMA_PERIODO]
	@DataOrganograma	datetime,
	@Login				varchar(8)
AS

BEGIN
	SET NOCOUNT ON	
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	;WITH ope AS (
		SELECT
			1 As Nivel, 
			Gestor, 
			LoginAd
		FROM Organograma 
		WHERE DataOrganograma = @DataOrganograma AND LoginAd = @Login
	   ),
	   gestao as (
		SELECT * FROM ope 
		UNION ALL
		SELECT
			Nivel - 1,
			O.Gestor,
			O.LoginAd
		FROM gestao
		INNER JOIN Organograma O ON O.DataOrganograma = @DataOrganograma AND O.LoginAd = gestao.Gestor
	   ),
	   equipe as (
		SELECT * FROM ope 
		UNION ALL
		SELECT
			Nivel + 1,
			O.Gestor,
			O.LoginAd
		FROM equipe
		INNER JOIN Organograma O ON O.DataOrganograma = @DataOrganograma AND O.Gestor = equipe.LoginAd
	   ),
	   consolidado as (
		SELECT * FROM gestao 
		UNION
		SELECT * FROM equipe 
	   )
	SELECT
		Nivel,
		consolidado.LoginAd,
		O.Nome,
		consolidado.Gestor,
		G.Nome NomeGestor,
		A.Grupo 
	FROM consolidado
	INNER JOIN Organograma A ON A.DataOrganograma = @DataOrganograma AND A.LoginAd = consolidado.LoginAd
	LEFT OUTER JOIN Operador O ON O.LoginAd = A.LoginAd
	LEFT OUTER JOIN Operador G ON G.LoginAd = A.Gestor
	ORDER BY Nivel, NomeGestor, Grupo, O.Nome

END



