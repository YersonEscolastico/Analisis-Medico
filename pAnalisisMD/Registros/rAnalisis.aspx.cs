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
    public partial class rAnalisis : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                ValoresDeDropdowns();
                LimpiarAnalisis();
                LimpiarTipoAnalisis();

                ViewState["Analisis"] = new Analisis();
                BindGrid();
            }
        }

        private void ValoresDeDropdowns()
        {
            RepositorioBase<Pacientes> db = new RepositorioBase<Pacientes>();
            var listado = new List<Pacientes>();
            listado = db.GetList(p => true);
            PacienteDropDown.DataSource = listado;
            PacienteDropDown.DataValueField = "PacienteId";
            PacienteDropDown.DataTextField = "Nombres";
            PacienteDropDown.DataBind();

            RepositorioBase<TipoAnalisis> repositorio = new RepositorioBase<TipoAnalisis>();
            var list = new List<TipoAnalisis>();
            list = repositorio.GetList(p => true);
            TiposAnalisisDropDown.DataSource = list;
            TiposAnalisisDropDown.DataValueField = "TiposId";
            TiposAnalisisDropDown.DataTextField = "Analisis";
            TiposAnalisisDropDown.DataBind();
        }
        protected void BindGrid()
        {
            if (ViewState["Analisis"] != null)
            {
                Grid.DataSource = ((Analisis)ViewState["Analisis"]).Detalle;
                Grid.DataBind();
            }
        }
        private void LimpiarAnalisis()
        {
            IDTextBox.Text = "0";
            FechaTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
            PacienteDropDown.SelectedIndex = 0;
            TiposAnalisisDropDown.SelectedIndex = 0;
            ResultadoTextBox.Text = string.Empty;
            MontoTextBox.Text = string.Empty;
            BalanceTextBox.Text = string.Empty;
            Grid.DataSource = null;
            Grid.DataBind();
        }
        private void LimpiarTipoAnalisis()
        {
            TiposIdTextBox.Text = "0";
            AnalisisTextBox.Text = string.Empty;
            PrecioTextBox.Text = string.Empty;
            TiposAnalisisFechaTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
        private bool TiposExisteEnLaBaseDeDatos()
        {
            RepositorioBase<TipoAnalisis> Repositorio = new RepositorioBase<TipoAnalisis>();
            TipoAnalisis Tipos = Repositorio.Buscar(Utils.ToInt(TiposIdTextBox.Text));
            return (Tipos != null);
        }
        private bool AnalisisExisteEnLaBaseDeDatos()
        {
            RepositorioBase<Analisis> Repositorio = new RepositorioBase<Analisis>();
            Analisis Analisis = Repositorio.Buscar(Utils.ToInt(IDTextBox.Text));
            return (Analisis != null);
        }
        private TipoAnalisis TiposLlenaClase()
        {
            TipoAnalisis Tipos = new TipoAnalisis();

            Tipos.TiposId = Utils.ToInt(TiposIdTextBox.Text);
            Tipos.Analisis = AnalisisTextBox.Text;
            Tipos.Precio = Utils.ToDecimal(PrecioTextBox.Text);
            Tipos.Fecha = Utils.ToDateTime(TiposAnalisisFechaTextBox.Text);

            return Tipos;
        }
        private Analisis AnalisisLlenaClase()
        {
            Analisis Analisis = new Analisis();

            Analisis = (Analisis)ViewState["Analisis"];
            Analisis.AnalisisId = Utils.ToInt(IDTextBox.Text);
            Analisis.Paciente = PacienteDropDown.SelectedItem.ToString();
            Analisis.Balance = Utils.ToDecimal(BalanceTextBox.Text);
            Analisis.Monto = Utils.ToDecimal(MontoTextBox.Text);
            Analisis.Fecha = Utils.ToDateTime(FechaTextBox.Text);

            return Analisis;
        }
        private void LlenaCampo(TipoAnalisis Tipos)
        {
            TiposIdTextBox.Text = Tipos.TiposId.ToString();
            AnalisisTextBox.Text = Tipos.Analisis;
            PrecioTextBox.Text = Tipos.Precio.ToString();
            TiposAnalisisFechaTextBox.Text = Tipos.Fecha.ToString("yyyy-MM-dd");
        }
        private void LlenaCampo(Analisis Analisis)
        {
            ((Analisis)ViewState["Analisis"]).Detalle = Analisis.Detalle;
            IDTextBox.Text = Analisis.AnalisisId.ToString();
            FechaTextBox.Text = Analisis.Fecha.ToString("yyyy-MM-dd");
            // PacienteDropDown.SelectedValue = Analisis.Paciente;
            MontoTextBox.Text = Analisis.Monto.ToString();
            BalanceTextBox.Text = Analisis.Balance.ToString();
            this.BindGrid();
        }
        protected void AgregarGrid_Click(object sender, EventArgs e)
        {
            Analisis Analisis = new Analisis();

            Analisis = (Analisis)ViewState["Analisis"];

            Analisis.Detalle.Add(new AnalisisDetalle(
                Utils.ToInt(TiposAnalisisDropDown.SelectedValue),
                ResultadoTextBox.Text,
                Utils.ToDateTime(FechaTextBox.Text)));

            ViewState["Detalle"] = Analisis.Detalle;

            this.BindGrid();

            Grid.Columns[1].Visible = false;

            ResultadoTextBox.Text = string.Empty;

            decimal Total = 0;
            foreach (var item in Analisis.Detalle.ToList())
            {
                TipoAnalisis T = new RepositorioBase<TipoAnalisis>().Buscar(item.TiposId);
                Total += T.Precio;
            }
            BalanceTextBox.Text = Total.ToString();
            MontoTextBox.Text = Total.ToString();
        }

        protected void Grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Analisis Analisis = new Analisis();

            Analisis = (Analisis)ViewState["Analisis"];

            ViewState["Detalle"] = Analisis.Detalle;

            int Fila = e.RowIndex;

            Analisis.Detalle.RemoveAt(Fila);

            this.BindGrid();

            ResultadoTextBox.Text = string.Empty;
            decimal Total = 0;
            foreach (var item in Analisis.Detalle.ToList())
            {
                Total += item.Precio;
            }
            BalanceTextBox.Text = Total.ToString();
            MontoTextBox.Text = Total.ToString();
        }

        protected void Grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Grid.DataSource = ViewState["Detalle"];

            Grid.PageIndex = e.NewPageIndex;

            Grid.DataBind();
        }

        protected void TiposGuardarButton_Click(object sender, EventArgs e)
        {
            TipoAnalisis Tipo = new TipoAnalisis();
            RepositorioBase<TipoAnalisis> Repositorio = new RepositorioBase<TipoAnalisis>();

            bool paso = false;

            Tipo = TiposLlenaClase();

            if (Utils.ToInt(TiposIdTextBox.Text) == 0)
            {
                paso = Repositorio.Guardar(Tipo);
                Response.Redirect(Request.RawUrl);
            }
            else
            {
                if (!TiposExisteEnLaBaseDeDatos())
                {

                    Utils.ShowToastr(this.Page, "No se pudo Guardar", "Error");
                    return;
                }
                paso = Repositorio.Modificar(Tipo);
                Response.Redirect(Request.RawUrl);
            }

            if (paso)
            {
                Utils.ShowToastr(this.Page, "Exito Eliminado", "success");
                return;
            }
            else
                Utils.ShowToastr(this.Page, "No se pudo Guardar", "Error");
        }


        protected void TiposEliminarButton_Click(object sender, EventArgs e)
        {
            RepositorioBase<TipoAnalisis> Repositorio = new RepositorioBase<TipoAnalisis>();

            var TipoAnalisis = Repositorio.Buscar(Utils.ToInt(TiposIdTextBox.Text));

            if (TipoAnalisis != null)
            {
                if (Repositorio.Eliminar(Utils.ToInt(TiposIdTextBox.Text)))
                {
                    Utils.ShowToastr(this.Page, "Exito Eliminado", "success");
                }
                else
                    Utils.ShowToastr(this.Page, "No se pudo Eliminar", "Error");
            }
            else
                Utils.ShowToastr(this.Page, "No se pudo Eliminar", "Error");

            LimpiarTipoAnalisis();
        }

        protected void TiposBuscarButton_Click(object sender, EventArgs e)
        {
            RepositorioBase<TipoAnalisis> Repositorio = new RepositorioBase<TipoAnalisis>();

            TipoAnalisis Tipos = new TipoAnalisis();

            Tipos = Repositorio.Buscar(Utils.ToInt(TiposIdTextBox.Text));

            if (Tipos != null)
                LlenaCampo(Tipos);
            else
            {
                Utils.ShowToastr(this, "No se pudo Buscar", "Error", "error");
                LimpiarTipoAnalisis();
            }
        }

        protected void TiposNuevoButton_Click(object sender, EventArgs e)
        {
            LimpiarTipoAnalisis();
        }

        protected void AnalisisNuevoButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void AnalisisBuscarButton_Click(object sender, EventArgs e)
        {
            RepositorioBase<Analisis> Repositorio = new RepositorioBase<Analisis>();

            Analisis Analisis = new Analisis();

            Analisis = Repositorio.Buscar(Utils.ToInt(IDTextBox.Text));

            if (Analisis != null)
                LlenaCampo(Analisis);
            else
            {
                Utils.ShowToastr(this, "No se pudo Buscar", "Error", "error");
                LimpiarTipoAnalisis();
            }
        }

        protected void AnalisisGuardarButton_Click(object sender, EventArgs e)
        {
            Analisis Analisis = new Analisis();
            RepositorioBase<Analisis> Repositorio = new RepositorioBase<Analisis>();
            bool paso = false;


            Analisis = AnalisisLlenaClase();

            if (Utils.ToInt(IDTextBox.Text) == 0)
            {
                if (Grid.Rows.Count == 0)
                {
                    Utils.ShowToastr(this, "No se pudo guardar", "Error", "error");
                    return;
                }
                else
                    paso = Repositorio.Guardar(Analisis);
                LimpiarAnalisis();
            }
            else
            {
                if (!AnalisisExisteEnLaBaseDeDatos())
                {

                    Utils.ShowToastr(this, "No se pudo guardar", "Error", "error");
                    return;
                }
                paso = Repositorio.Modificar(Analisis);
                LimpiarAnalisis();
            }

            if (paso)
            {
                Utils.ShowToastr(this, "Guardado", "Exito", "success");
                return;
            }
            else

                Utils.ShowToastr(this, "No se pudo guardar", "Error", "error");

            LimpiarAnalisis();
        }

        protected void AnalisisEliminarButton_Click(object sender, EventArgs e)
        {
            RepositorioBase<Analisis> Repositorio = new RepositorioBase<Analisis>();

            var Analisis = Repositorio.Buscar(Utils.ToInt(IDTextBox.Text));

            if (Analisis != null)
            {
                if (Repositorio.Eliminar(Utils.ToInt(IDTextBox.Text)))
                {
                    Utils.ShowToastr(this.Page, "Exito Eliminado", "success");
                    Response.Redirect(Request.RawUrl);
                }
               

                else
                    Utils.ShowToastr(this.Page, "No se pudo Eliminar", "Error");
            }
            else
                Utils.ShowToastr(this.Page, "No se pudo Eliminar", "Error");

            LimpiarTipoAnalisis();
        }

    }
}