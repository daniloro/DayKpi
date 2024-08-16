USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_ALTERAR_ANALISE_KPI
      Objetivo : Altera uma analise de KPI
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_ALTERAR_ANALISE_KPI]
	@CodAnalise		int,		
	@DscAnalise		varchar(max),
	@Login			varchar(8),
	@Concluido		bit
AS

BEGIN
	SET NOCOUNT ON	
	
	UPDATE AnaliseKPI SET 
		DscAnalise = @DscAnalise,
		LoginAd = @Login,
		DataAtualizacao = getdate(),
		Concluido = @Concluido
	WHERE 
		CodAnalise = @CodAnalise	
	
END
