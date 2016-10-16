using System;
using System.Linq;
using System.Windows;
using DevExpress.Xpf.Bars;
using Gc.Core.Data;
using Gc.Core.DataManage.Core;


namespace GC.Views.Gestioncabinet
{/// <summary>
    /// Interaction logic for AddRendezVous.xaml
    /// </summary>
    public partial class AddRendezVous
    {
        private readonly IRepository<DossierPatient> _repository;
        private readonly IRepository<Rendezvou> _repositoryRendezVous;

        readonly MainWindow _mainWindow = (MainWindow)Application.Current.MainWindow;
        public delegate void UpdateDg();
        public AddPatient.UpdateDg UpdateDataDg;
        readonly Rendezvou _rendezvou;

        public AddRendezVous(Rendezvou rendezvou)
        {
           
            _mainWindow.LoadingDecorator.IsSplashScreenShown = true;
            InitializeComponent();
            _rendezvou = rendezvou;
            _repository = _mainWindow.AutofacBootStrap.ResoleDossierPatientRepo();
            _repositoryRendezVous = _mainWindow.AutofacBootStrap.RepositoryRendezVous();
            CbPatients.ItemsSource = _repository.Query();
            if (_rendezvou.Id == 0)
            {
                
                
                var d = DateTime.Now.Date;
                var n = _repositoryRendezVous.FindBy(x=>x.Date==d).Count();
                if (n==0) _rendezvou.Num = 1.ToString();
                else
                {
                    var last = _repositoryRendezVous.FindBy(x => x.Date == d).ToList().LastOrDefault();
                    if (last != null)
                    {
                        var num = Convert.ToInt32(last.Num) + 1;_rendezvou.Num = num.ToString();
                    }
                }
            }
            DataContext =_rendezvou;
            _mainWindow.LoadingDecorator.IsSplashScreenShown = false;
        }

        private void SeTDateTime(Rendezvou item)
        {
            if (item.Date == null) return;if (item.HourFrom != null && item.HourTo != null)
            {
                var hFrom =item. HourFrom.Value.Hour;
                var mFrom = item.HourFrom.Value.Minute;

                var hTo = item.HourTo.Value.Hour;
                var mTo = item.HourTo.Value.Minute;
                item.HourFrom = new DateTime(item.Date.Value.Year, item.Date.Value.Month, item.Date.Value.Day, hFrom, mFrom, 00);
                item.HourTo = new DateTime(item.Date.Value.Year, item.Date.Value.Month, item.Date.Value.Day, hTo, mTo, 00);
            }
        }

        private void AddBtn_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = DataContext as Rendezvou;
            if (item == null || item.PatientId == 0)
            {
                MessageBox.Show("Valeurs nulles non autorisé, selectionner un patient ", "Attention!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                SeTDateTime(item);
                if (item.Id > 0) _repositoryRendezVous.Update(item);
                else
                {
                    if (!_repositoryRendezVous.DoesExist(x => x.HourFrom <= item.HourTo  && x.Date == item.Date))
                    {
                        _repositoryRendezVous.Insert(item);
                    }
                    else
                    {
                        MessageBox.Show("cette Heure est occupée", "Attention", MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                        return;
                    }
                }
                _repositoryRendezVous.SaveChanges();
                MessageBox.Show("L'enregistrement est terminer avec succés");
                if (UpdateDataDg != null) UpdateDataDg();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);

            }
        }

        private void AddAndCloseBtn_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = DataContext as Rendezvou;
            if (item == null)
            {
                MessageBox.Show("Valeurs nulles non autorisé", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                SeTDateTime(item);
                if (item.Id > 0) _repositoryRendezVous.Update(item);
                else
                {

                    if (!_repositoryRendezVous.DoesExist(x => x.HourFrom <= item.HourTo && x.Date == item.Date))
                    {
                        _repositoryRendezVous.Insert(item);
                    }
                    else
                    {
                        MessageBox.Show("cette Heure est occupée", "Attention", MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                        return;
                    }
                }
                _repository.SaveChanges();
                MessageBox.Show("L'enregistrement est terminer avec succés");
                if (UpdateDataDg != null) UpdateDataDg();
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);

            }
        }

        private void AddAndCtnBtn_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = DataContext as Rendezvou;
            if (item == null)
            {MessageBox.Show("Valeurs nulles non autorisé", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            SeTDateTime(item);
            try
            {
                if (item.Id > 0) _repositoryRendezVous.Update(item);
                else
                {
                    if (!_repositoryRendezVous.DoesExist(x => x.HourFrom <= item.HourTo && x.Date == item.Date))
                    {
                        _repositoryRendezVous.Insert(item);
                    }
                    else
                    {
                        MessageBox.Show("cette Heure est occupée", "Attention", MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                        return;
                    }
                }
                _repository.SaveChanges();
                MessageBox.Show("L'enregistrement est terminer avec succés");
                if (UpdateDataDg != null) UpdateDataDg();
                DataContext = new Rendezvou {PatientId = _rendezvou.PatientId};
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);

            }
        }

    }
}
