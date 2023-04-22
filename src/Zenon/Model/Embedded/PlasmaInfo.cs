using Zenon.Model.Embedded.Json;

namespace Zenon.Model.Embedded
{
    public class PlasmaInfo : IJsonConvertible<JPlasmaInfo>
    {
        public PlasmaInfo(JPlasmaInfo json)
        {
            CurrentPlasma = json.currentPlasma;
            MaxPlasma = json.maxPlasma;
            QsrAmount = json.qsrAmount;
        }

        public PlasmaInfo(long currentPlasma, long maxPlasma, long qsrAmount)
        {
            CurrentPlasma = currentPlasma;
            MaxPlasma = maxPlasma;
            QsrAmount = qsrAmount;
        }

        public long CurrentPlasma { get; }
        public long MaxPlasma { get; }
        public long QsrAmount { get; }

        public virtual JPlasmaInfo ToJson()
        {
            return new JPlasmaInfo()
            {
                currentPlasma = CurrentPlasma,
                maxPlasma = MaxPlasma,
                qsrAmount = QsrAmount
            };
        }
    }
}
