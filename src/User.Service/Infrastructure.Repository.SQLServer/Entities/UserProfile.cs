using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Infrastructure.Repository.SQLServer.Entities;

public class UserProfile
{ 

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int UserId { get; set; }

    [MaxLength(150)]
    public string FirstName { get; set; }

    [MaxLength(150)]
    public string LastName { get; set; }


}