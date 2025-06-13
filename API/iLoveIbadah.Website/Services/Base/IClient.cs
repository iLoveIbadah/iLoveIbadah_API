namespace iLoveIbadah.Website.Services.Base
{
    // because this is a partial interface it can be extended in other files
    // it is just to deeply in the serviceclient so not verry accessible so making this public httpclient here
    public partial interface IClient
    {
        public HttpClient HttpClient { get; }
    }
}
