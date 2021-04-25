using System;
using System.IO;
using System.Linq;
using System.Text;
using MathNet.Numerics.Statistics;

class Program
{
    static void Main(string[] args)
    {
        var size = 30;
        var N = size * size;

        var csv = new StringBuilder();
        csv.AppendLine("temp,mean_energy,energy_std,mean_spin,spin_std");
        var temps = Enumerable.Range(2, 400);

        foreach (var x in temps)
        {
            double temp = Convert.ToDouble(x) / 100;
            var iss = new Ising(size);
            var res = iss.MCSteps(temp, 150 * N);

            if (res.Item1.Any() && res.Item2.Any())
            {

                var mean_energy = res.Item1.Average() / N;
                var energy_std = res.Item1.StandardDeviation() / N;

                var mean_spin = res.Item2.Average() / N;
                var spin_std = res.Item2.StandardDeviation() / N;
                csv.AppendLine($"{temp},{mean_energy},{energy_std},{mean_spin},{spin_std}");
            }
        }
        File.WriteAllText("data.csv", csv.ToString());
    }
}

