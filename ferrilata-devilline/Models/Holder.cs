namespace ferrilata_devilline.Models
{
    public class Holder
    {
        public string Name { get; set; }
        public string Message { get; set; }
        public bool PitchStatus { get; set; }

        public Holder(string name, string message, bool pitchStatus)
        {
            Message = message;
            Name = name;
            PitchStatus = pitchStatus;
        }
    }
}