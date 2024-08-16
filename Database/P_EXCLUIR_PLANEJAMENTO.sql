USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_EXCLUIR_PLANEJAMENTO
      Objetivo : Exclui um sistema na GMUD
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_EXCLUIR_PLANEJAMENTO]
	@CodPlanejamento		int	
AS

BEGIN
	SET NOCOUNT ON

	DELETE FROM Planejamento WHERE CodPlanejamento = @CodPlanejamento

END
