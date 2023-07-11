using DB.TableModels;


namespace DB.TableModels
{
    public class PageAccessRight
    {
        public int Id {get;set;}

        public string? Source {get;set;}

        public int UserId {get;set;}

        public User? User {get;set;}
    }    
}
