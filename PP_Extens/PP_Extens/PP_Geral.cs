using Primavera.Extensibility.BusinessEntities; using Primavera.Extensibility.CustomCode;
using System; using System.Collections.Generic; using System.Linq; using System.Text; using System.Threading.Tasks; using System.Runtime.InteropServices;
using StdBE100; using StdPlatBS100;

// Conversão de PORTIPESCA/Modules/Geral
namespace PP_Extens
{
    public class PP_Geral : CustomCode
    {
        public static string GetGUID()
        {
            Guid x = Guid.NewGuid();
            return x.ToString();
        }

        public string nz <T> (ref T s)
        {
            if (EqualityComparer<T>.Default.Equals(s, default)) { return null; }
            else { return s.ToString(); }
        }

        public double nzn <T> (ref T n)
        {
            if (EqualityComparer<T>.Default.Equals(n, default(T))) { return 0; }
            else { return Convert.ToDouble(n); }
        }


        // 'enum's ajudam a restringir quais os argumentos passados a uma função.
        public enum MesAnt
        {
            Inicio,
            Fim
        }

        public static string MesAnterior(MesAnt t)
        {
            DateTime data;
            StdBSMensagensDialogos Dialogos = new StdBSMensagensDialogos();

            // Pede e valida data. 
            string s = "";

            while (true)
            {
                Dialogos.MostraDialogoInput(ref s, "Mes Anterior", "Introduzir Data");
                try { data = Convert.ToDateTime(s); break; }
                catch { Dialogos.MostraAviso("Data inválida!", StdBSTipos.IconId.PRI_Exclama); continue; }
            }

            //Transforma em ShortDate no dia 1 de há um mês atrás
            if (t == MesAnt.Inicio)
            {
                data = data.AddMonths(-1);
                data = data.AddDays(data.Day - (data.Day + 1));
                s = data.ToString("d");
                return s;
            }
            //Transforma em ShortDate no último dia de há um mês atrás
            else if (t == MesAnt.Fim)
            {
                data = data.AddMonths(-1);
                int diasMes = DateTime.DaysInMonth(data.Year, data.Month);
                data = data.AddDays(data.Day + ( diasMes - data.Day));
                s = data.ToString("d");
                return s;
            }
            // Não activa mas todos os caminhos possíveis têm de retornar valores.
            return s;
        }

        //public string Avaliar (string formula)
        //{
        //    #region deprecated
        //    /* Código original convertido para .NET. MSScriptControl não estva a funcioonar por isso vai ser convertido
        //    MSScriptControl.ScriptControl sc = new MSScriptControl.ScriptControl();
        //    try
        //    {
        //        // Check espaço que a variável ocupa em memória. 0 = vazia. Talvez desnecessário em .NET?
        //        if (Marshal.SizeOf(formula) == 0 || formula == null) { sc = null; return null; }
        //        sc.Language = "VBSCRIPT";
        //        return Convert.ToString(sc.Eval(formula));
        //    }
        //    catch
        //    {
        //        sc = null; return null;
        //    }
        //    */
        //    #endregion
        //}

        public string ConstructorDescricaoEmLista(Dictionary<string, string> armazemDict)
        {
            StringBuilder descricaoBuilder = new StringBuilder();

            foreach (var kvp in armazemDict)
            {
                descricaoBuilder.AppendLine($"{kvp.Key} - {kvp.Value}");
            }
            return descricaoBuilder.ToString();
        }

        public bool UnidadeCaixa(string unidade)
        {
            // Devolve True se os dois caractéres do centro da string forem numeros. Usado abaixo.
            bool isNumeric = int.TryParse(unidade.Substring(unidade[unidade.Length / 2], 2), out _);
            // Devolve True se a primeira letra for C e isNumeric for true (verifica a integridade da UnidadeCaixa)
            return (unidade.Substring(0, 1) == "C" && isNumeric);
        }

        public double ObterKgDaUnidade (string unidade)
        {
            if ( UnidadeCaixa(unidade)) { double.TryParse(unidade.Substring(unidade[unidade.Length / 2], 2), out double num); return num; }
            else { return -1; }
        }
    }
}

