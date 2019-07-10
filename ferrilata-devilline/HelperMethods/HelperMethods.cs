using System;
using ferrilata_devilline.Models;

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
            if (pitch.Username == null ||
                pitch.BadgeName == null ||
                pitch.Status == null ||
                pitch.PitchMessage == null)
            {
                return false;
            }
            return true;
        }
    }
}
