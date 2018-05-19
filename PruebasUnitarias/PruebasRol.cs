using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using CN;

namespace PruebasUnitarias
{
    [TestClass]
    public class PruebasRol
    {
        [TestMethod]
        public void traerRoles()
        {
            List<Rol> listado = Rol.traerTodos();          
            Assert.AreEqual<bool>(true, listado.Count > 0);
        }

        [TestMethod]
        public void guardar()
        {
            Rol oRol = new Rol("nombre", 0, "descripcion");

           // oRol.g

            //Assert.AreEqual<bool>(true, listado.Count > 0);
        }
    }
}
