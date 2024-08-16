USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_CONSULTAR_PLANEJAMENTO
      Objetivo : Consulta o planejamento do analista
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_CONSULTAR_PLANEJAMENTO]	
	@Login				varchar(8),
	@DataPlanejamento	datetime
AS

BEGIN
	SET NOCOUNT ON	
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	SELECT
		CodPlanejamento,
		LoginAd,
		DataPlanejamento,
		CodTipoPlanejamento,
		NroAtividade,		
		DscPlanejamento
	FROM Planejamento	
	WHERE 
		LoginAd = @Login AND
		DataPlanejamento = @DataPlanejamento
	ORDER BY 
		CodTipoPlanejamento, 
		DataPlanejamento

END
