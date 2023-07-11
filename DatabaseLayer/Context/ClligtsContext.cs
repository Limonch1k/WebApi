
using System.Data;
using AutoMapper;
using Oracle.ManagedDataAccess;
using Oracle.ManagedDataAccess.Client;
using GeneralObject.Constants;
using Microsoft.Extensions.Configuration;
using DB.TableModels;
using System;
using System.Collections.Generic;
using DB.DBModels;

namespace DBLayer.Context
{
    public class CligtsContext
    {
        private IConfiguration _configuration {get;set;}

        private IMapper _mapper{get;set;}

        private OracleConnection? connection {get;set;}

        private ColumnsName _columnName{get;set;}

        public CligtsContext(IConfiguration configuration, IMapper mapper, ColumnsName columnName)
        {
            _mapper = mapper;
            _configuration = configuration;
            _columnName = columnName;
        }

        ~CligtsContext()
        {
            try
            {
                connection.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }

        public void OpenConnection()
        {
            
            try
            {       
                string? constr = _configuration.GetConnectionString("OracleConnection");
                connection = new OracleConnection(constr);
                connection.Open();
            }
            catch(Exception e)
            {
                e.ToString();
            }
            
        }

        public void CloseConnection()
        {
            try
            {
                connection.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        
        public List<AverageTempDB> GetAverageTempToday(DateTime day)
        {
            //check format of variable
            //DateTime dateTime = DateTime.ParseExact(day, "yyyy-mm-dd", System.Globalization.CultureInfo.InvariantCulture);
            //create fetch

            string query = "SELECT " + _columnName.STATION_ID + "," + _columnName.DATE + ", " + _columnName.TEMP + ", " + _columnName.LOCALITY + 
            " FROM CLIGTS.NORM_DAY INNER JOIN CLIGTS.DCT_METEO ON NORM_DAY." + _columnName.STATION_ID + " = DCT_METEO.IND" + 
            " WHERE DATE_PERIOD = " + _columnName.DATE +" '" + day.ToString("yyyy-MM-dd") + "'" +
            " ORDER BY " + _columnName.STATION_ID + " DESC";

            //string query = "SELECT 5 FROM dual";

            OracleDataAdapter adp = new OracleDataAdapter
            (query, connection);
            //
            DataTable dt = new DataTable();
            //
            adp.Fill(dt);
            //
            var rows = dt.AsEnumerable();
            //       
            return _mapper.Map<IEnumerable<DataRow>,IList<AverageTempDB>>
            (rows, opt => 
                {
                    opt.Items.Add("station_id",_columnName.STATION_ID); 
                    opt.Items.Add("date", _columnName.DATE); 
                    opt.Items.Add("temp", _columnName.TEMP);
                    opt.Items.Add("locality", _columnName.LOCALITY);
                }
            ).ToList();
        } 

        public List<AverageTempDB> GetAverageTempByDays(DateTime dtAt, DateTime dtEnd)
        {        
            //запрос
            string query = "SELECT " + "SYNOP_DAY." + _columnName.STATION_ID + ", " + "SYNOP_DAY." + _columnName.DATE + ", " + "SYNOP_DAY." + _columnName.TEMP + ", " + "STATIONS." + _columnName.LOCALITY + 
            " FROM CLIGTS.SYNOP_DAY INNER JOIN CLIGTS.STATIONS ON SYNOP_DAY." + _columnName.STATION_ID + " = STATIONS.STATION_ID" + 
            " WHERE " + _columnName.DATE + " <= DATE '" + dtEnd.ToString("yyyy-MM-dd") + "' AND DATE_OBS >=  DATE '" + dtAt.ToString("yyyy-MM-dd") + "'" +
            " ORDER BY " + _columnName.STATION_ID +" DESC, " + _columnName.DATE +" DESC";
            
            OracleDataAdapter adp = new OracleDataAdapter
            (query, connection);
            //
            DataTable dt = new DataTable();
            //
            adp.Fill(dt);
            //
            var rows = dt.AsEnumerable();

            return _mapper.Map<IEnumerable<DataRow>,IList<AverageTempDB>>
            (rows, opt => 
                {
                    opt.Items.Add("station_id",_columnName.STATION_ID); 
                    opt.Items.Add("date", _columnName.DATE); 
                    opt.Items.Add("temp", _columnName.TEMP);
                    opt.Items.Add("locality", _columnName.LOCALITY);
                }
            ).ToList();
        }
    }
}