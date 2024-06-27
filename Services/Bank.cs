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

        public float Deposit(string accountId, float amount)
        {
            var account = GetOrCreateAccount(accountId);
            account.Balance += amount;
            _dbContext.SaveChanges();

            return account.Balance;
        }

        public float GetBalance(string accountId)
        {
            var account = _dbContext.Accounts.Where(ac => ac.Id == accountId).FirstOrDefault();

            if (account == null)
            {
                throw new Exception($"Account {accountId} did not find");
            }

            return account.Balance;
        }

        public void Reset()
        {
            _dbContext.Accounts.RemoveRange(_dbContext.Accounts);
            _dbContext.SaveChanges();
        }

        public void Transfer(string originId, string destinationId, float amount)
        {
            var accountOrigin = _dbContext.Accounts.Where(ac => ac.Id == originId).FirstOrDefault();
            var accountDestination = GetOrCreateAccount(destinationId);
            
            if (accountOrigin == null)
            {
                throw new Exception($"Account {originId} did not find");
            }

            if (accountOrigin.Balance < amount)
            {
                throw new Exception($"No balance available");
            }

            accountOrigin.Balance -= amount;
            accountDestination.Balance += amount;

            _dbContext.SaveChanges();
        }

        public float Withdraw(string accountId, float amount)
        {
            var account = _dbContext.Accounts.Where(ac => ac.Id == accountId).FirstOrDefault();
            if (account == null)
            {
                throw new Exception($"Account {accountId} did not find");
            }

            account.Balance -= amount;
            _dbContext.SaveChanges();

            return account.Balance;
        }

        private Account GetOrCreateAccount(string accountId)
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
