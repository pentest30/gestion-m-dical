using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GC.MV;
using Microsoft.Reporting.WinForms;

namespace GC.Views.Reporting
{
    /// <summary>
    /// Interaction logic for BilanReportingView.xaml
    /// </summary>
    public partial class BilanReportingView : Window
    {
        public BilanReportingView(List<OrdenanceVm> sources )
        {
            InitializeComponent();
            reportViewer.LocalReport.ReportPath = Environment.CurrentDirectory + @"\Reporting\ReportBlian.rdlc";
            reportViewer.LocalReport.DataSources.Clear();
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", sources.AsQueryable()));
            reportViewer.RefreshReport();
            reportViewer.Show();
        }
    }
}
