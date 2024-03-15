﻿namespace HelpersPrimavera10
{
    public interface ISecrets
    {
        void SetBSO(ErpBS100.ErpBS sBSO);
        void SetPSO(StdPlatBS100.StdBSInterfPub sPSO);

        ErpBS100.ErpBS BSO();
        StdPlatBS100.StdBSInterfPub PSO();

        //string Empresa();
        //string Utilizador();
        //string Password();

        string GetBDServidorInstancia();
        //string BDNomeDB();
        //string BDUtilizador();
        //string BDPassword();
    }
}
