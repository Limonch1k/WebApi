using BL.BLModels;

namespace BL.Models
{
    public class UserBL
    {
        public string? Username {get;set;}

        public string? Password {get;set;}

        public List<AccessRightBL> right {get;set;}
    }
}