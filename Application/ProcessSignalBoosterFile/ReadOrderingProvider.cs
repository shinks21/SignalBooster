using Application.ProcessSignalBoosterFile.Interfaces;
using Application.ProcessSignalBoosterFile.Requests;
using Application.ProcessSignalBoosterFile.Responses;
using CSharpFunctionalExtensions;

namespace Application.ProcessSignalBoosterFile
{
    public class ReadOrderingProvider(ISignalBooster last) : ISignalBooster
    {
        public async Task<Result<SignalBoosterResponse>> Process(SignalBoosterRequest request)
        {
            var resultLast = await last.Process(request);

            if (resultLast.IsFailure)
            {
                return resultLast;
            }

            return ParseOrderingProvider(resultLast.Value);
        }

        private static SignalBoosterResponse ParseOrderingProvider(SignalBoosterResponse response)
        {
            var index = response.FileText.IndexOf("Dr.");

            if (index >= 0)
            {
                response.OrderingProvider =
                    response.FileText[index..]
                    .Replace("Ordered by ", "").Trim('.');
            }

            return response;
        }
    }
}
