using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using PrecoApi.Domain;
using PrecoApi.Scripts;

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

        public DiscountSegmentation GetDiscountSegmentation(long productCode, long filial, long medalCode)
        {
            var sql = PriceScripts.SELECT_SEGMENTACAO_DESCONTO_POR_FILIAL_CODIGO_PRODUTO_E_CODIGO_MEDALHA;
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Filial", filial, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@CodigoProduto", productCode, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@CodigoMedalha", medalCode, DbType.Int32, ParameterDirection.Input);
                DiscountSegmentation returnDiscount = connection.QueryFirstOrDefault<DiscountSegmentation>(sql, parameters);
                return returnDiscount;
            }
        }
    }
}

