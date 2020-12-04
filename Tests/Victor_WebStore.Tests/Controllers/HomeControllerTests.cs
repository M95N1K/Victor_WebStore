using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Victor_WebStore.Controllers;

using Assert = Xunit.Assert;

namespace Victor_WebStore.Tests
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void Index_Returns_View()
        {
            var controller = new HomeController();
            var result = controller.Index();
            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void NotFound404_Returns_View()
        {
            var controller = new HomeController();
            var result = controller.NotFound404();
            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void Blog_Returns_View()
        {
            var controller = new HomeController();
            var result = controller.Blog();
            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void BlogSingle_Returns_View()
        {
            var controller = new HomeController();
            var result = controller.BlogSingle();
            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void Contact_Returns_View()
        {
            var controller = new HomeController();
            var result = controller.Contact();
            Assert.IsType<ViewResult>(result);
        }
    }
}
