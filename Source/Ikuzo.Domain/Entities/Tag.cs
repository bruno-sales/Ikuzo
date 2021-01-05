
namespace Ikuzo.Domain.Entities
{
    public enum TagTypes
    {
        None = 99,
        Fast = 1,
        Stop = 2,
        Touristic = 3,
        Dangerous = 4,
        Downtown = 5
    }

    public class Tag
    { 
        public int TagId { get; set; }
        public string TagName { get; set; }
    }
}
