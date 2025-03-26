namespace HeQuanTriDB.repositories
{
    public interface IMonAnRespository
    {
        string ConnectionString { get; }
        Task<List<MonAn>> GetAllMonAns();
        Task<MonAn> GetMonAnByID(int maMonAn);
        Task<int> AddMonAn(MonAn monAn);
        Task<int> UpdateMonAn(MonAn monAn);
        Task<int> DeleteMonAn(int maMonAn);
    }
}