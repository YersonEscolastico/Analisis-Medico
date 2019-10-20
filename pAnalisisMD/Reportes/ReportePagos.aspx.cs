using Entidades;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace pAnalisisMD.Reportes
{
    public partial class ReportePagos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                BLL.RepositorioBase<Pagos> repositorio = new BLL.RepositorioBase<Pagos>();
                var lista = repositorio.GetList(x => true);

                MyReportViewer.ProcessingMode = ProcessingMode.Local;
                MyReportViewer.LocalReport.ReportPath = Server.MapPath(@"~\Reportes\PagoListado.rdlc");

                MyReportViewer.LocalReport.DataSources.Add(new ReportDataSource("PagoList", lista));
                MyReportViewer.LocalReport.Refresh();

            }
        }
    }
}