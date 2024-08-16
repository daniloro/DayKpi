USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_CONSULTAR_ATIVIDADE_ATUACAO
      Objetivo : Obtem as atividades com atua��o no per�odo
      Analista : Lucas J
      Data     : 04/01/2021
      *** Hist�rico de Altera��es ****************************************
      Item  Data        Analista		Descri��o
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_CONSULTAR_ATIVIDADE_ATUACAO]
	@Login		varchar(8),
	@DataInicio	datetime,
	@DataFim	datetime
AS

BEGIN
	SET NOCOUNT ON	
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	SELECT 
		A.NroAtividade,
		A.DscAtividade,
		C.NroChamado,
		C.DscChamado,
		COUNT(AHC.NroAtividade) QtdConfirmacao,
		A.CodStatus
	FROM Atividade A
	INNER JOIN Chamado C ON C.NroChamado = A.NroChamado
	INNER JOIN Operador O ON A.Operador = O.Nome
	LEFT OUTER JOIN AtividadeHistoricoConfirmacao AHC ON AHC.NroAtividade = A.NroAtividade AND DataConfirmacao >= @DataInicio AND DataConfirmacao < @DataFim
	WHERE
		O.LoginAd = @Login AND
		((CodStatus = 0) OR (CodStatus = 1 AND DataConclusao >= @DataInicio AND DataConclusao < @DataFim))
	GROUP BY
		A.NroAtividade,
		A.DscAtividade,
		C.NroChamado,
		C.DscChamado,
		A.CodStatus
	ORDER BY 
		A.CodStatus DESC,
		A.NroAtividade

END