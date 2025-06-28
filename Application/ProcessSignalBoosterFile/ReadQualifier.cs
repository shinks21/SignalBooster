using Application.ProcessSignalBoosterFile.Interfaces;
using Application.ProcessSignalBoosterFile.Requests;
using Application.ProcessSignalBoosterFile.Responses;
using CSharpFunctionalExtensions;
using Domain.Extensions;

namespace Application.ProcessSignalBoosterFile
{
    public class ReadQualifier(ISignalBooster last) : ISignalBooster
    {
        public async Task<Result<SignalBoosterResponse>> Process(SignalBoosterRequest request)
        {
            var resultLast = await last.Process(request);

            if (resultLast.IsFailure)
            {
                return resultLast;
            }

            return ParseQualifier(resultLast.Value);
        }

        private static SignalBoosterResponse ParseQualifier(SignalBoosterResponse response)
        {
            var qualifierExists = response.FileText.ContainsSignalValue("AHI > 20");

            if (qualifierExists)
            {
                response.Qualifier = "AHI > 20";
            }

            return response;
        }
    }
}
