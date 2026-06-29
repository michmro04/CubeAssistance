using System;

namespace CubeAssistance
{
    class Program{

        static bool checkEnd(string c){
            if(c=="Y"){ 
                Console.WriteLine("THE END!");
                return true;
            }
            else if(c == "N"){
                return false;
            }
            else
            {
                Console.WriteLine("ERROR - bad value! [\"Y\"/\"N\" only]");
                return false;
            }
        }


        static void Main(string[] args){

            bool end = false;

            //main loop
            while (!end)
            {

                Console.WriteLine("Do you want to finish your cube session [Y/N]?");
                string input = Console.ReadLine().ToUpper();
                end = checkEnd(input);    
            }

        }
    }
}