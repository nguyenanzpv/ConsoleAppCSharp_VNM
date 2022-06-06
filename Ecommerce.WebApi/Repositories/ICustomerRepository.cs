using SolidEdu.Shared; //Get model Customer
namespace Ecommerce.WebApi.Repositories;

public interface ICustomerRepository
{
    Task<Customer?> CreateAsync(Customer c); //Post
    Task<IEnumerable<Customer?>> RetrieveAllAsync();// Get All tra ra 1 tap hop -> dua vao IEnumerable
    Task<Customer?> RetrieveAsync(string id); //Get by id
    Task<Customer?> UpdateAsync(string id, Customer c ); //Put
    Task<bool?>DeleteAsync(string id);//Delete

}
