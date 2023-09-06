using Zenon.Model.Embedded.Json;

namespace Zenon.Model.Embedded
{
    public class GetRequiredResponse : IJsonConvertible<JGetRequiredResponse>
    {
        public ulong AvailablePlasma { get; }
        public ulong BasePlasma { get; }
        public ulong RequiredDifficulty { get; }

        public GetRequiredResponse(JGetRequiredResponse json)
        {
            this.AvailablePlasma = json.availablePlasma;
            this.BasePlasma = json.basePlasma;
            this.RequiredDifficulty = json.requiredDifficulty;
        }

        public virtual JGetRequiredResponse ToJson()
        {
            return new JGetRequiredResponse()
            {
                availablePlasma = this.AvailablePlasma,
                basePlasma = this.BasePlasma,
                requiredDifficulty = this.RequiredDifficulty
            };
        }
    }
}
