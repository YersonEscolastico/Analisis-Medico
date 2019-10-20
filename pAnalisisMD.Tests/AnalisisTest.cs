using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BLL;
using Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace pAnalisisMD.Tests
{
    [TestClass()]
    public class RepositorioBaseTests
    {
        [TestMethod()]
        public void GuardarTest()
        {
            Pacientes pacientes = new Pacientes();

            pacientes.Nombre = "William";
            pacientes.Direccion = "Williamsc";
            pacientes.telefono = "829-694-5889";

            RepositorioBase<Pacientes> repositorioBase = new RepositorioBase<Pacientes>();
            Assert.IsTrue(repositorioBase.Guardar(pacientes));
        }

        [TestMethod()]
        public void ModificarTest()
        {
            Pacientes pacientes = new Pacientes();
            pacientes.PacienteID = 1;
            pacientes.Nombre = "William";
            pacientes.Direccion = "Williabh98@gamil.com";
            pacientes.telefono = "829-694-5889";

            RepositorioBase<Pacientes> repositorioBase = new RepositorioBase<Pacientes>();
            Assert.IsTrue(repositorioBase.Modificar(pacientes));
        }

        [TestMethod()]
        public void BuscarTest()
        {
            int id = 2;
            Pacientes pacientes = new Pacientes();
            RepositorioBase<Pacientes> repositorioBase = new RepositorioBase<Pacientes>();
            pacientes = repositorioBase.Buscar(id);
            Assert.AreEqual(true, pacientes != null);
        }

        [TestMethod()]
        public void GetListTest()
        {
            RepositorioBase<Pacientes> repositorioBase = new RepositorioBase<Pacientes>();
            List<Pacientes> lista = new List<Pacientes>();
            Expression<Func<Pacientes, bool>> resultado = u => true;
            lista = repositorioBase.GetList(resultado);
            Assert.IsNotNull(lista);
        }

        [TestMethod()]
        public void EliminarTest()
        {
            Pacientes pacientes = new Pacientes();
            RepositorioBase<Pacientes> repositorioBase = new RepositorioBase<Pacientes>();
            pacientes.PacienteID = 1;
            Assert.AreEqual(true, repositorioBase.Eliminar(pacientes.PacienteID));
        }
    }
}

