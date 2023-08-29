namespace EmployeePro.Bll.Extentsions;

static class StringExtensions
{
   public static Guid StringToGuid(this string value)
   {
      return new Guid(value);
   } 
}