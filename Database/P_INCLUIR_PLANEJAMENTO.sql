USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_INCLUIR_PLANEJAMENTO
      Objetivo : Inclui um novo registro na tabela Planejamento
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_INCLUIR_PLANEJAMENTO]	
	@LoginAd				varchar(8),
	@DataPlanejamento		datetime,
	@CodTipoPlanejamento	int,
	@NroAtividade			varchar(14),
	@DscPlanejamento		varchar(500)
AS

BEGIN
	SET NOCOUNT ON

	IF NOT EXISTS(SELECT CodPlanejamento FROM Planejamento 
		WHERE LoginAd = @LoginAd AND DataPlanejamento = @DataPlanejamento AND 
				((@CodTipoPlanejamento <> 3 AND NroAtividade = @NroAtividade) OR (@CodTipoPlanejamento = 3 AND DscPlanejamento = @DscPlanejamento)))
	BEGIN
		INSERT INTO Planejamento (LoginAd, DataPlanejamento, CodTipoPlanejamento, NroAtividade, DscPlanejamento, DataUltimaAlteracao) 
		VALUES (@LoginAd, @DataPlanejamento, @CodTipoPlanejamento, @NroAtividade, @DscPlanejamento, getdate())		
	END
END
