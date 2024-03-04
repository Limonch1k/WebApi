using DB.TableModels;


namespace DB.TableModels
{
    public class ParamAccessRight
    {
        public int Id {get;set;}

        public string? Param_Name {get;set;}

        public int UserId {get;set;}

        public string? TypeSource { get; set; }

        public User? User {get;set;}
    }    
}
