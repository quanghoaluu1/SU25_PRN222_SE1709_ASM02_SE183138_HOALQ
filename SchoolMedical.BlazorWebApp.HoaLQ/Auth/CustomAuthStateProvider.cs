using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using SchoolMedical.Repositories.HoaLQ.Models;
using SchoolMedical.Services.HoaLQ;

namespace SchoolMedical.Repositories.HoaLQ.Auth;

public class CustomAuthStateProvider: AuthenticationStateProvider
{
    private readonly IJSRuntime _jsRuntime;
    private readonly ISystemUserAccountService _userAccountService;
    private readonly ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

    public CustomAuthStateProvider(IJSRuntime jsRuntime, ISystemUserAccountService userAccountService)
    {
        _jsRuntime = jsRuntime;
        _userAccountService = userAccountService;
    }
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            return await Task.FromResult(new AuthenticationState(_anonymous));
        }
        catch
        {
            return await Task.FromResult(new AuthenticationState(_anonymous));
        }
    }
    
    public async Task UpdateAuthenticationState(SystemUserAccount user)
    {
        ClaimsPrincipal claimsPrincipal;

        if (user != null)
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserAccountId.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.GivenName, user.FullName),
                new Claim(ClaimTypes.Role, user.RoleId.ToString())
            }, "apiauth");

            claimsPrincipal = new ClaimsPrincipal(identity);
            
        }
        else
        {
            claimsPrincipal = _anonymous;
            
        }

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }
}