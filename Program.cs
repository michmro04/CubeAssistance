using System;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Runtime.ConstrainedExecution;

namespace CubeAssistance
{
    class Program{

        static List<double> myList = new List<double>();
        public  static Stopwatch stoper = new Stopwatch();
        public static Stopwatch inspectionTimer = new Stopwatch();
        public static Random random = new Random();
        public static bool isRunning = true;
        public static int index = 1;
        public static char[] moves = ['U', 'D', 'R', 'L', 'F', 'B'];
        static private bool twoSecPenalty = false;
        static private bool dnfPenalty = false;
        static private bool eightSecWarningGiven = false;
        static private bool twelveSecWarningGiven = false;
        static private bool twoSecPenaltyWarningGiven = false;
        static private bool dnfPenaltyWarningGiven = false;


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
            StringBuilder scramble = new StringBuilder("");
            int randomIndex = 0;
            int prevRandomIndex = -1;
            bool addPrim = false; 
            bool addTwo = false;

            for(int i=0; i<20; i++)
            {
                addPrim = random.Next(3) == 1;
                addTwo = random.Next(3) == 2;

                do
                {
                randomIndex = random.Next(moves.Length);                    
                }while(randomIndex==prevRandomIndex);

                scramble.Append(moves[randomIndex]);
                if(addPrim) scramble.Append("'");
                if(addTwo && !addPrim) scramble.Append("2");
                
                scramble.Append(" ");
                prevRandomIndex = randomIndex;  
            }
            Console.WriteLine(scramble);
        }

        


        static void Main(string[] args){

            //Welcoming texts            
            Console.WriteLine("🎲 ----- Welcome in your CubeAssistance!! ----- 🎲");
            Console.WriteLine("\n# Instruction #:");
            Console.WriteLine("- press SPACEBAR to start the stoper of inspection,\n- press again to start timer,\n- press again to stop,");
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
                        Console.WriteLine("Inspection si running (15s)...");
                        inspectionTimer.Start();
                        do{
                            double inspectionTimeSpan = inspectionTimer.Elapsed.TotalMilliseconds;
                            inspectionTimeSpan /= 1000; 
                            if(inspectionTimeSpan>=8.0 && eightSecWarningGiven==false){
                                Console.WriteLine(" --- 8 sec left --- ");
                                eightSecWarningGiven = true;
                            }
                            if(inspectionTimeSpan>=12.0 && twelveSecWarningGiven==false){ 
                                Console.WriteLine(" --- 12 sec left --- ");
                                twelveSecWarningGiven = true;
                            }
                            if(inspectionTimeSpan>=15.0 && twoSecPenaltyWarningGiven==false) {
                                twoSecPenalty = true;
                                Console.WriteLine(" --- +2 sec penalty --- ");
                                twoSecPenaltyWarningGiven = true;
                            }
                            if(inspectionTimeSpan>=17.0 && dnfPenaltyWarningGiven==false) {
                                dnfPenalty = true;
                                Console.WriteLine(" --- DNF penalty --- ");
                                dnfPenaltyWarningGiven = true;
                            }  

                        }while(!Console.KeyAvailable);
                        
                        if(Console.ReadKey(true).Key == ConsoleKey.Spacebar){
                            inspectionTimer.Stop();                
                            stoper.Start();
                            Console.WriteLine("Timer is running!!");
                        }

                        if(Console.ReadKey().Key == ConsoleKey.Spacebar)
                            stoper.Stop();
                    
                        double timeSpan = stoper.Elapsed.TotalMilliseconds;
                        timeSpan /= 1000;

                        //adding penalties
                        if(twoSecPenalty) timeSpan+=2.0;
                        if(dnfPenalty) timeSpan = double.PositiveInfinity;
                        
                        timeSpan = Math.Round(timeSpan, 3);

                        if(double.IsPositiveInfinity(timeSpan))
                            Console.WriteLine("Time" + index + " = DNF.");
                        else if(twoSecPenalty)
                            Console.WriteLine("Time" + index + " = " + timeSpan + "s. (+2)");
                        else 
                            Console.WriteLine("Time" + index + " = " + timeSpan + " s.");
                        
                        myList.Add(timeSpan);
                        stoper.Reset();
                        showStats();

                        break;


                    case ConsoleKey.Q:
                        Console.WriteLine("The End!");
                        isRunning = false;
                        break;
                }
                index++;
                twoSecPenalty = false;
                dnfPenalty = false;
                eightSecWarningGiven = false;
                twelveSecWarningGiven = false;
                twoSecPenaltyWarningGiven = false;
                dnfPenaltyWarningGiven = false;
                inspectionTimer.Reset();
            }
        }
    }
}