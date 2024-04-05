/* УП Практическая работа 2.5
Задание. Автотранспортное предприятие имеет некоторый набор автобусов,
микроавтобусов и других автомобилей для оказания услуг по перевозке пассажиров по
заданным маршрутам и разовым заявкам. На предприятии имеется штат водителей,
способных осуществлять эти перевозки. Напишите консольное приложение, которое
позволит автоматизировать некоторые процесс данного предприятия.

Необходимо реализовать следующий функционал:
1. Просмотр и добавление типов машин;
2. Просмотр и добавление водителей и их прав;
3. Просмотр и добавление машин;
4. Просмотр и добавление маршрутов;
5. Просмотр и добавление рейсов; */

namespace YTT_5;

public class Program 
{
    public static void Main(string[] args)
    {
        bool MainMenuWork = true;
        while (MainMenuWork)
        {
            int MainMenu = StringToInt(AntiEmptyStringMenu("| Главное меню |\n1 - Просмотр информации \n2 - Добавление информации \n3 - Выход \n\nВыберите нужное действие: "));
            switch (MainMenu)
            {
                case 0:
                    break;
                case 1:
                    bool ViewMenuWork = true;
                    while (ViewMenuWork)
                    {
                        int ViewMenu = StringToInt(AntiEmptyStringMenu("| Меню просмотра информации |\n1 - Типы машин \n2 - Водители и права \n3 - Машины \n4 - Маршруты \n5 - Рейсы \n6 - Назад \n\nВыберите нужное действие: "));
                        switch (ViewMenu)
                        {
                            case 0:
                                break;
                            case 1:
                                DBRequests.GetTypeCarQuery();
                                break;
                            case 2:
                                bool RightsAndDriverMenuWork = true;
                                while (RightsAndDriverMenuWork)
                                {
                                    int RightsAndDriverMenu = StringToInt(AntiEmptyStringMenu("| Водители и их права |\n1 - Вывести всех водителей \n2 - Вывести все категории водительских прав \n3 - Вывести категорию водителя \n4 - Назад \n\nВыберите нужное действие: "));
                                    switch (RightsAndDriverMenu)
                                    {
                                        case 1:
                                            DBRequests.GetDriverQuery();
                                            break;
                                        case 2:
                                            DBRequests.GetRightsCategoryQuery();
                                            break;
                                        case 3:
                                            int idDriver2 = StringToInt(AntiEmptyString("Введите id водителя (int): "));
                                            DBRequests.GetDriverRightsCategoryQuery(idDriver2);
                                            break;
                                        case 4:
                                            RightsAndDriverMenuWork = false;
                                            break;
                                        default:
                                            Console.WriteLine("----------");
                                            Console.WriteLine("Неизвестное действие!");
                                            Console.WriteLine("----------");
                                            break;
                                    }
                                }
                                break;
                            case 3:
                                DBRequests.GetCarsQuery();
                                break;
                            case 4:
                                DBRequests.GetItineraryQuery();
                                break;
                            case 5:
                                DBRequests.GetRouteQuery();
                                break;
                            case 6:
                                ViewMenuWork = false;
                                break;
                            default:
                                Console.WriteLine("----------");
                                Console.WriteLine("Неизвестное действие!");
                                Console.WriteLine("----------");
                                break;
                        }
                    }
                    break;
                case 2:
                    bool AddMenuWork = true;
                    while (AddMenuWork)
                    {
                        int AddViewMenu = StringToInt(AntiEmptyStringMenu("| Меню добавления информации |\n1 - Типы машин \n2 - Водители и права \n3 - Машины \n4 - Маршруты \n5 - Рейсы \n6 - Назад \n\nВыберите нужное действие: "));
                        switch (AddViewMenu)
                        {
                            case 0:
                                break;
                            case 1:
                                string newTypeCar = AntiEmptyString("Введите новый тип машины (string): ");
                                DBRequests.AddTypeCarQuery(newTypeCar);
                                break;
                            case 2:
                                bool RightsAndDriverMenuWork = true;
                                while (RightsAndDriverMenuWork)
                                {
                                    int RightsAndDriverMenu = StringToInt(AntiEmptyStringMenu("| Водители и их права |\n1 - Добавить категорию прав \n2 - Добавить категорию прав водителю \n3 - Добавить водителя \n4 - Назад \n\nВыберите нужное действие: "));
                                    switch (RightsAndDriverMenu)
                                    {
                                        case 0:
                                            break;
                                        case 1:
                                            string newRightsCategory = AntiEmptyString("Введите новую категорию прав (string): ");
                                            DBRequests.AddRightsCategoryQuery(newRightsCategory);
                                            break;
                                        case 2:
                                            int idRightsCategory = StringToInt(AntiEmptyString("Введите id категории прав (int): "));
                                            int idDriver1 = StringToInt(AntiEmptyString("Введите id водителя (int): "));
                                            DBRequests.AddDriverRightsCategoryQuery(idDriver1, idRightsCategory);
                                            break;
                                        case 3:
                                            string name = AntiEmptyString("Введите имя водителя (string): ");
                                            string surname = AntiEmptyString("Введите фамилию водителя (string): ");
                                            string birthdateString = AntiEmptyString("Введите день рождения водителя (в формате: 01.01.2000): ");
                                            try
                                            {
                                                DateTime birthdate = DateTime.Parse(birthdateString);
                                                DBRequests.AddDriverQuery(name, surname, birthdate);
                                            }
                                            catch (FormatException)
                                            {
                                                Console.WriteLine();
                                                Console.WriteLine("----------");
                                                Console.WriteLine("Ошибка ввода! (невозможно преобразовать данное значение в DateTime)");
                                                Console.WriteLine("----------");
                                            }
                                            break;
                                        case 4:
                                            RightsAndDriverMenuWork = false;
                                            break;
                                        default:
                                            Console.WriteLine("----------");
                                            Console.WriteLine("Неизвестное действие!");
                                            Console.WriteLine("----------");
                                            break;
                                    }
                                }
                                break;
                            case 3:
                                int idCar1 = StringToInt(AntiEmptyString("Введите id типа машины (int): "));
                                string nameCar = AntiEmptyString("Введите название машины (string): ");
                                string stateNumber = AntiEmptyString("Введите автомобильный номер (string): ");
                                int numberPassengers1 = StringToInt(AntiEmptyString("Введите количество пассажиров (int): "));
                                DBRequests.AddCarQuery(idCar1, nameCar, stateNumber, numberPassengers1);
                                break;
                            case 4:
                                string nameItinerary = AntiEmptyString("Введите название маршрута (string): ");
                                DBRequests.AddItineraryQuery(nameItinerary);
                                break;
                            case 5:
                                int idDriver3 = StringToInt(AntiEmptyString("Введите id водителя (int): "));
                                int idCar2 = StringToInt(AntiEmptyString("Введите id машины (int): "));
                                int idItinerary = StringToInt(AntiEmptyString("Введите id маршрута (int): "));
                                int numberPassengers2 = StringToInt(AntiEmptyString("Введите количество пассажиров (int): "));
                                DBRequests.AddRouteQuery(idDriver3, idCar2, idItinerary, numberPassengers2);
                                break;
                            case 6:
                                AddMenuWork = false;
                                break;
                            default:
                                Console.WriteLine("----------");
                                Console.WriteLine("Неизвестное действие!");
                                Console.WriteLine("----------");
                                break;
                        }
                    }
                    break;
                case 3:
                    MainMenuWork = false;
                    break;
                default:
                    Console.WriteLine("----------");
                    Console.WriteLine("Неизвестное действие!");
                    Console.WriteLine("----------");
                    break;
            }
        }
    }
    
    public static int StringToInt (string inputStr)
    {
        int input = 0;
        try
        {
            input = Convert.ToInt32(inputStr);
        }
            
        catch (FormatException)
        {
            Console.WriteLine();
            Console.WriteLine("----------");
            Console.WriteLine("Ошибка ввода! (невозможно преобразовать данное значение в int)");
            Console.WriteLine("----------");
        }

        return input;
    }
    
    public static string AntiEmptyString (string inputText)
    {
        string input;
        while (true)
        {
            Console.Write(inputText);
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                break;
            }
        }

        return input;
    }
    
    public static string AntiEmptyStringMenu (string inputText)
    {
        string input;
        while (true)
        {
            Console.WriteLine();
            Console.Write(inputText);
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                break;
            }
        }

        return input;
    }
}
