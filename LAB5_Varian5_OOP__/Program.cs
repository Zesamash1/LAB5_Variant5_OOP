using LAB5_Varian5_OOP__;
using System;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        // Ініціалізує об'єкт operations, який управляє замовленнями
        IWillHaveOrder operations = new IWillHaveOrder();

        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n--- Система керування доставкою замовлень у ресторані ---");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("1. Додати замовлення на страву");
            Console.WriteLine("2. Додати замовлення на напій");
            Console.WriteLine("3. Скасувати замовлення");
            Console.WriteLine("4. Показати всі замовлення");
            Console.WriteLine("5. Показати деталі замовлення за номером");
            Console.WriteLine("6. Показати деталі замовлення за назвою");
            Console.WriteLine("7. Доставити замовлення");
            Console.WriteLine("8. Подати замовлення");
            Console.WriteLine("9. Зберегти замовлення у файл");
            Console.WriteLine("10. Завантажити замовлення з файлу");
            Console.WriteLine("0. Вихід");
            Console.ResetColor();

            Console.Write("Оберіть опцію: ");
            // Перевіряє, чи введення є числом, і зберігає його у змінній choice
            if (!int.TryParse(Console.ReadLine(), out int choice))  
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Невірний формат введення. Будь ласка, введіть число.");
                Console.ResetColor();
                continue;
            }

            switch (choice)   // Вибір дії залежно від значення choice
            {
                // додавання страви
                case 1:
                    string foodName;
                    do
                    {
                        Console.Write("Введіть назву страви: ");
                        foodName = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(foodName))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Назва страви не може бути порожньою. Будь ласка, введіть назву.");
                            Console.ResetColor();
                        }
                    } while (string.IsNullOrWhiteSpace(foodName));

                    decimal foodPrice;
                    Console.Write("Введіть ціну: ");
                    while (!decimal.TryParse(Console.ReadLine(), out foodPrice) || foodPrice <= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Неправильне значення. Введіть коректну ціну більше нуля.");
                        Console.ResetColor();
                        Console.Write("Введіть ціну: ");
                    }

                    Console.Write("Введіть адресу доставки (натисніть Enter, щоб пропустити): ");
                    string foodAddress = Console.ReadLine();
                    foodAddress = string.IsNullOrWhiteSpace(foodAddress) ? null : foodAddress;

                    Food foodOrder = new Food(foodName, foodPrice, foodAddress);
                    operations.AddOrder(foodOrder);
                    break;

                    //Додавання напою
                case 2:
                    string drinkName;
                    do
                    {
                        Console.Write("Введіть назву напою: ");
                        drinkName = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(drinkName))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Назва напою не може бути порожньою. Будь ласка, введіть назву.");
                            Console.ResetColor();
                        }
                    } while (string.IsNullOrWhiteSpace(drinkName));

                    decimal drinkPrice;
                    Console.Write("Введіть ціну: ");
                    while (!decimal.TryParse(Console.ReadLine(), out drinkPrice) || drinkPrice <= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Неправильне значення. Введіть коректну ціну більше нуля.");
                        Console.ResetColor();
                        Console.Write("Введіть ціну: ");
                    }

                    Console.Write("Введіть адресу доставки (натисніть Enter, щоб пропустити): ");
                    string drinkAddress = Console.ReadLine();
                    drinkAddress = string.IsNullOrWhiteSpace(drinkAddress) ? null : drinkAddress;

                    Drink drinkOrder = new Drink(drinkName, drinkPrice, drinkAddress);
                    operations.AddOrder(drinkOrder);
                    break;
                case 3:
                    // Запитує номер замовлення для скасування і викликає метод для його видалення
                    Console.Write("Введіть номер замовлення для скасування: ");
                    if (int.TryParse(Console.ReadLine(), out int cancelOrderNumber) && cancelOrderNumber > 0)
                    {
                        operations.CancelOrder(cancelOrderNumber);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Невірний формат введення. Будь ласка, введіть правильний номер.");
                        Console.ResetColor();
                    }
                    break;
                case 4:
                    // Відображає список усіх замовлень
                    operations.DisplayAllOrders();
                    break;

                case 5:
                    // Запитує номер замовлення для відображення деталей
                    Console.Write("Введіть номер замовлення: ");
                    if (int.TryParse(Console.ReadLine(), out int orderNumber))
                    {
                        operations.DisplayOrderDetailsByNumber(orderNumber);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Невірний формат введення. Будь ласка, введіть число.");
                        Console.ResetColor();
                    }
                    break;

                case 6:
                    // Запитує назву замовлення для відображення деталей
                    Console.Write("Введіть назву замовлення: ");
                    string orderName = Console.ReadLine();
                    operations.DisplayOrderDetailsByName(orderName);
                    break;
                case 7:
                    // Запит номеру замовлення для доставки
                    Console.Write("Введіть номер замовлення для доставки: ");
                    if (int.TryParse(Console.ReadLine(), out int orderToSend) && orderToSend > 0)
                    {
                        operations.DeliverOrder(orderToSend);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Неправильний формат введення. Введіть позитивне число.");
                        Console.ResetColor();
                    }
                    break;
                case 8:
                    // Запит номеру замовлення для подачі в ресторані
                    Console.Write("Введіть номер замовлення для подачі в ресторані: ");
                    if (int.TryParse(Console.ReadLine(), out int serveOrderNumber) && serveOrderNumber > 0)
                    {
                        operations.ServeOrder(serveOrderNumber);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Неправильний формат введення. Введіть позитивне число.");
                        Console.ResetColor();
                    }
                    break;
               
                case 9:
                    // Запит назв файлу для збереження замовлень у файл
                    Console.Write("Введіть назву файлу: ");
                    string saveFile = Console.ReadLine();
                    operations.SaveOrdersToFile(saveFile);
                    break;

                case 10:
                    // Запит назви файлу для завантаження замовлень із файлу
                    Console.Write("Введіть назву файлу: ");
                    string loadFile = Console.ReadLine();
                    operations.LoadOrdersFromFile(loadFile);
                    break;

                // Вихід із програми
                case 0:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Вихід з програми...");
                    Console.ResetColor();
                    return;

                default:
                    // Повідомлення про помилку при невірному виборі опції
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Невірний вибір. Будь ласка, спробуйте ще раз.");
                    Console.ResetColor();
                    break;
            }
        }
    }
}