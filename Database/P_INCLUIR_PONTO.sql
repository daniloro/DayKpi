USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_INCLUIR_PONTO
      Objetivo : Inclui um novo registro na tabela Ponto
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_INCLUIR_PONTO]	
	@LoginAd		varchar(8),
	@DataPonto		datetime,
	@HoraInicio		datetime,
	@HomeOffice		bit
AS

BEGIN
	SET NOCOUNT ON

	INSERT INTO Ponto (LoginAd, DataPonto, HoraInicio, HomeOffice) VALUES (@LoginAd, @DataPonto, @HoraInicio, @HomeOffice)

END
