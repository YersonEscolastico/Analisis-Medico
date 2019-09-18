using BLL;
using Entidades;
using pAnalisisMD.Utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace pAnalisisMD.Registros
{
    public partial class rPagos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            FechaTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
            if (!Page.IsPostBack)
            {
                ViewState["Pagos"] = new Pagos();
                Llenar();
                BindGrid();
            }
        }
        public void Limpiar()
        {
            AnalisisDropdownList.SelectedIndex = -1;
            BalanceTextBox.Text = string.Empty;
            Llenar();
            this.BindGrid();
        }
        private void Llenar()
        {
            AnalisisDropdownList.Items.Clear();
            RepositorioAnalisis repositorio = new RepositorioAnalisis();
            List<Analisis> lista = RepositorioAnalisis.GetList(x => x.Balance == 0);
            AnalisisDropdownList.DataSource = lista;
            AnalisisDropdownList.DataValueField = "AnalisisId";
            AnalisisDropdownList.DataBind();

        }
        private void BindGrid()
        {
            if (ViewState["Pagos"] != null)
            {
                DetalleGridView.DataSource = ((Pagos)ViewState["Pagos"]).Detalle;
                DetalleGridView.DataBind();
            }
        }
        protected void NuevoButton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
        protected void GuadarButton_Click(object sender, EventArgs e)
        {
            bool paso = false;
            RepositorioPago repositorio = new RepositorioPago();
            Pagos pagos = LlenaClase();

            if (pagos.PagoId == 0)
                paso = RepositorioPago.Guardar(pagos);
            else
            {
                if (ExisteEnLaBaseDeDatos())
                {
                    Utils.ShowToastr(this.Page, "No se pudo guardar!!", "Error", "error");
                    return;
                }
                paso = RepositorioPago.Modificar(pagos);
            }
            if (paso)
            {
                Limpiar();
                Utils.ShowToastr(this.Page, "Guardado con exito!!", "Guardado", "success");
            }

        }
        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            RepositorioPago repositorio = new RepositorioPago();
            Pagos pagos = RepositorioPago.Buscar(PagoIdTextBox.Text.ToInt());
            if (pagos !=null)
            {
                Limpiar();
                LlenarCampos(pagos);
            }
            else
                Utils.ShowToastr(this.Page, "El analisis que intenta buscar no existe", "Error", "error");
        }
        protected void EliminarButton_Click(object sender, EventArgs e)
        {
            RepositorioPago repositorio = new RepositorioPago();
            int id = PagoIdTextBox.Text.ToInt();
            if (ExisteEnLaBaseDeDatos())
            {
                Utils.ShowToastr(this.Page, "No se pudo eliminar", "Error", "error");
                return;
            }
            else
            {
                if (RepositorioPago.Eliminar(id))
                {
                    Utils.ShowToastr(this.Page, "Eliminado con exito!!", "Error", "success");
                    Limpiar();
                }
            }

        }
        private void LlenarCampos(Pagos pagos)
        {
            PagoIdTextBox.Text = pagos.PagoId.ToString();
            ViewState["Pagos"] = new Pagos();
            this.BindGrid();
        }

        private Pagos LlenaClase()
        {
            Pagos pagos = new Pagos();
            pagos.PagoId = PagoIdTextBox.Text.ToInt();
            FechaTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
            return pagos;
        }


        protected void AnalisisDropdownList_TextChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt32( AnalisisDropdownList.Text);
            if (AnalisisDropdownList.Items.Count > 0)
            {
                int AnalisisID = AnalisisDropdownList.SelectedValue.ToInt();
                RepositorioAnalisis repositorio = new RepositorioAnalisis();
                Analisis analisis = BLL.RepositorioAnalisis.Buscar(id);
                BalanceTextBox.Text = analisis.Balance.ToString();
            }
        }
        private bool ExisteEnLaBaseDeDatos()
        {
            RepositorioPago repositorio = new RepositorioPago();
            Pagos pagos = BLL.RepositorioPago.Buscar(PagoIdTextBox.Text.ToInt());
            return pagos != null;
        }
        protected void DetalleGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Pagos Pago = new Pagos();
            DetalleGridView.DataSource = Pago.Detalle;
            DetalleGridView.PageIndex = e.NewPageIndex;
            DetalleGridView.DataBind();
        }

        protected void AgregarButton_Click(object sender, EventArgs e)
        {
            if (MontoTextBox.Text.ToDecimal() <= 0)
                return;
            Pagos pago = new Pagos();
            pago.AgregarDetalle(pago.PagoId, AnalisisDropdownList.SelectedValue.ToInt(), Convert.ToDecimal(MontoTextBox.Text));

            ViewState["Pagos"] = pago;
            this.BindGrid();
            MontoTextBox.Text = string.Empty;
        }
    }
}