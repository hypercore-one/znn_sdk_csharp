﻿using Zenon.Model.NoM.Json;
using Zenon.Model.Primitives;

namespace Zenon.Model.NoM
{
    public class AccountBlockConfirmationDetail : IJsonConvertible<JAccountBlockConfirmationDetail>
    {
        public AccountBlockConfirmationDetail(JAccountBlockConfirmationDetail json)
        {
            NumConfirmations = json.numConfirmations;
            MomentumHeight = json.momentumHeight;
            MomentumHash = Hash.Parse(json.momentumHash);
            MomentumTimestamp = json.momentumTimestamp;
        }

        public ulong NumConfirmations { get; }
        public ulong MomentumHeight { get; }
        public Hash MomentumHash { get; }
        public ulong MomentumTimestamp { get; }

        public virtual JAccountBlockConfirmationDetail ToJson()
        {
            return new JAccountBlockConfirmationDetail()
            {
                numConfirmations = NumConfirmations,
                momentumHeight = MomentumHeight,
                momentumHash = MomentumHash.ToString(),
                momentumTimestamp = MomentumTimestamp
            };
        }
    }
}
