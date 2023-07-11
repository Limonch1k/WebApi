using DB.TableModels;


namespace DB.TableModels
{
    public class PunktAccessRightBL
    {
        public int Id {get;set;}

        public int? Punkt_Id {get;set;}

        public int UserId {get;set;}

        public User? User {get;set;}
    }    
}