using Bunit;
using FilterButtons.Pages;
using Microsoft.AspNetCore.Components;

namespace FilterButtons.Tests;

public sealed class HomeTests : BunitContext
{
    [Fact]
    public void StartsWithAllSampleFiltersAndDisablesPickerWhenNoFieldsRemain()
    {
        var cut = Render<Home>();

        Assert.Equal(3, cut.FindAll(".filter-chip").Count);
        Assert.Equal(2, cut.FindAll("tbody tr").Count);
        Assert.True(cut.Find("#filter-field").HasAttribute("disabled"));
        Assert.True(cut.Find(".apply-button").HasAttribute("disabled"));
        Assert.Contains("All available fields are already being used.", cut.Markup);
    }

    [Fact]
    public void RemovingFilterMakesOnlyThatFieldAvailable()
    {
        var cut = Render<Home>();

        cut.Find("[aria-label='Remove Status: Open']").Click();
        cut.WaitForAssertion(() =>
        {
            var select = cut.Find("#filter-field");

            Assert.False(select.HasAttribute("disabled"));
            Assert.Equal(2, cut.FindAll(".filter-chip").Count);
            Assert.Equal("Status", select.QuerySelector("option[value='status-equals']")?.TextContent);
            Assert.Null(select.QuerySelector("option[value='region-equals']"));
            Assert.Null(select.QuerySelector("option[value='owner-equals']"));
        });

        Assert.True(cut.Find(".apply-button").HasAttribute("disabled"));
    }

    [Fact]
    public void ApplyingSelectedFieldAddsFilterAndRefreshesResults()
    {
        var cut = Render<Home>();

        cut.Find("[aria-label='Remove Status: Open']").Click();
        cut.WaitForAssertion(() => Assert.False(cut.Find("#filter-field").HasAttribute("disabled")));

        cut.Find("#filter-field").Change(new ChangeEventArgs { Value = "status-equals" });
        cut.WaitForAssertion(() => Assert.False(cut.Find(".apply-button").HasAttribute("disabled")));

        cut.Find(".apply-button").Click();
        cut.WaitForAssertion(() =>
        {
            Assert.Equal(3, cut.FindAll(".filter-chip").Count);
            Assert.Equal(2, cut.FindAll("tbody tr").Count);
            Assert.True(cut.Find("#filter-field").HasAttribute("disabled"));
        }, TimeSpan.FromSeconds(2));
    }
}
