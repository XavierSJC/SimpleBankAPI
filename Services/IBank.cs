using SimpleBankAPI.Models;

namespace SimpleBankAPI.Services
{
    public interface IBank
    {
        void Reset();
        Account Deposit(string accountId, float amount);
        float GetBalance(string accountId);
        Account Withdraw(string accountId, float amount);
        IEnumerable<Account> Transfer(string originId, string destinationId, float amount);
    }
}
