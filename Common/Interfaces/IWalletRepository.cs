using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IWalletRepository
    {
        Task<bool> InsertWallet(Wallet wallet);
        Task<bool> UpdateWallet(Wallet wallet);
    }
}
