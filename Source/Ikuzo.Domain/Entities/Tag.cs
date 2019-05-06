using System; 

namespace Ikuzo.Domain.Entities
{
    public static class TagNames
    {
        public const string None = null;
        public const string Fast = "Fast";
        public const string Stop = "Stop";
        public const string Touristic = "Touristic";
        public const string Dangerous = "Dangerous";
    }

    public class Tag
    {
        public Tag()
        {
            LastUpdateDate = DateTime.UtcNow; 
        }

        public int TagId { get; set; }
        public string TagName { get; set; }

        public string LineId { get; set; }
        public virtual Line Line { get; set; }

        public DateTime LastUpdateDate { get; set; }
    }
}
