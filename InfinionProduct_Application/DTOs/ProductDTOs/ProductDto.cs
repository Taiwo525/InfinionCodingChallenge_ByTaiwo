using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfinionProduct_Core.DTOs.ProductDTOs
{
    public class ProductDto : BaseProductDto
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
