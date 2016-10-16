using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using Gc.Core.Data;
using GC.MV;
using GC.Views.Reporting;

namespace GC.Views
{
    /// <summary>
    /// Interaction logic for GraphView.xaml
    /// </summary>
    /// 
    public class WeberItemw
    {
        public string Val1 { get; set; }
        public string Val2 { get; set; }
        public string Val3 { get; set; }
        public string Val4 { get; set; }
        public string Val5 { get; set; }
        public string Val6 { get; set; }
    }
    public partial class GraphView
    {
        private readonly DossierPatient _patient;
        private ListBox _dragSource;

        public GraphView(DossierPatient patient)
        {
            _patient = patient;
          
            InitializeComponent();
            DataContext = patient;
            for (int i = 0; i < 3; i++)
            {
              
                ListView.Items.Add(new WeberItemw());
            }
         
            ListBox.Items.Add(new ListBoxItem {Content = "-------->"});
            ListBox.Items.Add(new ListBoxItem { Content = "<------->" });
            ListBox.Items.Add(new ListBoxItem { Content = "<-------" });
          


            //DataGridDroite.ItemsSource = source;
        }



        private void MydropEvent(object sender, DragEventArgs e)
        {
            object data = e.Data.GetData(typeof (ListBoxItem));
            DependencyObject dep = (DependencyObject) e.OriginalSource;

            // iteratively traverse the visual tree
            while ((dep != null) &&
                   !(dep is DataGridCell) &&
                   !(dep is DataGridColumnHeader))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep == null)
                return;

            if (dep is DataGridColumnHeader)
            {
                //DataGridColumnHeader columnHeader = dep as DataGridColumnHeader;
                // do something
            }

            if (dep is DataGridCell)
            {
                DataGridCell cell = dep as DataGridCell;
                DataGridRow dgr = FindVisualParent<DataGridRow>(cell);
                DataGridBoundColumn col = cell.Column as DataGridBoundColumn;
                if (col != null)
                {
                    Binding binding = col.Binding as Binding;
                    if (binding != null)
                    {
                        string boundPropertyName = binding.Path.Path;
                        ListView.SelectedItem = dgr.Item ;
                        ListView.ScrollIntoView(dgr.Item);
                        cell.Content = ((ListBoxItem)data).Content.ToString();
                        switch (boundPropertyName)
                        {
                            case "Val1":
                                ((WeberItemw) ListView.SelectedItem).Val1 = cell.Content.ToString();break;
                            case "Val2":
                                ((WeberItemw)ListView.SelectedItem).Val2 = cell.Content.ToString(); break;
                            case "Val3":
                                ((WeberItemw)ListView.SelectedItem).Val3 = cell.Content.ToString(); break;
                            case "Val4":
                                ((WeberItemw)ListView.SelectedItem).Val4 = cell.Content.ToString(); break;
                            case "Val5":
                                ((WeberItemw)ListView.SelectedItem).Val5 = cell.Content.ToString(); break;
                            case "Val6":
                                ((WeberItemw)ListView.SelectedItem).Val6 = cell.Content.ToString(); break;
                        }
                    }
                }
            }

        }
        private static T FindVisualParent<T>(DependencyObject dependencyObject) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(dependencyObject);

            if (parent == null) return null;

            var parentT = parent as T;
            return parentT ?? FindVisualParent<T>(parent);
        }
     

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var datacontext = RightEar.DataContext as GraphVm;
            var datacontext2 = LeftEar.DataContext as GraphVmGauche;
         
            var list = new List<SurditeReport>();
            if (datacontext != null && datacontext2 != null)
            {
                var t = ListView.Items[0] as WeberItemw;
                var v = ListView.Items[1] as WeberItemw;
                var m = ListView.Items[2] as WeberItemw;
                for (int i = 0; i < datacontext.Sources.Count; i++)
                {
                    var surdite = new SurditeReport();
                    try
                    {
                        surdite.Nom = _patient.Nom;
                        surdite.Prenom = _patient.Prenom;
                        surdite.Age = _patient.Age;
                        surdite.Addresse = _patient.Addresse;
                        surdite.Sexe = _patient.Sexe;
                        surdite.Observation = RichTextBoxObserv.Text;
                        surdite.AxisX = datacontext.Sources[i].Argument;
                        surdite.AxisY = datacontext.Sources[i].Value;
                        surdite.AxisXCa = datacontext.SourcesLeft[i].Argument;
                        surdite.AxisYCa = datacontext.SourcesLeft[i].Value;
                        PopulateWeber(t, surdite, v, m);
                        list.Add(surdite);
                    }
// ReSharper disable once EmptyGeneralCatchClause
                    catch (Exception)
                    {

                        //continue;
                    }
                }
                for (int i = 0; i < datacontext2.Sources.Count; i++)
                {
                    var surdite = new SurditeReport();
                    try
                    {
                        surdite.Nom = _patient.Nom;
                        surdite.Prenom = _patient.Prenom;
                        surdite.Age = _patient.Age;
                        surdite.Addresse = _patient.Addresse;
                        surdite.Observation = RichTextBoxObserv.Text;
                        surdite.Sexe = _patient.Sexe;
                        surdite.AxisX1 = datacontext2.Sources[i].Argument;
                        surdite.AxisY1 = datacontext2.Sources[i].Value;
                        surdite.AxisXCa1 = datacontext2.SourcesLeft[i].Argument;
                        surdite.AxisYCa1 = datacontext2.SourcesLeft[i].Value;
                        PopulateWeber(t, surdite, v, m);
                        list.Add(surdite);
                    }
// ReSharper disable once EmptyGeneralCatchClause
                    catch (Exception)
                    {

                        //continue;
                    }
                }
              
               


            }
            var frm = new SurditeReportingView(list);
            frm.Show();

        }

        private static void PopulateWeber(WeberItemw t, SurditeReport surdite, WeberItemw v, WeberItemw m)
        {
            if (t != null)
            {
                surdite.Val1t = t.Val1;
                surdite.Val2t = t.Val2;
                surdite.Val3t = t.Val3;
                surdite.Val4t = t.Val4;
                surdite.Val5t = t.Val5;
                surdite.Val6t = t.Val6;
            }
            if (v != null)
            {
                surdite.Val1v = v.Val1;
                surdite.Val2v = v.Val2;
                surdite.Val3v = v.Val3;
                surdite.Val4v = v.Val4;
                surdite.Val5v = v.Val5;
                surdite.Val6v = v.Val6;
            }
            if (m != null)
            {
                surdite.Val1m = m.Val1;
                surdite.Val2m = m.Val2;
                surdite.Val3m = m.Val3;
                surdite.Val4m = m.Val4;
                surdite.Val5m = m.Val5;
                surdite.Val6m = m.Val6;
            }
        }

        private void ChartControl1_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }



       

      

        private void UIElement_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListBox parent = (ListBox) sender;
            _dragSource = parent;
            object data = GetDataFromListBox(_dragSource, e.GetPosition(parent));

            if (data != null)
            {
                DragDrop.DoDragDrop(parent, data, DragDropEffects.Move);
            }
        }

        private object GetDataFromListBox(ListBox source, Point point)
        {
            UIElement element = source.InputHitTest(point) as UIElement;
            if (element != null)
            {
                object data = DependencyProperty.UnsetValue;
                while (data == DependencyProperty.UnsetValue)
                {
                    if (element != null)
                    {
                        data = source.ItemContainerGenerator.ItemFromContainer(element);
                        if (data == DependencyProperty.UnsetValue)
                        {
                            element = VisualTreeHelper.GetParent(element) as UIElement;
                        }
// ReSharper disable once PossibleUnintendedReferenceComparison
                        if (element == source)
                        {
                            return null;
                        }
                    }
                }
                if (data != DependencyProperty.UnsetValue)
                {
                    return data;
                }
            }
            return null;
        }

      
    }


}
