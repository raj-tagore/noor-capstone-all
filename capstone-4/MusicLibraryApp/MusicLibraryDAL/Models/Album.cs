using System.Collections.Generic;

namespace MusicLibraryDAL.Models;

public partial class Album
{
    public string AlbumName { get; set; }

    public int TotalSongs { get; set; }

    public int CurrentSongs { get; set; }

    public virtual ICollection<Song> Songs { get; set; } = new List<Song>();
}
