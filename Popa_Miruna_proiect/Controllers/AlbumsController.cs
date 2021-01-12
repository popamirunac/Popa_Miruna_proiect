using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Popa_Miruna_proiect.Data;
using Popa_Miruna_proiect.Models;
using Popa_Miruna_proiect.Models.SpotifyViewModels;

namespace Popa_Miruna_proiect.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly SpotifyContext _context;

        public AlbumsController(SpotifyContext context)
        {
            _context = context;
        }

        // GET: Albums
        public async Task<IActionResult> Index(int? id, int? songID)
        {
            var viewModel = new AlbumIndexData(); viewModel.Albums = await _context.Albums
                .Include(i => i.AlbumSongs)
                .ThenInclude(i => i.Song)
                .ThenInclude(i => i.Orders)
                .ThenInclude(i => i.Customer)
                .AsNoTracking()
                .OrderBy(i => i.AlbumName)
                .ToListAsync(); 
            if (id != null) 
            {
                ViewData["albumID"] = id.Value; 
                Album album = viewModel.Albums.Where(i => i.ID == id.Value).Single(); 
                viewModel.Songs = album.AlbumSongs.Select(s => s.Song); }
            if (songID != null) { ViewData["SongID"] = songID.Value; 
                viewModel.Orders = viewModel.Songs.Where(x => x.ID == songID).Single().Orders; 
            }
            return View(viewModel);
        }

        // GET: Albums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albums
                .FirstOrDefaultAsync(m => m.ID == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // GET: Albums/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Albums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,AlbumName,Year")] Album album)
        {
            if (ModelState.IsValid)
            {
                _context.Add(album);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(album);
        }

        // GET: Albums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albums.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }
            return View(album);
        }

        // POST: Albums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int? id, string[] selectedSongs) 
        {
            if (id == null) 
            {
                return NotFound(); 
            }
            var albumToUpdate = await _context.Albums
                .Include(i => i.AlbumSongs)
                .ThenInclude(i => i.Song)
                .FirstOrDefaultAsync(m => m.ID == id); 
            if (await TryUpdateModelAsync<Album>(albumToUpdate, "", i => i.AlbumName, i => i.Year)) 
            {
                UpdateAlbumSongs(selectedSongs, albumToUpdate); 
                try
                {
                    await _context.SaveChangesAsync(); 
                }
                catch (DbUpdateException /* ex */) 
                {
                    ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists, ");
                }
                return RedirectToAction(nameof(Index)); 
            }
            UpdateAlbumSongs(selectedSongs, albumToUpdate);
            PopulateAlbumSongData(albumToUpdate); 
            return View(albumToUpdate);
        }

        private void PopulateAlbumSongData(Album albumToUpdate)
        {
            throw new NotImplementedException();
        }

        private void UpdateAlbumSongs(string[] selectedSongs, Album albumToUpdate)
        {
            if (selectedSongs == null)
            {
                albumToUpdate.AlbumSongs = new List<AlbumSong>();
                return;
            }
            var selectedSongsHS = new HashSet<string>(selectedSongs); 
            var albumSongs = new HashSet<int>(albumToUpdate.AlbumSongs.Select(c => c.Song.ID)); 
            foreach (var song in _context.Songs) 
            {
                if (selectedSongsHS.Contains(song.ID.ToString())) 
                {
                    if (!albumSongs.Contains(song.ID)) 
                    {
                        albumToUpdate.AlbumSongs.Add(new AlbumSong 
                        {
                            AlbumID = albumToUpdate.ID, 
                            SongID = song.ID }); 
                    } 
                }
                else 
                {
                    if (albumSongs.Contains(song.ID)) 
                    {
                        AlbumSong bookToRemove = albumToUpdate.AlbumSongs
                            .FirstOrDefault(i => i.SongID == song.ID); 
                        _context.Remove(bookToRemove); } } }
        }

        // GET: Albums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albums
                .FirstOrDefaultAsync(m => m.ID == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var album = await _context.Albums.FindAsync(id);
            _context.Albums.Remove(album);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlbumExists(int id)
        {
            return _context.Albums.Any(e => e.ID == id);
        }
    }
}
