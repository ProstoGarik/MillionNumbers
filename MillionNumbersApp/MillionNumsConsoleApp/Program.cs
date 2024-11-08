using MillionNumbersClassLib;
using System.Diagnostics;

NumberList numberList = new NumberList();
Stopwatch stopWatch = new Stopwatch();
int maxProcCount = Environment.ProcessorCount;

for (int i = 100000000; i <= 500000000; i += 100000000)
{
    stopWatch.Start();
    numberList.InitializeList(i);
    stopWatch.Stop();
    Console.WriteLine("\nСписок на " + i + " элементов создан за " + stopWatch.Elapsed.TotalSeconds + " секунд");
    stopWatch.Reset();

    stopWatch.Start();
    Console.WriteLine("\nСумма: " + numberList.GetSumBasic());
    stopWatch.Stop();
    Console.WriteLine("Последовательное суммирование заняло " + stopWatch.Elapsed.TotalSeconds + " секунд\n\n");
    stopWatch.Reset();

    for (int p = 2; p <= maxProcCount; p++)
    {
        stopWatch.Start();
        Console.WriteLine("\nСумма: " + numberList.GetSumPar(p));
        stopWatch.Stop();
        Console.WriteLine("Используется " + p + " потоков\nПараллельное суммирование заняло " + stopWatch.Elapsed.TotalSeconds + " секунд");
        stopWatch.Reset();
    }
}

