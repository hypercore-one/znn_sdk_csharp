using System.Numerics;
using Zenon.Model.Embedded.Json;
using Zenon.Utils;

namespace Zenon.Model.Embedded
{
    public class PlasmaInfo : IJsonConvertible<JPlasmaInfo>
    {
        public PlasmaInfo(JPlasmaInfo json)
        {
            CurrentPlasma = json.currentPlasma;
            MaxPlasma = json.maxPlasma;
            QsrAmount = AmountUtils.ParseAmount(json.qsrAmount);
        }

        public PlasmaInfo(long currentPlasma, long maxPlasma, BigInteger qsrAmount)
        {
            CurrentPlasma = currentPlasma;
            MaxPlasma = maxPlasma;
            QsrAmount = qsrAmount;
        }

        public long CurrentPlasma { get; }
        public long MaxPlasma { get; }
        public BigInteger QsrAmount { get; }

        public virtual JPlasmaInfo ToJson()
        {
            return new JPlasmaInfo()
            {
                currentPlasma = CurrentPlasma,
                maxPlasma = MaxPlasma,
                qsrAmount = QsrAmount.ToString()
            };
        }
    }
}
