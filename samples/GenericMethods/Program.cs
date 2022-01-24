// See https://aka.ms/new-console-template for more information

using VariadicGenerics;
using System;
using System.Text;

Stuff stuff = new();
Console.WriteLine(Stuff.Concat(1, 2.56f, "aaa"));

class Stuff
{
    [InductionBaseOf("Concat")]
    public static StringBuilder ConcatBase() => new();

    [InductionTransitionOf("Concat")]
    public static StringBuilder ConcatTransition<T>(T value, StringBuilder folded)
        where T : class
    {
        if (typeof(T) == typeof(int))
            folded.Append((int) (object) value);
        else if (typeof(T) == typeof(float))
            folded.Append((float) (object) value);
        else
            folded.Append(value);
        return folded;
    }

    [InductionFinalizationOf("Concat")]
    public static string ConcatFinalize(StringBuilder sb) => sb.ToString();
}

class InductionBaseOfAttribute : Attribute
{
    public string Name { get; }
    public InductionBaseOfAttribute(string name) => Name = name;
}

class InductionTransitionOfAttribute : Attribute
{
    public string Name { get; }
    public InductionTransitionOfAttribute(string name) => Name = name;
}

class InductionFinalizationOfAttribute : Attribute
{
     public string Name { get; }
    public InductionFinalizationOfAttribute(string name) => Name = name;
}