using System;
using System.Collections.Generic;
using System.Text;

namespace TP02
{
    class hashtable
    {
        class hashentry
        {
            int key;
            string data;
            public hashentry(int key, string data)
            {
                this.key = key;
                this.data = data;
            }
            public int getkey()
            {
                return key;
            }
            public string getdata()
            {
                return data;
            }
        }
        private int maxSize; //our table size
        hashentry[] table;
        public hashtable(int maxSize)
        {
            this.maxSize = maxSize;
            table = new hashentry[maxSize];
            for (int i = 0; i < maxSize; i++)
            {
                table[i] = null;
            }
        }
        public string retrieve(int key, out int comparacoes)
        {
            comparacoes = 0;

            int hash = key % maxSize;
            while (table[hash] != null && table[hash].getkey() != key)
            {
                comparacoes++;

                hash = (hash + 1) % maxSize;
            }

            comparacoes++;

            if (table[hash] == null)
            {
                return "nothing found!";
            }
            else
            {
                return table[hash].getdata();
            }
        }
        public void insert(int key, string data)
        {
            if (!checkOpenSpace())//if no open spaces available
            {
                Console.WriteLine("table is at full capacity!");
                return;
            }
            int hash = (key % maxSize);
            while (table[hash] != null && table[hash].getkey() != key)
            {
                hash = (hash + 1) % maxSize;
            }
            table[hash] = new hashentry(key, data);
        }
        private bool checkOpenSpace()//checks for open spaces in the table for insertion
        {
            bool isOpen = false;
            for (int i = 0; i < maxSize; i++)
            {
                if (table[i] == null)
                {
                    isOpen = true;
                }
            }
            return isOpen;
        }
        public bool remove(int key)
        {
            int hash = key % maxSize;
            while (table[hash] != null && table[hash].getkey() != key)
            {
                hash = (hash + 1) % maxSize;
            }
            if (table[hash] == null)
            {
                return false;
            }
            else
            {
                table[hash] = null;
                return true;
            }
        }
        public void print()
        {
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] == null && i <= maxSize)//if we have null entries
                {
                    continue;//dont print them, continue looping
                }
                else
                {
                    Console.WriteLine("{0}", table[i].getdata());
                }
            }
        }
        public void quadraticHashInsert(int key, string data)
        {
            //quadratic probing method
            if (!checkOpenSpace())//if no open spaces available
            {
                Console.WriteLine("table is at full capacity!");
                return;
            }

            int j = 0;
            int hash = key % maxSize;
            while (table[hash] != null && table[hash].getkey() != key)
            {
                j++;
                hash = (hash + j * j) % maxSize;
            }
            if (table[hash] == null)
            {
                table[hash] = new hashentry(key, data);
                return;
            }
        }
        public void doubleHashInsert(int key, string data)
        {
            if (!checkOpenSpace())//if no open spaces available
            {
                Console.WriteLine("table is at full capacity!");
                return;
            }

            //double probing method
            int hashVal = hash1(key);
            int stepSize = hash2(key);

            while (table[hashVal] != null && table[hashVal].getkey() != key)
            {
                hashVal = (hashVal + stepSize * hash2(key)) % maxSize;
            }
            table[hashVal] = new hashentry(key, data);
            return;
        }
        private int hash1(int key)
        {
            return key % maxSize;
        }
        private int hash2(int key)
        {
            //must be non-zero, less than array size, ideally odd
            return 5 - key % 5;
        }
    }
}
