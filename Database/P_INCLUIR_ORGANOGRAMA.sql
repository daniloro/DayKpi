USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_INCLUIR_ORGANOGRAMA
      Objetivo : Inclui um registro no organograma do Periodo
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_INCLUIR_ORGANOGRAMA]	
	@DataOrganograma		datetime,
	@Login					varchar(8),
	@Nome					varchar(50),
	@Grupo					varchar(50),
	@Gestor					varchar(8),
	@NomeGestor				varchar(50),
	@CodAnalise				int,
	@DataUltimaAlteracao	datetime
AS

BEGIN
	SET NOCOUNT ON

	IF EXISTS(SELECT LoginAd FROM Organograma WHERE DataOrganograma = @DataOrganograma AND LoginAd = @Login)	
	BEGIN
		UPDATE Organograma SET 
			Grupo = @Grupo, 
			Gestor = @Gestor, 
			NomeGestor = @NomeGestor, 
			CodAnalise = @CodAnalise, 
			DataUltimaAlteracao = getdate()
		WHERE DataOrganograma = @DataOrganograma AND LoginAd = @Login
	END
	ELSE
	BEGIN
		INSERT INTO Organograma (DataOrganograma, LoginAd, Nome, Grupo, Gestor, NomeGestor, CodAnalise, DataUltimaAlteracao) 
						VALUES (@DataOrganograma, @Login, @Nome, @Grupo, @Gestor, @NomeGestor, @CodAnalise, @DataUltimaAlteracao)
	END	
END
