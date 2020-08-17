using NUnit.Framework;
using RaceApp.Models;
using System;
using System.Collections.Generic;

namespace RaceApp.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShouldNotPassInspectionWhenVehicleDoesNotHaveAnyInspections()
        {
            Vehicle v = new Vehicle { Id = Guid.NewGuid(), InspectionTypes = null };
            Assert.IsFalse(v.Inspected);
        }

        [Test]
        public void ShouldThrowExceptionWhenProvideInvalidType()
        {
            List<Inspection> carInspection = new List<Inspection> {
                new Inspection { Id = Guid.NewGuid(), InspectionType = InspectionTypeEnum.TOW_STRAP, Passed = false },
                new Inspection { Id = Guid.NewGuid(), InspectionType = InspectionTypeEnum.LESS_THAN_85_TIRE_WEAR, Passed = true }
            };
            Vehicle v = new Vehicle { Id = Guid.NewGuid(), InspectionTypes = carInspection, Type = (VehicleTypeEnum)3 };
            Assert.That(() => { return v.Inspected; }, Throws.Exception.TypeOf<Exception>().With.Property(nameof(Exception.Message)).EqualTo("Invalid vehicle type"));
        }

        [Test]
        public void ShouldNotPassInspectionWhenCarDoesNotHaveAnyInspections()
        {
            Vehicle v = new Vehicle { Id = Guid.NewGuid(), InspectionTypes = null, Type = VehicleTypeEnum.Car };
            Assert.IsFalse(v.Inspected);
        }

        [Test]
        public void ShouldNotPassInspectionWhenCarDoesNotHaveTowStrap()
        {
            List<Inspection> carInspection = new List<Inspection> {
                new Inspection { Id = Guid.NewGuid(), InspectionType = InspectionTypeEnum.TOW_STRAP, Passed = false },
                new Inspection { Id = Guid.NewGuid(), InspectionType = InspectionTypeEnum.LESS_THAN_85_TIRE_WEAR, Passed = true }
            };
            Vehicle v = new Vehicle { Id = Guid.NewGuid(), InspectionTypes = carInspection, Type = VehicleTypeEnum.Car };
            Assert.IsFalse(v.Inspected);
        }

        [Test]
        public void ShouldNotPassInspectionWhenCarHaveMoreThan85TireWear()
        {
            List<Inspection> carInspection = new List<Inspection> {
                new Inspection { Id = Guid.NewGuid(), InspectionType = InspectionTypeEnum.TOW_STRAP, Passed = true },
                new Inspection { Id = Guid.NewGuid(), InspectionType = InspectionTypeEnum.LESS_THAN_85_TIRE_WEAR, Passed = false }
            };
            Vehicle v = new Vehicle { Id = Guid.NewGuid(), InspectionTypes = carInspection, Type = VehicleTypeEnum.Car };
            Assert.IsFalse(v.Inspected);
        }

        [Test]
        public void ShouldPassInspectionForCar()
        {
            List<Inspection> carInspection = new List<Inspection> {
                new Inspection { Id = Guid.NewGuid(), InspectionType = InspectionTypeEnum.TOW_STRAP, Passed = true },
                new Inspection { Id = Guid.NewGuid(), InspectionType = InspectionTypeEnum.LESS_THAN_85_TIRE_WEAR, Passed = true }
            };
            Vehicle v = new Vehicle { Id = Guid.NewGuid(), InspectionTypes = carInspection, Type = VehicleTypeEnum.Car };
            Assert.IsTrue(v.Inspected);
        }

        [Test]
        public void ShouldNotPassInspectionWhenTruckDoesNotHaveTowStrap()
        {
            List<Inspection> truckInspection = new List<Inspection> {
                new Inspection { Id = Guid.NewGuid(), InspectionType = InspectionTypeEnum.TOW_STRAP, Passed = false },
                new Inspection { Id = Guid.NewGuid(), InspectionType = InspectionTypeEnum.NOT_LIFTED_MORE_THAN_5_INCHES, Passed = true }
            };
            Vehicle v = new Vehicle { Id = Guid.NewGuid(), InspectionTypes = truckInspection, Type = VehicleTypeEnum.Truck };
            Assert.IsFalse(v.Inspected);
        }

        [Test]
        public void ShouldNotPassInspectionWhenTruckHaveMoreThan85TireWear()
        {
            List<Inspection> truckInspection = new List<Inspection> {
                new Inspection { Id = Guid.NewGuid(), InspectionType = InspectionTypeEnum.TOW_STRAP, Passed = true },
                new Inspection { Id = Guid.NewGuid(), InspectionType = InspectionTypeEnum.NOT_LIFTED_MORE_THAN_5_INCHES, Passed = false }
            };
            Vehicle v = new Vehicle { Id = Guid.NewGuid(), InspectionTypes = truckInspection, Type = VehicleTypeEnum.Truck };
            Assert.IsFalse(v.Inspected);
        }

        [Test]
        public void ShouldPassInspectionForTruck()
        {
            List<Inspection> truckInspection = new List<Inspection> {
                new Inspection { Id = Guid.NewGuid(), InspectionType = InspectionTypeEnum.TOW_STRAP, Passed = true },
                new Inspection { Id = Guid.NewGuid(), InspectionType = InspectionTypeEnum.NOT_LIFTED_MORE_THAN_5_INCHES, Passed = true }
            };
            Vehicle v = new Vehicle { Id = Guid.NewGuid(), InspectionTypes = truckInspection, Type = VehicleTypeEnum.Truck };
            Assert.IsTrue(v.Inspected);
        }

        [Test]
        public void ShouldBeInvalidWhenTopSpeedIsNotGreaterThanZero()
        {
            Vehicle v = new Vehicle { Id = Guid.NewGuid(), Model = "test", TopSpeed = 0, Type = VehicleTypeEnum.Truck };
            Assert.IsFalse(v.IsValid);
        }

        [Test]
        public void ShouldBeInvalidWhenModelIsZero()
        {
            Vehicle v = new Vehicle { Id = Guid.NewGuid(), Model = null, TopSpeed = 100, Type = VehicleTypeEnum.Truck };
            Assert.IsFalse(v.IsValid);
        }

        [Test]
        public void ShouldBeInvalidWhenHasInvalidType()
        {
            Vehicle v = new Vehicle { Id = Guid.NewGuid(), Model = "test", TopSpeed = 100, Type = (VehicleTypeEnum)3 };
            Assert.IsFalse(v.IsValid);
        }
    }
}