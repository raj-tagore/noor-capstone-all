using MusicLibraryDAL;
using MusicLibraryDAL.Models;

namespace MusicLibraryApp.ConsoleApp
{
    public class Program
    {
        static MusicLibraryDbContext context;
        static MusicLibraryRepository repository;

        static Program()
        {
            context = new MusicLibraryDbContext();
            repository = new MusicLibraryRepository(context);
        }

        static void Main(string[] args)
        {
            TestSongNotInDatabase();
            TestNewAlbumDoesNotExist();
            TestNoSpaceInNewAlbum();
            TestUpdateSongAlbumSuccess();
            PrintLibrary();
        }

        public static void TestSongNotInDatabase()
        {
            int result = repository.UpdateSongAlbum("Nowhere Man", "Help");
            Console.WriteLine("Song not in database: " + result + " (expected -1)");
        }

        public static void TestNewAlbumDoesNotExist()
        {
            int result = repository.UpdateSongAlbum("Yesterday", "White Album");
            Console.WriteLine("New album does not exist: " + result + " (expected -2)");
        }

        public static void TestNoSpaceInNewAlbum()
        {
            int result = repository.UpdateSongAlbum("Something", "Revolver");
            Console.WriteLine("No space in new album: " + result + " (expected -3)");
        }

        public static void TestUpdateSongAlbumSuccess()
        {
            int result = repository.UpdateSongAlbum("Come Together", "Help");
            if (result == 1)
            {
                Console.WriteLine("Come Together moved to Help. Result = 1.");
            }
            else
            {
                Console.WriteLine("Update failed. Result = " + result + " (expected 1)");
            }
        }

        public static void PrintLibrary()
        {
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("{0,-20}{1,-12}{2,-12}", "AlbumName", "Current", "Total");
            Console.WriteLine("----------------------------------------------------------");
            foreach (Album album in context.Albums.OrderBy(a => a.AlbumName))
            {
                Console.WriteLine("{0,-20}{1,-12}{2,-12}", album.AlbumName, album.CurrentSongs, album.TotalSongs);
            }

            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("{0,-20}{1,-20}", "SongName", "AlbumName");
            Console.WriteLine("----------------------------------------------------------");
            foreach (Song song in context.Songs.OrderBy(s => s.SongName))
            {
                Console.WriteLine("{0,-20}{1,-20}", song.SongName, song.AlbumName);
            }
        }
    }
}
