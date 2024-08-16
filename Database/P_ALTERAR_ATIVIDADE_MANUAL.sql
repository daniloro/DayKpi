USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_ALTERAR_ATIVIDADE_MANUAL
      Objetivo : Altera os dados da Atividade
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_ALTERAR_ATIVIDADE_MANUAL]
	@NroAtividade	varchar(14),
	@DataFinal		datetime
AS

BEGIN
	SET NOCOUNT ON
	
	SELECT NroAtividade FROM AtividadeManual WHERE NroAtividade = @NroAtividade	
	IF @@rowcount > 0
	BEGIN
		UPDATE AtividadeManual SET DataFinal = @DataFinal WHERE NroAtividade = @NroAtividade	
	END
	ELSE BEGIN
		INSERT INTO AtividadeManual
		SELECT NroAtividade, LoginAd, null, @DataFinal, 0, getdate() FROM Atividade INNER JOIN Operador ON Operador.Nome = Atividade.Operador WHERE NroAtividade = @NroAtividade
	END
END

