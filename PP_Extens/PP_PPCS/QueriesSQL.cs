using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP_PPCS
{
    class QueriesSQL
    {
        public static string Query01 { get; set; } = @"
            SELECT 
                scc.TipoEntidade, 
                scc.Entidade, 
                scc.Filial, 
                scc.TipoDoc, 
                scc.Serie, 
                scc.NumDoc, 
                CASE 
                    WHEN (ISNULL(ce.CDU_EntLocal,'')) = '' THEN scc.Entidade 
                    ELSE ce.CDU_EntLocal 
                END AS EntLocal, 
                scc.Filial AS FilialLoc, 
                CASE 
                    WHEN (ISNULL(cc.TipoDoc,'')) = '' THEN di.CDU_DocDestino 
                    ELSE cc.TipoDoc 
                END AS TipoDocLoc, 
                cc.Serie AS SerieLoc, 
                cc.NumDoc AS NumDocLocal,  
                CASE 
                    WHEN (ISNULL(cc.DataDoc,'')) = '' THEN scc.CDU_DataStock 
                    ELSE cc.DataDoc 
                END AS Data, 
                CASE 
                    WHEN sccs.Anulado = 1 THEN 'A' 
                    WHEN (ISNULL(cc.NumDoc,0)) = 0 THEN 'S' 
                    ELSE 'N' 
                END AS Importa 
            INTO 
               {0}
            FROM 
                Servidor1.Priportipesca.dbo.CabecCompras scc 
            INNER JOIN 
                Servidor1.Priportipesca.dbo.CabecComprasStatus sccs ON scc.id = sccs.IdCabecCompras 
            INNER JOIN 
                TDU_DocumentosImportaveis di ON scc.TipoDoc = di.CDU_Documento AND 'C' = di.CDU_TipoDocumento 
            LEFT OUTER JOIN 
                CabecCompras cc ON  scc.Filial = cc.CDU_FilialOrig AND scc.TipoDoc = cc.CDU_TipoDocOrig 
                AND scc.Serie = cc.CDU_SerieOrig AND scc.NumDoc = cc.CDU_NumDocOrig 
            LEFT OUTER JOIN 
                TDU_CorrespondenciaEntidades ce ON scc.Entidade = ce.CDU_EntERP AND N'F' = ce.CDU_TipoEntidade 
            LEFT OUTER JOIN 
                Fornecedores fo ON ce.CDU_EntLocal = fo.Fornecedor 
            WHERE 
                (scc.DataDoc = CONVERT(DATE, '{1}', 105) OR (ISNULL(cc.DataDoc,'') = CONVERT(DATE, '{1}', 105)));
            ";

        public static string Query02 { set; get; } = @"
            INSERT INTO 
                {0} (TipoEntidade, Entidade, Filial, TipoDoc, Serie, NumDoc, EntLocal, FilialLoc, TipoDocLoc, SerieLoc, NumDocLocal, Data, Importa) 
            SELECT 
                scd.TipoEntidade, 
                scd.Entidade, 
                scd.Filial, 
                scd.TipoDoc, 
                scd.Serie, 
                scd.NumDoc,  
                CASE 
                    WHEN (ISNULL(ce.CDU_EntLocal, '')) = '' THEN scd.Entidade 
                    ELSE ce.CDU_EntLocal 
                END AS EntLocal, 
                scd.Filial AS FilialLoc, 
                CASE 
                    WHEN (ISNULL(cd.TipoDoc, '')) = '' THEN di.CDU_DocDestino 
                    ELSE cd.TipoDoc 
                END AS TipoDocLoc, 
                cd.Serie AS SerieLoc, 
                cd.NumDoc AS NumDocLocal, 
                cd.Data AS Data, 
                CASE 
                    WHEN scds.Anulado = 1 THEN 'A' 
                    WHEN (ISNULL(cd.NumDoc,0)) = 0 THEN 'S' 
                    ELSE 'N' 
                END AS Importa  
            FROM 
                Servidor1.Priportipesca.dbo.CabecDoc scd 
            INNER JOIN 
                Servidor1.Priportipesca.dbo.CabecDocStatus scds ON scd.Id = scds.IdCabecDoc  
            INNER JOIN 
                TDU_DocumentosImportaveis di ON scd.TipoDoc = di.CDU_Documento AND 'V' = di.CDU_TipoDocumento 
            LEFT OUTER JOIN 
                CabecDoc cd ON  scd.Filial = cd.CDU_FilialOrig AND scd.TipoDoc = cd.CDU_TipoDocOrig 
                AND scd.Serie = cd.CDU_SerieOrig AND scd.NumDoc = cd.CDU_NumDocOrig 
            LEFT OUTER JOIN 
                TDU_CorrespondenciaEntidades ce ON scd.Entidade = ce.CDU_EntERP AND N'C' = ce.CDU_TipoEntidade 
            LEFT OUTER JOIN 
                Clientes cl ON ce.CDU_EntLocal = cl.Cliente 
            WHERE 
                scd.Data = CONVERT(DATE, '{1}', 105);
            ";

        public static string GetQuery01(string tabela, string data)
        {
            return string.Format(Query01, tabela, data);
        }

        public static string GetQuery02(string tabela, string data)
        {
            return string.Format(Query02, tabela, data);
        }
    }



}
