using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2PNET
{
    using System;
    using System.Threading.Tasks;

    public class SerwisPitStop
    {
        private bool _znakStopAktywny = false;

        public async Task ZnakStop()
        {
            _znakStopAktywny = true;
            while (_znakStopAktywny)
            {
                Console.WriteLine("Znak stop aktywny, stan: OK");
                await Task.Delay(100);
            }
        }

        public void ZatrzymajZnakStop()
        {
            _znakStopAktywny = false;
        }

        public async Task WymianaKola(int numerKola)
        {
            Console.WriteLine($"Rozpoczęcie wymiany koła numer {numerKola}");
            await Task.Delay(2000);
            Console.WriteLine($"Zakończenie wymiany koła numer {numerKola}");
        }

        public async Task Tankowanie()
        {
            Console.WriteLine("Rozpoczęcie tankowania");
            await Task.Delay(5000);
            Console.WriteLine("Zakończenie tankowania");
        }

        public async Task UstawSkrzydlo()
        {
            Console.WriteLine("Rozpoczęcie ustawiania skrzydła");
            await Task.Delay(1000);
            Console.WriteLine("Zakończenie ustawiania skrzydła");
        }

        public async Task WyczyscKask()
        {
            Console.WriteLine("Rozpoczęcie czyszczenia kasku");
            await Task.Delay(500);
            Console.WriteLine("Zakończenie czyszczenia kasku");
        }
    }

}
