
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;

namespace VPNService.Entities.Models
{
    [Table("ScreeningItem")]
    public class ScreeningItem
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;

        [ForeignKey(nameof(Screening))]
        public int ScreeningID { get; set; }
        public string Number { get; set; } = string.Empty;
    }
}