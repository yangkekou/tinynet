using System;

[AttributeUsage(AttributeTargets.Class)]
public class DbAttribute : Attribute
{
    private string _schema;
    public DbAttribute(string schema)
    {
        this._schema = schema;
    }
}

[AttributeUsage(AttributeTargets.Field)]
public class DbFiledAttribute : Attribute
{
    private Type _type;
    private string _filedName;
    private Object _defaultValue;

    public DbFiledAttribute(Type t, string name, Object defaultValue)
    {
        this._type = t;
        this._filedName = name;
        this._defaultValue = defaultValue;
    }
}