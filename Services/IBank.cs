namespace SimpleBankAPI.Services
{
    public interface IBank
    {
        void Reset();
        float Deposit(int accountId, float amount);
        float GetBalance(int accountId);
        float Withdraw(int accountId, float amount);
        void Transfer(int originId, int destinationId, float amount);
    }
}
