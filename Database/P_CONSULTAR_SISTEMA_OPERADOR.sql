USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_CONSULTAR_SISTEMA_OPERADOR
      Objetivo : Consultar os Sistemas do Operador
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_CONSULTAR_SISTEMA_OPERADOR]
	@Login		varchar(8)
AS

BEGIN
	SET NOCOUNT ON	
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	--SELECT
	--	CodSistema,
	--	NomeSistema
	--FROM Sistema
	--ORDER BY
	--	NomeSistema

	SELECT
		SistemaGrupo.CodSistema,
		NomeSistema
	FROM SistemaGrupo
	INNER JOIN Sistema ON Sistema.CodSistema = SistemaGrupo.CodSistema
	INNER JOIN OperadorGrupo ON OperadorGrupo.Grupo = SistemaGrupo.Grupo	
	WHERE
		LoginAd = @Login
	GROUP BY 
		SistemaGrupo.CodSistema,
		NomeSistema
	ORDER BY
		NomeSistema

END