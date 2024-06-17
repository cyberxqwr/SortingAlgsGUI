using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Sorting_GUI
{
    public partial class Form1 : Form
    {

        #region Variables
        const int GraphSP = 150;
        const int NUMBER = 80;
        private bool sorting = false;
        private List<Graphics> runtimeGraphics = new List<Graphics>();
        private ManualResetEvent sortingEvent = new ManualResetEvent(false);
        int[] a = new int[NUMBER];

        #endregion

        #region Initialization
        public Form1()
        {
            InitializeComponent();
            Generator();
            List<Label> labels = new List<Label>()
            {
            label1, label4, label2, label3, label5, label6, label8, label9, label10, label11
            };

            modifyLabel(labels, 30);
            label7.Text = "Numbers used to sort: " + NUMBER.ToString();

        }

        #endregion

        #region BubbleSort ASC

        /// <summary>
        /// Lyginam gretimus elementus
        /// </summary>
        void BubbleSort1()
        {
            Graphics g = CreateGraphics();
            Stopwatch stopwatch = new Stopwatch();
            const int height = 30;

            stopwatch.Start();

            double x = GraphSP;

            for (int i = 0; i < NUMBER; i++)
            {
                if (sorting && !sortingEvent.WaitOne(10))
                {
                    bool swapped = false;

                    for (int j = 0; j < NUMBER - i - 1; j++)
                    {
                        if (a[j] < a[j + 1])
                        {
                            int t = a[j];
                            a[j] = a[j + 1];
                            a[j + 1] = t;
                            swapped = true;

                            g.FillEllipse(new SolidBrush(Color.Blue), (int)x, height, 10, 10);
                            x += 0.25;
                            Thread.Sleep(2);
                        }
                    }

                    if (!swapped)
                    {
                        break;
                    }
                }
            }

            runtimeGraphics.Add(g);

            stopwatch.Stop();
            createLabel("BubbleSort", stopwatch.ElapsedMilliseconds.ToString() + "ms", height);

        }

        #endregion

        #region BubbleSort DSC
        void BubbleSort2()
        {
            Graphics g = CreateGraphics();
            double x = GraphSP;
            Stopwatch stopwatch = new Stopwatch();
            const int height = 60;

            stopwatch.Start();

            for (int i = 0; i < NUMBER; i++)
            {

                if (sorting)
                {
                    for (int j = i + 1; j < NUMBER - 1; j++)
                    {
                        if (a[j] < a[j + 1])
                        {
                            int t = a[j + 1];
                            a[j + 1] = a[j];
                            a[j] = t;
                            g.FillEllipse(new SolidBrush(Color.Blue), (int)x, height, 10, 10);
                            x += 0.25;
                            Thread.Sleep(2);
                        }
                    }
                }
            }

            runtimeGraphics.Add(g);

            stopwatch.Stop();
            createLabel("BubbleSort2", stopwatch.ElapsedMilliseconds.ToString() + "ms", height);

        }

        #endregion

        #region SelectionSort

        /// <summary>
        /// Randam maziausia elementa ir sukeiciam su pirmu elementu
        /// </summary>
        void SelectionSort()
        {
            Graphics g = CreateGraphics();

            Stopwatch stopwatch = new Stopwatch();
            const int height = 90;
            stopwatch.Start();

            double x = GraphSP;

            for (int i = 0; i < NUMBER - 1; i++)
            {

                if (sorting && !sortingEvent.WaitOne(10))
                {
                    int k = i;
                    for (int j = i + 1; j < NUMBER; j++)
                        if (a[j] < a[k])
                        {
                            k = j;
                            g.FillEllipse(new SolidBrush(Color.Blue), (int)x, height, 10, 10);
                            x += 0.25;
                            Thread.Sleep(2);
                        }
                    Swap(i, k);

                }

            }

            runtimeGraphics.Add(g);

            stopwatch.Stop();
            createLabel("SelectionSort", stopwatch.ElapsedMilliseconds.ToString() + "ms", height);
        }

        #endregion

        #region InsertionSort

        /// <summary>
        /// Masyvas dalijamas i dalis, pirmas elementas iterpiamas i tinkama pozicija rusiuotame masyve
        /// </summary>
        void InsertionSort()
        {
            Graphics g = CreateGraphics();
            double x = GraphSP;

            Stopwatch stopwatch = new Stopwatch();
            const int height = 120;
            stopwatch.Start();


            for (int i = 1; i < NUMBER; i++)
            {

                if (sorting && !sortingEvent.WaitOne(10))
                {
                    int j = i - 1, k = a[i];
                    while (j >= 0 && a[j] > k)
                    {
                        a[j + 1] = a[j];
                        j--;
                        g.FillEllipse(new SolidBrush(Color.Blue), (int)x, height, 10, 10);
                        x += 0.25;
                        Thread.Sleep(2);
                    }
                    a[j + 1] = k;
                }
            }

            runtimeGraphics.Add(g);

            stopwatch.Stop();
            createLabel("InsertionSort", stopwatch.ElapsedMilliseconds.ToString() + "ms", height);

        }

        #endregion

        #region MergeSort

        /// <summary>
        /// Padalinimas sarasas per puse, rekursiskai rusiuojamos abi puses
        /// </summary>
        /// <param name="numbers"></param>
        /// <param name="left"></param>
        /// <param name="mid"></param>
        /// <param name="right"></param>

        void DoMerge(int[] numbers, int left, int mid, int right)
        {

            Graphics g = CreateGraphics();
            double x = GraphSP;
            const int height = 150;

            int n1 = mid - left + 1;
            int n2 = right - mid;
            int[] leftArr = new int[n1];
            int[] rightArr = new int[n2];

            Array.Copy(numbers, left, leftArr, 0, n1);
            Array.Copy(numbers, mid + 1, rightArr, 0, n2);

            int i = 0, j = 0, k = left;
            while (i < n1 && j < n2)
            {
                if (leftArr[i] <= rightArr[j])
                    numbers[k++] = leftArr[i++];
                else
                    numbers[k++] = rightArr[j++];

                g.FillEllipse(new SolidBrush(Color.Blue), (int)x, height, 10, 10);
                x += 0.25;
                Thread.Sleep(2);
            }

            while (i < n1)
            {
                numbers[k++] = leftArr[i++];

                g.FillEllipse(new SolidBrush(Color.Blue), (int)x, height, 10, 10);
                x += 0.25;
                Thread.Sleep(2);
            }

            while (j < n2)
            {
                numbers[k++] = rightArr[j++];

                g.FillEllipse(new SolidBrush(Color.Blue), (int)x, height, 10, 10);
                x += 0.25;
                Thread.Sleep(2);
            }

            runtimeGraphics.Add(g);

        }

        void MergeSort_Recursive(int[] numbers, int left, int right)
        {
            if (left < right && sorting && !sortingEvent.WaitOne(10))
            {
                int mid = (left + right) / 2;
                MergeSort_Recursive(numbers, left, mid);
                MergeSort_Recursive(numbers, mid + 1, right);
                DoMerge(numbers, left, mid, right);
            }
        }

        void MergeSort()
        {
            Stopwatch stopwatch = new Stopwatch();
            const int height = 150;

            int[] numbers = new int[NUMBER];
            Array.Copy(a, numbers, a.Length);
            int left = 0, right = numbers.Length - 1;
            stopwatch.Start();
            MergeSort_Recursive(numbers, left, right);
            stopwatch.Stop();

            createLabel("MergeSort", stopwatch.ElapsedMilliseconds.ToString() + "ms", height);
        }

        #endregion

        #region QuickSort

        /// <summary>
        /// Parenkamas pivot elementas, visi elementai mazesni uz ji bus pries ji, didesni - po
        /// </summary>
        void QuickSort()
        {
            int[] b = new int[NUMBER];
            Array.Copy(a, b, a.Length);

            Stopwatch stopwatch = new Stopwatch();
            const int height = 180;
            stopwatch.Start();

            DoQuickSort(b, 0, b.Length - 1);

            stopwatch.Stop();

            createLabel("QuickSort", stopwatch.ElapsedMilliseconds.ToString() + "ms", height);
        }
        void DoQuickSort(int[] a, int start, int end)
        {

            if (start >= end)
            {
                return;
            }
            if (sorting && !sortingEvent.WaitOne(10))
            {
                int pivotIndex = Partition(start, end);
                DoQuickSort(a, start, pivotIndex - 1);
                DoQuickSort(a, pivotIndex + 1, end);
            }
        }

        int Partition(int start, int end)
        {

            int pivot = a[end];
            int i = start - 1;

            for (int j = start; j < end; j++)
            {
                if (a[j] <= pivot)
                {
                    i++;
                    Swap(i, j);
                }
            }

            Swap(i + 1, end);

            Graphics g = CreateGraphics();
            int x = GraphSP;
            g.FillEllipse(new SolidBrush(Color.Blue), x++, 180, 10, 10);
            Thread.Sleep(2);

            runtimeGraphics.Add(g);

            return i + 1;
        }

        #endregion

        #region HeapSort

        /// <summary>
        /// Konvertuojam masyva ir pakartotinai traukiam maksimalu elementa rusiuoti masyvui
        /// Heapify lygina root su children, jei reikia, pakeicia
        /// </summary>

        void HeapSort()
        {
            Graphics g = CreateGraphics();
            Stopwatch stopwatch = new Stopwatch();
            const int height = 210;

            stopwatch.Start();

            double x = GraphSP;

            int n = a.Length;

            void Heapify(int n, int i)
            {
                int largest = i;
                int left = 2 * i + 1;
                int right = 2 * i + 2;

                if (left < n && a[left] > a[largest])
                    largest = left;

                if (right < n && a[right] > a[largest])
                    largest = right;

                if (largest != i)
                {
                    int swap = a[i];
                    a[i] = a[largest];
                    a[largest] = swap;

                    Heapify(n, largest);
                }
            }

            for (int i = n / 2 - 1; i >= 0; i--)
                Heapify(n, i);

            for (int i = n - 1; i > 0; i--)
            {
                int temp = a[0];
                a[0] = a[i];
                a[i] = temp;

                Heapify(i, 0);
                g.FillEllipse(new SolidBrush(Color.Blue), (int)x, height, 10, 10);
                x += 0.25;
                Thread.Sleep(2);
            }

            runtimeGraphics.Add(g);

            stopwatch.Stop();
            createLabel("HeapSort", stopwatch.ElapsedMilliseconds.ToString() + "ms", height);
        }

        #endregion

        #region ShellSort

        /// <summary>
        /// Panasu i insertion sorta, sokineja zingsniais ir veliau jie mazeja
        /// </summary>

        void ShellSort()
        {
            Graphics g = CreateGraphics();
            Stopwatch stopwatch = new Stopwatch();
            const int height = 240;

            stopwatch.Start();

            double x = GraphSP;

            int n = a.Length;
            for (int gap = n / 2; gap > 0; gap /= 2)
            {
                for (int i = gap; i < n; i++)
                {
                    int temp = a[i];
                    int j;
                    for (j = i; j >= gap && a[j - gap] < temp; j -= gap)
                        a[j] = a[j - gap];

                    a[j] = temp;
                    g.FillEllipse(new SolidBrush(Color.Blue), (int)x, height, 10, 10);
                    x += 0.25;
                    Thread.Sleep(2);
                }
            }

            runtimeGraphics.Add(g);

            stopwatch.Stop();
            createLabel("ShellSort", stopwatch.ElapsedMilliseconds.ToString() + "ms", height);
        }

        #endregion

        #region CombSort

        /// <summary>
        /// Panasu i shell sorta, naudoja tarpus tarp lyginimu
        /// </summary>

        void CombSort()
        {
            Graphics g = CreateGraphics();
            Stopwatch stopwatch = new Stopwatch();
            const int height = 270;

            stopwatch.Start();

            double x = GraphSP;

            int n = a.Length;
            int gap = n;
            bool swapped = true;

            while (gap != 1 || swapped)
            {
                gap = (gap * 10) / 13;
                if (gap < 1)
                    gap = 1;

                swapped = false;

                for (int i = 0; i < n - gap; i++)
                {
                    if (a[i] < a[i + gap])
                    {
                        int temp = a[i];
                        a[i] = a[i + gap];
                        a[i + gap] = temp;
                        swapped = true;
                        g.FillEllipse(new SolidBrush(Color.Blue), (int)x, height, 10, 10);
                        x += 0.25;
                        Thread.Sleep(2);
                    }
                }
            }

            runtimeGraphics.Add(g);

            stopwatch.Stop();
            createLabel("CombSort", stopwatch.ElapsedMilliseconds.ToString() + "ms", height);
        }

        #endregion

        #region CountingSort

        /// <summary>
        /// Skaiciuoja kiekvienos reiksmes pasikartojimus, juos naudojant surusiuoja
        /// </summary>

        void CountingSort()
        {
            Graphics g = CreateGraphics();
            Stopwatch stopwatch = new Stopwatch();
            const int height = 300;

            stopwatch.Start();

            double x = GraphSP;

            int max = a.Max();
            int min = a.Min();
            int range = max - min + 1;

            int[] count = new int[range];

            for (int i = 0; i < a.Length; i++)
            {
                count[a[i] - min]++;
                g.FillEllipse(new SolidBrush(Color.Blue), (int)x, height, 10, 10);
                x += 0.25;
                Thread.Sleep(2);
            }

            for (int i = 1; i < count.Length; i++)
            {
                count[i] += count[i - 1];
            }

            for (int i = a.Length - 1; i >= 0; i--)
            {
                count[a[i] - min]--;
                g.FillEllipse(new SolidBrush(Color.Blue), (int)x, height, 10, 10);
                x += 0.25;
                Thread.Sleep(2);
            }

            runtimeGraphics.Add(g);

            stopwatch.Stop();
            createLabel("CountingSort", stopwatch.ElapsedMilliseconds.ToString() + "ms", height);
        }

        #endregion

        #region Supporting Functions

        void Generator()
        {

            Random r = new Random();
            for (int i = 0; i < NUMBER; i++)
            {
                int t = r.Next(1000);
                a[i] = t;
            }
        }

        void createLabel(string labelName, string timerVal, int y)
        {

            Type labelType = typeof(Label);
            Label newLabel = (Label)Activator.CreateInstance(labelType);
            newLabel.Name = labelName;
            newLabel.Tag = "runtimeLabel";
            newLabel.AutoSize = true;
            newLabel.Text = timerVal;
            newLabel.Location = new Point(GraphSP - 50, y);

            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => this.Controls.Add(newLabel)));
            }
            else
            {
                this.Controls.Add(newLabel);
            }

        }

        void Swap(int i, int j)
        {
            int t = a[i];
            a[i] = a[j];
            a[j] = t;
        }

        void modifyLabel(List<Label> labels, int height)
        {
            foreach (Label label in labels)
            {
                Point loc = label.Location;
                loc.Y = height;

                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => label.Location = loc));
                }
                else
                {
                    label.Location = loc;
                }

                height += 30;
            }
        }

        #endregion

        #region WinForms Controls
        private void button1_Click(object sender, EventArgs e)
        {

            button1.Visible = false;
            sortingEvent.Reset();
            sorting = true;

            List<Thread> sortThreads = new List<Thread>()
            {
                new(new ThreadStart(BubbleSort1)),
                new(new ThreadStart(BubbleSort2)),
                new(new ThreadStart(InsertionSort)),
                new(new ThreadStart(SelectionSort)),
                new(new ThreadStart(MergeSort)),
                new(new ThreadStart(QuickSort)),
                new(new ThreadStart(HeapSort)),
                new(new ThreadStart(ShellSort)),
                new(new ThreadStart(CombSort)),
                new(new ThreadStart(CountingSort))
            };



            foreach (Thread t in sortThreads)
            {

                t.Start();
            }

            button2.Visible = true;



            //Application.Restart();


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            Application.Restart();

        }

        #endregion
    }
}