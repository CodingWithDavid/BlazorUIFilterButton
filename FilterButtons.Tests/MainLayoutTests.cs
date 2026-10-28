using Bunit;
using FilterButtons.Layout;
using Microsoft.AspNetCore.Components;

namespace FilterButtons.Tests;

public sealed class MainLayoutTests : BunitContext
{
    [Fact]
    public void ThemeSwitchTogglesModeAndAccessibleState()
    {
        var cut = Render<MainLayout>(parameters => parameters
            .Add(component => component.Body,
                (RenderFragment)(builder => builder.AddMarkupContent(0, "<p>Test content</p>"))));

        var toggle = cut.Find(".theme-toggle");

        Assert.Equal("Switch to dark mode", toggle.GetAttribute("aria-label"));
        Assert.Equal("false", toggle.GetAttribute("aria-pressed"));
        Assert.Contains("light-mode", cut.Find(".page").ClassList);

        toggle.Click();

        Assert.Equal("Switch to light mode", cut.Find(".theme-toggle").GetAttribute("aria-label"));
        Assert.Equal("true", cut.Find(".theme-toggle").GetAttribute("aria-pressed"));
        Assert.Contains("dark-mode", cut.Find(".page").ClassList);
    }
}
