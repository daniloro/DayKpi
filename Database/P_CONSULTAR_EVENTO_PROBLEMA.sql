USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_CONSULTAR_EVENTO_PROBLEMA
      Objetivo : Consulta os eventos de problema do Periodo
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_CONSULTAR_EVENTO_PROBLEMA]
	@Login		varchar(8),
	@DataInicio	datetime,
	@DataFim	datetime
AS

BEGIN
	SET NOCOUNT ON	
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	;WITH ListaSistema AS (
		SELECT
			CodSistema
		FROM SistemaGrupo
		INNER JOIN OperadorGrupo ON OperadorGrupo.Grupo = SistemaGrupo.Grupo
		WHERE
			LoginAd = @Login
		GROUP BY CodSistema
	   )
	SELECT
		CodEvento,
		EP.CodTipoEvento,
		DscTipoEvento,		
		NomeSistema,
		DataEvento,
		TituloEvento,
		NroChamado
	FROM EventoProblema EP
	INNER JOIN TipoEventoProblema TEP ON TEP.CodTipoEvento = EP.CodTipoEvento
	INNER JOIN Sistema S ON S.CodSistema = EP.CodSistema
	INNER JOIN ListaSistema LS ON LS.CodSistema = S.CodSistema
	WHERE
		DataEvento BETWEEN @DataInicio AND @DataFim
	ORDER BY 
		DataEvento, EP.CodTipoEvento

END