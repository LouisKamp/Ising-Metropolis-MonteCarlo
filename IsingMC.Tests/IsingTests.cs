using System;
using Xunit;
using System.Linq;
using System.Collections.Generic;

public class IsingTests : IDisposable
{
    private int size = 5;
    private Ising iss;
    public IsingTests()
    {
        iss = new Ising(size);
    }

    public void Dispose()
    {
        //throw new NotImplementedException();
    }

    [Fact]
    public void MatrixIsFilledWithOnes()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Assert.Equal(1, iss.GetValue(i, j));
            }
        }
    }

    [Fact]
    public void CanGetValue()
    {
        Assert.Equal(1, iss.GetValue(1, 1));
    }

    [Fact]
    public void CanToggle()
    {
        var before = iss.GetValue(1, 1);
        iss.Toggle(1, 1);
        var after = iss.GetValue(1, 1);

        Assert.True(before == (-1) * after);
    }

    [Fact]
    public void GetNeighbors()
    {
        var listOne = new List<int>() { 1, 1, 1, 1 };
        Assert.Equal(listOne, iss.GetNeighbors(1, 1));

        var listTwo = new List<int>() { -1, 1, 1, 1 };
        iss.Toggle(0, 1);
        Assert.Equal(listTwo, iss.GetNeighbors(1, 1));

    }
    [Fact]
    public void CanGetNeighborsInConor()
    {
        iss.Toggle(size - 1, 0);
        iss.Toggle(0, size - 1);
        var listOne = new List<int>() { -1, 1, -1, 1 };
        Assert.Equal(listOne, iss.GetNeighbors(0, 0));

        var listTwo = new List<int>() { 1, -1, 1, -1 };
        Assert.Equal(listTwo, iss.GetNeighbors(4, 4));
    }
    [Fact]
    public void CanCalculatePointEnergy()
    {
        Assert.Equal(-4, iss.PointEnergy(1, 1));

        iss.Toggle(1, 1);

        Assert.Equal(4, iss.PointEnergy(1, 1));

        iss.Toggle(1, 2);
        Assert.Equal(2, iss.PointEnergy(1, 1));

        iss.Toggle(1, 0);
        Assert.Equal(0, iss.PointEnergy(1, 1));

        iss.Toggle(0, 1);
        Assert.Equal(-2, iss.PointEnergy(1, 1));

        iss.Toggle(2, 1);
        Assert.Equal(-4, iss.PointEnergy(1, 1));

    }

    [Fact]
    public void CanGetEnergy()
    {
        Assert.Equal(-100, iss.GetEnergy());
    }

    [Fact]
    public void CanGetSpin()
    {
        Assert.Equal(25, iss.GetSpin());
    }

    [Fact]
    public void CanCalculateEnergyChange()
    {

        var dE = iss.PointEnergyChange(1, 1);

        var Ei = iss.PointEnergy(1, 1);
        iss.Toggle(1, 1);
        var Ef = iss.PointEnergy(1, 1);

        Assert.Equal(Ef - Ei, dE);
    }
}
