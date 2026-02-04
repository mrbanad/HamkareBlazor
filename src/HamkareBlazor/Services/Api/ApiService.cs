using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using HamkareBlazor.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace HamkareBlazor;

[UnconditionalSuppressMessage("Trimming",
    "IL2026:Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code",
    Justification = "<Pending>")]
public class ApiService(HttpClient httpClient, ISnackbar snackbar, NavigationManager navigationManager) : IApiService
{
    #region Simple

    public async Task<T?> GetAsync<T>(string url)
    {
        var response = await httpClient.GetAsync(url);
        var result = await HandleResponse<T>(response);
        return result.Value;
    }

    public async Task<bool> DeleteAsync(string url)
    {
        var response = await httpClient.DeleteAsync(url);
        var result = await HandleResponse<bool>(response);
        return result.Value;
    }

    public async Task<T?> PostAsync<T>(string url, object data)
    {
        var response = await httpClient.PostAsJsonAsync(url, data);
        var result = await HandleResponse<T>(response);
        return result.Value;
    }

    public async Task<T?> PutAsync<T>(string url, object data)
    {
        var response = await httpClient.PutAsJsonAsync(url, data);
        var result = await HandleResponse<T>(response);
        return result.Value;
    }

    #endregion

    #region Toast

    public async Task<T?> GetToastAsync<T>(string url)
    {
        var response = await httpClient.GetAsync(url);
        var result = await HandleResponse<T>(response, true);
        return result.Value;
    }

    public async Task<bool> DeleteToastAsync(string url)
    {
        var response = await httpClient.DeleteAsync(url);
        var result = await HandleResponse<bool>(response, true);
        SendMessage(result.Type, result.Message);
        return result.Value;
    }

    public async Task<T?> PostToastAsync<T>(string url, object data)
    {
        var response = await httpClient.PostAsJsonAsync(url, data);
        var result = await HandleResponse<T>(response, true);
        SendMessage(result.Type, result.Message);
        return result.Value;
    }

    public async Task<T?> PutToastAsync<T>(string url, object data)
    {
        var response = await httpClient.PutAsJsonAsync(url, data);
        var result = await HandleResponse<T>(response, true);
        SendMessage(result.Type, result.Message);
        return result.Value;
    }

    #endregion

    #region Primary

    public async Task<T?> GetPrimaryAsync<T>(string url)
    {
        var response = await httpClient.GetAsync(url);
        var result = await HandleResponse<T>(response, true, true);
        return result.Value;
    }

    public async Task<bool> DeletePrimaryAsync(string url)
    {
        var response = await httpClient.DeleteAsync(url);
        var result = await HandleResponse<bool>(response, true, true);
        return result.Value;
    }

    public async Task<T?> PostPrimaryAsync<T>(string url, object data)
    {
        var response = await httpClient.PostAsJsonAsync(url, data);
        var result = await HandleResponse<T>(response, true, true);
        return result.Value;
    }

    public async Task<T?> PutPrimaryAsync<T>(string url, object data)
    {
        var response = await httpClient.PutAsJsonAsync(url, data);
        var result = await HandleResponse<T>(response, true, true);
        return result.Value;
    }

    #endregion

    private async Task<ApiResponse<T?>> HandleResponse<T>(HttpResponseMessage response, bool toast = false,
        bool redirect = false)
    {
        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        options.Converters.Add(new JsonStringEnumConverter());

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<T?>>(options) ??
                         throw new BusinessException(128002,
                             LanguageResource.ErrorServerSendNull);

            if (toast)
                SendMessage(result.Type, result.Message);

            return result;
        }

        switch (response.StatusCode)
        {
            case HttpStatusCode.BadRequest:
                {
                    var result = await response.Content.ReadFromJsonAsync<ApiResponse<T?>>(options) ??
                                 throw new BusinessException(128002,
                                     LanguageResource.ErrorServerSendNull);

                    if (redirect)
                        navigationManager.NavigateTo("/Error");

                    if (toast)
                    {
                        if (string.IsNullOrWhiteSpace(result.Message))
                            throw new BusinessException(128003,
                                LanguageResource.ErrorServerExceptionDontHaveMessage);

                        throw new BusinessException(result.Message);
                    }

                    throw new SilenceException();
                }
            case HttpStatusCode.NotFound:
                {
                    var result = await response.Content.ReadFromJsonAsync<ApiResponse<T?>>(options) ??
                                 throw new BusinessException(128002,
                                     LanguageResource.ErrorServerSendNull);

                    if (redirect)
                        navigationManager.NavigateTo("/NotFound");

                    if (toast)
                    {
                        if (string.IsNullOrWhiteSpace(result.Message))
                            throw new BusinessException(128004,
                                LanguageResource.ErrorServerExceptionDontHaveMessage);

                        throw new BusinessException(result.Message);
                    }

                    throw new SilenceException();
                }
            case HttpStatusCode.Unauthorized:
            case HttpStatusCode.Forbidden:
                {
                    var result = await response.Content.ReadFromJsonAsync<ApiResponse<T?>>(options) ??
                                 throw new BusinessException(128002,
                                     LanguageResource.ErrorServerSendNull);

                    if (redirect)
                        navigationManager.NavigateTo("/login");

                    if (toast)
                    {
                        if (string.IsNullOrWhiteSpace(result.Message))
                            throw new BusinessException(128005,
                                LanguageResource.ErrorServerExceptionDontHaveMessage);

                        throw new BusinessException(result.Message);
                    }

                    throw new SilenceException();
                }
            default:
                {
                    if (redirect)
                        navigationManager.NavigateTo("/Error");

                    throw new BusinessException(128006, string.Format(
                        LanguageResource.ErrorServerStatusCode,
                        response.StatusCode.ToString()));
                }
        }
    }

    public async Task<string?> UploadFileAsync(
        string url,
        IBrowserFile file,
        string formFieldName = "file",
        bool showToast = true,
        long maxFileSize = 10 * 1024 * 1024)
    {
        try
        {
            using var content = new MultipartFormDataContent();
            await using var stream = file.OpenReadStream(maxFileSize);

            var fileContent = new StreamContent(stream);
            content.Add(fileContent, formFieldName, file.Name);

            var response = await httpClient.PostAsync(url, content);
            var result = await HandleResponse<string>(response, true);
            return result.Value;
        }
        catch (Exception ex)
        {
            if (showToast)
            {
                SendMessage(Severity.Error, ex switch
                {
                    IOException _ => LanguageResource.ErrorReadFile,
                    HttpRequestException _ => LanguageResource.ErrorConnectWithServer,
                    _ => LanguageResource.ErrorUploadingFile
                });
            }

            Console.WriteLine($@"UploadFileAsync error: {ex.Message}");
            return null;
        }
    }

    private void SendMessage(Severity type, string? message)
    {
        switch (type)
        {
            case Severity.Error:
                if (!string.IsNullOrWhiteSpace(message))
                    snackbar.Error(message);
                break;
            case Severity.Warning:
                if (!string.IsNullOrWhiteSpace(message))
                    snackbar.Warning(message);
                break;
            case Severity.Success:
                if (!string.IsNullOrWhiteSpace(message))
                    snackbar.Success(message);
                break;
            default:
                if (!string.IsNullOrWhiteSpace(message))
                    snackbar.Info(message);
                break;
        }
    }
}
