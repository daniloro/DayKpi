USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_CONSULTAR_PONTO
      Objetivo : Consultar lista de ponto
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_CONSULTAR_PONTO]
	@Login		varchar(8),
	@DataPonto	datetime
AS

BEGIN
	SET NOCOUNT ON	
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	SELECT
		Nome,
		HoraInicio,
		HoraAlmoco,
		HoraRetorno,
		HoraSaida,
		HomeOffice
	FROM Operador O
	LEFT OUTER JOIN Ponto ON Ponto.LoginAd = O.LoginAd AND DataPonto = @DataPonto
	INNER JOIN OperadorGrupo OG1 ON OG1.LoginAd = O.LoginAd AND Principal = 1
	INNER JOIN OperadorGrupo OG2 ON OG2.Grupo = OG1.Grupo
	WHERE
		OG2.LoginAd = @Login
		AND TipoOperador IN (1,2) AND Ativo = 1
	ORDER BY Nome

END
