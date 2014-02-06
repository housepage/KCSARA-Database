﻿/*
 * Copyright 2014 Matthew Cosand
 */
namespace Kcsara.Database.Web.api.Models
{
  using System;
  using M = Kcsar.Database.Model;

  public class MissionResponse
  {
    public Guid Id { get; set; }
    public Mission Mission { get; set; }
    public Responder Responder { get; set; }
    public RespondingUnit Unit { get; set; }
    public string Status { get; set; }
    public string Role { get; set; }
    public DateTime? Updated { get; set; }
    public decimal? Hours { get; set; }
    public int? Miles { get; set; }
    public GeoLocation Location { get; set; }

    public static MissionResponse FromDatabase(M.MissionResponder r, bool doResponder = false, bool doUnit = false, bool doMission = false)
    {
      return new MissionResponse
      {
        Id = r.Id,
        Responder = doResponder ? Responder.FromDatabase(r) : null,
        Mission = doMission ? Mission.FromDatabase(r.Mission) : null,
        Unit = doUnit ? RespondingUnit.FromDatabase(r.RespondingUnit) : null,
        Status = r.LastTimeline == null ? "Unknown" : r.LastTimeline.Status.ToString(),
        Updated = r.LastTimeline == null ? (DateTime?)null : r.LastTimeline.Time,
        Role = r.Role,
        Hours = r.Hours,
        Miles = r.Miles,
        Location = r.LastTimeline == null ? null : GeoLocation.FromGrography(r.LastTimeline.Location)
      };
    }
  }
}