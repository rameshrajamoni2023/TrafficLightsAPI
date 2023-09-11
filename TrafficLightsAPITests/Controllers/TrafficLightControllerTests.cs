using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrafficLightsAPI.Controllers;
using TrafficLightsAPI.Model;
using TrafficLightsAPI.Services;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using TrafficLightsAPI.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.Extensions.Configuration;

namespace TrafficLightController.Tests
{

    [TestClass()]
    public class TrafficLightControllerTests
    {
        Mock<TrafficLightService> mockTrafficLightService = new Mock<TrafficLightService>();
        Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
        Mock<ILogger> mockLogger = new Mock<ILogger>();
        Dictionary<string, TrafficLight> trafficLights = new Dictionary<string, TrafficLight>();

        [TestInitialize]
        public void TestInitialize()
        {
            //add initialization code here
            // Initialize an empty dictionary
            // Add items to the dictionary
            trafficLights["North"] = new TrafficLight();
            trafficLights["South"] = new TrafficLight();
            trafficLights["East"] = new TrafficLight();
            trafficLights["West"] = new TrafficLight();

        }

        [TestCleanup]
        public void TestCleanup()
        {
            //add clean up code here
        }



        [TestMethod]
        public void GetTrafficLights_ReturnsOkResult()
        {
    
        }

        [TestMethod]
        public void SetTrafficLightColor_ReturnsOkResult()
        {

        }

        [TestMethod]
        public void GetConfiguration_ReturnsOkResult()
        {

        }

    }
}