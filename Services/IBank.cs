namespace SimpleBankAPI.Services
{
    public interface IBank
    {
        bool Reset();
        float Deposit(int accountId, float amount);
        float GetBalance(int accountId);
        float Withdraw(int accountId, float amount);
        void Transfer(int originId, int destination, float amount);
    }
}
