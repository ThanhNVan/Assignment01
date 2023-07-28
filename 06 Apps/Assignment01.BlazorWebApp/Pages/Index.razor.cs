using Assignment01.EntityProviders;
using Assignment01.ServiceProviders;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Assignment01.BlazorWebApp;

public partial class Index
{
    #region [ Properties ]
    [Inject]
    private NavigationManager NavigationManager { get; set; }

    [Inject]
    private ISessionStorageService SessionStorage { get; set; }

    [Inject]
    private ServiceContext ServiceContext { get; set; }

    private string Email { get; set; }
    private string Password { get; set; }
    private string Warning { get; set; } = string.Empty;
    #endregion

    #region [ Methods -  ]
    public async Task LoginAsync() {
        var response = await this.ServiceContext.Members.LoginAdminAsync(Email, Password);

        if (response == AppRole.Admin) {
            await SessionStorage.SetItemAsStringAsync(AppRole.Role, AppRole.Admin);
            NavigationManager.NavigateTo($"Products", true);
            return;
        }

        var memberResponse = await this.ServiceContext.Members.LoginMemberAsync(Email, Password);

        if (memberResponse!= null) {
            await SessionStorage.SetItemAsStringAsync(AppRole.Role, AppRole.Member);
            await SessionStorage.SetItemAsStringAsync("Email", memberResponse.Email);
            await SessionStorage.SetItemAsync("MemberId", memberResponse.MemberId);
            NavigationManager.NavigateTo($"Products", true);
            return;
        }

        this.Warning = "Login Error, Please try again";
        return;
    }
    #endregion
}
