USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_OBTER_CHAMADO_POSSUI_GRUPO
      Objetivo : Verifica se o chamado possui alguma atividade com o grupo específico
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_OBTER_CHAMADO_POSSUI_GRUPO]
	@NroChamado varchar(14),
	@Login		varchar(8)
AS

BEGIN
	SET NOCOUNT ON	
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
	
	SELECT		
		Chamado.NroChamado,
		DscChamado,		
		CodStatusChamado,
		DataImplementacao
	FROM Chamado
	INNER JOIN Atividade ON Atividade.NroChamado = Chamado.NroChamado
	INNER JOIN OperadorGrupo ON OperadorGrupo.Grupo = Atividade.Grupo
	WHERE 
		Chamado.NroChamado = @NroChamado AND
		OperadorGrupo.LoginAd = @Login
	GROUP BY 
		Chamado.NroChamado,
		DscChamado,		
		CodStatusChamado,
		DataImplementacao
	
END