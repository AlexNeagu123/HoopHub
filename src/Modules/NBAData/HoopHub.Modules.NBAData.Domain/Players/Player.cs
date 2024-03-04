using HoopHub.BuildingBlocks.Domain;

namespace HoopHub.Modules.NBAData.Domain.Players
{
    public class Player
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        private Player(string firstName, string lastName)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
        }

        public static Result<Player> Create(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                return Result<Player>.Failure("First name is required");
            if (string.IsNullOrWhiteSpace(lastName))
                return Result<Player>.Failure("Last name is required");

            return Result<Player>.Success(new Player(firstName, lastName));
        }
    }
}
