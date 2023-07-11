using BL.IServices;
using DBLayer.Context;
using BL.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using BL.BLModels;
using BusinessLayer.IServices;
using DB.TableModels;
using System.Reflection;
using Static.Service;
using GeneralObject.MyCustomAttribute;

namespace BL.Services
{
    public class NullDataServices<DataBL,ErroreModel> : INoDataServices<DataBL,ErroreModel> where DataBL : class where ErroreModel : NullDataTable, new()
    {
        private UserdbContext _user{get;set;}

        private IMapper _mapper {get;set;}
        
        public NullDataServices(UserdbContext user, IMapper mapper)
        {
            _user = user;
            _mapper = mapper;
        }

        public List<ErroreModel> CheckNullDataInList(List<DataBL> station_list, string[] param_list)
        {
            //Dictionary<int, string> error_list = new Dictionary<int, string>();
            List<ErroreModel> error_list = new List<ErroreModel>();

            foreach(var station in station_list)
            {
                var properties = station.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();

                foreach(var property in properties)
                {
                    if(param_list.Contains(property.Name) && property.GetValue(station) is null)
                    {
                        error_list.Add(new ErroreModel()
                        {
                            PunktId = (int?)typeof(DataBL).GetProperties().FirstOrDefault(p => p.GetCustomAttribute<StationIdAttribute>() is not null).GetValue(station),
                            param = property.Name,
                            Srok = (DateTime)typeof(DataBL).GetProperties().FirstOrDefault(p => p.GetCustomAttribute<DateObservationAttribute>() is not null).GetValue(station)
                        }) ;

                        /*error_list.Add(new ErroreModel()
                            {
                                PunktId = (int?)station.GetType().GetProperties()
                                    .Where(prop => 
                                    prop.Name.Equals("StationId") 
                                    || prop.Name.Equals("stationId")
                                    || prop.Name.Equals("PunktId")
                                    || prop.Name.Equals("punktId")
                                    || prop.Name.Equals("CityId")
                                    || prop.Name.Equals("cityId"))
                                    .FirstOrDefault()
                                    .GetValue(station),
                                param = property.Name,
                                Srok = (DateTime)station.GetType().GetProperties()
                                    .Where(prop => 
                                    prop.Name.Equals("DateObs") 
                                    
                                    || prop.Name.Equals("Srok"))
                                    .FirstOrDefault()
                                    .GetValue(station)
                            });*/
                    }
                }                      
            }
            foreach(var l in param_list)
            {
                Console.WriteLine(l);
            }
            return error_list;
        }

        public List<ErroreModel>  CheckCityModelingNullData(List<DataBL> city_list,  string[] param_list)
        {
            /*
            IN NEXT UPDATE;
            */

            throw new Exception();
        }

        public void PutErrorData(int UserId, List<ErroreModel> error_list)
        {
            //Task.Run( () => 
            //{
                var user_param_list = _user.ParamAccessRights.Where(param => param.UserId == UserId).Select(param => param.Param_Name).AsQueryable().ToList();

                var user_punkt_list = _user.PunktAccessRight.Where(punkt => punkt.UserId == UserId).Select(punkt => punkt.Punkt_Id).AsQueryable().ToList();
                
                List<ErroreModel> list = new List<ErroreModel>();
                
                if (user_param_list.Count() != 0 || user_punkt_list.Count() != 0)
                {

                    // проверка на то что бы в список добавились только те записи по станциям к которым у пользователя есть доступ
                    foreach(var punkt in user_punkt_list)
                    {
                        // проверка на то что бы в список добавились только те записи по станциям к которым у пользователя есть доступ
                        if(error_list.Select(e => e.PunktId).ToArray().Contains(punkt))
                        {
                            //проверка на записи по параметрам по которыс у пользователя есть доступ 
                            foreach(var param in user_param_list)
                            {
                                //проверка на записи по параметрам по которыс у пользователя есть доступ 
                                if (error_list.Select(e => e.param).ToArray().Contains(param))
                                {
                                    //ПРОВЕРКА СУЩЕСТВУЕТ ЛИ В БД ЗАПИСЬ ПО УКАЗАННОМУ СРОКУ // пункту// значению
                                    foreach (var errore in error_list) 
                                    {
                                        if (!_user.NullDataTables.Where(e => e.PunktId == errore.PunktId && e.param.Equals(errore.param) && e.UserId == UserId
                                        && e.Srok == errore.Srok)
                                        .Any())
                                        {
                                            var error = error_list.Where(c => c.PunktId == punkt && c.param.Equals(param));
                                            if (error is not null)
                                            {
                                                foreach (var e in error)
                                                {
                                                    e.UserId = UserId;
                                                    e.DateWrite = DateTime.Now;
                                                }
                                                list.AddRange(error);
                                            }
                                        }
                                    }
                                    
                                }
                            }
                        }
                    }
                }

                _user.NullDataTables.AddRange(list);
                Console.WriteLine(list.Count());
            //});
        }

        public async Task Save()
        {
            await _user.SaveChangesAsync();
        }

        public async Task Dispose()
        {
            await _user.DisposeAsync();
        }

        public void CalculateDiscont(int UserId, List<ErroreModel> error_list)
        {
            //Maybe in next update....
        }
    }
}
