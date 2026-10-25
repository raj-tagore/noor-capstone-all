using System.Linq;
using MusicLibraryDAL.Models;

namespace MusicLibraryDAL
{
    public class MusicLibraryRepository
    {
        MusicLibraryDbContext context;

        public MusicLibraryRepository(MusicLibraryDbContext context)
        {
            this.context = context;
        }

        //  1  song moved; old album count - 1, new album count + 1
        // -1  song name is not in Songs
        // -2  new album name is not in Albums
        // -3  new album is full (CurrentSongs >= TotalSongs)
        // -99 exception
        public int UpdateSongAlbum(string songName, string newAlbumName)
        {
            int result;
            try
            {
                Song song = (from s in context.Songs
                             where s.SongName == songName
                             select s).FirstOrDefault();

                if (song == null)
                {
                    result = -1;
                }
                else
                {
                    Album oldAlbum = context.Albums.Find(song.AlbumName);
                    Album newAlbum = context.Albums.Find(newAlbumName);

                    if (newAlbum != null)
                    {
                        if (newAlbum.CurrentSongs < newAlbum.TotalSongs)
                        {
                            song.AlbumName = newAlbumName;
                            oldAlbum.CurrentSongs = oldAlbum.CurrentSongs - 1;
                            newAlbum.CurrentSongs = newAlbum.CurrentSongs + 1;
                            context.SaveChanges();
                            result = 1;
                        }
                        else
                        {
                            result = -3;
                        }
                    }
                    else
                    {
                        result = -2;
                    }
                }
            }
            catch (Exception)
            {
                result = -99;
            }
            return result;
        }
    }
}
