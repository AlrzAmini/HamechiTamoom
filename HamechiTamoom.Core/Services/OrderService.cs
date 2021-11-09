using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamechiTamoom.Core.Services.Interfaces;
using HamechiTamoom.DataLayer.Context;

namespace HamechiTamoom.Core.Services
{
    class OrderService : IOrderService
    {
        private readonly HamechiTamoomContext _context;
        private readonly IUserService _userService;

    }
}
