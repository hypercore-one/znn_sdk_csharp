namespace Zenon.Wallet.Ledger
{
    /// <summary>
    /// An broken up in to its constituent parts for the purpose of performing operations on a Hardwarewallet device.
    /// </summary>
    public interface IAddressPath
    {
        /// <summary>
        /// The collection of IAddressPathElements which make up the address path
        /// </summary>
        List<IAddressPathElement> AddressPathElements { get; }

        /// <summary>
        /// Returns the address path as an array of uints. Indices will be hardended or unhardened depending on their IsHardened property
        /// </summary>
        uint[] ToArray();
    }
}
