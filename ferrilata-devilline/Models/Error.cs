namespace ferrilata_devilline.Models
{
    public class Error
    {
        public string RequestId { get; set; }
        public string error { get; set; }

        public Error(string error)
        {
            this.error = error;
        }

        public Error()
        {
        }

        protected bool Equals(Error other)
        {
            return string.Equals(error, other.error);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Error)obj);
        }

        public override int GetHashCode()
        {
            return (error != null ? error.GetHashCode() : 0);
        }

        public static bool operator ==(Error left, Error right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Error left, Error right)
        {
            return !Equals(left, right);
        }
    }
}