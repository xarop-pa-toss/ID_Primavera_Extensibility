using System;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperFunctionsPrimavera10
{
    public interface ISecrets
    {
        string Empresa();
        string Utilizador();
        string Password();

        string BDServidorInstancia();
        string BDNomeDB();
        string BDUtilizador();
        string BDPassword();
    }
}
