/*
 * Part 9 - Events - Introduction
 * 
The following example demonstrates using asynchronous methods to
get Domain Name System information for the specified host computer.

Code copied from: https://learn.microsoft.com/en-us/dotnevt/standard/events/how-to-raise-and-consume-events
Also:
    https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/access-modifiers



==================================
=   Raise an event without data  =
==================================
These steps create a Counter class that fires a ThresholdReached event when a running total reaches or exceeds a threshold.

Declare the event using the EventHandler delegate.

Use EventHandler when your event doesn't pass data to the handler:

C#
public event EventHandler? ThresholdReached;
Add a protected virtual method (Protected Overridable in Visual Basic) to raise the event.

This pattern lets derived classes override the event-raising behavior without directly invoking the delegate. In C#, use the null-conditional operator (?.) to guard against no subscribers (in Visual Basic, RaiseEvent handles this automatically):

C#
protected virtual void OnThresholdReached(EventArgs e)
{
    ThresholdReached?.Invoke(this, e);
}
Call the raise method when the condition is met.

Pass Empty because this event carries no data:

C#
if (_total >= _threshold)
{
    OnThresholdReached(EventArgs.Empty);
}
Subscribe to the event using the += operator (in Visual Basic, AddHandler):

C#
c.ThresholdReached += c_ThresholdReached;
Define the event handler method.

Its signature must match the EventHandler delegate—the first parameter is the event source and the second is EventArgs:

C#
static void c_ThresholdReached(object? sender, EventArgs e)
{
    Console.WriteLine("The threshold was reached.");
    Environment.Exit(0);
}
The following example shows the complete implementation:

C#
*/
/*
class EventNoData
{
    static void Main()
    {
        Counter c = new(new Random().Next(10));
        c.ThresholdReached += c_ThresholdReached;

        Console.WriteLine("press 'a' key to increase total");
        while (Console.ReadKey(true).KeyChar == 'a')
        {
            Console.WriteLine("adding one");
            c.Add(1);
        }
    }

    static void c_ThresholdReached(object? sender, EventArgs e)
    {
        Console.WriteLine("The threshold was reached.");
        Environment.Exit(0);
    }
}

class Counter(int passedThreshold)
{
    private readonly int _threshold = passedThreshold;
    private int _total;

    public void Add(int x)
    {
        _total += x;
        if (_total >= _threshold)
        {
            OnThresholdReached(EventArgs.Empty);
        }
    }

    protected virtual void OnThresholdReached(EventArgs e)
    {
        ThresholdReached?.Invoke(this, e);
    }

    public event EventHandler? ThresholdReached;
}
*/



/*
===============================
=   Raise an event with data  =
===============================
These steps extend the previous Counter example to raise an event that includes data—the threshold value and the time it was reached.
Define an event data class that inherits from EventArgs.

Add properties for each piece of data you want to pass to the handler:
C#
public class ThresholdReachedEventArgs : EventArgs
{
    public int Threshold { get; set; }
    public DateTime TimeReached { get; set; }
}

Declare the event using the EventHandler<TEventArgs> delegate, passing your event data class as the type argument:
C#
public event EventHandler<ThresholdReachedEventArgs>? ThresholdReached;
Add a protected virtual method (Protected Overridable in Visual Basic) to raise the event.

This pattern lets derived classes override the event-raising behavior without directly invoking the delegate. In C#, use the null-conditional operator (?.) to guard against no subscribers (in Visual Basic, RaiseEvent handles this automatically):
C#
protected virtual void OnThresholdReached(ThresholdReachedEventArgs e)
{
    ThresholdReached?.Invoke(this, e);
}

Populate the event data object and call the raise method when the condition is met:
C#
if (_total >= _threshold)
{
    ThresholdReachedEventArgs args = new ThresholdReachedEventArgs();
    args.Threshold = _threshold;
    args.TimeReached = DateTime.Now;
    OnThresholdReached(args);
}

Subscribe to the event using the += operator (in Visual Basic, AddHandler):
C#
c.ThresholdReached += c_ThresholdReached;
Define the event handler.

The second parameter type is ThresholdReachedEventArgs instead of EventArgs, which lets the handler read the event data:
C#
static void c_ThresholdReached(object? sender, ThresholdReachedEventArgs e)
{
    Console.WriteLine($"The threshold of {e.Threshold} was reached at {e.TimeReached}.");
    Environment.Exit(0);
}

The following example shows the complete implementation:
C#
*/

class EventWithData
{
    static void Main()
    {
        CounterWithData c = new(new Random().Next(10));
        c.ThresholdReached += c_ThresholdReached;

        Console.WriteLine("press 'a' key to increase total");
        while (Console.ReadKey(true).KeyChar == 'a')
        {
            Console.WriteLine("adding one");
            c.Add(1);
        }
    }

    static void c_ThresholdReached(object? sender, ThresholdReachedEventArgs e)
    {
        Console.WriteLine($"The threshold of {e.Threshold} was reached at {e.TimeReached}.");
        Environment.Exit(0);
    }
}

class CounterWithData(int passedThreshold)
{
    private readonly int _threshold = passedThreshold;
    private int _total;

    public void Add(int x)
    {
        _total += x;
        if (_total >= _threshold)
        {
            ThresholdReachedEventArgs args = new ThresholdReachedEventArgs();
            args.Threshold = _threshold;
            args.TimeReached = DateTime.Now;
            OnThresholdReached(args);
        }
    }

    protected virtual void OnThresholdReached(ThresholdReachedEventArgs e)
    {
        ThresholdReached?.Invoke(this, e);
    }

    public event EventHandler<ThresholdReachedEventArgs>? ThresholdReached;
}

public class ThresholdReachedEventArgs : EventArgs
{
    public int Threshold { get; set; }
    public DateTime TimeReached { get; set; }
}




/*
==============================================
=   Declare a custom delegate for an event   =
==============================================
Declare a custom delegate only in rare scenarios, such as making your class available to legacy code that can't use generics. For most cases, use EventHandler<TEventArgs> as shown in the previous section.

Declare the custom delegate type.

The delegate signature must match the event handler signature—two parameters: the event source (object; in Visual Basic, Object) and the event data class:

C#
public delegate void ThresholdReachedEventHandler(object sender, ThresholdReachedEventArgs e);
Declare the event using your custom delegate type instead of EventHandler<TEventArgs>:

C#
public event ThresholdReachedEventHandler? ThresholdReached;
Add a protected virtual method(Protected Overridable in Visual Basic) to raise the event.

In C#, use the null-conditional operator (?.) to guard against no subscribers (in Visual Basic, RaiseEvent handles this automatically):

C#
protected virtual void OnThresholdReached(ThresholdReachedEventArgs e)
{
    ThresholdReached?.Invoke(this, e);
}
Populate the event data object and call the raise method when the condition is met:

C#
if (_total >= _threshold)
{
    ThresholdReachedEventArgs args = new();
args.Threshold = _threshold;
args.TimeReached = DateTime.Now;
OnThresholdReached(args);
}
Subscribe to the event using the += operator (in Visual Basic, AddHandler):

C#
c.ThresholdReached += c_ThresholdReached;
Define the event handler.

The handler signature must match the custom delegate—object for the sender and your event data class for the second parameter:

C#
static void c_ThresholdReached(object sender, ThresholdReachedEventArgs e)
    {
        Console.WriteLine($"The threshold of {e.Threshold} was reached at {e.TimeReached}.");
        Environment.Exit(0);
    }
The following example shows the complete implementation:

C#
*/

/*
class EventWithDelegate
{
    static void Main()
    {
        CounterWithDelegate c = new(new Random().Next(10));
        c.ThresholdReached += c_ThresholdReached;

        Console.WriteLine("press 'a' key to increase total");
        while (Console.ReadKey(true).KeyChar == 'a')
        {
            Console.WriteLine("adding one");
            c.Add(1);
        }
    }

    static void c_ThresholdReached(object sender, ThresholdReachedEventArgs e)
    {
        Console.WriteLine($"The threshold of {e.Threshold} was reached at {e.TimeReached}.");
        Environment.Exit(0);
    }
}

class CounterWithDelegate(int passedThreshold)
{
    private readonly int _threshold = passedThreshold;
    private int _total;

    public void Add(int x)
    {
        _total += x;
        if (_total >= _threshold)
        {
            ThresholdReachedEventArgs args = new();
            args.Threshold = _threshold;
            args.TimeReached = DateTime.Now;
            OnThresholdReached(args);
        }
    }

    protected virtual void OnThresholdReached(ThresholdReachedEventArgs e)
    {
        ThresholdReached?.Invoke(this, e);
    }

    public event ThresholdReachedEventHandler? ThresholdReached;
}

public delegate void ThresholdReachedEventHandler(object sender, ThresholdReachedEventArgs e);

public class ThresholdReachedEventArgs : EventArgs //note: this class was missing from the ms example
{
    public int Threshold { get; set; }
    public DateTime TimeReached { get; set; }
}
*/