using System.Data.SqlClient;
using System.Linq;

namespace Tests.DB
{
    public class DataContext : IDataContext
    {
        private readonly System.Data.Linq.DataContext dataContext;

        public DataContext()
        {
            var connection = new SqlConnection(
                "Data Source=(local);" +
                "Initial Catalog=Test;" +
                "Integrated Security=SSPI");
            dataContext = new System.Data.Linq.DataContext(connection);
        }

        public IQueryable<T> GetTable<T>() where T : class
        {
            return dataContext.GetTable<T>();
        }

        public void Update()
        {
            dataContext.SubmitChanges();
        }

        public void Delete<T>(T entity) where T : class
        {
            dataContext.GetTable<T>().DeleteOnSubmit(entity);
            dataContext.SubmitChanges();
        }

        public void Create<T>(T entity) where T : class
        {
            dataContext.GetTable<T>().InsertOnSubmit(entity);
            dataContext.SubmitChanges();
        }

        public void Dispose()
        {
            dataContext.Dispose();
        }
    }
}
