﻿using BasicWebApi.DataAccessLayer.Configuration.Common;
using BasicWebApi.DataAccessLayer.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebApi.DataAccessLayer.Configuration
{
    internal class OrderConfiguration : BaseEntityConfiguration<Orders>
    {
        public override void Configure(EntityTypeBuilder<Orders> builder)
        {
            base.Configure(builder);

           

            builder.Property(x => x.CreationDate).HasColumnName("ORD_DT");
            builder.Property(x => x.Status).HasConversion<string>();
        }
    }
}
