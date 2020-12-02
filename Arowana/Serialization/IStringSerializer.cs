namespace Arowana.Serialization
{
    public interface IStringSerializer
    {
        byte[] Destringify(string input);
        string Stringify(byte[] output);
    }
}