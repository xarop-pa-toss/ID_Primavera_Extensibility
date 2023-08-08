using System;
using ErpBS100;
using StdBE100; using StdPlatBS100; using static StdBE100.StdBETipos;


namespace FRU_AlterarTerceiros.Motor
{
    sealed class PriEngine : BasBE100.BasBaseBE
    {
        #region  Singleton pattern 

        // .NET guarantees thread safety for static initialization
        private static readonly PriEngine engineInstance = new PriEngine();

        /// <summary>
        /// Private constructor
        /// </summary>
        private PriEngine()
        {
        }

        public static PriEngine CreateContext(string Company, string User, string Password)
        {
            StdBSConfApl objAplConf = new StdBSConfApl();
            StdPlatBS Plataforma = new StdPlatBS();
            ErpBS MotorLP = new ErpBS();

            EnumTipoPlataforma objTipoPlataforma;
            objTipoPlataforma = EnumTipoPlataforma.tpProfissional;

            objAplConf.Instancia = Company;
            objAplConf.AbvtApl = "ERP";
            objAplConf.Utilizador = User;
            objAplConf.PwdUtilizador = Password;
            objAplConf.LicVersaoMinima = "10.00";

            StdBETransaccao objStdTransac = new StdBETransaccao();

            try {
                Plataforma.AbrePlataformaEmpresa(Company, objStdTransac, objAplConf, objTipoPlataforma);
            }
            catch (Exception ex) {
                System.Windows.Forms.MessageBox.Show("Não foi possivel abrir a plataforma/empresa.");
                throw (ex);
            }

            if (Plataforma.Inicializada) {
                MotorLP.AbreEmpresaTrabalho(objTipoPlataforma, Company, User, Password, objStdTransac, "Default");

                // Use this service to trigger the API events.
            //    StdBSExtensibility service = new StdBSExtensibility();
            //    // Suppress all message box events from the API.
            //    // Plataforma.ExtensibilityLogger.AllowInteractivity = false;
            //    service.Initialize(MotorLP);

            //    // Check if service is operational
            //    if (service.IsOperational) {
            //        // Inshore that all extensions are loaded.
            //        service.LoadExtensions();
            //    }

                Platform = Plataforma;
                Engine = MotorLP;

            //    if (MotorLP != null && Plataforma.BDInicializada) {
            //        //Inicializa o SDK
            //        PriSDKContexto = new PRISDK100.clsSDKContexto();
            //        PriSDKContexto.Inicializa(MotorLP, ConstantesPrimavera100.Modulos.Vendas);
            //        PriSDKContexto.InicializaPlataforma(Platform);
            //    }

                EngineStatus = true;
            }

            return engineInstance;
        }

        #endregion

        #region Public Properties
        /// <summary>
        /// The platform
        /// </summary>
        public static StdPlatBS Platform { get; set; }

        /// <summary>
        /// The engine that allows access to the modules.
        /// </summary>
        public static ErpBS Engine { get; set; }

        /// <summary>
        /// The engine status 0 - Fail | 1 - OK
        /// </summary>
        public static bool EngineStatus { get; private set; }

        /// <summary>
        /// SDK Context
        /// </summary>
        public static PRISDK100.clsSDKContexto PriSDKContexto { get; private set; }

        #endregion
    }
}
