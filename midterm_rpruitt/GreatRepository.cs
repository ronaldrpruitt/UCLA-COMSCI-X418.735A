using midterm_rpruitt.models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace midterm_rpruitt
{
    internal class GreatRepository
    {
        private static string connStr = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        public static List<Report> GetReportData()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                DataTable dt = new DataTable();
                List<Report> details = new List<Report>();

                SqlCommand cmd = new SqlCommand("SP_AllReportDataGet", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    Report obj = new Report();

                    obj.EmployeeId = (int)dr["EmployeeId"];
                    obj.FirstName = dr["FirstName"].ToString().Trim();
                    obj.LastName = dr["LastName"].ToString().Trim();
                    obj.Birthday = (DateTime)dr["Birthday"];
                    obj.HireDate = (DateTime)dr["HireDate"];
                    obj.Title = (Enums.JobTitle)dr["TitleId"];
                    obj.MonthlySalary = (decimal)dr["MonthlySalary"];
                    obj.BonusRate = (decimal)dr["BonusRate"];
                    if (!dr.IsNull("Amount"))
                    {
                        obj.AllowanceAmount = (decimal)dr["Amount"];
                        obj.AllowanceId = (Enums.AllowanceType)dr["AllowanceId"];
                    }
                    if (!dr.IsNull("SaleAmount"))
                    {
                        obj.Sales = (decimal)dr["SaleAmount"];
                    }

                    details.Add(obj);
                }

                return details;
            }
        }

        public static decimal GetAllSales(DateTime startDate, DateTime endDate)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SP_TotalSalesGet", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@DateFrom", startDate));
                cmd.Parameters.Add(new SqlParameter("@DateTo", endDate));

                return (decimal)cmd.ExecuteScalar();

            }
        }
    }
}