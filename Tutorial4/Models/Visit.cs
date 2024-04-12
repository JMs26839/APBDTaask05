namespace Tutorial4.Models;

public class Visit
{
    // date of visit
    //     animal
    // description of the visit price of the visit
    
    public DateTime Date { get; set; }
    public Animal Animal { get; set; }
    public string Description { get; set; }
    public Double price { get; set; }

    public Visit(Animal animal)
    {
        this.Animal = animal;
    }
}