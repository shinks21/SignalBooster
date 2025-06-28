using Application.ProcessSignalBoosterFile.Requests;
using Application.ProcessSignalBoosterFile.Responses;
using CSharpFunctionalExtensions;

namespace Application.ProcessSignalBoosterFile.Interfaces
{
    public interface ISignalBooster
    {
        Task<Result<SignalBoosterResponse>> Process(SignalBoosterRequest request);
    }
}