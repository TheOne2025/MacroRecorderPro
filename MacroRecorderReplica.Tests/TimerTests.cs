using System;
using System.Reflection;
using System.Threading;
using System.Windows;
using Xunit;

namespace MacroRecorderReplica.Tests;

public class TimerTests
{
    [Fact]
    public void TimerStopsOnWindowClosed()
    {
        Exception? captured = null;
        var thread = new Thread(() =>
        {
            try
            {
                var window = new MacroRecorderReplica.MainWindow();
                var start = typeof(MacroRecorderReplica.MainWindow).GetMethod("StartRecording_Click", BindingFlags.Instance | BindingFlags.NonPublic);
                start?.Invoke(window, new object?[] { null, new RoutedEventArgs() });
                Thread.Sleep(1200);
                var secondsField = typeof(MacroRecorderReplica.MainWindow).GetField("seconds", BindingFlags.Instance | BindingFlags.NonPublic);
                int beforeClose = (int)(secondsField?.GetValue(window) ?? 0);
                window.Close();
                Thread.Sleep(1500);
                int afterClose = (int)(secondsField?.GetValue(window) ?? 0);
                Assert.Equal(beforeClose, afterClose);
            }
            catch (Exception ex)
            {
                captured = ex;
            }
        });
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        thread.Join();
        if (captured != null)
        {
            throw new Xunit.Sdk.XunitException($"Exception in STA thread: {captured}");
        }
    }
}
