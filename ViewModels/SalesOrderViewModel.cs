namespace MormorDagnysInlämning.ViewModels;

public class SalesOrderViewModel
    {
        public DateTime OrderDate { get; set; }
        public IList <ProductViewModel> Products { get; set; }
    }
