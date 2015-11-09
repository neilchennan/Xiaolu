using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xiaolu.Models.DAL;

namespace Xiaolu.Webapi.Models
{
    public class UserApiModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }

        public string Gender { set; get; }

        public string Birthday { set; get; }

        public string Level { set; get; }

        public UserApiModel() { }

        public UserApiModel(User obj)
        {
            Id = obj.Id;
            Gender = obj.Gender ? "1" : "0";
            Name = obj.Name;
            Password = obj.Password;
            Description = obj.Description;
            Birthday = obj.Birthday.Value.ToString("yyyy-mm-dd");
            Level = Level.ToString();
        }

        public User ToDto()
        {
            User newObj = new User()
            {
                Id = Id,
                Name = Name,
                Password = Password,
                Description = Description,
                Birthday = DateTime.Parse(Birthday),
                Level = Int32.Parse(Level),
                Gender = Gender.Equals("1")
            };
            return newObj;
        }
    }
}