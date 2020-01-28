using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garbage
{

    class Resources : IDisposable
    {
        TextReader t = null;
        public Resources(string path)
        {
            Console.WriteLine("entering managed data");
            t = new StreamReader(path);
        }
        void ReleaseManagedResources()
        {
            Console.WriteLine("Releasing Managed Resources");
            if (t != null)
            {
                t.Dispose();
            }
        }
        void ReleaseUnmangedResources()
        {
            Console.WriteLine("Releasing Unmanaged Resources");
        }

        public void ShowData()
        {
            //Emulate class usage
            if (t != null)
            {
                Console.WriteLine(t.ReadToEnd() );
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            Console.WriteLine("disposing is " + disposing.ToString());
            if (disposing==true)
            {
                ReleaseManagedResources();
            }
            ReleaseUnmangedResources();
        }
        public void collect()
        {
            long mem1 = GC.GetTotalMemory(false);
            {
                int[] val = new int[500];
                val = null;
            }
            GC.Collect();
            long mem2 = GC.GetTotalMemory(false);
            Console.WriteLine(mem1+"  "+mem2);
        }
        ~Resources()
        {
            Console.WriteLine("Finalizer Called");
            Dispose(false);
        }
        static void Main(string[] args)
        {
            Resources r = new Resources(@"C:\Users\Pranshika_Singla\Desktop\visual\file1.txt");
            /* try
             {
                 r = new Resources(@"C:\Users\Pranshika_Singla\Desktop\visual\file1.txt");
                     r.ShowData();
             }
             finally
                 {
                 r.Dispose();
                     }*/
        
                
                using (r)
                {
                    r.ShowData();
                }
              
                r.collect();
               
            
        }
    }
}
