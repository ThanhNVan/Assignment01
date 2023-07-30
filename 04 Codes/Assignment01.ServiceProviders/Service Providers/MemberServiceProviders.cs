using Assignment01.EntityProviders;
using Azure;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Assignment01.ServiceProviders;

public class MemberServiceProviders : IMemberServiceProviders
{
    #region [ Fields ]
    private readonly HttpClient _httpClient;

    #endregion

    #region [ CTor ]
    public MemberServiceProviders(HttpClient httpClient) {
        this._httpClient = httpClient;
    }
    #endregion

    #region [ Methods - CRUD ]
    public async Task<List<Member>> GetListAllAsync() {
        var result = default(List<Member>);

        var url = Routing.BaseUrl + Routing.MemberApi + Routing.GetAll;
        result = await this._httpClient.GetFromJsonAsync<List<Member>>(url);

        return result;
    }
    #endregion

    #region [ Methods - Login ]
    public async Task<string> LoginAdminAsync(string email, string password) {
        var url = Routing.BaseUrl + Routing.MemberApi + Routing.Login;
        var payload = this.GetJsonPayload(new Admin() { Email = email, Password = password });
        var response = await this._httpClient.PostAsync(url, payload);

        var responseContent =await response.Content.ReadAsStringAsync();
        if (responseContent == AppRole.Admin) {
            return AppRole.Admin;
        }

        return "Not Admin";

    }

    public async Task<Member> LoginMemberAsync(string email, string password) {
        var url = Routing.BaseUrl + Routing.MemberApi + Routing.Login;
        var payload = this.GetJsonPayload(new Admin() { Email = email, Password = password });
        var response = await this._httpClient.PostAsync(url, payload);

        var result = default(Member);

        if (response.StatusCode == HttpStatusCode.OK) {
            result = JsonConvert.DeserializeObject<Member>(await response.Content.ReadAsStringAsync());
            return result;
        }
        return result;
    }
    #endregion

    #region [ Methods - CRUD ]
    public async Task<Member> GetSingleByIdAsync(int memberId) {
        var result = default(Member);
        var url = Routing.BaseUrl + Routing.MemberApi + Routing.GetSingle + memberId.ToString();

        result = await this._httpClient.GetFromJsonAsync<Member>(url);  

        return result;
    }

    public async Task<Member> GetSingleByEmailAsync(string email) {
        var result = default(Member);
        var url = Routing.BaseUrl + Routing.MemberApi + Routing.ByMemberId + email;

        result = await this._httpClient.GetFromJsonAsync<Member>(url);

        return result;
    }

    public async Task<bool> UpdateAsync(Member member) {
        var url = Routing.BaseUrl + Routing.MemberApi + Routing.Update;
        var respone = await this._httpClient.PutAsJsonAsync<Member>(url, member);

        if (respone.StatusCode == HttpStatusCode.OK) {
            return true;
        }
        return false;
    }
    #endregion

    #region [ Methods -  ]
    protected StringContent GetJsonPayload<TPayload>(TPayload payload) {
        return new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
    }
    #endregion
}
