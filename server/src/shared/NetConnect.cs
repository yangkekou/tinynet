using System.Net.Sockets;

public enum NetConnectType
{
    TCP,
    UDP,
    WEBSOCKET,
    HTTP,
    HTTPS,

}
public class NetConnect
{
    public string Id;
    private Socket _socket;
    public NetConnectType Type;
    public void Init(int port)
    {

    }

    public void Send(byte[] data)
    {

    }

    public void Recive(byte[] data)
    {

    }
}