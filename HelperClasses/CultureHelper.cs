using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NivesBrelihPhotography.HelperClasses
{
    public class CultureHelper
    {
        //all valid cultures
        private static readonly List<string> _validCultures = new List<string>()
        {
            "sl-SI","en-US","en-AU","en-NZ","en-za","en-tt"

        };
        //implemented cultures
        private static readonly List<string> _implementedCultures = new List<string>()
        {
            "en-US","sl-SI"
        }; 


        /// <summary>
        /// method that returns cultureName, needed when user manually changes culture in cookies
        /// </summary>
        /// <param name="cultureName">culture name to be checked if it is correct</param>
        /// <returns></returns>
        public static string GetCultureName(string cultureName)
        {
            //parameter is null or empty
            if (string.IsNullOrEmpty(cultureName))
            {
                return ReturnDefaultCulture();
            }

            //if it doesnt exist under valid cultures (all regions inside culture) return default culture
            if (_validCultures.Count(x => x.Equals(cultureName, StringComparison.InvariantCultureIgnoreCase)) == 0)
            {
                return ReturnDefaultCulture();
            }

            //if it is inside implemented cultures return culture name
            if (_implementedCultures.Count(x => x.Equals(cultureName, StringComparison.InvariantCultureIgnoreCase)) > 0)
            {
                return cultureName;
            }

            //find if any neutral culture matches implemented cultures
            foreach (var c in _implementedCultures)
            {
                if (c.StartsWith(ReturnNeutralCulture(cultureName)))
                {
                    return c;
                }
            }

            //if everything fails return default culture
            return ReturnDefaultCulture();
        }

        //returns default culture, first in list
        private static string ReturnDefaultCulture()
        {
            return _implementedCultures[0];
        }

        //return neutral culture (first part of xx-xx code)
        private static string ReturnNeutralCulture(string cultureName)
        {
            //already neutral culture inside parameters
            if (!cultureName.Contains('-'))
            {
                return cultureName;
            }
            //get neutral part from normal culture
            return cultureName.Split('-')[0];
        }

    }
}
