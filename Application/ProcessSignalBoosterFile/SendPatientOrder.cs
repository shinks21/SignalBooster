using System.Text;
using Application.ProcessSignalBoosterFile.Interfaces;
using Application.ProcessSignalBoosterFile.Requests;
using Application.ProcessSignalBoosterFile.Responses;
using CSharpFunctionalExtensions;

namespace Application.ProcessSignalBoosterFile
{
    public class SendPatientOrder(ISignalBooster last, IHttpClientFactory httpClientFactory) : ISignalBooster
    {
        public async Task<Result<SignalBoosterResponse>> Process(SignalBoosterRequest request)
        {
            var resultLast = await last.Process(request);

            if (resultLast.IsFailure)
            {
                return resultLast;
            }

            return await Send(resultLast.Value);
        }

        private async Task<SignalBoosterResponse> Send(SignalBoosterResponse response)
        {
            var stringContent = new StringContent(response.JsonToSend, Encoding.UTF8, "application/json");

            var httpClient = httpClientFactory.CreateClient("SignalBooster");

            HttpResponseMessage? httpResponse = await httpClient.PostAsync("DrExtract", stringContent);

            httpResponse.EnsureSuccessStatusCode();

            return response;
        }
    }
}
