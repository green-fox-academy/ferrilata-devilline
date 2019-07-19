using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace ferrilata_devilline.Services.Helpers
{
    public static class DatabaseSeeder
    {
        public static void SeedWithData(this ApplicationContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var badges = CreateBadges(context);

            var levels = CreateLevels(context, badges);
            UpdateBadgesWithLevels(context, badges, levels);

            var users = CreateUsers(context);

            var userLevels = CreateUserLevels(context, users, levels);
            levels = UpdateLevelsWithUserLevels(context, levels, userLevels);
            users = UpdateUsersWithUserLevels(context, users, userLevels);

            var pitches = CreatePitches(context, users, levels);
            users = UpdateUsersWithPitches(context, users, pitches);
            UpdateLevelsWithPitches(context, levels, pitches);

            var reviews = CreateReviews(context, users, pitches);
            UpdateUsersWithReviews(context, users, reviews);
            UpdatePitchesWithReviews(context, pitches, reviews);
        }

        private static List<Badge> CreateBadges(ApplicationContext context)
        {
            var badge1 = new Badge
            {
                Version = 1,
                Name = "badge1 name",
                Tag = "badge1 tag"
            };
            var badge2 = new Badge
            {
                Version = 2,
                Name = "badge2 name",
                Tag = "badge2 tag"
            };

            context.Badges.AddRange(badge1, badge2);
            context.SaveChanges();
            return context.Badges.OrderBy(b => b.BadgeId).ToList();
        }

        private static List<Level> CreateLevels(ApplicationContext context, List<Badge> badges)
        {
            var level1 = new Level
            {
                LevelNumber = 1,
                Description = "level1 description",
                Weight = "level1 weight",
                Badge = badges[0]
            };
            var level2 = new Level
            {
                LevelNumber = 2,
                Description = "level2 description",
                Weight = "level2 weight",
                Badge = badges[1]
            };

            context.Levels.AddRange(level1, level2);
            context.SaveChanges();
            return context.Levels.OrderBy(b => b.LevelId).ToList();
        }

        private static List<Badge> UpdateBadgesWithLevels(ApplicationContext context, List<Badge> badges, List<Level> levels)
        {
            badges[0].Levels = new List<Level> { levels[0] };
            badges[1].Levels = new List<Level> { levels[1] };

            context.Badges.UpdateRange(badges);
            context.SaveChanges();
            return badges;
        }

        private static List<User> CreateUsers(ApplicationContext context)
        {
            var user1 = new User
            {
                Name = "user1 name",
                Email = "user1 email",
                Role = "user1 role"
            };
            var user2 = new User
            {
                Name = "user2 name",
                Email = "user2 email",
                Role = "user2 role"
            };

            context.Users.AddRange(user1, user2);
            context.SaveChanges();
            return context.Users.OrderBy(b => b.UserId).ToList();
        }

        private static List<UserLevel> CreateUserLevels(ApplicationContext context, List<User> users, List<Level> levels)
        {
            var userLevel1 = new UserLevel(users[0], levels[0]);
            var userLevel2 = new UserLevel(users[1], levels[1]);

            context.UserLevels.AddRange(userLevel1, userLevel2);
            context.SaveChanges();
            return context.UserLevels.OrderBy(b => b.UserId).ToList();
        }

        private static List<Level> UpdateLevelsWithUserLevels(ApplicationContext context, List<Level> levels, List<UserLevel> userLevels)
        {
            levels[0].UserLevels = new List<UserLevel> { userLevels[0] };
            levels[1].UserLevels = new List<UserLevel> { userLevels[1] };

            context.Levels.UpdateRange(levels);
            context.SaveChanges();
            return levels;
        }

        private static List<User> UpdateUsersWithUserLevels(ApplicationContext context, List<User> users, List<UserLevel> userLevels)
        {
            users[0].UserLevels = new List<UserLevel> { userLevels[0] };
            users[1].UserLevels = new List<UserLevel> { userLevels[1] };

            context.Users.UpdateRange(users);
            context.SaveChanges();
            return users;
        }

        private static List<Pitch> CreatePitches(ApplicationContext context, List<User> users, List<Level> levels)
        {
            var pitch1 = new Pitch
            {
                Status = "open",
                PitchedLevel = "pitch1 pitchedLevel",
                PitchedMessage = "pitch1 pitchedMessage",
                Result = "pitch1 result",
                User = users[0],
                Level = levels[1]
            };
            var pitch2 = new Pitch
            {
                Status = "open",
                PitchedLevel = "pitch2 pitchedLevel",
                PitchedMessage = "pitch2 pitchedMessage",
                Result = "pitch2 result",
                User = users[1],
                Level = levels[0]
            };

            context.Pitches.AddRange(pitch1, pitch2);
            context.SaveChanges();
            return context.Pitches.OrderBy(b => b.PitchId).ToList();
        }

        private static List<User> UpdateUsersWithPitches(ApplicationContext context, List<User> users, List<Pitch> pitches)
        {
            users[0].Pitches = new List<Pitch> { pitches[0] };
            users[1].Pitches = new List<Pitch> { pitches[1] };

            context.Users.UpdateRange(users);
            context.SaveChanges();
            return users;
        }

        private static void UpdateLevelsWithPitches(ApplicationContext context, List<Level> levels, List<Pitch> pitches)
        {
            levels[0].Pitches = new List<Pitch> { pitches[1] };
            levels[1].Pitches = new List<Pitch> { pitches[0] };

            context.Levels.UpdateRange(levels);
            context.SaveChanges();
        }

        private static List<Review> CreateReviews(ApplicationContext context, List<User> users, List<Pitch> pitches)
        {
            var review1 = new Review
            {
                Message = "review1 message",
                Result = "review1 result",
                User = users[0],
                Pitch = pitches[0]
            };
            var review2 = new Review
            {
                Message = "review2 message",
                Result = "review2 result",
                User = users[1],
                Pitch = pitches[1]
            };

            context.Reviews.AddRange(review1, review2);
            context.SaveChanges();
            return context.Reviews.OrderBy(b => b.ReviewId).ToList();
        }

        private static void UpdateUsersWithReviews(ApplicationContext context, List<User> users, List<Review> reviews)
        {
            users[0].Reviews = new List<Review> { reviews[0] };
            users[1].Reviews = new List<Review> { reviews[1] };

            context.Users.UpdateRange(users);
            context.SaveChanges();
        }

        private static void UpdatePitchesWithReviews(ApplicationContext context, List<Pitch> pitches, List<Review> reviews)
        {
            pitches[0].Reviews = new List<Review> { reviews[0] };
            pitches[1].Reviews = new List<Review> { reviews[1] };

            context.Pitches.UpdateRange(pitches);
            context.SaveChanges();
        }
    }
}