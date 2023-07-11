using AutoMapper;
using DatabaseLayer.Context;
using DatabaseLayer.DBModel;
using DatabaseLayer.IRepository;
using DatabaseLayer.TableModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public string[] GetPoolList() 
        {
            var list = _context.GmfPuncts.Select(g => g.Bass).Distinct().ToArray();
            return list;
        }
        public string[] GetResourceIdList()
        {
            var list = _context.GmfPuncts.Select(g => g.Punkt).Distinct().ToArray();
            return list;
        }

        public string[] GetRiverList()
        {
            var list = _context.GmfPuncts.Select(g => g.River).Distinct().ToArray();
            return list;
        }

        public List<GmfCategoryDB> GetCategoryList(bool indicators)
        {
            var listDB = new List<GmfCategoryDB>();
            if (indicators)
            {
                var list = _context.GmfCategories.Include(i => i.GmfIndicators).Distinct().ToArray();
                listDB = _mapper.Map<List<GmfCategoryDB>>(list);
            }
            else 
            {
                var list = _context.GmfCategories.Distinct().ToArray();
                listDB = _mapper.Map<List<GmfCategoryDB>>(list);
            }
            
            return listDB;
        }

        public List<GmfProtocolDB> GetProtocols(int Kn, int year) 
        {
            var DB = new List<GmfProtocolDB>();
            var list = _context.GmfProtocols.Where(p => p.Kn == Kn && p.God == year).ToList();
            DB = _mapper.Map<List<GmfProtocolDB>>(list);
            return DB;
        }

        public bool InsertProtocols(List<GmfProtocolDB> models_db)
        {
            List<GmfProtocol> list;
            list = _mapper.Map<List<GmfProtocol>>(models_db);
            _context.GmfProtocols.AddRange(list);
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
            var list = _context.GmfIndicators.Where(c => c.Cat == _context.GmfCategories.Where(c => c.Idcat == cat_id).Select(c => c.Idcat).FirstOrDefault());
            return list;
        }

        public List<GmfPunctDB> GetPunctList()
        {
            var list = _context.GmfPuncts.Where(p => p.Gmf == true).Select(p => new GmfPunct { Kod = p.Kod, Punkt = p.Punkt, Bass = p.Bass, River = p.River }).ToList();
            var listdb = _mapper.Map<List<GmfPunctDB>>(list);
            return listdb;
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
