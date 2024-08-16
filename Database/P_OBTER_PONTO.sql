USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_OBTER_PONTO
      Objetivo : Obtem os dados do Ponto do Operador
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_OBTER_PONTO]
	@Login varchar(8)
AS

BEGIN
	SET NOCOUNT ON	
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	SELECT
		CodPonto,
		HoraInicio,
		HoraAlmoco,
		HoraRetorno,
		HoraSaida,
		HomeOffice
	FROM Ponto
	WHERE
		LoginAd = @Login AND
		DataPonto = CONVERT(VARCHAR(10), GETDATE(), 120)

END
