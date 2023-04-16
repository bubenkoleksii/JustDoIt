using JustDoIt.BLL.Models.Response;

namespace JustDoIt.BLL.Interfaces;

public interface IJobService
{
    public Task<ICollection<JobModelResponse>> GetAll(bool sortByStatus = true, bool sortByDueDate = true);
}