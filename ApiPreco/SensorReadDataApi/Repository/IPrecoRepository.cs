using PrecoApi.Domain;
using PrecoApi.Domain.Enum;

namespace PrecoApi.Repository
{
    public interface IPrecoRepository
    {
        public MedalDiscount GetMedalDiscount(long productCode, long filial, CodeMedal medalCode);
    }
}
