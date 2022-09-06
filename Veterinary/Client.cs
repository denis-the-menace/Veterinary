using RestSharp;

namespace Veterinary
{
    public static class Client
    {
        public static readonly RestClient _client = new RestClient("https://localhost:44326/api/");
    }
}
