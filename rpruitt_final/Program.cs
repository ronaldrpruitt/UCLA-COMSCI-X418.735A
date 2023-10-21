using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using static rpruitt_final.Enums;

namespace rpruitt_final
{
    public class Program
    {
        public delegate Order ProcessOrder(int order);

        public delegate void WorkStarted();

        public delegate void WorkProgressing(int order);

        public delegate Order WorkCompleted(int order);

        private static void Main(string[] args)
        {
            Print("*****Welcome to Quarantine Cafe*****");
            Print("Here is our full menu sorted by Menu Type and Price\n");

            List<MenuItem> allItems = ConsoleFoodRepository.GetAllMenuItems();
            allItems.Sort(new MenuSortByMenuAndPrice());
            PrintMenuHeader();
            PrintList(allItems, false);
            Print($"\nTotal Menu Items: {allItems.Count()}", false);

            Print("Or if you prefer by Price\n");
            allItems.Sort(new MenuSortByPrice());
            PrintMenuHeader();
            PrintList(allItems, false);

            int menuSelection = getMenuSelection();
            Print($"You Selected {(Menu)menuSelection}. Getting Menu\n");

            PrintMenuHeader();
            var menu = ConsoleFoodRepository.GetMenu(menuSelection);
            PrintList(menu, false);
            Print($"Total {menu.FirstOrDefault().MenuId} Items: {menu.Count()}");
            MenuItem item = getItemSelection((Menu)menuSelection);
            Print($"You selected {item.Name}");

            Print("Please wait while we submit your order");
            var order = new Order { orderStatusId = Enums.OrderStatus.Pending, OrderDate = DateTime.Now, MenuItemId = item.Id };
            int orderId = ConsoleFoodRepository.AddOrder(order);
            Print($"Your Order has been submitted. Order# {orderId}, Order Status {order.orderStatusId}");

            var kitchen = new Kitchen();
            kitchen.started += new WorkStarted(KitchenStartedWork);
            kitchen.progressing += new WorkProgressing(KitchenWorkProcessing);
            kitchen.completed += new WorkCompleted(KitchenWorkCompleted);
            kitchen.Cook(orderId);

            Console.ReadKey();
        }

        private static int getMenuSelection()
        {
            bool isMenuSelectionValid = false;
            int result = 0;
            while (!isMenuSelectionValid)
            {
                Print($"Please select a menu.  Breakfast: {(int)Menu.Breakfast}, Lunch: {(int)Menu.Lunch}, Dinner: {(int)Menu.Dinner}");
                int.TryParse(Console.ReadLine(), out result);

                isMenuSelectionValid = Enum.IsDefined(typeof(Menu), result);
                if (!isMenuSelectionValid)
                {
                    Print("Invalid Menu. Please try again");
                }
            }
            return result;
        }

        private static MenuItem getItemSelection(Menu menuId)
        {
            bool isItemSelectionValid = false;
            int result = 0;
            while (!isItemSelectionValid)
            {
                Print("Please select a food item. Limit one item per order");
                bool isValidId = int.TryParse(Console.ReadLine(), out result);
                if (!isValidId)
                {
                    Print("Invalid Id");
                    continue;
                }

                var item = ConsoleFoodRepository.GetAllMenuItems().Where(n => n.MenuId == menuId && n.Id == result).SingleOrDefault();
                if (item == null)
                {
                    Print($"Id {result} not found on the {menuId} Menu. Please try again");
                }
                else
                    return item;
            }
            return null;
        }

        private static void Print(string str, bool needExtraLine = true)
        {
            if (needExtraLine)
                Console.WriteLine("\n");
            Console.WriteLine(str);
        }

        private static void PrintList(IEnumerable data, bool needExtraLine = true)
        {
            if (needExtraLine)
                Console.WriteLine("\n");
            foreach (var item in data)
            {
                Console.WriteLine(item.ToString());
            }
        }

        private static void KitchenStartedWork()
        {
            Console.WriteLine("The Chef has received your order....");
            Thread.Sleep(2000);
        }

        private static Order KitchenWorkCompleted(int orderId)
        {
            ConsoleFoodRepository.UpdateOrderStatus((int)OrderStatus.Ready, orderId);
            Console.WriteLine($"The Chef has finished cooking your order. Order status is now {OrderStatus.Ready}....");
            return ConsoleFoodRepository.GetOrder(orderId);
        }

        private static void KitchenWorkProcessing(int orderId)
        {
            var order = ConsoleFoodRepository.GetOrder(orderId);
            TimeSpan processingTime = ConsoleFoodRepository.GetMenuItem(order.MenuItemId).PreparationTime;
            ConsoleFoodRepository.UpdateOrderStatus((int)OrderStatus.Processing, order.orderId);
            Console.WriteLine("The Chef has started to cook your order....");
            Thread.Sleep(processingTime);
        }
        
        public static string PrintMenuHeader()
        {
            string str = $"{"Id",-10} {"Name",-25} {"Price",-15} {"Menu",-18} {"CookTime Seconds",-15}\n" +
                $"{"==",-10} {"=========",-25} {"========",-15} {"========",-18} {"================",-15}";
            Console.WriteLine(str);
            return str;
        }
    }
}