namespace client;

public class TinyClient
{

    public void Init()
    {

    }

    /**
    <summary>
        for http GET
    </summary>
    */
    public void Get(string url, Action<HttpResponseMessage> callback)
    {

    }

    /**
    <summary>
        for http PUT
    </summary>
    */
    public void Put<T>(string url, T data, Action<HttpResponseMessage> callback)
    {

    }


    /**
    <summary>
        for socket send
    </summary>
    */
    
    public void Send<T, V>(T data, Action<V> callback)
    {

    }


    /**
    <summary>
        add listener for server pushing
    </summary>
    */
    public void RegisterListener<V>(Action<V> callback){

    }

    /**
    <summary>
        remove listener for server pushing
    </summary>
    */
    public void UnRegisterListener<V>(Action<V> callback){

    }
}
