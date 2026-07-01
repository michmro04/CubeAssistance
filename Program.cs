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
        public static bool isRunning = true;
        public static int index = 1;
        
        static private bool twoSecPenalty = false;
        static private bool dnfPenalty = false;
        static private bool eightSecWarningGiven = false;
        static private bool twelveSecWarningGiven = false;
        static private bool twoSecPenaltyWarningGiven = false;
        static private bool dnfPenaltyWarningGiven = false;

        static void Main(string[] args){

            var scrambleGenerator = new ScrambleGenerator();
            SessionManager session = new SessionManager();
            Console.CursorVisible = false;
            
            //Welcoming texts
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkGray;            
            Console.WriteLine("#---------- Welcome in your CubeAssistance!! ----------#");
            Console.WriteLine("|                                                      |");
            Console.WriteLine("|     # Instruction #:                                 |");
            Console.WriteLine("|- press SPACEBAR to start the stoper of inspection,   |");
            Console.WriteLine("|- press it again to start timer,                      |");
            Console.WriteLine("|- press it again to stop,                             |");
            Console.WriteLine("|- press 'Q' key after stop stoper to exit application.|");
            Console.WriteLine("#------------------!Enjoy your solves!-----------------#");
            Console.ResetColor();
           
            //main loop
            while(isRunning)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\nSolve no." + index);
                string newScramble = scrambleGenerator.generateScramble();
                Console.WriteLine(newScramble);
                
                var key = Console.ReadKey(true).Key;

                switch(key)
                {
                    case ConsoleKey.Spacebar:
                        Console.ResetColor();
                        Console.WriteLine("Inspection is running (15s)...");
                        inspectionTimer.Start();
                        do{
                            double inspectionTimeSpan = inspectionTimer.Elapsed.TotalMilliseconds;
                            inspectionTimeSpan /= 1000; 
                            if(inspectionTimeSpan>=8.0 && eightSecWarningGiven==false){
                                Console.Write(" --- 8 sec left --- ");
                                eightSecWarningGiven = true;
                            }
                            if(inspectionTimeSpan>=12.0 && twelveSecWarningGiven==false){ 
                                Console.SetCursorPosition(0, Console.CursorTop);
                                Console.Write(" --- 12 sec left ---         ");
                                twelveSecWarningGiven = true;
                            }
                            if(inspectionTimeSpan>=15.0 && twoSecPenaltyWarningGiven==false) {
                                Console.Write(new string(' ', Console.WindowWidth));
                                Console.SetCursorPosition(0, Console.CursorTop-1);
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write(" --- +2 sec penalty ---         ");
                                twoSecPenalty = true;
                                twoSecPenaltyWarningGiven = true;
                            }
                            if(inspectionTimeSpan>=17.0 && dnfPenaltyWarningGiven==false) {
                                Console.Write(new string(' ', Console.WindowWidth));
                                Console.SetCursorPosition(0, Console.CursorTop-1);
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(" --- DNF penalty ---           ");
                                dnfPenalty = true;
                                dnfPenaltyWarningGiven = true;
                            }  

                        }while(!Console.KeyAvailable);
                        
                        Console.SetCursorPosition(0, Console.CursorTop-1);
                        Console.Write(new string(' ', Console.WindowWidth));
                        Console.SetCursorPosition(0, Console.CursorTop);

                        if(dnfPenalty ){
                            Console.WriteLine("Penalty: DNF.");
                        }
                        else if (twoSecPenalty)
                        {
                            Console.WriteLine("Penalty: +2 sec.");
                        }
                        else
                        {
                            Console.WriteLine("No penlaty");
                        }

                        if(Console.ReadKey(true).Key == ConsoleKey.Spacebar){
                            inspectionTimer.Stop();                
                            stoper.Start();
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write("Timer is running!!       ");
                        }

                        if(Console.ReadKey().Key == ConsoleKey.Spacebar)
                            stoper.Stop();
                    
                        double timeSpan = stoper.Elapsed.TotalMilliseconds;
                        timeSpan /= 1000;

                        //adding penalties
                        if(twoSecPenalty) timeSpan+=2.0;
                        if(dnfPenalty) timeSpan = double.PositiveInfinity;
                        
                        timeSpan = Math.Round(timeSpan, 3);
                        Console.SetCursorPosition(0, Console.CursorTop);
                        
                        if(double.IsPositiveInfinity(timeSpan))
                            Console.WriteLine("Time" + index + " = DNF                   ");
                        else if(twoSecPenalty)
                            Console.WriteLine("Time" + index + " = " + timeSpan + "s (+2)                 ");
                        else 
                            Console.WriteLine("Time" + index + " = " + timeSpan + " s                ");
                        
                        session.AddTime(timeSpan);
                        stoper.Reset();
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("Ao5 = " +session.GetAo5() +"s");
                        Console.WriteLine("Ao12= " +session.GetAo12()+"s");
                        Console.WriteLine("Ao520= "+session.GetAo20()+"s");
                        break;

                    case ConsoleKey.Q:
                        Console.ResetColor();
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