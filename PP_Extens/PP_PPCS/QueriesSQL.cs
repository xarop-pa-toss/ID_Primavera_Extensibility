﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP_PPCS
{
    class QueriesSQL
    {
        // Set ambiente (teste ou produção)
        private static string _servidor;
        public QueriesSQL(string servidor)
        {
            if (servidor == "teste") { _servidor = "Primavera_P10"; }
            else if (servidor == "prod") { _servidor = "Servidor1"}
            _servidor = servidor;
        }

        private static readonly string Query01 = @"
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
               {1}
            FROM 
                {0}.Priportipesca.dbo.CabecCompras scc 
            INNER JOIN 
                {0}.Priportipesca.dbo.CabecComprasStatus sccs ON scc.id = sccs.IdCabecCompras 
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
                (scc.DataDoc = CONVERT(DATE, '{2}', 105) OR (ISNULL(cc.DataDoc,'') = CONVERT(DATE, '{2}', 105)));
            ";

        private static readonly string Query02 = @"
            INSERT INTO 
                {1} (TipoEntidade, Entidade, Filial, TipoDoc, Serie, NumDoc, EntLocal, FilialLoc, TipoDocLoc, SerieLoc, NumDocLocal, Data, Importa) 
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
                {0}.Priportipesca.dbo.CabecDoc scd 
            INNER JOIN 
                {0}.Priportipesca.dbo.CabecDocStatus scds ON scd.Id = scds.IdCabecDoc  
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
                scd.Data = CONVERT(DATE, '{2}', 105);";

        private static readonly string Query03 = @"
            SELECT TipoEntidade, Entidade, Filial, TipoDoc, Serie, NumDoc, EntLocal, FilialLoc, TipoDocLoc, SerieLoc, NumDocLocal, Data, Importa 
            FROM {1} 
            ORDER BY By TipoEntidade, Filial, TipoDoc, Serie, NumDoc;";

        private static readonly string Query04 = @"
            SELECT * 
            FROM {0}.PriPortipesca.dbo.CabecCompras 
            WHERE Filial = '{1}' 
                AND TipoDoc = '{2}' 
                AND Serie = '{3}' 
                AND NumDoc = {4};";

        private static readonly string Query05 = @"
            SELECT * 
            FROM {0}.PriPortipesca.dbo.CabecCompras 
            WHERE 
                Filial = '{1}' 
                AND TipoDoc = '{2}' 
                AND Serie = '{3}' 
                AND NumDoc = {4};";

        private static readonly string Query06 = @"
            SELECT CASE WHEN tca.CDU_ArtigoDestino IS NULL THEN '' ELSE tca.CDU_ArtigoDestino END AS ArtigoDestino, lc.*, cc.DescEntidade, cc.DescPag 
            FROM {0}.PriPortipesca.dbo.LinhasCompras lc 
                INNER JOIN {0}.PriPortipesca.dbo.CabecCompras cc ON lc.IdCabecCompras = cc.Id 
                LEFT OUTER JOIN TDU_CorrespondenciaArtigos tca ON lc.Artigo = tca.CDU_ArtigoOriginal 
            WHERE 
                cc.Filial = '{1}' 
                AND cc.TipoDoc = '{2}' 
                AND cc.Serie = '{3}' 
                AND cc.NumDoc = {4} 
                AND lc.CDU_Pescado = 1 
            ORDER BY NumLinha;";

        private static readonly string Query07 = @"
            SELECT *
            FROM {0}.PriPortipesca.dbo.CabecDoc 
            WHERE 
                Filial = '{1}' 
                AND cc.TipoDoc = '{2}' 
                AND cc.Serie = '{3}' 
                AND cc.NumDoc =  {4};";

        private static readonly string Query08 = @"
            SELECT IvaIncluido 
            FROM {0}.PriPortipesca.dbo.CabecDoc 
            WHERE 
                cc.TipoDoc = '{1}' 
                AND cc.Serie = '{2}' 
                AND cc.NumDoc =  {3};";

        private static readonly string Query09 = @"
            SELECT CASE WHEN tca.CDU_ArtigoDestino IS NULL THEN '' ELSE tca.CDU_ArtigoDestino END AS ArtigoDestino, ld.*, cd.DescEntidade, cd.DescPag 
            FROM {0}.PriPortipesca.dbo.LinhasDoc ld 
                INNER JOIN {0}.PriPortipesca.dbo.CabecDoc cd On ld.IdCabecDoc = cd.Id 
                LEFT OUTER JOIN TDU_CorrespondenciaArtigos tca On ld.Artigo = tca.CDU_ArtigoOriginal 
            WHERE cd.Filial = '{1}' 
                AND cd.TipoDoc = '{2}' 
                AND cd.Serie = '{3}' 
                AND cd.NumDoc = {4} 
                AND ld.CDU_Pescado = 1
            ORDER BY NumLinha;";


        public static string GetQuery01(string tabela, string data)
        {
            return string.Format(Query01, _servidor, tabela, data);
        }

        public static string GetQuery02(string tabela, string data)
        {
            return string.Format(Query02, _servidor, tabela, data);
        }

        public static string GetQuery03(string tabela, string data)
        {
            return string.Format(Query03, _servidor, tabela, data);
        }

        public static string GetQuery04(string filial, string tipoDoc, string serie, string numDoc)
        {
            return string.Format(Query04, _servidor, filial, tipoDoc, serie, numDoc);
        }

        public static string GetQuery05(string filial, string tipoDoc, string serie, string numDoc)
        {
            return string.Format(Query05, _servidor, filial, tipoDoc, serie, numDoc);
        }

        public static string GetQuery06(string filial, string tipoDoc, string serie, string numDoc)
        {
            return string.Format(Query06, _servidor, filial, tipoDoc, serie, numDoc);
        }

        public static string GetQuery07(string filial, string tipoDoc, string serie, string numDoc)
        {
            return string.Format(Query07, _servidor, filial, tipoDoc, serie, numDoc);
        }

        public static string GetQuery08(string tipoDoc, string serie, string numDoc)
        {
            return string.Format(Query08, _servidor, tipoDoc, serie, numDoc);
        }

        public static string GetQuery09(string filial, string tipoDoc, string serie, string numDoc)
        {
            return string.Format(Query09, _servidor, filial, tipoDoc, serie, numDoc);
        }
    }
}




