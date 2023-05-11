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
        try
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
        catch
        {
            return null;
        }
    }

    public async Task<CategoryEntityResponse> GetOneById(Guid id)
    {
        try
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
        catch
        {
            return null;
        }
    }

    public async Task<CategoryEntityResponse> GetOneByName(string name)
    {
        try
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
        catch
        {
            return null;
        }
    }

    public async Task Add(CategoryEntityRequest category)
    {
        try
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
        catch (Exception exception)
        {
            throw exception;
        }
    }

    public async Task Remove(Guid id)
    {
        try
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
        catch (Exception exception)
        {
            throw exception;
        }
    }

    private CategoryEntityResponse ParseXmlToCategory(XmlNode categoryXml)
    {
        try
        {
            var category = new CategoryEntityResponse();
            category.Id = Guid.Parse(categoryXml.Attributes[nameof(category.Id)].Value);
            category.Name = categoryXml.Attributes[nameof(category.Name)].Value;

            return category;
        }
        catch (Exception exception)
        {
            throw exception;
        }
    }

    private int GetCountOfJobsInCategory(Guid id)
    {
        try
        {
            var document = new XmlDocument();
            document.Load(_jobStoragePath);

            var jobs = document.SelectNodes($"/Jobs/Job[@CategoryId='{id}']");
            return jobs.Count;
        }
        catch (Exception exception)
        {
            throw exception;
        }
    }
}