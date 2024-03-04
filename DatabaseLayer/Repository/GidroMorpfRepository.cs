using AutoMapper;
using DatabaseLayer.Context;
using DatabaseLayer.DBModel;
using DatabaseLayer.IRepository;
using DatabaseLayer.TableModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Repository
{
    public class GidroMorpfRepository : IGidroMorpfRepository
    {
        private GidroMorpfContext _context { get; set; }

        private IMapper _mapper { get; set; }

        public GidroMorpfRepository(GidroMorpfContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        public string[] GetBassList() 
        {
            var list = _context.GmfBasss.Select(g => g.Nambass).Where(x => !x.Equals("default_value")).Distinct().ToArray();
            return list;
        }

        public bool UpdateBass(string _new, string _old)
        {
            var bass = _context.GmfBasss.Where(x => x.Nambass.Equals(_old)).FirstOrDefault();
            if (bass is null)
            {
                return false;
            }

            /*var query = @$"UPDATE gxav.gmf_bass " + 
                   $@"SET ""Nambass"" = '{_new}' " +
                   $@"WHERE ""Nambass"" = '{_old}'";

            var fs = FormattableStringFactory.Create(query);

            _context.Database.ExecuteSqlRaw(query);*/

            try
            {
                _context.GmfBasss.Where(x => x.Nambass.Equals(_old)).ExecuteUpdate(x => x.SetProperty(b => b.Nambass, b => _new));
            }
            catch(Exception e)
            {

            }


            //bass.Nambass = _new;

            return true;
        }

        public List<GmfRegionDB> GetRegionList()
        {
            var list = _context.GmfRegions.ToList();

            var list_db = _mapper.Map<List<GmfRegionDB>>(list);

            return list_db;
        }

        public bool UpdateRegion(string old_name, string new_name)
        {
            if (old_name.Equals("неопределенно"))
            {
                return false;
            }

            var region = _context.GmfRegions.Where(x => x.tregion.Equals(old_name)).FirstOrDefault();

            var b = true;

            try
            {
                if(region is null && old_name.Equals(new_name))
                {
                    _context.GmfRegions.Add(new GmfRegion() { tregion = new_name});
                }
                else if (region is not null)
                {
                    /*var query = @$"UPDATE gxav.gmf_region " + 
                                $@"SET ""tregion"" = '{new_name}' " +
                                $@" where ""tregion"" = '{old_name}'";

                    var fs = FormattableStringFactory.Create(query);*/

                    //int i = _context.Database.ExecuteSqlRaw(query);

                    _context.GmfRegions.Where(x => x.tregion.Equals(old_name))
                    .ExecuteUpdate(x => x.SetProperty(r => r.tregion, r => $@"{new_name}"));
                }   
            }
            catch
            {
                b = false;
            }

            return b;
        }

        public async Task<bool> DeleteRegion(string treg)
        {
            bool b = true;

            if (treg.Equals("неопределенно"))
            {
                return false;
            }

            //var region = _context.GmfRegions.Where(x => x.tregion.Equals(treg)).FirstOrDefault();

            int rows_count = _context.GmfRegions.Where(x => x.tregion.Equals(treg)).ExecuteDelete();

            /*if (region is null)
            {
                return false;
            }*/

            //_context.GmfRegions.Remove(region);
            return true;
        }

        public bool UpdateRiverList(List<GmfRiverDB> list_new, List<GmfRiverDB> list_old)
        {
            bool b = true;
            int count = 0;
            try
            {
                foreach(var o in list_old)
                {
                    var new_river = list_new[count];
                    count++;
                    var river = _context.GmfRivers.Where(x => x.namriver.Equals(o.namriver) && x.riverbass.Equals(o.riverbass)).FirstOrDefault();

                    if (river is null && o.namriver.Equals(new_river.namriver) && o.riverbass.Equals(new_river.riverbass))
                    {
                        _context.GmfRivers.Add(new GmfRiver() { namriver = new_river.namriver, riverbass = new_river.riverbass});
                    }
                    else if (river is not null)
                    {
                        /*var query = @$"UPDATE gxav.gmf_river " + 
                            $@"SET ""riverbass"" = '{new_river.riverbass}', " +
                            $@"""namriver"" = '{new_river.namriver}'" +
                            $@" where ""riverbass"" = '{o.riverbass}' 
                            and ""namriver"" = '{o.namriver}'";

                        var fs = FormattableStringFactory.Create(query);

                        _context.Database.ExecuteSqlRaw(query);*/

                        _context.GmfRivers.Where(x => x.riverbass.Equals(o.riverbass) && x.namriver.Equals(o.namriver))
                        .ExecuteUpdate(x => x.SetProperty(r => r.riverbass, r => new_river.riverbass).SetProperty(r => r.namriver, r => new_river.namriver));
                    }
                }   
            }
            catch
            {
                b = false;
            }
            finally
            {

            }
            return b;      
        }

        public bool DeleteRiverList(List<GmfRiverDB> list_old)
        {
            int count = 0;

            var namriver_list = list_old.Select(x => x.namriver).ToArray();
            var riverbass_list = list_old.Select(x => x.riverbass).ToArray();

            int row_num = _context.GmfRivers.Where(x => namriver_list.Contains(x.namriver) && riverbass_list.Contains(x.riverbass)).ExecuteDelete();
            
            if (row_num == 0)
            {
                return false;
            }

            return true;
        }

        public bool DeleteRiver(GmfRiverDB riverDB)
        {
            int row_num = _context.GmfRivers.Where(x => riverDB.namriver.Equals(x.namriver) && riverDB.riverbass.Equals(x.riverbass)).ExecuteDelete();
            if (row_num == 0)
            {
                return false;
            }
            return true;
        }
        
        public string[] GetResourceIdList()
        {
            var list = _context.GmfRegions.Select(g => g.tregion).Distinct().ToArray();
            return list;
        }

        public List<GmfRiverDB> GetRiverList()
        {
            var list = _context.GmfRivers.Select(g => new GmfRiver() { namriver = g.namriver, riverbass = g.riverbass}).Distinct().ToArray();
            var list_db = _mapper.Map<List<GmfRiverDB>>(list);
            return list_db;
        }

        public List<GmfRiverDB> GetRiverList(string riverbass, string namriver)
        {
            var list = _context.GmfRivers
            .Where(g => 
            ((riverbass == null) ? true : g.riverbass.Equals(riverbass)) && 
            ((namriver == null) ? true : g.namriver.Equals(namriver))).ToList();
            
            var list_db = _mapper.Map<List<GmfRiverDB>>(list);
            return list_db;
        }

        public bool InsertRiverList(List<GmfRiverDB> list)
        {
            var db = _mapper.Map<List<GmfRiverDB>>(list);
            
            var group_Db = db.GroupBy(x => new {x.group_indeteficator});

            foreach(var group in group_Db)
            {
                GmfRiver b = null;
                foreach(var g in group)
                {
                    if(b is not null)
                    {
                       b.namriver = g.namriver;
                       b.riverbass = g.riverbass;
                       break;
                    }

                    b = _context.GmfRivers.Where(x => x.namriver.Equals(g.namriver) && x.riverbass.Equals(g.riverbass)).FirstOrDefault();
                    
                } 
            }

            _context.SaveChanges();
            return true;
        }

        public List<GmfCategoryDB> GetCategoryList(bool indicators)
        {
            var listDB = new List<GmfCategoryDB>();
            if (indicators)
            {
                var list = _context.GmfCategories.Include(i => i.GmfIndicators).Distinct()/*.AsNoTracking()*/.ToArray();
                listDB = _mapper.Map<List<GmfCategoryDB>>(list);
            }
            else 
            {
                var list = _context.GmfCategories.Distinct().ToArray();
                //var sql = list.ToQueryString();
                listDB = _mapper.Map<List<GmfCategoryDB>>(list);
            }
            
            return listDB;
        }

        public List<GmfProtocolDB> GetProtocols(uint year, uint kn, bool indicator = false) 
        {
            var DB = new List<GmfProtocolDB>();
            List<GmfProtocol>? list = null;
            if(indicator)
            {
                list = _context.GmfProtocols.Where(p => p.Kn == kn && p.God == year).ToList();
                DB = _mapper.Map<List<GmfProtocolDB>>(list);
            }
            else
            {
                list = _context.GmfProtocols.Select(x => 
                new GmfProtocol() { God = x.God, Kn = x.Kn, Pcat = x.Pcat, Pindicator = null, Aestimation = null, 
                Bestimation = null, MarkEstam = null, Prim = null, Opisanie = null })
                .Distinct().ToList();

                DB = _mapper.Map<List<GmfProtocolDB>>(list);
                DB = DB.Select(x => new GmfProtocolDB() { God = x.God, Kn = x.Kn}).DistinctBy(x => new {x.God , x.Kn}).ToList();
            }
            
           
            return DB;
        }

        public List<GmfProtocolDB> GetProtocols(bool indicator = false) 
        {
            var DB = new List<GmfProtocolDB>();
            var list = new List<GmfProtocol>();
            if(indicator)
            {
                list = _context.GmfProtocols.ToList();
            }
            else
            {
                list = _context.GmfProtocols.Select(x => 
                new GmfProtocol() { God = x.God, Kn = x.Kn, Pcat = x.Pcat, Pindicator = null, Aestimation = null, 
                Bestimation = null, MarkEstam = null, Prim = null, Opisanie = null })
                .Distinct().ToList();
            }
            DB = _mapper.Map<List<GmfProtocolDB>>(list);
            return DB;
        }

        public List<GmfProtocolDB> GetProtocols(int year, int kod, string bass = null, string river = null, string punkt = null,string pasp = null,string region = null, bool indicator = false) 
        {
            var DB = new List<GmfProtocolDB>();

            List<GmfProtocol?> list;

            if(indicator)
            {
                list = _context.GmfProtocols.Where(p => 
                ((year == -1) ? true : p.God == year) 
                && ((kod == -1) ? true : p.Kn == kod)
                && ((bass == null) ? true : p.KnNavigation.Bass.Equals(bass))
                && ((river == null) ? true : p.KnNavigation.River.Equals(river))
                && ((punkt == null) ? true : p.KnNavigation.Punkt.Equals(punkt))
                && ((pasp == null) ? true : p.KnNavigation.Pasp.Equals(pasp))
                && ((region == null) ? true : p.KnNavigation.Region.Equals(region))).ToList();
            }
            else
            {
                list = _context.GmfProtocols.Where(p => 
                ((year == -1) ? true : p.God == year)
                && ((kod == -1) ? true : p.Kn == kod)
                && ((bass == null) ? true : p.KnNavigation.Bass.Equals(bass))
                && ((river == null) ? true : p.KnNavigation.River.Equals(river)) 
                && ((punkt == null) ? true : p.KnNavigation.Punkt.Equals(punkt))
                && ((pasp == null) ? true : p.KnNavigation.Pasp.Equals(pasp))
                && ((region == null) ? true : p.KnNavigation.Region.Equals(region))).Select(x => 
                new GmfProtocol() { God = x.God, Kn = x.Kn, Pcat = x.Pcat, Pindicator = null, Aestimation = null, 
                Bestimation = null, MarkEstam = null, Prim = null, Opisanie = null })
                .ToList();
            }


            DB = _mapper.Map<List<GmfProtocolDB>>(list);
            return DB;
        }

        public bool InsertProtocols(List<GmfProtocolDB> models_db)
        {
            List<GmfProtocol> list;
            list = _mapper.Map<List<GmfProtocol>>(models_db);
            foreach (var l in list)
            {
                var conc = _context.GmfProtocols.Where(x => x.Kn == l.Kn && x.Pindicator == l.Pindicator && x.God == l.God).FirstOrDefault();
                if (conc is not null)
                {
                    conc.Opisanie = l.Opisanie;
                    conc.Prim = l.Prim;
                    conc.Pcat = l.Pcat;
                    conc.PindicatorNavigation = l.PindicatorNavigation;
                    conc.Aestimation = l.Aestimation;
                    conc.Bestimation = l.Bestimation;
                    conc.KnNavigation = l.KnNavigation;
                    conc.MarkEstam = l.MarkEstam;
                    conc.Date = l.Date;
                }
                else 
                {
                    _context.GmfProtocols.Add(l);
                }
                
            }
            _context.SaveChanges();
            return true;
        }

        public bool UpdateProtocols(List<GmfProtocolDB> models_db) 
        {
            List<GmfProtocol> list;
            list = _mapper.Map<List<GmfProtocol>>(models_db);
 
            foreach (var l in list) 
            {
                var conc = _context.GmfProtocols.Where(x => x.Kn == l.Kn && x.Pindicator == l.Pindicator && x.God == l.God).FirstOrDefault();
                if (conc is not null) 
                {
                    conc.Opisanie = l.Opisanie;
                    conc.Prim = l.Prim;
                    conc.Pcat = l.Pcat;
                    conc.PindicatorNavigation = l.PindicatorNavigation;
                    conc.Aestimation = l.Aestimation;
                    conc.Bestimation = l.Bestimation;
                    conc.KnNavigation = l.KnNavigation;
                    conc.MarkEstam = l.MarkEstam;
                }
                
            }
            _context.SaveChanges();
            /*var kn_list = models_db.Select(x => x.Kn).ToArray();
            var pindicator_list = models_db.Select(x => x.Pindicator).ToArray();
            var god_list = models_db.Select(x => x.God).ToArray();
           /* _context.GmfProtocols.Where(x => kn_list.Contains(x.Kn) && pindicator_list.Contains(x.Pindicator) && god_list.Contains(x.God))
            .ExecuteUpdate(x => x.SetProperty(p => p.Opisanie, p => ))*/

            return true;
        }

        public bool DeleteProtocols(int kn,int god)
        {
            var protocols= _context.GmfProtocols.Where(p => p.Kn == kn  && p.God == god);
            try
            {
                _context.GmfProtocols.RemoveRange(protocols);
            }
            catch(Exception e)
            {
                return false;
            }
           return true;
        }

        public List<GmfIndicatorDB> GetIndicatorList()
        {
            var list = _context.GmfIndicators.Distinct().ToList();
            var listdb = _mapper.Map<List<GmfIndicatorDB>>(list);
            return listdb;
        }

        public List<GmfIndicatorDB> GetIndicatorList(int kod, int year)
        {
            //var list = _context.GmfIndicators.Select(c => c.Namindicator.ToString()).Distinct().ToArray();
            var list = _context.GmfIndicators.Include(c => c.GmfProtocols.Where(p => p.Kn == kod && p.God == year)).ToList();
            List<GmfIndicatorDB> listdb = _mapper.Map<List<GmfIndicatorDB>>(list);
            return listdb;
        }

        public object GetIndicatorByCategoryId(int cat_id)
        {
            //var list = _context.GmfIndicators.Where(c => c.Cat == _context.GmfCategories.Where(c => {return true}c.Idcat == cat_id).Select(c => c.Idcat).FirstOrDefault());
            return null;//list;
        }

        public List<GmfPunctDB> GetPunctList(string pasp = null, bool? trans = null, string bass = null, string river = null, string region = null)
        {
            if (trans == null)
            {

            }
            var list = _context.GmfPuncts.Where
            (p => p.Gmf == true 
            && ((pasp == null) ? true : p.Pasp.Equals(pasp)) 
            && ((trans == null) ? true : (bool)trans == p.Trans)
            && ((bass == null) ? true : p.Bass.Equals(bass))
            && ((river == null) ? true : p.River.Equals(river))
            && ((region == null ? true : p.Region.Equals(region))))
            .ToList();
            var listdb = _mapper.Map<List<GmfPunctDB>>(list);
            return listdb;
        }


        public bool InsertPunkt(List<GmfPunctDB> models_db)
        {
            List<GmfPunct> list;
            list = _mapper.Map<List<GmfPunct>>(models_db);
            foreach (var l in list)
            {
                l.Gmf = true;
                var conc = _context.GmfPuncts.Where(x => x.Kod == l.Kod).FirstOrDefault();
                if (conc is not null)
                {
                    conc.Bass = l.Bass;
                    conc.Pasp = l.Pasp;
                    conc.Punkt = l.Punkt;
                    conc.Reestr = l.Reestr;
                    conc.Region = l.Region;
                    conc.River = l.River;
                    conc.Trans = l.Trans;
                }
                else 
                {
                    _context.GmfPuncts.Add(l);
                }
                
            }
            _context.SaveChanges();
            return true;
        }

        public bool DeletePunct(int kod)
        {
            int row_num = _context.GmfPuncts.Where(x => x.Kod == kod).ExecuteDelete();
            if (row_num == 0)
            {
                return false;
            }

            return true;
        }

        public List<GmfTotalDB> GetTotalList(int kn, int god)
        {
            var list = _context.GmfTotals.Where(x => x.Tkn == kn && x.Tgod == god).Include(t => t.gmfClass3).Include(t => t.gmfClass5).Include(t => t.gmfPunct).ToList();
            var list_db = _mapper.Map<List<GmfTotalDB>>(list);
            return list_db;
        }

        public int GetClass3(double Bestimation)
        {
            return _context.GmfClass3s.Where(x => x.Np3 < (decimal)Bestimation && x.Vp3 >= (decimal)Bestimation).Select(x => x.Class3).FirstOrDefault();    
        }

        public int GetClass5(double Aestimation)
        {
            return _context.GmfClass5s.Where(x => x.Np5 < (decimal)Aestimation && x.Vp5 >= (decimal)Aestimation).Select(x => x.Class5).FirstOrDefault();    
        }

        public List<string> GetGroupIndexsName(int groupNumber)
        {
            var list = _context.GmfIndicators.Where(x => x.GroupIndic == groupNumber).Select(x => x.Idindicator).ToList();
            return list;
        }

        public List<string> GetZonaIndexName(int zona_index)
        {
            var list = _context.GmfIndicators.Where(z => z.Zona_indic == zona_index).Select(x => x.Idindicator).ToList();
            return list;
        }

        public bool InsertGmfTotal(GmfTotalDB totalDB)
        {
            var boolean = true;
            var total = _mapper.Map<GmfTotal>(totalDB);
            try
            {
                var total_ = _context.GmfTotals.Where(t => t.Tgod == total.Tgod && t.Tkn == total.Tkn).FirstOrDefault();
                if (total_ is null)
                {
                    _context.GmfTotals.Add(total);
                }
                else
                {
                    var type = total.GetType();

                    FieldInfo[] fields_type = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                    foreach (FieldInfo field in fields_type)
                    {
                        // Получение значения поля на исходном объекте
                        object value = field.GetValue(total);

                        // Установка значения поля на целевом объекте
                        field.SetValue(total_, value);
                    }
                    
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            return boolean;
        }

        public bool InsertBass(string nambass)
        {
            bool b = true;
            try
            {
                if (_context.GmfBasss.Where(x => x.Nambass.Equals(nambass)).Any())
                {
                    return false;
                }
                GmfBass bass = new GmfBass();
                bass.Nambass = nambass;
                _context.GmfBasss.Add(bass);
            }
            catch
            {
                b = false;
            }
            return b;
        }

        public bool DeleteBass(string nambass)
        {
            if (nambass.Equals("default_value"))
            {
                return false;
            }

            int row_nums = _context.GmfBasss.Where(x => x.Nambass.Equals(nambass)).ExecuteDelete();         

            return true;
        }
        public async Task SaveChangesAsync()
        {
            _context.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }


    }
}
