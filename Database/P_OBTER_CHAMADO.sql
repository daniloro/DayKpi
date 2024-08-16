USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_OBTER_CHAMADO
      Objetivo : Obtem os dados do Chamado
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_OBTER_CHAMADO]
	@NroChamado varchar(14)
AS

BEGIN
	SET NOCOUNT ON	
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
	
	SELECT		
		NroChamado,
		DscChamado,
		TipoChamado,
		CodStatusChamado
	FROM Chamado	
	WHERE 
		NroChamado = @NroChamado
	
END
