using NUnit.Framework;
using RaceApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RaceApp.Test
{
    class TrackTests
    {
        [Test]
        public void ShouldBeInvalidWhenTrackHasNoCars()
        {
            Track track = new Track { Length = 20, Location = "test", Vehicles = new List<Vehicle>() };
            Assert.IsFalse(track.IsValidTrack);
        }

        [Test]
        public void ShouldBeInvalidWhenTrackHasNoLength()
        {
            Vehicle vehicle = new Vehicle { Id = Guid.NewGuid(), Model = "Nissan GTR", TopSpeed = 315, Type = VehicleTypeEnum.Car };
            Track track = new Track { Location = "test", Vehicles = new List<Vehicle>() { vehicle } };
            Assert.IsFalse(track.IsValidTrack);
        }

        [Test]
        public void ShouldBeInvalidWhenTrackHasNoLocation()
        {
            Vehicle vehicle = new Vehicle { Id = Guid.NewGuid(), Model = "Nissan GTR", TopSpeed = 315, Type = VehicleTypeEnum.Car };
            Track track = new Track { Length = 10, Vehicles = new List<Vehicle>() { vehicle } };
            Assert.IsFalse(track.IsValidTrack);
        }

        [Test]
        public void ShouldBeValid()
        {
            List<Inspection> carInspection = new List<Inspection> {
                new Inspection { Id = Guid.NewGuid(), InspectionType = InspectionTypeEnum.TOW_STRAP, Passed = true },
                new Inspection { Id = Guid.NewGuid(), InspectionType = InspectionTypeEnum.LESS_THAN_85_TIRE_WEAR, Passed = true }
            };
            Vehicle vehicle = new Vehicle { Id = Guid.NewGuid(), Model = "Nissan GTR", TopSpeed = 315, Type = VehicleTypeEnum.Car, InspectionTypes = carInspection };
            Track track = new Track { Length = 10, Vehicles = new List<Vehicle>() { vehicle } };
            Assert.IsFalse(track.IsValidTrack);
        }
    }
}
