using EmployeePro.Dal;
using EmployeePro.Dal.Entities;
using EmployeePro.Dal.Providers;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace EmployeePro.Dall.Test;

public class RepositoryTests
{
    private readonly ApplicationContext _applicationContext;
    private readonly Repository<DepartmentEntity> _repository;

    public RepositoryTests()
    {
        var builder = new DbContextOptionsBuilder<ApplicationContext>();
        builder.UseInMemoryDatabase(GetType().Name);

        _applicationContext = new ApplicationContext(builder.Options);
        _applicationContext.Database.EnsureDeleted();
        _applicationContext.Database.EnsureCreated();
        _repository = new Repository<DepartmentEntity>(_applicationContext);
    }

    [Fact]
    public async Task GetAll()
    {
        // Arrange
        DepartmentEntity[] listToAdd =
        {
            new() { Title = "Starks" },
            new() { Title = "Lanisters" },
            new() { Title = "Martels" }
        };

        await _applicationContext.DepartmentEntities.AddRangeAsync(listToAdd);
        await _applicationContext.SaveChangesAsync();

        // Act
        var getAllResult = await _repository.GetAll();

        // Assert
        getAllResult.Should().BeEquivalentTo(listToAdd);
    }

    [Fact]
    public async Task GetById()
    {
        // Arrange
        var expected = new DepartmentEntity { Title = "Unsullied" };
        await _applicationContext.AddAsync(expected);
        await _applicationContext.SaveChangesAsync();

        // Act
        var actual = await _repository.GetById(expected.Id);

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task Create()
    {
        // Arrange
        var expected = new DepartmentEntity { Title = "Unsullied" };

        // Act
        await _repository.Create(expected);

        // Assert
        var actual = await _applicationContext.DepartmentEntities
            .FirstAsync(x => x.Id == expected.Id);

        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task Update()
    {
        // Arrange
        var initial = new DepartmentEntity { Title = "Unsullied" };
        await _applicationContext.AddAsync(initial);
        await _applicationContext.SaveChangesAsync();

        var afterChangeExpected = await _applicationContext.DepartmentEntities
            .FirstAsync(x => x.Id == initial.Id);

        // Act
        afterChangeExpected.Title = "Sullied";
        await _repository.Update(afterChangeExpected);

        // Assert
        var afterChangeActual = await _applicationContext.DepartmentEntities
            .FirstAsync(x => x.Id == initial.Id);

        afterChangeActual.Should().BeEquivalentTo(afterChangeExpected);

    }

    [Fact]
    public async Task Delete()
    {
        // Arrange
        var initial = new DepartmentEntity { Title = "Unsullied" };
        await _applicationContext.AddAsync(initial);
        await _applicationContext.SaveChangesAsync();

        // Act
        await _repository.Delete(initial.Id);

        // Assert
        var ifExistAfterDelete = _applicationContext.DepartmentEntities
            .Any(x => x.Id == initial.Id);

        Assert.False(ifExistAfterDelete);
    }
}