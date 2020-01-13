using System;

namespace CleanArchitecture.SharedKernel.AppSettings
{
    public class ProductAPISettings
    {
        public string BaseUrl { get; set; }
        public string Token { get; set; }

        public string ProductsUri { get { return String.Format("{0}/products?token={1}", BaseUrl, Token); } }
        public string ShopperHistoryUri { get { return String.Format("{0}/shopperHistory?token={1}", BaseUrl, Token); } }
    }
}
