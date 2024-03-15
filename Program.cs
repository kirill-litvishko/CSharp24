using System;
using System.Security.Cryptography.X509Certificates;
namespace ProgTest1
{
    class CalendarEvent
    {
        public string EventName { get; set; }
        public string Day { get; set; }
        public CalendarEvent(string eventName, string day)
        {
            eventName = eventName;
            Day = day;
        }

        public void PrintEventDetails()
        {
            Console.WriteLine($"Event name:{EventName} in day {Day}");
        }

        class Program
        {
            static int variant(int student) => (student - 1) % 3 + 1;
            static void Main(string[] args)
            {
                Console.WriteLine($"Variant: {variant(14)}");

                List<string> DaysOfWeek = new List<string>();
                DaysOfWeek.Add("Monday");
                DaysOfWeek.Add("Tuesday");
                DaysOfWeek.Add("Wednesday");
                DaysOfWeek.Add("Chetver");
                DaysOfWeek.Add("Friday");
                DaysOfWeek.Add("Saturday");
                DaysOfWeek.Add("Nedila");


                CalendarEvent calendarEvent = new CalendarEvent("Test","Friday");
                int idx = 0;
                foreach (string day in DaysOfWeek)
                {
                    if (calendarEvent.Day == DaysOfWeek[idx])
                    {
                        calendarEvent.PrintEventDetails();
                    }
                    idx++;
                }
                
            }
        }
    }
}