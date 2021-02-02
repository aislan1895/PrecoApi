namespace PrecoApi.Scripts
{
    public class PriceScripts
    {
        public readonly static string SELECT_SEGMENTACAO_DESCONTO_POR_FILIAL_CODIGO_PRODUTO_E_CODIGO_MEDALHA =
            @"SELECT SD.DMCS_CL_CLIENTE AS CodigoMedalha,
                     SD.DMCS_DS_CL_CLI AS Medalha,
                     SD.DMCS_PR_DESCONTO AS PercentualDesconto
                        FROM CosmosFL..SEGMENTACAO_DESCONTOS_MEDALHA_CONSOLIDADO as SD WITH (NOLOCK)
                            INNER JOIN CosmosFL..SEGMENTACAO_DESCONTOS_MEDALHA_ENV as SDF WITH (NOLOCK)
                            ON SD.DMCS_CD_DESCONTO = SDF.DMCS_CD_DESCONTO
                        WHERE SDF.FILI_CD_FILIAL = @Filial
                          AND SD.PRME_CD_PRODUTO = @CodigoProduto
                          AND DMCS_CL_CLIENTE = @CodigoMedalha";
    }
}
