USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_OBTER_EVENTO_PROBLEMA
      Objetivo : Obtem os dados do Evento de Problema
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_OBTER_EVENTO_PROBLEMA]
	@CodEvento	int
AS

BEGIN
	SET NOCOUNT ON	
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
	
	SELECT		
		CodEvento,
		CodTipoEvento,
		CodSistema,
		DataEvento,
		TituloEvento,
		DscEvento,
		Correcao,
		NroChamado,
		NroCorrecao
	FROM EventoProblema	
	WHERE 
		CodEvento = @CodEvento
	
END
