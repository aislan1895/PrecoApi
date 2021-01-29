using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using PrecoApi.Domain;

namespace PrecoApi.Repository
{
    public sealed class PrecoRepository : IPrecoRepository
    {
        private readonly string _connectionString;

        public PrecoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PrecoDataServer");
        }

        public IEnumerable<PriceReturn> ListAll()
        {
            using var connection = new SqlConnection(_connectionString);

            var sensorData = connection.Query<PriceReturn>("select top 5 DPCA_CT_DESCPRDCAB, DPCA_CD_FILIAL, DPCA_TP_PRECOBASE from DESC_PRODUTO_CAB");

            return sensorData;
        }        
    }
}

