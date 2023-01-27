﻿using Primavera.Extensibility.BusinessEntities;
using Primavera.Extensibility.CustomCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASRLB_ImportacaoFatura.Sales
{
    public class OpenFormCode : CustomCode
    {
        public void Abrir_janelaFaturasExploracao_WF()
        {
            formFaturasExploracao_WF form = new formFaturasExploracao_WF();
            form.ShowDialog();
            PSO.UI.AdicionaFormMDI(form);
        }

        //public void Abrir_janelaImportarFatura()
        //{
        //    janelaImportarFatura form1 = new janelaImportarFatura();
        //    form1.Show();
        //    PSO.UI.AdicionaFormMDI(form1);
        //}

    }
}