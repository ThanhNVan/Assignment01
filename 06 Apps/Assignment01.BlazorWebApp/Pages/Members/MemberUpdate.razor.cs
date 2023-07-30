using Assignment01.EntityProviders;
using Assignment01.ServiceProviders;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Assignment01.BlazorWebApp;

public partial class MemberUpdate
{
    #region [ Properties ]
    [Parameter]
    public int? MemberId { get; set; }    

    [Inject]
    private NavigationManager NavigationManager { get; set; }

    [Inject]
    private ISessionStorageService SessionStorage { get; set; }

    [Inject]
    private ServiceContext ServiceContext { get; set; }

    private Member Member { get; set; }

    private string Role { get; set; } = string.Empty;
    #endregion

    #region [ Methods - OnInitializedAsync ]

    protected async override Task OnInitializedAsync() {
        try {
            this.Role = await SessionStorage.GetItemAsStringAsync(AppRole.Role);
        } catch {
            this.Role = string.Empty;
        }

        this.Member = await this.ServiceContext.Members.GetSingleByIdAsync(this.MemberId.Value);

        await base.OnInitializedAsync();
    }
    #endregion

    #region [ Methods -  ]
    public async Task UpdateMemberAsync() {
        var result = await this.ServiceContext.Members.UpdateAsync(this.Member);
        this.NavigationManager.NavigateTo("/Members");
    }
    #endregion
}
