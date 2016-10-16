using System.Globalization;
using System.Linq;
using System.Windows;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;

namespace GC.Views.Statistics
{
    /// <summary>
    /// Interaction logic for ConsultStatisticsView.xaml
    /// </summary>
    public partial class ConsultStatisticsView
    {
        readonly MainWindow _mainWindow = (MainWindow)Application.Current.MainWindow;

        public ConsultStatisticsView()
        {
            _mainWindow.LoadingDecorator.IsSplashScreenShown = true;
            InitializeComponent();
            var repositoryConsult = _mainWindow.AutofacBootStrap.ResoleConsultRepo();
            string[] param = { "DossierPatient" };
            DataGrid.ItemsSource = repositoryConsult.QueryWith(param).ToList();
            _mainWindow.LoadingDecorator.IsSplashScreenShown = false;
        }

        private void DataGrid_OnSelectedItemChanged(object sender, SelectedItemChangedEventArgs e)
        {
            
        }

        private void edit_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            var dateTime = ((DateEdit)sender).DateTime;
            var selectedDate = dateTime.Date.ToString(CultureInfo.InvariantCulture);
            var nextday = dateTime.AddDays(1).Date.ToString(CultureInfo.CurrentUICulture);
            DataGrid.FilterString = string.Format("[DateConsultation] >=#{0}# and [DateConsultation]<#{1}#",
                selectedDate, nextday);
        }
    }
}
