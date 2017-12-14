namespace LibExport
{
    public interface IValue
    {
        string ToJson(int tableNum);
        string ToLua(int tableNum);
    }

    public interface IKeyType : IValue
    {
        string ToJsonKey();

        string ToLuaKey();
    }
    
}
