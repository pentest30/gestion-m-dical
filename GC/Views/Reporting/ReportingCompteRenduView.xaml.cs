using System;
using System.Collections.Generic;
using System.Linq;
using GC.MV;
using Microsoft.Reporting.WinForms;

namespace GC.Views.Reporting
{
    /// <summary>
    /// Interaction logic for ReportingCompteRenduView.xaml
    /// </summary>
    public partial class ReportingCompteRenduView
    {
        public ReportingCompteRenduView(List<CompteRebduVm>sources )
        {
            InitializeComponent();
            reportViewer.LocalReport.ReportPath = Environment.CurrentDirectory + @"\Reporting\ReportCompteRendu.rdlc";
            reportViewer.LocalReport.DataSources.Clear();
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", sources.AsQueryable()));
            reportViewer.RefreshReport();
            reportViewer.Show();
        }
    }
}
