using EmployeePro.Bll.Dtos;
using EmployeePro.Bll.Services;
using EmployeePro.Dal;
using EmployeePro.Dal.Entities;
using EmployeePro.Dal.Providers;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace EmployeePro.Bll.Test;

public class DepartmentManagerTests
{
    private readonly ApplicationContext _applicationContext;
    private readonly Repository<EmployeeEntity> _employeeProvider;
    private readonly Repository<DepartmentEntity> _departmentProvider;

    public DepartmentManagerTests()
    {
        var builder = new DbContextOptionsBuilder<ApplicationContext>();
        builder.UseInMemoryDatabase(GetType().Name);

        _applicationContext = new ApplicationContext(builder.Options);
        _applicationContext.Database.EnsureDeleted();
        _applicationContext.Database.EnsureCreated();

        _employeeProvider = new Repository<EmployeeEntity>(_applicationContext);
        _departmentProvider = new Repository<DepartmentEntity>(_applicationContext);
    }

    [Fact]
    public async void CreateDepartment_ShouldCreateDepartmentItInDb_PositiveTest()
    {
        // Arrange
        var departmentManager = new DepartmentManager(_departmentProvider, _employeeProvider);
        var departmentDto = new DepartmentDto
        {
            Title = "Golder swords"
        };

        // Act
        await departmentManager.CreateDepartment(departmentDto);

        // Assert
        var departmentEntity = await _applicationContext.DepartmentEntities.FirstAsync();
        Assert.Equal(departmentDto.Title, departmentEntity.Title);
    }

    [Fact]
    public async void DeleteDepartment_ShouldDeleteDepartmentByIdInDb_PositiveTest()
    {
        // Arrange
        var departmentManager = new DepartmentManager(_departmentProvider, _employeeProvider);
        var departmentEntity = new DepartmentEntity
        {
            Title = "Golder swords"
        };

        await _applicationContext.DepartmentEntities.AddAsync(departmentEntity);
        await _applicationContext.SaveChangesAsync();

        // Act
        await departmentManager.DeleteDepartment(departmentEntity.Id);

        // Assert
        var actualDepartmentEntity = await _applicationContext.DepartmentEntities
            .FirstOrDefaultAsync(x => x.Id == departmentEntity.Id);

        Assert.Null(actualDepartmentEntity);
    }

    [Fact]
    public async void GetAllDepartments_ShouldGetAllDepartment_PositiveTest()
    {
        // Arrange
        var departmentManager = new DepartmentManager(_departmentProvider, _employeeProvider);
        var departmentEntity = new DepartmentEntity[]
        {
            new DepartmentEntity
            {
                Title = "Golder swords"
            },
            new DepartmentEntity
            {
                Title = "Silver swords"
            }
        };

        await _applicationContext.DepartmentEntities.AddRangeAsync(departmentEntity);
        await _applicationContext.SaveChangesAsync();

        // Act
        var departmentDtos = await departmentManager.GetAllDepartments();

        // Assert
        departmentDtos
            .Select(x => x.Title)
            .Should()
            .BeEquivalentTo(departmentEntity.Select(x => x.Title));
    }

    [Fact]
    public async void ChangeDepartment_ShouldChangeDepartmentInDb_PositiveTest()
    {
        // Arrange
        var departmentManager = new DepartmentManager(_departmentProvider, _employeeProvider);
        var departmentEntity = new DepartmentEntity
        {
            Title = "Golder swords"
        };
        var departmentDto = new DepartmentDto
        {
            Title = "Silver swords",
            Id = departmentEntity.Id
        };

        await _applicationContext.DepartmentEntities.AddAsync(departmentEntity);
        await _applicationContext.SaveChangesAsync();

        // Act
        await departmentManager.ChangeDepartment(departmentDto);

        // Assert
        var updateDepartmentEntity = await _applicationContext.DepartmentEntities.FirstAsync();
        Assert.Equal(departmentDto.Title, updateDepartmentEntity.Title);
    }

    [Fact]
    public async void AddEmployeeToDepartment_ShouldAddEmployeeToDepartment_PositiveTest()
    {
        // Arrange
        var departmentManager = new DepartmentManager(_departmentProvider, _employeeProvider);
        var departmentEntity = new DepartmentEntity
        {
            Title = "Golder swords"
        };
        var employeeEntity = new EmployeeEntity
        {
            Fullname = "Jester",
            Email = "cirk",
            PasswordHash = "asd"
        };

        await _applicationContext.DepartmentEntities.AddAsync(departmentEntity);
        await _applicationContext.EmployeeEntities.AddAsync(employeeEntity);
        await _applicationContext.SaveChangesAsync();

        // Act
        await departmentManager.AddEmployeeToDepartment(employeeEntity.Id, departmentEntity.Id);

        // Assert
        var actualEmployeeEntity = await _applicationContext
            .EmployeeEntities
            .FirstAsync(x => x.Id == employeeEntity.Id);

        var actualDepartmentEntity = await _applicationContext
            .DepartmentEntities
            .FirstAsync(x => x.Id == departmentEntity.Id);

        Assert.Equal(actualDepartmentEntity, actualEmployeeEntity.DepartmentEntity);
    }

    [Fact]
    public async Task
        DeleteEmployeeFromDepartment_ShouldDeleteDepartmentEntityAndDepartmentIdInEmployeeEntity_PositiveTest()
    {
        // Arrange
        var departmentManager = new DepartmentManager(_departmentProvider, _employeeProvider);
        var departmentEntity = new DepartmentEntity
        {
            Title = "Golder swords"
        };
        var employeeEntity = new EmployeeEntity
        {
            Fullname = "Jester",
            Email = "cirk",
            PasswordHash = "asd",
            DepartmentEntity = departmentEntity,
            DepartmentId = departmentEntity.Id
        };

        await _applicationContext.DepartmentEntities.AddAsync(departmentEntity);
        await _applicationContext.EmployeeEntities.AddAsync(employeeEntity);
        await _applicationContext.SaveChangesAsync();
        
        // Act
        await departmentManager.DeleteEmployeeFromDepartment(departmentEntity.Id, employeeEntity.Id);
        
        // Assert
        var actualEmployeeEntity = await _applicationContext
            .EmployeeEntities
            .FirstAsync(x => x.Id == employeeEntity.Id);
        
        Assert.Null(actualEmployeeEntity.DepartmentEntity);
        Assert.Null(actualEmployeeEntity.DepartmentId);
    }
}