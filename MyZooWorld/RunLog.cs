using System;
using System.Collections.Generic;
using System.Text;

namespace MyZooWorld;

// A disposable log. Writes a file and closes it safely,
// even when an exception is thrown. Safe to dispose twice.
public class RunLog : IDisposable
{
    private StreamWriter writer;
    private bool disposed;

    public RunLog(string path)
    {
        writer = new StreamWriter(path);
        disposed = false;
    }

    public void Write(string line)
    {
        writer.WriteLine(line);
    }

    public void Dispose()
    {
        if (disposed)
            return;

        writer.WriteLine("Log closed");
        writer.Dispose();
        disposed = true;
    }
}