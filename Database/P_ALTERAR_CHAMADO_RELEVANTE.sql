USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_ALTERAR_CHAMADO_RELEVANTE
      Objetivo : Define se um chamado � Relevante ou n�o
      Analista : Lucas J
      Data     : 04/01/2021
      *** Hist�rico de Altera��es ****************************************
      Item  Data        Analista		Descri��o
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_ALTERAR_CHAMADO_RELEVANTE]
	@NroChamado				varchar(14),
	@Relevante				bit
AS

BEGIN	

	UPDATE Chamado SET IndImportante = @Relevante WHERE NroChamado = @NroChamado

END
