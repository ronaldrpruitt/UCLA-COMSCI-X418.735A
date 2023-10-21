using System;
using static rpruitt_final.Enums;

namespace rpruitt_final
{
    public class MenuItem : IComparable<MenuItem>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public TimeSpan PreparationTime { get; set; }

        public Menu MenuId { get; set; }

        public int CompareTo(MenuItem other)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return $"{Id,-10} {Name,-25} {Price.ToString("C"),-15} {MenuId,-18} {PreparationTime.TotalSeconds,-15}";
        }
    }
}