using EbayControl;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EbayControlTest
{
    public  class OperationDb
    {
        public static void Add(Ebay_Control product)
        {
            using (AppDbContext context = new AppDbContext())
            {
                context.Amazon_Ebay.Add(product);
                context.SaveChanges();
            }
        }


        public static void DeleteAll()
        {
            using (AppDbContext context = new AppDbContext())
            {
                var rows = context.Amazon_Ebay.ToList();
                context.Amazon_Ebay.RemoveRange(rows);
                context.SaveChanges();
            }
        }


        public static List<Ebay_Control> GetList()
        {
            using (AppDbContext context = new AppDbContext())
            {
                return context.Amazon_Ebay.ToList(); 
            }
        }

        public static void Update(Ebay_Control product)
        {
            using (AppDbContext context = new AppDbContext())
            {
                var updatedEntity = context.Entry(product);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }


        public static Ebay_Control  Get(Expression<Func<Ebay_Control, bool>> filter)
        {
            using (AppDbContext context = new AppDbContext())
            {
                return context.Set<Ebay_Control>().FirstOrDefault(filter);
            }
        }


        public static int GetProductsNumber()
        {
            using (AppDbContext context = new AppDbContext())
            {
                return context.Amazon_Ebay.Count();
            }
        }

    }
}
