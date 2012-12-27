using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ZMTFixedAssetsWebApp.Domain.Abstract;
using ZMTFixedAssetsWebApp.Domain.Model;
using ZMTFixedAssetsWebApp.WebUI.Controllers;

namespace ZMTFixedAssetsWebApp.UnitTests
{
    [TestClass]
    public class SectionUnitTest
    {
        [TestMethod]
        public void GetAllShortNames()
        {
            Mock<IRepository<Section>> mock = new Mock<IRepository<Section>>();
            mock.Setup(m => m.Repository).Returns(new Section []
            {
                new Section { id = 1, short_name = "IMZ1" },
                new Section { id = 2, short_name = "IMZ2" },
                new Section { id = 3, short_name = "IMZ3" },
                new Section { id = 4, short_name = "IMR1" },
                new Section { id = 5, short_name = "IMR2" },
                new Section { id = 3, short_name = "IMZ3" },
            }.AsQueryable());

            SectionController controller = new SectionController(mock.Object);

            Dictionary<int, string> lista = controller.GetAllShortNameSections();
            Assert.AreEqual(lista.Count, 5);
            Assert.AreEqual(lista[4], "IMR1");
        }
    }
}
