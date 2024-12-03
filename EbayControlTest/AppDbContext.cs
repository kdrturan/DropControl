using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbayControl
{
    public class AppDbContext : DbContext
    {
        public DbSet<Ebay_Control> Amazon_Ebay { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EbayControl;Trusted_Connection=true");
        }

    }
}
