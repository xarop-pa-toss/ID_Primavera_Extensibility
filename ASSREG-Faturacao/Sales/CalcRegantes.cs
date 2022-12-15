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
        private string _ultimaLeitura, _dataFull, _cultura, _leitura1, _leitura2, _leitura3;
        private int _ano;
        private decimal _consumoTotal, _hectares;
        private decimal _consumo1, _consumo2, _consumo3, _taxa1, _taxa2, _taxa3;

        public CalcRegantes(Dictionary<string, string> linhaDict)
        {
            _cultura = linhaDict["Cultura"];
            _dataFull = linhaDict["Data1"];
            _leitura1 = linhaDict["Leitura1"];
            _leitura2 = linhaDict["Leitura2"];
            _leitura3 = linhaDict["Leitura3"];
            _ultimaLeitura = linhaDict["UltimaLeitura"];
            _hectares = Convert.ToDecimal(linhaDict["Area"]);

            //int
            _ano = Convert.ToDateTime(_dataFull).Year;
            //float
            _consumoTotal = Convert.ToInt32(_leitura3) - Convert.ToInt32(_ultimaLeitura);

            
            if (linhaDict["TRH"].Equals('S') && linhaDict["TaxaPenalizadora"].Equals('S'))
            {
                //Define _consumo1, _consumo2, _consumo3
                ConsumosRegantes();
                //Define _taxa1, _taxa2, _taxa3 a serem aplicadas a cada consumo. _taxa1 é a mais baixa da tabela se _ano = 2022;
                AplicarTaxas();

                linhaDict["Taxa1"] = _taxa1.ToString();
                linhaDict["Consumo1"] = _consumo1.ToString();
                linhaDict["Taxa2"] = _taxa2.ToString();
                linhaDict["Consumo2"] = _consumo2.ToString();
                linhaDict["Taxa3"] = _taxa3.ToString();
                linhaDict["Consumo3"] = _consumo3.ToString();
            }
        }

        private void ConsumosRegantes()
        {
            // Divisão do consumo total pelos três escalões. Uma quantidade para cada escalão. Será ignorada se for zero.
            // Se houver mais que 7000 de consumo, 5000 ficam no primeiro escalão, 2000 (diferença entre 5000 e 7000) ficam no segundo e o restante no terceiro.
            decimal consumoCorrente;

            consumoCorrente = 5000 - _consumoTotal;
            if (consumoCorrente < 0) { _consumo1 = 5000; consumoCorrente = -(consumoCorrente); } else { _consumo1 = consumoCorrente; }

            consumoCorrente = 2000 - consumoCorrente;
            if (consumoCorrente < 0) { _consumo2 = 2000; consumoCorrente = -(consumoCorrente); } else { _consumo2 = 0; }

            if (consumoCorrente < 0) { _consumo3 = consumoCorrente; } else { _consumo3 = 0; }

            _consumo1 = _consumo1 / _hectares;
            _consumo2 = _consumo2 / _hectares;
            _consumo3 = _consumo3 / _hectares;

        }

        private void AplicarTaxas()
        {
            StdBELista listaTaxa = BSO.Consulta("SELECT * FROM TDU_TaxaPenalizadora WHERE Cultura = '" + _cultura + "'");
            listaTaxa.Inicio();
            _taxa1 = listaTaxa.Valor("CDU_ate5000");
            _taxa2 = listaTaxa.Valor("CDU_5000a7000");
            _taxa3 = listaTaxa.Valor("CDU_maior7000");

            if (_ano == 2022)
            {
                listaTaxa = BSO.Consulta("SELECT CDU_ate5000 FROM TDU_TaxaPenalizadora WHERE Predio = 'PD'");
                listaTaxa.Inicio();
                _taxa1 = listaTaxa.Valor("CDU_ate5000");
            }

            listaTaxa.Dispose();
        }

        private void DadosFatura()
        {

        }
    }
}
