using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;

namespace ipan;


public class Service
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int ClientID { get; set; }
    public int OutgoingInvoiceID { get; set; }
    public int OperatorID { get; set; }
}

public class AppDbContext : DbContext
{
    public DbSet<Service> Services { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=d:/servicesdatabase.db");
            optionsBuilder.EnableSensitiveDataLogging(); // true is default
        }

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure your entities and relationships here
        modelBuilder.Entity<Service>().ToTable("Services");
        // Add any additional configurations as needed
        //  this.Database.EnsureCreated(); // hm?
    }
}

public class ServiceRepository
{
    private readonly AppDbContext _dbContext;
    private readonly AppDbContext _queryContext;

    public ServiceRepository()
    {
        _dbContext = new AppDbContext();
        _queryContext = new AppDbContext();
    }

    public void CreateService(Service service)
    {
        _dbContext.Services.Add(service);
        _dbContext.SaveChanges();
    }

    public List<Service> GetAllServices()
    {
        var services = _queryContext.Services.ToList();
        DetachEntities(_dbContext);
        return services;
    }

    public void UpdateService(Service service)
    {
        _dbContext.Services.Update(service);
        _dbContext.SaveChanges();
    }

    public void DeleteService(int serviceID)
    {
        var service = _dbContext.Services.FirstOrDefault(s => s.ID == serviceID);
        if (service != null)
        {
            _dbContext.Services.Remove(service);
            _dbContext.SaveChanges();
        }
    }

        private void DetachEntities(AppDbContext context)
    {
        foreach (var entry in context.ChangeTracker.Entries())
        {
            entry.State = EntityState.Detached;
        }
    }
}


