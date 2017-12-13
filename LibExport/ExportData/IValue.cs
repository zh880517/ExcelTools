namespace LibExport
{
    public interface IValue
    {
        string ToJson(int tableNum);
        string ToLua(int tableNum);
    }
    
}
