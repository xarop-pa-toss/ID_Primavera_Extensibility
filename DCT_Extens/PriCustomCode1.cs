using Primavera.Extensibility.BusinessEntities;
using Primavera.Extensibility.CustomCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCT_Extens
{
    public class PriCustomCode1 : CustomCode
    {
        public PriCustomCode1() 
        {
            var form = new FormCustom();

            form.ShowDialog();
        }
    }
}
