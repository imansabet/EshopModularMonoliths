namespace Catalog.Data.Seed;

public static class InitialData
{
    public static IEnumerable<Product> Products =>
    new List<Product>
    {
        Product.Create(new Guid("00f90a50-6c63-4d5a-a586-edb4d89d7938"),"IPhone X",["category1"],"Long Desc","Imagefile",500),
        Product.Create(new Guid("9b9b444c-0222-445b-b6d3-171b9772be88"),"Samsung 10",["category2"],"Long Desc","Imagefile",400),
        Product.Create(new Guid("47466980-47be-4bfd-86e7-18939af0e0ed"),"Huawei Plus",["category1"],"Long Desc","Imagefile",650),
        Product.Create(new Guid("0a012df2-9002-4b3d-b2e0-de6245ba592e"),"Xiaomi Mi",["category2"],"Long Desc","Imagefile",450)
    };
}
