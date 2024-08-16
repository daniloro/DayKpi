USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_OBTER_ATIVIDADE
      Objetivo : Obtem os dados da Atividade
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_OBTER_ATIVIDADE]
	@NroAtividade varchar(14)
AS

BEGIN
	SET NOCOUNT ON	
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
	
	SELECT		
		Chamado.NroChamado,
		DscChamado,
		NroAtividade,
		DscAtividade,
		Atividade.DataFinal
	FROM Atividade
	INNER JOIN Chamado ON Chamado.NroChamado = Atividade.NroChamado	
	WHERE 
		NroAtividade = @NroAtividade
	
END