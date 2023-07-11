

using System.Reflection;
using DatabaseLayer.ModelInterface;
using DB.DBModels;
using DB.TableModels;
using GeneralObject.DynamicGenerator;
using System.Linq.Dynamic.Core;

namespace Static.Repository_Old
{
    public static partial class StaticRepositoryFilter_old
    {
        public static IQueryable<TSource> ByDateFilter<TSource>(this IQueryable<TSource> que, DateTime startAt, DateTime endAt) where TSource : class, ISynopProperty
        {
            if (startAt == default(DateTime) && endAt == default(DateTime)) 
            {
                return que;
            }

            var type = que.ElementType;
            
            if (type == typeof(Synop))
            {
                return (que as IQueryable<Synop>).Where(c => c.DateObservation >= startAt && c.DateObservation <= endAt).AsQueryable() as IQueryable<TSource>;
                
            }
            if (type == typeof(MeasuringAm))
            {
                return (que as IQueryable<MeasuringAm>).Where(c => c.DateObservation >= startAt && c.DateObservation <= endAt).AsQueryable() as IQueryable<TSource>;
            }

            if (type == typeof(GroundDatum)) 
            {
                return (que as IQueryable<GroundDatum>).Where(c => c.DateObservation >= startAt && c.DateObservation <= endAt).AsQueryable() as IQueryable<TSource>;
            }

            return null;
        }

        public static IQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> que, string[] orderBy) where TSource : class, ISynopProperty
        {

            if (orderBy is null || orderBy.Length == 0) 
            {
                return que;
            }

            var type = que.ElementType;

            if (type == typeof(Synop))
            {
                string fields = "";

                foreach (var p in orderBy)
                {
                    fields += p + " desc,";
                }
                fields = fields[0..^1];
                return que.OrderBy(fields);
            }        

            if (type == typeof(MeasuringAm))
            {
                string fields = "";

                foreach (var p in orderBy)
                {
                    fields += p + " desc,";
                }
                fields = fields[0..^1];
                return que.OrderBy(fields);
            }

            if (type == typeof(GroundDatum)) 
            {
                string fields = "";

                foreach (var p in orderBy)
                {
                    fields += p + " desc,";
                }
                fields = fields[0..^1];
                return que.OrderBy(fields);
            }

            return null;
        }

        public static IQueryable<TSource> GetLastFilter<TSource>(this IQueryable<TSource> que) where TSource : class, ISynopProperty
        {
            DateTime startAt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, ((DateTime.Now.Hour / 3) * 3) - 3, 00, 00);

            var type = que.ElementType;

            if (type == typeof(Synop))
            {
                return (que as IQueryable<Synop>).Where(syn => syn.DateObservation >= startAt) as IQueryable<TSource>;
            }

            if (type == typeof(MeasuringAm))
            {
                return (que as IQueryable<MeasuringAm>).Where(syn => syn.DateWrite >= startAt) as IQueryable<TSource>;
            }

            if (type == typeof(GroundDatum)) 
            {
                return (que as IQueryable<GroundDatum>).Where(syn => syn.DateWrite >= startAt) as IQueryable<TSource>;
            }

            return null;
        }

        public static IQueryable<TSource> FieldListInFilter<TSource>(this IQueryable<TSource> que,string[] stationList) where TSource : class, ISynopProperty
        {
            if ((stationList is null) || stationList.Length == 0) 
            {
                return que;
            }

            var type = que.ElementType;

            if (type == typeof(Synop))
            {
                return (que as IQueryable<Synop>).Where(c =>  stationList.Contains(c.ResourceId.ToString())) as IQueryable<TSource>;
            }

            if (type == typeof(MeasuringAm))
            {
                return (que as IQueryable<MeasuringAm>).Where(c =>  stationList.Contains(c.ResourceId.ToString())) as IQueryable<TSource>;
            }

            if (type == typeof(GroundDatum)) 
            {
                return (que as IQueryable<GroundDatum>).Where(c => stationList.Contains(c.ResourceId.ToString())) as IQueryable<TSource>;
            }

            return null;
        }

        public static IQueryable<TSource> SelectParamField<TSource>(this IQueryable<TSource> que,string[] paramList) where TSource : class, ISynopProperty
        {
            if (paramList is null || paramList.Length == 0) 
            {
                return que;
            }

            var type = que.ElementType;

            if (type == typeof(Synop))
            {
                string fields = "new (ResourceId,DateObservation,DateWrite,";

                foreach(var p in paramList)
                {
                    fields += p + ",";
                }

               
                fields = fields[0 .. ^1];
                fields += ")";

                return (que as IQueryable<Synop>).Select<Synop>(fields) as IQueryable<TSource>;

            }

            if (type == typeof(MeasuringAm))
            {

                string fields = "new (ResourceId,DateObservation,DateWrite,";

                foreach(var p in paramList)
                {
                    fields += p + ",";
                }

                fields = fields[0 .. ^1];
                fields += ")";
                return (que as IQueryable<MeasuringAm>).Select<MeasuringAm>(fields) as IQueryable<TSource>;
            }

            if (type == typeof(GroundDatum)) 
            {
                string fields = "new (ResourceId,DateObservation,DateWrite,";

                foreach (var p in paramList) 
                {
                    fields += p + ",";
                }

                fields = fields[0 .. ^1];
                fields += ")";

                return (que as IQueryable<GroundDatum>).Select<GroundDatum>(fields) as IQueryable<TSource>;
            }
            

            return null;
        }

        public static IQueryable<TSource> ModelIdFilter<TSource>(this IQueryable<TSource> que, string model_id) where TSource : class, ISynopProperty 
        {
            if (model_id is null || model_id.Equals("")) 
            {
                return que;
            }

            var type = que.ElementType;

            if (type == typeof(Synop))
            {
                return (que as IQueryable<Synop>) as IQueryable<TSource>;
            }

            if (type == typeof(MeasuringAm))
            {
                return (que as IQueryable<MeasuringAm>) as IQueryable<TSource>;
            }

            if (type == typeof(GroundDatum)) 
            {
                int model = Int32.Parse(model_id);
                return (que as IQueryable<GroundDatum>).Where(c => c.ModelId == model) as IQueryable<TSource>;
            }

            return null;
        }
    }
}