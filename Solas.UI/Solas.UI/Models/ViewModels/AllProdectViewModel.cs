using Solas.BL.Models;

namespace Solas.UI.Models.ViewModels
{
    public class AllProdectViewModel
    {
        public IEnumerable<Product> products { get; set; }
        public IEnumerable<Option> options { get; set; }
        public IEnumerable<OptionGroup> optionGroups { get; set; }
        public IEnumerable<Category> categories { get; set; }



    }
}
