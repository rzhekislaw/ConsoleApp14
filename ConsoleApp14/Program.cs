namespace ns
{
    public delegate void TimeLeft(int t);
    public delegate void TimeSpent(int t);
    class tClass
    {
        public static void Main()
        {
            Console.WriteLine("Press \'t\' to start timer, \'s\' to start stopwatch");
            var mode = Console.ReadLine();
            if (mode == "t")
            {            
                int s = 0;
                do
                {
                    Console.Clear();
                    Console.WriteLine("Enter seconds to countdown");
                    if (int.TryParse(Console.ReadLine(), out var input))
                    {
                        s = input;
                        break;
                    }
                    Console.WriteLine("Wrong input");
                }
                while (true);

                var timer = new Timer();

                timer.WriteTimeLeft += ConsoleWriteTimeLeft;

                Console.WriteLine("Press any key to start");
                Console.ReadLine();

                timer.Start(s);
            }
            else if (mode == "s")
            {
                var sw = new Stopwath();

                sw.WriteTimeSpent += ConsoleWriteTimeSpent;

                Console.WriteLine("Press any key to start");
                Console.ReadLine();

                sw.Start();
            }
            else
            {
                Console.WriteLine("GG");
                return;
            }
        }

        public static void ConsoleWriteTimeLeft(int t)
        {
            Console.Clear();
            if (t > 0)
            {
                Console.WriteLine($"Time left {t} s");
            }
            else
            {
                Console.WriteLine("Time is over");
            }
        }

        public static void ConsoleWriteTimeSpent(int t)
        {
            Console.Clear();
            Console.WriteLine($"Total time spent {t} s");
        }

        public class Timer
        {
            public event TimeLeft WriteTimeLeft;

            public void Start(int s)
            {
                while (s > 0)
                {
                    if (s > 1)
                    {
                        Thread.Sleep(1000);
                        s --;
                        WriteTimeLeft?.Invoke(s);
                    }
                    else
                    {
                        Thread.Sleep(1000);
                        s = 0;
                        WriteTimeLeft?.Invoke(s);
                    }
                }
            }
        }

        public class Stopwath
        {
            public event TimeSpent WriteTimeSpent;

            public void Start()
            {
                Console.WriteLine("Press any key to stop");
                int s = 0;
                bool stop = false;
                while (!stop)
                {
                    Task.Run(async () =>
                    {
                        var input = Console.ReadLine();
                        if (input == null || input != null)
                        {
                            WriteTimeSpent?.Invoke(s);
                            stop = true;
                        }
                    });

                    Task.Run(async () =>
                    {
                        Thread.Sleep(1000);
                        s++;
                        WriteTimeSpent?.Invoke(s);
                    });
                }
            }
        }
    }
}