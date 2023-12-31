﻿using Ahlatci.Shop.Domain.Common;

namespace Ahlatci.Shop.Domain.Entities
{
    public class Category : AuditableEntity
    {
        public string Name { get; set; }

        //Navigation Property
        public ICollection<Product> Products { get; set; }

    }
}


