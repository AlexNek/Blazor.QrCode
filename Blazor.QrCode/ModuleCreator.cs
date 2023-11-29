using Microsoft.JSInterop;

namespace Blazor.QrCode;

public class ModuleCreator
{
    public ModuleCreator(IJSRuntime jsInstance)
    {
        JsInstance = jsInstance;
    }

    public async Task<QrCodeModule> CreateAsync()
    {
        IJSObjectReference moduleRef = await JsInstance.InvokeAsync<IJSObjectReference>(
                                           "import",
                                           "./_content/Blazor.QrCode/qrcodeInterop.js").ConfigureAwait(false);

        return new QrCodeModule(JsInstance, moduleRef);
    }

    public IJSRuntime JsInstance { get; }
}
