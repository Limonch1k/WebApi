using DB.TableModels;
using Microsoft.EntityFrameworkCore;

namespace DB.TableModels
{
    public class NullDataTable
    {
        
        public int Id {get;set;}
        public DateTime Srok {get;set;}

        public int? PunktId {get;set;}     

        public string? param {get;set;}

        public int UserId {get;set;}

        public User? User {get;set;}

        public DateTime DateWrite {get;set;}
    }    
}