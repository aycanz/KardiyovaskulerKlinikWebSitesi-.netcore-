using DataAccessesLayer.Abstract;
using DataAccessesLayer.Repositories;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessesLayer.EntityFramework
{
    public class EfDoctorRepository :GenericRepository<Doctor>,IDoctorDal
    {
    }
}
