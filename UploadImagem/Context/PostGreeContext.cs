using Microsoft.EntityFrameworkCore;
using UploadImagem.Models;

namespace UploadImagem.Context
{
    public class PostGreeContext : DbContext
    {
        public PostGreeContext(DbContextOptions<PostGreeContext> options) : base(options) { }

        public DbSet<Loja> Lojas { get; set; }
        public DbSet<ArquivoDigitalizacao> ArquivoDigitalizacoes { get; set; }
    }
}
