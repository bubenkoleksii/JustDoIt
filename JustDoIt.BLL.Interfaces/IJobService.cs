using JustDoIt.BLL.Models.Request;
using JustDoIt.BLL.Models.Response;

namespace JustDoIt.BLL.Interfaces;

public interface IJobService
{
    public Task<ICollection<JobModelResponse>> GetAll(bool sortByDueDate = true);

    public Task Add(JobModelRequest job);

    public Task Remove(Guid id);

    public Task Check(Guid id);
}