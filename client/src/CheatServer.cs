using System.Web;
using TinyClient;
using TinyClient.Proto;

///<summary>
///when the user has no net connection,we can using this cheat code for simulating a server
///</summary>
public class CheatServer
{
    public void Init()
    {

    }
    public ResponseData<R> OnGetRequested<R>(string url, ProtoType proto)
    {
        var requestUrl = new Uri(url);
        Console.WriteLine(requestUrl);
        var parse = HttpUtility.ParseQueryString(requestUrl.Query);
        var id = parse.Get(Const.SequnceIdName);
        switch (proto)
        {
            case ProtoType.Login:
                {
                    var data = new LoginResponse
                    {
                        Name = "none",
                        Pic = "",
                        UserId = 001,
                        Message = "ok",
                    };
                    return new ResponseData<LoginResponse>(long.Parse(id), 200, proto, data, null) as ResponseData<R>;
                }

            case ProtoType.LoginOut:
                {
                    var data = new LoginOutResponse(true);
                    return new ResponseData<LoginOutResponse>(long.Parse(id), 200, proto, data, null) as ResponseData<R>;
                }
        }

        return default;
    }

    public ResponseData<R> OnPutRequested<T, R>(string url, RequestData<T> requestData)
    {
        return default;
    }
}