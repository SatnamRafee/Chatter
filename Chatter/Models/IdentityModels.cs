using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;

namespace Chatter.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        //Add Followers
        [ScriptIgnore]
        public virtual ICollection<ApplicationUser> Followers { get; set; }
        public virtual ICollection <ApplicationUser> Following { get; set; }

        //Add First name and Last name
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //Add User name
        [Display(Name = "Chat Name")]
        public string ChatName { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        //
        public virtual ICollection<Chat> Chats { get; set; }

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {            
            return new ApplicationDbContext();
        }

        //db set for followers/following
        //public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<ApplicationUser>()
        //        .HasMany(x => x.Followers).WithMany(x => x.Following)
        //        .Map(x => x.ToTable("Followers")
        //            .MapLeftKey("ApplicationUserId")
        //            .MapRightKey("FollowerId"));
        //}

        public System.Data.Entity.DbSet<Chatter.Models.Chat> Chats { get; set; }
    }
}