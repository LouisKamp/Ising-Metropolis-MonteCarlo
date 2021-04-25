using System;
using System.Collections.Generic;
using System.Linq;
public class Ising
{
    private int[,] Matrix;
    private int Size;
    public Ising(int size)
    {

        Matrix = new int[size, size];
        Size = size;

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Matrix[i, j] = 1;
            }
        }
    }
    public List<int> GetNeighbors(int x, int y)
    {
        var i = new int[] { -1, 1 };
        var list = new List<int>();
        var mod = (Size - 1);

        foreach (var neighborX in i)
        {

            list.Add(Matrix[(Size + x + neighborX) % Size, y]);
        }

        foreach (var neighborY in i)
        {
            list.Add(Matrix[x, (Size + y + neighborY) % Size]);
        }

        return list;
    }
    public void Toggle(int x, int y)
    {
        Matrix[x, y] = Matrix[x, y] * (-1);
    }
    public int GetValue(int x, int y)
    {
        return Matrix[x, y];
    }

    public int PointEnergy(int x, int y)
    {
        return -GetNeighbors(x, y).Aggregate((x, y) => x + y) * GetValue(x, y);
    }

    public int PointEnergyChange(int x, int y)
    {
        return 2 * GetValue(x, y) * GetNeighbors(x, y).Aggregate((x, y) => x + y);
    }

    public int GetEnergy()
    {
        var sum = 0;

        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                sum += PointEnergy(i, j);
            }
        }

        return sum / 2;
    }

    public int GetSpin()
    {
        var sum = 0;

        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                sum += GetValue(i, j);
            }
        }

        return sum;

    }

    public Tuple<List<double>, List<double>> MCSteps(double temp, int iter)
    {
        var energies = new List<double>();
        var spins = new List<double>();

        var E = GetEnergy();
        var S = GetSpin();

        var rand = new Random();

        for (int i = 0; i < iter; i++)
        {
            var x = rand.Next(0, Size);
            var y = rand.Next(0, Size);

            var dE = PointEnergyChange(x, y);

            var z = rand.NextDouble();

            if (0 < dE)
                if (z > Math.Exp(-dE / temp))
                    continue;

            Toggle(x, y);
            E += dE;
            S += 2 * GetValue(x, y);
            energies.Add(E);
            spins.Add(S);
        }

        return new Tuple<List<double>, List<double>>(energies, spins);
    }
}