﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementProject
{
    public interface IBasicDB<T> where T : IPoco
    {
        long Add(T t);
        T GetById(long id);
        IList<T> GetAll();
        bool Remove(T t);
        bool Update(T t);
    }
}