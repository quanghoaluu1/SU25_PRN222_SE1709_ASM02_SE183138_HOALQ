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
            // Tạm thời, chúng ta sẽ không dùng persistent login (lưu session)
            // Trong một ứng dụng thật, bạn sẽ đọc token từ localStorage/sessionStorage ở đây
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
            // Tạo claims cho người dùng đã đăng nhập
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserAccountId.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.GivenName, user.FullName),
                new Claim(ClaimTypes.Role, user.RoleId.ToString())
            }, "apiauth");

            claimsPrincipal = new ClaimsPrincipal(identity);
            
            // Trong ứng dụng thật, bạn sẽ lưu token vào đây
            // await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "authToken", "your_jwt_token_here");
        }
        else
        {
            claimsPrincipal = _anonymous;
            // Xóa token khi logout
            // await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "authToken");
        }

        // Thông báo cho Blazor rằng trạng thái đăng nhập đã thay đổi
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }
}