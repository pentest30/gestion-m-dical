namespace GC.DXSplashScreen
{
    /// <summary>
    /// Interaction logic for SplashScreenView1.xaml
    /// </summary>
    public partial class SplashScreenView1
    {
        public SplashScreenView1()
        {
            InitializeComponent();
            for (int i = 0; i <= 100; i++)
            {
                DevExpress.Xpf.Core.DXSplashScreen.Progress(i);
                //... 
            }
        }
    }
}
