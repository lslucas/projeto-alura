using System.Data.SqlClient;
using Dapper;

namespace ProjetoAlura_Lucas.Dal
{
	public class ClienteRepository : IClienteRepository
	{
		IConfiguration _configuration;

		public ClienteRepository(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public string GetConnection() => _configuration.GetValue<string>("AppConnection");

		protected bool EhClienteValido(Cliente cliente)
		{
            if (string.IsNullOrEmpty(cliente.Nome))
            {
                return false;
            }
			if (string.IsNullOrEmpty(cliente.Email))
            {
                return false;
            }

			return true;
		}

		public int Add(Cliente cliente)
		{
			try
			{
				var conn = this.GetConnection();
				using (var cn = new SqlConnection(conn)) {
					var rawQuery = "INSERT INTO TBClientes (Nome, Email, ProfilePic) VALUES (@Nome, @Email, @ProfilePic);";
					return cn.Execute(rawQuery, cliente);
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public Cliente GetOne(int id)
		{
			try
			{
				var conn = this.GetConnection();
				using (var cn = new SqlConnection(conn)) {
					var result = cn.Query<Cliente>("SELECT Nome, Email, ProfilePic FROM TBClientes WHERE Id=@Id;", new { Id = id });
                    return result.FirstOrDefault();
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public IEnumerable<Cliente> GetAll()
		{
			try
			{
				var conn = this.GetConnection();
				using (var cn = new SqlConnection(conn)) {
					var result = cn.Query<Cliente>("SELECT Id, Nome, Email, ProfilePic FROM TBClientes;");
                    return result;
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int Update(int id, Cliente cliente)
		{
			try
			{
				if (id != cliente.Id)
				{
					throw new InvalidDataException("ID informado não existe ou é inválido!");
				}

				var conn = this.GetConnection();
				using (var cn = new SqlConnection(conn)) {
					var rawQuery = "UPDATE TBClientes SET Nome=@Nome, Email=@Email, ProfilePic=@ProfilePic WHERE Id=@Id;";
					return cn.Execute(rawQuery, new { cliente, Id = id });
				}
			}
			catch (Exception ex)
			{
				throw new Exception($"Falha ao atualizar item: {ex.Message}");
			}
		}

		public int Remove(int id)
		{
			try
			{
				var exists = GetOne(id);
				if (exists == null)
				{
					throw new InvalidDataException("ID informado não existe ou é inválido!");
				}

				var conn = this.GetConnection();
				using (var cn = new SqlConnection(conn)) {
					var rawQuery = "DELETE FROM TBClientes WHERE Id=@Id;";
					return cn.Execute(rawQuery, new { Id = id });
				}
			}
			catch (Exception ex)
			{
				throw new Exception($"Falha ao remover item: {ex.Message}");
			}
		}

	}
}