USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_INCLUIR_ATIVIDADE_MANUAL
      Objetivo : Inclui uma atividade Manual
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_INCLUIR_ATIVIDADE_MANUAL]
	@Login			varchar(8),
	@NroAtividade	varchar(14),
	@DscManual		varchar(100),
	@DataFinal		datetime
AS

BEGIN
	SET NOCOUNT ON	
	
	IF EXISTS(SELECT NroAtividade FROM AtividadeManual WHERE NroAtividade = @NroAtividade)	
	BEGIN
		UPDATE AtividadeManual SET LoginAd = @Login, DscManual = @DscManual, DataFinal = @DataFinal, Confirmado = 0, DataInclusao = getdate() WHERE NroAtividade = @NroAtividade
	END
	ELSE
	BEGIN
		INSERT INTO AtividadeManual (NroAtividade, LoginAd, DscManual, DataFinal, Confirmado, DataInclusao)
		VALUES (@NroAtividade, @Login, @DscManual, @DataFinal, 0, getdate())
	END
	
END