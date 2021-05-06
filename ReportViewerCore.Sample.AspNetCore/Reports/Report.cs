using Microsoft.Reporting.NETCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace ReportViewerCore
{
	class Report
	{
		public static void Load(LocalReport report, decimal widgetPrice, decimal gizmoPrice)
		{
			var items = new[] { new ReportItem { Description = "Widget 6000", Price = widgetPrice, Qty = 1 }, new ReportItem { Description = "Gizmo MAX", Price = gizmoPrice, Qty = 25 } };
			var parameters = new[] { new ReportParameter("Title", "标题哦") };
			//using var rs = Assembly.GetExecutingAssembly().GetManifestResourceStream("ReportViewerCore.Sample.AspNetCore.Reports.Report.rdlc");
            string RootPath = Path.GetDirectoryName(typeof(Report).Assembly.Location);
			var rdlcPath = Path.Combine(RootPath, "Reports", "Report.rdlc");
            if (!File.Exists(rdlcPath))
            {
                throw new Exception("报表模板不存在");
            }
            Stream rs = File.OpenRead(rdlcPath);

			report.LoadReportDefinition(rs);
			report.DataSources.Add(new ReportDataSource("Items", items));
			report.SetParameters(parameters);
		}
	}
}
