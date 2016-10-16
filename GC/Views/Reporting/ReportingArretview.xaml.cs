using System;
using System.Collections.Generic;
using System.Linq;
using GC.MV;
using Microsoft.Reporting.WinForms;

namespace GC.Views.Reporting
{
    /// <summary>
    /// Interaction logic for ReportingArretview.xaml
    /// </summary>
    public partial class ReportingArretview
    {
        public ReportingArretview(List<OrdenanceVm> sources )
        {
            InitializeComponent();
            InitializeComponent();
            reportViewer.LocalReport.ReportPath = Environment.CurrentDirectory + @"\Reporting\ReportArret.rdlc";
            reportViewer.LocalReport.DataSources.Clear();
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", sources.AsQueryable()));
            reportViewer.RefreshReport();
            reportViewer.Show();
        }
    }
}
