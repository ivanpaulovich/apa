using System;

namespace TP02.Hash
{
    public class HashEnderecamentoAberto
    {
        private int tamanho;
        private HashEntry[] entradas;

        public HashEnderecamentoAberto(int tamanho)
        {
            this.tamanho = tamanho;
            entradas = new HashEntry[tamanho];
            for (int i = 0; i < tamanho; i++)
            {
                entradas[i] = null;
            }
        }

        public string Pesquisar(int chave, out int comparacoes)
        {
            comparacoes = 0;

            int hash = chave % tamanho;
            while (entradas[hash] != null && 
                entradas[hash].getChave() != chave)
            {
                comparacoes++;

                hash = (hash + 1) % tamanho;
            }

            comparacoes++;

            if (entradas[hash] == null)
            {
                throw new Exception($"Não foi possível encontrar a entrada '{chave.ToString()}'");
            }
            else
            {
                return entradas[hash].getDados();
            }
        }

        public void Inserir(int chave, string dados)
        {
            if (!VerificarEspacoAberto())
            {
                throw new Exception($"A tabela já esta cheia");
            }

            int hash = (chave % tamanho);
            while (entradas[hash] != null && entradas[hash].getChave() != chave)
            {
                hash = (hash + 1) % tamanho;
            }

            entradas[hash] = new HashEntry(chave, dados);
        }

        public bool Retirar(int chave)
        {
            int hash = chave % tamanho;

            while (entradas[hash] != null && 
                entradas[hash].getChave() != chave)
            {
                hash = (hash + 1) % tamanho;
            }

            if (entradas[hash] == null)
            {
                return false;
            }
            else
            {
                entradas[hash] = null;
                return true;
            }
        }

        private bool VerificarEspacoAberto()
        {
            bool aberto = false;
            for (int i = 0; i < tamanho; i++)
            {
                if (entradas[i] == null)
                {
                    aberto = true;
                }
            }
            return aberto;
        }
    }
}
