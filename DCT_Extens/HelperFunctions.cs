using Primavera.Extensibility;
using Primavera.Extensibility.BusinessEntities;
using Primavera.Extensibility.CustomCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErpBS100; using StdPlatBS100; using BasBE100; using StdBE100;


namespace DCT_Extens
{
    public class HelperFunctions : CustomCode
    {
        public void TDU_Insert(string NomeTDU, Dictionary<string, string> Dict, ErpBS BSO)
        {
            // Criar Campos para RegistoUtil para então inserir na TDU
            // https://v10api.primaverabss.com/html/api/plataforma/StdBE100.StdBETipos.EnumTipoCampo.html

            StdBERegistoUtil registoUtil = new StdBERegistoUtil();
            StdBECampos linha = new StdBECampos();

            foreach (KeyValuePair<string, string> kvp in Dict)
            {
                StdBECampo campo = new StdBECampo();
                campo.Nome = kvp.Key;
                campo.Valor = kvp.Value;

                linha.Add(campo);
            }
            registoUtil.EmModoEdicao = true;
            registoUtil.Campos = linha;
            registoUtil.EmModoEdicao = false;

            BSO.TabelasUtilizador.Actualiza(NomeTDU, registoUtil);
        }
    }
}

