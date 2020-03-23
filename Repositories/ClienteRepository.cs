        
using DesafioThomasGreg.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioThomasGreg.Repositories
{
    public class ClienteRepository
    {
        private readonly string _connectionString;

        public ClienteRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("defaultConnection");
        }

        private Cliente MapToValue(SqlDataReader reader)
        {
            return new Cliente()
            {
                Id = (int)reader["ClienteId"],
                Nome = reader["Nome"].ToString(),
                Email = reader["Email"].ToString(),
                LogoTipo = reader["LogoTipo"].ToString(),
                LogradouroId = (int)reader["LogradouroId"]
            };
        }
		
		public async Task Criar(Cliente cliente)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("CriarCliente", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ClienteId", cliente.Id));
                    cmd.Parameters.Add(new SqlParameter("@Nome", cliente.Nome));
                    cmd.Parameters.Add(new SqlParameter("@Email", cliente.Email));
                    cmd.Parameters.Add(new SqlParameter("@LogoTipo", cliente.LogoTipo));
                    cmd.Parameters.Add(new SqlParameter("@LogradouroId", cliente.LogradouroId));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }
		
		public async Task Atualizar(Cliente cliente)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AtualizarCliente", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
					cmd.Parameters.Add(new SqlParameter("@ClienteId", cliente.Id));
                    cmd.Parameters.Add(new SqlParameter("@Nome", cliente.Nome));
                    cmd.Parameters.Add(new SqlParameter("@Email", cliente.Email));
                    cmd.Parameters.Add(new SqlParameter("@LogoTipo", cliente.LogoTipo));
                    cmd.Parameters.Add(new SqlParameter("@LogradouroId", cliente.LogradouroId));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }

        public async Task<List<Cliente>> Visualizar(int ClienteId)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("VisualizarCliente", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ClienteId", ClienteId));
                    var response = new List<Cliente>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValue(reader));
                        }
                    }

                    return response;
                }
            }
        }

        public async Task Remover(int ClienteId, int LogradouroId)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("RemoverCliente", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ClienteId", ClienteId));
                    cmd.Parameters.Add(new SqlParameter("@LogradouroId", LogradouroId));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }

        public async Task<List<Cliente>> Email(string EmailCliente)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("EmailCliente", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Email", EmailCliente));
                    var response = new List<Cliente>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValue(reader));
                        }
                    }

                    return response;
                }
            }
        }
    }
}