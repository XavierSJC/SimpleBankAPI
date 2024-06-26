using SimpleBankAPI.Data;
using SimpleBankAPI.Models;

namespace SimpleBankAPI.Services
{
    public class Bank : IBank
    {
        private readonly BankContext _dbContext;

        public Bank(BankContext dbContext)
        {
            _dbContext = dbContext;
        }

        public float Deposit(int accountId, float amount)
        {
            var account = GetOrCreateAccount(accountId);
            account.Balance += amount;
            _dbContext.SaveChanges();

            return 0;
        }

        public float GetBalance(int accountId)
        {
            var account = _dbContext.Accounts.Where(ac => ac.Id == accountId).FirstOrDefault();

            if (account == null)
            {
                throw new Exception($"Account {accountId} did not find");
            }

            return account.Balance;
        }

        public bool Reset()
        {
            throw new NotImplementedException();
        }

        public void Transfer(int originId, int destination, float amount)
        {
            throw new NotImplementedException();
        }

        public float Withdraw(int accountId, float amount)
        {
            throw new NotImplementedException();
        }

        private Account GetOrCreateAccount(int accountId)
        {
            var account = _dbContext.Accounts.Where(ac => ac.Id == accountId).FirstOrDefault();
            if (account == null)
            {
                account = new Account() { Id = accountId };
                _dbContext.Accounts.Add(account);
            }

            return account;
        }
    }
}
