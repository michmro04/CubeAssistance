using System;
using System.Collections.Generic;
using System.Linq;

namespace CubeAssistance
{
    public class SessionManager
    {
        private List<double> myList = new List<double>();
        
        private double averageOfN(int n, List<double> list)
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
        
        public double GetAo5() {return averageOfN(5, myList); }
        public double GetAo12() {return averageOfN(12, myList); }
        public double GetAo20() {return averageOfN(20, myList); }

        public void AddTime(double time)
        {
            myList.Add(time);
        }

        
    }
}