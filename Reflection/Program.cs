/*Домашнее задание

Рефлексия и её применение
Цель:

Написать свой класс-сериализатор данных любого типа в формат CSV, сравнение его быстродействия с типовыми механизмами серализации.
Полезно для изучения возможностей Reflection, а может и для применения данного класса в будущем.

Описание/Пошаговая инструкция выполнения домашнего задания:

Основное задание:

    Написать сериализацию свойств или полей класса в строку
    Проверить на классе: class F { int i1, i2, i3, i4, i5; Get() => new F() { i1 = 1, i2 = 2, i3 = 3, i4 = 4, i5 = 5 }; }
Замерить время до и после вызова функции (для большей точности можно сериализацию сделать в цикле 100-100000 раз)
    Вывести в консоль полученную строку и разницу времен
    Отправить в чат полученное время с указанием среды разработки и количества итераций
    Замерить время еще раз и вывести в консоль сколько потребовалось времени на вывод текста в консоль
    Провести сериализацию с помощью каких-нибудь стандартных механизмов (например в JSON)
    И тоже посчитать время и прислать результат сравнения
    Написать десериализацию/загрузку данных из строки (ini/csv-файла) в экземпляр любого класса
    Замерить время на десериализацию
    Общий результат прислать в чат с преподавателем в системе в таком виде:
    Сериализуемый класс: class F { int i1, i2, i3, i4, i5; }
код сериализации-десериализации: ...
    количество замеров: 1000 итераций
    мой рефлекшен:
    Время на сериализацию = 100 мс
    Время на десериализацию = 100 мс
    стандартный механизм (NewtonsoftJson):
    Время на сериализацию = 100 мс
    Время на десериализацию = 100 мс
*/

using System.Diagnostics;
using Reflection;

class Program
{
    static void Main(string[] args)
    {
        // Создание экземпляра класса для сериализации
        var obj = new F().Get();
        int kol = 100000;

        // Замер времени для своего класса-сериализатора
        List<string> strCSV = new();
        var sw = Stopwatch.StartNew();
        for (int i = 0; i < kol; i++)
        {
            strCSV.Add(CsvConvert.Serialize(obj));
        }
        sw.Stop();
        Console.WriteLine($"Время выполнения CsvConvert.Serialize: {sw.ElapsedMilliseconds} мс");

        sw.Restart();
        foreach (var str in strCSV)
        {
            _ = CsvConvert.Deserialize<F>(str);
        }
        sw.Stop();
        strCSV.Clear();
        Console.WriteLine($"Время выполнения CsvConvert.Deserialize: {sw.ElapsedMilliseconds} мс");


        // Замер времени для встроенного механизма сериализации (например, Json.NET)
        sw.Restart();
        for (int i = 0; i < kol; i++)
        {
            strCSV.Add(Newtonsoft.Json.JsonConvert.SerializeObject(obj));
        }
        sw.Stop();
        Console.WriteLine($"Время выполнения Newtonsoft.Json.JsonConvert.SerializeObject: {sw.ElapsedMilliseconds} мс");

        sw.Restart();
        foreach (var str in strCSV)
        {
            _ = Newtonsoft.Json.JsonConvert.DeserializeObject<F>(str);
        }
        sw.Stop();
        strCSV.Clear();
        Console.WriteLine($"Время выполнения Newtonsoft.Json.JsonConvert.DeserializeObject: {sw.ElapsedMilliseconds} мс");


        // Замер времени для встроенного механизма сериализации (например, Json.NET)
        sw.Restart();
        for (int i = 0; i < kol; i++)
        {
            strCSV.Add(System.Text.Json.JsonSerializer.Serialize(obj));
        }
        sw.Stop();
        Console.WriteLine($"Время выполнения System.Text.Json.JsonSerializer.Serialize: {sw.ElapsedMilliseconds} мс");

        sw.Restart();
        foreach (var str in strCSV)
        {
            _ = System.Text.Json.JsonSerializer.Deserialize<F>(str);
        }
        sw.Stop();
        strCSV.Clear();
        Console.WriteLine($"Время выполнения System.Text.Json.JsonSerializer.Deserialize: {sw.ElapsedMilliseconds} мс");

    }
}