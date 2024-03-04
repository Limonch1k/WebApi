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
        public string[] GetBassList();

        public bool InsertBass(string nambass);

        public bool UpdateBass(string _new, string _old);

        public List<GmfRegionDB> GetRegionList();

        public bool UpdateRegion(string old_name, string new_name);

        public Task<bool> DeleteRegion(string treg);

        public bool DeleteRiverList(List<GmfRiverDB> list_old);

        public bool DeleteRiver(GmfRiverDB riverDB);

        public bool UpdateRiverList(List<GmfRiverDB> list_new, List<GmfRiverDB> list_old);
        public List<GmfRiverDB> GetRiverList();

        public List<GmfRiverDB> GetRiverList(string riverbass, string namriver);

        public bool InsertRiverList(List<GmfRiverDB> list_db);

        public bool DeleteBass(string nambass);
        public string[] GetResourceIdList();
        public List<GmfCategoryDB> GetCategoryList(bool indicators);

        public List<GmfProtocolDB> GetProtocols(bool indicators);
        public List<GmfProtocolDB> GetProtocols(uint kn, uint god, bool indicator);

        public List<GmfProtocolDB> GetProtocols(int god, int kod, string bass, string river, string punkt, string pasp,string region, bool indicators);

        public bool InsertProtocols(List<GmfProtocolDB> models_db);

        public bool UpdateProtocols(List<GmfProtocolDB> models_db);

        public bool DeleteProtocols(int kn, int god);

        public List<GmfIndicatorDB> GetIndicatorList(int kn, int god);

        public List<GmfIndicatorDB> GetIndicatorList();

        public List<GmfPunctDB> GetPunctList(string pasp, bool? trans, string bass = null, string river = null, string region = null);

        public List<GmfTotalDB> GetTotalList(int kn, int god);

        public int GetClass3(double Aestimation);

        public int GetClass5(double Bestimation);

        public List<string> GetGroupIndexsName(int groupNumber);

        public List<string> GetZonaIndexName(int zona_index);

        public bool InsertGmfTotal(GmfTotalDB totalDB);

        public bool InsertPunkt(List<GmfPunctDB> models_db);

        public bool DeletePunct(int kod);

        public int SaveChanges();
    }
}
