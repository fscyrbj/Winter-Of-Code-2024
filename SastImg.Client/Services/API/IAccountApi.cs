// <auto-generated>
//     This code was generated by Refitter.
// </auto-generated>


using Refit;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using SastImg.Client.Service.API;

#nullable enable annotations

namespace SastImg.Client.Service.API
{
    [System.CodeDom.Compiler.GeneratedCode("Refitter", "1.5.0.0")]
    [Headers("Authorization: Bearer")]
    public partial interface IAccountApi
    {
        /// <returns>
        /// A <see cref="Task"/> representing the <see cref="IApiResponse"/> instance containing the result:
        /// <list type="table">
        /// <listheader>
        /// <term>Status</term>
        /// <description>Description</description>
        /// </listheader>
        /// <item>
        /// <term>200</term>
        /// <description>OK</description>
        /// </item>
        /// </list>
        /// </returns>
        [Headers("Accept: text/plain, application/json, text/json")]
        [Post("/api/account/register")]
        Task<IApiResponse<AuthenticationResponse>> RegisterAsync([Body] RegisterRequest body, CancellationToken cancellationToken = default);

        /// <returns>
        /// A <see cref="Task"/> representing the <see cref="IApiResponse"/> instance containing the result:
        /// <list type="table">
        /// <listheader>
        /// <term>Status</term>
        /// <description>Description</description>
        /// </listheader>
        /// <item>
        /// <term>200</term>
        /// <description>OK</description>
        /// </item>
        /// </list>
        /// </returns>
        [Headers("Accept: text/plain, application/json, text/json")]
        [Post("/api/account/login")]
        Task<IApiResponse<AuthenticationResponse>> LoginAsync([Body] LoginRequest body, CancellationToken cancellationToken = default);

        /// <returns>
        /// A <see cref="Task"/> representing the <see cref="IApiResponse"/> instance containing the result:
        /// <list type="table">
        /// <listheader>
        /// <term>Status</term>
        /// <description>Description</description>
        /// </listheader>
        /// <item>
        /// <term>200</term>
        /// <description>OK</description>
        /// </item>
        /// </list>
        /// </returns>
        [Post("/api/account/reset/password")]
        Task<IApiResponse> ResetPasswordAsync([Body] ResetPasswordRequest body, CancellationToken cancellationToken = default);

        /// <returns>
        /// A <see cref="Task"/> representing the <see cref="IApiResponse"/> instance containing the result:
        /// <list type="table">
        /// <listheader>
        /// <term>Status</term>
        /// <description>Description</description>
        /// </listheader>
        /// <item>
        /// <term>200</term>
        /// <description>OK</description>
        /// </item>
        /// </list>
        /// </returns>
        [Post("/api/account/reset/username")]
        Task<IApiResponse> ResetUsernameAsync([Body] ResetUsernameRequest body, CancellationToken cancellationToken = default);

        /// <returns>
        /// A <see cref="Task"/> representing the <see cref="IApiResponse"/> instance containing the result:
        /// <list type="table">
        /// <listheader>
        /// <term>Status</term>
        /// <description>Description</description>
        /// </listheader>
        /// <item>
        /// <term>200</term>
        /// <description>OK</description>
        /// </item>
        /// </list>
        /// </returns>
        [Get("/api/account/username/check")]
        Task<IApiResponse> CheckUsernameExistenceAsync([Query] string username, CancellationToken cancellationToken = default);
    }

}