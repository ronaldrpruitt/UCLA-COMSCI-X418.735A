using System;
using static rpruitt_final.Program;

namespace rpruitt_final
{
    public class Kitchen
    {
        public event WorkStarted started;

        public event WorkProgressing progressing;

        public event WorkCompleted completed;

        public void Cook(int orderId)
        {
            if (orderId == 0)
            {
                throw new Exception("Value must be greater than 0");
            }
            if (started != null) started();
            if (progressing != null) progressing(orderId);
            if (completed != null)
            {
                foreach (WorkCompleted wc in completed.GetInvocationList())
                {
                    wc.BeginInvoke(orderId, new AsyncCallback(OrderPickup), wc);
                }
            }
        }

        private void OrderPickup(IAsyncResult result)
        {
            WorkCompleted wc = (WorkCompleted)result.AsyncState;
            Order order = wc.EndInvoke(result);
            var item = ConsoleFoodRepository.GetMenuItem(order.MenuItemId);
            Console.WriteLine($"Order# {order.orderId} is now ready for pickup. Order Total is {item.Price.ToString("C")}");
            Console.WriteLine("Thank you for your order. Press any key to exit");
        }
    }
}