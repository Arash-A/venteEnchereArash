using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using venteTest.Controllers;
using venteTest.Models;
using venteTest.Services;
using Xunit;

namespace venteTest.Tests
{


    //packages to add:
    //    <PackageReference Include = "Microsoft.AspNetCore.All" Version="2.0.0" />
    //    <PackageReference Include = "Microsoft.AspNetCore.TestHost" Version="2.0.0" />
    //    <PackageReference Include = "FluentAssertions" Version="4.19.2" />
    //    <PackageReference Include = "Moq" Version="4.7.63" />

    public class UnitTest1
    {
        [Fact]
        public void HomeTest1()
        {
            HomeController sut = new venteTest.Controllers.HomeController();

            IActionResult result = sut.Contact();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void HomeTest2()
        {
            HomeController sut = new venteTest.Controllers.HomeController();

            IActionResult result = sut.Faq();

            Assert.IsType<ViewResult>(result);
        }
        [Fact]
        public void HomeTest3()
        {
            HomeController sut = new venteTest.Controllers.HomeController();

            IActionResult result = sut.Sent();

            Assert.IsType<ViewResult>(result);
        }

        //THIS TEST WILL FAIL ON PURPOSE
        [Fact]
        public void HomeTest4()
        {
            HomeController sut = new venteTest.Controllers.HomeController();

            IActionResult result = sut.Error();

            Assert.IsType<ViewResult>(result);
        }

        //THIS TEST WILL FAIL ON PURPOSE
        [Fact]
        public void AdminTest1()
        {
            AdminController sut = new venteTest.Controllers.AdminController();

            IActionResult result = (IActionResult)sut.Index();

            Assert.IsType<ViewResult>(result);
        }
    }
}
