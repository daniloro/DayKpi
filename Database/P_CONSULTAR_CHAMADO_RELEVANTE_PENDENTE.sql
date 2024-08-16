USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_CONSULTAR_CHAMADO_RELEVANTE_PENDENTE
      Objetivo : Consulta os chamados relevantes pendentes
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_CONSULTAR_CHAMADO_RELEVANTE_PENDENTE]	
	@Login		varchar(8)
AS

BEGIN
	SET NOCOUNT ON	
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
	
	SELECT		
		Chamado.NroChamado,
		DscChamado,
		Chamado.Categoria,
		Chamado.SubCategoria,	
		CodStatusChamado,
		Chamado.DataFinal
	FROM Chamado
	INNER JOIN Atividade ON Atividade.NroChamado = Chamado.NroChamado
	INNER JOIN OperadorGrupo ON OperadorGrupo.Grupo = Atividade.Grupo
	WHERE 		
		OperadorGrupo.LoginAd = @Login AND
		CodStatusChamado = 0 AND
		IndImportante = 1
	GROUP BY 
		Chamado.NroChamado,
		DscChamado,
		Chamado.Categoria,
		Chamado.SubCategoria,
		CodStatusChamado,
		Chamado.DataFinal
	ORDER BY
		Chamado.DataFinal
	
END