using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiskInventory.Models;

namespace DiskInventory.Controllers
{

    public class BorrowerController : Controller
    {
        private disk_inventoryjwContext context { get; set; }
        public BorrowerController(disk_inventoryjwContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            List<Borrower> borrowers = context.Borrowers.OrderBy(b => b.Lname).ThenBy(b => b.Fname).ToList();
            return View(borrowers);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Borrowers = context.Borrowers.OrderBy(l => l.Lname).ToList();
            ViewBag.Borrowers = context.Borrowers.OrderBy(f => f.Fname).ToList();
            ViewBag.Borrowers = context.Borrowers.OrderBy(p => p.PhoneNum).ToList();
            return View("Edit", new Borrower());
        } //will not commit rn
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Borrowers = context.Borrowers.OrderBy(l => l.Lname).ToList();
            ViewBag.Borrowers = context.Borrowers.OrderBy(f => f.Fname).ToList();
            ViewBag.Borrowers = context.Borrowers.OrderBy(p => p.PhoneNum).ToList();
            var borrower = context.Borrowers.Find(id);
            return View(borrower);
        }

        [HttpPost]
        public IActionResult Edit(Borrower borrower)
        {
            if (ModelState.IsValid)
            {
                if (borrower.BorrowerId == 0)
                    context.Borrowers.Add(borrower);
                else
                    context.Borrowers.Update(borrower);
                context.SaveChanges();
                return RedirectToAction("Index", "Borrower");
            }
            else
            {
                ViewBag.Action = (borrower.BorrowerId == 0) ? "Add" : "Edit";
                ViewBag.Borrowers = context.Borrowers.OrderBy(l => l.Lname).ToList();
                ViewBag.Borrowers = context.Borrowers.OrderBy(f => f.Fname).ToList();
                ViewBag.Borrowers = context.Borrowers.OrderBy(p => p.PhoneNum).ToList();
                return View(borrower);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var borrower = context.Borrowers.Find(id);
            return View(borrower);
        }

        [HttpPost]
        public IActionResult Delete(Borrower borrower)
        {
            context.Borrowers.Remove(borrower);
            context.SaveChanges();
            return RedirectToAction("Index", "Borrower");
        }
    }
}
