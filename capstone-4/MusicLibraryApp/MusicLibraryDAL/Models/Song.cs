namespace MusicLibraryDAL.Models;

public partial class Song
{
    public int SongId { get; set; }

    public string SongName { get; set; }

    public string AlbumName { get; set; }

    public virtual Album Album { get; set; }
}
