using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components.Forms;

namespace HamkareBlazor;

public class ApiService(HttpClient httpClient)
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    static ApiService()
    {
        JsonOptions.Converters.Add(new JsonStringEnumConverter());
    }
    
    private async Task<ApiResponse<T>> HandleResponse<T>(
        HttpResponseMessage response,
        bool toast = false,
        bool redirect = false)
    {
        if (response.IsSuccessStatusCode)
            return await ConvertAsync<T>(response);

        var message = await GetErrorMessage(response);

        switch (response.StatusCode)
        {
            case HttpStatusCode.BadRequest:
                throw CreateException(
                    "BadRequest",
                    message,
                    toast,
                    redirect,
                    "/Error");

            case HttpStatusCode.NotFound:
                throw CreateException(
                    "NotFound",
                    message,
                    toast,
                    redirect,
                    "/NotFound");

            case HttpStatusCode.Unauthorized:
                throw CreateException(
                    "Unauthorized",
                    message,
                    toast,
                    redirect,
                    "/login");

            case HttpStatusCode.Forbidden:
                throw CreateException(
                    "Forbidden",
                    message,
                    toast,
                    redirect,
                    "/login");

            default:
                throw CreateException(
                    $"Http_{(int)response.StatusCode}",
                    string.IsNullOrWhiteSpace(message)
                        ? string.Format(
                            Resources.LanguageResource.Error_ServerStatusCode,
                            response.StatusCode)
                        : message,
                    toast,
                    redirect,
                    "/Error");
        }
    }

    private static HamkareException CreateException(
        string errorCode,
        string? message,
        bool toast,
        bool redirect,
        string redirectUrl)
    {
        message = string.IsNullOrWhiteSpace(message)
            ? Resources.LanguageResource.Error_ServerExceptionDontHaveMessage
            : message;

        if (redirect)
        {
            return new RedirectException(
                redirectUrl,
                errorCode,
                message);
        }

        if (toast)
        {
            return new ToastException(
                errorCode,
                message);
        }

        return new SilenceException(
            errorCode,
            message);
    }
    
    private async Task<string?> GetErrorMessage(
        HttpResponseMessage response)
    {
        try
        {
            var result =
                await response.Content.ReadFromJsonAsync<ApiResponse<string>>(
                    JsonOptions);

            return result?.Message;
        }
        catch (JsonException)
        {
            return null;
        }
    }
    
    private async Task<ApiResponse<T>> ConvertAsync<T>(
        HttpResponseMessage response)
    {
        try
        {
            return await response.Content
                       .ReadFromJsonAsync<ApiResponse<T>>(JsonOptions)
                   ?? throw new ToastException(
                       "InvalidApiResponse",
                       Resources.LanguageResource.Error_ServerSendNull);
        }
        catch (HamkareException)
        {
            throw;
        }
        catch (JsonException exception)
        {
            await Console.Error.WriteLineAsync(
                $"Try.Parse Error: {response.RequestMessage?.RequestUri}");

            throw new ToastException(
                "InvalidApiResponse",
                Resources.LanguageResource.Error_ServerSendNull,
                exception);
        }
    }

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

    public async Task<T?> PostAsync<T>(
        string url,
        object data)
    {
        var response =
            await httpClient.PostAsJsonAsync(url, data, JsonOptions);

        var result = await HandleResponse<T>(response);

        return result.Value;
    }

    public async Task<T?> PutAsync<T>(
        string url,
        object data)
    {
        var response =
            await httpClient.PutAsJsonAsync(url, data, JsonOptions);

        var result = await HandleResponse<T>(response);

        return result.Value;
    }

    #endregion

    #region Toast

    public async Task<T?> GetToastAsync<T>(string url)
    {
        var response = await httpClient.GetAsync(url);
        var result = await HandleResponse<T>(response, toast: true);

        return result.Value;
    }

    public async Task<bool> DeleteToastAsync(string url)
    {
        var response = await httpClient.DeleteAsync(url);
        var result = await HandleResponse<bool>(response, toast: true);

        return result.Value;
    }

    public async Task<T?> PostToastAsync<T>(
        string url,
        object data)
    {
        var response =
            await httpClient.PostAsJsonAsync(url, data, JsonOptions);

        var result = await HandleResponse<T>(response, toast: true);

        return result.Value;
    }

    public async Task<T?> PutToastAsync<T>(
        string url,
        object data)
    {
        var response =
            await httpClient.PutAsJsonAsync(url, data, JsonOptions);

        var result = await HandleResponse<T>(response, toast: true);

        return result.Value;
    }

    #endregion

    #region Primary

    public async Task<T?> GetPrimaryAsync<T>(string url)
    {
        var response = await httpClient.GetAsync(url);

        var result = await HandleResponse<T>(
            response,
            toast: true,
            redirect: true);

        return result.Value;
    }

    public async Task<bool> DeletePrimaryAsync(string url)
    {
        var response = await httpClient.DeleteAsync(url);

        var result = await HandleResponse<bool>(
            response,
            toast: true,
            redirect: true);

        return result.Value;
    }

    public async Task<T?> PostPrimaryAsync<T>(
        string url,
        object data)
    {
        var response =
            await httpClient.PostAsJsonAsync(url, data, JsonOptions);

        var result = await HandleResponse<T>(
            response,
            toast: true,
            redirect: true);

        return result.Value;
    }

    public async Task<T?> PutPrimaryAsync<T>(
        string url,
        object data)
    {
        var response =
            await httpClient.PutAsJsonAsync(url, data, JsonOptions);

        var result = await HandleResponse<T>(
            response,
            toast: true,
            redirect: true);

        return result.Value;
    }

    #endregion

    #region Upload

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

            await using var stream =
                file.OpenReadStream(maxFileSize);

            using var fileContent =
                new StreamContent(stream);

            content.Add(
                fileContent,
                formFieldName,
                file.Name);

            var response =
                await httpClient.PostAsync(url, content);

            var result =
                await HandleResponse<string>(
                    response,
                    toast: showToast);

            return result.Value;
        }
        catch (HamkareException)
        {
            throw;
        }
        catch (IOException exception)
        {
            throw new ToastException(
                "FileReadError",
                Resources.LanguageResource.HamkareFileUpload_ErrorReadFile,
                exception);
        }
        catch (HttpRequestException exception)
        {
            throw new ToastException(
                "ConnectionError",
                Resources.LanguageResource.Error_ConnectWithServer,
                exception);
        }
    }

    #endregion
}

