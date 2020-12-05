using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Victor_WebStore.Controllers;
using Victor_WebStore.Interfaces.TestApi;
using Assert = Xunit.Assert;

namespace Victor_WebStore.Tests.Controllers
{
    [TestClass]
    public class WebApiControllerTests
    {
        Mock<IValueService> _value_service_mock;
        WebApiController _controller;

        [TestInitialize]
        public void InitTest()
        {
            var excepted_values = new[] { "1", "2", "3" };
            _value_service_mock = new Mock<IValueService>();
            _value_service_mock
                .Setup(service => service.Get())
                .Returns(excepted_values);

            _controller = new WebApiController(_value_service_mock.Object);
        }

        [TestMethod]
        public void Index_Return_Views_with_IEnumerableString()
        {
            //Стаб
            const int expected_count = 3;
            var result = _controller.Index();
            var result_view = Assert.IsType<ViewResult>(result);
            var result_model = Assert.IsAssignableFrom<IEnumerable<string>>(result_view.Model);
            Assert.Equal(expected_count, result_model.Count());

            //Mok

            _value_service_mock.Verify(service => service.Get());
            _value_service_mock.VerifyNoOtherCalls();
        }

        
    }
}
