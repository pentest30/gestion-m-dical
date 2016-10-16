using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;

namespace Gc.Core.Data
{
   public class ImportFromXls
    {

       public static bool ImporteListe(string fileName)
       {
         var dt = GetTable(fileName, "produit");
          
         
           var medicaments = (from row in dt.AsEnumerable()
                              select new
                              {
                                  Dci = string.IsNullOrWhiteSpace(row.Field<string>("NOM_DCI")) ? "" : row.Field<string>("NOM_DCI"),
                                  NumEnreg = string.IsNullOrWhiteSpace(row.Field<string>("NUM_ENR")) ? "" : row.Field<string>("NUM_ENR"),   
                                  Nom = string.IsNullOrWhiteSpace(row.Field<string>("NOM_COM")) ? "" : row.Field<string>("NOM_COM"),
                                  Dosage = string.IsNullOrWhiteSpace(row.Field<string>("DOSAGE")) ? "" : row.Field<string>("DOSAGE"),
                                  Condit = string.IsNullOrWhiteSpace(row.Field<string>("CONDIT")) ? "" : row.Field<string>("CONDIT"),
                                  Forme = string.IsNullOrWhiteSpace(row.Field<string>("LIB_FORME")) ? "" : row.Field<string>("LIB_FORME"),
                               
                              }).Distinct().AsEnumerable().ToArray();
           var dcis = medicaments.Select(x => x.Dci).Distinct();
           foreach (var d in dcis.Select(dci => new Dci(){Nom = dci}))
           {
               using (var db = new DbManager())
               {
                   db.Dcis.Add(d);
                   db.SaveChanges();
               }
           }
           using (var db =  new  DbManager())
           {
             
               var  list = new List<Medicament>();
               foreach (var medicament in medicaments)
               {
                   var l = new Medicament();
                   var firstOrDefault = db.Dcis.FirstOrDefault(x => x.Nom.Equals(medicament.Dci));
                   if (firstOrDefault != null)
                       l.DciId = firstOrDefault.Id;
                   l.Forme = medicament.Forme;l.Conditionnement = medicament.Condit;
                   l.NomCommercial = medicament.Nom;
                   l.Dose = medicament.Dosage;
                   l.NumeroEnregistrement = medicament.NumEnreg;
                  list.Add(l);
               }
               foreach (var medicament in list)
               {
                   try
                   {
                       db.Medicaments.Add(medicament);
                       db.SaveChanges();
                   }
                   catch (Exception exception)
                   {
                       continue;
                   }

                   
               }
              
           }
           
         
           return true;
       }
       private static DataTable GetTable(string filePath, string tableName)
       {
           string connection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath +
                               ";Persist Security Info=False;";
           OleDbConnection conn = new OleDbConnection(connection);
           var strSql = string.Format("SELECT * FROM {0}", tableName);
           OleDbCommand cmd = new OleDbCommand(strSql, conn);
           DataSet dataset = new DataSet();
           OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
           adapter.Fill(dataset);
           return dataset.Tables[0];
       }
    }
}
