using System.Collections.Generic;

namespace ArabicTutorials.Data.Models
{
    public class Course: ModelBase
    {
        public Course()
        {
            Videos = new HashSet<Video>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Video> Videos { get; set; }
    }
}
