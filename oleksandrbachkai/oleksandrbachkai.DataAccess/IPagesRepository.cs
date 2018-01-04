using oleksandrbachkai.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace oleksandrbachkai.DataAccess
{
    public interface IPagesRepository: IRepository<Page>
    {
        Task<IEnumerable<PageName>> GetPageNames();

        Task UpdatePageContent(int id, string content);
    }
}