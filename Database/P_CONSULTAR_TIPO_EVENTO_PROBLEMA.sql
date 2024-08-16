USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_CONSULTAR_TIPO_EVENTO_PROBLEMA
      Objetivo : Consulta os tipos de Evento de Problema
      Analista : Lucas J
      Data     : 04/01/2021
      *** Hist�rico de Altera��es ****************************************
      Item  Data        Analista		Descri��o
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_CONSULTAR_TIPO_EVENTO_PROBLEMA]
AS

BEGIN
	SET NOCOUNT ON	
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	SELECT
		CodTipoEvento,
		DscTipoEvento
	FROM TipoEventoProblema	

END