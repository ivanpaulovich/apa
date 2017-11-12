﻿using SysBio.dataStructures;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace TP02
{
    class Program
    {
        static void GerarNumerosAleatorios(out int[] array, int size)
        {
            array = Enumerable.Range(0, size).ToArray();
            var rnd = new Random();

            for (int i = 0; i < array.Length; ++i)
            {
                int randomIndex = rnd.Next(array.Length);
                int temp = array[randomIndex];
                array[randomIndex] = array[i];
                array[i] = temp;
            }
        }

        static void EscolheNumerosAleatorios(int[] array, out int[] escolhidos, int n)
        {
            List<int> dominio = array.ToList();

            escolhidos = new int[n];
            var rnd = new Random();

            for (int i = 0; i < escolhidos.Length; i++)
            {
                int randomIndex = rnd.Next(dominio.Count);
                escolhidos[i] = dominio[randomIndex];
                dominio.RemoveAt(randomIndex);
            }
        }

        static void Main(string[] args)
        {
            int[] aleatorios10k;
            int[] aleatorios5k;
            int[] aleatorios1k;
            int[] aleatorios100;

            GerarNumerosAleatorios(out aleatorios10k, 10000);

            EscolheNumerosAleatorios(aleatorios10k, out aleatorios5k, 5000);
            EscolheNumerosAleatorios(aleatorios5k, out aleatorios1k, 1000);
            EscolheNumerosAleatorios(aleatorios1k, out aleatorios100, 100);

            var bt10k = new TBinarySTree();
            var bt5k = new TBinarySTree();
            var bt1k = new TBinarySTree();

            var hash10k = new hashtable(10000);
            var hash5k = new hashtable(5000);
            var hash1k = new hashtable(1000);

            PreencherEstruturas(
                aleatorios1k, aleatorios5k, aleatorios10k,
                bt1k, bt5k, bt10k,
                hash1k, hash5k, hash10k);

            TimeSpan tempoHash1k, tempoHash5k, tempoHash10k,
                tempoBST1k, tempoBST5k, tempoBST10k;

            int compHash1k, compHash5k, compHash10k,
                compBST1k, compBST5k, compBST10k;

            BuscarBST(aleatorios100, bt1k, out tempoBST1k, out compBST1k);
            BuscarBST(aleatorios100, bt5k, out tempoBST5k, out compBST5k);
            BuscarBST(aleatorios100, bt10k, out tempoBST10k, out compBST10k);

            BuscarHash(aleatorios100, hash1k, out tempoHash1k, out compHash1k);
            BuscarHash(aleatorios100, hash5k, out tempoHash5k, out compHash5k);
            BuscarHash(aleatorios100, hash10k, out tempoHash10k, out compHash10k);

            Console.WriteLine($"\tTempo(ms)\t\tComparacoes");
            Console.WriteLine($"\tHash\tBST\t\tHash\tBST");
            Console.WriteLine($"1k\t{tempoHash1k.TotalMilliseconds}\t{tempoBST1k.TotalMilliseconds}\t\t{compBST1k.ToString()}\t{compHash1k.ToString()}");
            Console.WriteLine($"5k\t{tempoHash5k.TotalMilliseconds}\t{tempoBST5k.TotalMilliseconds}\t\t{compBST5k.ToString()}\t{compHash5k.ToString()}");
            Console.WriteLine($"10k\t{tempoHash10k.TotalMilliseconds}\t{tempoBST10k.TotalMilliseconds}\t\t{compBST10k.ToString()}\t{compHash10k.ToString()}");

            Console.ReadKey();
        }

        private static void BuscarHash(
            int[] aleatorios100, 
            hashtable hash1k,
            out TimeSpan tempoGasto,
            out int comparacoes)
        {
            comparacoes = 0;

            Stopwatch sw = new Stopwatch();
            sw.Start();

            foreach (var item in aleatorios100)
            {
                int contador;
                var symbol = hash1k.retrieve(item, out contador);
                comparacoes += contador;

                if (symbol == null)
                    throw new Exception("O elemento deveria ter sido encontrado");
            }

            sw.Stop();

            tempoGasto = sw.Elapsed;
        }

        private static void BuscarBST(
            int[] aleatorios, 
            TBinarySTree arvore,
            out TimeSpan tempoGasto,
            out int comparacoes)
        {
            comparacoes = 0;

            Stopwatch sw = new Stopwatch();
            sw.Start();

            foreach (var item in aleatorios)
            {
                int contador;
                TTreeNode symbol = arvore.findSymbol(item, out contador);

                comparacoes += contador;
                
                if (symbol == null)
                    throw new Exception("O elemento deveria ter sido encontrado");
            }

            sw.Stop();

            tempoGasto = sw.Elapsed;
        }
        
        private static void PreencherEstruturas(
            int[] aleatorios1k,
            int[] aleatorios5k,
            int[] aleatorios10k,
            TBinarySTree bt1k,
            TBinarySTree bt5k,
            TBinarySTree bt10k,
            hashtable hash1k,
            hashtable hash5k,
            hashtable hash10k)
        {
            foreach (var item in aleatorios10k)
            {
                bt10k.insert(item, item);
                hash10k.insert(item, item.ToString());
            }

            foreach (var item in aleatorios5k)
            {
                bt5k.insert(item, item);
                hash5k.insert(item, item.ToString());
            }

            foreach (var item in aleatorios1k)
            {
                bt1k.insert(item, item);
                hash1k.insert(item, item.ToString());
            }
        }
    }
}
