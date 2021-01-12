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
    public class SongsController : Controller
    {
        private readonly SpotifyContext _context;

        public SongsController(SpotifyContext context)
        {
            _context = context;
        }

        // GET: Songs
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";
            ViewData["CurrentSort"] = sortOrder;

            if (searchString != null) 
            {
                pageNumber = 1; 
            }
            else 
            {
                searchString = currentFilter; 
            }
            ViewData["CurrentFilter"] = searchString;
            var songs = from b in _context.Songs
                        select b;
            if (!String.IsNullOrEmpty(searchString)) 
            {
                songs = songs.Where(s => s.Title.Contains(searchString)); 
            }

            switch (sortOrder)
            {
                case "title_desc": songs = songs.OrderByDescending(b => b.Title);
                    break;
                case "Price": songs = songs.OrderBy(b => b.Price);
                    break;
                case "price_desc": songs = songs.OrderByDescending(b => b.Price);
                    break;
                default:
                    songs = songs.OrderBy(b => b.Title);
                    break;
            }
            int pageSize = 2; 
            return View(await PaginatedList<Song>.CreateAsync(songs.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Songs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // GET: Songs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Songs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Singer,Price")] Song song)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(song);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex*/)
            {
                ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists ");
            }
            return View(song);
        }

        // GET: Songs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albums
                .Include(i => i.AlbumSongs)
                .ThenInclude(i => i.Song)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id); 
            if (album == null) 
            {
                return NotFound(); 
            }

            PopulateAlbumSongData(album); 
            return View(album);
        }
        private void PopulateAlbumSongData(Album album) 
        {
            var allSongs = _context.Songs; 
            var albumSongs = new HashSet<int>(album.AlbumSongs.Select(c => c.SongID));
            var viewModel = new List<AlbumSongData>(); 
            foreach (var song in allSongs) 
            {
                viewModel.Add(new AlbumSongData
                {
                    SongID = song.ID, Title = song.Title, IsAlbum = albumSongs.Contains(song.ID) }); 
            } ViewData["Songs"] = viewModel; 
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentToUpdate = await _context.Songs.FirstOrDefaultAsync(s => s.ID == id);
            if (await TryUpdateModelAsync<Song>(
                studentToUpdate,
                "",
                s => s.Singer, s => s.Title, s => s.Price))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists");
                }
            }
            return View(studentToUpdate);

        }

        // GET: Songs/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (song == null)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] = "Delete failed. Try again";
            }

            return View(song);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var song = await _context.Songs.FindAsync(id);
            if (song == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _context.Songs.Remove(song);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }


        private bool SongExists(int id)
        {
            return _context.Songs.Any(e => e.ID == id);
        }
    }
}
    

