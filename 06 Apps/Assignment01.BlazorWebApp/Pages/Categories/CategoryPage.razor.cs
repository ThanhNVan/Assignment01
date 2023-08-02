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

public partial class CategoryPage
{
    #region [ Fields ]
    private string _searchEmail;
    private IEnumerable<Category> _categoryList;
    #endregion

    #region [ Properties ]
    [Inject]
    private NavigationManager NavigationManager { get; set; }

    [Inject]
    private ISessionStorageService SessionStorage { get; set; }

    [Inject]
    public ServiceContext ServiceContext { get; set; }

    private string Role { get; set; } = string.Empty;

    public IQueryable<Category> CategoryList { get; set; }

    public string SearchName {
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
            this._categoryList = await this.ServiceContext.Categories.GetListAllAsync();

            this.CategoryList = this._categoryList.AsQueryable();
        }
        StateHasChanged();
    }
    #endregion

    #region [ Public Methods -  ]
    public void Update(int categoryId) {
        this.NavigationManager.NavigateTo($"/Categories/Update/{categoryId}");
    }

    public void Delete(int cateogryId) {
        var aa = cateogryId;
    } 
    
    public void Detail(int categoryId) {
        this.NavigationManager.NavigateTo($"/Categories/Detail/{categoryId}");
    }
    #endregion

    #region [ Private Methods -  ]
    private void Search() {

        this.CategoryList = this._categoryList.Where(x => x.CategoryName.Contains(this.SearchName, StringComparison.InvariantCultureIgnoreCase)).AsQueryable();

    }

    #endregion
}
