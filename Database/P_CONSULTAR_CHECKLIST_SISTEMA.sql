USE [DCV_AVALIACAO]
GO
/*
      Procedure: P_CONSULTAR_CHECKLIST_SISTEMA
      Objetivo : Selecione o checklist do dia por grupo
      Analista : Lucas J
      Data     : 04/01/2021
      *** Histórico de Alterações ****************************************
      Item  Data        Analista		Descrição
      ********************************************************************
*/
CREATE PROCEDURE [dbo].[P_CONSULTAR_CHECKLIST_SISTEMA]
	@Login		varchar(8),
	@Tipo		int
AS

BEGIN
	SET NOCOUNT ON	
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	SELECT
		CL.CodItem,
		CodCheckList,
		Sistema,
		Modulo,
		Descricao,
		CL.Grupo,
		Nome,
		CodStatus,
		Observacao
	FROM CheckListSistema CL
	INNER JOIN OperadorGrupo ON OperadorGrupo.Grupo = CL.Grupo
	LEFT OUTER JOIN CheckListSistemaDia Dia ON Dia.CodItem = CL.CodItem AND DataItem = CONVERT(VARCHAR(10), GETDATE(), 120) AND Dia.Tipo = @Tipo
	LEFT OUTER JOIN Operador ON Operador.LoginAd = Dia.LoginAd
	WHERE
		OperadorGrupo.LoginAd = @Login AND
		CL.Tipo = @Tipo
	ORDER BY 
		CL.Grupo, Sistema, Modulo, Descricao

END


