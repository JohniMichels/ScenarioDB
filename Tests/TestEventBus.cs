using Xunit;
using ScenarioDB;
using System.Collections.Generic;
using System.Reactive.Linq;
using FluentAssertions;
using Xunit.Categories;
using System;

public class TestEventBus
{

    [Fact]
    [UnitTest]
    public void ObservableReceivesOnRecordSent()
    {
        var bus = new EventBus();
        var record = new ScenarioDB.Record(){
            Id = "123",
            Path = "test",
            Fields = new Dictionary<string, dynamic>()
            {
                { "test", "test" }
            }
        };

        var fired = false;

        var events = bus.Events.Do(
            x => {
                fired = true;
                x.Should().Be(record);
            }
        );

        bus.SendRecord(record);

        events
            .Take(1)
            .Count()
            .Timeout(System.TimeSpan.FromMilliseconds(20))
            .Wait();
            
        fired.Should().BeTrue();

    }

    [Fact]
    [UnitTest]
    public void ObservableDontStoreRecords()
    {
        var bus = new EventBus();
        var record = new ScenarioDB.Record(){
            Id = "123",
            Path = "test",
            Fields = new Dictionary<string, dynamic>()
            {
                { "test", "test" }
            }
        };

        bus.SendRecord(record);

        var act = () =>
        {
            bus.Events.Timeout(TimeSpan.FromMilliseconds(20)).LastAsync().Wait();
        };

        act.Should().Throw<TimeoutException>();

    }
}