namespace PrecoApi.Domain
{
    public class DiscountSegmentation
    {
        public long Desconto { get; set; }
        public int CodigoProduto { get; set; }
        public int CodigoMedalha { get; set; }
        public int Filial { get; set; }
        public bool PBM { get; set; }
        public bool FarmaciaPopular { get; set; }
        public decimal PercentualDesconto { get; set; }
        public string Medalha { get; set; }
    }
}