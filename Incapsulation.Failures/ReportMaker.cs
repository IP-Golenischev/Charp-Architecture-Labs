using System;
using System.Collections.Generic;
using System.Linq;

namespace Incapsulation.Failures
{
    //Перечисление типов падения: неожиданное падеие, короткое время нет ответа, падение железа, проблемы с соединением
    public enum FailtureType
    {
        UnexpectedShutdown,
        ShortNonReponding,
        HardwareFailtures,
        ConnectionProblems
    }

    //класс, представляющий собой стройство
    public class Device
    {
        //свойство, доступное только для чтения, Id устройства
        public int Id { get; }
        //свойство, доступное только для чтения, имя устройства
        public string Name { get; }
        // Конструктор для создания объекта устрйства, принимающий Id и имя устройства
        public Device(int id, string name)
        {
            //Присваивание значений соответствующим полям
            Id = id;
            Name = name;
        }
    }
    //Класс представляющий собой падение устрйства
    public class Failture
    {
        //Свойство, доступное только для чтения, устройство
        public Device Device { get; }
        //Свойство, доступное только для чтения, тип ошибки
        public FailtureType FailtureType { get; }
        //Свойство, доступное только для чтения, дата ошибки
        public DateTime Date { get; }
        //Конструктор, создающий падение устройство с параметрами устройство, тип паделения и дата падения
        public Failture(Device device, FailtureType failtureType, DateTime date)
        {
            //Присваивание значений соответствующим полям
            Device = device;
            FailtureType = failtureType;
            Date = date;
        }
    }
    //Класс для создания метода расширения на тип FailtureType для проверки степени серьёзности расщирения
    public static class FailtureTypeExtensions
    {
        //Метод расширения на тип FailtureType, проверяющий, что ошибка серьёзная. Если Номер ошибки чётный, тогда она серьёзная.
        // Стрелочка пишется, потому что в полном описании метод содержит одно единственное выражение return, соответственно, чтобы не писать
        //фигурные скобки и return мы можем таким образом сократить код
        public static bool IsFailtureSerious(this FailtureType failture) => ((int)failture) % 2 == 0;
    }


    public class ReportMaker
    {
        /// <summary>
        /// </summary>
        /// <param name="day"></param>
        /// <param name="failureTypes">
        /// 0 for unexpected shutdown, 
        /// 1 for short non-responding, 
        /// 2 for hardware failures, 
        /// 3 for connection problems
        /// </param>
        /// <param name="deviceId"></param>
        /// <param name="times"></param>
        /// <param name="devices"></param>
        /// <returns></returns>
        public static List<string> FindDevicesFailedBeforeDateObsolete(
            int day,
            int month,
            int year,
            int[] failureTypes,
            int[] deviceId,
            object[][] times,
            List<Dictionary<string, object>> devices)
        {
            //Используя System.Linq выбираем набор всех имеющихся устройств, в которых происходили ошибки, то есть преобразуем
            //список словарей, каждый из которых представляет устройство, в нормальный объект типа устройство, и проверяем содержится
            // ли Id устройства в списке устройств, где проихошли ошибки

            var resultDevices = devices.Select(d => new Device((int)d["DeviceId"], (string)d["Name"])).Where(d => deviceId.Contains(d.Id)).ToList();
            //создаём список падений в указанных устройствах
            var failtures = new List<Failture>();
            // Проходим по всем устройствам
            for(int i=0;i<resultDevices.Count;i++)
            {
                // получаем индекс устройства в массиве идентификаторов устройств
                int index = deviceId.ToList().IndexOf(resultDevices[i].Id);
                //получаем тип аадения index-ного устройства
                var type = (FailtureType)failureTypes[index];
                //получаем дату падения index-ного устройства, в формате гож, месяц, день
                var date = new DateTime((int)times[index][2], (int)times[index][1], (int)times[index][0]);
                //Создаём падение и добавляем в коллекцию падений
                failtures.Add(new Failture(resultDevices[i], type, date));
            }
            // Вызываем наш переписанный метод
            return FindDevicesFailedBeforeDate(new DateTime(year, month, day), failtures );
        }
        //Метод, возвращающий названия устройств, где были паделения до указанной даты, и эти падения были серьёзными. Из полученной
        //коллекции падений с помощью LINQ получаем падения, которые были до указанной даты и эти падения были серьёзными,
        //в потом из полученной коллекции получеем названия устройств и приводим его в список
        public static List<string> FindDevicesFailedBeforeDate(DateTime date, IEnumerable<Failture> failtures)
            => failtures
            .Where(f => f.Date <= date && f.FailtureType.IsFailtureSerious())
            .Select(f => f.Device.Name).ToList();



    }
}
