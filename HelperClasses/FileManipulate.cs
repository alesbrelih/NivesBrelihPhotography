using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NivesBrelihPhotography.HelperClasses
{
    public class FileManipulate
    {
        //deletes photo from the server
        public static void DeletePhoto(string basedir,string filename)
        {
            try
            {
                //1. delete from min
                string minPath = Path.Combine(basedir, "MIN", filename);
                File.Delete(minPath);

                //2. delete from mid
                string midPath = Path.Combine(basedir, "MID", filename);
                File.Delete(midPath);

                //3 delete from org
                string orgPath = Path.Combine(basedir, "ORG", filename);
                File.Delete(orgPath);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
