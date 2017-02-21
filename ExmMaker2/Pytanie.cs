using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExmMaker2
{
    class Pytanie
    {
        public string trescPytania = "";

        public bool czyJednaOdpowiedz = false;
        public bool czyJestObraz = false;
        public bool czyPytaniaLosowo = false;

        public List<string> trescOdpowiedzi = new List<string>();
        public List<bool> ktorePoprawne = new List<bool>();


    }
}
