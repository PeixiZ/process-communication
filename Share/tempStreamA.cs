using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Threading;

class Program
{
    // Process A:
    static void Main(string[] args)
    {
        using (MemoryMappedFile mmf = MemoryMappedFile.CreateNew("testmap", 10000))
        {
            bool mutexCreated;
            Mutex mutex = new Mutex(true, "testmapmutex", out mutexCreated);
            using (MemoryMappedViewStream stream = mmf.CreateViewStream())
            {
                BinaryWriter writer = new BinaryWriter(stream);
                writer.Write(1);
            }
            mutex.ReleaseMutex();

            Console.WriteLine("Start Process B and press ENTER to continue.");
            Console.ReadLine();

            mutex.WaitOne();
            using (MemoryMappedViewStream stream = mmf.CreateViewStream())
            {
                BinaryReader reader = new BinaryReader(stream);
                Console.WriteLine("Process A says: {0}", reader.ReadBoolean());
                Console.WriteLine("Process B says: {0}", reader.ReadBoolean());                
            }
            mutex.ReleaseMutex();
        }
    }
}