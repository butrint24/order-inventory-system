using Inventory.Data._Contracts;

namespace Inventory.Data.Sql;

public class DataRepository : IDataRepository
{
    private readonly ApplicationDbContext _dataContext;

    public DataRepository(ApplicationDbContext dataContext) => _dataContext = dataContext;
}
