using Assignment01.EntityProviders;
using Azure;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
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

    #region [ Methods -  ]
    protected StringContent GetJsonPayload<TPayload>(TPayload payload) {
        return new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
    }
    #endregion
}
