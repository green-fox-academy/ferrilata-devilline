using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Repositories;
using ferrilata_devilline.Services.Interfaces;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ferrilata_devilline.Services
{
    public class BadgeService : IBadgeService
    {
        readonly ApplicationContext _context;

        public BadgeService(ApplicationContext context)
        {
            _context = context;
        }

        public List<Badge> GetAll()
        {
            return _context.Badges.ToList();
        }
    }
}