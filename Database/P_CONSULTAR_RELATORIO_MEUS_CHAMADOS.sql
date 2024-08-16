USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_CONSULTAR_RELATORIO_MEUS_CHAMADOS
      Objetivo : Consulta o ponto mensal da Equipe. Horas Extras.
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_CONSULTAR_RELATORIO_MEUS_CHAMADOS]
	@Login		varchar(8)
AS

BEGIN
	SET NOCOUNT ON	
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	SELECT
		Chamado.NroChamado,
		Chamado.DscChamado,
		TempPlanejador.Grupo,
		TempPlanejador.Operador,
		TempPlanejador.NroAtividade,
		Planejador.Abreviado,
		TempPlanejador.DataFinal
	FROM Chamado
	INNER JOIN Atividade ON Atividade.NroChamado = Chamado.NroChamado
	INNER JOIN Operador ON Operador.Nome = Atividade.Operador
	LEFT OUTER JOIN TempPlanejador ON TempPlanejador.NroChamado = Chamado.NroChamado AND TempPlanejador.CodPlanejador = Chamado.CodPlanejador
	LEFT OUTER JOIN Planejador ON Planejador.CodPlanejador = Chamado.CodPlanejador
	WHERE 
		Chamado.TipoChamado = 'Req. Complexa' AND CodStatusChamado = 0 AND Operador.LoginAd = @Login
	GROUP BY 
		Chamado.NroChamado, Chamado.DscChamado, TempPlanejador.Grupo, TempPlanejador.Operador, TempPlanejador.NroAtividade, TempPlanejador.DscAtividade, Chamado.CodPlanejador, Planejador.Abreviado, TempPlanejador.DataFinal
	ORDER BY 
		Chamado.CodPlanejador, Chamado.NroChamado, TempPlanejador.Grupo, TempPlanejador.Operador, TempPlanejador.DataFinal

END