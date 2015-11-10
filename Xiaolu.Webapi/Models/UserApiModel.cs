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

        public string Mobile { set; get; }

        public string WeixinId { set; get; }

        public UserApiModel() { }

        public UserApiModel(User obj)
        {
            Id = obj.Id;
            Gender = obj.Gender ? "1" : "0";
            Name = obj.Name;
            Password = obj.Password;
            Description = obj.Description;
            Birthday = obj.Birthday.Value.ToString("yyyy-mm-dd");
            Level = obj.Level.ToString();
            Mobile = obj.Mobile;
            WeixinId = obj.WeixinId;
        }

        public string ValidateInputs()
        {
            string errors = "";
            if (string.IsNullOrWhiteSpace(Name))
            {
                errors += "用户名不能为空!";
            }

            try
            {
                int genderInt = Int32.Parse(Gender);
                if (genderInt != 1 && genderInt != 0)
                {
                    errors += "性别参数有误!只能为1或0";
                }
            }
            catch (Exception e)
            {
                errors += "性别参数有误!只能为1或0";
            }

            try
            {
                DateTime birthdayDT = DateTime.Parse(Birthday);
            }
            catch (Exception e)
            {
                errors += "生日输入有误，无法解析时间格式";
            }

            return errors;
        }

        public User ToDto()
        {
            string errors = ValidateInputs();
            if (!string.IsNullOrEmpty(errors))
            {
                throw new Exception(errors);
            }

            User newObj = new User()
            {
                Id = Id,
                Name = Name,
                Password = Password,
                Description = Description,
                Birthday = DateTime.Parse(Birthday),
                Level = Int32.Parse(Level),
                Gender = Gender.Equals("1"),
                Mobile = Mobile,
                WeixinId = WeixinId
            };
            return newObj;
        }

        public static List<UserApiModel> FromUserList(List<User> objList)
        {
            List<UserApiModel> returnList = new List<UserApiModel>();
            foreach (User item in objList)
            {
                UserApiModel newObj = new UserApiModel(item);
                returnList.Add(newObj);
            }
            return returnList;
        }
    }
}