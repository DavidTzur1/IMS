using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPNService.Entities.DTO
{
    public class ScreeningReadingDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CompanyID { get; set; }

    }
}
