using System.Xml;
using JustDoIt.DAL.Entities.Request;
using JustDoIt.DAL.Entities.Response;
using JustDoIt.DAL.Interfaces;

namespace JustDoIt.DAL.Implementations.Repositories;

public class CategoryXmlRepository : ICategoryRepository
{
    private readonly string _categoryStoragePath;

    private readonly string _jobStoragePath;

    public CategoryXmlRepository(XmlConnectionFactory connectionFactory)
    {
        _categoryStoragePath = connectionFactory.GetCategoryStoragePath();
        _jobStoragePath = connectionFactory.GetJobStoragePath();
    }

    public async Task<IEnumerable<CategoryEntityResponse>> GetAll()
    {
        var document = new XmlDocument();
        document.Load(_categoryStoragePath);

        var categories = document.SelectNodes("/Categories/Category");
        if (categories == null)
            return null;

        var categoriesResponse = new List<CategoryEntityResponse>();
        foreach (XmlNode category in categories)
        {
            var categoryResponse = ParseXmlToCategory(category);
            categoryResponse.CountOfJobs = GetCountOfJobsInCategory(categoryResponse.Id);

            categoriesResponse.Add(categoryResponse);
        }

        return categoriesResponse;
    }

    public async Task<CategoryEntityResponse> GetOneById(Guid id)
    {
        var document = new XmlDocument();
        document.Load(_categoryStoragePath);

        var categoryXml = document.SelectSingleNode($"Categories/Category[@Id='{id}']");
        if (categoryXml == null)
            return null;

        var categoryResponse = ParseXmlToCategory(categoryXml);
        categoryResponse.CountOfJobs = GetCountOfJobsInCategory(categoryResponse.Id);

        return categoryResponse;
    }

    public async Task<CategoryEntityResponse> GetOneByName(string name)
    {
        var document = new XmlDocument();
        document.Load(_categoryStoragePath);

        var categoryXml = document.SelectSingleNode($"Categories/Category[@Name={name}]");
        if (categoryXml == null)
            return null;

        var categoryResponse = ParseXmlToCategory(categoryXml);
        categoryResponse.CountOfJobs = GetCountOfJobsInCategory(categoryResponse.Id);

        return categoryResponse;
    }

    public async Task Add(CategoryEntityRequest category)
    {
        var document = new XmlDocument();
        document.Load(_categoryStoragePath);

        var categoryXml = document.CreateElement("Category");
        var id = Guid.NewGuid();

        categoryXml.SetAttribute("Id", id.ToString());
        categoryXml.SetAttribute(nameof(category.Name), category.Name);

        document.DocumentElement.AppendChild(categoryXml);
        document.Save(_categoryStoragePath);
    }

    public async Task Remove(Guid id)
    {
        var document = new XmlDocument();
        document.Load(_categoryStoragePath);

        var categoryXml = document.SelectSingleNode($"Categories/Category[@Id='{id}']");
        if (categoryXml == null)
            return;

        document.DocumentElement.RemoveChild(categoryXml);
        document.Save(_categoryStoragePath);

        document.Load(_jobStoragePath);
        var jobs = document.SelectNodes($"/Jobs/Job[@CategoryId='{id}']");
        foreach (XmlNode job in jobs) document.DocumentElement.RemoveChild(job);
        document.Save(_jobStoragePath);
    }

    private CategoryEntityResponse ParseXmlToCategory(XmlNode categoryXml)
    {
        var category = new CategoryEntityResponse();
        category.Id = Guid.Parse(categoryXml.Attributes[nameof(category.Id)].Value);
        category.Name = categoryXml.Attributes[nameof(category.Name)].Value;

        return category;
    }

    private int GetCountOfJobsInCategory(Guid id)
    {
        var document = new XmlDocument();
        document.Load(_jobStoragePath);

        var jobs = document.SelectNodes($"/Jobs/Job[@CategoryId='{id}']");
        return jobs.Count;
    }
}