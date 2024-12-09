using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteElectronicMind.Core.Models;
using WebSiteElectronicMind.ML.Repositories;

namespace WebSiteElectronicMind.Application.Services
{
    public class PositionService : IPositionService
    {
        private readonly IPositionRepositories _positionRepositories;

        public PositionService(IPositionRepositories positionRepositories)
        {
            _positionRepositories = positionRepositories;
        }

        public async Task<Result<Position>> UpdateTypeOfCharacteristic(Position data)
        {
            var position = await _positionRepositories.DetermineTypeAndFormatAsync(data);
            return Result.Success(position);
        }

    }
}
