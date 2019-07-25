using System.Collections.Generic;
using ferrilata_devilline.Models.DAOs;

namespace ferrilata_devilline.Repositories
{
	public interface IBadgeRepository
	{
		void SaveOrUpdateBadge(Badge badge);
		List<Badge> RetrieveBadgesFromDB();
		Badge FindBadgeById(long id);
		void DeleteBadgeById(long id);
		Level FindLevelById(long levelId);
		List<Level> RetrieveLevelsFromDB();
		void SaveOrUpdateLevel(Level level);
	}
}