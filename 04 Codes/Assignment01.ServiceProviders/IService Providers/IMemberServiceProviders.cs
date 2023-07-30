﻿using Assignment01.EntityProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment01.ServiceProviders;

public interface IMemberServiceProviders
{
    #region [ Methods - Login ]
    Task<string> LoginAdminAsync(string email, string password);
    Task<Member> LoginMemberAsync(string email, string password);
    #endregion

    #region [ Methods - CRUD ]
    Task<List<Member>> GetListAllAsync();

    Task<Member> GetSingleByIdAsync(int memberId);

    Task<Member> GetSingleByEmailAsync(string email);
    Task<bool> UpdateAsync(Member member);
    #endregion
}
