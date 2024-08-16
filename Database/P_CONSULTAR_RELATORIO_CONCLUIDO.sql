USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_CONSULTAR_RELATORIO_CONCLUIDO
      Objetivo : Consulta o n�mero de chamados concluidos por categoria em um determinado periodo
      Analista : Lucas J
      Data     : 04/01/2021
      *** Hist�rico de Altera��es ****************************************
      Item  Data        Analista		Descri��o
      ********************************************************************
*/
ALTER PROCEDURE [dbo].[P_CONSULTAR_RELATORIO_CONCLUIDO]
	@Login		varchar(8),
	@DataInicio	datetime,
	@DataFim	datetime
AS

BEGIN
	SET NOCOUNT ON	
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	SELECT
		Operador,
		Atividade.Grupo,	
		Atividade.TipoChamado,
		Atividade.NroChamado,
		DscChamado,
		Atividade.NroAtividade,
		DscPlanejador,
		DscAtividade,		
		DataAbertura,
		DataConclusao		
	FROM Atividade
	INNER JOIN Chamado ON Chamado.NroChamado = Atividade.NroChamado
	LEFT OUTER JOIN Planejador ON Planejador.CodPlanejador = Atividade.CodPlanejador
	INNER JOIN OperadorGrupo OG1 ON OG1.Grupo = Atividade.Grupo
	WHERE 
		OG1.LoginAd = @Login AND CodStatus = 1 AND DataConclusao >= @DataInicio AND DataConclusao < @DataFim
	ORDER BY
		Operador,
		TipoChamado,
		DscPlanejador,
		DscAtividade,
		DataConclusao
		

END

