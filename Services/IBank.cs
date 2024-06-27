namespace SimpleBankAPI.Services
{
    public interface IBank
    {
        void Reset();
        float Deposit(string accountId, float amount);
        float GetBalance(string accountId);
        float Withdraw(string accountId, float amount);
        void Transfer(string originId, string destinationId, float amount);
    }
}
