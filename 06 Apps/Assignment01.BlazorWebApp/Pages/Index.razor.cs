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
        var response = await this.ServiceContext.Members.LoginAsync(Email, Password);

        if (response == AppRole.Admin) {
            await SessionStorage.SetItemAsStringAsync(AppRole.Role, AppRole.Admin);
            NavigationManager.NavigateTo($"Product", true);
            return;
        }

        if (response.GetType() == typeof(Member)) {
            await SessionStorage.SetItemAsStringAsync(AppRole.Role, AppRole.Member);
            NavigationManager.NavigateTo($"Product", true);
            return;
        }

        this.Warning = response.ToString();
        return;
    }
    #endregion
}
