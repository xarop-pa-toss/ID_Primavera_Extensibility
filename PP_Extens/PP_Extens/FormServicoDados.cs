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


        public static string Descricao
        {
            get
            {
                string valor = GetOuDefeito("Descricao", "Descricao_Defeito");
                LimparKey("Descricao");
                return valor;
                
            }
            set => FormDados["Descricao"] = value;
        }

        public static string ValorDefeito
        {
            get
            {
                string valor = GetOuDefeito("ValorDefeito", "0");
                LimparKey("ValorDefeito");
                return valor;

            }
            set => FormDados["ValorDefeito"] = value;
        }


        // Custom method to get a value with a fallback
        private static string GetOuDefeito(string key, string defeito)
        {
            return FormDados.TryGetValue(key, out var value) ? value : defeito;
        }

        // Limpa chave se existir. Garante que ao utilizar Forms não se transportem valores de forms anteriores para os novos (por ser static)
        private static void LimparKey(string key)
        {
            if (FormDados.ContainsKey(key))
            {
                FormDados.Remove(key);
            }
        }
    }
}
