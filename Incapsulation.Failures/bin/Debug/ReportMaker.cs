using System;
using System.Collections.Generic;
using System.Linq;

namespace Incapsulation.Failures
{
    public enum FailtureType
    {
        UnexpectedShutdown,
        ShortNonReponding,
        HardwareFailtures,
        ConnectionProblems
    }

    public class Device
    {
        public int Id { get; }
        public string Name { get; }
        public Device(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
    public class Failture
    {
        public Device Device { get; }
        public FailtureType FailtureType { get; }
        public DateTime Date { get; }
        public Failture(Device device, FailtureType failtureType, DateTime date)
        {
            Device = device;
            FailtureType = failtureType;
            Date = date;
        }


    }
    public static class FailtureTypeExtensions
    {
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

            var resultDevices = devices.Select(d => new Device((int)d["DeviceId"], (string)d["Name"])).Where(d => deviceId.Contains(d.Id)).ToList();
            var failtures = new List<Failture>();
            for(int i=0;i<resultDevices.Count;i++)
            {
                int index = deviceId.ToList().IndexOf(resultDevices[i].Id);
                var type = (FailtureType)failureTypes[index];
                var date = new DateTime((int)times[index][2], (int)times[index][1], (int)times[index][0]);
                failtures.Add(new Failture(resultDevices[i], type, date));
            }
            return FindDevicesFailedBeforeDate(new DateTime(year, month, day), failtures );
        }
        public static List<string> FindDevicesFailedBeforeDate(DateTime date, IEnumerable<Failture> failtures)
            => failtures
            .Where(f => f.Date <= date && f.FailtureType.IsFailtureSerious())
            .Select(f => f.Device.Name).ToList();



    }
}
