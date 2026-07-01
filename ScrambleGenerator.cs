using System;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Runtime.ConstrainedExecution;

namespace CubeAssistance
{

    public class ScrambleGenerator
    {
    private static char[] moves = ['U', 'D', 'R', 'L', 'F', 'B'];
    private static Random random = new Random();
    
        public string generateScramble()
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
            return scramble.ToString();
        }
    }
}