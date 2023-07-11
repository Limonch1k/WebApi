using DatabaseLayer.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.IRepository
{
    public interface IGidroMorpfRepository
    {
        public string[] GetPoolList();
        public string[] GetRiverList();
        public string[] GetResourceIdList();
        public List<GmfCategoryDB> GetCategoryList(bool indicators);
        public List<GmfProtocolDB> GetProtocols(int kn, int god);

        public bool InsertProtocols(List<GmfProtocolDB> models_db);

        public List<GmfIndicatorDB> GetIndicatorList(int kn, int god);

        public List<GmfIndicatorDB> GetIndicatorList();

        public List<GmfPunctDB> GetPunctList();

        public int SaveChanges();
    }
}
