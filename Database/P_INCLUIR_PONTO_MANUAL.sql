USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_INCLUIR_PONTO_MANUAL
      Objetivo : Inclui um novo registro na tabela Ponto
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_INCLUIR_PONTO_MANUAL]	
	@CodPonto		int,
	@HoraManual		datetime,
	@TipoPonto		int
AS

BEGIN
	SET NOCOUNT ON

	INSERT INTO PontoManual (CodPonto, HoraManual, TipoPonto)
    VALUES (@CodPonto, @HoraManual, @TipoPonto)

END
