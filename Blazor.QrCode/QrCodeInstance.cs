using Microsoft.JSInterop;

namespace Blazor.QrCode;

public class QrCodeInstance : JsInteropObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="JsInteropObject"/> class.
    /// </summary>
    /// <param name="jsObjRef">The java script object reference.</param>
    /// <param name="jsModuleRef"></param>
    protected QrCodeInstance(IJSObjectReference jsObjRef, IJSObjectReference jsModuleRef)
        : base(jsObjRef)
    {
        JsModuleRef = jsModuleRef;
    }

    public async Task ClearAsync()
    {
        await JsModuleRef.InvokeVoidAsync("clear", JsObjRef);
    }

    public static QrCodeInstance Create(IJSObjectReference jsObjRef, IJSObjectReference jsModuleRef)
    {
        return new QrCodeInstance(jsObjRef, jsModuleRef);
    }

    public static async Task<QrCodeInstance> CreateAsync(IJSObjectReference jsModuleRef, string canvasId, string text, QrCodeOptions? qrCodeOptions)
    {
        IJSObjectReference? jsQrCode;
        if (qrCodeOptions is null)
        {
            jsQrCode = await jsModuleRef.InvokeAsync<IJSObjectReference>("createQrCode", canvasId, text);
        }
        else
        {
            string colorDark = ColorConverter.Convert(qrCodeOptions.ColorDark);
            string colorLight = ColorConverter.Convert(qrCodeOptions.ColorLight);
            jsQrCode = await jsModuleRef.InvokeAsync<IJSObjectReference>(
                           "createQrCode",
                           canvasId,
                           text,
                           qrCodeOptions.Size,
                           qrCodeOptions.Size,
                           colorDark,
                           colorLight,
                           (int)qrCodeOptions.ErrorCorrectionLevel);
        }

        return new QrCodeInstance(jsQrCode, jsModuleRef);
    }

    public async Task UpdateAsync(string text)
    {
        await JsModuleRef.InvokeVoidAsync("clear", JsObjRef);
        await JsModuleRef.InvokeVoidAsync("makeCode", JsObjRef, text);
    }

    public IJSObjectReference JsModuleRef { get; }
}
