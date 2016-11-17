using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NivesBrelihPhotography.Enums
{
    public class DbResults
    {

        public enum PhotoDb
        {
            Success,
            FileIsNotImage,
            NameAlreadyExist,
            OtherFailure,
        }

    }
}