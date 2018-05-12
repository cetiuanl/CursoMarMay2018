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
    }
}
