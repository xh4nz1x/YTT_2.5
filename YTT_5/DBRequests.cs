using Npgsql;

namespace YTT_5;

public class DBRequests
{
    public static void GetTypeCarQuery()
    {
        var querySql = "SELECT * FROM type_car";
        using var cmd = new NpgsqlCommand(querySql, DBService.GetSqlConnection());
        using var reader = cmd.ExecuteReader();
        
        Console.WriteLine("----------");
        while (reader.Read())
        {
            Console.WriteLine($"id: {reader[0]} | Название: {reader[1]}");
        }
        Console.WriteLine("----------");
    }
    
    public static void GetDriverRightsCategoryQuery(int driver)
    {
        var querySql = "SELECT dr.first_name, dr.last_name, rc.name " +
                       "FROM driver_rights_category " +
                       "INNER JOIN driver dr on driver_rights_category.id_driver = dr.id " +
                       "INNER JOIN rights_category rc on rc.id = driver_rights_category.id_rights_category " +
                       $"WHERE dr.id = {driver};";
        
        
        string checkIdDriver = $"SELECT EXISTS(SELECT * FROM driver WHERE id = {driver})";
        using var cmdCheckDriver = new NpgsqlCommand(checkIdDriver, DBService.GetSqlConnection());
        if ((bool)cmdCheckDriver.ExecuteScalar())
        {
            using var cmd = new NpgsqlCommand(querySql, DBService.GetSqlConnection());
            using var reader = cmd.ExecuteReader();
            Console.WriteLine("----------");
            while (reader.Read())
            {
                Console.WriteLine($"Имя: {reader[0]} | Фамилия: {reader[1]} | Категория прав: {reader[2]}");
            }
            Console.WriteLine("----------");
        }
        else
        {
            Console.WriteLine("----------");
            Console.WriteLine("Ошибка: Данного водителя не существует!");
            Console.WriteLine("----------");
        }
    }
    
    public static void GetDriverQuery()
    {
        var querySql = "SELECT * FROM driver";
        using var cmd = new NpgsqlCommand(querySql, DBService.GetSqlConnection());
        using var reader = cmd.ExecuteReader();

        Console.WriteLine("----------");
        while (reader.Read())
        {
            Console.WriteLine($"id: {reader[0]} | Имя: {reader[1]} | Фамилия: {reader[2]} | Дата рождения: {reader[3]}");
        }
        Console.WriteLine("----------");
    }
    
    public static void GetCarsQuery()
    {
        var querySql = "SELECT * FROM car";
        using var cmd = new NpgsqlCommand(querySql, DBService.GetSqlConnection());
        using var reader = cmd.ExecuteReader();

        Console.WriteLine("----------");
        while (reader.Read())
        {
            Console.WriteLine($"id: {reader[0]} | id типа машины: {reader[1]} | Название машины: {reader[2]} | Автомобильный номер: {reader[3]} | Количество пассажиров: {reader[4]}");
        }
        Console.WriteLine("----------");
    }
    
    public static void GetRouteQuery()
    {
        var querySql = "SELECT * FROM route";
        using var cmd = new NpgsqlCommand(querySql, DBService.GetSqlConnection());
        using var reader = cmd.ExecuteReader();

        Console.WriteLine("----------");
        while (reader.Read())
        {
            Console.WriteLine($"id: {reader[0]} | id водителя: {reader[1]} | id машины: {reader[2]} | id маршрута: {reader[3]} | Количество пассажиров: {reader[4]}");
        }
        Console.WriteLine("----------");
    }
    
    public static void GetRightsCategoryQuery()
    {
        var querySql = "SELECT * FROM rights_category";
        using var cmd = new NpgsqlCommand(querySql, DBService.GetSqlConnection());
        using var reader = cmd.ExecuteReader();

        Console.WriteLine("----------");
        while (reader.Read())
        {
            Console.WriteLine($"id: {reader[0]} | Категория: {reader[1]}");
        }
        Console.WriteLine("----------");
    }
    
    public static void GetItineraryQuery()
    {
        var querySql = "SELECT * FROM itinerary";
        using var cmd = new NpgsqlCommand(querySql, DBService.GetSqlConnection());
        using var reader = cmd.ExecuteReader();

        Console.WriteLine("----------");
        while (reader.Read())
        {
            Console.WriteLine($"id: {reader[0]} | Название: {reader[1]}");
        }
        Console.WriteLine("----------");
    }
    
    public static void AddRightsCategoryQuery(string name)
    {
        var querySql = $"INSERT INTO rights_category(name) VALUES ('{name}')";
        using var cmd = new NpgsqlCommand(querySql, DBService.GetSqlConnection());
        cmd.ExecuteNonQuery();
        Console.WriteLine($"\n -----> Категория прав '{name}' успешна добавлена!");
    }
    
    public static void AddDriverRightsCategoryQuery(int driver, int rightsCategory)
    {
        var querySql = $"INSERT INTO driver_rights_category(id_driver, id_rights_category) VALUES ({driver}, {rightsCategory})";
        string checkRightsCategory = $"SELECT EXISTS(SELECT * FROM rights_category WHERE id = {rightsCategory})";
        using var cmdCheckRightsCategory = new NpgsqlCommand(checkRightsCategory, DBService.GetSqlConnection());
        if ((bool)cmdCheckRightsCategory.ExecuteScalar())
        {
            string checkIdDriver = $"SELECT EXISTS(SELECT * FROM driver WHERE id = {driver})";
            using var cmdCheckDriver = new NpgsqlCommand(checkIdDriver, DBService.GetSqlConnection());
            if ((bool)cmdCheckDriver.ExecuteScalar())
            {
                using var cmd = new NpgsqlCommand(querySql, DBService.GetSqlConnection());
                cmd.ExecuteNonQuery();
                Console.WriteLine($"\n -----> Категория прав водителя успешна добавлена!");
            }
            else
            {
                Console.WriteLine("----------");
                Console.WriteLine("Ошибка: Данного водителя не существует!");
                Console.WriteLine("----------");
            }
        }
        else
        {
            Console.WriteLine("----------");
            Console.WriteLine("Ошибка: Категория прав не существует!");
            Console.WriteLine("----------");
        }
        
    }

    public static void AddTypeCarQuery(string name)
    {
        var querySql = $"INSERT INTO type_car(name) VALUES ('{name}')";
        using var cmd = new NpgsqlCommand(querySql, DBService.GetSqlConnection());
        cmd.ExecuteNonQuery();
        Console.WriteLine($"\n -----> Тип машины '{name}' успешно добавлен!");
    }
    
    public static void AddDriverQuery(string firstName, string lastName, DateTime birthdate)
    {
        var querySql = $"INSERT INTO driver(first_name, last_name, birthdate) VALUES ('{firstName}', '{lastName}', '{birthdate}')";
        using var cmd = new NpgsqlCommand(querySql, DBService.GetSqlConnection());
        cmd.ExecuteNonQuery();
        Console.WriteLine($"\n -----> Водитель '{firstName} {lastName}' успешно добавлен!");
    }
    
    public static void AddRouteQuery (int idDriver, int idCar, int idItinerary, int numberPassengers)
    {
        var querySql = $"INSERT INTO route(id_driver, id_car, id_itinerary, number_passengers) VALUES ('{idDriver}', '{idCar}', '{idItinerary}', '{numberPassengers}')";

        string checkIdDriver = $"SELECT EXISTS(SELECT * FROM driver WHERE id = {idDriver})";
        using var cmdCheckIdDriver = new NpgsqlCommand(checkIdDriver, DBService.GetSqlConnection());
        if ((bool)cmdCheckIdDriver.ExecuteScalar())
        {
            string checkIdCar = $"SELECT EXISTS(SELECT * FROM car WHERE id = {idCar})";
            using var cmdCheckIdCar = new NpgsqlCommand(checkIdCar, DBService.GetSqlConnection());
            if ((bool)cmdCheckIdCar.ExecuteScalar())
            {
                string checkIdItinerary = $"SELECT EXISTS(SELECT * FROM itinerary WHERE id = {idItinerary})";
                using var cmdCheckIdItinerary = new NpgsqlCommand(checkIdItinerary, DBService.GetSqlConnection());
                if ((bool)cmdCheckIdItinerary.ExecuteScalar())
                {
                    using var cmd = new NpgsqlCommand(querySql, DBService.GetSqlConnection());
                    cmd.ExecuteNonQuery();
                    Console.WriteLine($"\n -----> Рейс успешно добавлен!");
                }
                else
                {
                    Console.WriteLine("----------");
                    Console.WriteLine("Ошибка: Данного маршрута не существует!");
                    Console.WriteLine("----------");
                }
            }
            else
            {
                Console.WriteLine("----------");
                Console.WriteLine("Ошибка: Данной машины не существует!");
                Console.WriteLine("----------");
            }
        }
        else
        {
            Console.WriteLine("----------");
            Console.WriteLine("Ошибка: Данного водителя не существует!");
            Console.WriteLine("----------");
        }
    }
    
    public static void AddCarQuery(int idCar, string nameCar, string stateNumber, int numberPassengers)
    {
        var querySql = $"INSERT INTO car(id_type_car, name, state_number, number_passengers) VALUES ('{idCar}', '{nameCar}', '{stateNumber}', '{numberPassengers}')";

        string checkIdCar = $"SELECT EXISTS(SELECT * FROM car WHERE id = {idCar})";
        using var cmdCheckIdCar = new NpgsqlCommand(checkIdCar, DBService.GetSqlConnection());
        if ((bool)cmdCheckIdCar.ExecuteScalar())
        {
            using var cmd = new NpgsqlCommand(querySql, DBService.GetSqlConnection());
            cmd.ExecuteNonQuery();
            Console.WriteLine($"\n -----> Машина '{nameCar}' успешно добавлена!");
        }
        else
        {
            Console.WriteLine("----------");
            Console.WriteLine("Ошибка: Данного типа машины не существует!");
            Console.WriteLine("----------");
        }
    }
    
    public static void AddItineraryQuery(string name)
    {
        var querySql = $"INSERT INTO itinerary(name) VALUES ('{name}')";
        using var cmd = new NpgsqlCommand(querySql, DBService.GetSqlConnection());
        cmd.ExecuteNonQuery();
        Console.WriteLine($"\n -----> Маршрут '{name}' успешно добавлен!");
    }
}
