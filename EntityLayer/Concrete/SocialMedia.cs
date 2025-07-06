using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class SocialMedia
    {
        [Key]  // ID için key atamsı yapıyoruz. ID birincil anahtar olması için
        public int SocialMediaId { get; set; }
        public string? Name { get; set; }
        public string? Url { get; set; }
        public string? Icon { get; set; }
        public bool? Status { get; set; }    // gerektiğinde kaldırabilmek için on-off yapacaz
    }
}
