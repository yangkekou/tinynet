//this script should be generated auto
using System.Text.Json.Serialization;

namespace TinyClient.Proto
{
    public enum ProtoType
    {
        Login,
        LoginOut,
    }

    public class RequestData<T>
    {
        public long SequnceId;
        public T Data;

        public ProtoType Protocol;

        [JsonConstructor]
        public RequestData(long sequnceId, ProtoType protocol, T data)
        {
            this.Protocol = protocol;
            this.SequnceId = sequnceId;
            this.Data = data;
        }
    }

    public class ResponseData<T>
    {
        public long SequnceId { get; }

        public int Code { get; }
        public ProtoType Protocol { get; }

        public T Data { get; }
        public string Error { get; }

        [JsonConstructor]
        public ResponseData(long sequnceId, int code, ProtoType protocol, T data, string error)
        {
            this.SequnceId = sequnceId;
            this.Code = code;
            this.Data = data;
            this.Error = error;
            this.Protocol = protocol;
        }
    }

    public class LoginRequest
    {
        public string Name;
        public string Password;
    }

    public class LoginResponse
    {
        public bool Success;
        public string Message;

        public long UserId;
        public string Name;
        public string Pic;
    }

    public class LoginOutRequest
    {
        public long Time;
    }

    public class LoginOutResponse
    {
        public bool Success { get; }


        [JsonConstructor]
        public LoginOutResponse(bool success)
        {
            this.Success = success;
        }
    }

}