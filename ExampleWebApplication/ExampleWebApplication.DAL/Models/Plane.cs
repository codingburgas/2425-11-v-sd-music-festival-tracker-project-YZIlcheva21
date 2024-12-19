using System.ComponentModel.DataAnnotations.Schema;

namespace ExampleWebApplication.DAL.Models;

// Database Table Planes
[Table("Planes")]
public class Plane
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    
    
}