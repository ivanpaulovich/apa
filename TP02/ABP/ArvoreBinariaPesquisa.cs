using System;

namespace TP02.ABP
{
    public class ArvoreBinariaPesquisa
    {
        private No raiz;

        public ArvoreBinariaPesquisa()
        {
            raiz = null;
        }

        public No Pesquisar(double chave, out int contador)
        {
            No no = raiz;
            double comparacao;
            contador = 0;

            while (no != null)
            {
                comparacao = chave - no.Chave;

                contador++;

                if (comparacao == 0)
                    return no;

                contador++;

                if (comparacao < 0)
                    no = no.Esquerda;
                else
                    no = no.Direita;
            }

            throw new Exception($"A chave {chave} não pode ser encontrada.");
        }

        private void Adicionar(No no, ref No arvore)
        {
            if (arvore == null)
                arvore = no;
            else
            {
                double comparacao = no.Chave - arvore.Chave;
                if (comparacao == 0)
                    throw new Exception();

                if (comparacao < 0)
                {
                    Adicionar(no, ref arvore.Esquerda);
                }
                else
                {
                    Adicionar(no, ref arvore.Direita);
                }
            }
        }

        public No Inserir(double chave, double dados)
        {
            No no = new No(chave, dados);

            if (raiz == null)
                raiz = no;
            else
                Adicionar(no, ref raiz);

            return no;
        }

        private No ProcurarPai(double chave, ref No pai)
        {
            No no = raiz;
            pai = null;
            double comparacao;
            while (no != null)
            {
                comparacao = chave - no.Chave;
                if (comparacao == 0) 
                    return no;

                if (comparacao < 0)
                {
                    pai = no;
                    no = no.Esquerda;
                }
                else
                {
                    pai = no;
                    no = no.Direita;
                }
            }

            throw new Exception($"Não foi possível encontrar a entrada '{chave.ToString()}'");
        }

        public No ProcurarSucessor(No inicio, ref No pai)
        {
            pai = inicio;
            inicio = inicio.Direita;
            while (inicio.Esquerda != null)
            {
                pai = inicio;
                inicio = inicio.Esquerda;
            }
            return inicio;
        }

        public void Retirar(double chave)
        {
            No pai = null;
            No noParaExcluir = ProcurarPai(chave, ref pai);
            if (noParaExcluir == null)
                throw new Exception($"Não foi possivel excluir a chave {chave.ToString()}");

            if ((noParaExcluir.Esquerda == null) && 
                (noParaExcluir.Direita == null))
            {
                if (pai == null)
                {
                    raiz = null;
                    return;
                }
                
                if (pai.Esquerda == noParaExcluir)
                    pai.Esquerda = null;
                else
                    pai.Direita = null;
                return;
            }
            
            if (noParaExcluir.Esquerda == null)
            {
                if (pai == null)
                {
                    raiz = noParaExcluir.Direita;
                    return;
                }

                if (pai.Esquerda == noParaExcluir)
                    pai.Direita = noParaExcluir.Direita;
                else
                    pai.Esquerda = noParaExcluir.Direita;
                noParaExcluir = null;
                return;
            }

            if (noParaExcluir.Direita == null)
            {
                if (pai == null)
                {
                    raiz = noParaExcluir.Esquerda;
                    return;
                }

                if (pai.Esquerda == noParaExcluir)
                    pai.Esquerda = noParaExcluir.Esquerda;
                else
                    pai.Direita = noParaExcluir.Esquerda;
                noParaExcluir = null;
                return;
            }

            No sucessor = ProcurarSucessor(noParaExcluir, ref pai);
            No temp = new No(sucessor.Chave, sucessor.Valor);

            if (pai.Esquerda == sucessor)
                pai.Esquerda = null;
            else
                pai.Direita = null;

            noParaExcluir.Chave = temp.Chave;
            noParaExcluir.Valor = temp.Valor;
        }
    }
}
