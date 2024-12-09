using CSharpFunctionalExtensions;
using WebSiteElectronicMind.Core.Models;

namespace WebSiteElectronicMind.Application.Services
{
    public interface IPositionService
    {
        Task<Result<Position>> UpdateTypeOfCharacteristic(Position data);
    }
}