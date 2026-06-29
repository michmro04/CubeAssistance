using System;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;

namespace CubeAssistance
{
    class Program{


        static void Main(string[] args){

            List<double> myList = new List<double>();
            Stopwatch stoper = new Stopwatch();
            bool isRunning = true;

            //Welcoming texts            
            Console.WriteLine("🎲----- Welcome in your CubeAssistance!! ----- 🎲");
            Console.WriteLine("\n#Instruction#:");
            Console.WriteLine("- press SPACEBAR to start the stoper and press again to stop");
            Console.WriteLine("- press 'Q' key after stop stoper to exit application.");
            Console.WriteLine("\nEnjoy your solves!!\n");

            //main loop
            while(isRunning)
            {
                var key = Console.ReadKey(true).Key;

                switch(key)
                {
                    case ConsoleKey.Spacebar:
                        stoper.Start();

                        if(Console.ReadKey().Key == ConsoleKey.Spacebar)
                            stoper.Stop();
                    
                        double timeSpan = stoper.Elapsed.TotalMilliseconds;
                        timeSpan /= 1000;
                        timeSpan = Math.Round(timeSpan, 3);
                        myList.Add(timeSpan);

                        Console.WriteLine("RunTime " + timeSpan + " s.");
                        stoper.Reset();


                        break;


                    case ConsoleKey.Q:
                        Console.WriteLine("The End!");
                        isRunning = false;
                        break;
                }
            }
        }
    }
}