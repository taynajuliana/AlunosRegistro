using AlunosRegistro.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AlunosRegistro.Service
{
    public class AlunoService
    {
        private readonly IMongoCollection<Aluno> _alunoCollection;
        public AlunoService(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _alunoCollection = mongoDatabase.GetCollection<Aluno>("Aluno");
        }

        public async Task<List<Aluno>> GetAsync() =>
            await _alunoCollection.Find(_ => true).ToListAsync();
        public async Task<Aluno?> GetAsync(string id) =>
            await _alunoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        public async Task CreateAluno(Aluno newAluno) =>
            await _alunoCollection.InsertOneAsync(newAluno);

        public async Task UpdateAluno(string id, Aluno updateAluno)=>
            await _alunoCollection.ReplaceOneAsync(x=>x.Id == id, updateAluno);

        public async Task RemoveAluno(string id) =>
            await _alunoCollection.DeleteOneAsync(x => x.Id == id);
    }
}
