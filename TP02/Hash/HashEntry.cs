namespace TP02.Hash
{
    public class HashEntry
    {
        private int chave;
        private string dados;

        public HashEntry(int chave, string dados)
        {
            this.chave = chave;
            this.dados = dados;
        }

        public int getChave()
        {
            return chave;
        }

        public string getDados()
        {
            return dados;
        }
    }
}
