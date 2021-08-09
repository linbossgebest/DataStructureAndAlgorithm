using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static BuyTickets.BuyTrainService;

namespace BuyTickets
{
    class Program
    {
        static void Main(string[] args)
        {
            var queueTrain = new BuyTrainService().InitQueue();    //抢到票的队列

            var queueAll = new BuyTrainService().InitQueue();    //所有进入队列的用户队列

            var tt = new BuyTrainService();

            //一个并行线程进行执行抢票操作

            Parallel.For(0, 150, (t, state) =>

            {
                Thread.Sleep(100);

                Train train = new Train();

                train.Name = "用户" + t;

                var x = tt.Buy(queueAll, queueTrain, train);

                if (x.Code == -1)

                {
                    Console.WriteLine(x.Name + ":" + x.Msg + ",线程id:" + Thread.CurrentThread.ManagedThreadId);

                    //state.Break();

                }

                else

                    Console.WriteLine(x.Name + ":" + x.Msg + "还剩下:" + x.Cnt + "件"+",线程id:"+Thread.CurrentThread.ManagedThreadId);

            });

            //可以进行消化队列

            while (queueTrain.Count != 0)

            {
                Console.WriteLine("当前队列剩余个数：" + queueTrain.Count);

                queueTrain.Dequeue();

                //Console.WriteLine("处理用户订单中:" + queueTrain.Dequeue());

            }

            Console.ReadKey();
        }
    }

    public class BuyTrainService
    {
        public static int cnt = 10;

        public Queue<Train> InitQueue()

        {
            var tt = new Queue<Train>();

            return tt;

        }

        object obj = new object();

        public RetData Buy(Queue<Train> AllQueue, Queue<Train> tainQueue, Train tain)

        {
            try
            {
                AllQueue.Enqueue(tain);

                if (AllQueue.Count > cnt)
                {
                    return new RetData { Name = tain.Name, Code = -1, Msg = "商品抢光了", Cnt = 0 };

                }

                tainQueue.Enqueue(tain);

                return new RetData { Name = tain.Name, Code = 1, Msg = "恭喜已抢到", Cnt = cnt - tainQueue.Count };

            }
            catch (Exception ex)
            {
                Console.WriteLine("异常：" + ex);

                return new RetData { Name = tain.Name, Code = 1, Msg = "异常：" + ex, Cnt = cnt - tainQueue.Count };

            }

        }

        public class RetData
        {
            public string Name { get; set; }

            public int Code { get; set; }

            public string Msg { get; set; }

            public int Cnt { get; set; }
        }

        public class Train
        {
            public string Name { get; set; }
        }
    }
}
