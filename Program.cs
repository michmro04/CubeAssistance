using System;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Linq;

namespace CubeAssistance
{
    class Program{

        static List<double> myList = new List<double>();
        public  static Stopwatch stoper = new Stopwatch();
        public static Random random = new Random();
        public static bool isRunning = true;
        public static int index = 1;
        public static char[] moves = ['U', 'D', 'R', 'L', 'F', 'B'];


        static double averageOfN(int n, List<double> list)
        {
            int capacity = list.Count();
            if(capacity < n) { return 0.0; }
            
           List<double>listOfN = list.TakeLast(n).ToList();

            //removing best and worst time 
            double min = listOfN.Min();
            double max = listOfN.Max();
        
            listOfN.Remove(min);
            listOfN.Remove(max);
        
            //calculating average
            double result = 0;
            foreach(double time in listOfN)
            {
                result+=time;
            }
            result /=(n-2);
            result = Math.Round(result, 3); 
            return result;
        }
        
        static void showStats()
        {
            double ao5 = averageOfN(5, myList);
            Console.WriteLine("Ao5=" + ao5);
            double ao12 = averageOfN(12, myList);
            Console.WriteLine("Ao12=" + ao12);
            double ao20 = averageOfN(20, myList);
            Console.WriteLine("Ao20=" + ao20);
        }

        static void generateScramble()
        {
            string scramble = "";
            int randomIndex = 0;
            int prevRandomIndex = -1;
            bool addPrim = false; 
            bool addTwo = false;

            for(int i=0; i<20; i++)
            {
                addPrim = random.Next(3) == 1;
                addTwo = random.Next(3) == 2;

                randomIndex = random.Next(moves.Length);
                if(randomIndex==prevRandomIndex) 
                    randomIndex = (randomIndex+2)%6;

                scramble += moves[randomIndex];
                if(addPrim) scramble += "'";
                if(addTwo && !addPrim) scramble += "2";
                
                scramble += " "; 
                prevRandomIndex = randomIndex;  
            }
            Console.WriteLine(scramble);
        }


        static void Main(string[] args){

            //Welcoming texts            
            Console.WriteLine("🎲 ----- Welcome in your CubeAssistance!! ----- 🎲");
            Console.WriteLine("\n# Instruction #:");
            Console.WriteLine("- press SPACEBAR to start the stoper and press again to stop");
            Console.WriteLine("- press 'Q' key after stop stoper to exit application.");
            Console.WriteLine("\nEnjoy your solves!!\n");

            //main loop
            while(isRunning)
            {
                Console.WriteLine("\nSolve no." + index);
                generateScramble();
                
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

                        Console.WriteLine("Time" + index + " = " + timeSpan + " s.");
                        stoper.Reset();

                        showStats();

                        break;


                    case ConsoleKey.Q:
                        Console.WriteLine("The End!");
                        isRunning = false;
                        break;
                }
                index++;
            }
        }
    }
}