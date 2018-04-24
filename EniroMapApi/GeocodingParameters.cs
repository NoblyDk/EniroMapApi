using System;

namespace EniroMapApi
{
    public class GeocodingParameters
    {
        public string Name { get; set; }
        public TypeEnum Type { get; set; }
        public CountryEnum Country { get; set; }
    }

    public enum TypeEnum 
    {
        Any, 
        Address, 
        Street,
        city
    }

    public enum CountryEnum
    {
        SE,
        DK,
        FI,
        NO,
        ALL,
    }

}
