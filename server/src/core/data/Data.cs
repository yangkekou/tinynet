
public enum DataCmdType
{
    Login = 1,
    Game = 100,
    Pay = 200,
    Email = 300
}

class RequestData<T>
{
    public long SequnceId;
    public DataCmdType CmdType;

    public void DeSerial(byte[] bytes)
    {

    }
}

class ReviveData<T>
{
    public readonly long SequnceId;
    public DataCmdType CmdType;
    public readonly int Code;
    public readonly string Error;
    public readonly T Data;

    public byte[] GetBytes()
    {
        return null;
    }
}