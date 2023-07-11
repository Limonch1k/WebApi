using System.ComponentModel.DataAnnotations;
using DB.TableModels;
using System.Collections.Generic;

namespace DB.TableModels
{
    public class User
    {
        [Key]
        public int UserId {get;set;}

        public string ApiKey {get;set;}

        public DateTime? SrokDogovora { get; set; }

        public List<PageAccessRight>? right_list {get;set;}

        public List<ParamAccessRight>? param_list {get;set;}

        public List<PunktAccessRight>? punkt_list {get;set;} 

        public List<NullDataTable>? errore_list {get;set;}
    }
}
