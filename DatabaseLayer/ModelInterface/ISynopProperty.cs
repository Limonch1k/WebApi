using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.ModelInterface
{
    public interface ISynopProperty
    {
        public int ResourceId { get; set; }

        public DateTime DateObservation { get; set; }

        public DateTime DateWrite { get; set; }
    }
}
