using System.Collections.Generic;

namespace ArabicTutorials.Data.Models
{
    public class Video : ModelBase
    {
        public Video()
        {
            Courses = new HashSet<Course>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public ICollection<Course> Courses { get; set; }
    }
}
