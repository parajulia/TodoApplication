using TodoApplication.Entities;
using TodoApplication.Infrastructure;

namespace TodoApplication.Repository
{
    public class TodoRepository : BaseRepository<TodoItem>, ITodoRepository
    {
        public TodoRepository(DataContext dataContext) : base(dataContext)
        {
        }
    }
}
