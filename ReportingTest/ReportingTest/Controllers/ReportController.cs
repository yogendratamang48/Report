using Microsoft.Reporting.WebForms;
using ReportingTest.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReportingTest.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ReportDemo()
        {
            int applicantID = 4043;
            if (Session["applicantID"] != null)
            {
                applicantID = Convert.ToInt32(Session["applicantID"]);
            }
           

            Warning[] warnings;
            string mimeType;
            string[] streamids;
            string encoding;
            string filenameExtension;
            //DataTable table = ReportsHelper.ConvertoToDataTable(homes);
            var viewer = new ReportViewer();
            viewer.LocalReport.ReportPath = @"Reports\ReportTest.rdlc";


            //ReportDataSource objectives = new ReportDataSource("Objectives", lstObjectives);

            //ReportParameter lastPageFirstPara = new ReportParameter("LastPageFirstPara", Convert.ToString(lst.FirstPara));

            dsReport.sp_Get_CustomerDataTable dt = new dsReport.sp_Get_CustomerDataTable();
            Reports.dsReportTableAdapters.sp_Get_CustomerTableAdapter ta = new Reports.dsReportTableAdapters.sp_Get_CustomerTableAdapter();
            ta.Fill(dt);


            ReportDataSource rd = new ReportDataSource();
            rd.Name = "Customers";
            rd.Value = dt;
            //viewer.LocalReport.SetParameters(new ReportParameter[] { lastPageFirstPara, accNumber, prmMFormName, prmMFormTitle, prmMForType, prmBankBranch });
            viewer.LocalReport.DataSources.Add(rd);
            viewer.LocalReport.Refresh();

            var bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
            return new FileContentResult(bytes, mimeType);
        }
    }
}