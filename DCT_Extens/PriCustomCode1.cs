using Primavera.Extensibility.CustomCode;

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
