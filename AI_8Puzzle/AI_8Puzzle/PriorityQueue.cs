using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_8Puzzle
{
    public class PriorityQueue<Int32, Node> // P priority derecesini V ise değeri temsil eder
    {
        public List<KeyValuePair<Int32, Node>> heap; // Heap veri yapısına uygun olacak şekilde içeriğimizi tutacağımız koleksiyon
        private IComparer<Int32> comparer; // Max-Heap veya Min-Heap' e göre bir uyarlamaya hizmet verebilmek için kullanılacak arayüz referansı
        private const string ioeMessage = "Koleksiyonda hiç eleman yok";

        #region Constructors 

        // P ile gelen tipin varsayılan karşılaştırma kriterine göre bir yol izlenir
        public PriorityQueue()
            : this(Comparer<Int32>.Default)
        {
        }

        // P tipinin karşılaştırma işlevselliğini üstlenen bir IComparer implementasyonu ile bir Construct işlemi söz konusudur
        public PriorityQueue(IComparer<Int32> Comparer)
        {
            if (Comparer == null)
                throw new ArgumentNullException();

            heap = new List<KeyValuePair<Int32, Node>>();
            comparer = Comparer;
        }

        #endregion

        #region Temel Fonksiyonlar

        // Koleksiyona bir veri eklemek için kullanıyoruz
        public void Enqueue(Int32 Priority, Node Value)
        {
            KeyValuePair<Int32, Node> pair = new KeyValuePair<Int32, Node>(Priority, Value);
            heap.Add(pair);

            // Sondan başa doğru yeniden bir sıralama yaptırılır
            LastToFirstControl(heap.Count - 1);
        }

        public KeyValuePair<Int32, Node> Dequeue()
        {
            if (!IsEmpty) // Eğer koleksiyon boş değilse 
            {
                KeyValuePair<Int32, Node> result = heap[0]; // Heap' in Root' undaki elemanı al
                // Dequeue mantıksal olarak ilk elemanı geriye döndürürken aynı zamanda koleksiyondan çıkartmalıdır
                if (heap.Count <= 1) // 1 veya daha az eleman söz konusu ise zaten temizle
                {
                    heap.Clear();
                }
                else // 1 den fazla eleman var ise ilgili elemanı koleksiyondan çıkart
                {
                    heap[0] = heap[heap.Count - 1];
                    heap.RemoveAt(heap.Count - 1);
                    FirstToLastControl(0); // ve koleksiyonu baştan sona yeniden sırala
                }
                return result;
            }
            else
                throw new InvalidOperationException(ioeMessage);
        }

        // Peek operasyonu varsayılan olarak ilk elemanı geriye döndürür ama koleksiyondan çıkartmaz(Dequeue gibi değildir yani)
        public KeyValuePair<Int32, Node> Peek()
        {
            if (!IsEmpty)
                return heap[0];
            else
                throw new InvalidOperationException(ioeMessage);
        }

        public int Count()
        {
            return heap.Count;
        }
        public List<Node> getHeapVariables()
        {
            List<Node> list = new List<Node>();

            for(int i = 0; i < heap.Count; i++)
            {
                list.Add(heap[i].Value);
            }
            return list;
        }
        public List<int> getHeapCosts()
        {
            List<int> list = new List<int>();

            for (int i = 0; i < heap.Count; i++)
            {
                list.Add(Convert.ToInt32(heap[i].Key));
            }
            return list;
        }

        // Koleksiyonda eleman olup olmadığını belirtir
        public bool IsEmpty
        {
            get { return heap.Count == 0; }
        }

        #endregion

        #region Sıralama Fonksiyonları

        private int LastToFirstControl(int Posisiton)
        {
            if (Posisiton >= heap.Count)
                return -1;

            int parentPos;

            while (Posisiton > 0)
            {
                parentPos = (Posisiton - 1) / 2;
                if (comparer.Compare(heap[parentPos].Key, heap[Posisiton].Key) > 0)
                {
                    ExchangeElements(parentPos, Posisiton);
                    Posisiton = parentPos;
                }
                else break;
            }
            return Posisiton;
        }

        private void FirstToLastControl(int Position)
        {
            if (Position >= heap.Count)
                return;

            while (true)
            {
                int smallestPosition = Position;
                int leftPosition = 2 * Position + 1;
                int rightPosition = 2 * Position + 2;
                if (leftPosition < heap.Count &&
                    comparer.Compare(heap[smallestPosition].Key, heap[leftPosition].Key) > 0)
                    smallestPosition = leftPosition;
                if (rightPosition < heap.Count &&
                    comparer.Compare(heap[smallestPosition].Key, heap[rightPosition].Key) > 0)
                    smallestPosition = rightPosition;

                if (smallestPosition != Position)
                {
                    ExchangeElements(smallestPosition, Position);
                    Position = smallestPosition;
                }
                else break;
            }
        }

        private void ExchangeElements(int Position1, int Position2)
        {
            KeyValuePair<Int32, Node> val = heap[Position1];
            heap[Position1] = heap[Position2];
            heap[Position2] = val;
        }

        #endregion
    }
}
