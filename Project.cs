using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Proje3
{
    public class Part : IEquatable<Part>, IComparable<Part> //Heap düzeni için
    {
        public string duraklar { get; set; }
        public int NormalBisiklet { get; set; }
        public override string ToString()
        {
            return "(" + duraklar + ", " + NormalBisiklet + ")";
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Part objAsPart = obj as Part;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }
        public int CompareTo(Part comparePart)
        {
            // A null value means that this object is greater.
            if (comparePart == null)
                return 1;

            else
                return this.NormalBisiklet.CompareTo(comparePart.NormalBisiklet);
        }
        public override int GetHashCode()
        {
            return NormalBisiklet;
        }
        public bool Equals(Part other)
        {
            if (other == null) return false;
            return (this.NormalBisiklet.Equals(other.NormalBisiklet));
        }
    }
    class Program
    { 
        private static void Quick_Sort(int[] NormalBisiklet,int left,int right)
        {
            if (left < right)
            {
                int pivot = Partition(NormalBisiklet, left, right);
                if (pivot > 1)
                {
                    Quick_Sort(NormalBisiklet, left, pivot - 1);
                }

                if (pivot + 1 < right)
                {
                    Quick_Sort(NormalBisiklet, pivot + 1, right);
                }
            }
        }
        private static int Partition(int[] NormalBisiklet, int left, int right)
        {
            int pivot = NormalBisiklet[left];
            while (true)
            {
                while (NormalBisiklet[left] < pivot)
                {
                    left++;
                }

                while (NormalBisiklet[right] > pivot)
                {
                    right--;
                }
                if (left < right)
                {
                    if (NormalBisiklet[left] == NormalBisiklet[right]) return right;

                    int temp = NormalBisiklet[left];
                    NormalBisiklet[left] = NormalBisiklet[right];
                    NormalBisiklet[right] = temp;
                }
                else
                {
                    return right;
                }
            }
        }
        // Düğüm Sınıfı
        class TreeNode
        {
            public string duraklar;
            public TreeNode leftChild;
            public TreeNode rightChild;
            public void displayNode() { Console.Write("\nDurak Adı: " + duraklar + " "); }
        }

        // Agaç Sınıfı
        class Tree
        {
            private TreeNode root;

            public Tree() { root = null; }

            public TreeNode getRoot()
            { return root; }


            // Agacın inOrder Dolasılması
            public void inOrder(TreeNode localRoot)
            {
                if (localRoot != null)
                {
                    inOrder(localRoot.leftChild);
                    localRoot.displayNode();
                    inOrder(localRoot.rightChild);
                }
            }


            // Agaca bir dügüm eklemeyi saglayan metot
            public void insert(string newdata)
            {
                TreeNode newNode = new TreeNode();
                newNode.duraklar = newdata;
                if (root == null)
                    root = newNode;
                else
                {
                    TreeNode current = root;
                    TreeNode parent;
                    while (true)
                    {
                        parent = current;
                        if (String.Compare(newdata, current.duraklar) < 0)
                        {
                            current = current.leftChild;
                            if (current == null)
                            {
                                parent.leftChild = newNode;
                                return;
                            }
                        }
                        else
                        {
                            current = current.rightChild;
                            if (current == null)
                            {
                                parent.rightChild = newNode;
                                return;
                            }
                        }
                    } // end while
                } // end else not root
            } // end insert()

        } // class Tree




        // Test Sınıfı
        class TreeTest
        {
            static void Main(string[] args)
            {
                Tree agac = new Tree();

                string[] duraklar = { "İnciraltı", "Sahilevleri", "Doğal Yaşam Parkı", "Bostanlı İskele", "Konak", "Karantina", "Köprü", "Lozan", "Montrö" };
                int[] BosPark = { 28, 8, 17, 7, 10, 8, 9, 9, 6 };
                int[] TandemBisiklet = { 2, 1, 1, 0, 2, 0, 1, 0, 2 };
                int[] NormalBisiklet = { 10, 11, 16, 5, 10, 7, 6, 6, 9 };
                int[] BosPark5 = { 23, 3, 12, 2, 5, 3, 4, 4, 1 };//Bospark 5'ten fazla olanlarda. 5 bisiklet eklenerek bospark-5 yapıldı. (hashtable için)

                List<Part> parts = new List<Part>();

                for (int i = 0; i < 9; i++)
                {
                    agac.insert(duraklar[i]);
                }
                agac.inOrder(agac.getRoot());

                for (int i = 0; i < 9; i++)
                {
                    Console.Write("\nDurak Adı: " + duraklar[i] + " " + "BP: " + BosPark[i] + " " + "TB: " + TandemBisiklet[i] + " " + "NB: " + NormalBisiklet[i]);
                    
                }
                Console.WriteLine();

                Hashtable my_hashtable1 = new Hashtable();
                for (int i = 1; i < 9; i++)
                {
                    my_hashtable1.Add(duraklar[i], BosPark[i]);
                }

                Console.WriteLine("\nHashtable:");

                foreach (DictionaryEntry entry in my_hashtable1)
                    Console.WriteLine("{0},{1}",entry.Key,entry.Value);

                Hashtable my_hashtable2 = new Hashtable();
                for (int i = 1; i < 9; i++)
                {
                    my_hashtable2.Add(duraklar[i], BosPark5[i]);
                }

                Console.WriteLine("\nGüncellenen Hashtable:");

                foreach (DictionaryEntry entry in my_hashtable2)
                    Console.WriteLine("{0},{1}", entry.Key, entry.Value);

                for (int i = 0; i < duraklar.Length; i++)
                {
                    parts.Add(new Part() { duraklar = duraklar[i], NormalBisiklet = NormalBisiklet[i] });//öncelikli kuyruk eleman ekleme
                }
                parts.Sort();//Sıralama yapılan yer
                Console.WriteLine();
                parts.Reverse();
                foreach (Part aPart in parts)	
                {
                    Console.WriteLine(aPart);
                }

                List<Part> subList = parts.Take<Part>(3).ToList<Part>();
                Console.WriteLine("\nEn çok 3 istasyon: ");
                foreach (Part aPart in subList)//en çok 3 yeri yazdırma
                {
                    Console.WriteLine(aPart);
                }

                int temp = 0;

                for (int j = 0; j <= NormalBisiklet.Length - 2; j++)
                {
                    for (int i = 0; i <= NormalBisiklet.Length - 2; i++)
                    {
                        if (NormalBisiklet[i] > NormalBisiklet[i + 1])
                        {
                            temp = NormalBisiklet[i + 1];
                            NormalBisiklet[i + 1] = NormalBisiklet[i];
                            NormalBisiklet[i] = temp;
                        }
                    }
                }
                Console.WriteLine("\nSimple Sorted(Bubble Sort):");
                foreach (int p in NormalBisiklet)
                    Console.Write(p + " ");

                Quick_Sort(NormalBisiklet, 0, NormalBisiklet.Length - 1);

                Console.WriteLine();
                Console.WriteLine("\nAdvanced Sorted(Quicksort);");

                foreach(var item in NormalBisiklet)
                {
                    Console.Write(" " + item);
                }
                Console.ReadKey();
            }
        }
    }
}