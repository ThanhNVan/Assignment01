using Assignment01.EntityProviders;
using Assignment01.ServiceProviders;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Assignment01.BlazorWebApp;

public partial class MemberPage
{
    #region [ Fields ]
    private string _searchEmail;
    private IEnumerable<Member> _memberListInit;
    #endregion

    #region [ Properties ]
    [Inject]
    private NavigationManager NavigationManager { get; set; }

    [Inject]
    private ISessionStorageService SessionStorage { get; set; }

    [Inject]
    public ServiceContext ServiceContext { get; set; }

    private string Role { get; set; } = string.Empty;

    public IQueryable<Member> MemberList { get; set; }

    public string SearchEmail {
        get => this._searchEmail;
        set {
            this._searchEmail = value;
            this.Search();
        }
    }
    #endregion

    #region [ Methods - Override ]
    protected override async Task OnInitializedAsync() {
        try {
            this.Role = await SessionStorage.GetItemAsStringAsync(AppRole.Role);
        } catch {
            this.Role = string.Empty;
        }
        StateHasChanged();

        if (!Role.IsNullOrEmpty()) {
            this._memberListInit = await this.ServiceContext.Members.GetListAllAsync();

            this.MemberList = this._memberListInit.AsQueryable();
        }
        StateHasChanged();
    }
    #endregion

    #region [ Public Methods -  ]
    public void Update(int memberId) {
        this.NavigationManager.NavigateTo($"/Members/Update/{memberId}");
    }
    
    public void Delete(int memberId) {
        var aa = memberId;
    }
    #endregion

    #region [ Private Methods -  ]
    private void Search() {

        this.MemberList = this._memberListInit.Where(x => x.Email.Contains(this.SearchEmail, StringComparison.InvariantCultureIgnoreCase)).AsQueryable();

    }

    #endregion
}
