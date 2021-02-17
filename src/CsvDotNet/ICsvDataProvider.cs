namespace CsvDotNet
{
    public interface ICsvDataProvider
    {
        int Next();

        int Peek();
    }
}
