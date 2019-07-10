
namespace ferrilata_devilline.Models.DAOs
{
    public class UserLevel
    {
        public long UserId { get; set; }

        public User User { set; get; }

        public long LevelId { get; set; }

        public Level Level { set; get; }

        public UserLevel(User user, Level level)
        {
            User = user;
            Level = level;
            UserId = user.UserId;
            LevelId = Level.LevelId;
        }

        public UserLevel()
        {
        }
    }
}