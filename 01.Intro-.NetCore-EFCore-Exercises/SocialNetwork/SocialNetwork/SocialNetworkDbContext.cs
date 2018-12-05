
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Data;
using SocialNetwork.Models;

namespace SocialNetwork.Data
{

    public class SocialNetworkDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserFriends> UserFriends { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<PictureAlbums> PictureAlbums { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.ConnectionString);
            }

            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {


            builder
                .Entity<User>()
                .HasMany(u => u.Friends)
                .WithOne(f => f.User)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(f => f.UserId);

            builder
                .Entity<User>()
                .HasMany(u => u.Albums)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);

            builder
                .Entity<UserFriends>()
                .HasKey(uf => new {uf.UserId, uf.FriendId});

            builder
                .Entity<PictureAlbums>()
                .HasKey(pa => new {pa.PictureId, pa.AlbumId});

            builder
                .Entity<Album>()
                .HasMany(a => a.Pictures)
                .WithOne(p => p.Album)
                .HasForeignKey(p => p.AlbumId);


            builder
                .Entity<Picture>()
                .HasMany(p => p.Albums)
                .WithOne(p => p.Picture)
                .HasForeignKey(p => p.PictureId);
                

                

            base.OnModelCreating(builder);
        }
    }
}
