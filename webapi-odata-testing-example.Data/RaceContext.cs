﻿using System.Data.Entity;
using webapi_odata_testing_example.Data.Models;

namespace webapi_odata_testing_example.Data
{
    public class RaceContext : DbContext
    {
         public DbSet<Car> Cars { get; set; }
    }
}