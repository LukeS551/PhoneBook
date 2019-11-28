using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Models;
using CsvHelper;
using System.IO;
using PdfSharp;
using PdfSharp.Pdf.IO;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Drawing;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Text;

namespace PhoneBook.Controllers
{
    public class HomeController : Controller
    {
        private readonly ContactContext _context;

        public HomeController(ContactContext context)
        {
            _context = context;
        }

        // GET: Contacts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Contacts.ToListAsync());
        }

        // GET: Contacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts
                .FirstOrDefaultAsync(m => m.ContactId == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // GET: Contacts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContactId,First_name,Last_name,Middle_initial,Home_phone,Cell_phone,Office_ext,IRD,active")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }

        // GET: Contacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContactId,First_name,Last_name,Middle_initial,Home_phone,Cell_phone,Office_ext,IRD,active")] Contact contact)
        {
            if (id != contact.ContactId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactExists(contact.ContactId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }

        // GET: Contacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts
                .FirstOrDefaultAsync(m => m.ContactId == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id){
            var contact = await _context.Contacts.FindAsync(id);
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactExists(int id){
            return _context.Contacts.Any(e => e.ContactId == id);
        }
        public ActionResult export()
        {
            var contactList = new List<Contact> { };
            foreach (var item in _context.Contacts)
            {
                contactList.Add(item);
            }
            var appRoot = constructPath();

            using (var writer = new StreamWriter(appRoot + "\\files.csv"))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecords(contactList);
            }

            var file = appRoot + "\\files.csv";
            return new PhysicalFileResult(@file, "application/csv");
        }

        public ActionResult print()
        {
            PdfDocument document = new PdfDocument();
            draw(document);

            var appRoot = constructPath();
            document.Save(appRoot + "\\files.pdf");
            var file = appRoot + "\\files.pdf";

            return new PhysicalFileResult(@file, "application/pdf");
        }

        /// <summary>
        /// Draw to passed pdf file
        /// </summary>
        /// <param name="document"></param>
        private void draw(PdfDocument document)
        {
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont fontHeader = new XFont("Lato", 24.0, XFontStyle.Regular);
            XFont fontBody = new XFont("Lato", 16, XFontStyle.Regular);
            int heightCount = 40;

            gfx.DrawString("Report", fontHeader, XBrushes.Black, new XRect
                (20, 0, page.Width, page.Height), XStringFormats.TopLeft);

            foreach (var contact in _context.Contacts)
            {
                heightCount += 25;
                String output = contact.First_name + " " + contact.Last_name + 
                    " " + contact.Cell_phone + " " + contact.Home_phone;
                gfx.DrawString(output,fontBody, XBrushes.Black, new XRect
                    (20, heightCount, page.Width, page.Height), XStringFormats.TopLeft);
            }
        }

        /// <summary>
        /// Construct path to project root
        /// </summary>
        /// <returns>String</returns>
        private string constructPath()
        {
            Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
            var exePath = Path.GetDirectoryName(System.Reflection
                .Assembly.GetExecutingAssembly().CodeBase);
            var appRoot = appPathMatcher.Match(exePath).Value;

            return appRoot;
        }
    }
}
