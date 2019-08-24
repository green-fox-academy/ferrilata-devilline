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
                Name = "Feedback receiver",
                Tag = "badge1 tag"
            };
            var badge2 = new Badge
            {
                Version = 2,
                Name = "English speaker",
                Tag = "badge2 tag"
            };
            var badge3 = new Badge
            {
                Version = 3,
                Name = "Process improver",
                Tag = "badge3 tag"
            };

            context.Badges.AddRange(badge1, badge2, badge3);
            context.SaveChanges();
            return context.Badges.OrderBy(b => b.BadgeId).ToList();
        }

        private static List<Level> CreateLevels(ApplicationContext context, List<Badge> badges)
        {
            var level1 = new Level
            {
                LevelNumber = 1,
                Description = "Receive feedback",
                Weight = "level1 weight",
                Badge = badges[0]
            };
            var level11 = new Level
            {
                LevelNumber = 2,
                Description = "Receive feedback well",
                Weight = "level11 weight",
                Badge = badges[0]
            };
            var level12 = new Level
            {
                LevelNumber = 3,
                Description = "Receive feedback very well",
                Weight = "level12 weight",
                Badge = badges[0]
            };
            var level13 = new Level
            {
                LevelNumber = 4,
                Description = "Receive feedback exceptionally well",
                Weight = "level13 weight",
                Badge = badges[0]
            };
            var level2 = new Level
            {
                LevelNumber = 1,
                Description = "Speak English",
                Weight = "level2 weight",
                Badge = badges[1]
            };
            var level21 = new Level
            {
                LevelNumber = 2,
                Description = "Speak English well",
                Weight = "level21 weight",
                Badge = badges[1]
            };
            var level22 = new Level
            {
                LevelNumber = 3,
                Description = "Speak English very well",
                Weight = "level22 weight",
                Badge = badges[1]
            };
            var level23 = new Level
            {
                LevelNumber = 4,
                Description = "Speak English exceptionally well",
                Weight = "level22 weight",
                Badge = badges[1]
            };
            var level3 = new Level
            {
                LevelNumber = 1,
                Description = "Improve processes",
                Weight = "level3 weight",
                Badge = badges[2]
            };
            var level31 = new Level
            {
                LevelNumber = 2,
                Description = "Improve processes well",
                Weight = "level31 weight",
                Badge = badges[2]
            };
            var level32 = new Level
            {
                LevelNumber = 3,
                Description = "Improve processes very well",
                Weight = "level32 weight",
                Badge = badges[2]
            };
            var level33 = new Level
            {
                LevelNumber = 4,
                Description = "Improve processes exceptionally well",
                Weight = "level33 weight",
                Badge = badges[2]
            };

            context.Levels.AddRange(level1, level2, level11, level12, level13, level21, level22, level23, level3, level31, level32, level33);
            context.SaveChanges();
            return context.Levels.ToList();
        }

        private static List<Badge> UpdateBadgesWithLevels(ApplicationContext context, List<Badge> badges, List<Level> levels)
        {
            badges[0].Levels = new List<Level> { levels[0], levels[2], levels[3], levels[4] };
            badges[1].Levels = new List<Level> { levels[1], levels[5], levels[6], levels[7] };
            badges[2].Levels = new List<Level> { levels[8], levels[9], levels[10], levels[11] };

            context.Badges.UpdateRange(badges);
            context.SaveChanges();
            return badges;
        }

        private static List<User> CreateUsers(ApplicationContext context)
        {
            var user1 = new User
            {
                Name = "Iaroslav",
                Email = "iaroslav.miller@gmail.com",
                Role = "user1 role"
            };
            var user2 = new User
            {
                Name = "Anna",
                Email = "anna.sabransky@gmail.com",
                Role = "user2 role"
            };
            var user3 = new User
            {
                Name = "Ks",
                Email = "kskulikovaa@gmail.com",
                Role = "user3 role"
            };

            context.Users.AddRange(user1, user2, user3);
            context.SaveChanges();
            return context.Users.ToList();
        }

        private static List<UserLevel> CreateUserLevels(ApplicationContext context, List<User> users, List<Level> levels)
        {
            var userLevel1 = new UserLevel(users[0], levels[0]);
            var userLevel11 = new UserLevel(users[0], levels[2]);
            var userLevel12 = new UserLevel(users[0], levels[3]);
            var userLevel13 = new UserLevel(users[0], levels[4]);
            var userLevel2 = new UserLevel(users[1], levels[1]);
            var userLevel21 = new UserLevel(users[1], levels[5]);
            var userLevel22 = new UserLevel(users[1], levels[6]);
            var userLevel23 = new UserLevel(users[1], levels[7]);
            var userLevel3 = new UserLevel(users[2], levels[8]);
            var userLevel31 = new UserLevel(users[2], levels[9]);
            var userLevel32 = new UserLevel(users[2], levels[10]);
            var userLevel33 = new UserLevel(users[2], levels[11]);

            context.UserLevels.AddRange(userLevel1, userLevel2, userLevel11, userLevel12, userLevel13, userLevel21, userLevel22, userLevel23, userLevel3, userLevel31, userLevel32, userLevel33);
            context.SaveChanges();
            return context.UserLevels.ToList();
        }

        private static List<Level> UpdateLevelsWithUserLevels(ApplicationContext context, List<Level> levels, List<UserLevel> userLevels)
        {
            levels[0].UserLevels = new List<UserLevel> { userLevels[0] };
            levels[2].UserLevels = new List<UserLevel> { userLevels[2] };
            levels[3].UserLevels = new List<UserLevel> { userLevels[3] };
            levels[4].UserLevels = new List<UserLevel> { userLevels[4] };
            levels[1].UserLevels = new List<UserLevel> { userLevels[1] };
            levels[5].UserLevels = new List<UserLevel> { userLevels[5] };
            levels[6].UserLevels = new List<UserLevel> { userLevels[6] };
            levels[7].UserLevels = new List<UserLevel> { userLevels[7] };
            levels[8].UserLevels = new List<UserLevel> { userLevels[8] };
            levels[9].UserLevels = new List<UserLevel> { userLevels[9] };
            levels[10].UserLevels = new List<UserLevel> { userLevels[10] };
            levels[11].UserLevels = new List<UserLevel> { userLevels[11] };

            context.Levels.UpdateRange(levels);
            context.SaveChanges();
            return levels;
        }

        private static List<User> UpdateUsersWithUserLevels(ApplicationContext context, List<User> users, List<UserLevel> userLevels)
        {
            users[0].UserLevels = new List<UserLevel> { userLevels[0], userLevels[2], userLevels[3], userLevels[4] };
            users[1].UserLevels = new List<UserLevel> { userLevels[1], userLevels[5], userLevels[6], userLevels[7] };
            users[2].UserLevels = new List<UserLevel> { userLevels[8], userLevels[9], userLevels[10], userLevels[11] };
            User user = users[1];

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
                Level = levels[0]
            };
            var pitch2 = new Pitch
            {
                Status = "open",
                PitchedLevel = "pitch2 pitchedLevel",
                PitchedMessage = "pitch2 pitchedMessage",
                Result = "pitch2 result",
                User = users[1],
                Level = levels[1]
            };
            var pitch11 = new Pitch
            {
                Status = "open",
                PitchedLevel = "pitch11 pitchedLevel",
                PitchedMessage = "pitch11 pitchedMessage",
                Result = "pitch11 result",
                User = users[0],
                Level = levels[2]
            };
            var pitch12 = new Pitch
            {
                Status = "open",
                PitchedLevel = "pitch12 pitchedLevel",
                PitchedMessage = "pitch12 pitchedMessage",
                Result = "pitch12 result",
                User = users[0],
                Level = levels[3]
            };
            var pitch13 = new Pitch
            {
                Status = "open",
                PitchedLevel = "pitch12 pitchedLevel",
                PitchedMessage = "pitch12 pitchedMessage",
                Result = "pitch12 result",
                User = users[0],
                Level = levels[4]
            };

            context.Pitches.AddRange(pitch1, pitch2, pitch11, pitch12, pitch13);
            context.SaveChanges();
            return context.Pitches.OrderBy(b => b.PitchId).ToList();
        }

        private static List<User> UpdateUsersWithPitches(ApplicationContext context, List<User> users, List<Pitch> pitches)
        {
            users[0].Pitches = new List<Pitch> { pitches[0], pitches[2], pitches[3], pitches[4] };
            users[1].Pitches = new List<Pitch> { pitches[1] };

            context.Users.UpdateRange(users);
            context.SaveChanges();
            return users;
        }

        private static void UpdateLevelsWithPitches(ApplicationContext context, List<Level> levels, List<Pitch> pitches)
        {
            levels[0].Pitches = new List<Pitch> { pitches[1] };
            levels[1].Pitches = new List<Pitch> { pitches[0] };
            levels[2].Pitches = new List<Pitch> { pitches[2] };
            levels[3].Pitches = new List<Pitch> { pitches[3] };
            levels[4].Pitches = new List<Pitch> { pitches[4] };

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
            var review11 = new Review
            {
                Message = "review11 message",
                Result = "review11 result",
                User = users[0],
                Pitch = pitches[2]
            };
            var review12 = new Review
            {
                Message = "review12 message",
                Result = "review12 result",
                User = users[0],
                Pitch = pitches[3]
            };
            var review13 = new Review
            {
                Message = "review12 message",
                Result = "review12 result",
                User = users[0],
                Pitch = pitches[4]
            };

            context.Reviews.AddRange(review1, review2, review11, review12, review13);
            context.SaveChanges();
            return context.Reviews.OrderBy(b => b.ReviewId).ToList();
        }

        private static void UpdateUsersWithReviews(ApplicationContext context, List<User> users, List<Review> reviews)
        {
            users[0].Reviews = new List<Review> { reviews[0], reviews[2], reviews[3], reviews[4] };
            users[1].Reviews = new List<Review> { reviews[1] };

            context.Users.UpdateRange(users);
            context.SaveChanges();
        }

        private static void UpdatePitchesWithReviews(ApplicationContext context, List<Pitch> pitches, List<Review> reviews)
        {
            pitches[0].Reviews = new List<Review> { reviews[0] };
            pitches[1].Reviews = new List<Review> { reviews[1] };
            pitches[2].Reviews = new List<Review> { reviews[2] };
            pitches[3].Reviews = new List<Review> { reviews[3] };
            pitches[4].Reviews = new List<Review> { reviews[4] };

            context.Pitches.UpdateRange(pitches);
            context.SaveChanges();
        }
    }
}