using Microsoft.AspNetCore.Mvc.Razor;

namespace LocationExpander
{
    public class ViewLocationExpander: IViewLocationExpander 
    {

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations) {            
            string[] locations = new string[] { "/ViewLayer/View/{1}/{0}.cshtml"};
            return locations.Concat(viewLocations);    
        }


        public void PopulateValues(ViewLocationExpanderContext context) {
            //добавляются подобласти сдесь
            context.Values["customviewlocation"] = nameof(ViewLocationExpander);
        }
    }
}

