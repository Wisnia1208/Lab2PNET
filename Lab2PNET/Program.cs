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

    public void PitStopSynchroniczny()
    {
        // Uruchomienie ZnakStop w osobnym wątku
        var znakStopTask = Task.Run(() => ZnakStop());

        // Wymiana 4 kół
        for (int i = 1; i <= 4; i++)
        {
            WymianaKola(i).Wait();
        }

        // Tankowanie
        Tankowanie().Wait();

        // Ustawienie skrzydła
        UstawSkrzydlo().Wait();

        // Czyszczenie kasku
        WyczyscKask().Wait();

        // Zatrzymanie ZnakStop
        ZatrzymajZnakStop();
        znakStopTask.Wait(); // Czekaj na zakończenie zadania ZnakStop
    }

    public async Task PitStopAsynchroniczny()
    {
        // Uruchomienie ZnakStop w osobnym wątku
        var znakStopTask = Task.Run(() => ZnakStop());

        // Wymiana 4 kół
        for (int i = 1; i <= 4; i++)
        {
            await WymianaKola(i);
        }

        // Tankowanie
        await Tankowanie();

        // Ustawienie skrzydła
        await UstawSkrzydlo();

        // Czyszczenie kasku
        await WyczyscKask();

        // Zatrzymanie ZnakStop
        ZatrzymajZnakStop();
        await znakStopTask; // Czekaj na zakończenie zadania ZnakStop
    }

    public static async Task Main(string[] args)
    {
        Console.WriteLine("Hello World");

        SerwisPitStop serwis = new SerwisPitStop();

        // Wywołanie procedury PitStop synchronicznej
        serwis.PitStopSynchroniczny();

        // Wywołanie procedury PitStop asynchronicznej
        await serwis.PitStopAsynchroniczny();
    }
}
