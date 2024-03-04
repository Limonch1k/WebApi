using DB.TableModels;


namespace DB.TableModels
{
    public class PunktAccessRightDB
    {
        public int Id {get;set;}

        public int? Punkt_Id {get;set;}

        public int UserId {get;set;}

        public User? User {get;set;}
    }    
}