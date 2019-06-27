namespace ferrilata_devilline.Models
{
    public class Holder
    {
        public string name { get; set; }
        public string message { get; set; }
        public bool pitchStatus { get; set; }

        public Holder(string name, string message, bool pitchStatus)
        {
            this.message = message;
            this.name = name;
            this.pitchStatus = pitchStatus;
        }
    }
}