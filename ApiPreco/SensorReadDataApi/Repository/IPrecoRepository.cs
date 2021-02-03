using PrecoApi.Domain;
using PrecoApi.Domain.Enum;
using System.Collections.Generic;

namespace PrecoApi.Repository
{
    public interface IPrecoRepository
    {
        public IEnumerable<ReturnPrice> ListAll();

        public MedalDiscount GetMedalDiscount(long productCode, long filial, CodeMedal medalCode);

    }
}
