using Newtonsoft.Json;

// Resharper Disable All
public class Program
{
    public static async Task Main(string[] args)
    {
        // Primero busco la url:
        var url = "https://rickandmortyapi.com/api/character";
        // objeto de la clase HttpClient
        
        var cliente = new HttpClient();
        var response = await cliente.GetStringAsync(url);
        var data = JsonConvert.DeserializeObject<RickAndMortyResponse>(response);
        var summer = data!.results.FirstOrDefault(c => c.name == "Summer Smith");
        if (summer != null)
        {
            Console.WriteLine($"Nombre: {summer.name}");
            Console.WriteLine($"Estado: {summer.status}");
            Console.WriteLine($"Especie: {summer.species}");
            Console.WriteLine($"Tipo: {summer.type}");
            Console.WriteLine($"Género: {summer.gender}");
            Console.WriteLine($"Origen: {summer.origin.name}");
            Console.WriteLine($"Ubicación: {summer.location.name}");
        }
    }
}

public class RickAndMortyResponse
{
    public Info? info { get; set; }
    public Personaje[]? results { get; set; }
}

public class Info
{
    public int count { get; set; }
    public int pages { get; set; }
    public string next { get; set; }
    public string prev { get; set; }
}

/**
 * Clase con cada tipo de dato de valor del Json
 * que tiene el personaje
 */
public class Personaje
{
    public int id { get; set; }
    public string? name  { get; set; }
    public string? status { get; set; }
    public string? species { get; set; }
    public string? type { get; set; }
    public string? gender { get; set; }
    public Location? origin { get; set; }
    public Location? location { get; set; }
}

public class Location
{
    public string? name { get; set; }
}