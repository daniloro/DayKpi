USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_CONSULTAR_PONTO_MENSAL_OPERADOR
      Objetivo : Consultar o detalhe do Ponto Mensal do Operador
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_CONSULTAR_PONTO_MENSAL_OPERADOR]
	@Login			varchar(8),
	@DataInicio		datetime,
	@DataFim		datetime
AS

BEGIN
	SET NOCOUNT ON	
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	SELECT
		LoginAd,
		DataPonto,
		Ponto.CodPonto,
		HoraInicio,
		HoraAlmoco,
		HoraRetorno,
		HoraSaida,
		PM1.CodPonto HoraInicioManual,
		PM2.CodPonto HoraAlmocoManual,
		PM3.CodPonto HoraRetornoManual,
		PM4.CodPonto HoraSaidaManual
	FROM Ponto
	LEFT OUTER JOIN DiaUtil ON DiaUtil.DataDiaUtil = Ponto.DataPonto
	LEFT OUTER JOIN PontoManual PM1 ON PM1.CodPonto = Ponto.CodPonto AND PM1.TipoPonto = 1
	LEFT OUTER JOIN PontoManual PM2 ON PM2.CodPonto = Ponto.CodPonto AND PM2.TipoPonto = 2
	LEFT OUTER JOIN PontoManual PM3 ON PM3.CodPonto = Ponto.CodPonto AND PM3.TipoPonto = 3
	LEFT OUTER JOIN PontoManual PM4 ON PM4.CodPonto = Ponto.CodPonto AND PM4.TipoPonto = 4
	WHERE LoginAd = @Login AND DataPonto >= @DataInicio AND DataPonto < @DataFim AND DataPonto <= CONVERT(VARCHAR(10), getdate(), 120)
	UNION
	SELECT
		@Login,
		DataDiaUtil,
		CodPonto,
		HoraInicio,
		HoraAlmoco,
		HoraRetorno,
		HoraSaida,
		null, null, null, null
	FROM DiaUtil
	LEFT OUTER JOIN Ponto ON Ponto.DataPonto = DiaUtil.DataDiaUtil AND LoginAd = @Login
	WHERE DataDiaUtil >= @DataInicio AND DataDiaUtil < @DataFim AND DataDiaUtil <= CONVERT(VARCHAR(10), getdate(), 120) AND CodPonto IS NULL
	ORDER BY DataPonto

END
