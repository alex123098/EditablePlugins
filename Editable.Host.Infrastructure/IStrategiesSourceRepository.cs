namespace Editable.Host.Infrastructure
{
    public interface IStrategiesSourceRepository
    {
        string FetchVoidMethodPluginSource();

        string FetchResultMethodPluginSource();

        void StoreVoidMethodPluginSource(string source);

        void StoreResultMethodPluginSource(string source);
    }
}
