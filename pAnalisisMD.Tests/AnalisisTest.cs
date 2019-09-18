using System;
using BLL;
using Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace pAnalisisMD.Tests
{
    [TestClass]
    public class PagoTest
    {
        [TestMethod]
        public void GuardarTest()
        {



            Analisis analisis = new Analisis()
            {
                AnalisisId = 1,
                FechaRegistro = DateTime.Now,
                Monto = 900,
                Balance = 900
            };
            Assert.IsTrue(RepositorioAnalisis.Guardar(analisis));
        }

        [TestMethod()]
        public void ModificarTest()
        {


            Analisis ve = new Analisis()
            {
                AnalisisId = 1,
                FechaRegistro = DateTime.Now,
                Monto = 900,
                Balance = 500
            };

            Assert.IsTrue(RepositorioAnalisis.Modificar(ve));
        }


        [TestMethod()]
        public void EliminarTest()
        {


            Assert.IsTrue(RepositorioAnalisis.Eliminar(1));
        }
    }
}


