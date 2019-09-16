﻿using BLL;
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
    public partial class rPacientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            {
                if (!Page.IsPostBack)
                {
                    int id = Utils.ToInt(Request.QueryString["id"]);
                    if (id > 0)
                    {
                        RepositorioBase<Pacientes> repositorio = new RepositorioBase<Pacientes>();
                        var registro = repositorio.Buscar(id);

                        if (registro == null)
                        {
                            Utils.ShowToastr(this.Page, "Registro no existe", "Error", "error");
                        }
                        else
                        {
                            LlenaCampos(registro);
                        }
                    }
                }
            }
        }



        protected void Limpiar()
        {
            IdTextBox.Text = "0";
            NombresTextBox.Text = string.Empty;
            DireccionTextBox.Text = string.Empty;
            TelefonoTextBox.Text = string.Empty;
        }

        protected void NuevoButton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        protected Pacientes LlenaClase(Pacientes pacientes)
        {
            pacientes.PacienteId = Utils.ToInt(IdTextBox.Text);
            pacientes.Nombres = NombresTextBox.Text;
            pacientes.Direccion = DireccionTextBox.Text;
            pacientes.Telefono = TelefonoTextBox.Text;

            return pacientes;
        }

        private void LlenaCampos(Pacientes pacientes)
        {
            IdTextBox.Text = Convert.ToString(pacientes.PacienteId);
            NombresTextBox.Text = pacientes.Nombres;
            DireccionTextBox.Text = pacientes.Direccion;
            TelefonoTextBox.Text = pacientes.Telefono;
        }


        private bool ExisteEnLaBaseDeDatos()
        {
            RepositorioBase<Pacientes> repositorio = new RepositorioBase<Pacientes>();
            Pacientes pacientes = repositorio.Buscar(Utils.ToInt(IdTextBox.Text));
            return (pacientes != null);
        }

        protected void GuardarButton_Click1(object sender, EventArgs e)
        {
             RepositorioBase<Pacientes> Repositorio = new RepositorioBase<Pacientes>();
            Pacientes pacientes = new Pacientes();
            bool paso = false;

            pacientes = LlenaClase(pacientes);

            if (Utils.ToInt(IdTextBox.Text) == 0)
            {
                paso = Repositorio.Guardar(pacientes);
                Limpiar();
            }
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {

                    Utils.ShowToastr(this, "No se pudo guardar", "Error", "error");
                    return;
                }
                paso = Repositorio.Modificar(pacientes);
                Limpiar();
            }

            if (paso)
            {
                Utils.ShowToastr(this, "Guardado", "Exito", "success");
                return;
            }
            else

                Utils.ShowToastr(this, "No se pudo guardar", "Error", "error");
        }



        protected void BuscarButton_Click1(object sender, EventArgs e)
        {
            RepositorioBase<Pacientes> repositorio = new RepositorioBase<Pacientes>();
            var pacientes = repositorio.Buscar(Utils.ToInt(IdTextBox.Text));

            if (pacientes != null)
            {
                Limpiar();
                LlenaCampos(pacientes);
                Utils.ShowToastr(this, "Busqueda exitosa", "Exito", "success");
            }
            else
            {
                Utils.ShowToastr(this.Page, "El paciente que intenta buscar no existe", "Error", "error");
                Limpiar();
            }
        }

        protected void EliminarButton_Click1(object sender, EventArgs e)
        {
            if (Utils.ToInt(IdTextBox.Text) > 0)
            {
                int id = Convert.ToInt32(IdTextBox.Text);
                RepositorioBase<Pacientes> repositorio = new RepositorioBase<Pacientes>();
                if (repositorio.Eliminar(id))
                {

                    Utils.ShowToastr(this.Page, "Eliminado con exito!!", "Eliminado", "info");
                }
                else
                    Utils.ShowToastr(this.Page, "Fallo al Eliminar :(", "Error", "error");
                Limpiar();
            }
            else
            {
                Utils.ShowToastr(this.Page, "No se pudo eliminar, paciente no existe", "error", "error");
            }
        }

    }
}
