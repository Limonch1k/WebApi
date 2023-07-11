using DB.TableModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.DBModel
{
    public class UserDB
    {
        public int UserId { get; set; }

        public string ApiKey { get; set; }

        public DateTime? SrokDogovora { get; set; }
    }
}
