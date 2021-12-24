using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace ScenarioDB;

public class EventBus
{
    public EventBus()
    {
        Events = RecordQueue.Reader.ReadAllAsync().ToObservable();
    }

    private System.Threading.Channels.Channel<Record> RecordQueue { get; } =
        System.Threading.Channels.Channel.CreateUnbounded<Record>();

    public void SendRecord(Record newRecord)
    {
        RecordQueue.Writer.TryWrite(newRecord);
    }

    public IObservable<Record> Events { get; private set; }
}