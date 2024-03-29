﻿using System.Numerics;
using Zenon.Model.Embedded.Json;
using Zenon.Model.Primitives;
using Zenon.Utils;

namespace Zenon.Model.Embedded
{
    public class FusionEntry : IJsonConvertible<JFusionEntry>
    {
        public FusionEntry(JFusionEntry json)
        {
            QsrAmount = AmountUtils.ParseAmount(json.qsrAmount);
            Beneficiary = Address.Parse(json.beneficiary);
            ExpirationHeight = json.expirationHeight;
            Id = Hash.Parse(json.id);
        }

        public FusionEntry(Address beneficiary, ulong expirationHeight, Hash id, BigInteger qsrAmount)
        {
            Beneficiary = beneficiary;
            ExpirationHeight = expirationHeight;
            Id = id;
            QsrAmount = qsrAmount;
        }

        public BigInteger QsrAmount { get; }
        public Address Beneficiary { get; }
        public ulong ExpirationHeight { get; }
        public Hash Id { get; }
        public bool? IsRevocable { get; }

        public virtual JFusionEntry ToJson()
        {
            return new JFusionEntry()
            {
                qsrAmount = QsrAmount.ToString(),
                beneficiary = Beneficiary.ToString(),
                expirationHeight = ExpirationHeight,
                id = Id.ToString()
            };
        }
    }
}
