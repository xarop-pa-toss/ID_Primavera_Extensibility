SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* -- Actualiza o Objetivo de acordo com o que for inserido na Margem e o valor em VendasAnoAnterior --
CREATE TRIGGER dbo.AtualizaObjetivos ON dbo.TDU_Objetivos AFTER UPDATE
AS 
BEGIN
	SET NOCOUNT ON;
	IF UPDATE (CDU_Margem)
	   BEGIN
	   
	   UPDATE obj
	   SET obj.CDU_Objetivo = obj.CDU_VendasAnoAnterior * (1 + (ins.CDU_Margem / 100))
	   FROM TDU_Objetivos obj
	   INNER JOIN inserted ins ON ins.CDU_ID = obj.CDU_ID
	END
END
*/


/* -- Apaga as linhas de objetivos para um vendedor se o seu estado VendedorMSS passar para False --
CREATE TRIGGER dbo.ApagaObjetivosInativos ON dbo.Vendedores AFTER UPDATE
AS
BEGIN
	SET NOCOUNT ON;
	IF UPDATE (CDU_VendedorMSS)
		DELETE FROM TDU_Objetivos 
		WHERE CDU_ID IN (
			SELECT CDU_ID 
			FROM TDU_Objetivos obj 
			INNER JOIN inserted ins ON obj.CDU_Vendedor = ins.Vendedor
			WHERE CDU_VendedorMSS = 'False')
END
GO*/


/* --Insere as linhas na tabela de Objetivos se o estado do VendedorMSS passar para True --
CREATE TRIGGER dbo.InsereObjetivosAtivos ON dbo.Vendedores AFTER UPDATE
AS
BEGIN
	IF UPDATE (CDU_VendedorMSS)
	BEGIN
		SET LANGUAGE Portuguese
		DECLARE @count BIGINT;
		SET @count = (SELECT MAX(CDU_ID) FROM TDU_Objetivos);

		SELECT s.Vendedor,
			s.DataInicio,
			s.DataFim,
			SUM(s.TotalVendas) AS VendasAnoAtual
		INTO #VendasAnoAtual_tempTable
		FROM 
			(SELECT TRY_CONVERT(int, ld.Vendedor) AS Vendedor,
				SUM(ld.PrecoLiquido) AS TotalVendas,
				DATEADD(YEAR, -1, DATEADD(DAY, 1, EOMONTH(ld.Data, -1))) AS DataInicio,
				DATEADD(YEAR, -1, EOMONTH(ld.Data)) AS DataFim
			FROM LinhasDoc ld
			INNER JOIN CabecDoc cd ON cd.Id = ld.IdCabecDoc
			INNER JOIN CabecDocStatus cds ON ld.IdCabecDoc = cds.IdCabecDoc
			INNER JOIN DocumentosVenda dv ON cd.TipoDoc = dv.Documento
			INNER JOIN Vendedores vd ON ld.Vendedor = vd.Vendedor
			WHERE dv.TipoDocumento = '4' AND cds.Anulado = '0' AND ld.Artigo <> '' AND YEAR(ld.data) = YEAR(GETDATE())AND ld.Vendedor NOT IN(0, 2, 13) AND ld.Vendedor IS NOT NULL
			GROUP BY ld.Vendedor, vd.Nome, ld.PrecoLiquido, MONTH(ld.Data), ld.Data ) AS s
		GROUP BY s.Vendedor, s.DataInicio, s.DataFim
		ORDER BY s.Vendedor, s.DataInicio
		SELECT * FROM #VendasAnoAtual_tempTable ORDER BY Vendedor, DataInicio

		INSERT INTO TDU_Objetivos (CDU_ID, CDU_Vendedor, CDU_Nome, CDU_DataInicio, CDU_DataFim, CDU_Descricao, CDU_VendasAnoAnterior, CDU_VendasAnoAtual)
		SELECT 
			@count + row_number() OVER (ORDER BY s.Vendedor) AS ID,
			s.Vendedor,
			s.Nome, 
			s.DataInicio, 
			s.DataFim, 
			s.Descricao, 
			ROUND(SUM(s.TotalVendas),2) AS VendasAnoAnterior, 
			ROUND(temp.VendasAnoAtual,2) AS VendasAnoAtual
		FROM (SELECT TRY_CONVERT(int, ld.Vendedor) AS Vendedor, vd.Nome AS Nome, SUM(ld.PrecoLiquido) AS TotalVendas,  DATEADD(DAY, 1, EOMONTH(ld.Data, -1)) AS DataInicio , EOMONTH(ld.Data) AS DataFim, DATENAME(MONTH, ld.Data) AS Descricao
			FROM LinhasDoc ld
			INNER JOIN CabecDoc cd ON cd.Id = ld.IdCabecDoc
			INNER JOIN CabecDocStatus cds ON ld.IdCabecDoc = cds.IdCabecDoc
			INNER JOIN DocumentosVenda dv ON cd.TipoDoc = dv.Documento
			INNER JOIN Vendedores vd ON ld.Vendedor = vd.Vendedor
			INNER JOIN inserted ins ON ld.Vendedor = ins.Vendedor
			WHERE dv.TipoDocumento = '4' AND cds.Anulado = '0' AND ld.Artigo <> '' AND YEAR(ld.data) = YEAR(GETDATE()) - 1 AND ld.Vendedor = ins.Vendedor
			GROUP BY ld.Vendedor, vd.Nome, ld.PrecoLiquido, MONTH(ld.Data), ld.Data
			) AS s
			LEFT JOIN #VendasAnoAtual_tempTable temp ON temp.Vendedor = s.Vendedor AND temp.DataInicio = s.DataInicio --AND temp.VendasAnoAtual IS NULL
		GROUP BY s.Vendedor, s.DataInicio, s.DataFim, s.Nome, s.Descricao, temp.VendasAnoAtual
		ORDER BY DataInicio, Vendedor
		DROP TABLE #VendasAnoAtual_tempTable
	END
END
GO */


/* -- JOB para Update à coluna ValoresAnoAtual --
USE [PRIDECANTE]
SET LANGUAGE Portuguese
DECLARE @count BIGINT;
SET @count = (SELECT MAX(CDU_ID) FROM TDU_Objetivos);

SELECT s.Vendedor,
	s.DataInicio,
	s.DataFim,
	SUM(s.TotalVendas) AS VendasAnoAtual
INTO #VendasAnoAtual_tempTable
FROM 
	(SELECT TRY_CONVERT(int, ld.Vendedor) AS Vendedor,
		SUM(ld.PrecoLiquido) AS TotalVendas,
		DATEADD(YEAR, -1, DATEADD(DAY, 1, EOMONTH(ld.Data, -1))) AS DataInicio,
		DATEADD(YEAR, -1, EOMONTH(ld.Data)) AS DataFim
	FROM LinhasDoc ld
	INNER JOIN CabecDoc cd ON cd.Id = ld.IdCabecDoc
	INNER JOIN CabecDocStatus cds ON ld.IdCabecDoc = cds.IdCabecDoc
	INNER JOIN DocumentosVenda dv ON cd.TipoDoc = dv.Documento
	INNER JOIN Vendedores vd ON ld.Vendedor = vd.Vendedor
	WHERE dv.TipoDocumento = '4' AND cds.Anulado = '0' AND ld.Artigo <> '' AND YEAR(ld.data) = YEAR(GETDATE())AND ld.Vendedor NOT IN(0, 2, 13) AND ld.Vendedor IS NOT NULL
	GROUP BY ld.Vendedor, vd.Nome, ld.PrecoLiquido, MONTH(ld.Data), ld.Data ) AS s
GROUP BY s.Vendedor, s.DataInicio, s.DataFim
ORDER BY s.Vendedor, s.DataInicio
SELECT * FROM #VendasAnoAtual_tempTable ORDER BY Vendedor, DataInicio

UPDATE TDU_Objetivos
SET TDU_Objetivos.CDU_VendasAnoAtual = temp.VendasAnoAtual
FROM TDU_Objetivos AS obj
LEFT JOIN #VendasAnoAtual_tempTable AS temp
ON obj.CDU_Vendedor = temp.Vendedor AND obj.CDU_DataInicio = temp.DataInicio

DROP TABLE #VendasAnoAtual_tempTable
		
