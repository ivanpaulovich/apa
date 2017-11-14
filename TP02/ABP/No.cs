namespace TP02.ABP
{
    public class No
    {
        public double Chave;
        public double Valor;
        public No Esquerda;
        public No Direita;

        public No(double chave, double valor)
        {
            this.Chave = chave;
            this.Valor = valor;
            Esquerda = null;
            Direita = null;
        }
    }
}
