using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Repositories;
using System.Collections.Generic;

namespace ferrilata_devilline.Services.Extensions
{
    public static class DatabaseSeeder
    {
        public static void SeedWithData(this ApplicationContext context)
        {
            List<Badge> badges = CreateBadges();
            List<Level> levels = CreateLevels(badges);
            List<User> users = CreateUsers();
            List<UserLevel> userLevels = CreateUserLevels(users, levels);

            levels = UpdateLevelsWithUserLevels(levels, userLevels);
            users = UpdateUsersWithUserLevels(users, userLevels);

            List<Pitch> pitches = CreatePitches(users, levels);
            List<Review> reviews = CreateReviews(users, pitches);

            context.Badges.AddRange(badges);
            context.Levels.AddRange(levels);
            context.Users.AddRange(users);
            context.UserLevels.AddRange(userLevels);
            context.Pitches.AddRange(pitches);
            context.Reviews.AddRange(reviews);

            context.SaveChanges();
        }

        private static List<Badge> CreateBadges()
        {
            Badge badge1 = new Badge
            {
                Version = 1,
                Name = "badge1 name",
                Tag = "badge1 tag"
            };
            Badge badge2 = new Badge
            {
                Version = 2,
                Name = "badge2 name",
                Tag = "badge2 tag"
            };

            return new List<Badge> { badge1, badge2 };
        }

        private static List<Level> CreateLevels(List<Badge> badges)
        {
            Level level1 = new Level
            {
                Description = "level1 description",
                Weight = "level1 weight",
                Badge = badges[0]
            };
            Level level2 = new Level
            {
                Description = "level2 description",
                Weight = "level2 weight",
                Badge = badges[1]
            };

            return new List<Level> { level1, level2 };
        }

        private static List<User> CreateUsers()
        {
            User user1 = new User
            {
                Name = "user1 name",
                Email = "user1 email",
                Role = "user1 role"
            };
            User user2 = new User
            {
                Name = "user2 name",
                Email = "user2 email",
                Role = "user2 role"
            };

            return new List<User> { user1, user2 };
        }

        private static List<UserLevel> CreateUserLevels(List<User> users, List<Level> levels)
        {
            UserLevel userLevel1 = new UserLevel(users[0], levels[0]);
            UserLevel userLevel2 = new UserLevel(users[1], levels[1]);

            return new List<UserLevel> { userLevel1, userLevel2 };
        }

        private static List<Level> UpdateLevelsWithUserLevels(List<Level> levels, List<UserLevel> userLevels)
        {
            levels[0].UserLevels = new List<UserLevel> { userLevels[0] };
            levels[1].UserLevels = new List<UserLevel> { userLevels[1] };

            return levels;
        }

        private static List<User> UpdateUsersWithUserLevels(List<User> users, List<UserLevel> userLevels)
        {
            users[0].UserLevels = new List<UserLevel> { userLevels[0] };
            users[1].UserLevels = new List<UserLevel> { userLevels[1] };

            return users;
        }

        private static List<Pitch> CreatePitches(List<User> users, List<Level> levels)
        {
            Pitch pitch1 = new Pitch
            {
                PitchedLevel = "pitch1 pitchedLevel",
                PitchedMessage = "pitch1 pitchedMessage",
                Result = "pitch1 result",
                User = users[0],
                Level = levels[1]
            };
            Pitch pitch2 = new Pitch
            {
                PitchedLevel = "pitch2 pitchedLevel",
                PitchedMessage = "pitch2 pitchedMessage",
                Result = "pitch2 result",
                User = users[1],
                Level = levels[0]
            };

            return new List<Pitch> { pitch1, pitch2 };
        }

        private static List<Review> CreateReviews(List<User> users, List<Pitch> pitches)
        {
            Review review1 = new Review
            {
                Message = "review1 message",
                Result = "review1 result",
                User = users[0],
                Pitch = pitches[1]
            };
            Review review2 = new Review
            {
                Message = "review2 message",
                Result = "review2 result",
                User = users[1],
                Pitch = pitches[0]
            };
            return new List<Review> { review1, review2 };
        }
    }
}