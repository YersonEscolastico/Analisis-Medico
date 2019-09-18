using BLL;
using Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pAnalisisMD.Tests
{
    [TestClass]
    public class RepositorioPagosTest
    {
        [TestMethod]
        public void GuardaPagos()
        {
            RepositorioPago repositorio = new RepositorioPago();
            Pagos pagos = new Pagos
            {
                FechaRegistro = DateTime.Now
            };
            pagos.AgregarDetalle(1,1,100);
            Assert.IsTrue(RepositorioPago.Guardar(pagos));
        }
        [TestMethod]
        public void ModificarPagos()
        {
            RepositorioPago repositorio = new RepositorioPago();
            Pagos pagos = RepositorioPago.Buscar(1);
            pagos.FechaRegistro = DateTime.Now;
            pagos.AgregarDetalle(1,1,200);
            Assert.IsTrue(RepositorioPago.Modificar(pagos));
        }

        [TestMethod]
        public void GetListPagos()
        {
            RepositorioPago repositorio = new RepositorioPago();
            Assert.IsTrue((RepositorioPago.GetList(x => true).Count > 0));
        }
        [TestMethod]
        public void EliminarPagos()
        {
            RepositorioPago repositorio = new RepositorioPago();
            Assert.IsTrue(RepositorioPago.Eliminar(2));
        }
    }
}