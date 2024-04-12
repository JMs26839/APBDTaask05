using Microsoft.AspNetCore.Http.HttpResults;
using Tutorial4.Database;
using Tutorial4.Models;

namespace Tutorial4.Endpoints;

public static class AnimalEndpoints
{
    public static void MapAnimalEndpoints(this WebApplication app)

    {

        //List from staatic data:
        var animalList = StaticData.animals;
        //FROM CLASSESS
        // var animalList = new List<Animal>();
        // app.MapGet("/animals-minimalapi/{id}", (int id) =>
        // {
        //     // process data
        //     if (id != 1)
        //     {
        //         return Results.NotFound();
        //     }
        //
        //     return Results.Ok();
        // });
        //
        //1.. get List of animals
        app.MapGet("/animals-miniamalApiList", () => { return Results.Ok(animalList); });

        //2.. aadd animaal to list
        app.MapPost("/animals-add", (Animal animal) =>
        {
            animalList.Add(animal);
            return Results.StatusCode(StatusCodes.Status201Created);
        });

        //get by id
        app.MapGet("/animals-retriveId/{id:int}", (int id) =>
        {

            var animal = StaticData.animals.FirstOrDefault(a => a.Id == id);
            return animal == null ? Results.NotFound($"animal with id{id} not found") : Results.Ok(animalList);
        });

        app.MapPut("/animals-ModifyAnimal/{id:int}", (int id, Animal UpdatedAnimal) =>
        {
            var animalToedit = animalList.Find(a => a.Id == id);
            if (animalToedit == null)
            {
                return Results.NotFound($"animal with id {id} not found");
            }

            animalToedit.FirstName = UpdatedAnimal.FirstName;
            animalToedit.Weight = UpdatedAnimal.Weight;
            animalToedit.Caategory = UpdatedAnimal.Caategory;
            animalToedit.FullColor = UpdatedAnimal.FullColor;
            return Results.Ok($"modified animal{id}");

        });



        app.MapDelete("/animals-deleteAniamal/{id:int}", (int id) =>
        {
            var animal = animalList.FirstOrDefault(a => a.Id == id);
            // return animal==null?Results.NotFound($"animal with id{id} not found"):animalList.Remove(animal);

            if (animal == null)
            {
                return Results.NotFound($"animal with id{id} not found");
            }

            animalList.Remove(animal);
            return Results.Ok($"removed animal{id}");
        });
        // 200 - Ok
        // 201 - Created
        // 404 - Not Found

        app.MapPost("/animals-minimalapi", (Animal animal) =>
        {
            Console.WriteLine(animal.Id);
            Console.WriteLine(animal.FirstName);
            if (false)
            {
                return Results.BadRequest();
            }

            return Results.Created();
        });



        var wizyty = StaticData.visits;
        // ///
        // //     1. we would like to be able to retrieve a list of visits associated with a given animal
        // app.MapGet("animals/{int id}/visits", (int id) =>
        // {
        //     var visits = wizyty.Where(v => v.Animal.Id == id);
        //     return visits.Any() ? Results.Ok(visits) : Results.NotFound($"no viisits found for animaal{id}");
        // });
        
        
        
        app.MapGet("/animals/{id:int}/visits", (int id) =>
        {
            var animalVisits = StaticData.visits.Where(v => v.Animal.Id == id).ToList();
            return animalVisits.Any() ? Results.Ok(animalVisits) : Results.NotFound($"No visits found for animal {id}");
        });


        //     2. we would like to be able to add new visits

        app.MapPost("/animals/{int id}visits", (int id, Visit v) =>
        {
            var animal = StaticData.animals.FirstOrDefault(a => a.Id == id);
            if (animal == null)
            {
                return Results.NotFound($"animal with id{id} not found");
            }

            v.Animal = animal;
            wizyty.Add(v);
            return Results.Created($"animal{id} visit", v);
        });
        
        
        app.MapPost("/animals/{id:int}/visits", (int id, Visit visit) =>
        {
            var animal = animalList.FirstOrDefault(a => a.Id == id);
            if (animal == null)
            {
                return Results.NotFound($"Animal with id {id} not found");
            }

            visit.Animal = animal;
                wizyty.Add(visit);
            return Results.Created($"/animals/{id}/visits/{visit.GetHashCode()}", visit);
        });
    }
}