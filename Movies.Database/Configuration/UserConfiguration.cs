using Movies.Models;
using System.Data.Entity.ModelConfiguration;

namespace Movies.Database.Configuration
{
    /// <summary>
    /// Entity Framework Class responsible to configure the User Table of the database
    /// All the constraints and relevant configuration must be explicitly defined by it.
    /// </summary>
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            this.HasKey(u => u.Id)
              .Property(u => u.Id)
              .IsRequired();

            this.Property(u => u.Login)
                .HasMaxLength(100)
                .IsRequired();

            this.Property(u => u.Password)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}

