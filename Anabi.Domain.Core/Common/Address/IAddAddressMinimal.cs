
namespace Anabi.Domain.Common.Address
{
    /* Light address interface */
    public interface IAddAddressMinimal
    {
        string CountyCode { get; set; }

        string City { get; set; }

        string Street { get; set; }

        string Building { get; set; }
    }
}
