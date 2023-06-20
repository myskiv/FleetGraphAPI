using FleetGraphAPI.Database;
using FleetGraphAPI.Database.Models;
using FleetGraphAPI.GraphQL.Vehicles;
using HotChocolate.Subscriptions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace FleetGraphAPI.Test.GraphQL.Vehicles;

public class VehicleMutationsTests
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly Mock<ITopicEventSender> _eventSenderMock;
        private readonly VehicleMutations _mutations;

        public VehicleMutationsTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _dbContext = new ApplicationDbContext(options);
            _eventSenderMock = new Mock<ITopicEventSender>();
            _mutations = new VehicleMutations();
        }

        [Fact]
        public async Task AddVehicleAsync_ValidInput_ReturnsVehiclePayload()
        {
            // Arrange
            var input = new AddVehicleInput("Toyota", "Camry", 2023, "1234567890");

            var cancellationToken = CancellationToken.None;

            // Act
            var result = await _mutations.AddVehicleAsync(input, _dbContext, cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Vehicle);
            Assert.Equal(input.Make, result.Vehicle.Make);
            Assert.Equal(input.Model, result.Vehicle.Model);
            Assert.Equal(input.Year, result.Vehicle.Year);
            Assert.Equal(input.VIN, result.Vehicle.VIN);
        }

        [Fact]
        public async Task UpdateVehicleStatusAsync_ValidInput_ReturnsVehiclePayload()
        {
            // Arrange
            var input = new UpdateVehicleStatusInput(1, "New York", 75);

            var cancellationToken = CancellationToken.None;

            var existingVehicle = new Vehicle
            {
                Id = input.VehicleId,
                Make = "Ford",
                Model = "Mustang",
                Year = 2022,
                VIN = "0987654329"
            };
            _dbContext.Vehicles.Add(existingVehicle);
            
            // Act
            var result = await _mutations.UpdateVehicleStatusAsync(input, _dbContext, _eventSenderMock.Object, cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Vehicle);
            Assert.Equal(input.Location, result.Vehicle.CurrentLocation);
            Assert.Equal(input.FuelLevel, result.Vehicle.FuelLevel);
        }
    }