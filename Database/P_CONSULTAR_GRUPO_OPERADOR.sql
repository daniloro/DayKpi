USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_CONSULTAR_GRUPO_OPERADOR
      Objetivo : Consulta todos grupos do TopDesk do Operador
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_CONSULTAR_GRUPO_OPERADOR]
	@Login	varchar(8)
AS

BEGIN
	SET NOCOUNT ON	
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	SELECT
		Grupo                                                                    
	FROM Operador
	INNER JOIN OperadorGrupo ON OperadorGrupo.LoginAd = Operador.LoginAd
	WHERE 
		Operador.LoginAd = @Login AND
		OperadorGrupo.EntradaManual = 0
	ORDER BY 
		Grupo

END
