using Assignment01.EntityProviders;
using Assignment01.ServiceProviders;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Threading.Tasks;

namespace Assignment01.BlazorWebApp;

public partial class RegisterPage
{
    #region [ Fields ]
    public string _emailWarning;
    #endregion
    #region [ Properties ]
    [Inject]
    private ServiceContext ServiceContext { get; set; }
    [Inject]
    private ISessionStorageService SessionStorage { get; set; }

    [Inject]
    private NavigationManager NavigationManager { get; set; }

    public string DisplayEmail { get; set; }

    public string DisplayCity { get; set; }

    public string DisplayCountry { get; set; }

    public string DisplayCompanyName { get; set; }

    public string DisplayPassword { get; set; }

    public string DisplayRePassword { get; set; }
    public string DisplayRegister { get; set; }

    public string EmailWarning {
        get => this._emailWarning;
        set {
            this._emailWarning = value;
        }
    }
    public string PasswordWarning { get; set; } = string.Empty;
    #endregion

        #region [ Methods -  ]
    public async Task CheckEmailAsync() {
        if (DisplayEmail.IsNullOrEmpty()) {
            this.EmailWarning = "Empty Address";
            return;
        }
        var result = await this.ServiceContext.Members.GetSingleByEmailAsync(DisplayEmail);
        if (result == null) {
            this.EmailWarning = "Valid Email";
            return;
        } else {
            this.EmailWarning = "Duplicated Email";
        }
    }

    public async Task RegisterAsync() {
        if (this.DisplayPassword != this.DisplayRePassword) {
            this.PasswordWarning = "Not Match Re - password";
            return;
        }

        await this.CheckEmailAsync();

        if (this.EmailWarning == "Valid Email") {
            var rdm = new Random();
            var member = new Member() {
                MemberId = (await this.ServiceContext.Members.GetListAllAsync()).Count + rdm.Next(),
                Email = this.DisplayEmail,
                CompanyName= this.DisplayCompanyName,
                Country = this.DisplayCountry,
                City = this.DisplayCity,
                Password = this.DisplayPassword,
            };

            var response = await this.ServiceContext.Members.AddAsync(member);

            if (response) {
                this.DisplayRegister = "Register Success";
                await Task.Delay(750);
                this.NavigationManager.NavigateTo("/");
            }
            return;
        }

        return;

    }
    #endregion
}
