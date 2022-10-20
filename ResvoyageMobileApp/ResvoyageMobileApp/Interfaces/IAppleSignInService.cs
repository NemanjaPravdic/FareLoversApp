using System;
using System.Threading.Tasks;
using ResvoyageMobileApp.Models.User;

namespace ResvoyageMobileApp.Interfaces
{
    public interface IAppleSignInService
    {
        bool IsAvailable { get; }
        Task<AppleSignInCredentialState> GetCredentialStateAsync(string userId);
        Task<AppleAccount> SignInAsync();
    }
}
