﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using PrecoApi.Domain;
using PrecoApi.Domain.Enum;
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

        public IEnumerable<ReturnPrice> ListAll()
        {
            using var connection = new SqlConnection(_connectionString);

            var sensorData = connection.Query<ReturnPrice>("select top 5 DPCA_CT_DESCPRDCAB, DPCA_CD_FILIAL, DPCA_TP_PRECOBASE from DESC_PRODUTO_CAB");

            return sensorData;
        }

        public MedalDiscount GetMedalDiscount(long productCode, long storeId, CodeMedal medalCode)
        {
            var sql = PriceScripts.SELECT_SEGMENTACAO_DESCONTO_POR_FILIAL_CODIGO_PRODUTO_E_CODIGO_MEDALHA;
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Filial", storeId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@CodigoProduto", productCode, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@CodigoMedalha", medalCode, DbType.Int32, ParameterDirection.Input);
                MedalDiscount returnDiscount = connection.QueryFirstOrDefault<MedalDiscount>(sql, parameters);
                return returnDiscount;
            }
        }
    }
}

