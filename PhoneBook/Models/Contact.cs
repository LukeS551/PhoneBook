using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Models
{
    public class Contact
    {

        [Key]
        public int ContactId { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        [Required(ErrorMessage = "field is required.")]
        [DisplayName("First Name")]
        public string First_name { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        [Required(ErrorMessage = "field is required.")]
        [DisplayName("Last Name")]
        public string Last_name { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        [DisplayName("Middle Name")]
        public string Middle_initial { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        [DisplayName("Home phone")]
        public string Home_phone { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        [DisplayName("Cell Phone")]
        public string Cell_phone { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        [DisplayName("Office ext")]
        public string Office_ext { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        [DisplayName("IRD")]
        public int IRD { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        [DisplayName("active")]
        public bool active { get; set; }



    }
}
