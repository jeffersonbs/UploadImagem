namespace UploadImagem.Models
{
    public class ArquivoDigitalizacao
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Formato { get; set; }
        public byte[] Arquivo { get; set; }
    }
}
