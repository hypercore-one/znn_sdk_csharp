﻿using System;
using System.Numerics;
using Zenon.Model.Embedded.Json;
using Zenon.Model.Primitives;
using Zenon.Utils;

namespace Zenon.Model.Embedded
{
    public class HtlcInfo : IJsonConvertible<JHtlcInfo>
    {
        public HtlcInfo(JHtlcInfo json)
        {
            Id = Hash.Parse(json.id);
            TimeLocked = Address.Parse(json.timeLocked);
            HashLocked = Address.Parse(json.hashLocked);
            TokenStandard = TokenStandard.Parse(json.tokenStandard);
            Amount = AmountUtils.ParseAmount(json.amount);
            ExpirationTime = json.expirationTime;
            HashType = json.hashType;
            KeyMaxSize = json.keyMaxSize;
            HashLock = Convert.FromBase64String(json.hashLock);
        }

        public Hash Id { get; }
        public Address TimeLocked { get; }
        public Address HashLocked { get; }
        public TokenStandard TokenStandard { get; }
        public BigInteger Amount { get; }
        public long ExpirationTime { get; }
        public int HashType { get; }
        public int KeyMaxSize { get; }
        public byte[] HashLock { get; }

        public virtual JHtlcInfo ToJson()
        {
            return new JHtlcInfo()
            {
                timeLocked = TimeLocked.ToString(),
                hashLocked = HashLocked.ToString(),
                tokenStandard = TokenStandard.ToString(),
                amount = Amount.ToString(),
                expirationTime = ExpirationTime,
                hashType = HashType,
                keyMaxSize = KeyMaxSize,
                hashLock = Convert.ToBase64String(HashLock)
            };
        }
    }
}
