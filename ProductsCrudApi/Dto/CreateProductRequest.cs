namespace ProductsCrudApi.Dto
{
    public class CreateProductRequest
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public string Producer { get; set; }

    }
}
