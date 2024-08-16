USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_ALTERAR_EVENTO_PROBLEMA
      Objetivo : Altera um novo registro na tabela EventoProblema
      Analista : Lucas J
      Data     : 04/01/2021
      *** Hist�rico de Altera��es ****************************************
      Item  Data        Analista		Descri��o
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_ALTERAR_EVENTO_PROBLEMA]
	@CodEvento		int,
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

	UPDATE EventoProblema SET 
		CodTipoEvento = @CodTipoEvento,
		CodSistema = @CodSistema,
		DataEvento = @DataEvento,
		TituloEvento = @TituloEvento,
		DscEvento = @DscEvento,
		Correcao = @DscCorrecao,
		NroChamado = @NroChamado,
		NroCorrecao = @NroCorrecao,
		LoginAd = @Login,
		DataUltimaAlteracao = getdate()
	WHERE 
		CodEvento = @CodEvento

END