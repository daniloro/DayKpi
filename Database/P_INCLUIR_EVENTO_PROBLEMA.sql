USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_INCLUIR_EVENTO_PROBLEMA
      Objetivo : Inclui um novo registro na tabela EventoProblema
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_INCLUIR_EVENTO_PROBLEMA]	
	@CodTipoEvento	int,
	@CodSistema		int,
	@DataEvento		datetime,
	@TituloEvento	varchar(100),
	@DscEvento		varchar(550),
	@DscCorrecao	varchar(550),
	@NroChamado		varchar(14),
	@NroCorrecao	varchar(14),
	@Login			varchar(8)
AS

BEGIN
	SET NOCOUNT ON

	INSERT INTO EventoProblema (CodTipoEvento, CodSistema, DataEvento, TituloEvento, DscEvento, Correcao, NroChamado, NroCorrecao, LoginAd, DataUltimaAlteracao) 
	VALUES (@CodTipoEvento, @CodSistema, @DataEvento, @TituloEvento, @DscEvento, @DscCorrecao, @NroChamado, @NroCorrecao, @Login, getdate())

END
