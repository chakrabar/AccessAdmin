using AccessManagement.Domain;
using System.Collections.Generic;

namespace AccessManagement.Repository
{
    public interface IRepository<T> where T : Entity
    {
        IEnumerable<T> Get();
        T Get(int id);
    }
}
