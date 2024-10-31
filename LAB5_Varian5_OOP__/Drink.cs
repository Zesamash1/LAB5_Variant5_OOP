using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB5_Varian5_OOP__
{
    // Клас Drink, успадковує Order та реалізує інтерфейси IDrinkOrder і IDeliverable
    public class Drink : Order, IDrinkOrder, IDeliverable
    {

        public Drink() { } // Конструктор без параметрів для створення екземпляра Drink
        // Конструктор з параметрами для ініціалізації назви, ціни та адреси доставки (адреса вважається необов'язкова)
        public Drink(string name, decimal price, string deliveryAddress = null) : base(name, price, deliveryAddress) { }

        public void ServeDrink()  // Метод для подачі напою
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Подання напою: {Name}.");
            Console.ResetColor();
        }

        public void DeliverOrder() // Метод для доставки напою за вказаною адресою
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Доставка напою '{Name}' за адресою: {DeliveryAddress}.");
            Console.ResetColor();
        }

        public override void DisplayInfo() // Перевизначений метод для виведення інформації про замовлення на напій
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Замовлення на напій: {Name}, Ціна: {Price:C}, Адреса доставки: {(DeliveryAddress ?? "немає")}");
            Console.ResetColor();
        }
    }
}
