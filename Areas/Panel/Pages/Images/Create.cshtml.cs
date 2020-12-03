using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ComaCuras.web.Data;
using ComaCuras.web.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace ComaCuras.web.Areas.Panel.Pages.Images
{
    public class CreateModel : PageModel
    {
        private readonly ComaCuras.web.Data.ComaCuraswebContext _context;

        public CreateModel(ComaCuras.web.Data.ComaCuraswebContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public BufferedSingleFileUploadDb ImageUpload { get; set; }

        public class BufferedSingleFileUploadDb
        {
            [Required]
            [Display(Name = "File")]
            public IFormFile FormFile { get; set; }
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPost(int? id, string type)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (id == null || string.IsNullOrEmpty(type))
            {
                return NotFound();
            }

            if (!VerifyFile())
            {
                ModelState.AddModelError("File", "fichier Invalid");
                return Page();
            }

            using (var memoryStream = new MemoryStream())
            {
                await ImageUpload.FormFile.CopyToAsync(memoryStream);

                // Upload the file if less than 512 MB
                if (memoryStream.Length < 524288)
                {
                    if (type == "shop")
                    {
                        var shop = await _context.Shop.FindAsync(id);
                        shop.Image = memoryStream.ToArray();
                        _context.Attach(shop).State = EntityState.Modified;
                    }
                    await _context.SaveChangesAsync();
                }
                else
                {
                    ModelState.AddModelError("File", "The file is too large. must be less that 512 kb");
                    return Page();
                }
            }
            return Redirect("/Panel/Manager/Details");
        }

        private bool VerifyFile()
        {
            Dictionary<string, List<byte[]>> fileSignature =
                new Dictionary<string, List<byte[]>>
                {
                    {
                        ".jpeg", new List<byte[]>
                        {
                            new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                            new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
                            new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 },
                        }
                    },
                    {
                        ".jpg", new List<byte[]>
                        {
                            new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                            new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 },
                            new byte[] { 0xFF, 0xD8, 0xFF, 0xE8 },
                        }
                    },
                    {
                        ".png", new List<byte[]>
                        {
                            new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A },
                        }
                    },
                };
            var ext = Path.GetExtension(ImageUpload.FormFile.FileName).ToLowerInvariant();
            if (string.IsNullOrEmpty(ext) || !fileSignature.Keys.Contains(ext))
            {
                return false;
            }

            //using (var memoryStream = new MemoryStream())
            //{
            //    ImageUpload.FormFile.CopyTo(memoryStream);
            //    var signatures = fileSignature[ext];
            //    var reader = new BinaryReader(memoryStream);
            //    var headerBytes = reader.ReadBytes(signatures.Max(m => m.Length));

            //    return signatures.Any(signature =>
            //        headerBytes.Take(signature.Length).SequenceEqual(signature));
            //}
            return true;
        }
    }
}
