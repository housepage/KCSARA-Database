﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Internal.Website.Api;
using api = Kcsara.Database.Web.api.Models;
using Kcsara.Database.Web.Areas.Missions.Controllers;
using NUnit.Framework;
using Moq;
using Kcsara.Database.Services;
using Kcsara.Database.Web.Controllers;

namespace Internal.Website
{
  [TestFixture]
  public class ResponseControllerTests
  {
    [Test]
    public void Index()
    {
      var result = TestSimpleView<ViewResult>(f => f.Index());
      Assert.IsNotNull(result.ViewBag.Data, "missing data");
      Assert.IsInstanceOf<api.MissionResponseStatus[]>(result.ViewBag.Data);
    }

    [Test]
    public void Create()
    {
      var result = TestSimpleView<ViewResult>(f => f.Create());
      Assert.IsTrue(result.ViewBag.IsMissionEditor, "mission editor");
    }

    [Test]
    public void Dashboard()
    {
      var result = TestSimpleView<ViewResult>(f => f.Dashboard());
    }

    [Test]
    public void Info()
    {
      var mockAuth = new Mock<IAuthService>();
      mockAuth.Setup(f => f.IsInRole(It.IsAny<string>())).Returns(true);
      var mockData = ResponseApiControllerTests.GetBasicResponseData();
      var controller = new ResponseController(new ControllerArgs(mockData.Object, mockAuth.Object, new ConsoleLogger(), null));

      var result = (ViewResult)controller.Info(mockData.Object.Missions.First().Id);
      //TODO: checks
      Assert.IsNotNull(result, "not null");
      

    }

    private T TestSimpleView<T>(Func<ResponseController, ActionResult> method) where T : ActionResult
    {
      var mockAuth = new Mock<IAuthService>();
      mockAuth.Setup(f => f.IsInRole(It.IsAny<string>())).Returns(true);
      return TestSimpleView<T>(method, mockAuth.Object);
    }

    private T TestSimpleView<T>(Func<ResponseController, ActionResult> method, IAuthService auth) where T : ActionResult
    {
      var mockData = ResponseApiControllerTests.GetBasicResponseData();
      var controller = new ResponseController(new ControllerArgs(mockData.Object, auth, new ConsoleLogger(), null));
      var result = method(controller);
      Assert.IsNotNull(result, "not null");
      Assert.IsInstanceOf<T>(result, "return type");

      return (T)result;
    }
  }
}
