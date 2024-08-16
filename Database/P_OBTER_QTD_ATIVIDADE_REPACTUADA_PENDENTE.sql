USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_OBTER_QTD_ATIVIDADE_REPACTUADA_PENDENTE
      Objetivo : Obtem a quantidade de atividade repactuada pendente
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_OBTER_QTD_ATIVIDADE_REPACTUADA_PENDENTE]
	@Login			varchar(8),
	@DataConclusao	datetime
AS

BEGIN
	SET NOCOUNT ON	
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	SELECT
		COUNT(CodHistorico) Qtd
	FROM AtividadeHistorico
	INNER JOIN Atividade ON Atividade.NroAtividade = AtividadeHistorico.NroAtividade
	INNER JOIN OperadorGrupo ON OperadorGrupo.Grupo = Atividade.Grupo
	INNER JOIN Operador ON Operador.LoginAd = OperadorGrupo.LoginAd
	WHERE 
		(CodStatus = 0 OR DataConclusao >= @DataConclusao) AND
		TipoLancamento = 0 AND
		LoginAlteracao IS NULL AND
		OperadorGrupo.LoginAd = @Login AND
		(Operador.TipoOperador IN (2,3) OR Operador.Nome = Atividade.Operador)

END