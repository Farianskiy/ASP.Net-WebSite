using CSharpFunctionalExtensions;

namespace WebSiteElectronicMind.Core.Models
{
    public class Position
    {
        public const int MAX_LENGTH = 250;

        private Position(string code, string arcticul, string name, string type, Dictionary<string,string> characteristic)
        {
            Code = code;
            Articul = arcticul;
            Name = name;
            Type = type;
            Characteristic = characteristic;
        }

        public string Code { get;}
        public string Articul {  get; }
        public string Name { get; }
        public string Type { get; } = string.Empty;
        public Dictionary<string, string> Characteristic { get; } = new Dictionary<string, string>();

        public static Result<Position> Create(string code, string arcticul, string name, string type, Dictionary<string, string> characteristic)
        {
            if (string.IsNullOrEmpty(code) || code.Length > MAX_LENGTH)
            {
                return Result.Failure<Position>($"'{nameof(code)}' cannot be null or empty");
            }

            if (string.IsNullOrEmpty(arcticul) || arcticul.Length > MAX_LENGTH)
            {
                return Result.Failure<Position>($"'{nameof(arcticul)}' cannot be null or empty");
            }

            if (string.IsNullOrEmpty(name) || name.Length > MAX_LENGTH)
            {
                return Result.Failure<Position>($"'{nameof(name)}' cannot be null or empty");
            }

            var position = new Position(code, arcticul, name, type, characteristic);

            return Result.Success(position);
        }

        public static Result<Position> Update(Position existingPosition, string name, string type, Dictionary<string, string> characteristic)
        {
            if (existingPosition == null)
            {
                return Result.Failure<Position>("Existing position cannot be null");
            }

            var updatedPosition = new Position(
                existingPosition.Code,
                existingPosition.Articul,
                name,
                type,
                characteristic
            );

            return Result.Success(updatedPosition);
        }
    }
}
