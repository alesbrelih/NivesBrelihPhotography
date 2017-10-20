using NivesBrelihPhotography.DbContexts;
using NivesBrelihPhotography.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;

namespace NivesBrelihPhotography.HelperClasses.DatabaseCommunication
{
    public class TermsConditionsDatabase
    {
        public static TermsConditions GetTermsConditions(NbpContext db)
        {
            return db.TermsConditions.FirstOrDefault();

        }

        public async static Task SetTermsConditions(TermsConditions termsModel, NbpContext db)
        {
            // create 
            if(termsModel.Id == null)
            {
                db.TermsConditions.Add(termsModel);
                await db.SaveChangesAsync();

            } else
            {
                // update
                var termsDb = await db.TermsConditions.FindAsync(termsModel.Id);
                termsDb.Content = termsModel.Content;
                db.Entry(termsDb).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }
    }
}