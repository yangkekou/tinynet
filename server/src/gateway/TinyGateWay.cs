using System.Reflection.PortableExecutable;

public static class TinyGateWay
{
    private static Dictionary<DataCmdType, IService> _services = new Dictionary<DataCmdType, IService>();
    public static void Init()
    {
        //add services
        _services.Add(DataCmdType.Login, new LoginService());
        _services.Add(DataCmdType.Game, new GameService());
        _services.Add(DataCmdType.Pay, new PayService());
        _services.Add(DataCmdType.Email, new EmailService());


        var config = Config.LoadConfig<ServerConf>("./config.yaml");

        foreach(var cfg in config.protocol){
            InitProtocol(cfg);
        }
    }

    private static void InitProtocol(ServerProtocol protocol){
        
    }

    private static void OnHttp(){

    }

    private static void OnTcp(){

    }

    private static void OnUdp(){

    }

    private static void OnWebsocket(){

    }

    public static void Clear()
    {

    }
}