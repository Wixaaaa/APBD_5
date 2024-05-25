namespace Zadanie7.Interfaces
{
    public interface IClientService
    {
        Task<int> DeleteClientAsync(int id);
    }
}
