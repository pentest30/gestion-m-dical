using System;
using System.Collections.Generic;
using System.Linq;
using GC.MV;
using Microsoft.Reporting.WinForms;

namespace GC.Views.Reporting
{
    /// <summary>
    /// Interaction logic for ReportingVIew.xaml
    /// </summary>
    public partial class ReportingVIew
    {
        public ReportingVIew(List<OrdenanceVm> sources)
        {
            InitializeComponent();
            reportViewer.LocalReport.ReportPath = Environment.CurrentDirectory + @"\Reporting\ReportOrde.rdlc";
            reportViewer.LocalReport.DataSources.Clear();
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1",sources.AsQueryable()));
            reportViewer.RefreshReport();
            reportViewer.Show();
        }
    }
}
