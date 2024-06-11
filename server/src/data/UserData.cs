
[Db("redis://localhoast:5760/db/user")]
public class UserData
{

    [DbFiled(typeof(string), "name", null)]
    public string Name;

    [DbFiled(typeof(int), "age", null)]
    public int Age;
}