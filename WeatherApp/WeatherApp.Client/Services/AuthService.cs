using Blazored.LocalStorage;

namespace WeatherApp.Client.Services;

public class AuthService
{
    private readonly Supabase.Client _supabase;
    private readonly ILocalStorageService _localStorage;
    private const string SessionKey = "supabase_session";

    public AuthService(Supabase.Client supabase, ILocalStorageService localStorage)
    {
        _supabase = supabase;
        _localStorage = localStorage;
    }

    public async Task InitializeAsync()
    {
        try
        {
            var token = await _localStorage.GetItemAsync<string>(SessionKey);
            if (!string.IsNullOrEmpty(token))
            {
                await _supabase.Auth.SetSession(token, token);
            }
        }
        catch { }
    }

    public async Task<bool> SignUp(string email, string password)
    {
        var result = await _supabase.Auth.SignUp(email, password);
        return result?.User != null;
    }

    public async Task<bool> SignIn(string email, string password)
    {
        var result = await _supabase.Auth.SignIn(email, password);
        if (result?.User != null)
        {
            await _localStorage.SetItemAsync(SessionKey, result.AccessToken);
            return true;
        }
        return false;
    }

    public async Task SignOut()
    {
        await _supabase.Auth.SignOut();
        await _localStorage.RemoveItemAsync(SessionKey);
    }

    public bool IsLoggedIn()
    {
        return _supabase.Auth.CurrentUser != null;
    }

    public string? GetCurrentUserEmail()
    {
        return _supabase.Auth.CurrentUser?.Email;
    }

    public string? GetCurrentUserId()
    {
        return _supabase.Auth.CurrentUser?.Id;
    }
}