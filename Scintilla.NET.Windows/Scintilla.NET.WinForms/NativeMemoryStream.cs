using System;
using System.IO;
using System.Runtime.InteropServices;

namespace ScintillaNet.WinForms;

/// <summary>
/// Like an UnmanagedMemoryStream execpt it can grow.
/// </summary>
internal sealed unsafe class NativeMemoryStream : Stream
{
    #region Fields

    private IntPtr ptr;
    private int capacity;
    private int position;
    private int length;

    #endregion Fields

    #region Methods

    protected override void Dispose(bool disposing)
    {
        if (FreeOnDispose && ptr != IntPtr.Zero)
        {
            Marshal.FreeHGlobal(ptr);
            ptr = IntPtr.Zero;
        }

        base.Dispose(disposing);
    }

    public override void Flush()
    {
        // NOP
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        throw new NotImplementedException();
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
        switch (origin)
        {
            case SeekOrigin.Begin:
                position = (int)offset;
                break;

            default:
                throw new NotImplementedException();
        }

        return position;
    }

    public override void SetLength(long value)
    {
        throw new NotImplementedException();
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        if (position + count > capacity)
        {
            // Realloc buffer
            var minCapacity = position + count;
            var newCapacity = capacity * 2;
            if (newCapacity < minCapacity)
            {
                newCapacity = minCapacity;
            }

            var newPtr = Marshal.AllocHGlobal(newCapacity);
            NativeMethods.MoveMemory(newPtr, ptr, length);
            Marshal.FreeHGlobal(ptr);

            ptr = newPtr;
            capacity = newCapacity;
        }

        Marshal.Copy(buffer, offset, (IntPtr)((long)ptr + position), count);
        position += count;
        length = Math.Max(length, position);
    }

    #endregion Methods

    #region Properties

    public override bool CanRead => throw new NotImplementedException();

    public override bool CanSeek => true;

    public override bool CanWrite => true;

    public bool FreeOnDispose { get; set; }

    public override long Length => length;

    public IntPtr Pointer => ptr;

    public override long Position
    {
        get => position;
        set => throw new NotImplementedException();
    }

    #endregion Properties

    #region Constructors

    public NativeMemoryStream(int capacity)
    {
        if (capacity < 4)
        {
            capacity = 4;
        }

        this.capacity = capacity;
        ptr = Marshal.AllocHGlobal(capacity);
        FreeOnDispose = true;
    }

    #endregion Constructors
}