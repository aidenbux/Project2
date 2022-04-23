using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DiskInventory.Models
{
    public partial class Borrower
    {
        public Borrower()
        {
            DiskHasBorrowers = new HashSet<DiskHasBorrower>();
        }

        public int BorrowerId { get; set; }
        [Required(ErrorMessage = "Please Enter Borrower Last Name.")]
        public string Lname { get; set; }
        [Required(ErrorMessage = "Please Enter Borrower First Name.")]
        public string Fname { get; set; }
        [Required(ErrorMessage = "Please Enter Borrower Phone Number.")]
        public string PhoneNum { get; set; }

        public virtual ICollection<DiskHasBorrower> DiskHasBorrowers { get; set; }
    }
}
