namespace EmployeePro.Dal.Entities.Abstract;

public abstract class BaseEntity
{
    public virtual Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreationDateTime { get; set; } = DateTime.Now.ToUniversalTime();
}