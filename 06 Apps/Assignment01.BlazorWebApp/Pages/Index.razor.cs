using Assignment01.EntityProviders;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Assignment01.BlazorWebApp;

public partial class Index
{
    #region [ Properties ]
    [Inject]
    private NavigationManager NavigationManager { get; set; }

    [Inject]
    private ISessionStorageService SessionStorage { get; set; }

    private string Email { get; set; }
    private string Password { get; set; }
    private string Warning { get; set; } = string.Empty;
    #endregion

    #region [ Methods -  ]
    public async Task LoginAsync() {
        await SessionStorage.SetItemAsStringAsync(AppRole.Role, AppRole.Admin);
        NavigationManager.NavigateTo($"Product",true);
    }
    #endregion
}
