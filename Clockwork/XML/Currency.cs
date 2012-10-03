
namespace Clockwork.XML
{
    /// <summary>
    /// XML Serialisation class for Currencies
    /// </summary>
    public class Currency
    {
        /// <summary>
        /// ISO Country Code
        /// ISO 3166-1 alpha-2 codes
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Currency Symbol
        /// </summary>
        public string Symbol { get; set; }        
    }
}
