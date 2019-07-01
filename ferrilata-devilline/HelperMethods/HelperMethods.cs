using System;
using ferrilata_devilline.Models;

namespace ferrilata_devilline.HelperMethods
{
    public class HelperMethods
    {
        public static bool checkMissingPostedPitchFields(Pitch NewPitch)
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
    }
}
