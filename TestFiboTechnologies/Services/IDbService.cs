﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestFiboTechnologies.Models;

namespace TestFiboTechnologies.Services
{
    public interface IDbService
    {
        Task<List<Users>> GetUsersAsync();
        Task<int> InsertUserAsync(Users item);
        Task<int> UpdateUserAsync(Users item);
        Task<int> DeleteUserAsync(Users item);
    }
}
