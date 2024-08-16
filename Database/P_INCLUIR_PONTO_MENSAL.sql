USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_INCLUIR_PONTO_MENSAL
      Objetivo : Inclui os dados do ponto mensal do Operador
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_INCLUIR_PONTO_MENSAL]
	@DataPonto			datetime,
	@LoginAd			varchar(8),
	@LoginGestor		varchar(8),
	@Atraso				int,
	@CinquentaPorcento	int,
	@CemPorcento		int,
	@Noturno			int,
	@Observacao			varchar(500)
AS

BEGIN
	SET NOCOUNT ON	
	
	INSERT INTO PontoMensal (DataPonto, LoginAd, LoginGestor, Atraso, CinquentaPorcento, CemPorcento, Noturno, DataAtualizacao, Observacao)
		VALUES (@DataPonto, @LoginAd, @LoginGestor, @Atraso, @CinquentaPorcento, @CemPorcento, @Noturno, getdate(), @Observacao)
	
END