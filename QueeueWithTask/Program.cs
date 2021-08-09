using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace QueeueWithTask
{
    class Program
    {
        static void Main(string[] args)
        {
            //var queueAll = new Queue();
            var queueAll=Queue.Synchronized(new Queue());
            //ConcurrentQueue<string> queueAll = new ConcurrentQueue<string>();
            for (int i = 0; i < 10; i++)
            {
                queueAll.Enqueue(i.ToString());
                Console.WriteLine(i.ToString()) ;
            }

            Parallel.For(0, 150, (t, state) =>
            {
                Thread.Sleep(100);
                Buy(queueAll, t);
            });
            List<Task> tasks = new List<Task>();
            // tasks.Add(TaskFactory.StartNew(() =>Buy(queueAll));
            //for (int i = 0; i < 150; i++)l
            //{
            //    int k = i;
            //    tasks.Add(Task.Factory.StartNew(() =>
            //    {
            //        Thread.Sleep(100);
            //        if (queueAll.Count > 0)
            //        {
            //            queueAll.Dequeue();
            //            Console.WriteLine("用户" + k + "秒杀成功，队列减一" + queueAll.Count + " 线程id:" + Thread.CurrentThread.ManagedThreadId + "时间：" + DateTime.Now);
            //        }
            //        else
            //        {
            //            Console.WriteLine("用户" + k + "很遗憾，秒杀结束。。。。" + " 线程id:" + Thread.CurrentThread.ManagedThreadId + "时间：" + DateTime.Now);
            //        }

            //    }));
            //}

            Task.WaitAll(tasks.ToArray());

            Console.ReadKey();
        }

        static void Buy(Queue queueAll,int k)
        {
            Task.Run(()=>{
                Thread.Sleep(500);
                if (queueAll.Count > 0)
                {
                    queueAll.Dequeue();
                    Console.WriteLine("用户" + k + "秒杀成功，队列减一" + queueAll.Count+" 线程id:"+Thread.CurrentThread.ManagedThreadId + "时间："+DateTime.Now);
                }
                else
                {
                    Console.WriteLine("用户" + k + "很遗憾，秒杀结束。。。。" + " 线程id:" + Thread.CurrentThread.ManagedThreadId + "时间：" + DateTime.Now);
                }

            });
        }

       
    }
}
