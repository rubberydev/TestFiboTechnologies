using System;
using System.Threading.Tasks;
using TestFiboTechnologies.Models;

namespace TestFiboTechnologies.Services
{
    public interface IApiService
    {
        Task<Response> Delete<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken,
            T model);

        Task<Response> Get<T>(
            string urlBase,
            string servicePrefix);

        Task<Response> GetList<T>(
            string urlBase,
            string servicePrefix,
            string controller);

        Task<Response> GetList<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken);
        Task<Response> GetList<T>(string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken,
            int id);

        Task<Response> Post<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken,
            T model);

        Task<Response> Post<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            T model);

        Task<Response> Put<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken,
            T model);
    }
}
