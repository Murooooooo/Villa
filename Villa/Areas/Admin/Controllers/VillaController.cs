using Microsoft.AspNetCore.Mvc;

namespace Villa.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]

    public class VillaController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public VillaController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            var Villas = _context.Villas.ToList();
            return View(Villas);
        }


        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Villa.Models.Villa villa, IFormFile file)
        {
            string filename = Guid.NewGuid() + file.FileName;
            var path = Path.Combine(_env.WebRootPath, "Upload", filename);
            using FileStream fileStream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(fileStream);
            villa.PhotoUrl = "/Upload/"+filename;
            await _context.Villas.AddAsync(villa);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            var villa = _context.Villas.FirstOrDefault(x => x.Id == id);
            if (villa == null) return NotFound();
            return View(villa);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Villa.Models.Villa villa, IFormFile file,int id)
        {
            var oldVilla = _context.Villas.FirstOrDefault(x => x.Id == id);
            if (villa == null) return NotFound();
            if(!ModelState.IsValid)
            {
                return View();
            }
            if (file != null)
            {
                string filename = Guid.NewGuid() + file.FileName;
                var path = Path.Combine(_env.WebRootPath, "Upload", filename);
                using FileStream fileStream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(fileStream);
                villa.PhotoUrl = "/Upload/"+filename;
            }
            else
            {
                villa.PhotoUrl = oldVilla.PhotoUrl;
            }
            villa.Location = villa.Location;
            villa.Price = villa.Price;
            villa.PhotoUrl = villa.PhotoUrl;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            var villa = _context.Villas.FirstOrDefault(x => x.Id == id);
            if (villa == null) return NotFound();
            _context.Villas.Remove(villa);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
