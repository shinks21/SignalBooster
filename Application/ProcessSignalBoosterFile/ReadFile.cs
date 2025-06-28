using Application.ProcessSignalBoosterFile.Interfaces;
using Application.ProcessSignalBoosterFile.Requests;
using Application.ProcessSignalBoosterFile.Responses;
using CSharpFunctionalExtensions;

namespace Application.ProcessSignalBoosterFile
{
    public class ReadFile : ISignalBooster
    {
        public async Task<Result<SignalBoosterResponse>> Process(SignalBoosterRequest request)
        {
            var resultReadFile = FileExists(request);

            if (resultReadFile.IsFailure)
            {
                return Result.Failure<SignalBoosterResponse>(resultReadFile.Error);
            }

            return await ExtractText(request);
        }

        private static Result FileExists(SignalBoosterRequest request)
        {
            if (!File.Exists(request.PhysicianFileName))
            {
                return Result.Failure($"File {Environment.CurrentDirectory}\\{request.PhysicianFileName} does not exist.");
            }

            return new Result();
        }

        private static async Task<Result<SignalBoosterResponse>> ExtractText(SignalBoosterRequest request)
        {
            var text = await File.ReadAllTextAsync(request.PhysicianFileName);

            if (string.IsNullOrWhiteSpace(text))
            {
                return Result.Failure<SignalBoosterResponse>($"Error reading file {Environment.CurrentDirectory}\\{request.PhysicianFileName}.");
            }

            return new SignalBoosterResponse(text);
        }
    }
}
