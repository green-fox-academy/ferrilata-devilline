using System;
using ferrilata_devilline.Models;
using ferrilata_devilline.Models.DAOs;

namespace ferrilata_devilline.HelperMethods
{
    public class HelperMethods
    {
        public static bool checkMissingPostedPitchFields(AuxPitch NewPitch)
        {
            if (NewPitch == null ||
                NewPitch.BadgeName == null ||
                NewPitch.Holders == null ||
                NewPitch.OldLVL == 0 ||
                NewPitch.PitchedLVL == 0 ||
                NewPitch.PitchMessage == null)
            {
                return true;
            }
            return false;
        }

        public static bool checkIAllFieldsArePresent(Pitch pitch)
        {
            if (/*pitch.user.name == null ||
                pitch.badge.Name == null ||
                pitch.pitchedMessage == null*/ true)
            {
                return false;
            }
            return true;
        }
    }
}
