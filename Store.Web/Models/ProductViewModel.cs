using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Store.Web.Models;

public class ProductViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O Nome é obrigatório")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "O Preço é obrigatório")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "A Descrição é obrigatória")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "O Estoque é obrigatório")]
    public long Stock { get; set; }

    [Required(ErrorMessage = "A URL da imagem é obrigatória")]
    public string? ImageURL { get; set; }

    public string? CategoryName { get; set; }
    [DisplayName("Categoria")]
    [Required(ErrorMessage = "A Categoria é obrigatória")]
    public int CategoryId { get; set; }
}