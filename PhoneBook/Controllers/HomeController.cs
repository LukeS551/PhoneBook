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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactExists(int id)
        {
            return _context.Contacts.Any(e => e.ContactId == id);
        }
        public ActionResult export()
        {
            var contactList = new List<Contact> { };
            foreach (var item in _context.Contacts)
            {
                contactList.Add(item);
            }

            using (var writer = new StreamWriter("D:\\files.csv"))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecords(contactList);
            }
            return RedirectToAction("Index");
        }
        public ActionResult print()
        {

            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Lato", 20.0, XFontStyle.Regular);
            gfx.DrawString("Report", font, XBrushes.Black, new XRect(20, 0, page.Width, page.Height), XStringFormats.TopLeft);
            var contactList = new List<Contact> { };

            int heightCount = 40;
            foreach (var item in _context.Contacts)
            {

                heightCount += 40;
                gfx.DrawString(item.First_name + " " + item.Last_name + " " + item.Cell_phone + " " + item.Home_phone, font, XBrushes.Black, new XRect(20, heightCount, page.Width, page.Height), XStringFormats.TopLeft);
            }
            var exePath = Path.GetDirectoryName(System.Reflection
                   .Assembly.GetExecutingAssembly().CodeBase);
            Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
            var appRoot = appPathMatcher.Match(exePath).Value;
            document.Save(appRoot + "\\files.pdf");
            var file = appRoot + "\\files.pdf";
            return new PhysicalFileResult(@file, "application/pdf");
        }
    }
}
