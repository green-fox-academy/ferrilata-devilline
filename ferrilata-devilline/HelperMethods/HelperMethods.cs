using System;
using ferrilata_devilline.Models;

namespace ferrilata_devilline.HelperMethods
{
    public class HelperMethods
    {
        public static bool checkMissingPostedPitchFields(Pitch NewPitch)
        {
            if (NewPitch.Username == null ||
                NewPitch.BadgeName == null ||
                NewPitch.Status == null ||
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
