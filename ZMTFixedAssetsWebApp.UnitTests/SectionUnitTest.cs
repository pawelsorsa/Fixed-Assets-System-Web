using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ZMTFixedAssetsWebApp.Domain.Abstract;
using ZMTFixedAssetsWebApp.Domain.Model;
using ZMTFixedAssetsWebApp.WebUI.Controllers;
using ZMTFixedAssetsWebApp.WebUI.Models;
using System.Web.Mvc;
using System.Data.Entity.Infrastructure;
using System.Web;
using System.Web.Routing;


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


        [TestMethod]
        public void GetAllSections()
        {
            Mock<IRepository<Section>> mock_section = new Mock<IRepository<Section>>();
            mock_section.Setup(m => m.Repository).Returns(CreateSectionTab().AsQueryable());

            SectionController controller = new SectionController(mock_section.Object);
            Assert.AreEqual(mock_section.Object.Repository.Count(), 6);
        }

        [TestMethod]
        public void CanAddSection()
        {
            Mock<IRepository<Section>> mock_section = new Mock<IRepository<Section>>();
            mock_section.Setup(m => m.Repository).Returns(CreateSectionTab().AsQueryable());

            SectionController controller = new SectionController(mock_section.Object);
            SectionModel section = new SectionModel();

            section.email = "imz8@plk-sa.pl";
            section.locality = "Gdańsk";
            section.name = "XXX";
            section.phone_number = "552134152";
            section.post = "Gdańsk";
            section.postal_code = "22-242";
            section.short_name = "IMZ8";
            section.street = "Królewska 11";

            var redirectToRouteResult = controller.Add(section) as RedirectToRouteResult;
            mock_section.Verify(m => m.AddObject(It.IsAny<Section>()), Times.Once());

            Assert.IsNotNull(redirectToRouteResult);
            Assert.AreEqual("Index", redirectToRouteResult.RouteValues["Action"]);

        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CantAddSection()
        {
            Mock<IRepository<Section>> mock_section = new Mock<IRepository<Section>>();
            mock_section.Setup(m => m.Repository).Returns(CreateSectionTab().AsQueryable());

            SectionController controller = new SectionController(mock_section.Object);
            SectionModel section = null;
            var redirectToRouteResult = controller.Add(section) as RedirectToRouteResult;
            mock_section.Verify(m => m.AddObject(It.IsAny<Section>()), Times.Once());

            Assert.IsNotNull(redirectToRouteResult);
            Assert.AreEqual("Index", redirectToRouteResult.RouteValues["Action"]);
        }

        [TestMethod]
        public void CanEditSection()
        {
            Mock<IRepository<Section>> mock_section = new Mock<IRepository<Section>>();
            mock_section.Setup(m => m.Repository).Returns(CreateSectionTab().AsQueryable());

            SectionController controller = new SectionController(mock_section.Object);
            SectionModel section = new SectionModel();
           
            Section temp = mock_section.Object.Repository.FirstOrDefault(x => x.short_name == "IMZ6");
            Assert.IsNotNull(temp);
            Assert.AreEqual(temp.id, 6);
            Assert.AreEqual(temp.name, "FFF");
            Assert.AreEqual(temp.email, "imz6@plk-sa.pl");
            Assert.AreEqual(temp.locality, "Łódź");
            Assert.AreEqual(temp.phone_number, "723178153");
            Assert.AreEqual(temp.postal_code, "22-336");
            Assert.AreEqual(temp.post, "Łódź");
            Assert.AreEqual(temp.short_name, "IMZ6");
            Assert.AreEqual(temp.street, "Spółdzielców 61");

            section.id = 6;
            section.email = "imz111@plk-sa.pl";
            section.locality = "Radom";
            section.name = "YYY";
            section.phone_number = "551111111";
            section.post = "Radom";
            section.postal_code = "11-111";
            section.short_name = "IMZ111";
            section.street = "Łódzka 55";

            var redirectToRouteResult = controller.Edit(section) as RedirectToRouteResult;


            temp = mock_section.Object.Repository.FirstOrDefault(x => x.id == 6);
            Assert.AreEqual(temp.name, "YYY");
            Assert.AreEqual(temp.email, "imz111@plk-sa.pl");
            Assert.AreEqual(temp.locality, "Radom");
            Assert.AreEqual(temp.phone_number, "551111111");
            Assert.AreEqual(temp.postal_code, "11-111");
            Assert.AreEqual(temp.post, "Radom");
            Assert.AreEqual(temp.short_name, "IMZ111");
            Assert.AreEqual(temp.street, "Łódzka 55");

            mock_section.Verify(m => m.EditObject(temp), Times.Once());

            Assert.IsNotNull(redirectToRouteResult);
            Assert.AreEqual("Index", redirectToRouteResult.RouteValues["Action"]);
        }



        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CantEditSection()
        {
            Mock<IRepository<Section>> mock_section = new Mock<IRepository<Section>>();
            mock_section.Setup(m => m.Repository).Returns(CreateSectionTab().AsQueryable());

            SectionController controller = new SectionController(mock_section.Object);
            SectionModel section = null;

            var redirectToRouteResult = controller.Edit(section) as RedirectToRouteResult;

            mock_section.Verify(m => m.EditObject(It.IsAny<Section>()), Times.Never());

            Assert.IsNotNull(redirectToRouteResult);
            Assert.AreEqual("Index", redirectToRouteResult.RouteValues["Action"]);
        }



        [TestMethod]
        public void CanDeleteSection()
        {
            Mock<IRepository<Section>> mock_section = new Mock<IRepository<Section>>();
            mock_section.Setup(m => m.Repository).Returns(CreateSectionTab().AsQueryable());

            SectionController controller = new SectionController(mock_section.Object);
            DeleteObjectByName model = new DeleteObjectByName() { Name = "IMZ4" };

            var redirectToRouteResult = controller.Delete(model) as RedirectToRouteResult;
            mock_section.Verify(m => m.DeleteObject(It.IsAny<Section>()), Times.Once());

            Assert.IsNotNull(redirectToRouteResult);
            Assert.AreEqual("Index", redirectToRouteResult.RouteValues["Action"]);
        }


        [TestMethod]
        public void CantDeleteSection()
        {
            Mock<HttpRequestBase> request = new Mock<HttpRequestBase>();
            Mock<HttpResponseBase> response = new Mock<HttpResponseBase>();
            Mock<HttpContextBase> context = new Mock<HttpContextBase>();

            context.Setup(c => c.Request).Returns(request.Object);
            context.Setup(c => c.Response).Returns(response.Object);
            //Add XMLHttpRequest request header
            request.Setup(req => req["X-Requested-With"]).
                Returns("XMLHttpRequest");


            Mock<IRepository<Section>> mock_section = new Mock<IRepository<Section>>();
            mock_section.Setup(m => m.Repository).Returns(CreateSectionTab().AsQueryable());

            SectionController controller = new SectionController(mock_section.Object);
            controller.ControllerContext = new ControllerContext(
            context.Object, new RouteData(), controller);
            DeleteObjectByName model = new DeleteObjectByName() { Name = "IMZ4444" };
            var viewResult = controller.Delete(model.Name) as PartialViewResult;

            Assert.IsNotNull(viewResult);
            Assert.AreEqual("_Info", viewResult.ViewName);
            Assert.AreEqual("Podana sekcja nie istnieje", ((InfoModel)viewResult.Model).Description);
            Assert.AreEqual("Index", ((InfoModel)viewResult.Model).Action);
            Assert.AreEqual("Section", ((InfoModel)viewResult.Model).Controller);
            mock_section.Verify(m => m.DeleteObject(It.IsAny<Section>()), Times.Never());
        }

        [TestMethod]
        public void CantDeleteSectionIfPersonListExist()
        {
            Mock<HttpRequestBase> request = new Mock<HttpRequestBase>();
            Mock<HttpResponseBase> response = new Mock<HttpResponseBase>();
            Mock<HttpContextBase> context = new Mock<HttpContextBase>();

            context.Setup(c => c.Request).Returns(request.Object);
            context.Setup(c => c.Response).Returns(response.Object);
            //Add XMLHttpRequest request header
            request.Setup(req => req["X-Requested-With"]).
                Returns("XMLHttpRequest");


            Mock<IRepository<Section>> mock_section = new Mock<IRepository<Section>>();
            mock_section.Setup(m => m.Repository).Returns(CreateSectionTab().AsQueryable());

            SectionController controller = new SectionController(mock_section.Object);
            controller.ControllerContext = new ControllerContext(
            context.Object, new RouteData(), controller);
            DeleteObjectByName model = new DeleteObjectByName() { Name = "IMZ1" };
            var viewResult = controller.Delete(model.Name) as PartialViewResult;

            Assert.IsNotNull(viewResult);
            Assert.AreEqual("Section/_SectionDelete", viewResult.ViewName);
            mock_section.Verify(m => m.DeleteObject(It.IsAny<Section>()), Times.Never());
        }


        private Person[] CreatePersonTab()
        {
            return new Person[]
            {
                new Person { id = 1, name = "Paweł", surname = "Kowalski", area_code = 12, email = "f.kowalski@plk-sa.pl", phone_number = 3934133, phone_number2 = 666037123, id_section = 1  },
                new Person { id = 2, name = "Kamil", surname = "Nowak", area_code = 13, email = "k.nowak@plk-sa.pl", phone_number = 3934134, phone_number2 = 666037124, id_section = 2  },
                new Person { id = 3, name = "Damian", surname = "Waśniowski", area_code = 14, email= "d.wasniowski@plk-sa.pl", phone_number = 3934135, phone_number2 = 666037125, id_section = 3  },
                new Person { id = 4, name = "Julia", surname = "Kowalska", area_code = 15, email = "j.kowalska@plk-sa.pl", phone_number = 3934136, phone_number2 = 666037126, id_section = 4 },
                new Person { id = 5, name = "Filip", surname = "Konarski", area_code = 16, email = "f.konarski@plk-sa.pl", phone_number = 3934137, phone_number2 = 666037127, id_section = 5 },
                new Person { id = 6, name = "Jola", surname = "Poznańska", area_code = 17, email = "j.poznanska@plk-sa.pl", phone_number = 3934136, phone_number2 = 666037128, id_section = 6 },
            };
        }

        private Section[] CreateSectionTab()
        {
            return new Section[]
            {
                new Section { id = 1, short_name = "IMZ1", email = "imz1@plk-sa.pl", name = "AAA", locality = "Kraków", post = "Kraków", street = "Warszawska 4", postal_code = "11-231", phone_number = "123934233", People = CreatePersonTab() },
                new Section { id = 2, short_name = "IMZ2", email = "imz2@plk-sa.pl", name = "BBB", locality = "Kraków", post = "Kraków", street = "Warszawska 4", postal_code = "11-231", phone_number = "123934233" },
                new Section { id = 3, short_name = "IMZ3", email = "imz3@plk-sa.pl", name = "CCC", locality = "Warszawa", post = "Warszawa", street = "Krakowska 8", postal_code = "22-111", phone_number = "224934133", People = CreatePersonTab() },
                new Section { id = 4, short_name = "IMZ4", email = "imz4@plk-sa.pl", name = "DDD", locality = "Poznań", post = "Poznań", street = "Wrocławska 14", postal_code = "11-231", phone_number = "323934133", People = CreatePersonTab() },
                new Section { id = 5, short_name = "IMZ5", email = "imz5@plk-sa.pl", name = "EEE", locality = "Bydgoszcz", post = "Bydgoszcz", street = "Ludwikowo 51", postal_code = "41-531", phone_number = "523134143" },
                new Section { id = 6, short_name = "IMZ6", email = "imz6@plk-sa.pl", name = "FFF", locality = "Łódź", post = "Łódź", street = "Spółdzielców 61", postal_code = "22-336", phone_number = "723178153" },
            };
        }


    }
}
