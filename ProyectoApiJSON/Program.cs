using Newtonsoft.Json;

// Resharper Disable All
public class Program
{
    // Declaro mi Main como async para poder usar el await.
    public static async Task Main(string[] args)
    {
        // Objeto de la clase 
        var service = new BuscadorRickyMorty();
        
        // Creo un onjeto de GetPersonaje, elijo Summer Smith
        var elPersonaje = await service.GetPersonaje("Summer Smith");
        
        // Me aseguro de que el personaje no sea nulo, si existe, imprime los datos:
        if (elPersonaje != null)
        {
            Console.WriteLine($"Nombre: {elPersonaje.name}");
            Console.WriteLine($"Estado: {elPersonaje.status}");
            Console.WriteLine($"Especie: {elPersonaje.species}");
            Console.WriteLine($"Tipo: {elPersonaje.type}"); // En el caso de Summer, no existe tipo
            Console.WriteLine($"Género: {elPersonaje.gender}");
            Console.WriteLine($"Origen: {elPersonaje.origin!.name}");
            Console.WriteLine($"Ubicación: {elPersonaje.location!.name}");
            Console.WriteLine("Episodios:");
            if (elPersonaje.episode != null)
                foreach (var episode in elPersonaje.episode)
                {
                    var episodeNumber = episode.Split('/').Last();
                    Console.WriteLine(episodeNumber);
                }
        }
    }
}

/// <summary>
/// La clase para interactuar con la API
/// </summary>
public class BuscadorRickyMorty
{
    private readonly HttpClient _cliente;

    /// <summary>
    /// Constructor de la clase
    /// </summary>
    public BuscadorRickyMorty()
    {
        // Objeto de la clase HttpClient
        _cliente = new HttpClient();
    }
    
    /// <summary>
    /// Método para obtener el personaje. Uso POO, le paso un nombre como parámetro, luego llamo al objeto en el Main
    /// </summary>
    /// <param name="nombre">El nombre del personaje, como su nombre indica.</param>
    /// <returns>Devolverá el personaje, o null si este no existe</returns>
    public async Task<Personaje?> GetPersonaje(string nombre)
    {
        var url = "https://rickandmortyapi.com/api/character";
        var response = await _cliente.GetStringAsync(url);
        var data = JsonConvert.DeserializeObject<RespuestaRickyMorty>(response);
        return data!.results!.FirstOrDefault(c => c.name == nombre);
    }
}

/// <summary>
/// Clase para deserializar la respuesta de la API de Rick and Morty.
/// </summary>
public class RespuestaRickyMorty
{
    /// <summary>
    /// Información sobre la paginación de los resultados.
    /// </summary>
    public Info? info { get; set; }
    
    /// <summary>
    /// Los personajes devueltos por la API.
    /// </summary>
    public Personaje[]? results { get; set; }
}
public class Info
{
    public int count { get; set; }
    public int pages { get; set; }
    public string? next { get; set; }
    public string? prev { get; set; }
}

/// <summary>
/// Clase para deserializar la información de un personaje...
/// </summary>
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
    public string[]? episode { get; set; } // A obtengo los episodios en los que sale el pj
}

/// <summary>
/// Creo una clase Location, puesto que la voy a usar tanto para origin como para location
/// </summary>
public class Location
{
    public string? name { get; set; }
}