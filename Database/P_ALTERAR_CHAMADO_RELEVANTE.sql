USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_ALTERAR_CHAMADO_RELEVANTE
      Objetivo : Define se um chamado é Relevante ou não
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_ALTERAR_CHAMADO_RELEVANTE]
	@NroChamado				varchar(14),
	@Relevante				bit
AS

BEGIN	

	UPDATE Chamado SET IndImportante = @Relevante WHERE NroChamado = @NroChamado

END
