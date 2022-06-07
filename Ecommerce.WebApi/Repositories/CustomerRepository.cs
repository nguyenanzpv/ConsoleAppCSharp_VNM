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
            return customerCache.AddOrUpdate(c.CustomerId,c, UpdateCached);
        }
        else
        {
            return null;
        }
    }

    public async Task<bool?> DeleteAsync(string id)
    {
        //for best performance -> get from cached(.net cache)
        id = id.ToUpper();
        //remove from db
        Customer? c = db.Customers.Find(id);//out, ref, params
        if (c is null) return null!;
        db.Customers.Remove(c);// remove c from Customers
        int effected = await db.SaveChangesAsync();// affected db
        if(effected == 1)
        {
            if (customerCache is null) return null;
            return customerCache.TryRemove(id, out c);// remove from cache
        }
        else
        {
            return null;
        }
    }

    public Task<IEnumerable<Customer>> RetrieveAllAsync()
    {
        //for best performance -> get from cached(.net cache)
        return Task.FromResult(customerCache is null ? Enumerable.Empty<Customer>() : customerCache.Values);
    }

    public Task<Customer?> RetrieveAsync(string id)
    {
        //for best performance -> get from cached(.net cache)
        id = id.ToUpper();
        if (customerCache is null) return null!;
        customerCache.TryGetValue(id, out Customer? c);
        return Task.FromResult(c);
    }

    public async Task<Customer?> UpdateAsync(string id, Customer c)
    {
        //for best performance -> get from cached(.net cache)
        id = id.ToUpper();//old
        c.CustomerId = c.CustomerId.ToUpper();
        //update vao db bang EFCore
        db.Customers.Update(c);
        int effected = await db.SaveChangesAsync();
        if(effected == 1)
        {
            //update in cached
            return UpdateCached(id, c);
        }
        return null;
    }

    private Customer UpdateCached(string id, Customer c)
    {
        Customer? old;
        if(customerCache is not null)
        {
            if(customerCache.TryGetValue(id, out old))//out de giu thong tin va tra ra
            {
                if (customerCache.TryUpdate(id, c,old))//update c vao old qua id
                {
                    return c;
                }
            }
        }
        return null;
    }
    
}
