﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Zenon.Model.Primitives;
using Zenon.Utils;

namespace Zenon.Embedded.TestData
{
    internal class AbiFunctionValidTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            var address = Address.Parse("z1qq0hffeyj0htmnr4gc6grd8zmqfvwzgrydt402");
            var hash = Hash.Parse("05a0fef85008e63f0680b68d11743ba5caf199994d642590febe570b2a84b612");

            #region Common functions
            yield return new object[]
            {
                Definitions.Common,
                "CollectReward",
                new object[0],
                Convert.FromHexString("AF43D3F0")
            };
            yield return new object[]
            {
                Definitions.Common,
                "DepositQsr",
                new object[0],
                Convert.FromHexString("D49577F4")
            };
            yield return new object[]
            {
                Definitions.Common,
                "WithdrawQsr",
                new object[0],
                Convert.FromHexString("B3D658FD")
            };
            #endregion

            #region Ptlc functions
            yield return new object[]
            {
                Definitions.Ptlc,
                "Create",
                new object[] { 86400, 0, BytesUtils.FromHexString("de543a6cab8db5bdc086d1720b97b0f097458841cd0264d789350e3b07587f5b") },
                Convert.FromHexString("ae0f71640000000000000000000000000000000000000000000000000000000000015180000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000600000000000000000000000000000000000000000000000000000000000000020de543a6cab8db5bdc086d1720b97b0f097458841cd0264d789350e3b07587f5b")
            };
            yield return new object[]
            {
                Definitions.Ptlc,
                "Reclaim",
                new object[] { hash.Bytes },
                Convert.FromHexString("7e003c8d05a0fef85008e63f0680b68d11743ba5caf199994d642590febe570b2a84b612")
            };
            yield return new object[]
            {
                Definitions.Ptlc,
                "Unlock",
                new object[] { hash.Bytes, Encoding.UTF8.GetBytes("all your znn belong to us") },
                Convert.FromHexString("d33791d305a0fef85008e63f0680b68d11743ba5caf199994d642590febe570b2a84b61200000000000000000000000000000000000000000000000000000000000000400000000000000000000000000000000000000000000000000000000000000019616c6c20796f7572207a6e6e2062656c6f6e6720746f20757300000000000000")
            };
            #endregion

            #region Accelerator functions
            yield return new object[]
            {
                Definitions.Accelerator,
                "CreateProject",
                new object[] { "TestProject", "Test Project", "", 500, 0 },
                Convert.FromHexString("77C044B600000000000000000000000000000000000000000000000000000000000000A000000000000000000000000000000000000000000000000000000000000000E0000000000000000000000000000000000000000000000000000000000000012000000000000000000000000000000000000000000000000000000000000001F40000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000B5465737450726F6A656374000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000C546573742050726F6A656374000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000")
            };
            yield return new object[]
            {
                Definitions.Accelerator,
                "AddPhase",
                new object[] { hash.Bytes, "TestProject", "Test Project", "", 500, 0 },
                Convert.FromHexString("C7E13DDC05A0FEF85008E63F0680B68D11743BA5CAF199994D642590FEBE570B2A84B61200000000000000000000000000000000000000000000000000000000000000C00000000000000000000000000000000000000000000000000000000000000100000000000000000000000000000000000000000000000000000000000000014000000000000000000000000000000000000000000000000000000000000001F40000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000B5465737450726F6A656374000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000C546573742050726F6A656374000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000")
            };
            yield return new object[]
            {
                Definitions.Accelerator,
                "UpdatePhase",
                new object[] { hash.Bytes, "TestProject", "Test Project", "", 500, 0 },
                Convert.FromHexString("C1D7D32305A0FEF85008E63F0680B68D11743BA5CAF199994D642590FEBE570B2A84B61200000000000000000000000000000000000000000000000000000000000000C00000000000000000000000000000000000000000000000000000000000000100000000000000000000000000000000000000000000000000000000000000014000000000000000000000000000000000000000000000000000000000000001F40000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000B5465737450726F6A656374000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000C546573742050726F6A656374000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000")
            };
            yield return new object[]
            {
                Definitions.Accelerator,
                "Donate",
                new object[0],
                Convert.FromHexString("CB7F8B2A")
            };
            yield return new object[]
            {
                Definitions.Accelerator,
                "VoteByName",
                new object[] { hash.Bytes, "TestPillar", 10 },
                Convert.FromHexString("5C6C106405A0FEF85008E63F0680B68D11743BA5CAF199994D642590FEBE570B2A84B6120000000000000000000000000000000000000000000000000000000000000060000000000000000000000000000000000000000000000000000000000000000A000000000000000000000000000000000000000000000000000000000000000A5465737450696C6C617200000000000000000000000000000000000000000000")
            };
            yield return new object[]
            {
                Definitions.Accelerator,
                "VoteByProdAddress",
                new object[] { hash.Bytes, 10 },
                Convert.FromHexString("90ED001C05A0FEF85008E63F0680B68D11743BA5CAF199994D642590FEBE570B2A84B612000000000000000000000000000000000000000000000000000000000000000A")
            };
            #endregion

            #region Pillar functions
            yield return new object[]
            {
                Definitions.Pillar,
                "Register",
                new object[] { "TestPillar", address, address, 60, 100 },
                Convert.FromHexString("644DE92700000000000000000000000000000000000000000000000000000000000000A0000000000000000000000000001F74A72493EEBDCC75463481B4E2D812C70903000000000000000000000000001F74A72493EEBDCC75463481B4E2D812C70903000000000000000000000000000000000000000000000000000000000000003C0000000000000000000000000000000000000000000000000000000000000064000000000000000000000000000000000000000000000000000000000000000A5465737450696C6C617200000000000000000000000000000000000000000000")
            };
            yield return new object[]
            {
                Definitions.Pillar,
                "RegisterLegacy",
                new object[] { "TestPillar", address, address, 50, 50, "pPM27W6X3qngoo+wjt5D9BSNrNkLnyHZcmYr+UB8r7A=", "8TMpgC8O/VYhjLxUj2gHK7jcvH+SBELkYixicx5eA5jlxQTpzqohXRv93BkSdtmDAdpvzF1ADtnJo9uF0Gb5DQ==" },
                Convert.FromHexString("E458820700000000000000000000000000000000000000000000000000000000000000E0000000000000000000000000001F74A72493EEBDCC75463481B4E2D812C70903000000000000000000000000001F74A72493EEBDCC75463481B4E2D812C709030000000000000000000000000000000000000000000000000000000000000032000000000000000000000000000000000000000000000000000000000000003200000000000000000000000000000000000000000000000000000000000001200000000000000000000000000000000000000000000000000000000000000180000000000000000000000000000000000000000000000000000000000000000A5465737450696C6C617200000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000002C70504D323757365833716E676F6F2B776A7435443942534E724E6B4C6E79485A636D59722B5542387237413D0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000005838544D706743384F2F5659686A4C78556A3267484B376A6376482B5342454C6B596978696378356541356A6C785154707A716F685852763933426B5364746D44416470767A46314144746E4A6F3975463047623544513D3D0000000000000000")
            };
            yield return new object[]
            {
                Definitions.Pillar,
                "UpdatePillar",
                new object[] { "TestPillar", address, address, 40, 60 },
                Convert.FromHexString("DE0AE34B00000000000000000000000000000000000000000000000000000000000000A0000000000000000000000000001F74A72493EEBDCC75463481B4E2D812C70903000000000000000000000000001F74A72493EEBDCC75463481B4E2D812C709030000000000000000000000000000000000000000000000000000000000000028000000000000000000000000000000000000000000000000000000000000003C000000000000000000000000000000000000000000000000000000000000000A5465737450696C6C617200000000000000000000000000000000000000000000")
            };
            yield return new object[]
            {
                Definitions.Pillar,
                "Revoke",
                new object[] { "TestPillar" },
                Convert.FromHexString("956313060000000000000000000000000000000000000000000000000000000000000020000000000000000000000000000000000000000000000000000000000000000A5465737450696C6C617200000000000000000000000000000000000000000000")
            };
            yield return new object[]
            {
                Definitions.Pillar,
                "Delegate",
                new object[] { "TestPillar" },
                Convert.FromHexString("7C2D5D6E0000000000000000000000000000000000000000000000000000000000000020000000000000000000000000000000000000000000000000000000000000000A5465737450696C6C617200000000000000000000000000000000000000000000")
            };
            yield return new object[]
            {
                Definitions.Pillar,
                "Undelegate",
                new object[0],
                Convert.FromHexString("7E8952C8")
            };
            #endregion

            #region Plasma functions
            yield return new object[]
            {
                Definitions.Plasma,
                "Fuse",
                new object[] { address },
                Convert.FromHexString("5AC942E8000000000000000000000000001F74A72493EEBDCC75463481B4E2D812C70903")
            };
            yield return new object[]
            {
                Definitions.Plasma,
                "CancelFuse",
                new object[] { hash.Bytes },
                Convert.FromHexString("F9CA9DC305A0FEF85008E63F0680B68D11743BA5CAF199994D642590FEBE570B2A84B612")
            };
            #endregion

            #region Sentinel functions
            yield return new object[]
            {
                Definitions.Sentinel,
                "Register",
                new object[0],
                Convert.FromHexString("4DD23517")
            };
            yield return new object[]
            {
                Definitions.Sentinel,
                "Revoke",
                new object[0],
                Convert.FromHexString("58363E24")
            };
            #endregion

            #region Stake functions
            yield return new object[]
            {
                Definitions.Stake,
                "Stake",
                new object[] { 2592000 },
                Convert.FromHexString("D802845A0000000000000000000000000000000000000000000000000000000000278D00")
            };
            yield return new object[]
            {
                Definitions.Stake,
                "Cancel",
                new object[] { hash.Bytes },
                Convert.FromHexString("5A92FE3205A0FEF85008E63F0680B68D11743BA5CAF199994D642590FEBE570B2A84B612")
            };
            #endregion

            #region Swap functions
            yield return new object[]
            {
                Definitions.Swap,
                "RetrieveAssets",
                new object[] { "pPM27W6X3qngoo+wjt5D9BSNrNkLnyHZcmYr+UB8r7A=", "8TMpgC8O/VYhjLxUj2gHK7jcvH+SBELkYixicx5eA5jlxQTpzqohXRv93BkSdtmDAdpvzF1ADtnJo9uF0Gb5DQ==" },
                Convert.FromHexString("47F12C81000000000000000000000000000000000000000000000000000000000000004000000000000000000000000000000000000000000000000000000000000000A0000000000000000000000000000000000000000000000000000000000000002C70504D323757365833716E676F6F2B776A7435443942534E724E6B4C6E79485A636D59722B5542387237413D0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000005838544D706743384F2F5659686A4C78556A3267484B376A6376482B5342454C6B596978696378356541356A6C785154707A716F685852763933426B5364746D44416470767A46314144746E4A6F3975463047623544513D3D0000000000000000")
            };
            #endregion

            #region Token functions
            yield return new object[]
            {
                Definitions.Token,
                "IssueToken",
                new object[] { "TestToken", "TST", "www.test.com", 2993292970217428, 9007199254740991, 8, true, false, true },
                Convert.FromHexString("BC410B910000000000000000000000000000000000000000000000000000000000000120000000000000000000000000000000000000000000000000000000000000016000000000000000000000000000000000000000000000000000000000000001A0000000000000000000000000000000000000000000000000000AA2625432CFD4000000000000000000000000000000000000000000000000001FFFFFFFFFFFFF0000000000000000000000000000000000000000000000000000000000000008000000000000000000000000000000000000000000000000000000000000000100000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001000000000000000000000000000000000000000000000000000000000000000954657374546F6B656E000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000035453540000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000C7777772E746573742E636F6D0000000000000000000000000000000000000000")
            };
            yield return new object[]
            {
                Definitions.Token,
                "Mint",
                new object[] { TokenStandard.ZnnZts, 2993292900217428, address },
                Convert.FromHexString("CD70F9BC0000000000000000000000000000000000000000000014E66318C6318C6318C6000000000000000000000000000000000000000000000000000AA2625006B254000000000000000000000000001F74A72493EEBDCC75463481B4E2D812C70903")
            };
            yield return new object[]
            {
                Definitions.Token,
                "Burn",
                new object[0],
                Convert.FromHexString("3395AB94")
            };
            yield return new object[]
            {
                Definitions.Token,
                "UpdateToken",
                new object[] { TokenStandard.ZnnZts, address, true, true },
                Convert.FromHexString("2A3CF32C0000000000000000000000000000000000000000000014E66318C6318C6318C6000000000000000000000000001F74A72493EEBDCC75463481B4E2D812C7090300000000000000000000000000000000000000000000000000000000000000010000000000000000000000000000000000000000000000000000000000000001")
            };
            #endregion
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
