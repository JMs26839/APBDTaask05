using Tutorial4.Models;

namespace Tutorial4.Database;

public class StaticData
{
    public static List<Animal> animals = new List<Animal>()
    {
        new Animal(),
        new Animal(),
        new Animal(),
        new Animal(),
    };

    public static void Add(Animal animal)
    {
       animals.Add(animal);
    }

    public static List<Visit> visits = new List<Visit>();
    public static void Add(Visit v)
    {
        visits.Add(v);
    }
}