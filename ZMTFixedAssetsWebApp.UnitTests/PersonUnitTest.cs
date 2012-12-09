using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ZMTFixedAssetsWebApp.Domain.Abstract;
using ZMTFixedAssetsWebApp.Domain.Model;
using ZMTFixedAssetsWebApp.WebUI.Controllers;
using System.Web.Mvc;
using ZMTFixedAssetsWebApp.WebUI.Models;

namespace ZMTFixedAssetsWebApp.UnitTests
{
    [TestClass]
    public class PersonUnitTest
    {
        [TestMethod]
        public void GetAllPeople()
        {
            Mock<IPersonRepository> mock_person = new Mock<IPersonRepository>();
            Mock<ISectionRepository> mock_section = new Mock<ISectionRepository>();
            mock_section.Setup(m => m.Sections).Returns(CreateSectionTab().AsQueryable());
            mock_person.Setup(m => m.People).Returns(CreatePersonTab().AsQueryable());

            PersonController controller = new PersonController(mock_person.Object, mock_section.Object);

            Assert.AreEqual(mock_person.Object.People.Count(), 6);
        }

        [TestMethod]
        public void CanAddPerson()
        {
            Mock<IPersonRepository> mock_person = new Mock<IPersonRepository>();
            Mock<ISectionRepository> mock_section = new Mock<ISectionRepository>();

            mock_section.Setup(m => m.Sections).Returns(CreateSectionTab().AsQueryable());
            mock_person.Setup(m => m.People).Returns(CreatePersonTab().AsQueryable());

            PersonController controller = new PersonController(mock_person.Object, mock_section.Object);
            PersonSectionAddEditModel personSection = new PersonSectionAddEditModel();

            personSection.id = 5;
            personSection.name = "Zbigniew";
            personSection.surname = "Strzelec";
            personSection.area_code = 44;
            personSection.email = "w.strzelec@plk-sa.pl";
            personSection.section_name = "4";
            personSection.phone_number = 12345;
            personSection.phone_number2 = 666666;

            var redirectToRouteResult = controller.Add(personSection) as RedirectToRouteResult;
            mock_person.Verify(m => m.AddPerson(It.IsAny<Person>()), Times.Once());

            Assert.IsNotNull(redirectToRouteResult);
            Assert.AreEqual("Index", redirectToRouteResult.RouteValues["Action"]);
        }


        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CantAddPerson()
        {
            Mock<IPersonRepository> mock_person = new Mock<IPersonRepository>();
            Mock<ISectionRepository> mock_section = new Mock<ISectionRepository>();

            mock_section.Setup(m => m.Sections).Returns(CreateSectionTab().AsQueryable());
            mock_person.Setup(m => m.People).Returns(CreatePersonTab().AsQueryable());

            PersonController controller = new PersonController(mock_person.Object, mock_section.Object);
            PersonSectionAddEditModel personSection = null;
            var redirectToRouteResult = controller.Add(personSection) as RedirectToRouteResult;
            mock_person.Verify(m => m.AddPerson(It.IsAny<Person>()), Times.Once());

            Assert.IsNotNull(redirectToRouteResult);
            Assert.AreEqual("Index", redirectToRouteResult.RouteValues["Action"]);
        }


        [TestMethod]
        public void CanEditPerson()
        {
            Mock<IPersonRepository> mock_person = new Mock<IPersonRepository>();
            Mock<ISectionRepository> mock_section = new Mock<ISectionRepository>();

            mock_section.Setup(m => m.Sections).Returns(CreateSectionTab().AsQueryable());
            mock_person.Setup(m => m.People).Returns(CreatePersonTab().AsQueryable());

            PersonController controller = new PersonController(mock_person.Object, mock_section.Object);
            PersonSectionAddEditModel personSection = new PersonSectionAddEditModel();

            personSection.id = 5;
            personSection.name = "Zbigniew";
            personSection.surname = "Strzelec";
            personSection.area_code = 44;
            personSection.email = "w.strzelec@plk-sa.pl";
            personSection.section_name = "4";
            personSection.phone_number = 12345;
            personSection.phone_number2 = 666666;

            var redirectToRouteResult = controller.Edit(personSection) as RedirectToRouteResult;

            Person temp = mock_person.Object.People.FirstOrDefault(x => x.id == 5);
            Assert.AreEqual(temp.name, "ZBIGNIEW");
            Assert.AreEqual(temp.surname, "STRZELEC");
            Assert.AreEqual(temp.area_code, 44);
            Assert.AreEqual(temp.email, "w.strzelec@plk-sa.pl");
            Assert.AreEqual(temp.id_section, 4);
            Assert.AreEqual(temp.phone_number, 12345);
            Assert.AreEqual(temp.phone_number2, 666666);

            mock_person.Verify(m => m.EditPerson(temp), Times.Once());

            Assert.IsNotNull(redirectToRouteResult);
            Assert.AreEqual("Index", redirectToRouteResult.RouteValues["Action"]);
        }


        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CantEditPerson()
        {
            Mock<IPersonRepository> mock_person = new Mock<IPersonRepository>();
            Mock<ISectionRepository> mock_section = new Mock<ISectionRepository>();

            mock_section.Setup(m => m.Sections).Returns(CreateSectionTab().AsQueryable());
            mock_person.Setup(m => m.People).Returns(CreatePersonTab().AsQueryable());

            PersonController controller = new PersonController(mock_person.Object, mock_section.Object);
            PersonSectionAddEditModel personSection = null;

            var redirectToRouteResult = controller.Edit(personSection) as RedirectToRouteResult;

            mock_person.Verify(m => m.EditPerson(It.IsAny<Person>()), Times.Never());

            Assert.IsNotNull(redirectToRouteResult);
            Assert.AreEqual("Index", redirectToRouteResult.RouteValues["Action"]);
        }


        [TestMethod]
        public void CanDeletePerson()
        { 
            Mock<IPersonRepository> mock_person = new Mock<IPersonRepository>();
            Mock<ISectionRepository> mock_section = new Mock<ISectionRepository>();
            
            mock_section.Setup(m=>m.Sections).Returns(CreateSectionTab().AsQueryable());
            mock_person.Setup(m => m.People).Returns(CreatePersonTab().AsQueryable());

            PersonController controller = new PersonController(mock_person.Object, mock_section.Object);
            DeleteObject model = new DeleteObject() { Description = "XXXX", Id = 5 };

            var redirectToRouteResult = controller.Delete(model) as RedirectToRouteResult;

            Assert.IsNotNull(redirectToRouteResult);
            Assert.AreEqual("Index", redirectToRouteResult.RouteValues["Action"]);
            mock_person.Verify(m => m.DeletePerson(model.Id), Times.Once());
        }

        [TestMethod]
        public void CantDeletePerson()
        {
            Mock<IPersonRepository> mock_person = new Mock<IPersonRepository>();
            Mock<ISectionRepository> mock_section = new Mock<ISectionRepository>();

            mock_section.Setup(m => m.Sections).Returns(CreateSectionTab().AsQueryable());
            mock_person.Setup(m => m.People).Returns(CreatePersonTab().AsQueryable());

            PersonController controller = new PersonController(mock_person.Object, mock_section.Object);
            DeleteObject model = new DeleteObject() { Description = "XXXX", Id = 155 };
            var viewResult = controller.Delete(model.Id) as ViewResult;
            
            Assert.IsNotNull(viewResult);
            Assert.AreEqual("Info", viewResult.ViewName);
            Assert.AreEqual("Podany pracownik nie istnieje", ((InfoModel)viewResult.Model).Description);
            Assert.AreEqual("Index", ((InfoModel)viewResult.Model).Action);
            Assert.AreEqual("Person", ((InfoModel)viewResult.Model).Controller);
            
            mock_person.Verify(m => m.DeletePerson(model.Id), Times.Never());
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
                new Section { id = 1, short_name = "IMZ1" },
                new Section { id = 2, short_name = "IMZ2" },
                new Section { id = 3, short_name = "IMZ3" },
                new Section { id = 4, short_name = "IMZ4" },
                new Section { id = 5, short_name = "IMZ5" },
                new Section { id = 6, short_name = "IMZ6" },
            };

        }

    }
}
