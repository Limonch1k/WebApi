using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.IServices
{
    public  interface IGidroMorpfServices
    {
        public string[] GetPoolList();

        public string[] GetResourceIdList();

        public string[] GetRiverList();

        public List<GmfCategoryBL> GetCategoryList(bool indicators);

        public List<GmfProtocolBL> GetProtocols(string Kn, string God);

        public bool InsertProtocols(List<GmfProtocolBL> models_bl);

        public List<GmfIndicatorBL> GetIndicatorList(string kn, string God);

        public List<GmfPunctBL> GetPunctList();
    }
}
