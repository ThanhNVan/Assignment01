﻿using Assignment01.EntityProviders;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assignment01.ServiceProviders;

public interface IOrderServiceProviders
{
    Task<Order> GetSingleByIdAsync(int id);

    Task<List<Order>> GetListAllAsync();

    Task<IEnumerable<Order>> GetListByMemberIdAsync(int memberId);

    Task<IEnumerable<Order>> GetListByDateRangeAsync(DateTime startDate, DateTime endDate);

    Task<Order> AddOrderAsync(Order order);
}
