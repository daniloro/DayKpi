USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_CONSULTAR_ORGANOGRAMA
      Objetivo : Lista o organograma do login selecionado
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
ALTER PROCEDURE [dbo].[P_CONSULTAR_ORGANOGRAMA]
	@Login varchar(8)
AS

BEGIN
	SET NOCOUNT ON	
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	;WITH ope AS (
		SELECT
			1 As Nivel, 
			Gestor, 
			LoginAd,
			Nome
		FROM Operador 
		WHERE LoginAd = @Login
	   ),
	   gestao as (
		SELECT * FROM ope 
		UNION ALL
		SELECT
			Nivel - 1,
			O.Gestor,
			O.LoginAd,
			O.Nome
		FROM gestao
		INNER JOIN Operador O ON O.LoginAd = gestao.Gestor AND O.Ativo = 1
	   ),
	   equipe as (
		SELECT * FROM ope 
		UNION ALL
		SELECT
			Nivel + 1,
			O.Gestor,
			O.LoginAd,
			O.Nome
		FROM equipe
		INNER JOIN Operador O ON O.Gestor = equipe.LoginAd AND O.Ativo = 1
	   ),
	   consolidado as (
		SELECT * FROM gestao 
		UNION
		SELECT * FROM equipe 
	   )
	SELECT
		Nivel,
		consolidado.LoginAd,
		consolidado.Nome,
		consolidado.Gestor,
		O.Nome AS NomeGestor,
		ISNULL(OG.Equipe, OG.Grupo) Grupo
	FROM consolidado
	INNER JOIN OperadorGrupo OG ON OG.LoginAd = consolidado.LoginAd AND OG.Principal = 1
	LEFT OUTER JOIN Operador O ON O.LoginAd = consolidado.Gestor
	ORDER BY Nivel, NomeGestor, Grupo, Nome

END