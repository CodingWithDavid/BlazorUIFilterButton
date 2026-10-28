using Bunit;
using FilterButtons.Components;
using Microsoft.AspNetCore.Components;

namespace FilterButtons.Tests;

public sealed class ActiveFilterBarTests : BunitContext
{
    [Fact]
    public void RendersFiltersWithAccessibleActionsAndCount()
    {
        var filters = new[]
        {
            new ActiveFilterBar.FilterToken("status", "Status", "Open"),
            new ActiveFilterBar.FilterToken("owner", "Owner", "David")
        };

        var cut = Render<ActiveFilterBar>(parameters => parameters
            .Add(component => component.Filters, filters)
            .Add(component => component.OnRemove,
                EventCallback.Factory.Create<ActiveFilterBar.FilterToken>(this, _ => { }))
            .Add(component => component.OnClearAll,
                EventCallback.Factory.Create(this, () => { })));

        Assert.Equal(2, cut.FindAll(".filter-chip").Count);
        Assert.Equal("Remove Status: Open", cut.Find("[aria-label='Remove Status: Open']").GetAttribute("aria-label"));
        Assert.Equal("Remove Owner: David", cut.Find("[aria-label='Remove Owner: David']").GetAttribute("aria-label"));
        Assert.Equal("2 active filters.", cut.Find("[role='status']").TextContent.Trim());
        Assert.NotNull(cut.Find(".clear-button"));
    }

    [Fact]
    public void RemoveInvokesCallbackForTheSelectedFilter()
    {
        ActiveFilterBar.FilterToken? removed = null;
        var filters = new[]
        {
            new ActiveFilterBar.FilterToken("status", "Status", "Open")
        };

        var cut = Render<ActiveFilterBar>(parameters => parameters
            .Add(component => component.Filters, filters)
            .Add(component => component.OnRemove,
                EventCallback.Factory.Create<ActiveFilterBar.FilterToken>(
                    this,
                    filter => removed = filter)));

        cut.Find(".remove-button").Click();

        Assert.Equal(filters[0], removed);
    }

    [Fact]
    public void EmptyStateHidesClearAllAndAnnouncesNoFilters()
    {
        var cut = Render<ActiveFilterBar>(parameters => parameters
            .Add(component => component.Filters, Array.Empty<ActiveFilterBar.FilterToken>()));

        Assert.Equal("No filters are active.", cut.Find(".empty-message").TextContent);
        Assert.Empty(cut.FindAll(".clear-button"));
        Assert.Equal("No filters are active.", cut.Find("[role='status']").TextContent.Trim());
    }

    [Fact]
    public void BusyStateDisablesFilterActions()
    {
        var filters = new[]
        {
            new ActiveFilterBar.FilterToken("status", "Status", "Open"),
            new ActiveFilterBar.FilterToken("owner", "Owner", "David")
        };

        var cut = Render<ActiveFilterBar>(parameters => parameters
            .Add(component => component.Filters, filters)
            .Add(component => component.Busy, true)
            .Add(component => component.OnRemove,
                EventCallback.Factory.Create<ActiveFilterBar.FilterToken>(this, _ => { }))
            .Add(component => component.OnClearAll,
                EventCallback.Factory.Create(this, () => { })));

        Assert.All(cut.FindAll("button"), button => Assert.True(button.HasAttribute("disabled")));
        Assert.Equal("true", cut.Find(".filter-bar").GetAttribute("aria-busy"));
    }
}
