USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_ALTERAR_ATIVIDADE_CONFIRMADO
      Objetivo : Confirma se o planejamento para a atividade est� ok ou se ser� alterado
      Analista : Lucas J
      Data     : 04/01/2021
      *** Hist�rico de Altera��es ****************************************
      Item  Data        Analista		Descri��o
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_ALTERAR_ATIVIDADE_CONFIRMADO]
	@NroAtividade	varchar(14),
	@Confirmado		int
AS

BEGIN
	SET NOCOUNT ON

	IF EXISTS(SELECT NroAtividade FROM AtividadeManual WHERE NroAtividade = @NroAtividade)
	BEGIN
		UPDATE AtividadeManual
			SET Confirmado = @Confirmado
		WHERE 
			NroAtividade = @NroAtividade
	END
	 
	UPDATE Atividade
		SET Confirmado = @Confirmado
	WHERE 
		NroAtividade = @NroAtividade	

END