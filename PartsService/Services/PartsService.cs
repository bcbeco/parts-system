using MongoDB.Driver;
using PartsService.Models;

namespace PartsService.Services;

public class PartService
{
    private readonly IMongoCollection<Part> _collection;

   public PartService(IConfiguration config)
{
    var settings = config.GetSection("MongoSettings");

    var client = new MongoClient(settings["Connection"]);
    var database = client.GetDatabase(settings["Database"]);
    _collection = database.GetCollection<Part>(settings["Collection"]);
}

    public async Task<List<Part>> GetAsync() =>
        await _collection.Find(_ => true).ToListAsync();

    public async Task<Part> CreateAsync(Part part)
    {
        await _collection.InsertOneAsync(part);
        return part;
    }

    public async Task UpdateAsync(string id, Part updated) =>
        await _collection.ReplaceOneAsync(p => p.Id == id, updated);

    public async Task DeleteAsync(string id) =>
        await _collection.DeleteOneAsync(p => p.Id == id);
}