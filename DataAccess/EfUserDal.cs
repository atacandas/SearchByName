using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class EfUserDal
    {
        public List<User> GetAll()
        {
            using (var context = new FakeNameContext())
            {
                return context.Set<User>().ToList();
            }
        }
    }
}
