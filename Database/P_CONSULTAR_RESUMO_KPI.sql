USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_CONSULTAR_RESUMO_KPI
      Objetivo : Consulta o Resumo de Preenchimento do KPI Mensal
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
ALTER PROCEDURE [dbo].[P_CONSULTAR_RESUMO_KPI]	
	@DataConsulta		datetime,
	@LoginAd			varchar(8),
	@TipoOperador		int
AS

BEGIN
	SET NOCOUNT ON	
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
	
	SELECT
		LoginAd,
		Nome,
		CodTipoEquipe,

		(SELECT TOP 1 Concluido 
			FROM AnaliseKPI 
			INNER JOIN PerguntaKPI ON PerguntaKPI.CodPergunta = AnaliseKPI.CodPergunta 
			WHERE DataAnalise = @DataConsulta 
			AND LoginAd = O.LoginAd 
			AND CodCategoria = 1 
			AND Concluido = 1
			) Objetivo,

		(SELECT TOP 1 Concluido 
			FROM AnaliseKPI 
			INNER JOIN PerguntaKPI ON PerguntaKPI.CodPergunta = AnaliseKPI.CodPergunta 
			WHERE DataAnalise = @DataConsulta 
			AND LoginAd = O.LoginAd 
			AND CodCategoria = 2 
			AND Concluido = 1
			) Organograma,

		(SELECT TOP 1 Concluido 
			FROM AnaliseKPI 
			INNER JOIN PerguntaKPI ON PerguntaKPI.CodPergunta = AnaliseKPI.CodPergunta 
			WHERE DataAnalise = @DataConsulta 
			AND LoginAd = O.LoginAd 
			AND CodCategoria = 4 
			AND Concluido = 1
			) Meta,

		(SELECT TOP 1 Concluido 
			FROM AnaliseKPI 
			INNER JOIN PerguntaKPI ON PerguntaKPI.CodPergunta = AnaliseKPI.CodPergunta 
			WHERE DataAnalise = @DataConsulta 
			AND LoginAd = O.LoginAd 
			AND CodCategoria = 5 
			AND Concluido = 1
			) Backlog,

		(SELECT COUNT(DISTINCT OG.Grupo)
			FROM OperadorGrupo OG
			LEFT OUTER JOIN AnaliseKPI KPI ON KPI.DataAnalise = @DataConsulta AND KPI.Grupo = OG.Grupo
			LEFT OUTER JOIN PerguntaKPI P ON P.CodPergunta = KPI.CodPergunta AND P.CodCategoria = 2
			WHERE OG.LoginAd = O.LoginAd
			AND (Concluido IS NULL OR Concluido = 0)
			) QtdEmFila,

		(SELECT COUNT(DISTINCT OG.Grupo)
			FROM OperadorGrupo OG
			LEFT OUTER JOIN AnaliseKPI KPI ON KPI.DataAnalise = @DataConsulta AND KPI.Grupo = OG.Grupo
			LEFT OUTER JOIN PerguntaKPI P ON P.CodPergunta = KPI.CodPergunta AND P.CodCategoria = 3
			WHERE OG.LoginAd = O.LoginAd
			AND (Concluido IS NULL OR Concluido = 0)
			) QtdAbertoConcluido,

		(SELECT COUNT(DISTINCT Operador.LoginAd) FROM Operador	
			LEFT OUTER JOIN AnaliseKPI KPI ON KPI.LoginAd = Operador.LoginAd AND DataAnalise = @DataConsulta
			LEFT OUTER JOIN PerguntaKPI P ON P.CodPergunta = KPI.CodPergunta AND P.CodCategoria = 4
			WHERE Gestor = O.LoginAd AND TipoOperador IN (1, 2) AND DataCriacao < DATEADD(month, 1, @DataConsulta) AND (Concluido IS NULL OR Concluido = 0)
			) QtdAtendimento,

		(SELECT COUNT(DISTINCT Operador.LoginAd) FROM Operador	
			LEFT OUTER JOIN AnaliseKPI KPI ON KPI.LoginAd = Operador.LoginAd AND DataAnalise = @DataConsulta
			LEFT OUTER JOIN PerguntaKPI P ON P.CodPergunta = KPI.CodPergunta AND P.CodCategoria = 5
			WHERE Gestor = O.LoginAd AND TipoOperador IN (1, 2) AND DataCriacao < DATEADD(month, 1, @DataConsulta) AND (Concluido IS NULL OR Concluido = 0)
			) QtdPerformance,

		(SELECT TOP 1 Concluido 
			FROM AnaliseKPI 
			INNER JOIN PerguntaKPI ON PerguntaKPI.CodPergunta = AnaliseKPI.CodPergunta
			WHERE DataAnalise = @DataConsulta 
			AND LoginAd = O.LoginAd 
			AND CodCategoria = 6 
			AND Concluido = 1
			) Gmud,		

		(SELECT COUNT(Operador.LoginAd) FROM Operador	
			LEFT OUTER JOIN PontoMensal P ON P.LoginAd = Operador.LoginAd AND DataPonto = @DataConsulta
			WHERE Gestor = O.LoginAd AND TipoOperador IN (1, 2) AND DataCriacao < DATEADD(month, 1, @DataConsulta) AND CodPontoMensal IS NULL
			) QtdHoraExtra,
							
		(SELECT COUNT(NroAtividade) FROM Atividade A
			INNER JOIN OperadorGrupo OG ON OG.Grupo = A.Grupo
			WHERE OG.LoginAd = O.LoginAd AND A.CodStatus = 0 AND A.DataAbertura < DATEADD(month, 1, @DataConsulta)
			) QtdVencido

	FROM Operador O
	WHERE 
		TipoOperador = 2 AND
		(LoginAd = @LoginAd OR Gestor = @LoginAd OR @TipoOperador = 3)
	ORDER BY
		Nome

END




			


