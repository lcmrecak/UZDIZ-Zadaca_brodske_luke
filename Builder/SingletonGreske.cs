using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lcmrecak__zadaca_3.Uzorci
{
    public class SingletonGreske
    {
        int greskaBr = 0;

        static SingletonGreske greska = new SingletonGreske();
        private SingletonGreske()
        {

        }
        public static SingletonGreske getInstanceGreska()
        {
            return greska;  
        }

        public void povecajGresku()
        {
            greskaBr++;
            Console.Write(greskaBr.ToString() + ". greska: ");
        }

    }
}
