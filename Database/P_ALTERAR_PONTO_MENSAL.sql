USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_ALTERAR_PONTO_MENSAL
      Objetivo : Altera um registro na tabela PontoMensal
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_ALTERAR_PONTO_MENSAL]	
	@CodPontoMensal		int,
	@LoginGestor		varchar(8),
	@Atraso				int,
	@CinquentaPorcento	int,
	@CemPorcento		int,
	@Noturno			int,
	@Observacao			varchar(500)
AS

BEGIN
	SET NOCOUNT ON

	UPDATE PontoMensal SET 
		LoginGestor = @LoginGestor,
		Atraso = @Atraso,
		CinquentaPorcento = @CinquentaPorcento,
		CemPorcento = @CemPorcento,
		Noturno = @Noturno,
		DataAtualizacao = getdate(),
		Observacao = @Observacao
	WHERE 
		CodPontoMensal = @CodPontoMensal

END
