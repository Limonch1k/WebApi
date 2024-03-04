using BusinessLayer.Models;
using DatabaseLayer.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.IServices
{
    public  interface IGidroMorpfServices
    {
        public string[] GetBassList();

        public bool DeleteBass(string[] nambass);

        public bool InsertBass(string[] nambass);

        public Task<bool> UpdateBass(string _new, string _old);

        public Task<List<GmfRegionBL>> GetRegionList();

        public Task<bool> UpdateRegion(string old_name, string new_name);

        public Task<bool> DeleteRegion(string treg);

        public bool UpdateRiverList(List<GmfRiverBL> list_bl_new, List<GmfRiverBL> list_bl_old);

        public Task<bool> DeleteRiverList(List<GmfRiverBL> list_bl);

        public string[] GetResourceIdList();

        public List<GmfRiverBL> GetRiverList();

        public bool InsertRiverList(List<GmfRiverBL> list_bl);

        public List<GmfCategoryBL> GetCategoryList(bool indicators);

        public List<GmfProtocolBL> GetProtocols(bool indicators);

        public List<GmfProtocolBL> GetProtocols(string God, string kn, bool indicator);

        public List<GmfProtocolBL> GetProtocols(string God, string kod, string bass, string river, string punkt,string pasp, string region, bool indicators);

        public bool InsertProtocols(List<GmfProtocolBL> models_bl);

        public bool DeleteProtocols(string Kn, string God);

        public bool UpdateProtocols(List<GmfProtocolBL> models);

        public List<GmfTotalBL> GetTotalList(int kn = 0, int god = 0, string region = null, string bass = null, string river = null, string punkt = null, string pasp = null);

        public List<GmfIndicatorBL> GetIndicatorList(string kn, string God);

        public List<GmfPunctBL> GetPunctList(string pasp, bool? trans);

        public bool InputPunctList(List<GmfPunctBL> models);

        public bool DeletePunctList(int[] models);

        public Task<bool> RecalcAllTotals();
    }
}
