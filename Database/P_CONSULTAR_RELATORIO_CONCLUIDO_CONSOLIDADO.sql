USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_CONSULTAR_RELATORIO_CONCLUIDO_CONSOLIDADO
      Objetivo : Consulta o número de chamados concluidos por categoria em um determinado periodo
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_CONSULTAR_RELATORIO_CONCLUIDO_CONSOLIDADO]
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
		TipoChamado,
		CodPlanejador,
		COUNT(NroAtividade) Qtd
	FROM Atividade
	INNER JOIN OperadorGrupo OG1 ON OG1.Grupo = Atividade.Grupo
	WHERE 
		OG1.LoginAd = @Login AND CodStatus = 1 AND DataConclusao >= @DataInicio AND DataConclusao < @DataFim
	GROUP BY 
		Operador, Atividade.Grupo, TipoChamado, CodPlanejador

END