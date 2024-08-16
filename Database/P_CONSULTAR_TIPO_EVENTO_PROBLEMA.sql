USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_CONSULTAR_TIPO_EVENTO_PROBLEMA
      Objetivo : Consulta os tipos de Evento de Problema
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
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