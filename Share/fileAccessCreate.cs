using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;

class Program
{
    static void Main(string[] args)
    {
        long offset = 0x00000010; // 256 megabytes
        long length = 0x00000010; // 512 megabytes

        // Create the memory-mapped file.
        using (var mmf = MemoryMappedFile.CreateFromFile(@"fileA.txt", FileMode.Open, "ImgA"))
        {
            // Create a random access view, from the 256th megabyte (the offset)
            // to the 768th megabyte (the offset plus length).
            using (var accessor = mmf.CreateViewAccessor(offset, length))
            {
                int colorSize = Marshal.SizeOf(typeof(MyColor));
                MyColor color;

                // Make changes to the view.
                for (long i = 0; i < length; i += colorSize)
                {
                    accessor.Read(i, out color);
                    color.Brighten(10);
                    accessor.Write(i, ref color);
                }
            }
        }
    }
}

public struct MyColor
{
    public int _value;

    // Make the view brighter.
    public void Brighten(short value)
    {
        _value += value;
    }
}