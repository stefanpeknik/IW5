﻿namespace TaHooK.IdentityProvider.App.Pages.Login;

public class LoginOptions
{
    public static bool AllowLocalLogin = true;
    public static bool AllowRememberLogin = true;
    public static TimeSpan RememberMeLoginDuration = TimeSpan.FromDays(30);
    public static string InvalidCredentialsErrorMessage = "Invalid username or password";
    public static string NotVerifiedEmailAddress = "Email address is not verified";
}
