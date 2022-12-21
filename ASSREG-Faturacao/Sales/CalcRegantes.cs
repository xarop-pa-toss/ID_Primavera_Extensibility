using Primavera.Extensibility;
using Primavera.Extensibility.BusinessEntities;
using Primavera.Extensibility.Integration;
using Primavera.Extensibility.Sales.Editors;
using StdBE100;
using VndBE100;
using BasBE100;
using StdPlatBS100;
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataRow = System.Data.DataRow;


namespace ASRLB_ImportacaoFatura.Sales
{
    public class CalcRegantes : EditorVendas
    {
        private string _dataFull, _cultura;
        private int _ano, _consumoTotal, _ultimaLeitura, _leitura1, _leitura2, _leitura3;
        private int _escalao1, _escalao2;
        private decimal  _taxa1, _taxa2, _taxa3;
        private double _consumo1, _consumo2, _consumo3, _hectares;

        public CalcRegantes(Dictionary<string, string> linhaDict)
        {
            _cultura = linhaDict["Cultura"];
            _dataFull = linhaDict["Data1"];
            _leitura1 = Convert.ToInt32(linhaDict["Leitura1"]);
            _leitura2 = Convert.ToInt32(linhaDict["Leitura2"]);
            _leitura3 = Convert.ToInt32(linhaDict["Leitura3"]);
            _ultimaLeitura = Convert.ToInt32(linhaDict["UltimaLeitura"]);

            //int
            _ano = Convert.ToDateTime(_dataFull).Year;
            _consumoTotal = ConsumoTotal(linhaDict);
            linhaDict["Consumo"] = _consumoTotal.ToString();

            if (linhaDict["TRH"].Equals('S') && linhaDict["TaxaPenalizadora"].Equals('S'))
            {
                //Define _consumo1, _consumo2, _consumo3
                ConsumosRegantes();
                //Define _taxa1, _taxa2, _taxa3 a serem aplicadas a cada consumo. _taxa1 � a mais baixa da tabela se _ano = 2022;
                TaxasPenalizadoras();

                linhaDict["Taxa1"] = _taxa1.ToString();
                linhaDict["Consumo1"] = _consumo1.ToString();
                linhaDict["Taxa2"] = _taxa2.ToString();
                linhaDict["Consumo2"] = _consumo2.ToString();
                linhaDict["Taxa3"] = _taxa3.ToString();
                linhaDict["Consumo3"] = _consumo3.ToString();
            }
        }

        private int ConsumoTotal(Dictionary<string, string> linhaDict)
        {
            if (linhaDict["Leitura1"] == null)
            {
                linhaDict["DataLeituraFinal"] = "0";
                linhaDict["LeituraFinal"] = "0";
                linhaDict["TotalLeituras"] = "0";
                return 0; 
            }

            if (linhaDict["Leitura2"] == null)
            { 
                linhaDict["DataLeituraFinal"] = linhaDict["Data1"];
                linhaDict["LeituraFinal"] = linhaDict["Leitura1"];
                linhaDict["TotalLeituras"] = "1";
                return _leitura1 - _ultimaLeitura; }

            if (linhaDict["Leitura3"] == null) 
            { 
                linhaDict["DataLeituraFinal"] = linhaDict["Data2"];
                linhaDict["LeituraFinal"] = linhaDict["Leitura2"];
                linhaDict["TotalLeituras"] = "2";
                return _leitura2 - _ultimaLeitura; 
            } 
            else 
            { 
                linhaDict["DataLeituraFinal"] = linhaDict["Data3"];
                linhaDict["LeituraFinal"] = linhaDict["Leitura3"];
                linhaDict["TotalLeituras"] = "3";
                return _leitura3 - _ultimaLeitura; }
            }

        private void ConsumosRegantes()
        {
            // Separa��o do consumo total pelos tr�s escal�es. Preenche o 1� escal�o at� ao seu limite antes de ir pro 2�. Ser� ignorado se for zero.
            // Se houver mais que 7000 de consumo, 5000 ficam no primeiro escal�o, 2000 (diferen�a entre 5000 e 7000) ficam no segundo e o restante no terceiro.
            if (_cultura != "CA") 
            { _escalao1 = 5000; _escalao2 = 2000; }
            else 
            { _escalao1 = 12000; _escalao2 = 2000; }

            double consumoCorrente;

            consumoCorrente = _escalao1 - _consumoTotal;
            if (consumoCorrente < 0) { _consumo1 = _escalao1; consumoCorrente = -(consumoCorrente); } else { _consumo1 = consumoCorrente; }

            consumoCorrente = _escalao2 - consumoCorrente;
            if (consumoCorrente < 0) { _consumo2 = _escalao2; consumoCorrente = -(consumoCorrente); } else { _consumo2 = 0; }

            if (consumoCorrente < 0) { _consumo3 = consumoCorrente; } else { _consumo3 = 0; }

            /* ERRADO ??!?!?!?!
            _consumo1 = _consumo1 / _hectares;
            _consumo2 = _consumo2 / _hectares;
            _consumo3 = _consumo3 / _hectares;
            */
        }

        private void TaxasPenalizadoras()
        {
            StdBELista listaTaxa = BSO.Consulta("SELECT * FROM TDU_TaxaPenalizadora WHERE Cultura = '" + _cultura + "'");
            listaTaxa.Inicio();

            if (_cultura != "CA")
            {
                _taxa1 = listaTaxa.Valor("CDU_ate5000");
                _taxa2 = listaTaxa.Valor("CDU_5000a7000");
                _taxa3 = listaTaxa.Valor("CDU_maior7000");
            }
            else
            {
                _taxa1 = listaTaxa.Valor("CDU_ate12000");
                _taxa2 = listaTaxa.Valor("CDU_12000a14000");
                _taxa3 = listaTaxa.Valor("CDU_maior14000");
            }

            if (_ano == 2022)
            {
                listaTaxa = BSO.Consulta("SELECT CDU_ate5000 FROM TDU_TaxaPenalizadora WHERE Predio = 'PD'");
                listaTaxa.Inicio();
                _taxa1 = listaTaxa.Valor("CDU_ate5000");
            }

            listaTaxa.Dispose();
        }
    }
}
