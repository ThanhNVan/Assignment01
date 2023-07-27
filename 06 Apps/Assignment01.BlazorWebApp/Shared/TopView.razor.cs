using Assignment01.EntityProviders;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Assignment01.BlazorWebApp;

public partial class TopView
{
    [Inject]
    private NavigationManager NavigationManager { get; set; }

    [Inject]
    private ISessionStorageService SessionStorage { get; set; }

    private async Task LogoutAsync() {
        await SessionStorage.ClearAsync();
        NavigationManager.NavigateTo("/", true);
    }
}
