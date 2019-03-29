﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Redundancy;
using Database.Repository_Implementations;

namespace Database.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBarRepository BarRepository { get; }

        BarEventRepository BarEventRepository { get; }

        BarrepresentativeRepository BarRepRepository { get; }

        CouponRepository CouponRepository { get; }

        CustomerRepository CustomerRepository { get; }

        DrinkRepository DrinkRepository { get; }

        ReviewRepository ReviewRepository { get; }

        int Complete();
    }
}