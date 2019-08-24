using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ferrilata_devilline.ViewModels
{
    public class BadgeLibraryViewModel
    {
        public long UserId { get; set; }
        public List<BadgeDTO> Badges { get; set; }
        public List<User> Users { get; set; }
    }
}
