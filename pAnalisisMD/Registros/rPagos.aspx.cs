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
            if (!Page.IsPostBack)
            {
                ValoresDeDropdowns();
                ValoresDePaciente();
                ViewState["Pagos"] = new Pagos();
                BindGrid();
                IDTextBox.Text = "0";
            }

        }

        private void ValoresDeDropdowns()
        {

            RepositorioBase<Analisis> repositorio = new RepositorioBase<Analisis>();
            var list = new List<Analisis>();
            list = repositorio.GetList(p => true);
            AnalisisDropDown.DataSource = list;
            AnalisisDropDown.DataValueField = "AnalisisId";
            AnalisisDropDown.DataTextField = "AnalisisId";
            AnalisisDropDown.DataBind();
        }

        private void ValoresDePaciente()
        {

            RepositorioBase<Pacientes> repositorio = new RepositorioBase<Pacientes>();
            var list = new List<Pacientes>();
            list = repositorio.GetList(p => true);
            PacienteDropDownList.DataSource = list;
            PacienteDropDownList.DataValueField = "PacienteId";
            PacienteDropDownList.DataTextField = "PacienteId";
            PacienteDropDownList.DataBind();
        }



        protected void BindGrid()
        {
            if (ViewState["Pagos"] != null)
            {
                Grid.DataSource = ((Pagos)ViewState["Pagos"]).Detalle;
                Grid.DataBind();
            }
        }
        private bool ExisteEnLaBaseDeDatos()
        {
            RepositorioBase<Pagos> Repositorio = new RepositorioBase<Pagos>();
            Pagos P = Repositorio.Buscar(Utils.ToInt(IDTextBox.Text));
            return (P != null);
        }
        private Pagos LlenaClase()
        {
            Pagos P = new Pagos();

            P = (Pagos)ViewState["Pagos"];
            P.PagosId = Utils.ToInt(IDTextBox.Text);
            P.AnalisisId = Utils.ToInt(AnalisisDropDown.SelectedValue);
            P.Pagado = Utils.ToDecimal(PagadoTextBox.Text);
            P.FechaRegistro = DateTime.Now;
            P.PacienteId = Utils.ToInt(PacienteDropDownList.SelectedValue);

            return P;
        }
        private void LlenaCampo(Pagos P)
        {
            ((Pagos)ViewState["Pagos"]).Detalle = P.Detalle;
            IDTextBox.Text = P.PagosId.ToString();
            AnalisisDropDown.SelectedValue = P.AnalisisId.ToString();
            PagadoTextBox.Text = P.Pagado.ToString();
            PacienteDropDownList.SelectedValue = P.PacienteId.ToString();
            this.BindGrid();
        }

        protected void Grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Pagos P = new Pagos();

            P = (Pagos)ViewState["Pagos"];

            ViewState["Detalle"] = P.Detalle;

            int Fila = e.RowIndex;

            P.Detalle.RemoveAt(Fila);

            this.BindGrid();

            MontoPagadoTextBox.Text = string.Empty;
            decimal Total = 0;
            foreach (var item in P.Detalle.ToList())
            {
                Total += item.MontoPagado;
            }
            PagadoTextBox.Text = Total.ToString();
        }

        protected void Grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Grid.DataSource = ViewState["Detalle"];

            Grid.PageIndex = e.NewPageIndex;

            Grid.DataBind();
        }

        protected void AgregarGrid_Click(object sender, EventArgs e)
        {
            Pagos P = new Pagos();
            P = (Pagos)ViewState["Pagos"];
            Analisis A = new RepositorioBase<Analisis>().Buscar(Utils.ToInt(AnalisisDropDown.SelectedValue));

            int id = Utils.ToInt(AnalisisDropDown.SelectedValue);

            foreach (var item in P.Detalle.ToList())
            {
                if (id == item.AnalisisId)
                {

                    Utils.ShowToastr(this, "Ya esta agregado", "Error", "error");
                    return;
                }
            }

            P.Detalle.Add(new PagosDetalle(
                  Utils.ToInt(IDTextBox.Text),Utils.ToInt(PacienteDropDownList.SelectedValue), Utils.ToInt(AnalisisDropDown.SelectedValue),
                A.Balance,
                Utils.ToDecimal(MontoPagadoTextBox.Text)
                ));

            ViewState["Detalle"] = P.Detalle;

            this.BindGrid();

            Grid.Columns[1].Visible = false;

            MontoPagadoTextBox.Text = string.Empty;

            decimal Total = 0;
            foreach (var item in P.Detalle.ToList())
            {
                Total += item.MontoPagado;
            }
            PagadoTextBox.Text = Total.ToString();
        }

        protected void NuevoButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void GuardarButton_Click(object sender, EventArgs e)
        {
            Pagos P = new Pagos();
            bool paso = false;


            P = LlenaClase();

            if (Utils.ToInt(IDTextBox.Text) == 0)
            {
                paso = RepositorioPago.Guardar(P);
                Response.Redirect(Request.RawUrl);
            }
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {

                    Utils.ShowToastr(this, "No se pudo guardar", "Error", "error");
                    return;
                }
                paso = RepositorioPago.Modificar(P);
                Response.Redirect(Request.RawUrl);
            }

            if (paso)
            {

                Utils.ShowToastr(this, "Guardado", "Exito", "success");
                return;
            }
            else
                Utils.ShowToastr(this, "No se pudo guardar", "Error", "error");
        }

        protected void EliminarButton_Click(object sender, EventArgs e)
        {
            RepositorioBase<Pagos> Repositorio = new RepositorioBase<Pagos>();

            var P = Repositorio.Buscar(Utils.ToInt(IDTextBox.Text));

            if (P != null)
            {
                if (RepositorioPago.Eliminar(Utils.ToInt(IDTextBox.Text)))
                {
                    Utils.ShowToastr(this.Page, "Exito Eliminado", "success");
                    Response.Redirect(Request.RawUrl);
                }
                else
                    Utils.ShowToastr(this.Page, "No se pudo Eliminar", "Error");
            }
            else
                Utils.ShowToastr(this.Page, "No se pudo Eliminar", "Error");

        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            RepositorioBase<Pagos> Repositorio = new RepositorioBase<Pagos>();

            Pagos P = new Pagos();

            P = Repositorio.Buscar(Utils.ToInt(IDTextBox.Text));

            if (P != null)
                LlenaCampo(P);
            else
            {
                Utils.ShowToastr(this.Page, "Id no exite", "Error", "error");

            }
        }
    }
}