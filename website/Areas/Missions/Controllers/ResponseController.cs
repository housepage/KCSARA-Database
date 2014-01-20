﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kcsar.Database.Model;
using Kcsara.Database.Services;
using Kcsara.Database.Web.Areas.Missions.api;
using Kcsara.Database.Web.Controllers;
using log4net;

namespace Kcsara.Database.Web.Areas.Missions.Controllers
{
  public class ResponseController : BaseController
  {
    public ResponseController(IKcsarContext db, IAuthService auth, ILog log, IAppSettings appSettings)
      : base(db, auth, log, appSettings)
    {
    }

    [Authorize]
    public ActionResult Index()
    {
      var responseApi = new ResponseApiController(this.db, this.Permissions, log);
      ViewBag.Data = responseApi.GetCurrentStatus();
      return View();
    }

    [Authorize]
    public ActionResult Create()
    {
      ViewBag.IsMissionEditor = Permissions.IsInRole("cdb.missioneditors");
      return View();
    }

    public ActionResult Info(Guid id)
    {
      ViewBag.Data = new { Title = "Foo Mission" };
      return View();
    }

    [Authorize]
    public ActionResult Dashboard()
    {
      return View();
    }

  }
}
