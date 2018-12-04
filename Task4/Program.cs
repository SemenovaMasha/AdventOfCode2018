using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Task4
{
    class Program
    {
        static void Main(string[] args)
        {
            System.IO.StreamReader file = new System.IO.StreamReader("../../input.txt");

            string line;
            List<Record> records = new List<Record>();
            
            while ((line = file.ReadLine()) != null)
            {
                Record record = new Record();

                Regex regex = new Regex(@"\[(.*?)\]");
                var v = regex.Match(line);
                string t = v.Groups[1].ToString();

                record.Time = DateTime.ParseExact(t, "yyyy-MM-dd HH:mm",
                    System.Globalization.CultureInfo.InvariantCulture);

                regex = new Regex(@"\] (\S+)");
                v = regex.Match(line);
                t = v.Groups[1].ToString();

                if (t == "falls")
                {
                    record.EventType = EventType.FallAsleep;
                }else if (t == "wakes")
                {
                    record.EventType = EventType.WakeUp;
                }
                else
                {
                    record.EventType = EventType.StartShift;

                    regex = new Regex(@"Guard #(.*)begins");
                    v = regex.Match(line);
                    t = v.Groups[1].ToString();

                    record.GuardId = Convert.ToInt32(t);

                }
                records.Add(record);

            }

            file.Close();

            records.Sort((x, y) =>
                x.Time.CompareTo(y.Time));


            List<Guard> guards = new List<Guard>();
            Guard currentGuard = new Guard(0);
            DateTime lastStartTime = DateTime.Today;
            foreach (Record record in records)
            {
                if (record.EventType == EventType.StartShift)
                {
                    Guard guard = guards.FirstOrDefault(g => g.id == record.GuardId);
                    if (guard == null)
                    {
                        guard = new Guard(record.GuardId);
                        guards.Add(guard);
                    }
                    currentGuard = guard;
                }
                else if(record.EventType == EventType.FallAsleep)
                {
                    lastStartTime = record.Time;
                }
                else
                {
                    currentGuard.asleepTimes.Add(record.Time.Minute - lastStartTime.Minute);

                    for (int time = lastStartTime.Minute; time < record.Time.Minute;time++)
                    {
                        currentGuard.sleepsInMinute[time]++;
                    }
                }
            }

            int maxFrequentMinute = guards[0].GetMostFrequentlySleepMinute();
            Guard guardWithMaxAsleepTime = guards[0];
            foreach (Guard guard in guards)
            {
                int currentMostFreq = guard.GetMostFrequentlySleepMinute();
                if (currentMostFreq > maxFrequentMinute)
                {
                    guardWithMaxAsleepTime = guard;
                    maxFrequentMinute = currentMostFreq;
                }
            }


            //int maxAsleepGuard = guards.Max(g => g.GetAsleepSum());
            //Guard guardWithMaxAsleepTime = guards.FirstOrDefault(g => g.GetAsleepSum() == maxAsleepGuard);

            int maxSleepMinute = guardWithMaxAsleepTime.GetMaxSleepMinute();

            Console.WriteLine(maxSleepMinute * guardWithMaxAsleepTime.id);

            Console.ReadKey();
        }
        
    }

    class Record
    {
        public int GuardId { get; set; }
        public DateTime Time { get; set; }
        public EventType EventType { get; set; }

    }
    enum EventType { StartShift, FallAsleep, WakeUp}

    class Guard
    {
        public int id;
        public List<int> asleepTimes;
        public int[] sleepsInMinute;

        public Guard(int id)
        {
            this.id = id;
            asleepTimes = new List<int>();
            sleepsInMinute= new int[60];
        }

        public int GetAsleepSum()
        {
            return asleepTimes.Sum();
        }

        public int GetMaxSleepMinute()
        {
            int maxMinute = 0;

            for (int i = 1; i < sleepsInMinute.Length; i++)
            {
                if (sleepsInMinute[i] > sleepsInMinute[maxMinute])
                {
                    maxMinute = i;
                }
            }

            return maxMinute;
        }
        public int GetMostFrequentlySleepMinute()
        {
            int maxFrequentMinute = sleepsInMinute[0];

            for (int i = 1; i < sleepsInMinute.Length; i++)
            {
                if (sleepsInMinute[i] > maxFrequentMinute)
                {
                    maxFrequentMinute = sleepsInMinute[i];
                }
            }

            return maxFrequentMinute;
        }
    }
}
