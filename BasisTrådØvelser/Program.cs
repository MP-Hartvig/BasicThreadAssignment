using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BasisTrådØvelser
{
    internal class Program
    {
        // ØVELSE 0
        public void WorkThreadFunction(Thread t1, Thread t2)
        {
            Console.WriteLine(t1.Name);
            Console.WriteLine(t2.Name);
        }

        private static void EmptyThread() { }

        static void Main(string[] args)
        {
            Program pg = new Program();

            Thread thread1 = new Thread(EmptyThread);
            thread1.Name = "Thread1";
            thread1.Start();

            Thread thread2 = new Thread(EmptyThread);
            thread2.Name = "Thread2";
            thread2.Start();

            pg.WorkThreadFunction(thread1, thread2);

            Console.Read();
        }
    }

    // ØVELSE 1 OG 2
    internal class Program2
    {
        private static void Thread1Text()
        {
            Console.WriteLine("C#-trådning er nemt!");
        }

        private static void Thread2Text()
        {
            Console.WriteLine("Også med flere tråde");
        }

        static void Main2(string[] args)
        {
            for (int i = 0; i < 5; i++)
            {
                Thread thread1 = new Thread(Thread1Text);
                thread1.Start();

                Thread thread2 = new Thread(Thread2Text);
                thread2.Start();

                Thread.Sleep(1000);
            }
            Console.Read();
        }
    }

    // ØVELSE 3
    internal class Program3
    {
        public static int alarmCounter = 0;
        public static int temperature = 0;
        public static bool terminator = true;
        public static bool updated = false;

        private static int TempGenerator()
        {
            Random rand = new Random();
            return rand.Next(-20, 120);
        }

        private static void FinalTemp()
        {
            while (terminator == true)
            {
                temperature = TempGenerator();
                Console.WriteLine(temperature.ToString());
                updated = true;
                Thread.Sleep(500);
            }
        }

        private static void AlarmCount()
        {
            while (terminator == true)
            {
                if (alarmCounter > 3)
                {
                    terminator = false;
                }

                if (updated == true && (temperature < 0 || temperature > 100))
                {
                    alarmCounter++;
                    Console.WriteLine("Error count: " + alarmCounter);
                    updated = false;
                }
            }
        }

        static void Main3(string[] args)
        {
            Thread temperatureThread = new Thread(FinalTemp);
            temperatureThread.Start();

            Thread alarmThread = new Thread(AlarmCount);
            alarmThread.Start();

            while (true)
            {
                if (!temperatureThread.IsAlive)
                {
                    Console.WriteLine("Alarm-tråden er termineret");
                    break;
                }
                Thread.Sleep(3000);
            }
            Console.Read();
        }
    }

    // Øvelse 4
    internal class Program4
    {
        static char ch = '*';

        static bool terminator = false;

        private static void InputHandler()
        {
            while (terminator == false)
            {
                char holder;
                holder = Console.ReadKey().KeyChar;

                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    Console.WriteLine("");
                    ch = holder;
                }
            }            
        }

        private static void PrintThread()
        {
            while(terminator == false)
            {
                Console.Write(ch);
                Thread.Sleep(300);
            }            
        }

        static void Main4(string[] args)
        {
            Thread inputThread = new Thread(InputHandler);
            inputThread.Start();

            Thread printThread = new Thread(PrintThread);
            printThread.Start();

            while(terminator == false)
            {
                if (ch == 'x')
                {
                    terminator = true;
                }
            }
            Console.Read();
        }
    }
}
