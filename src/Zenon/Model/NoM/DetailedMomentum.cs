using System.Collections.Generic;
using System.Linq;
using Zenon.Model.NoM.Json;

namespace Zenon.Model.NoM
{
    public class DetailedMomentum : IJsonConvertible<JDetailedMomentum>
    {
        public DetailedMomentum(JDetailedMomentum json)
        {
            Blocks = json.blocks.Aggregate(new List<AccountBlock>(), (previousValue, j) =>
            {
                previousValue.Add(new AccountBlock(j));
                return previousValue;
            }).ToArray();
            Momentum = new Momentum(json.momentum);
        }

        public AccountBlock[] Blocks { get; }
        public Momentum Momentum { get; }

        public virtual JDetailedMomentum ToJson()
        {
            return new JDetailedMomentum()
            {
                blocks = Blocks.Select(x => x.ToJson()).ToArray(),
                momentum = Momentum.ToJson(),
            };
        }
    }
}
