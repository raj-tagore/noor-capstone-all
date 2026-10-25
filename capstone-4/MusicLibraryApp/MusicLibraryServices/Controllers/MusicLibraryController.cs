using Microsoft.AspNetCore.Mvc;
using MusicLibraryDAL;

namespace MusicLibraryServices.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MusicLibraryController : Controller
    {
        MusicLibraryRepository rep;

        public MusicLibraryController(MusicLibraryRepository repository)
        {
            rep = repository;
        }

        #region UpdateSongAlbum - Do not modify the signature
        [HttpPut]
        public int UpdateSongAlbum(string songName, string newAlbumName)
        {
            int result;
            try
            {
                result = rep.UpdateSongAlbum(songName, newAlbumName);
            }
            catch (Exception)
            {
                result = -99;
            }
            return result;
        }
        #endregion
    }
}
