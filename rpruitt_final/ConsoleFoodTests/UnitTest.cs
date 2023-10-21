using Microsoft.VisualStudio.TestTools.UnitTesting;
using rpruitt_final;
using System;
using System.Collections.Generic;

namespace ConsoleFoodTests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        [ExpectedException(typeof(Exception), "Value must be greater than 0")]
        public void Kitchen_Cook_WhenInvalidOrderId_ShouldThrowError()
        {
            var kitchen = new Kitchen();

            kitchen.Cook(0);
        }

        [TestMethod]
        public void MenuItem_StringOverride_ShouldOverrideAndFormatString()
        {
            var menu = new MenuItem { Id = 1, Name = "test", MenuId = Enums.Menu.Breakfast, PreparationTime = TimeSpan.Zero, Price = 1 };

            string expectedString = $"{menu.Id,-10} {menu.Name,-25} {menu.Price.ToString("C"),-15} {menu.MenuId,-18} {menu.PreparationTime.TotalSeconds,-15}";

            Assert.AreEqual(expectedString, menu.ToString());
        }

        [TestMethod]
        public void MenuSortByMenuAndPrice_ShouldSortByMenuAndPrice()
        {
            int[] expectedIdOrder = new int[] { 3, 2, 1 };
            var menuItems = new List<MenuItem>
            {
                new MenuItem { Id = 3, Name = "test", MenuId = Enums.Menu.Breakfast, PreparationTime = TimeSpan.Zero, Price = 2 },
                new MenuItem { Id = 1, Name = "test", MenuId = Enums.Menu.Lunch, PreparationTime = TimeSpan.Zero, Price = 4 },
                new MenuItem { Id = 2, Name = "test", MenuId = Enums.Menu.Breakfast, PreparationTime = TimeSpan.Zero, Price = 2 }
            };

            menuItems.Sort(new MenuSortByMenuAndPrice());

            int index = 0;
            foreach (var item in menuItems)
            {
                Assert.AreEqual(expectedIdOrder[index], item.Id);
                index++;
            }
        }

        [TestMethod]
        public void MenuSortByPrice_ShouldSortByPrice()
        {
            int[] expectedIdOrder = new int[] { 2, 3, 1 };
            var menuItems = new List<MenuItem>
            {
                new MenuItem { Id = 3, Name = "test", MenuId = Enums.Menu.Breakfast, PreparationTime = TimeSpan.Zero, Price = 2 },
                new MenuItem { Id = 1, Name = "test", MenuId = Enums.Menu.Lunch, PreparationTime = TimeSpan.Zero, Price = 4 },
                new MenuItem { Id = 2, Name = "test", MenuId = Enums.Menu.Breakfast, PreparationTime = TimeSpan.Zero, Price = 1 }
            };

            menuItems.Sort(new MenuSortByPrice());

            int index = 0;
            foreach (var item in menuItems)
            {
                Assert.AreEqual(expectedIdOrder[index], item.Id);
                index++;
            }
        }

        [TestMethod]
        public void Program_PrintMenuHeaderShouldReturnProperHeaderString()
        {
            string expectedStr = $"{"Id",-10} {"Name",-25} {"Price",-15} {"Menu",-18} {"CookTime Seconds",-15}\n" +
                $"{"==",-10} {"=========",-25} {"========",-15} {"========",-18} {"================",-15}";
            string actual = Program.PrintMenuHeader();

            Assert.AreEqual(expectedStr, actual);
        }
    }
}