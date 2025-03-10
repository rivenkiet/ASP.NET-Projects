using Bulky.DataAccess.Data;
using Bulky.Models;
using Bulky.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class OrderDetailRepositry : Repository<OrderDetail>, IOrderDetailRepository
    {
        private readonly ApplicationDbContext _db;
        public OrderDetailRepositry(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Models.OrderDetail orderDetail)
        {
            _db.orderDetails.Update(orderDetail);
        }
    }
}
