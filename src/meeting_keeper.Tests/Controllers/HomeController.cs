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



        [Fact]
        public void ReturnsView()
        {
            // Act
            var result = _homeController.Index();

            // Assert
            Assert.NotNull(result);
        }

        // NOT working while direct view that is returned is determined in core, not in method call itself!
        //[Fact]
        //public void ReturnsIndex()
        //{
        //    // Act
        //    var result = _homeController.Index() as ViewResult;

        //    // Assert
        //    Assert.Equal("Index", result.ViewName);
        //}


    }

}
