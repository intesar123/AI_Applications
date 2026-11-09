using System.Collections.Specialized;
using System.ComponentModel;
using Microsoft.SemanticKernel;

public class LightsPlugin
{
    // Mock data for lights
    private readonly List<LightsModel> _lights = new()
    {
        new LightsModel { Id = 1, Name = "Table Lamp", isOn = true },
        new LightsModel { Id = 2, Name = "Porch light", isOn = false },
        new LightsModel { Id = 3, Name = "Chandelier", isOn = true }
    };

    [KernelFunction("get_lights")]
    [Description("Gets a list of lights and their current status")]
    public async Task<List<LightsModel>> GetLightsAsync()
    {
        return await Task.FromResult(_lights);
    }

    [KernelFunction("change_state")]
    [Description("Changes the state of a light")]
    public async Task<LightsModel?> ChangeStateAsync(int id, bool isOn)
    {
        var light = _lights.FirstOrDefault(x => x.Id == id);
        if (light != null)
        {
            light.isOn = isOn;
        }
        await Task.CompletedTask;
        return light;
    }
}