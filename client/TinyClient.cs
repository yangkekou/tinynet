using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using TinyClient.Proto;

namespace TinyClient
{

    public class CachedHttpRequest
    {
        public long SequnceId;
        public string Url;
        public string Body;
        public ProtoType Proto;
        public HttpMethod HttpMethod;

        public bool Requesting = false;

        public Action<ResponseData<object>> Callback;
    }

    public class TinyClient
    {

        private HttpClient _httpClient;
        private CheatServer _cheatServer;

        private long _sequnceId = 0;
        private bool _netActive = false;
        public bool httpRequesing
        {
            get;
            private set;
        }

        public bool hasCachedRequest => this._cachedRequests.Count > 0;

        private readonly Queue<CachedHttpRequest> _cachedRequests = new Queue<CachedHttpRequest>();
        private readonly JsonSerializerOptions _jsonOption = new JsonSerializerOptions(JsonSerializerDefaults.Web);

        private long cachedRequestId => (this.hasCachedRequest ? this._cachedRequests.Peek().SequnceId : this._sequnceId % long.MaxValue) + 1;

        public void Init(bool netActive)
        {
            this.UpdateNetState(netActive);
            this._httpClient = new HttpClient();
            this._cheatServer = new CheatServer();
            _jsonOption.IncludeFields = true;
        }

        /**
        <summary>
            for http GET
        </summary>
        */
        public async void Get<T>(string url, ProtoType proto, Action<ResponseData<T>> callback)
        {
            var requestUrl = $"{url}?{Const.SequnceIdName}={this._sequnceId}";
            if (this._netActive && !this.httpRequesing)
            {
                try
                {
                    this.httpRequesing = true;
                    var response = await this._httpClient.GetFromJsonAsync<ResponseData<T>>(requestUrl, _jsonOption);
                    if (callback != null) callback(response);
                }
                catch (Exception e)
                {
                    var response = new ResponseData<T>(this._sequnceId, 400, proto, default, e.Message);
                    if (callback != null) callback(response);
                }
                finally
                {
                    this.httpRequesing = false;
                    addSequnceId();
                }
            }else if(this.httpRequesing){
                var requestData = new CachedHttpRequest()
                {
                    SequnceId = this.cachedRequestId,
                    Body = string.Empty,
                    Url = requestUrl,
                    Proto = proto,
                    HttpMethod = HttpMethod.Get,
                    Callback = (Action<ResponseData<object>>)callback
                };

                this._cachedRequests.Enqueue(requestData);
            }
            else
            {
                var response = this._cheatServer.OnGetRequested<T>(requestUrl, proto);
                if (callback != null) callback(response);
                addSequnceId();
            }
        }

        /**
        <summary>
            for http PUT
        </summary>
        */
        public async void Put<T, R>(string url, ProtoType proto, T data, Action<ResponseData<R>> callback)
        {
            var requestData = new RequestData<T>(this._sequnceId, proto, data);
            if (this._netActive && !this.httpRequesing)
            {
                try
                {
                    this.httpRequesing = true;
                    using var response = await this._httpClient.PutAsJsonAsync(url, requestData, _jsonOption);
                    response.EnsureSuccessStatusCode();
                    var json = await response.Content.ReadFromJsonAsync<ResponseData<R>>();
                    if (callback != null) callback(json);
                }
                catch (HttpRequestException e)
                {
                    var code = e.StatusCode ?? System.Net.HttpStatusCode.Ambiguous;
                    var json = new ResponseData<R>(this._sequnceId, (int)code, proto, default, e.Message);
                    if (callback != null) callback(json);
                }
                finally
                {
                    this.httpRequesing = false;
                    addSequnceId();
                }

            }
            else if(this.httpRequesing){
                var cdata = new CachedHttpRequest()
                {
                    SequnceId = this.cachedRequestId,
                    Url = url,
                    Proto = proto,
                    HttpMethod = HttpMethod.Put,
                    Body = JsonSerializer.Serialize(requestData),
                    Callback = (Action<ResponseData<object>>)callback
                };

                this._cachedRequests.Enqueue(cdata);
            }
            else
            {
                var response = this._cheatServer.OnPutRequested<T, R>(url, requestData);
                if (callback != null) callback(response);
                addSequnceId();
            }
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
        public void RegisterListener<V>(Action<V> callback)
        {

        }

        /**
        <summary>
            remove listener for server pushing
        </summary>
        */
        public void UnRegisterListener<V>(Action<V> callback)
        {

        }

        public void Update()
        {
            if (this.httpRequesing || !this.hasCachedRequest || !this._netActive) return;

            var cached = this._cachedRequests.Peek();
            if(cached.Requesting) return;
        
            cached.Requesting = true;
            var request = new HttpRequestMessage(cached.HttpMethod, cached.Url)
            {
                Content = new StringContent(cached.Body, Encoding.UTF8, "application/json")
            };

            this._httpClient.SendAsync(request).ContinueWith(t =>
            {
                this.addSequnceId();
                this._cachedRequests.Dequeue();
                cached.Requesting = false;
                if (t.IsFaulted || t.IsCanceled) return;
                var response = t.Result;
                response.EnsureSuccessStatusCode();
                var json = response.Content.ReadAsStringAsync().Result;
                var data = JsonSerializer.Deserialize<ResponseData<object>>(json);
                if (cached.Callback != null) cached.Callback(data);      
            });
        }

        public void UpdateNetState(bool netActive)
        {
            this._netActive = netActive;
        }

        private void addSequnceId()
        {
            this._sequnceId %= long.MaxValue;
            this._sequnceId++;
        }
    }
}