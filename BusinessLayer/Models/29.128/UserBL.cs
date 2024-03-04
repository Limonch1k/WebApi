using BL.BLModels;
using DB.TableModels;

namespace BL.Models
{
    public class UserBL
    {
        public string? Username {get;set;}

        public string? Password {get;set;}

        public List<PageAccessRight> right {get;set;}
    }
}