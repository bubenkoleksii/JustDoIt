using System.Xml;
using JustDoIt.DAL.Entities.Request;
using JustDoIt.DAL.Entities.Response;
using JustDoIt.DAL.Interfaces;

namespace JustDoIt.DAL.Implementations.Repositories;

public class JobXmlRepository : IJobRepository
{
    private readonly string _categoryStoragePath;
    private readonly string _jobStoragePath;

    public JobXmlRepository(XmlConnectionFactory connectionFactory)
    {
        _jobStoragePath = connectionFactory.GetJobStoragePath();
        _categoryStoragePath = connectionFactory.GetCategoryStoragePath();
    }

    public async Task<IEnumerable<JobEntityResponse>> GetAll(bool sortByDueDate = true)
    {
        var document = new XmlDocument();
        document.Load(_jobStoragePath);

        var jobs = document.SelectNodes("/Jobs/Job");
        if (jobs == null)
            return null;

        var jobsResponse = new List<JobEntityResponse>();
        foreach (XmlNode job in jobs)
        {
            var jobResponse = ParseXmlToJob(job);
            jobResponse.DateDifferenceInMinutes = (int)(jobResponse.DueDate - DateTime.Now).TotalMinutes;
            jobResponse.CategoryName = GetCategoryName(jobResponse.CategoryId);

            jobsResponse.Add(jobResponse);
        }

        return jobsResponse.OrderBy(j => j.IsCompleted)
            .ThenBy(j => j.DateDifferenceInMinutes);
    }

    public async Task<IEnumerable<JobEntityResponse>> GetByCategory(Guid categoryId, bool sortByDueDate = true)
    {
        var document = new XmlDocument();
        document.Load(_jobStoragePath);

        var jobs = document.SelectNodes($"/Jobs/Job[@CategoryId='{categoryId}']");
        if (jobs == null)
            return null;

        var jobsResponse = new List<JobEntityResponse>();
        foreach (XmlNode job in jobs)
        {
            var jobResponse = ParseXmlToJob(job);
            jobResponse.DateDifferenceInMinutes = (int)(DateTime.Now - jobResponse.DueDate).TotalMinutes;
            jobResponse.CategoryName = GetCategoryName(jobResponse.CategoryId);

            jobsResponse.Add(jobResponse);
        }

        return jobsResponse.OrderBy(j => j.IsCompleted)
            .ThenBy(j => j.DateDifferenceInMinutes);
    }

    public async Task<JobEntityResponse> GetOneById(Guid id)
    {
        var document = new XmlDocument();
        document.Load(_jobStoragePath);

        var jobXml = document.SelectSingleNode($"Jobs/Job[@Id='{id}']");
        if (jobXml == null)
            return null;

        var jobResponse = ParseXmlToJob(jobXml);
        jobResponse.DateDifferenceInMinutes = (int)(DateTime.Now - jobResponse.DueDate).TotalMinutes;
        jobResponse.CategoryName = GetCategoryName(jobResponse.CategoryId);

        return jobResponse;
    }

    public async Task Add(JobEntityRequest job)
    {
        var document = new XmlDocument();
        document.Load(_jobStoragePath);

        var jobXml = document.CreateElement("Job");
        var id = Guid.NewGuid();

        jobXml.SetAttribute("Id", id.ToString());
        jobXml.SetAttribute(nameof(job.CategoryId), job.CategoryId.ToString());
        jobXml.SetAttribute(nameof(job.Name), job.Name);
        jobXml.SetAttribute(nameof(job.DueDate), job.DueDate.ToString());
        jobXml.SetAttribute(nameof(job.IsCompleted), job.IsCompleted.ToString());

        document.DocumentElement.AppendChild(jobXml);
        document.Save(_jobStoragePath);
    }

    public async Task Remove(Guid id)
    {
        var document = new XmlDocument();
        document.Load(_jobStoragePath);

        var jobXml = document.SelectSingleNode($"Jobs/Job[@Id='{id}']");
        if (jobXml == null)
            return;

        document.DocumentElement.RemoveChild(jobXml);
        document.Save(_jobStoragePath);
    }

    public async Task Check(Guid id)
    {
        var document = new XmlDocument();
        document.Load(_jobStoragePath);

        var jobXml = (XmlElement?)document.SelectSingleNode($"Jobs/Job[@Id='{id}']");
        jobXml.SetAttribute("IsCompleted", true.ToString());

        document.Save(_jobStoragePath);
    }

    public async Task Uncheck(Guid id)
    {
        var document = new XmlDocument();
        document.Load(_jobStoragePath);

        var jobXml = (XmlElement?)document.SelectSingleNode($"Jobs/Job[@Id='{id}']");
        jobXml.SetAttribute("IsCompleted", false.ToString());

        document.Save(_jobStoragePath);
    }

    private JobEntityResponse ParseXmlToJob(XmlNode jobXml)
    {
        var job = new JobEntityResponse();
        job.Id = Guid.Parse(jobXml.Attributes[nameof(job.Id)].Value);
        job.Name = jobXml.Attributes[nameof(job.Name)].Value;
        job.CategoryId = Guid.Parse(jobXml.Attributes[nameof(job.CategoryId)].Value);
        job.DueDate = DateTime.Parse(jobXml.Attributes[nameof(job.DueDate)].Value);
        job.IsCompleted = bool.Parse(jobXml.Attributes[nameof(job.IsCompleted)].Value);

        return job;
    }

    private string GetCategoryName(Guid categoryId)
    {
        var document = new XmlDocument();
        document.Load(_categoryStoragePath);

        var categoryXml = document.SelectSingleNode($"Categories/Category[@Id='{categoryId}']");
        if (categoryXml == null)
            return null;

        return categoryXml.Attributes["Name"].Value;
    }
}