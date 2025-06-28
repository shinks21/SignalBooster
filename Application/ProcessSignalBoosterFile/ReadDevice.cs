using System.Text.RegularExpressions;
using Application.ProcessSignalBoosterFile.Interfaces;
using Application.ProcessSignalBoosterFile.Requests;
using Application.ProcessSignalBoosterFile.Responses;
using CSharpFunctionalExtensions;
using Domain.Enums;
using Domain.Extensions;

namespace Application.ProcessSignalBoosterFile
{
    public class ReadDevice(ISignalBooster last) : ISignalBooster
    {
        public async Task<Result<SignalBoosterResponse>> Process(SignalBoosterRequest request)
        {
            var resultLast = await last.Process(request);

            if (resultLast.IsFailure)
            {
                return resultLast;
            }

            return ParseDevice(resultLast.Value);
        }

        private static SignalBoosterResponse ParseDevice(SignalBoosterResponse response)
        {
            if (response.FileText.ContainsSignalValue("CPAP"))
            {
                response.Device = Device.CPAP;

                if (response.FileText.ContainsSignalValue("full face"))
                {
                    response.MaskType = MaskType.FullFace;
                }
            }
            else if (response.FileText.ContainsSignalValue("oxygen"))
            {
                response.Device = Device.Oxygen;

                ParseOxygenLiters(response);
                ParseSleepAndExertion(response);
            }
            else if (response.FileText.ContainsSignalValue("wheelchair"))
            {
                response.Device = Device.Wheelchair;
            }

            return response;
        }

        private static void ParseOxygenLiters(SignalBoosterResponse response)
        {
            Match regExMatch = Regex.Match(response.FileText, @"(\d+(\.\d+)?) ?L", RegexOptions.IgnoreCase);

            if (regExMatch.Success)
            {
                response.OxygenLiters = regExMatch.Groups[1].Value + " L";
            }
        }

        private static void ParseSleepAndExertion(SignalBoosterResponse response)
        {
            if (response.FileText.ContainsSignalValue("sleep")
                && response.FileText.ContainsSignalValue("exertion"))
            {
                response.OxygenUsage = OxygenUsage.SleepAndExertion;
            }
            else if (response.FileText.ContainsSignalValue("sleep"))
            {
                response.OxygenUsage = OxygenUsage.Sleep;
            }
            else if (response.FileText.ContainsSignalValue("exertion"))
            {
                response.OxygenUsage = OxygenUsage.Exertion;
            }
        }
    }
}
