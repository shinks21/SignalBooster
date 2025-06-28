using Application.ProcessSignalBoosterFile.Interfaces;
using Application.ProcessSignalBoosterFile.Requests;
using Application.ProcessSignalBoosterFile.Responses;
using CSharpFunctionalExtensions;
using Domain.Enums;
using Domain.Extensions;

namespace Application.ProcessSignalBoosterFile
{
    public class ReadAddOns(ISignalBooster last) : ISignalBooster
    {
        public async Task<Result<SignalBoosterResponse>> Process(SignalBoosterRequest request)
        {
            var resultLast = await last.Process(request);

            if (resultLast.IsFailure)
            {
                return resultLast;
            }

            return ParseAddOns(resultLast.Value);
        }

        private static SignalBoosterResponse ParseAddOns(SignalBoosterResponse response)
        {
            if (response.FileText.ContainsSignalValue("humidifier"))
            {
                response.AddOn = AddOn.Humidifier;
            }

            return response;
        }
    }
}