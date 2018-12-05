
using SocialNetwork.Models;

namespace SocialNetwork.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Album
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string BackgroundColor { get; set; }

        public bool IsPublic { get; set; }

        public List<PictureAlbums> Pictures { get; set; } = new List<PictureAlbums>();

        public User User { get; set; }

        public int UserId { get; set; }
    }
}
