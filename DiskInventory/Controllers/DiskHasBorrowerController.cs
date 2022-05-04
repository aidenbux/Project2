﻿using Microsoft.AspNetCore.Mvc;
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
            checkout.CurrentVM.BorrowedDate = DateTime.Now;
            checkout.Disks = context.Disks.OrderBy(d => d.DiskName).ToList();
            checkout.Borrowers = context.Borrowers.OrderBy(b => b.Lname).ToList();
            return View("Edit", checkout);
        }
        [HttpGet]

        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var diskhasborrower = context.DiskHasBorrowers.Find(id);
            DiskHasBorrowerViewModel checkout = new DiskHasBorrowerViewModel();
            checkout.Disks = context.Disks.OrderBy(d => d.DiskName).ToList();
            checkout.Borrowers = context.Borrowers.OrderBy(b => b.Lname).ToList();
            checkout.CurrentVM.DiskHasBorrowerId = diskhasborrower.DiskHasBorrowerId;
            checkout.CurrentVM.BorrowerId = diskhasborrower.BorrowerId;
            checkout.CurrentVM.DiskId = diskhasborrower.DiskId;
            checkout.CurrentVM.BorrowedDate = diskhasborrower.BorrowedDate;
            checkout.CurrentVM.ReturnedDate = diskhasborrower.ReturnedDate;
            return View(checkout);
        }

        [HttpPost]
        public IActionResult Edit(DiskHasBorrowerViewModel diskhasborrowerviewmodel)
        {
            DiskHasBorrower checkout = new DiskHasBorrower();
            
            if (ModelState.IsValid)
            {
                checkout.DiskHasBorrowerId = diskhasborrowerviewmodel.CurrentVM.DiskHasBorrowerId;
                checkout.BorrowerId = diskhasborrowerviewmodel.CurrentVM.BorrowerId;
                checkout.DiskId = diskhasborrowerviewmodel.CurrentVM.DiskId;
                checkout.BorrowedDate = diskhasborrowerviewmodel.CurrentVM.BorrowedDate;
                checkout.ReturnedDate = diskhasborrowerviewmodel.CurrentVM.ReturnedDate;
                if (checkout.DiskHasBorrowerId == 0)
                {
                    context.DiskHasBorrowers.Add(checkout);
                    TempData["message"] = "Checkout Added.";
                } 
                else
                {
                    context.DiskHasBorrowers.Update(checkout);
                    TempData["message"] = "Checkout Updated.";
                }
                context.SaveChanges();
                return RedirectToAction("Index", "DiskHasBorrower");
            }
            ViewBag.Action = (diskhasborrowerviewmodel.CurrentVM.DiskHasBorrowerId == 0) ? "Add" : "Edit";

            diskhasborrowerviewmodel.Disks = context.Disks.OrderBy(d => d.DiskName).ToList();
            diskhasborrowerviewmodel.Borrowers = context.Borrowers.OrderBy(b => b.Lname).ToList();

            return View(diskhasborrowerviewmodel);

        }
    }
}
