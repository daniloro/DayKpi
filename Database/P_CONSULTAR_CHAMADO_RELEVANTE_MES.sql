USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_CONSULTAR_CHAMADO_RELEVANTE_MES
      Objetivo : Consulta os chamados relevantes do mês
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
ALTER PROCEDURE [dbo].[P_CONSULTAR_CHAMADO_RELEVANTE_MES]	
	@Login			varchar(8),
	@DataConsulta	datetime
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
		DataImplementacao
	FROM Chamado
	INNER JOIN Atividade ON Atividade.NroChamado = Chamado.NroChamado
	INNER JOIN OperadorGrupo ON OperadorGrupo.Grupo = Atividade.Grupo
	WHERE 		
		OperadorGrupo.LoginAd = @Login AND
		CodStatusChamado = 1 AND
		DataImplementacao BETWEEN @DataConsulta AND DATEADD(MONTH, 1, @DataConsulta) AND		
		IndImportante = 1
	GROUP BY 
		Chamado.NroChamado,
		DscChamado,
		Chamado.Categoria,
		Chamado.SubCategoria,
		CodStatusChamado,
		DataImplementacao
	ORDER BY
		DataImplementacao
	
END