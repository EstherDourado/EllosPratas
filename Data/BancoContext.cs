using EllosPratas.Models;
using Microsoft.EntityFrameworkCore;

namespace EllosPratas.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {
        }
        // Defina suas DbSets aqui, por exemplo:
        // public DbSet<SeuModelo> SeusModelos { get; set; }

        public DbSet<ProdutosModel> Produtos { get; set; }
        public DbSet<LojaModel> Lojas { get; set; }
        public DbSet<EnderecoModel> Enderecos { get; set; }
        public DbSet<CaixaModel> Caixa { get; set; }
        public DbSet<EstoqueModel> Estoque { get; set; }
        public DbSet<FuncionarioModel> Funcionarios { get; set; }
        public DbSet<VendaModel> Vendas { get; set; }
        public DbSet<ItensVendaModel> ItensVenda { get; set; }
        public DbSet<PagamentoModel> Pagamentos { get; set; }
        public DbSet<ClienteModel> Clientes { get; set; }
        public DbSet<CategoriaModel> Categorias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProdutosModel>().HasKey(p => p.id_produto);

            modelBuilder.Entity<LojaModel>().HasKey(l => l.id_loja);

            modelBuilder.Entity<EnderecoModel>().HasKey(e => e.id_endereco);

            modelBuilder.Entity<CaixaModel>().HasKey(c => c.id_caixa);

            modelBuilder.Entity<EstoqueModel>().HasKey(e => e.id_estoque);

            modelBuilder.Entity<FuncionarioModel>().HasKey(f => f.id_funcionario);

            modelBuilder.Entity<VendaModel>().HasKey(v => v.id_venda);

            modelBuilder.Entity<ItensVendaModel>().HasKey(iv => iv.id_item_venda);

            modelBuilder.Entity<PagamentoModel>().HasKey(p => p.id_pagamento);

            modelBuilder.Entity<ClienteModel>().HasKey(c => c.id_cliente);

            modelBuilder.Entity<CategoriaModel>().HasKey(c => c.id_categoria);
        }

    }
}

