using SocialNetwork.Models;

namespace SocialNetwork.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Picture
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Caption { get; set; }

        public string Path { get; set; }

        public List<PictureAlbums> Albums { get; set; } = new List<PictureAlbums>();
    }
}
