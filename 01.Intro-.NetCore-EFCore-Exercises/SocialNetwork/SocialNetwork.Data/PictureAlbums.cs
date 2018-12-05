namespace SocialNetwork.Models
{
    public class PictureAlbums
    {
        public Picture Picture { get; set; }
        public int PictureId { get; set; }

        public Album Album { get; set; }
        public int AlbumId { get; set; }

    }
}
