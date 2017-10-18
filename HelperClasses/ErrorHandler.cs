using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace NivesBrelihPhotography.HelperClasses
{
    public enum ServerType
    {
        API,
        SERVER
    }
    
    public static class ErrorHandler
    {
        private static string apiErrorPath = Path.Combine(HttpContext.Current.Server.MapPath("~"),"Log/API/log.txt");
        public static void ServerError(ServerType type, Exception ex )
        {
            string text = String.Format("{0} ---> {1}", DateTime.Now.ToString("yyyy-MM-dd - HH:mm:ss"), ex.Message);
            if (type == ServerType.API)
            {
                using (TextWriter writer = new StreamWriter(apiErrorPath, true))
                {
                    writer.WriteLine(text);
                }

                
            }
        }
    }
}