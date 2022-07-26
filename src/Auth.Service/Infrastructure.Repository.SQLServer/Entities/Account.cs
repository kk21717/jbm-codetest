using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Infrastructure.Repository.SQLServer.Entities;

public class Account
{
    public Account()
    {
        Phone = string.Empty;
        Email = string.Empty;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
        
    public string Phone { get; set; }

    public string Email { get; set; }
}