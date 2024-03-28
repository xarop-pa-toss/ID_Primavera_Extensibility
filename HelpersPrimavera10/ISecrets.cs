namespace HelpersPrimavera10
{
    public interface ISecrets
    {
        ErpBS100.ErpBS BSO { get; set; }
        StdPlatBS100.StdBSInterfPub PSO { get; set; }

        string BDServidorInstancia { get; set; }
        //string GetBDServidorInstancia();
        //string SetBDServidorInstancia();
    }
}
