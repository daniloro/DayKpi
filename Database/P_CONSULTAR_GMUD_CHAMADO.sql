USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_CONSULTAR_GMUD_CHAMADO
      Objetivo : Consultar os chamados de uma GMUD
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_CONSULTAR_GMUD_CHAMADO]
	@Login		varchar(8),
	@VersionId	varchar(36)
AS

BEGIN
	SET NOCOUNT ON	
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	SELECT
		NroChamado,
		DscChamado,
		(SELECT COUNT(NroChamado) FROM Atividade WHERE Atividade.NroChamado = Chamado.NroChamado 
			AND Grupo IN (SELECT Grupo FROM OperadorGrupo WHERE LoginAd = @Login)) AS ChamadoEquipe
	FROM Chamado	
	WHERE
		VersionId =	@VersionId AND
		Gmud = 0
	ORDER BY 
		NroChamado

END