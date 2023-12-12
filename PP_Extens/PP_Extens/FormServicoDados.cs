using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP_Extens
{
    public class FormServicoDados
    {
        private static Dictionary<string, string> FormDados = new Dictionary<string, string>();

        public static string Titulo
        {
            get => GetOuDefeito("Titulo", "Titulo_Defeito");
            set => FormDados["Titulo"] = value;
        }
        public static string Descricao
        {
            get => GetOuDefeito("Descricao", "Descricao_Defeito");
            set => FormDados["Descricao"] = value;
        }
        public static string ValorDefeito
        {
            get => GetOuDefeito("ValorDefeito", "0");
            set => FormDados["ValorDefeito"] = value;
        }
        public static string Resposta 
        {
            get => GetOuDefeito("Resposta", "Resposta_Defeito");
            set => FormDados["Resposta"] = value; 
        }

        // Custom method to get a value with a fallback
        private static string GetOuDefeito(string key, string defeito)
        {
            return FormDados.TryGetValue(key, out var value) ? value : defeito;
        }

        public static void Limpar()
        {
            FormDados.Clear();
        }
        //// Limpa chave se existir. Garante que ao utilizar Forms não se transportem valores de forms anteriores para os novos (por ser static)
        //private static void LimparKey(string key)
        //{
        //    if (FormDados.ContainsKey(key))
        //    {
        //        FormDados.Remove(key);
        //    }
        //}
    }
}
