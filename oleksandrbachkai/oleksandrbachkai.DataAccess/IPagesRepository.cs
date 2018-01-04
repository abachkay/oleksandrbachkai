using oleksandrbachkai.Models.Entities;
using System.Collections.Generic;

namespace oleksandrbachkai.DataAccess
{
    public interface IPagesRepository: IRepository<Page>
    {
        IEnumerable<PageName> GetPageNames();

        void UpdatePageContent(int id, string content);
    }
}
