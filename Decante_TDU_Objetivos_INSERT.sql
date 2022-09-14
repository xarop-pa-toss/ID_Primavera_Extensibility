--Popula tabela TDU_Objetivos

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
	WHERE dv.TipoDocumento = '4' AND cds.Anulado = '0' AND ld.Artigo <> '' AND YEAR(ld.data) = YEAR(GETDATE()) AND ld.Vendedor NOT IN(0, 2, 13) AND ld.Vendedor IS NOT NULL
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
	WHERE dv.TipoDocumento = '4' AND cds.Anulado = '0' AND ld.Artigo <> '' AND YEAR(ld.data) = YEAR(GETDATE()) - 1 AND ld.Vendedor NOT IN(0, 2, 13) AND ld.Vendedor IS NOT NULL
	GROUP BY ld.Vendedor, vd.Nome, ld.PrecoLiquido, MONTH(ld.Data), ld.Data
	) AS s
	LEFT JOIN #VendasAnoAtual_tempTable temp ON temp.Vendedor = s.Vendedor AND temp.DataInicio = s.DataInicio AND temp.DataFim = s.DataFim --AND temp.VendasAnoAtual IS NULL
GROUP BY s.Vendedor, s.DataInicio, s.DataFim, s.Nome, s.Descricao, temp.VendasAnoAtual
ORDER BY DataInicio, Vendedor

DROP TABLE #VendasAnoAtual_tempTable
SELECT * FROM TDU_Objetivos
DELETE TDU_Objetivos
