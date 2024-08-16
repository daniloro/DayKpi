USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_ALTERAR_PONTO
      Objetivo : Altera um registro na tabela Ponto
      Analista : Lucas J
      Data     : 04/01/2021
      *** Hist�rico de Altera��es ****************************************
      Item  Data        Analista		Descri��o
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_ALTERAR_PONTO]	
	@CodPonto		int,
	@HoraAlmoco		datetime,
	@HoraRetorno	datetime,
	@HoraSaida		datetime
AS

BEGIN
	SET NOCOUNT ON

	UPDATE Ponto SET 
		HoraAlmoco = @HoraAlmoco,
		HoraRetorno = @HoraRetorno,
		HoraSaida = @HoraSaida
	WHERE 
		CodPonto = @CodPonto

END
