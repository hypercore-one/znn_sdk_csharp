namespace Zenon.LedgerWallet
{
    public abstract class AddressPathBase : IAddressPath
    {
        public List<IAddressPathElement> AddressPathElements { get; private set; } = new List<IAddressPathElement>();

        private static AddressPathElement ParseElement(string elementString)
        {
            if (!uint.TryParse(elementString.Replace("'", string.Empty), out var unhardenedNumber))
            {
                throw new Exception($"The value {elementString} is not a valid path element");
            }

            return new AddressPathElement { Harden = elementString.EndsWith("'"), Value = unhardenedNumber };
        }

        public uint[] ToArray() => AddressPathElements.Select(ape => ape.Harden ? AddressUtils.HardenNumber(ape.Value) : ape.Value).ToArray();

        public override string ToString()
        {
            return $"m/{string.Join("/", AddressPathElements.Select(ape => $"{ape.Value}{(ape.Harden ? "'" : string.Empty)}"))}";
        }

        public static T Parse<T>(string path) where T : AddressPathBase, new() =>
            new T
            {
                AddressPathElements = path.Split('/')
            .Where(t => string.Compare("m", t, StringComparison.OrdinalIgnoreCase) != 0)
            .Select(ParseElement)
            .Cast<IAddressPathElement>()
            .ToList()
            };
    }
}
