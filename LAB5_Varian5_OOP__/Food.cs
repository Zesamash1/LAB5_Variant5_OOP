using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB5_Varian5_OOP__
{
    // Клас Food, успадковує Order та реалізує інтерфейси IFoodOrder і IDeliverable
    public class Food : Order, IFoodOrder, IDeliverable
    {
        // Конструктор без параметрів для створення екземпляра Food
        public Food() { }
        // Конструктор з параметрами для ініціалізації назви, ціни та адреси доставки (адреса необов'язкова)
        public Food(string name, decimal price, string deliveryAddress = null) : base(name, price, deliveryAddress) { }

        public void ServeFood()   // Метод для подання страви
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Подання страви: {Name}.");
            Console.ResetColor();
        }
        public void DeliverOrder()  // Метод для доставки страви за вказаною адресою
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Доставка страви '{Name}' за адресою {DeliveryAddress}.");
            Console.ResetColor();
        }

        public override void DisplayInfo()  // Перевизначений метод для виведення інформації про замовлення на страву
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Замовлення на страву: {Name}, Ціна: {Price:C}, Адреса доставки: {(DeliveryAddress ?? "немає")}");
            Console.ResetColor();
        }
    }
}
