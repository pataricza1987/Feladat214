using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feladat214
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Csúcs startCsúcs = new Csúcs(new KorongÁllapot());

            //GráfKereső kereső = new MélységiKeresés(startCsúcs, true);
            GráfKereső kereső = new BackTrack(startCsúcs, 10, true);
            kereső.megoldásKiírása(kereső.Keresés());

            Console.ReadLine();
        }
    }
}
