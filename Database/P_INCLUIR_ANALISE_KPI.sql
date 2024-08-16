USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_INCLUIR_ANALISE_KPI
      Objetivo : Inclui uma analise de KPI
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_INCLUIR_ANALISE_KPI]
	@DataAnalise	datetime,
	@Grupo			varchar(50),
	@LoginOperador	varchar(8),
	@CodPergunta	int,
	@DscAnalise		varchar(max),
	@Login			varchar(8),
	@Concluido		bit
AS

BEGIN
	SET NOCOUNT ON	
	
	INSERT INTO AnaliseKPI (DataAnalise, Grupo, LoginOperador, CodPergunta, DscAnalise, LoginAd, DataAtualizacao, Concluido) 
	VALUES (@DataAnalise, @Grupo, @LoginOperador, @CodPergunta, @DscAnalise, @Login, getdate(), @Concluido)

	SELECT SCOPE_IDENTITY()	
END
