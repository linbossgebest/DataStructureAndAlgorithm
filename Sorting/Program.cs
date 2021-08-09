using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Sorting
{
    class Program
    {
        static void Main(string[] args)
        {


            #region Process

            //Process[] pros = Process.GetProcesses();

            //foreach (var item in pros)
            //{
            //    Console.WriteLine(item);
            //}

            // Process.Start("calc");
            //Process.Start("notepad");
            Parallel.For(1, 11, x => {
                Console.WriteLine(x);
            });

            #endregion


            //int[] nums = { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };
            //BubbleSort.Sort(nums);
            //SelectSort.Sort(nums);
            //InsertSort.Sort(nums);
            //MergeSort.Sort(nums);
            //string s2;
            //string s1 = s2 = "123456";
            //s1 = "444";

            //Console.WriteLine(s1);
            //Console.WriteLine(s2);

            //Console.WriteLine(Math.Round(-11.5));

            //new B();

            //test3 t3 = new test3();
            //foreach (var item in t3)
            //{
            //    Console.WriteLine(item);
            //}


            //String s = "123";
            //int l = s.Length;

            //string[] s1 = { "1", "2", "3" };


            //int y = 5;
            //Func<int, int> lambda = x => x + y;
            //Console.WriteLine(lambda(1));
            //y = 10;
            //Console.WriteLine(lambda(1));

            //Hashtable ht;

            #region 协变&逆变

            Bird bird1 = new Bird();
            Bird bird2 = new Sparrow();
            Sparrow sparrow1 = new Sparrow();

            //Sparrow sparrow2 = new Bird();//这个是编译不通过的，违反了继承性。
            //List<Bird> birdList1 = new List<Bird>();
            //List<Bird> birdList2 = new List<Sparrow>();//不是父子关系，没有继承关系
            //一群麻雀一定是一群鸟

            //IEnumerable<Bird> birdList1 = new List<Bird>();

            //IEnumerable<Bird> birdList2 = new List<Sparrow>();//协变  一群麻雀一定是一群鸟

            //ICustomerListOut<Bird> customerList1 = new CustomerListOut<Bird>();//这是能编译的
            //ICustomerListOut<Bird> customerList2 = new CustomerListOut<Sparrow>();//这也是能编译的，在泛型中，子类指向父类，我们称为协变


            ICustomerListIn<Sparrow> customerList2 = new CustomerListIn<Sparrow>();
            ICustomerListIn<Sparrow> customerList1 = new CustomerListIn<Bird>();//父类指向子类，我们称为逆变

            ICustomerListIn<Bird> birdList1 = new CustomerListIn<Bird>();
            birdList1.Show(new Sparrow());
            birdList1.Show(new Bird());

            #endregion

            #region 多线程 Thread ThreadPool Task

            //LinkedList<int> ls = new LinkedList<int>();
            //for (int i = 0; i < 100000; i++)
            //{
            //    Task.Run(() => 
            //    {
            //        ls.AddLast(i);
            //    });
            //}

            //Thread.Sleep(5000);
            //Console.WriteLine("linkedlist长度"+ls.Count);


            //Thread t1 = new Thread(bird1.test);
            // bird1.test(11);
            //int work;
            //int complete;
            //ThreadPool.GetMaxThreads(out work, out complete);
            //Console.WriteLine(work+";"+complete);


            //Task t = new Task(() => {
            //    Console.WriteLine("任务工作开始。。。");
            //    Thread.Sleep(1000);
            //});
            //t.Start();
            //t.ContinueWith((t) =>
            //{
            //    Console.WriteLine("任务完成，完成时的状态为:");
            //    Console.WriteLine("IsCanceled={0}\tIsCompleted={1}\tIsFaulted={2}", t.IsCanceled, t.IsCompleted, t.IsFaulted);
            //});

            //Console.WriteLine("主线程执行业务处理.");
            //AsyncFunction();
            //Console.WriteLine("主线程执行其他处理");
            //for (int i = 0; i < 10; i++)
            //{
            //    Console.WriteLine(string.Format("Main:i={0}", i));
            //}
            //Console.ReadLine();


            //var ret1 = AsyncGetSum();
            //Console.WriteLine("主线程执行其他处理");
            //for (int i = 1; i <= 3; i++)
            //    Console.WriteLine("Call Main()");
            //int result = ret1.Result;                  //阻塞主线程
            //Console.WriteLine("任务执行结果：{0}", result);

            //TaskFactory

            #endregion


            //Queue queue = new Queue();
            //queue.Dequeue();
            //Stack stack = new Stack();


            //int[] nums = {1,7,9,6,3,5 };
            //int[] ta = new int[10];
            ////Array arr 
            //foreach (var i in nums)
            //{
            //    Console.WriteLine(i);
            //}
            //var newNums= BubbleSort(nums);

            Console.ReadKey();
        }

        public static int[] BubbleSort(int[] nums)
        {
            for (int i = 0; i < nums.Length-1; i++)
            {
                for (int j = 0; j < nums.Length - i-1; j++)
                {
                    if (nums[j] > nums[j + 1])
                    {
                        int temp = nums[j];
                        nums[j] = nums[j + 1];
                        nums[j + 1] = temp;
                    }
                }
            }
            return nums;
        }

        async static void AsyncFunction()
        {
            await Task.Delay(1000);
            Console.WriteLine("使用Task执行异步任务--delay 1s");
            Console.WriteLine("使用Task执行异步任务--begin");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"AsyncFunction{i}");
            }

        }

        async static Task<int> AsyncGetSum()
        {
            await Task.Delay(1000);
            int sum = 0;
            Console.WriteLine("使用Task执行异步任务--begin");
            for (int i = 0; i < 10; i++)
            {
                sum += i;
            }

            return sum;
        }
        
    }





    /// <summary>
    /// 鸟
    /// </summary>
    public class Bird
    {
        public int Id { get; set; }

        public void test(int i)
        {
            lock (this)
            {
                if (i > 10)
                {
                    i--;
                    test(i);
                }
            }
        }
    }
    /// <summary>
    /// 麻雀
    /// </summary>
    public class Sparrow : Bird
    {
        public string Name { get; set; }
    }

    /// <summary>
    /// out 协变 只能是返回结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICustomerListOut<out T>
    {
        T Get();

        // void Show(T t);//T不能作为传入参数
    }

    /// <summary>
    /// 类没有协变逆变
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CustomerListOut<T> : ICustomerListOut<T>
    {
        public T Get()
        {
            return default(T);
        }

        public void Show(T t)
        {

        }
    }

    /// <summary>
    /// 逆变
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICustomerListIn<in T>
    {
        //T Get();//不能作为返回值

        void Show(T t);
    }

    public class CustomerListIn<T> : ICustomerListIn<T>
    {
        public T Get()
        {
            return default(T);
        }

        public void Show(T t)
        {
        }
    }

    class test1
    {
        static test1()
        {
        }

        private test1() { }

        public test1(string s) { }
    }

    abstract class test2 : test1
    {
        public test2(string s) : base(s) { }
    }

    interface Itest3
    {
        public int MyProperty { get; set; }
    }

    class A
    {
        public A()
        {
            PrintFields();
        }
        public virtual void PrintFields() { }
    }
    class B : A
    {
        int x = 1;
        int y;
        public B()
        {
            y = -1;
        }
        public override void PrintFields()
        {
            Console.WriteLine("x={0},y={1}", x, y);
        }
    }

    class test3 : IEnumerable
    {
        string[] s = { "one", "two", "three", "four" };

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < s.Length; i++)
                yield return s[i];
        }
    }
}
