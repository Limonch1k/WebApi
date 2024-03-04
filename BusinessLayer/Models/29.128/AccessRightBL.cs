using BL.Models;


namespace BL.BLModels
{
    public class AccessRightBL
    {
        public int Id {get;set;}

        public string? Source {get;set;}

        public int UserId {get;set;}

        public UserBL? User {get;set;}
    }    
}
