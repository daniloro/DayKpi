USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_OBTER_CHAMADO_POSSUI_GRUPO
      Objetivo : Verifica se o chamado possui alguma atividade com o grupo espec�fico
      Analista : Lucas J
      Data     : 04/01/2021
      *** Hist�rico de Altera��es ****************************************
      Item  Data        Analista		Descri��o
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