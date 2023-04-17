#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;
namespace ChefsNDishes.Models;

public class Chef
{
    [Key]
    public int ChefId { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    [MinimumAge(18, ErrorMessage ="You must be eighteen or older to register as a Chef")]
    public DateTime DateOfBirth { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public List<Dish> CreatedDishs { get; set; } = new List<Dish>();

    public int Age
    {
        get
        {
            DateTime now = DateTime.Today;
            int age = now.Year - DateOfBirth.Year;
            if (DateOfBirth > now.AddYears(-age)) age--;
            return age;
        }
    }
}

public class MinimumAgeAttribute : ValidationAttribute
{
    int _minimumAge;

    public MinimumAgeAttribute(int minimumAge)
    {
        _minimumAge = minimumAge;
    }

    public override bool IsValid(object value)
    {
        DateTime date;

        if (DateTime.TryParse(value.ToString(), out date))
        {
            return date.AddYears(_minimumAge) < DateTime.Now;
        }

        return false;
    }
}