using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace LAB5_Varian5_OOP__
{
    public class IWillHaveOrder      // Клас для управління списком замовлень
    {
        public List<Order> orders = new List<Order>(); // Список для зберігання замовлень

        public void AddOrder(Order order)  // Додати замовлення до списку
        {
            orders.Add(order);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Додано замовлення: {order.Name}.");
            Console.ResetColor();
        }
        public void CancelOrder(int orderNumber)   // Скасувати замовлення за номером
        {
            if (orderNumber > 0 && orderNumber <= orders.Count)
            {
                var canceledOrder = orders[orderNumber - 1];
                orders.RemoveAt(orderNumber - 1);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Замовлення '{canceledOrder.Name}' було скасоване.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Невірний номер замовлення.");
                Console.ResetColor();
            }
        }

        public void DisplayAllOrders()  // Вивести всі замовлення із нумерацією
        {
            if (orders.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Замовлень не знайдено.");
                Console.ResetColor();
                return;
            }

            for (int i = 0; i < orders.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {orders[i].Name}");
            }
        }
        public void ServeOrder(int orderNumber)   // Подати замовлення (страва або напій) та прибрати після цього зі списку
        {
            if (orderNumber > 0 && orderNumber <= orders.Count)
            {
                Order order = orders[orderNumber - 1];

                // Якщо замовлення - страва, подати страву
                if (order is IFoodOrder foodOrder)
                {
                    foodOrder.ServeFood();
                    orders.RemoveAt(orderNumber - 1); // Видаляємо зі списку
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Страва подана клієнту.");
                    Console.WriteLine("Видалення замовлення зі списку замовлень...");
                    Console.ResetColor();
                }
                // Якщо замовлення - напій, подати напій
                else if (order is IDrinkOrder drinkOrder)
                {
                    drinkOrder.ServeDrink();
                    orders.RemoveAt(orderNumber - 1); // Видаляємо зі списку
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Напій подано клієнту.");
                    Console.WriteLine("Видалення замовлення зі списку замовлень...");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Невірний номер замовлення.");
                Console.ResetColor();
            }
        }
        public void DeliverOrder(int orderNumber)    // Доставити замовлення
        {
            if (orderNumber > 0 && orderNumber <= orders.Count)
            {
                Order order = orders[orderNumber - 1];

                // Перевірка наявності адреси доставки
                if (string.IsNullOrWhiteSpace(order.DeliveryAddress) || order.DeliveryAddress == "Немає")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("У даного замовлення немає адреси доставки.");
                    Console.ResetColor();
                    return;
                }

                // Доставка замовлення
                if (order is IDeliverable deliverableOrder)
                {
                    deliverableOrder.DeliverOrder();
                    orders.RemoveAt(orderNumber - 1); // Видаляємо замовлення зі списку після доставки
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Клієнт отримав замовлення.");
                    Console.WriteLine("Видалення замовлення зі списку замовлень...");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Невірний номер замовлення.");
                Console.ResetColor();
            }
        }
       

        public void DisplayOrderDetailsByNumber(int orderNumber) // Вивести деталі замовлення за номером
        {
            if (orderNumber > 0 && orderNumber <= orders.Count)
            {
                Order order = orders[orderNumber - 1];

                // Виводимо деталі замовлення
                order.DisplayInfo();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Невірний номер замовлення.");
                Console.ResetColor();
            }
        }

        public void DisplayOrderDetailsByName(string name) // Вивести деталі замовлення за назвою
        {
            Order foundOrder = null;
            foreach (var order in orders)
            {
                if (string.Equals(order.Name, name, StringComparison.OrdinalIgnoreCase))
                {
                    foundOrder = order;
                    break;
                }
            }

            if (foundOrder != null)
            {
                foundOrder.DisplayInfo();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Замовлення з такою назвою не знайдено.");
                Console.ResetColor();
            }

        }

        public void SaveOrdersToFile(string fileName)  // Зберегти всі замовлення у файл у форматі XML
        {
            if (orders.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Немає замовлень для збереження.");
                Console.ResetColor();
                return;
            }
            // Створюємо серіалізатор для перетворення списку замовлень у XML
            XmlSerializer serializer = new XmlSerializer(typeof(List<Order>), new Type[] { typeof(Food), typeof(Drink) });
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                serializer.Serialize(writer, orders);
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Замовлення збережено у файл.");
            Console.ResetColor();
        }

        public void LoadOrdersFromFile(string fileName)  // Завантажити замовлення з файлу у форматі XML
        {
            if (!File.Exists(fileName))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" Файл не знайдено.");
                Console.ResetColor();
                return;
            }

            XmlSerializer serializer = new XmlSerializer(typeof(List<Order>), new Type[] { typeof(Food), typeof(Drink) });
            using (StreamReader reader = new StreamReader(fileName))
            {
                orders = (List<Order>)serializer.Deserialize(reader);
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Замовлення завантажено з файлу.");
            Console.ResetColor();
        }
    }
}
