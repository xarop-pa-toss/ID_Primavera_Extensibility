using System;
using System.Diagnostics;
using System.Drawing;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Platform.Services;
using StdPlatBS100;
using ASRLB_ImportacaoFatura;

namespace Primavera.CustomRibbon
{
    public class PrimaveraRibbon : Plataforma
    {
        const string cIDTAB = "10000";
        const string cIDGROUP = "100001";
        const string cIDBUTTON1 = "1000011";
        const string cIDBUTTON2 = "1000012";
        private StdBSPRibbon RibbonEvents;
        ///
        /// This event will execute for all ribbon changes.
        ///
        ///
        public override void DepoisDeCriarMenus(ExtensibilityEventArgs e)
        {
            // Register the Ribbon events.
            RibbonEvents = PSO.Ribbon;
            RibbonEvents.Executa += RibbonEvents_Executa;
            // Create a new TAB.
            PSO.Ribbon.CriaRibbonTab("Ferramentas", cIDTAB, 15);
            // Create a new Group.
            PSO.Ribbon.CriaRibbonGroup(cIDTAB, "Importar Faturas", cIDGROUP);
            // Create a new 32x32 Button.
            PSO.Ribbon.CriaRibbonButton(cIDTAB, cIDGROUP, cIDBUTTON1, "Faturas TE", true, null);
            PSO.Ribbon.CriaRibbonButton(cIDTAB, cIDGROUP, cIDBUTTON2, "Importar de Excel", true, null);
        }
        ///
        /// Ribbon events.
        ///
        private void RibbonEvents_Executa(string Id, string Comando)
        {
            try
            {
                switch (Id)
                {
                    case cIDBUTTON1:
                        using (var result = BSO.Extensibility.CreateCustomFormInstance(typeof(janelaImportarFatura)))
                        {
                            new janelaImportarFatura();
                            (result.Result as janelaImportarFatura).ShowDialog();
                        }
                        break;
                    case cIDBUTTON2:
                        using (var result = BSO.Extensibility.CreateCustomFormInstance(typeof(ASRLB_ImportacaoFatura.Sales.janelaFaturasExploracao)))
                        {
                            new ASRLB_ImportacaoFatura.Sales.janelaFaturasExploracao();
                            (result.Result as ASRLB_ImportacaoFatura.Sales.janelaFaturasExploracao).ShowDialog();
                        }
                        break;
                }
            }
            catch (System.Exception ex)
            {
                PSO.Dialogos.MostraAviso("Falha a executar comando.", StdBSTipos.IconId.PRI_Informativo, ex.Message);
            }
        }
    }
}
