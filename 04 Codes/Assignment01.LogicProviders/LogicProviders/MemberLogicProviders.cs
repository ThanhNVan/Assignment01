﻿using Assignment01.DataProviders;
using Assignment01.EntityProviders;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SharedLibraries;

namespace Assignment01.LogicProviders;

public class MemberLogicProviders : BaseEntityLogicProvider<Member, IMemberDataProviders>, IMemberLogicProviders
{
    #region [ CTor ]
    public MemberLogicProviders(ILogger<BaseEntityLogicProvider<Member, IMemberDataProviders>> logger, IMemberDataProviders dataProvider) : base(logger, dataProvider) {
    }
    #endregion

    #region [ Methods -  ]
    public async Task<Member> GetSingleByIdAsync(int id) {
        if (id == null) {
            return null;
        }
        return await this._dataProvider.GetSingleByIdAsync(id);
    }

    public async Task<Member> GetSingleByEmailAsync(string email) {
        if (email.IsNullOrEmpty()) {
            return null;
        }
        return await this._dataProvider.GetSingleByEmailAsync(email);
    }

    public async Task<Member> LoginAsync(string email, string password) {
        if (email.IsNullOrEmpty() || password.IsNullOrEmpty()) {
            return null;
        }

        return await this._dataProvider.LoginAsync(email, password); 
    }
    #endregion
}
