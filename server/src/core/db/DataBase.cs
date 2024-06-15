
public class DataBase
{
    public void Connect(string url)
    {

    }

    public void DisConnect()
    {

    }

    public async Task<T> GetData<T>(string id)
    {
        return await Task.Run(() =>
        {
            return default(T);
        });
    }

    public void GetDataByCallback<T>(string id, Action<T> callback)
    {

    }

    public async void SaveData<T>(string id, T data)
    {
        await Task.Run(() =>
        {
            Console.WriteLine("save data");
        });
    }


    public async void UpdateData<T>(string id, T data)
    {
        await Task.Run(() =>
        {
            Console.WriteLine("update data");
        });
    }
}