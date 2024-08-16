USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_CONSULTAR_ATIVIDADE_ATUAL
      Objetivo : Obtem as atividades atuais do Operador
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
ALTER PROCEDURE [dbo].[P_CONSULTAR_ATIVIDADE_ATUAL]
	@Login varchar(8)
AS

BEGIN
	SET NOCOUNT ON	
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
	
	;WITH cte AS
	(
		SELECT
			Chamado.NroChamado,
			Chamado.DscChamado,
			AM.NroAtividade,
			ISNULL(Chamado.DscChamado, DscManual) DscAtividade,
			AM.DataFinal,
			A.TipoChamado,
			A.CodPlanejador,
			Planejador.Abreviado,
			AM.Confirmado,
			'M' TipoEntrada,
			(SELECT COUNT(NroAtividade) FROM AtividadeHistoricoConfirmacao AHC WHERE AHC.NroAtividade = AM.NroAtividade AND AHC.LoginAd = Operador.LoginAd) QtdConfirmacao		
		FROM Operador
		INNER JOIN AtividadeManual AM ON AM.LoginAd = Operador.LoginAd
		LEFT OUTER JOIN Atividade A ON A.NroAtividade = AM.NroAtividade
		LEFT OUTER JOIN Chamado ON Chamado.NroChamado = A.NroChamado
		LEFT OUTER JOIN Planejador ON Planejador.CodPlanejador = A.CodPlanejador
		WHERE 
			Operador.LoginAd = @Login AND AM.Confirmado < 2
	)
	SELECT
		Chamado.NroChamado,
		DscChamado,
		NroAtividade,
		DscAtividade,
		A.DataFinal,
		A.TipoChamado,
		A.CodPlanejador,
		Planejador.Abreviado,
		Confirmado,
		'A' TipoEntrada,
		(SELECT COUNT(NroAtividade) FROM AtividadeHistoricoConfirmacao AHC WHERE AHC.NroAtividade = A.NroAtividade AND AHC.LoginAd = Operador.LoginAd) QtdConfirmacao
	FROM Operador
	INNER JOIN Atividade A ON A.Operador = Operador.Nome
	INNER JOIN Chamado ON Chamado.NroChamado = A.NroChamado
	LEFT OUTER JOIN Planejador ON Planejador.CodPlanejador = A.CodPlanejador
	WHERE 
		A.CodStatus = 0 AND Operador.LoginAd = @Login AND
		((A.NroAtividade NOT IN (SELECT NroAtividade FROM cte)) OR
		(A.NroAtividade IS NULL AND Operador.LoginAd NOT IN (SELECT LoginAd FROM cte)))
	
	UNION ALL
		SELECT * FROM cte
		ORDER BY 
		A.DataFinal, NroAtividade

END