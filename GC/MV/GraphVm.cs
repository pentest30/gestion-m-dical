using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using GalaSoft.MvvmLight.Command;
using GC.Properties;

namespace GC.MV
{
    public class GraphVm : INotifyPropertyChanged
    {
        public double AxisX { get; set; }
        public double AxisXCo { get; set; }

        private double _axisY = 250;

        public double AxisY
        {
            get { return _axisY; }
            set { _axisY = value; }
        }

        private double _axisYCo = 250;

        public double AxisYCo
        {
            get { return _axisYCo; }
            set { _axisYCo = value; }
        }



        

        public GraphVm()
        {
            Sources = new ObservableCollection<GraphdataSource>();
            SourcesLeft = new ObservableCollection<GraphdataSource>();

        }

        public ObservableCollection<GraphdataSource> Sources { get; set; }
        public ObservableCollection<GraphdataSource> SourcesLeft { get; set; }
        private RelayCommand _addItems;
        private GraphdataSource _selectedItem;

        public GraphdataSource SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(_selectedItem.ToString());
            }
        }

        private RelayCommand _addItemCoCommand;

        public RelayCommand AddItemCoCommand
        {
            get
            {
                return _addItemCoCommand ?? (_addItemCoCommand = new RelayCommand(() =>
                {
                    var selectedItem = new GraphdataSource
                    {
                        Argument = AxisXCo,
                        Value = AxisYCo
                    };
                   // AxisXCo = 0;
                    _axisYCo = _axisYCo*2;

                    Sources.Add(selectedItem);
                }));
            }
           
        }

        private RelayCommand _dellCaCommand;

        public RelayCommand DellCaCommand
        {
            get
            {
                return _dellCaCommand ?? (_dellCaCommand = new RelayCommand(() =>
                {
                    var item = (SourcesLeft.Count == 0) ? null : SourcesLeft.Last();
                    if (item != null)
                    {
                        SourcesLeft.Remove(item);
                        AxisY = AxisY/2;
                    }
                }));
            }

        }

        private RelayCommand _delleCommand;

        public RelayCommand DellCoCommand
        {
            get { return _delleCommand??(_delleCommand= new RelayCommand(() =>
            {
                var item = (Sources.Count == 0) ? null : Sources.Last();
                if (item != null)
                {
                    Sources.Remove(item);
                    AxisYCo = AxisYCo / 2;
                }
                
            })); }
            
        }
        
        
        public RelayCommand AddCaItems
        {
            get
            {
                return _addItems ?? (_addItems = new RelayCommand(() =>
                {
                    var selectedItem = new GraphdataSource
                    {
                        Argument = AxisX,
                        Value = AxisY
                    };
                 //   AxisX = 0;
                    AxisY = AxisY*2;

                    SourcesLeft.Add(selectedItem);
                }));
            }
        }

   



        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class GraphdataSource
   {
       public double Argument { get; set; }
       public double Value { get; set; }
      
   }
}
