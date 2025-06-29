using Application.ProcessSignalBoosterFile;
using Application.ProcessSignalBoosterFile.Interfaces;
using Application.ProcessSignalBoosterFile.Models;
using Application.ProcessSignalBoosterFile.Requests;
using Application.ProcessSignalBoosterFile.Responses;
using CSharpFunctionalExtensions;
using Domain.Enums;
using Moq;
using Xunit;

namespace SignalBoosterUnitTests.Application.ProcessSignalBoosterFileTests
{
    public class BuildJsonTests
    {
        [Fact]
        public async Task Process_Oxygen_Success()
        {
            // Arrange
            var mockSignalBooster = new Mock<ISignalBooster>();

            mockSignalBooster.Setup(x => x.Process(It.IsAny<SignalBoosterRequest>()))
                .ReturnsAsync(
                    Result.Success(new SignalBoosterResponse(It.IsAny<string>()) 
                        { 
                            AddOn = AddOn.Humidifier,
                            Device = Device.Oxygen,
                            MaskType = MaskType.FullFace,
                            OrderingProvider = "Dr. Smith",
                            Qualifier = "Oxygen therapy",
                            OxygenLiters = "2 L",
                            OxygenUsage = OxygenUsage.Sleep,                            
                    })); 

            var buildJson = new BuildJson(mockSignalBooster.Object);

            var request = new SignalBoosterRequest(It.IsAny<string>());

            // Act
            var resultProcess = await buildJson.Process(request);

            // Assert
            Assert.True(resultProcess.IsSuccess);
            Assert.Equal(@"{""device"":""Oxygen Tank"",""mask_type"":""full face"",""add_ons"":[""humidifier""],""qualifier"":""Oxygen therapy"",""ordering_provider"":""Dr. Smith"",""liters"":""2 L"",""usage"":""sleep""}",
                resultProcess.Value.JsonToSend);
        }

        [Fact]
        public async Task Process_CPAP_Success()
        {
            // Arrange
            var mockSignalBooster = new Mock<ISignalBooster>();

            mockSignalBooster.Setup(x => x.Process(It.IsAny<SignalBoosterRequest>()))
                .ReturnsAsync(
                    Result.Success(new SignalBoosterResponse(It.IsAny<string>())
                    {
                        AddOn = AddOn.Humidifier,
                        Device = Device.CPAP,
                        MaskType = MaskType.FullFace,
                        OrderingProvider = "Dr. Brown",
                        Qualifier = "Sleep apnea treatment",
                    }));

            var buildJson = new BuildJson(mockSignalBooster.Object);

            var request = new SignalBoosterRequest(It.IsAny<string>());

            // Act
            var resultProcess = await buildJson.Process(request);

            // Assert
            Assert.True(resultProcess.IsSuccess);
            Assert.Equal(@"{""device"":""CPAP"",""mask_type"":""full face"",""add_ons"":[""humidifier""],""qualifier"":""Sleep apnea treatment"",""ordering_provider"":""Dr. Brown""}",
                resultProcess.Value.JsonToSend);
        }
    }
}
