// Copyright (c) HamkareBlazor 2021
// HamkareBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace HamkareBlazor;

public interface IApiService
{
    Task<T?> GetAsync<T>(string url);

    Task<bool> DeleteAsync(string url);

    Task<T?> PostAsync<T>(string url, object data);

    Task<T?> PutAsync<T>(string url, object data);

    Task<T?> GetToastAsync<T>(string url);

    Task<bool> DeleteToastAsync(string url);
    
    Task<T?> PostToastAsync<T>(string url, object data);
    
    Task<T?> PutToastAsync<T>(string url, object data);

    Task<T?> GetPrimaryAsync<T>(string url);

    Task<bool> DeletePrimaryAsync(string url);

    Task<T?> PostPrimaryAsync<T>(string url, object data);

    Task<T?> PutPrimaryAsync<T>(string url, object data);
}
