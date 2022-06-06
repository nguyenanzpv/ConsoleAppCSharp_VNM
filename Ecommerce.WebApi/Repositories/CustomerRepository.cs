using Microsoft.EntityFrameworkCore.ChangeTracking;//quan ly su thay doi cua DB
using SolidEdu.Shared;//customer entity
using System.Collections.Concurrent; //Concurrent Dictionary

namespace Ecommerce.WebApi.Repositories;

public class CustomerRepository : ICustomerRepository
{
    //use a static dictionary field to cache the customers: real will use redis....
    private static ConcurrentDictionary<string, Customer> customerCache;
    //use a instance data context field
    private SolidStoreContext db;//session ket noi xuong db theo moi request

    public CustomerRepository(SolidStoreContext injectedContext)
    {
        this.db = injectedContext;//dependence inject via constructor
        if(customerCache == null)
        {
            customerCache = new ConcurrentDictionary<string, Customer>(
                db.Customers.ToDictionary(c=>c.CustomerId)
            );

        }
    }

    public async Task<Customer?> CreateAsync(Customer c)
    {
        //viet hoa customer
        c.CustomerId = c.CustomerId.ToUpper();
        //add to database via EFCore
        //await de cho task Add xong tra lai ket qua -> await la dong bo cho de nhan ket qua tung task
        EntityEntry<Customer> added = await db.Customers.AddAsync(c);
        //SaveChangesAsync from entityentry to db -> await cho ket qua : 1-success;
        int affected = await db.SaveChangesAsync();
        if(affected == 1)// save db success
        {
            if(customerCache  is null)
            {
                return c;
            }
            //if customer is new -> add to cache else update cache method
            return customerCache.AddOrUpdate(c.CustomerId,c,UpdateCached());
        }
        else
        {
            return null;
        }
    }

    public Task<bool?> DeleteAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Customer?>> RetrieveAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Customer?> RetrieveAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<Customer?> UpdateAsync(string id, Customer c)
    {
        throw new NotImplementedException();
    }

    private Customer UpdateCached(string id, Customer c)
    {
        Customer? old;
        if(customerCache is not null)
        {
            if(customerCache.TryGetValue(id, out old))//out de giu thong tin va tra ra
            {
               
            }
        }
        return old;
    }
    
}
