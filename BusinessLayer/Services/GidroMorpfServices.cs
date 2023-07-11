using AutoMapper;
using BusinessLayer.IServices;
using BusinessLayer.Models;
using DatabaseLayer.Context;
using DatabaseLayer.DBModel;
using DatabaseLayer.IRepository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class GidroMorpfServices : IGidroMorpfServices
    {
        private IGidroMorpfRepository _gidromorf { get; set; }

        private IMapper _mapper { get; set; }

        private ILogger _logger { get; set; }

        public GidroMorpfServices(IGidroMorpfRepository gidroChemistry, ILoggerProvider loggerProvider, IMapper mapper) 
        {
            _gidromorf = gidroChemistry;
            _logger = loggerProvider.CreateLogger("GidroServiceLogger");
            _mapper = mapper;
        }


        public string[] GetPoolList() 
        {
            string[] list;

            try
            {
                list = _gidromorf.GetPoolList();
            }
            catch (Exception e) 
            {
                _logger.LogError(e.Message);
                _logger.LogError("an errore has occured in GidroChemService at GetPoolList");
            }
            finally
            {
                list = new string[0];
            }
            
            return list;
        }

        public string[] GetResourceIdList()
        {
            string[] list;
            try 
            {
                list = _gidromorf.GetResourceIdList();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                _logger.LogError("an errore has occured in GidroChemService at GetResourceIdList");
            }
            finally
            {
                list = new string[0];
            }

            return list;
        }

        public string[] GetRiverList()
        {
            string[] list;
            try 
            {
                list = _gidromorf.GetRiverList();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                _logger.LogError("an errore has occured in GidroChemService at GetRiverList");
            }
            finally
            {
                list = new string[0];
            }
            return list;
        }

        public List<GmfCategoryBL> GetCategoryList(bool indicators)
        {
            List<GmfCategoryBL> listBL = new();
            List<GmfCategoryDB> listDB = new();
            try 
            {
                if (indicators)
                {
                    listDB = _gidromorf.GetCategoryList(true);
                }
                else 
                {
                    listDB = _gidromorf.GetCategoryList(false);
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                _logger.LogError("an errore has occured in GidroChemService at GetCategoryList");
            }
            finally
            {
                listBL = _mapper.Map<List<GmfCategoryBL>>(listDB);
            }

            return listBL;
        }

        public List<GmfProtocolBL> GetProtocols(string kod, string year) 
        {
            List<GmfProtocolBL> model;
            var modeldb = _gidromorf.GetProtocols(Int32.Parse(kod), Int32.Parse(year));
            model = _mapper.Map<List<GmfProtocolBL>>(modeldb);
            return model;
        }

        public bool InsertProtocols(List<GmfProtocolBL> models_bl)
        {
            List<GmfProtocolDB> models_db;
            models_db = _mapper.Map<List<GmfProtocolDB>>(models_bl);
            var success = _gidromorf.InsertProtocols(models_db);
            _gidromorf.SaveChanges();
            return success;
        }

        public List<GmfIndicatorBL> GetIndicatorList(string kod, string year)
        {
            List<GmfIndicatorBL> listBL;
            List<GmfIndicatorDB> listDB = null; 
            try
            {
                if (kod is null && year is null)
                {
                    listDB = _gidromorf.GetIndicatorList();
                }
                else 
                {
                    listDB = _gidromorf.GetIndicatorList(Int32.Parse(kod), Int32.Parse(year));
                }
                
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                _logger.LogError("an errore has occured in GidroChemService at GetIndicatorList");
            }
            finally
            {
                listBL = _mapper.Map<List<GmfIndicatorBL>>(listDB);
            }

            return listBL;
        }

        public List<GmfPunctBL> GetPunctList()
        {
            List<GmfPunctBL> listBL;
            List<GmfPunctDB> listDB = null;
            try
            {                
                listDB = _gidromorf.GetPunctList();                
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                _logger.LogError("an errore has occured in GidroChemService at GetIndicatorList");
            }
            finally
            {
                listBL = _mapper.Map<List<GmfPunctBL>>(listDB);
            }

            return listBL;
        }


    }
}
