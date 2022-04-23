using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DiskInventory.Models
{
    public partial class Disk
    {
        public Disk()
        {
            DiskHasBorrowers = new HashSet<DiskHasBorrower>();
        }

        public int DiskId { get; set; }
        [Required(ErrorMessage = "Please Enter Disk Name.")]
        public string DiskName { get; set; }
        [Required(ErrorMessage = "Please Enter Disk Release Date.")]
        public DateTime ReleaseDate { get; set; }
        [Required(ErrorMessage = "Please Enter Disk Genre.")]
        public int? GenreId { get; set; }
        [Required(ErrorMessage = "Please Enter Disk Status.")]
        public int? StatusId { get; set; }
        [Required(ErrorMessage = "Please Enter Disk Format.")]
        public int? DiskTypeId { get; set; }

        public virtual DiskType DiskType { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual Status Status { get; set; }
        public virtual ICollection<DiskHasBorrower> DiskHasBorrowers { get; set; }
    }
}
