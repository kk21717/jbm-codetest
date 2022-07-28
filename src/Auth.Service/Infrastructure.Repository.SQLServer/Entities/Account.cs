using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Repository.SQLServer.Entities;


[Index(nameof(Phone), IsUnique = true)]
public class Account:Domain.Entities.Account
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
    public new string Phone { get; set; }

    [MaxLength(150)]
    public new string Email { get; set; }

}