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

        public readonly static string SELECT_PRECO_ENCARTE_POR_FILIAL_E_CODIGO =
            @"SELECT pm.prme_cd_produto AS ProductId,
                     CASE
                       WHEN Cast(pf.prfi_dt_iniprcprom AS DATE) < Getdate() - 250
                            AND Cast(pf.prfi_dt_fimprcprom AS DATE) >=
                                CONVERT (DATE, Getdate() - 120) THEN
                         CASE
                           WHEN @MedalCode = 2 THEN pf.prfi_vl_prcprom
                           WHEN @MedalCode <> 2
                                AND @MedalCode <> 0
                                AND Isnull(encarte_cliente_sempre, 'N') IN ( 'N', 'S' ) THEN
                           pf.prfi_vl_prcprom
                           WHEN @MedalCode = 0
                                AND Isnull(encarte_cliente_sempre, 'N') = 'N' THEN
                           pf.prfi_vl_prcprom
                         END
                       ELSE 0
                     END                SalePrice
              FROM   cosmos_v14b..produto_filial pf WITH (nolock)
                     INNER JOIN cosmos_v14b..produto_mestre pm WITH (nolock)
                             ON pf.prme_cd_produto = pm.prme_cd_produto
              WHERE  pf.prme_cd_produto = @ProductId
                     AND PF.fili_cd_filial = @StoreId";
    }
}
