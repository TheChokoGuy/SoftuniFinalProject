﻿using Project.Data.Models;
using Project.Models;
using System.Security.Claims;

namespace Project.Services
{
    public interface IUserService
    {
        public Task AddToCartAsync(int productId);

        public Task<IEnumerable<ProductViewModel>> GetCartProducts();
    }
}
