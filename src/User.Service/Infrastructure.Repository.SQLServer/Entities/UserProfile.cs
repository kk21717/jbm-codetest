using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Infrastructure.Repository.SQLServer.Entities;

public class UserProfile:Domain.Entities.UserProfile
{ 

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public new int UserId { get; set; }

    [MaxLength(150)]
    public new string FirstName { get; set; }

    [MaxLength(150)]
    public new string LastName { get; set; }


}