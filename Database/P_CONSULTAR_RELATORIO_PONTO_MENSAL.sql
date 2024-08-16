USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_CONSULTAR_RELATORIO_PONTO_MENSAL
      Objetivo : Consulta o ponto mensal da Equipe. Horas Extras.
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_CONSULTAR_RELATORIO_PONTO_MENSAL]
	@Login		varchar(8),
	@DataInicio	datetime,
	@DataFim	datetime
AS

BEGIN
	SET NOCOUNT ON	
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	SELECT
		OG1.Grupo,
		O.LoginAd,
		Nome,
		(SELECT SUM(DATEDIFF(MINUTE, HoraInicio, HoraSaida) - 540) FROM Ponto INNER JOIN DiaUtil ON DiaUtil.DataDiaUtil = Ponto.DataPonto WHERE Ponto.LoginAd = O.LoginAd AND DataPonto >= @DataInicio AND DataPonto < @DataFim) +
		ISNULL((SELECT SUM(DATEDIFF(MINUTE, HoraInicio, HoraSaida)) FROM Ponto LEFT OUTER JOIN DiaUtil ON DiaUtil.DataDiaUtil = Ponto.DataPonto WHERE Ponto.LoginAd = O.LoginAd AND DataPonto >= @DataInicio AND DataPonto < @DataFim AND DataDiaUtil IS NULL), 0) HEMensal,
		(SELECT TOP 1 DATEDIFF(MINUTE, HoraInicio, HoraSaida) - 540 FROM DiaUtil LEFT OUTER JOIN Ponto ON Ponto.DataPonto = DiaUtil.DataDiaUtil AND Ponto.LoginAd = O.LoginAd WHERE DataDiaUtil < @DataFim AND DataDiaUtil < CONVERT(VARCHAR(10), getdate(), 120) ORDER BY DataDiaUtil DESC) HEUltimoDia,
		(SELECT COUNT(DataDiaUtil) FROM DiaUtil LEFT OUTER JOIN Ponto ON Ponto.DataPonto = DiaUtil.DataDiaUtil AND LoginAd = O.LoginAd WHERE DataDiaUtil >= @DataInicio AND DataDiaUtil < @DataFim AND DataDiaUtil < CONVERT(VARCHAR(10), getdate(), 120) AND CodPonto IS NULL) PontoPendente,
		(SELECT COUNT(CodPonto) FROM Ponto WHERE LoginAd = O.LoginAd AND DataPonto >= @DataInicio AND DataPonto < @DataFim AND DataPonto < CONVERT(VARCHAR(10), getdate(), 120) AND HoraSaida IS NULL) PontoIncompleto,
		(SELECT COUNT(DISTINCT DataPonto) FROM PontoManual INNER JOIN Ponto ON Ponto.CodPonto = PontoManual.CodPonto WHERE LoginAd = O.LoginAd AND DataPonto >= @DataInicio AND DataPonto < @DataFim) PontoManual
		FROM Operador O
		INNER JOIN OperadorGrupo OG1 ON OG1.LoginAd = O.LoginAd AND Principal = 1
		INNER JOIN OperadorGrupo OG2 ON OG2.Grupo = OG1.Grupo		
		WHERE OG2.LoginAd = @Login AND O.Ativo = 1 AND TipoOperador < 3
		ORDER BY 4 DESC

END