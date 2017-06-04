using AspNetCore.Identity.MongoDB;
using AspNetCore.Identity.MongoDB.Models;

namespace ArabicTutorials.Data.Models
{
    public class User : MongoIdentityUser
    {
        public User(string userName, string email) : base(userName, email)
        {
        }

        public User(string userName, MongoUserEmail email) : base(userName, email)
        {
        }

        public User(string userName) : base(userName)
        {
        }
    }
}
