using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using TrafficLightsAPI.Application.Contracts;
using TrafficLightsAPI.Data;
using TrafficLightsAPI.Model;
using TrafficLightsAPI.Services;
using System;
using System.Timers;

namespace TrafficLightServiceTests.Tests
{
    [TestClass]
    public class TrafficLightServiceTests
    {
        [TestMethod]
        public void GetTrafficLights_ReturnsExpectedResult()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<TrafficLightService>>();
            var mockDbContext = new Mock<TrafficLightDbContext>();
            var mockTrafficLightSettings = new Mock<TrafficLightSettings>();

            var service = new TrafficLightService(null, mockLogger.Object, mockTrafficLightSettings.Object);

            // Act
            var result = service.GetTrafficLights();

            // Assert
            Assert.IsInstanceOfType(result, typeof(Dictionary<string, TrafficLight>));
            Assert.AreEqual(4, result.Count); // Assuming you have 4 traffic lights
        }

        [TestMethod]
        public void SetTrafficLightColor_SetsColor()
        {
            // Arrange
            var direction = "North";
            var newColor = TrafficLightColor.Green;

            var mockLogger = new Mock<ILogger<TrafficLightService>>();
            var mockDbContext = new Mock<TrafficLightDbContext>();
            var mockTrafficLightSettings = new Mock<TrafficLightSettings>();

            var service = new TrafficLightService(null, mockLogger.Object, mockTrafficLightSettings.Object);

            // Act
            service.SetTrafficLightColor(direction, newColor);

            // Assert
            Assert.AreEqual(newColor, service.GetTrafficLights()[direction].CurrentColor);
        }

        [TestMethod]
        public void TimerElapsed_UpdatesTrafficLights()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<TrafficLightService>>();
            var mockDbContext = new Mock<TrafficLightDbContext>();
            var mockTrafficLightSettings = new Mock<TrafficLightSettings>();

            var service = new TrafficLightService(null, mockLogger.Object, mockTrafficLightSettings.Object);

            // Act
            // Simulate timer elapsed event (you can use a testing framework for this)
            // For simplicity, you can call service.TimerElapsed(sender, e) directly.

            // Assert
            // Write assertions to verify that traffic light states have changed as expected.
        }
    }
}
