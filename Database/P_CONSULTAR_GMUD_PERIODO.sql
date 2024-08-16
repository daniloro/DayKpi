USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_CONSULTAR_GMUD_PERIODO
      Objetivo : Consultar as GMUDs do Periodo
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
ALTER PROCEDURE [dbo].[P_CONSULTAR_GMUD_PERIODO]
	@Login		varchar(8),
	@DataInicio	datetime,
	@DataFim	datetime
AS

BEGIN
	SET NOCOUNT ON	
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	SELECT
		Gmud.VersionId,
		Gmud.NroChamado,		
		Gmud.Categoria,
		Gmud.SubCategoria,
		GmudVersao.DscVersao,
		Gmud.DscChamado,
		Gmud.DataImplementacao,
		(SELECT COUNT(NroChamado) FROM Chamado WHERE VersionId = Gmud.VersionId AND Gmud = 0) QtdChamadoTotal,
		(SELECT COUNT(DISTINCT Chamado.NroChamado)) QtdChamadoEquipe
	FROM Chamado
	INNER JOIN Atividade ON Atividade.NroChamado = Chamado.NroChamado
	INNER JOIN OperadorGrupo ON OperadorGrupo.LoginAd = @Login AND OperadorGrupo.Grupo = Atividade.Grupo
	INNER JOIN Chamado Gmud ON Gmud.VersionId = Chamado.VersionId
	LEFT OUTER JOIN GmudVersao ON GmudVersao.VersionId = Chamado.VersionId
	WHERE
		Gmud.CodStatusChamado = 1 AND	
		Gmud.DataImplementacao >= @DataInicio AND Gmud.DataImplementacao < @DataFim AND		
		Chamado.Gmud = 0 AND Gmud.Gmud = 1		
	GROUP BY
		Gmud.VersionId,
		Gmud.NroChamado,		
		Gmud.Categoria,
		Gmud.SubCategoria,
		GmudVersao.DscVersao,
		Gmud.DscChamado,
		Gmud.DataImplementacao	
		
	UNION ALL
	
	SELECT
		null,
		NroChamado,
		Categoria,
		SubCategoria,
		null,
		DscChamado,
		DataImplementacao,
		0,
		0
	FROM Chamado
	INNER JOIN OperadorGrupo ON OperadorGrupo.LoginAd = @Login AND OperadorGrupo.Grupo = Chamado.Grupo
	WHERE
		Chamado.CodStatusChamado = 1 AND	
		DataImplementacao >= @DataInicio AND DataImplementacao < @DataFim AND		 	
		Gmud = 1 AND VersionId IS NULL
	ORDER BY
		Categoria,
		DscVersao,
		DataImplementacao,
		NroChamado

END


