USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_CONSULTAR_ATIVIDADE_ANDAMENTO
      Objetivo : Lista as atividades em andamento para a Daily
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
ALTER PROCEDURE [dbo].[P_CONSULTAR_ATIVIDADE_ANDAMENTO]
	@Login varchar(8)
AS

BEGIN
	SET NOCOUNT ON	
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	;WITH cte AS
	(
		SELECT
			Operador.LoginAd,
			OG1.Grupo,
			Nome,
			AM.Confirmado,
			A.TipoChamado,
			A.CodPlanejador,
			Planejador.Abreviado,
			A.NroChamado,
			AM.NroAtividade,
			ISNULL(Chamado.DscChamado, AM.DscManual) DscChamado,
			'M' TipoEntrada,		
			AM.DataFinal
		FROM Operador
		INNER JOIN OperadorGrupo OG1 ON OG1.LoginAd = Operador.LoginAd AND Principal = 1
		INNER JOIN OperadorGrupo OG2 ON OG2.Grupo = OG1.Grupo	
		LEFT OUTER JOIN AtividadeManual AM ON AM.LoginAd = Operador.LoginAd
		LEFT OUTER JOIN Atividade A ON A.NroAtividade = AM.NroAtividade
		LEFT OUTER JOIN Chamado ON Chamado.NroChamado = A.NroChamado
		LEFT OUTER JOIN Planejador ON Planejador.CodPlanejador = A.CodPlanejador
		WHERE 
			Operador.Ativo = 1 AND
			Operador.TipoOperador = 1 AND
			OG2.LoginAd = @Login AND
			AM.Confirmado < 2 AND
			(A.NroAtividade IS NULL OR A.CodStatus > 0 OR A.Confirmado = 2 OR A.Operador <> Operador.Nome)		
	)
	SELECT
		Operador.LoginAd,
		OG1.Grupo,
		Nome,
		CASE WHEN AM.Confirmado IS NOT NULL THEN AM.Confirmado ELSE A.Confirmado END AS Confirmado,		
		A.TipoChamado,
		A.CodPlanejador,
		Planejador.Abreviado,
		A.NroChamado,
		A.NroAtividade,
		Chamado.DscChamado,
		CASE WHEN AM.NroAtividade IS NOT NULL THEN 'M' ELSE 'A' END AS TipoEntrada,		
		ISNULL(AM.Datafinal, A.DataFinal) DataFinal
	FROM Operador
	INNER JOIN OperadorGrupo OG1 ON OG1.LoginAd = Operador.LoginAd AND Principal = 1
	INNER JOIN OperadorGrupo OG2 ON OG2.Grupo = OG1.Grupo	
	LEFT OUTER JOIN Atividade A ON A.Operador = Operador.Nome AND A.CodStatus = 0 AND Confirmado < 2
	LEFT OUTER JOIN AtividadeManual AM ON AM.LoginAd = Operador.LoginAd AND AM.NroAtividade = A.NroAtividade
	LEFT OUTER JOIN Chamado ON Chamado.NroChamado = A.NroChamado
	LEFT OUTER JOIN Planejador ON Planejador.CodPlanejador = A.CodPlanejador
	WHERE 
		Operador.Ativo = 1 AND
		Operador.TipoOperador = 1 AND
		OG2.LoginAd = @Login AND		
		(A.NroAtividade IS NOT NULL OR Operador.LoginAd NOT IN (SELECT LoginAd FROM cte))
	
	UNION ALL
		SELECT * FROM cte
		ORDER BY 
		Operador.Nome, DataFinal, NroAtividade
	
END