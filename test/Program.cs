using System.Text.Json;
using System.Text.Json.Serialization;
using TinyClient.Proto;
using TC = TinyClient;
public class Program
{
    public static void Main()
    {
        var client = new TC.TinyClient();
        client.Init(true);
        client.Get<LoginOutResponse>("http://localhost:8080/exit", ProtoType.LoginOut, (response) =>
        {
            Console.WriteLine(response.Code);
            Console.WriteLine(response.Error);
        });

        Thread.Sleep(5 * 1000);
    }
}
