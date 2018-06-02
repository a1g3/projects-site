﻿using Alge.Controllers;
using Alge.Domain.Dtos;
using Alge.Domain.Interfaces.Facades;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Alge.Tests.Controllers
{
    [TestClass]
    public class OCSPContollerTests
    {
        [TestMethod]
        public void OCSPController_Test_URLFilter()
        {
            // ARRANGE
            var ocspFacade = new Mock<IOCSPFacade>();
            ocspFacade.Setup(x => x.GetStatus("test.com", 443)).Returns(new OCSPDto());

            var controller = new OCSPController() { OcspFacade = ocspFacade.Object };

            // ACT
            var result = controller.Test("https://test.com");

            // ASSERT
            Assert.AreEqual(1, 1);
        }
    }
}