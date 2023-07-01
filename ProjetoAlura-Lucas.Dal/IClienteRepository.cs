
namespace ProjetoAlura_Lucas.Dal
{
    public interface IClienteRepository
    {
        int Add(Cliente cliente);
        int Remove(int id);
        Cliente GetOne(int id);
        IEnumerable<Cliente> GetAll();
        int Update(int id, Cliente cliente);
    }
}
