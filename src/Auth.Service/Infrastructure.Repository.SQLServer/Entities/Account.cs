using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Repository.SQLServer.Entities;


[Index(nameof(Phone), IsUnique = true)]
public class Account
{
    public Account()
    {
        Phone = string.Empty;
        Email = string.Empty;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserId { get; set; }

    [MaxLength(30)]
    public string Phone { get; set; }

    [MaxLength(150)]
    public string Email { get; set; }

}