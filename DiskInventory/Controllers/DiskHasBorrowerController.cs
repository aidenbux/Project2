using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DiskInventory.Models;

namespace DiskInventory.Controllers
{
    public class DiskHasBorrowerController : Controller
    {
        private disk_inventoryjwContext context { get; set; }
        public DiskHasBorrowerController(disk_inventoryjwContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            var diskhasborrowers = context.DiskHasBorrowers.Include(d => d.Disk).OrderBy(d => d.Disk.DiskName).Include(b => b.Borrower).ToList();
            return View(diskhasborrowers);
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            DiskHasBorrowerViewModel checkout = new DiskHasBorrowerViewModel();
            checkout.BorrowedDate = DateTime.Now;
            checkout.Disks = context.Disks.OrderBy(d => d.DiskName).ToList();
            checkout.Borrowers = context.Borrowers.OrderBy(b => b.Lname).ToList();
            return View("Edit", checkout);
        }
        [HttpPost]
        public IActionResult Edit(DiskHasBorrowerViewModel diskhasborrowerviewmodel)
        {
            DiskHasBorrower checkout = new DiskHasBorrower();
            
            if (ModelState.IsValid)
            {
                checkout.DiskHasBorrowerId = diskhasborrowerviewmodel.DiskHasBorrowerId;
                checkout.BorrowerId = diskhasborrowerviewmodel.BorrowerId;
                checkout.DiskId = diskhasborrowerviewmodel.DiskId;
                checkout.BorrowedDate = diskhasborrowerviewmodel.BorrowedDate;
                checkout.ReturnedDate = diskhasborrowerviewmodel.ReturnedDate;
                if (checkout.DiskHasBorrowerId == 0)
                {
                    context.DiskHasBorrowers.Add(checkout);
                } 
                else
                {
                    context.DiskHasBorrowers.Update(checkout);
                }
                context.SaveChanges();
                return RedirectToAction("Index", "DiskHasBorrower");
            }
            ViewBag.Action = (diskhasborrowerviewmodel.DiskHasBorrowerId == 0) ? "Add" : "Edit";
            return View(diskhasborrowerviewmodel);

        }
    }
}
