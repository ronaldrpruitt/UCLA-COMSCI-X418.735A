using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using static rpruitt_final.Enums;

namespace rpruitt_final
{
    internal static class ConsoleFoodRepository
    {
        private static string connStr = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        public static Order GetOrder(int id)
        {
            Order order = new Order();
            using (SqlConnection myConnection = new SqlConnection(connStr))
            {
                string query = "Select * from [dbo].[Order] where orderId=@orderId";
                SqlCommand oCmd = new SqlCommand(query, myConnection);
                oCmd.Parameters.AddWithValue("@orderId", id);
                myConnection.Open();
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        order.orderId = (int)oReader["orderId"];
                        order.orderStatusId = (OrderStatus)oReader["orderStatusId"];
                        order.OrderDate = (DateTime)oReader["orderDate"];
                        order.MenuItemId = (int)oReader["MenuItemId"];
                    }

                    myConnection.Close();
                }
            }
            return order;
        }

        public static List<Order> GetOrders()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                DataTable dt = new DataTable();
                List<Order> details = new List<Order>();

                SqlCommand cmd = new SqlCommand("SP_GetOrders", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    Order obj = new Order();

                    obj.orderId = (int)dr["OrderId"];
                    obj.MenuId = (Menu)dr["MenuId"];
                    obj.orderStatusId = (OrderStatus)dr["OrderStatusId"];
                    obj.OrderDate = (DateTime)dr["OrderDate"];

                    details.Add(obj);
                }

                return details;
            }
        }

        public static List<MenuItem> GetMenu(int menuId)
        {
            List<MenuItem> items = new List<MenuItem>();
            using (SqlConnection myConnection = new SqlConnection(connStr))
            {
                string query = "Select * from [dbo].[MenuItems] where MenuId=@menuId";
                SqlCommand oCmd = new SqlCommand(query, myConnection);
                oCmd.Parameters.AddWithValue("@menuId", menuId);
                myConnection.Open();
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        items.Add(new MenuItem
                        {
                            Id = (int)oReader["Id"],
                            Name = oReader["Name"].ToString(),
                            Price = (decimal)oReader["Price"],
                            PreparationTime = (TimeSpan)oReader["PreparationTime"],
                            MenuId = (Menu)oReader["MenuId"]
                        });
                    }

                    myConnection.Close();
                }
            }
            return items;
        }

        public static List<MenuItem> GetAllMenuItems()
        {
            List<MenuItem> items = new List<MenuItem>();
            using (SqlConnection myConnection = new SqlConnection(connStr))
            {
                string query = "Select * from [dbo].[MenuItems]";
                SqlCommand oCmd = new SqlCommand(query, myConnection);
                myConnection.Open();
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        items.Add(new MenuItem
                        {
                            Id = (int)oReader["Id"],
                            Name = oReader["Name"].ToString(),
                            Price = (decimal)oReader["Price"],
                            PreparationTime = (TimeSpan)oReader["PreparationTime"],
                            MenuId = (Menu)oReader["MenuId"]
                        });
                    }

                    myConnection.Close();
                }
            }
            return items;
        }

        public static MenuItem GetMenuItem(int itemId)
        {
            MenuItem items = new MenuItem();
            using (SqlConnection myConnection = new SqlConnection(connStr))
            {
                string query = "Select * from [dbo].[MenuItems] where Id=@itemId";
                SqlCommand oCmd = new SqlCommand(query, myConnection);
                oCmd.Parameters.AddWithValue("@itemId", itemId);
                myConnection.Open();
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        items.Id = (int)oReader["Id"];
                        items.Name = oReader["Name"].ToString();
                        items.Price = (decimal)oReader["Price"];
                        items.PreparationTime = (TimeSpan)oReader["PreparationTime"];
                        items.MenuId = (Menu)oReader["MenuId"];
                    }

                    myConnection.Close();
                }
            }
            return items;
        }

        public static int AddOrder(Order order)
        {
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                string query = "INSERT INTO [dbo].[Order] (OrderStatusId, OrderDate, OrderTotal, MenuItemId) VALUES (@orderStatusId, @orderDate, @orderTotal, @menuItemId)";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    // Add Parameters to Command Parameters collection
                    cmd.Parameters.AddWithValue("@orderStatusId", order.orderStatusId);
                    cmd.Parameters.AddWithValue("@orderDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@orderTotal", (int)order.OrderTotal);
                    cmd.Parameters.AddWithValue("@menuItemId", (int)order.MenuItemId);

                    connection.Open();
                    int result = cmd.ExecuteNonQuery();

                    if (result < 0)
                    {
                        Console.WriteLine("Error inserting data into Database!");
                    }
                    return result;
                }
            }
        }

        public static int UpdateOrderStatus(int orderStatus, int orderId)
        {
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                string query = "UPDATE [dbo].[Order] Set OrderStatusId = @orderStatusId Where OrderId=@id";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    // Add Parameters to Command Parameters collection
                    cmd.Parameters.AddWithValue("@orderStatusId", orderStatus);
                    cmd.Parameters.AddWithValue("@id", orderId);

                    connection.Open();
                    int result = cmd.ExecuteNonQuery();

                    if (result < 0)
                    {
                        Console.WriteLine("Error updating data into Database!");
                    }
                    return result;
                }
            }
        }
    }
}