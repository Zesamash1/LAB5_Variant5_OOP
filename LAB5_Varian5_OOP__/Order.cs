using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB5_Varian5_OOP__
{
    // Абстрактний клас для загальних характеристик замовлення
    public abstract class Order
    {
        public string Name { get; set; } //  Назва замовлення (страва або напій)
        public decimal Price { get; set; } // Ціна
        public string DeliveryAddress { get; set; } // Адреса доставки
        public Order() { }

        // Перевантажений конструктор для ініціалізації замовлення
        public Order(string name, decimal price, string deliveryAddress = null)
        {
            Name = name;
            Price = price;
            DeliveryAddress = deliveryAddress;
        }
        // Абстрактний метод для виведення інформації про замовлення
        public abstract void DisplayInfo(); // Вивести інформацію
    }
}
