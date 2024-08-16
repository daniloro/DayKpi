USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_INCLUIR_ATIVIDADE_HISTORICO_CONFIRMACAO
      Objetivo : Inclui um novo hist�rio de altera��o da atividade
      Analista : Lucas J
      Data     : 04/01/2021
      *** Hist�rico de Altera��es ****************************************
      Item  Data        Analista		Descri��o
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_INCLUIR_ATIVIDADE_HISTORICO_CONFIRMACAO]	
	@NroAtividade			varchar(14),	
	@DataConfirmacao		datetime
AS

BEGIN
	SET NOCOUNT ON

	IF EXISTS(SELECT NroAtividade FROM AtividadeManual WHERE NroAtividade = @NroAtividade)
	BEGIN
		INSERT INTO AtividadeHistoricoConfirmacao 
			(NroAtividade, LoginAd, DataConfirmacao, DataInclusao, DataFinal, TipoEntrada)
		SELECT NroAtividade, LoginAd, @DataConfirmacao, getdate(), DataFinal, 'M' FROM AtividadeManual WHERE NroAtividade = @NroAtividade		
	END
	ELSE
	BEGIN 
		INSERT INTO AtividadeHistoricoConfirmacao 
			(NroAtividade, LoginAd, DataConfirmacao, DataInclusao, DataFinal, TipoEntrada)
		SELECT NroAtividade, LoginAd, @DataConfirmacao, getdate(), DataFinal, 'A' FROM Atividade INNER JOIN Operador ON Operador.Nome = Atividade.Operador WHERE NroAtividade = @NroAtividade
	END
END