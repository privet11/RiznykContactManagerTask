using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ContactManagerTask.Models
{
    public class CSV_Tools
    {
        public static DataTable ProcessCSV(string fileName)
        {
            string Feedback = string.Empty;
            string line = string.Empty;
            string[] strArray;
            DataTable dt = new DataTable();
            DataRow row;
            Regex r = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            StreamReader sr = new StreamReader(fileName);

            line = sr.ReadLine();
            strArray = r.Split(line);

            Array.ForEach(strArray, s => dt.Columns.Add(new DataColumn()));

            while ((line = sr.ReadLine()) != null)
            {
                row = dt.NewRow();

                row.ItemArray = r.Split(line);
                dt.Rows.Add(row);
            }

            sr.Dispose();

            return dt;

        }


    }
}