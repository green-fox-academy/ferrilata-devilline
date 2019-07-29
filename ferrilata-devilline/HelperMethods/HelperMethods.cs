using System;
using ferrilata_devilline.Models;
using ferrilata_devilline.Models.DAOs;

namespace ferrilata_devilline.HelperMethods
{
    public class HelperMethods
    {
        public static bool checkMissingPostedPitchFields(Pitch NewPitch)
        {
            if (NewPitch.Status == null)
            {
                return true;
            }
            return false;
        }

        public static bool checkIAllFieldsArePresent(Pitch pitch)
        {
            if (pitch.Status == null)
            {
                return false;
            }
            return true;
        }
    }
}
