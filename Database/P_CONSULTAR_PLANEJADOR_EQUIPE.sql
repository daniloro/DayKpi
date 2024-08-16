USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_CONSULTAR_PLANEJADOR_EQUIPE
      Objetivo : Consulta todos itens do Planejador do Operador
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_CONSULTAR_PLANEJADOR_EQUIPE]
	@Login	varchar(8)
AS

BEGIN
	SET NOCOUNT ON	
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	SELECT
		Planejador.CodPlanejador,
		DscPlanejador,
		Abreviado
	FROM Planejador
	INNER JOIN Operador ON Operador.CodTipoEquipe = Planejador.CodTipoEquipe
	WHERE 
		LoginAd = @Login
	ORDER BY 
		Ordem

END
