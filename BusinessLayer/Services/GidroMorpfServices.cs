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
using GeneralObject.LinqExtension;
using DatabaseLayer.TableModels;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Newtonsoft.Json;

namespace BusinessLayer.Services
{
    //класса business уровня, которыый содержит реализацию функционала как манипулировать данными что вернула бд 
    public class GidroMorpfServices : IGidroMorpfServices
    {
        // интерфейс уровня database layer предоставляющий доступ к данным бд гидроморфологии
        private IGidroMorpfRepository _gidromorf { get; set; }
        // это интерфейс маппера , который служит для преобразование одних уровней и форматов моделей данных в другие уровнии и форматы моделей данных 
        private IMapper _mapper { get; set; }
        // интерфейс предоставляющий доступ к моей реализации логгера, для отслеживания сообщений об ошибках на уровне бизнес уровня
        private ILogger _logger { get; set; }

        /// <summary>
        /// контроллер класса 
        /// </summary>
        /// <param name="gidroChemistry"> интерфейс уровня database layer предоставляющий доступ к данным бд гидроморфологии</param>
        /// <param name="loggerProvider"> интерфейс предоставляющий доступ к моей реализации логгера, для отслеживания сообщений об ошибках на уровне бизнес уровня</param>
        /// <param name="mapper"> это интерфейс маппера , который служит для преобразование одних уровней и форматов моделей данных в другие уровнии и форматы моделей данных</param>
        public GidroMorpfServices(IGidroMorpfRepository gidroChemistry, ILoggerProvider loggerProvider, IMapper mapper) 
        {
            _gidromorf = gidroChemistry;
            _logger = loggerProvider.CreateLogger("GidroServiceLogger");
            _mapper = mapper;
        }

        /// <summary>
        /// метод который получает данные из уровня бд, по моим соображениям метод должен формировать сообщения для статус кода http ответа 
        /// какую бизнес логику сюда можно запихнуть сюда я не знаю 
        /// </summary>
        /// <returns>возвращает массив строк, которые содержат информацию о уникальных бассейнах в какой то таблице</returns>
        public string[] GetBassList() 
        {
            string[] list = null;

            try
            {
                list = _gidromorf.GetBassList();
            }
            catch (Exception e) 
            {
                _logger.LogError(e.Message);
                _logger.LogError("an errore has occured in GidroChemService at GetPoolList");
            }
            finally
            {
            }
            
            return list;
        }

        public bool InsertBassList(string[] list)
        {
            var b = true;
            try
            {
                foreach(var l in list)
                {
                    _gidromorf.InsertBass(l);
                }
            }
            catch
            {
                b = false;
            }

            return b;
        }

        public async Task<bool> UpdateBass(string _new, string _old)
        {
            var b = true;

            b = _gidromorf.UpdateBass(_new, _old);

            _gidromorf.SaveChanges();

            return b;
        }

        public async Task<List<GmfRegionBL>> GetRegionList()
        {

            var list = _gidromorf.GetRegionList();
            var list_bl = _mapper.Map<List<GmfRegionBL>>(list);
            return list_bl;
        }

        public async Task<bool> UpdateRegion(string old_name, string new_name)
        {
            try
            {
                var b = _gidromorf.UpdateRegion(old_name, new_name);
                _gidromorf.SaveChanges();
                return b;
            }
            catch
            {
                return false;
            }
            
        }

        public async Task<bool> DeleteRegion(string treg)
        {
            bool b = true;
            try
            {
                b = await _gidromorf.DeleteRegion(treg);
                _gidromorf.SaveChanges();
                return b;    
            }
            catch(DbUpdateException ex)
            {
                var inner = ex.InnerException;
                var message = inner.Message;
                throw new Exception(message);
            }
            catch(PostgresException ex)
            {
                string mess = null;
                if (ex.Code.Equals("23503"))
                {
                    mess = "\'" + treg + "\'" + " " + "нельзя удалить запись. так как на нее ссылаются другие записи из таблицы пункт,\n" +
                                  "удалите их сперва: \n";

                    var list_of_punct = _gidromorf.GetPunctList(null, null, null,null, treg);

                    var error_message = new { Message = mess, body = list_of_punct.Select(x => new { kod_of_punct = x.Kod, Name = x.Punkt})};

                    string json = JsonConvert.SerializeObject(error_message);

                    throw new CustomException(json.Replace("\\", ""));
                }
                else
                {
                    b = false;
                }
            }
            finally
            {

            }
            return b;
        }

        public bool UpdateRiverList(List<GmfRiverBL> list_new, List<GmfRiverBL> list_old)
        {
            var b = true;

            var list = new List<GmfRiverDB>();

            try
            {
                var list_new_db = _mapper.Map<List<GmfRiverDB>>(list_new);
                var list_old_db = _mapper.Map<List<GmfRiverDB>>(list_old);

                foreach(var new_db in list_new_db)
                {
                    b = _gidromorf.InsertBass(new_db.riverbass);
                }

                _gidromorf.SaveChanges();

                b = _gidromorf.UpdateRiverList(list_new_db, list_old_db);

                if (b == false)
                {
                    return b;
                }

                _gidromorf.SaveChanges();
            }
            catch
            {

            }
            finally
            {

            }

            return b;
        }

        public async Task<bool> DeleteRiverList(List<GmfRiverBL> list_bl)
        {
            var b = true;
            var list_db = _mapper.Map<List<GmfRiverDB>>(list_bl);
            string global_name = null;

            try
            {
                foreach(var list in list_db)
                {
                    global_name = list.riverbass + "---" + list.namriver; 
                    b = _gidromorf.DeleteRiver(list);               
                }
                
                _gidromorf.SaveChanges();
            }
            catch(DbUpdateException ex)
            {
                var inner = ex.InnerException;
                var message = inner.Message;
                throw new Exception(message);
            }
            catch(PostgresException ex)
            {
                string mess = null;
                if (ex.Code.Equals("23503"))
                {
                    mess = "\'" + global_name + "\'" + " " + "нельзя удалить запись. так как на нее ссылаются другие записи из таблицы пункт,\n" +
                                  "удалите их сперва: \n";

                    var mass = global_name.Split("---");
                    var list_of_punct = _gidromorf.GetPunctList(null, null, mass[0],mass[1], null);

                    var error_message = new { Message = mess, body = list_of_punct.Select(x => new { kod_of_punct = x.Kod, Name = x.Punkt})};

                    string json = JsonConvert.SerializeObject(error_message);

                    throw new CustomException(json.Replace("\\", ""));
                
                }
                else
                {
                    mess = ex.Message;
                    b = false;
                }   
            }
            finally
            {
            }

            return b;
        }

 

        /// <summary>
        /// метод который получает данные из уровня бд, по моим соображениям метод должен формировать сообщения для статус кода http ответа 
        /// какую бизнес логику сюда можно запихнуть сюда я не знаю 
        /// </summary>
        /// <returns>возвращает массив строк, которые содержат информацию о чем хз в какой то таблице</returns>
        public string[] GetResourceIdList()
        {
            string[] list = null;
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
            }

            return list;
        }

        /// <summary>
        /// метод который получает данные из уровня бд, по моим соображениям метод должен формировать сообщения для статус кода http ответа 
        /// какую бизнес логику сюда можно запихнуть сюда я не знаю 
        /// </summary>
        /// <returns>возвращает массив строк, которые содержат информацию о уникальных реках в какой то таблице</returns>
        public List<GmfRiverBL> GetRiverList()
        {
            List<GmfRiverDB> list_db = null;
            List<GmfRiverBL> list_bl = null;
            try 
            {
                list_db = _gidromorf.GetRiverList();
                list_bl = _mapper.Map<List<GmfRiverBL>>(list_db);

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                _logger.LogError("an errore has occured in GidroChemService at GetRiverList");
            }
            finally
            {
            }
            return list_bl;
        }

        public bool InsertRiverList(List<GmfRiverBL> list)
        {
            bool b = true; 
            try
            {
                var dict_db = _mapper.Map<List<GmfRiverDB>>(list);
                b = _gidromorf.InsertRiverList(dict_db);
            }
            catch(Exception e)
            {
                b = false;
            }
            return b;
        }

        /// <summary>
        /// метод который получает данные из уровня бд, по моим соображениям метод должен формировать сообщения для статус кода http ответа 
        /// какую бизнес логику сюда можно запихнуть сюда я не знаю 
        /// </summary>
        /// <param name="indicators"> указывает уровню бд стоит ли возвращаьб в списке связанные сущности (строки) из таблицы indicators</param>
        /// <returns>возвращает List<GmfCategoryBL>, который содержит информацию о всех строках в константной таблице Categories</returns>
        
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
        /// <summary>
        /// метод который получает данные из уровня бд, по моим соображениям метод должен формировать сообщения для статус кода http ответа 
        /// какую бизнес логику сюда можно запихнуть сюда я не знаю 
        /// </summary>
        /// <param name="year"> просто фильтр по полю в таблице  </param>
        /// <param name="kod"> просто фильтр по полю в таблице</param>
        /// <returns> возращает List<GmfProtocolBL> который является интерпретацией списка моделей уровня бд на уровень модели бизнесс уровня GmfProtocolBL</returns>
        public List<GmfProtocolBL> GetProtocols(string year, string kod, bool indicator = false) 
        {
            List<GmfProtocolBL> model = null;
            uint ikod;
            uint iyear;
            List<GmfProtocolDB>? modeldb = null;
            try
            {
                ikod = uint.Parse(kod);
                iyear = uint.Parse(year);
                modeldb = _gidromorf.GetProtocols(iyear,ikod, indicator);
            }
            catch(Exception e)
            {
                return model;
            }

            
            model = _mapper.Map<List<GmfProtocolBL>>(modeldb);
            return model;
        }
        /// <summary>
        /// получить все протоколы из таблицы Protocols
        /// </summary>
        /// <param name="indicator">параметр указывает стоит ли вывести все значения и все поля или только уникальные строки и уникальыне поля</param>
        /// <returns>List<GmfProtocolBL> который является интерпретацией списка моделей уровня бд на уровень модели бизнесс уровня GmfProtocolBL</returns>
        public List<GmfProtocolBL> GetProtocols(bool indicator = false) 
        {
            List<GmfProtocolBL> model;
            var modeldb = _gidromorf.GetProtocols(indicator);
            model = _mapper.Map<List<GmfProtocolBL>>(modeldb);
            return model;
        }

        /// <summary>
        /// получить модели бизнесс уровня GmfProtocolBL  из моделей уровня  бд  по фильтрам
        /// </summary>
        /// <param name="year"> фильтр </param>
        /// <param name="bass"> фильтр</param>
        /// <param name="river"> фильтр</param>
        /// <param name="punkt"> фильтр</param>
        /// <param name="pasp"> фильтр</param>
        /// <param name="region"> фильтр</param>
        /// <param name="indicator"> фильтр</param>
        /// <returns> List<GmfProtocolBL> </returns>
        public List<GmfProtocolBL> GetProtocols(string year, string kod = null, string bass = null, string river = null, string punkt = null, string pasp = null, string region = null, bool indicator = false)
        {
            List<GmfProtocolBL> model;
            var modeldb = _gidromorf.GetProtocols(year is null ? -1 : Int32.Parse(year), kod is null ? -1 : Int32.Parse(kod), bass, river, punkt,pasp, region, indicator);
            model = _mapper.Map<List<GmfProtocolBL>>(modeldb);
            return model;
        }
        

        /// <summary>
        ///  интерпретирует данные бизнесс уровня GmfProtocolBL на существующие данные уровеня базы данных и изменяет их
        /// </summary>
        /// <param name="models_bl"> список моделей бизнесс уровня </param>
        /// <returns> возвращает индикатор исполнения операции</returns>
        public bool UpdateProtocols(List<GmfProtocolBL> models_bl) 
        {
            List<GmfProtocolDB> models_db;
            models_db = _mapper.Map<List<GmfProtocolDB>>(models_bl);
            var success = _gidromorf.UpdateProtocols(models_db);
            _gidromorf.SaveChanges();
            return success;
        }


        /// <summary>
        ///  интерпретирует данные бизнесс уровня GmfProtocolBL на новые данные уровеня базы данных и изменяет их
        /// </summary>
        /// <param name="models_bl"> список моделей бизнесс уровня </param>
        /// <returns> возвращает индикатор исполнения операции</returns>
        public bool InsertProtocols(List<GmfProtocolBL> models_bl)
        {
            GmfTotalBL total_bl = new GmfTotalBL(); 

            List<GmfProtocolDB> models_db;
            models_db = _mapper.Map<List<GmfProtocolDB>>(models_bl);

            var success = _gidromorf.InsertProtocols(models_db);

            if(success)
            {
                EstimationScores(models_bl, total_bl);
                EstimationByThreeGroup(models_bl, total_bl);
                EstimationByZona(models_bl, total_bl);
            }

            total_bl.Tkn = models_bl.FirstOrDefault().Kn;
            total_bl.Tgod = models_bl.FirstOrDefault().God;

            var total_db = _mapper.Map<GmfTotalDB>(total_bl);

            if(!success)
            {            
                _logger.LogError("an errore has occured during attempt to write GmfProtocol in InsertProtocols method");
                return false;        
            }

            var boolean = _gidromorf.InsertGmfTotal(total_db);

            if(!boolean)
            {            
                _logger.LogError("an errore has occured during attempt to write GmfTotal in InsertProtocols method");
                return false;        
            }
            _gidromorf.SaveChanges();

            return success;
        }

        /// <summary>
        /// удаляет данные уровня бд по уникальным ключам
        /// </summary>
        /// <param name="Kn"> уникальный ключ модели уровня бд </param>
        /// <param name="God"> уникальный ключ модели уровня бд </param>
        /// <returns></returns>
        public bool DeleteProtocols(string Kn, string God)
        {
            bool b;
            try
            {
                b = _gidromorf.DeleteProtocols(Int32.Parse(Kn), Int32.Parse(God));
                _gidromorf.SaveChanges();
                return b;
            }
            catch(DbUpdateException e)
            {
                var innerException = e.InnerException;
                string message = innerException.Message;
                throw new Exception(message);
            }
            catch
            {
                b = false;
            }

            return b;
            
        }

        /// <summary>
        /// Предоставляет интерпретацию данных модели GmfIndicatorBL из модели уровня базы данных
        /// </summary>
        /// <param name="kod"> уникальный ключ модели уровня бд </param>
        /// <param name="year"> уникальный ключ модели уровня бд </param>
        /// <returns> List<GmfIndicatorBL> список моделей GmfIndicatorBL</returns>
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

        /// <summary>
        ///  Предоставляет данные о существующих пунктах в которых проводят гидромолфологические наблюдения в интерпретации модели бизнес уровня
        /// </summary>
        /// <param name="pasp"> поле фильтр из модели уровня бд</param>
        /// <param name="trans">необязательное поле фильтр из модели уровня бд</param>
        /// <returns>List<GmfPunctBL> список модей бизнесс уровня о существующих пунктах в которых проводят гидромолфологические наблюдения</returns>
        public List<GmfPunctBL> GetPunctList(string pasp, bool? trans = null)
        {
            List<GmfPunctBL> listBL;
            List<GmfPunctDB> listDB = null;
            try
            {                
                listDB = _gidromorf.GetPunctList(pasp, trans);                
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

        public List<GmfTotalBL> GetTotalList(int kn = -1, int god = -1,string region = null, string bass = null, string river = null, string punkt = null, string pasp = null)
        {
            List<GmfTotalBL> listBL;
            List<GmfTotalDB> listDB = new List<GmfTotalDB>();
            try
            {
                var protocols = _gidromorf.GetProtocols(god,kn, bass, river, punkt, pasp,region, false);

                var groupBy = protocols.GroupBy( x => new  {x.God, x.Kn} );

                foreach(var group in groupBy)
                {
                    var p = group.FirstOrDefault();
                    var total_list = _gidromorf.GetTotalList(p.Kn, p.God);
                    foreach(var t in total_list)
                    {
                        if (t.Aclass5 is null)
                        {
                            t.gmfClass5 = new GmfClass5DB();
                        }

                        if (t.Bclass3 is null)
                        {
                            t.gmfClass3  = new GmfClass3DB();
                        }

                        if (t.gmfPunct is null)
                        {
                            t.gmfPunct = new GmfPunctDB();
                        }
                    } 
                    listDB.AddRange(total_list.AsEnumerable());
                }
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                _logger.LogError("");
            }
            finally
            {
                listBL = _mapper.Map<List<GmfTotalBL>>(listDB);
            }
            return listBL;
        }

        public bool InputPunctList(List<GmfPunctBL> models)
        {
            List<GmfPunctDB> models_db;
            models_db = _mapper.Map<List<GmfPunctDB>>(models);
            var success = _gidromorf.InsertPunkt(models_db);
            _gidromorf.SaveChanges();
            return success;
        }

        public bool DeletePunctList(int[] models)
        {
            bool b = true;
            string global_name = null;
            try
            {
                foreach(var m in models)
                {
                    global_name = m.ToString();
                    b = _gidromorf.DeletePunct(m);
                    if (b == false)
                    {
                        return b;
                    }
                }     
            }
            catch(DbUpdateException e)
            {
                var innerException = e.InnerException;
                string message = innerException.Message;
                throw new Exception(message);
            }
            catch(PostgresException ex)
            {
                string mess = null;
                if (ex.Code.Equals("23503"))
                {
                    mess = "\'" + global_name + "\'" + " " + "нельзя удалить запись. так как на нее ссылаются другие записи из таблицы пункт,\n" +
                                  "удалите их сперва: \n";

                    var list_of_protocols = _gidromorf.GetProtocols(-1,Int32.Parse(global_name),null, null, null, null, null, false).Select(x => new {Kn = x.Kn, God = x.God}).Distinct();

                    var error_message = new { Message = mess, body = list_of_protocols.Select(x => new { kod_of_protocols = x.Kn, year = x.God})};

                    string json = JsonConvert.SerializeObject(error_message);

                    throw new CustomException(json.Replace("\\", ""));
                
                }
                else
                {
                    mess = ex.Message;
                    b = false;
                }   
            }
            finally
            {
            }

            return b;
       
        }

        public bool InsertBass(string[] nambass)
        {
            var b = true;
            try
            {
                foreach(var nam in nambass)
                {
                    _gidromorf.InsertBass(nam);
                }
            }
            catch
            {
                b = false;
            }

            _gidromorf.SaveChanges();

            return b;
        }

        public bool DeleteBass(string[] nambass)
        {
            var b = true;
            string global_name = null;
            try
            {
                foreach(var nam in nambass)
                {
                    global_name = nam;
                    _gidromorf.DeleteBass(nam);
                }

                _gidromorf.SaveChanges();
            }
            catch(DbUpdateException e)
            {
                var innerException = e.InnerException;
                string message = innerException.Message;
                throw new Exception(message);
            }
            catch(PostgresException ex)
            {
                string mess = null;
                if (ex.Code.Equals("23503"))
                {
                    mess = @"\'" + global_name + @"\'" + " " + @"нельзя удалить запись. так как на нее ссылаются другие записи из таблицы рек,\n" +
                                  @"удалите их сперва: \n";

                    var list_of_river = _gidromorf.GetRiverList(global_name, null);

                    var error_message = new { Message = mess, body = list_of_river.Select(x => new { namriver = x.namriver})};

                    string json = JsonConvert.SerializeObject(error_message);

                    throw new CustomException(json.Replace("\\", ""));
                }
                else
                {
                    mess = ex.Message;
                    b = false;
                }   
            }
            finally
            {
            }

            return b;
        }


        public async Task<bool> RecalcAllTotals()
        {
            var b = true;

            var protocols = _gidromorf.GetProtocols(-1, -1, null, null, null, null, null, true);

            var protocols_bl = _mapper.Map<List<GmfProtocolBL>>(protocols);

            var groupBy = protocols_bl.GroupBy( x => new  {x.God, x.Kn} );

            foreach(var g in groupBy)
            {
                try
                {
                    var pr_bl = g.ToList();
                    GmfTotalBL total_bl = new GmfTotalBL();
                    total_bl.Tkn = pr_bl.FirstOrDefault().Kn;
                    total_bl.Tgod = pr_bl.FirstOrDefault().God;

                    EstimationScores(pr_bl, total_bl);
                    EstimationByThreeGroup(pr_bl, total_bl);
                    EstimationByZona(pr_bl, total_bl);

                    var total_db = _mapper.Map<GmfTotalDB>(total_bl);

                    var boolean = _gidromorf.InsertGmfTotal(total_db);

                }
                catch(Exception e)
                {
                    b = false;
                }
                
            }

            _gidromorf.SaveChanges();

            return b;
        }

        private void EstimationScores(List<GmfProtocolBL> models_bl, GmfTotalBL total_bl)
        {
            var groupedItems = models_bl.GroupBy(x => new {x.Kn, x.God});

            foreach (var group in groupedItems)
            {
                var summA  = 0;
                var summB = 0;
                var countA = 0;
                var countB = 0;

                //весь цикл это один протокол   
                foreach (var item in group)
                {
                    if (!string.IsNullOrEmpty(item.Aestimation))
                    {
                        if(!item.Aestimation.Equals("-") && !item.Aestimation.Equals("НП"))
                        {
                            summA += Int32.Parse(item.Aestimation);

                            countA++;
                        }
                        
                    }

                    if(!string.IsNullOrEmpty(item.Bestimation))
                    {
                        if(!item.Bestimation.Equals("-") && !item.Bestimation.Equals("НП"))
                        {
                            summB += Int32.Parse(item.Bestimation);

                            countB++;
                        }
                        
                    }
                }

                double averageA = 0;
                double averageB = 0;

                if(countA != 0)
                {
                    averageA = summA / (countA * 1.0);
                }
                
                if (countB != 0)
                {
                    averageB = summB / (countB * 1.0);
                }

                int? Aestimation = _gidromorf.GetClass5(averageA);
                int? Bestimation = _gidromorf.GetClass3(averageB);

                total_bl.Aclass5 = Aestimation == 0 ? null : Aestimation;
                total_bl.Aesb5 = Math.Round((decimal)averageA,1) == 0 ? null : Math.Round((decimal)averageA,1);

                total_bl.Bclass3 = Bestimation == 0 ? null : Bestimation;;
                total_bl.Besb3 = Math.Round((decimal)averageB, 1) == 0 ? null : Math.Round((decimal)averageB, 1);

                total_bl.GmfKlass = (Aestimation is null | Aestimation == 0) ? ((Bestimation is null | Bestimation == 0) ? null : Bestimation) : Aestimation;             
            }      
        }


        private void EstimationByThreeGroup(List<GmfProtocolBL> models_bl, GmfTotalBL total_bl)
        {
            var groupedItems  = models_bl.GroupBy(x => new { x.Kn, x.God});

            foreach (var group in groupedItems)
            {              
                var summOneA = 0;
                var countOneA = 0;
                var MaxFiveA = new List<int>();
                //var countFiveA = 0;
                var summSixA = 0;
                var countSixA = 0;
                var summOneB = 0;
                var countOneB = 0;
                var MaxFiveB = new List<int>();
                //var countFiveB = 0;
                var summSixB = 0;
                var countSixB = 0;

                var groupOne = _gidromorf.GetGroupIndexsName(1);
                var groupSecond = _gidromorf.GetGroupIndexsName(2);;
                var groupThird = _gidromorf.GetGroupIndexsName(3);;

                //весь цикл это один протокол   
                foreach (var item in group)
                {
                    if (item.Pindicator.ContainsAny(groupSecond.ToArray()))
                    {
                        if(!string.IsNullOrEmpty(item.Aestimation))
                        {
                            if(!item.Aestimation.Equals("-") && !item.Aestimation.Equals("НП"))
                            {
                                MaxFiveA.Add(Int32.Parse(item.Aestimation));
                            }
                                                        
                        }
                        
                        if(!string.IsNullOrEmpty(item.Bestimation))
                        {
                            if(!item.Bestimation.Equals("-") && !item.Bestimation.Equals("НП"))
                            {
                                MaxFiveB.Add(Int32.Parse(item.Bestimation));
                            }
                            
                        }
                        
                    }
                    else if (item.Pindicator.ContainsAny(groupThird.ToArray()))
                    {
                        if(!string.IsNullOrEmpty(item.Aestimation) )
                        {
                            if(!item.Aestimation.Equals("-")&& !item.Aestimation.Equals("НП"))
                            {
                                summSixA += Int32.Parse(item.Aestimation);
                                countSixA++;
                            }
                           
                        }
                        
                        if(!string.IsNullOrEmpty(item.Bestimation))
                        {
                            if(!item.Bestimation.Equals("-")&& !item.Bestimation.Equals("НП"))
                            {
                                summSixB += Int32.Parse(item.Bestimation);
                                countSixB++;
                            }
                           
                        }
                    }
                    else if (item.Pindicator.ContainsAny(groupOne.ToArray()))
                    {
                        if(!string.IsNullOrEmpty(item.Aestimation))
                        {
                            if(!item.Aestimation.Equals("-")&& !item.Aestimation.Equals("НП"))
                            {
                                summOneA += Int32.Parse(item.Aestimation);
                                countOneA++;
                            }
                            
                        }
                        
                        if(!string.IsNullOrEmpty(item.Bestimation))
                        {
                            if(!item.Bestimation.Equals("-")&& !item.Bestimation.Equals("НП"))
                            {
                                summOneB += Int32.Parse(item.Bestimation);
                                countOneB++;
                            }
                        }                      
                    }                  
                }

                double? AEstimation1 = double.IsNaN(summOneA / (countOneA * 1.0)) ? null : summOneA / (countOneA * 1.0);
                double? AEstimation2 = null;
                if(MaxFiveA.Count() != 0)
                {   
                    AEstimation2 = MaxFiveA.Max();
                }
                else
                {
                    AEstimation2 = null;            
                }

                double? Aestimation3 = double.IsNaN(summSixA / (countSixA * 1.0)) ? null : summSixA / (countSixA * 1.0); 

                double? Bestimation1 = double.IsNaN(summOneB / (countOneB * 1.0)) ? null : summOneB / (countOneB * 1.0);
                double? Bestimation2 = null;
                if(MaxFiveB.Count() != 0)
                {
                    Bestimation2 = MaxFiveB.Max();
                }
                else
                {
                    Bestimation2 = null;
                }
                
                double? Bestimation3 = double.IsNaN(summSixB / (countSixB * 1.0)) ? null : summSixB / (countSixB * 1.0);


                total_bl.Aesgr1 = AEstimation1 is null ? null : Math.Round((decimal)AEstimation1,1);
                total_bl.Aesgr2 = AEstimation2 is null ? null : Math.Round((decimal)AEstimation2,1);
                total_bl.Aesgr3 = Aestimation3 is null ? null : Math.Round((decimal)Aestimation3,1);

                total_bl.Besgr1 = Bestimation1 is null ? null : Math.Round((decimal)Bestimation1,1);
                total_bl.Besgr2 = Bestimation2 is null ? null : Math.Round((decimal)Bestimation2,1);
                total_bl.Besgr3 = Bestimation3 is null ? null : Math.Round((decimal)Bestimation3,1);              
            }     
        }


        private void EstimationByZona(List<GmfProtocolBL> models_bl, GmfTotalBL total_bl)
        {
            var groupedItems  = models_bl.GroupBy(x => new { x.Kn, x.God});
            foreach (var group in groupedItems)
            {
                var firstZonaA = 0;
                var firstCountA = 0;
                var secondZonaA = 0;
                var secondCountA = 0;
                var thirdZonaA = 0;
                var thirdCountA = 0;

                var firstZonaB = 0;
                var firstCountB = 0;
                var secondZonaB = 0;
                var secondCountB = 0;
                var thirdZonaB = 0;
                var thirdCountB = 0;

                var zonaFirst = _gidromorf.GetZonaIndexName(1);
                var zonaSecond = _gidromorf.GetZonaIndexName(2);
                var zonaThird = _gidromorf.GetZonaIndexName(3);
                foreach (var item in group)
                {
                    if (item.Pindicator.ContainsAny(zonaFirst.ToArray()))
                    {
                        if(!string.IsNullOrEmpty(item.Aestimation))
                        {
                            if(!item.Aestimation.Equals("-") && !item.Aestimation.Equals("НП"))
                            {
                                firstZonaA += Int32.Parse(item.Aestimation); 
                                firstCountA++;   
                            }
                                                    
                        }
                        
                        if(!string.IsNullOrEmpty(item.Bestimation))
                        {
                            if(!item.Bestimation.Equals("-") && !item.Bestimation.Equals("НП"))
                            {
                                firstZonaB += Int32.Parse(item.Bestimation);
                                firstCountB++;
                            }
                            
                        }
                        
                    }
                    else if (item.Pindicator.ContainsAny(zonaSecond.ToArray()))
                    {
                        if(!string.IsNullOrEmpty(item.Aestimation))
                        {
                            if(!item.Aestimation.Equals("-") && !item.Aestimation.Equals("НП"))
                            {
                                secondZonaA += Int32.Parse(item.Aestimation);
                                secondCountA++;     
                            }
                                      
                        }
                        
                        if(!string.IsNullOrEmpty(item.Bestimation))
                        {
                            if(!item.Bestimation.Equals("-") && !item.Bestimation.Equals("НП"))
                            {
                                secondZonaB += Int32.Parse(item.Bestimation);
                                secondCountB++;
                            }
                            
                        }
                    }
                    else if(item.Pindicator.ContainsAny(zonaThird.ToArray()))
                    {
                        if(!string.IsNullOrEmpty(item.Aestimation))
                        {
                            if(!item.Aestimation.Equals("-") && !item.Aestimation.Equals("НП"))
                            {
                                thirdZonaA += Int32.Parse(item.Aestimation);
                                thirdCountA++;
                            }
                            
                        }
                        
                        if(!string.IsNullOrEmpty(item.Bestimation))
                        {
                            if(!item.Bestimation.Equals("-") && !item.Bestimation.Equals("НП"))
                            {
                                thirdZonaB += Int32.Parse(item.Bestimation);
                                thirdCountB++;
                            }
                            
                        }                      
                    }                  
                }

                double firstEstimationA = Math.Round(firstZonaA / (firstCountA * 1.0), 1);
                double secondEstimationA = Math.Round(secondZonaA / (secondCountB * 1.0), 1);
                double thirdEstimationA = Math.Round(thirdZonaA / (thirdCountA * 1.0), 1);

                double firstEstimationB = Math.Round(firstZonaB / (firstCountB * 1.0), 1);
                double secondEstimationB = Math.Round(secondZonaB / (secondCountB * 1.0), 1);
                double thirdEstimationB = Math.Round(thirdZonaB / (thirdCountB * 1.0), 1); //

                total_bl.Aeszona1 = double.IsNaN(firstEstimationA) | firstEstimationA == 0 ? null : (decimal)firstEstimationA;
                total_bl.Aeszona2 = double.IsNaN(secondEstimationA) | secondEstimationA == 0 ? null : (decimal)secondEstimationA;
                total_bl.Aeszona3 = double.IsNaN(thirdEstimationA) | thirdEstimationA == 0 ? null : (decimal)thirdEstimationA;

                total_bl.Beszona1 = double.IsNaN(firstEstimationB) | firstEstimationB == 0 ? null : (decimal)firstEstimationB;
                total_bl.Beszona2 = double.IsNaN(secondEstimationB) | secondEstimationB == 0? null : (decimal)secondEstimationB;
                total_bl.Beszona3 = double.IsNaN(thirdEstimationB) | thirdEstimationB == 0 ? null : (decimal)thirdEstimationB;
            }
        }
    }
}
