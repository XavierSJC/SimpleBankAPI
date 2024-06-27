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

        public Account Deposit(string accountId, float amount)
        {
            var account = GetOrCreateAccount(accountId);
            account.Balance += amount;
            _dbContext.SaveChanges();

            return account;
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

        public IEnumerable<Account> Transfer(string originId, string destinationId, float amount)
        {
            var accountOrigin = _dbContext.Accounts.Where(ac => ac.Id == originId).FirstOrDefault();
            if (accountOrigin == null)
            {
                throw new Exception($"Account {originId} did not find");
            }

            if (accountOrigin.Balance < amount)
            {
                throw new Exception($"No balance available");
            }

            var accountDestination = GetOrCreateAccount(destinationId);
            accountOrigin.Balance -= amount;
            accountDestination.Balance += amount;

            _dbContext.SaveChanges();

            var result = new List<Account>();
            result.Add(accountOrigin);
            result.Add(accountDestination);

            return result;
        }

        public Account Withdraw(string accountId, float amount)
        {
            var account = _dbContext.Accounts.Where(ac => ac.Id == accountId).FirstOrDefault();
            if (account == null)
            {
                throw new Exception($"Account {accountId} did not find");
            }

            account.Balance -= amount;
            _dbContext.SaveChanges();

            return account;
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
