#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;
namespace ChefsNDishes.Models;

public class Dish
{
    [Key]
    public int DishId { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    [Range(1,9999)]
    public int Calories { get; set; }

    [Required]
    [Range(1,5)]
    public int Tastiness { get; set; }


    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;


    public int ChefId { get; set; }
    public Chef? Chef { get; set; }
}