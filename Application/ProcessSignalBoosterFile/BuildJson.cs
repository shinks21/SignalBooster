using System.Text.Json;
using Application.ProcessSignalBoosterFile.Interfaces;
using Application.ProcessSignalBoosterFile.Models;
using Application.ProcessSignalBoosterFile.Requests;
using Application.ProcessSignalBoosterFile.Responses;
using CSharpFunctionalExtensions;
using Domain.Enums;
using Domain.Extensions;

namespace Application.ProcessSignalBoosterFile
{
    public class BuildJson(ISignalBooster last) : ISignalBooster
    {
        public async Task<Result<SignalBoosterResponse>> Process(SignalBoosterRequest request)
        {
            var resultLast = await last.Process(request);

            if (resultLast.IsFailure)
            {
                return resultLast;
            }

            return Build(resultLast.Value);
        }

        private static SignalBoosterResponse Build(SignalBoosterResponse response)
        {
            var patientOrderJson = new PatientOrderJson
            {
                Device = response.Device.GetDescription(),
                MaskType = response.MaskType?.GetDescription(),
                AddOns = response.AddOn != null
                    ? new List<string> {  response.AddOn.GetDescription()  }
                    : null,
                Qualifier = response.Qualifier,
                OrderingProvider = response.OrderingProvider
            };

            if (response.Device == Device.Oxygen)
            {
                patientOrderJson.Liters = response.OxygenLiters;
                patientOrderJson.Usage = response.OxygenUsage?.GetDescription() ?? string.Empty;
            }

            response.JsonToSend = JsonSerializer.Serialize(patientOrderJson);

            return response;
        }
    }
}
