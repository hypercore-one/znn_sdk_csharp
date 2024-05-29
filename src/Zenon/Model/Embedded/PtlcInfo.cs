﻿using System;
using System.Numerics;
using Zenon.Model.Embedded.Json;
using Zenon.Model.Primitives;
using Zenon.Utils;

namespace Zenon.Model.Embedded
{
    public class PtlcInfo : IJsonConvertible<JPtlcInfo>
    {
        public PtlcInfo(JPtlcInfo json)
        {
            Id = Hash.Parse(json.id);
            TimeLocked = Address.Parse(json.timeLocked);
            TokenStandard = TokenStandard.Parse(json.tokenStandard);
            Amount = AmountUtils.ParseAmount(json.amount);
            ExpirationTime = json.expirationTime;
            PointType = json.pointType;
            PointLock = Convert.FromBase64String(json.pointLock);
        }

        public Hash Id { get; }
        public Address TimeLocked { get; }
        public TokenStandard TokenStandard { get; }
        public BigInteger Amount { get; }
        public long ExpirationTime { get; }
        public int PointType { get; }
        public byte[] PointLock { get; }

        public virtual JPtlcInfo ToJson()
        {
            return new JPtlcInfo()
            {
                timeLocked = TimeLocked.ToString(),
                tokenStandard = TokenStandard.ToString(),
                amount = Amount.ToString(),
                expirationTime = ExpirationTime,
                pointType = PointType,
                pointLock = Convert.ToBase64String(PointLock)
            };
        }
    }
}