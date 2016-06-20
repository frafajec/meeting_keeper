using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNet.Mvc;

namespace meeting_keeper.Tests.Controllers
{

    public class HomeController
    {

        private meeting_keeper.Controllers.HomeController _homeController;
        public HomeController() {
            // Arrange
            _homeController = new meeting_keeper.Controllers.HomeController();
        }



        //[Fact]
        //public void ReturnsView()
        //{
        //    // Act
        //    var result = _homeController.Index();

        //    // Assert
        //    Assert.NotNull(result);
        //}

        // NOT working while direct view that is returned is determined in core, not in method call itself!


        [Fact]
        public void ReturnsIndex()
        {
            // Act
            var result = _homeController.Index();

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void ReturnsAbout()
        {
            // Act
            var result = _homeController.About();

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void ReturnsContact()
        {
            // Act
            var result = _homeController.Contact();

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void ReturnsCookie()
        {
            // Act
            var result = _homeController.Cookie();

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void ReturnsSubscribe()
        {
            // Act
            var result = _homeController.Subscribe();

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void ReturnsTerms()
        {
            // Act
            var result = _homeController.Terms();

            // Assert
            Assert.NotNull(result);
        }


    }

}
